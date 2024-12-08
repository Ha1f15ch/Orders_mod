document.addEventListener("DOMContentLoaded", () => {

    var checkboxesStatus = document.querySelectorAll('input[name="statusId"]');
    var checkboxesPriority = document.querySelectorAll('input[name="priorityId"]');

    checkboxesStatus.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            var selectedStatuses = [];
            var checkedCheckboxesStatuses = document.querySelectorAll('input[name="statusId"]:checked');

            checkedCheckboxesStatuses.forEach(function (checkedCheckboxStatus) {
                var statusId = checkedCheckboxStatus.id;
                if (!selectedStatuses.includes(statusId)) {
                    selectedStatuses.push(statusId);
                }
            });

            var allCheckboxesStatuses = document.querySelectorAll('input[name="statusId"]');
            allCheckboxesStatuses.forEach(function (checkbox) {
                var statusId = checkbox.id;
                if (!checkbox.checked) {
                    var index = selectedStatuses.lastIndexOf(statusId);
                    if (index > -1) {
                        selectedStatuses.splice(index, 1)
                    }
                }
            });

            document.getElementById('statusesId').value = selectedStatuses.join(',');
        });
    });

    checkboxesPriority.forEach(function (checkbox) {
        checkbox.addEventListener('change', function () {
            var selectedPriorities = [];
            var checkedCheckboxesPriorities = document.querySelectorAll('input[name="priorityId"]:checked');

            checkedCheckboxesPriorities.forEach(function (checkedCheckboxPriority) {
                var priorityId = checkedCheckboxPriority.id;
                if (!selectedPriorities.includes(priorityId)) {
                    selectedPriorities.push(priorityId);
                }
            });

            var allCheckboxesPriorities = document.querySelectorAll('input[name="priorityId"]');
            allCheckboxesPriorities.forEach(function (checkbox) {
                var priorityId = checkbox.id;
                if (!checkbox.checked) {
                    var index = selectedPriorities.lastIndexOf(priorityId);
                    if (index > -1) {
                        selectedPriorities.splice(index, 1)
                    }
                }
            });

            document.getElementById('prioritiesId').value = selectedPriorities.join(',');
            console.log(document.getElementById('prioritiesId').value)
        });
    });

    window.onload = function () {
        if (localStorage.getItem("startDate")) {
            document.getElementById("dateCreateStart").value = localStorage.getItem("startDate");
        }
        if (localStorage.getItem("endDate")) {
            document.getElementById("dateCreateEnd").value = localStorage.getItem("endDate");
        }
        if (localStorage.getItem("cancelStartDate")) {
            document.getElementById("dateCanceledStart").value = localStorage.getItem("cancelStartDate");
        }
        if (localStorage.getItem("cancelEndDate")) {
            document.getElementById("dateCanceledEnd").value = localStorage.getItem("cancelEndDate");
        }
        if (localStorage.getItem("statusData")) {
            WriteStringForStatus(localStorage.getItem("statusData").split(','))
        }
        if (localStorage.getItem("priorityData")) {
            WriteStringForPriority(localStorage.getItem("priorityData").split(','))
        }
    }

    document.getElementById("CustomFilterForm").addEventListener("submit", function () {
        const startDate = document.getElementById("dateCreateStart").value;
        const endDate = document.getElementById("dateCreateEnd").value;
        const cancelStartDate = document.getElementById("dateCanceledStart").value;
        const cancelEndDate = document.getElementById("dateCanceledEnd").value;
        const statusData = readStringForStatus()
        const priorityData = readStringForPriority()

        localStorage.setItem("startDate", startDate);
        localStorage.setItem("endDate", endDate);
        localStorage.setItem("cancelStartDate", cancelStartDate);
        localStorage.setItem("cancelEndDate", cancelEndDate);
        localStorage.setItem("statusData", statusData);
        localStorage.setItem("priorityData", priorityData);
    });
});

function WriteStringForStatus(args) {
    console.log(args)
    var allStatus = document.querySelectorAll('input[name="statusId"]')

    Array.from(allStatus).forEach(function (checkbox) {
        if (args.includes(checkbox.id)) {
            checkbox.checked = true;
            document.querySelector('input[name="statusesId"]').value += ',' + checkbox.id
        }
        else {
            checkbox.checked = false;
        }
    })
}

function WriteStringForPriority(args) {
    var allPriority = document.querySelectorAll('input[name="priorityId"]')

    Array.from(allPriority).forEach(function (checkbox) {
        if (args.includes(checkbox.id)) {
            checkbox.checked = true;
            document.querySelector('input[name="prioritiesId"]').value += ',' + checkbox.id
        }
        else {
            checkbox.checked = false;
        }
    })
}

function readStringForStatus() {
    return document.querySelector('input[name="statusesId"]').value
}

function readStringForPriority() {
    return document.querySelector('input[name="prioritiesId"]').value
}
