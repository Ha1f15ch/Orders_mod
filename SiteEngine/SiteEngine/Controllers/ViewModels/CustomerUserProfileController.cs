using DtoModelsProj;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Models;
using SiteEngine.CommandsAndHandlers.Commands.OrderCommands;
using SiteEngine.CommandsAndHandlers.Commands.UserMetadata;
using SiteEngine.Middlewares;
using SiteEngine.Models.CustomerUserProfileModels;
using SiteEngine.Models.OrderModels;
using SiteEngine.Models.UserProfiles;

namespace SiteEngine.Controllers.ViewModels
{
    [ServiceFilter(typeof(AuthorizeAttributeFilter))]
    [Route("views/customer-main/")]
    public class CustomerUserProfileController : BaseController
    {
        private readonly IMediator mediator;

        public CustomerUserProfileController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index() => RedirectToAction("AllMyOrders");

        [HttpGet("profile")]
        public async Task<IActionResult> ProfileCustomer()
        {
            return View();
        }

        [HttpGet("orders")]
        public async Task<IActionResult> AllMyOrders()
        {
            var commandForGetCookieString = new UserIdMetadataCommand()
            {
                CookieString = HttpContext.Request.Cookies["access_token"]
            };

            var userIdByCookieString = await mediator.Send(commandForGetCookieString);

            if(userIdByCookieString > 0)
            {
                var commandForGetAllOrdersForCustomer = new GetAllOrdersForCustomerCommand
                {
                    UserId = userIdByCookieString,
                };

                var resultCommand = await mediator.Send(commandForGetAllOrdersForCustomer);

                if(!resultCommand.HasError)
                {
                    var model = new ListOrdersForCustomer
                    {
                        ListCustomerOrders = resultCommand.ListOrders,
                        UserProfile = resultCommand.UserProfile,
                        CustomerProfile = resultCommand.CustomerProfile,
                        ListCustomerOrderPriorities = resultCommand.ListOrderPriorities,
                        ListCustomerOrderStatuses = resultCommand.ListOrderStatuses,
                        HasError = false,
                        ErrorMessage = null
                    };

                    return View(model);
                }

                return View(new ListOrdersForCustomer
                {
                    ListCustomerOrders = resultCommand.ListOrders,
                    UserProfile = resultCommand.UserProfile,
                    CustomerProfile = resultCommand.CustomerProfile,
                    ListCustomerOrderPriorities = resultCommand.ListOrderPriorities,
                    ListCustomerOrderStatuses = resultCommand.ListOrderStatuses,
                    HasError = true,
                    ErrorMessage = (userIdByCookieString <= 0) ? "Указаны некорректные вводные данные - userId" : "Ошибка при получении данных о заказах"
                });
            }

            return RedirectToAction("ProfileCustomer");
        }

        [HttpGet("orders/filtered")]
        public async Task<IActionResult> AllMyOrders(
                DateOnly dateCreateStart, 
                DateOnly dateCreateEnd, 
                DateOnly dateCanceledStart,
                DateOnly dateCanceledEnd,
                string? statusesId,
                string? prioritiesId)
        {
            var commandForGetCookieString = new UserIdMetadataCommand()
            {
                CookieString = HttpContext.Request.Cookies["access_token"]
            };

            var userIdByCookieString = await mediator.Send(commandForGetCookieString);

            var filterCommand = new FilteredOrderListCommand
            {
                UserId = userIdByCookieString,
                startCreateD = dateCreateStart,
                endCreateD = dateCreateEnd,
                startDeleteD = dateCanceledStart,
                endDeleteD = dateCanceledEnd,
                listStatus = statusesId,
                listPriority = prioritiesId,
                isCustomer = true,
            };

            var resultFilteredCommand = await mediator.Send(filterCommand);

            if(!resultFilteredCommand.HasError)
            {
                var model = new ListOrdersForCustomer
                {
                    ListCustomerOrders = resultFilteredCommand.ListOrders,
                    UserProfile = resultFilteredCommand.UserProfile,
                    CustomerProfile = resultFilteredCommand.CustomerProfile,
                    ListCustomerOrderPriorities = resultFilteredCommand.ListOrderPriorities,
                    ListCustomerOrderStatuses = resultFilteredCommand.ListOrderStatuses,
                    HasError = resultFilteredCommand.HasError,
                    ErrorMessage = resultFilteredCommand.ErrorMessage
                };

                return View(model);
            }

            return View(new ListOrdersForCustomer
            {
                ListCustomerOrders = resultFilteredCommand.ListOrders,
                UserProfile = resultFilteredCommand.UserProfile,
                CustomerProfile = resultFilteredCommand.CustomerProfile,
                ListCustomerOrderPriorities = resultFilteredCommand.ListOrderPriorities,
                ListCustomerOrderStatuses = resultFilteredCommand.ListOrderStatuses,
                HasError = resultFilteredCommand.HasError,
                ErrorMessage = resultFilteredCommand.ErrorMessage
            });
        }

