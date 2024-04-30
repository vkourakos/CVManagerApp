document.addEventListener("DOMContentLoaded", function () {
    function addRow(tableId, rowTemplateId) {
        var tableBody = document.querySelector("#" + tableId + " tbody");
        var newRow = document.querySelector("#" + rowTemplateId).content.cloneNode(true);
        tableBody.appendChild(newRow);
    }

    document.querySelectorAll(".addRow").forEach(function (button) {
        button.addEventListener("click", function () {
            var tableId = this.getAttribute("data-table-id");
            var rowTemplateId = this.getAttribute("data-row-template-id");
            addRow(tableId, rowTemplateId);
        });
    });
});
