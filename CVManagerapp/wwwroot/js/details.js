document.addEventListener("DOMContentLoaded", function () {
    var isRowAdding = false;

    function addRow(tableId, rowTemplateId) {
        if (isRowAdding) {
            return; // Don't add another row if a row is already being added
        }

        isRowAdding = true;

        var tableBody = document.querySelector("#" + tableId + " tbody");
        var newRow = document.querySelector("#" + rowTemplateId).content.cloneNode(true);
        tableBody.appendChild(newRow);

        // Disable addRow buttons while a row is being added
        document.querySelectorAll(".addRow").forEach(function (button) {
            button.disabled = true;
        });
    }

    document.querySelectorAll(".addRow").forEach(function (button) {
        button.addEventListener("click", function () {
            var tableId = this.getAttribute("data-table-id");
            var rowTemplateId = this.getAttribute("data-row-template-id");
            addRow(tableId, rowTemplateId);
        });
    });

    // Enable addRow buttons when a row is saved or canceled
    document.addEventListener("click", function (event) {
        if (event.target.classList.contains("saveRow") || event.target.classList.contains("cancelRow")) {
            isRowAdding = false;
            document.querySelectorAll(".addRow").forEach(function (button) {
                button.disabled = false;
            });
        }
    });

    // Attach event listener for cancel buttons
    document.addEventListener("click", function (event) {
        if (event.target.classList.contains("cancelRow")) {
            var row = event.target.closest("tr");
            row.remove();

            isRowAdding = false;
            document.querySelectorAll(".addRow").forEach(function (button) {
                button.disabled = false;
            });
        }
    });
});