        [HttpGet("orders/id")]
        public async Task<IActionResult> OrderById(int id)
        {
            if(id > 0)
            {
                var commandForGetCookieString = new UserIdMetadataCommand()
                {
                    CookieString = HttpContext.Request.Cookies["access_token"]
                };

                var userIdByCookieString = await mediator.Send(commandForGetCookieString);

                var commandForGetOrderinfo = new GetOrderByOrderIdCommand
                {
                    OrderId = id,
                    Userid = userIdByCookieString
                };

                var resultCommand = await mediator.Send(commandForGetOrderinfo);

                if(!resultCommand.HasError && userIdByCookieString > 0)
                {
                    var model = new OrderModelForCustomer
                    {
                        Orderid = resultCommand.Order.Id,
                        OrderTitleName = resultCommand.Order.TitleName,
                        OrderAdress = resultCommand.Order.Adress,
                        OrderDescription = resultCommand.Order.Description,
                        OrderDayToDelay = resultCommand.Order.DayToDelay,
                        OrderContactInformation = resultCommand.Order.ContactInformation,
                        CreatedUserProfile = resultCommand.Customer_UserProfile,
                        UserAssignerUserProfile = resultCommand.Employer_UserProfile,
                        OrderStatus = resultCommand.OrderStatus.Description,
                        OrderPriority = resultCommand.OrderPriority.Description,
                        DateCreated = resultCommand.Order.DateCreated,
                        DateUpdated = resultCommand.Order.DateUpdated,
                    };

                    return View(model);
                }
            }

            return RedirectToAction("AllMyOrders");
        }

        [HttpGet("new-order")]
        public async Task<IActionResult> CreateNewOrder()
        {
            var getListOrderPriorityCommand = new GetOrderPriorityCommand();

            var listOrderPriority = await mediator.Send(getListOrderPriorityCommand);

            CommonModelForCreateNewOrder model = new CommonModelForCreateNewOrder
            {
                CreateNewOrderModel = new CreateNewOrderModel(),
                OrderPriorities = listOrderPriority
            };

            return View(model);
        }

        private bool CustomValidate(CommonModelForCreateNewOrder model)
        {
            var listModelState = ModelState.ToList();
            Dictionary<string?, object?> listCurrentKeys = new Dictionary<string, object>();
            List<Object> validStates = new List<Object>();
            foreach (var el_modelState in listModelState)
            {
                if (!el_modelState.Key.Contains("OrderPriorities"))
                {
                    listCurrentKeys.Add(el_modelState.Key, el_modelState.Value.ValidationState);
                }
            }

            var values = listCurrentKeys.Values;

            if (!values.Contains("Invalid"))
            {
                return true;
            }

            return false;
        }

        [HttpPost("new-order")]
        public async Task<IActionResult> CreateNewOrder(CommonModelForCreateNewOrder model)
        {
            if (CustomValidate(model))
            {
                var commandForGetCookieString = new UserIdMetadataCommand()
                {
                    CookieString = HttpContext.Request.Cookies["access_token"]
                };

                var userIdByCookieString = await mediator.Send(commandForGetCookieString);

                if(userIdByCookieString > 0)
                {
                    var newOrderDtoModel = new CreateOrderDto
                    {
                        Dto_TitleName = model.CreateNewOrderModel.TitleName,
                        Dto_Adress = model.CreateNewOrderModel.Adress,
                        Dto_Description = model.CreateNewOrderModel.Description,
                        Dto_DayToDelay = model.CreateNewOrderModel.DayToDelay,
                        Dto_ContactInformation = model.CreateNewOrderModel.ContactInformation,
                        Dto_UserIdCreated = userIdByCookieString,
                        Dto_OrderPriorityId = model.CreateNewOrderModel.OrderPriorityId,
                    };

                    var commandForCreateNewOrder = new CreateOrderAndReturnOrderIdCommand
                    {
                        OrderDto = newOrderDtoModel,
                    };

                    var resultCommand = await mediator.Send(commandForCreateNewOrder);

                    if(resultCommand.IsCreated)
                    {
                        return RedirectToAction("OrderById", "CustomerUserProfile", new {id = resultCommand.OrderId});
                    }

                    Console.WriteLine("Создать заказ с заданными параметрами не получлиось.");
                }
            }

            Console.WriteLine("Повторно отправляем данные в View");
            var getListOrderPriorityCommand = new GetOrderPriorityCommand();
            model.OrderPriorities = await mediator.Send(getListOrderPriorityCommand);

            return View(model);
        }
    }
}
