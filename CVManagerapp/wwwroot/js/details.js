﻿$(document).ready(function () {
    var tempEntries = {};

    $('.addRow').click(function () {
        var button = $(this);
        var tableId = button.data('table-id');
        var templateId = button.data('row-template-id');
        var template = $('#' + templateId).html();

        $('#' + tableId + ' tbody').append(template);
        $('#' + tableId + ' tbody tr:last-child').find('.saveRow').attr('data-template-id', templateId); // Set the templateId
        $('.addRow').prop('disabled', true);
    });

    $(document).on('click', '.cancelRow', function () {
        var row = $(this).closest('tr');
        var tableId = row.closest('table').attr('id');

        row.remove();
        $('.addRow').prop('disabled', false);

        if ($('#' + tableId + ' tbody tr').length === 0) {
            var colspan = $('#' + tableId + ' thead th').length;
            $('#' + tableId + ' tbody').append('<tr id="noEntriesRow"><td colspan="' + colspan + '">No entries found.</td></tr>');
        }
    });

    $(document).on('click', '.saveRow', function () {
        var row = $(this).closest('tr');
        var tableId = row.closest('table').attr('id');
        var tempId = Date.now().toString();
        var templateId = $(this).data('template-id'); // Capture templateId here

        var data = {
            CVId: $('#CVId').val()
        };
        var url = '';
        switch (tableId) {
            case 'educationTable':
                data.Institution = row.find('input[name="Institution"]').val();
                data.Degree = row.find('input[name="Degree"]').val();
                data.FieldOfStudy = row.find('input[name="FieldOfStudy"]').val();
                data.StartDate = formatDateForInput(row.find('input[name="StartDate"]').val());
                data.EndDate = formatDateForInput(row.find('input[name="EndDate"]').val());
                url = '/CV/AddEducation';
                break;
            case 'workExperienceTable':
                data.Company = row.find('input[name="Company"]').val();
                data.Position = row.find('input[name="Position"]').val();
                data.StartDate = formatDateForInput(row.find('input[name="StartDate"]').val());
                data.EndDate = formatDateForInput(row.find('input[name="EndDate"]').val());
                data.Description = row.find('input[name="Description"]').val();
                url = '/CV/AddWorkExperience';
                break;
            case 'skillTable':
                data.Name = row.find('input[name="Name"]').val();
                url = '/CV/AddSkill';
                break;
            case 'projectTable':
                data.Title = row.find('input[name="Title"]').val();
                data.Description = row.find('input[name="Description"]').val();
                data.StartDate = formatDateForInput(row.find('input[name="StartDate"]').val());
                data.EndDate = formatDateForInput(row.find('input[name="EndDate"]').val());
                url = '/CV/AddProject';
                break;
            case 'certificationTable':
                data.Name = row.find('input[name="Name"]').val();
                data.IssueDate = formatDateForInput(row.find('input[name="IssueDate"]').val());
                data.IssuingOrganization = row.find('input[name="IssuingOrganization"]').val();
                url = '/CV/AddCertification';
                break;
            case 'languageTable':
                data.Name = row.find('input[name="Name"]').val();
                data.ProficiencyLevel = row.find('input[name="ProficiencyLevel"]').val();
                url = '/CV/AddLanguage';
                break;
            case 'interestTable':
                data.Name = row.find('input[name="Name"]').val();
                url = '/CV/AddInterest';
                break;
            default:
                alert('Unknown table id: ' + tableId);
                return;
        }

        tempEntries[tempId] = data;

        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: function (response) {
                if (response.success) {
                    var entryId = response.id;
                    tempEntries[tempId].Id = entryId;

                    row.remove();
                    var rowToRemove = document.getElementById('noEntriesRow');
                    if (rowToRemove) {
                        rowToRemove.remove();
                    }
                    var newRow = '<tr>';
                    for (var key in data) {
                        if (key !== 'CVId' && key !== 'Id') {
                            newRow += '<td>' + data[key] + '</td>';
                        }
                    }
                    newRow += '<td><i class="bi bi-pencil-square addEditRow" style="cursor: pointer; margin-right: 5px;"';
                    for (var key in data) {
                        if (key !== 'CVId') {
                            newRow += ' data-' + key.toLowerCase() + '="' + data[key] + '"';
                        }
                    }
                    newRow += ' data-id="' + entryId + '" data-table-id="' + tableId + '" data-row-edit-template-id="' + templateId + '"></i>';
                    newRow += '<i class="bi bi-trash deleteRow" style="cursor: pointer; margin-left: 5px;" data-id="' + entryId + '" data-table-id="' + tableId + '"></i></td>';
                    newRow += '</tr>';
                    $('#' + tableId + ' tbody').append(newRow);
                    $('.addRow').prop('disabled', false);
                    delete tempEntries[tempId];
                } else {
                    var errorHtml = '<ul>';
                    response.errors.forEach(function (error) {
                        errorHtml += '<li>' + error + '</li>';
                    });
                    errorHtml += '</ul>';
                    row.find('.validationErrors').html(errorHtml);
                }
            },
            error: function () {
                alert('An error occurred while saving the entry.');
            }
        });
    });

    $(document).on('click', '.addEditRow', function () {
        var button = $(this);
        var tableId = button.data('table-id');
        var templateId = button.data('row-edit-template-id');
        var template = $('#' + templateId).html();

        var editRow = $(template);
        var dataAttributes = button[0].dataset;
        for (var key in dataAttributes) {
            if (key !== 'tableId' && key !== 'rowEditTemplateId') {
                var input = editRow.find('input[name="' + key.charAt(0).toUpperCase() + key.slice(1) + '"]');
                if (input.length) {
                    input.val(dataAttributes[key]);
                }
            }
        }

        button.closest('tr').after(editRow).remove();
        $('.addRow').prop('disabled', true);
    });

    $(document).on('click', '.cancelEditRow', function () {
        var row = $(this).closest('tr');
        var tableId = row.closest('table').attr('id');
        var originalData = row.data();

        var originalRow = '<tr>';
        for (var key in originalData) {
            if (key !== 'id' && key !== 'tableId' && key !== 'rowEditTemplateId') {
                originalRow += '<td>' + originalData[key] + '</td>';
            }
        }
        originalRow += '<td><i class="bi bi-pencil-square addEditRow" style="cursor: pointer; margin-right: 5px;"';
        for (var key in originalData) {
            originalRow += ' data-' + key + '="' + originalData[key] + '"';
        }
        originalRow += ' data-table-id="' + tableId + '" data-row-edit-template-id="' + templateId + '"></i>';
        originalRow += '<i class="bi bi-trash deleteRow" style="cursor: pointer; margin-left: 5px;" data-id="' + originalData['id'] + '" data-table-id="' + tableId + '"></i></td>';
        originalRow += '</tr>';

        row.after(originalRow).remove();
        $('.addRow').prop('disabled', false);
    });

    $(document).on('click', '.saveEditRow', function () {
        var row = $(this).closest('tr');
        var tableId = row.closest('table').attr('id');
        var templateId = row.find('input[name="Id"]').closest('table').data('row-edit-template-id');

        var data = {
            CVId: $('#CVId').val(),
            Id: row.find('input[name="Id"]').val()
        };
        var url = '';
        switch (tableId) {
            case 'educationTable':
                data.Institution = row.find('input[name="Institution"]').val();
                data.Degree = row.find('input[name="Degree"]').val();
                data.FieldOfStudy = row.find('input[name="FieldOfStudy"]').val();
                data.StartDate = formatDateForInput(row.find('input[name="StartDate"]').val());
                data.EndDate = formatDateForInput(row.find('input[name="EndDate"]').val());
                url = '/CV/EditEducation';
                break;
            case 'workExperienceTable':
                data.Company = row.find('input[name="Company"]').val();
                data.Position = row.find('input[name="Position"]').val();
                data.StartDate = formatDateForInput(row.find('input[name="StartDate"]').val());
                data.EndDate = formatDateForInput(row.find('input[name="EndDate"]').val());
                data.Description = row.find('input[name="Description"]').val();
                url = '/CV/EditWorkExperience';
                break;
            case 'skillTable':
                data.Name = row.find('input[name="Name"]').val();
                url = '/CV/EditSkill';
                break;
            case 'projectTable':
                data.Title = row.find('input[name="Title"]').val();
                data.Description = row.find('input[name="Description"]').val();
                data.StartDate = formatDateForInput(row.find('input[name="StartDate"]').val());
                data.EndDate = formatDateForInput(row.find('input[name="EndDate"]').val());
                url = '/CV/EditProject';
                break;
            case 'certificationTable':
                data.Name = row.find('input[name="Name"]').val();
                data.IssueDate = formatDateForInput(row.find('input[name="IssueDate"]').val());
                data.IssuingOrganization = row.find('input[name="IssuingOrganization"]').val();
                url = '/CV/EditCertification';
                break;
            case 'languageTable':
                data.Name = row.find('input[name="Name"]').val();
                data.ProficiencyLevel = row.find('input[name="ProficiencyLevel"]').val();
                url = '/CV/EditLanguage';
                break;
            case 'interestTable':
                data.Name = row.find('input[name="Name"]').val();
                url = '/CV/EditInterest';
                break;
            default:
                alert('Unknown table id: ' + tableId);
                return;
        }

        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: function (response) {
                if (response.success) {
                    row.remove();
                    var newRow = '<tr>';
                    for (var key in data) {
                        if (key !== 'CVId' && key !== 'Id') {
                            newRow += '<td>' + data[key] + '</td>';
                        }
                    }
                    newRow += '<td><i class="bi bi-pencil-square addEditRow" style="cursor: pointer; margin-right: 5px;"';
                    for (var key in data) {
                        if (key !== 'CVId') {
                            newRow += ' data-' + key.toLowerCase() + '="' + data[key] + '"';
                        }
                    }
                    newRow += ' data-id="' + data.Id + '" data-table-id="' + tableId + '" data-row-edit-template-id="' + templateId + '"></i>';
                    newRow += '<i class="bi bi-trash deleteRow" style="cursor: pointer; margin-left: 5px;" data-id="' + data.Id + '" data-table-id="' + tableId + '"></i></td>';
                    newRow += '</tr>';
                    $('#' + tableId + ' tbody').append(newRow);
                    $('.addRow').prop('disabled', false);
                } else {
                    var errorHtml = '<ul>';
                    response.errors.forEach(function (error) {
                        errorHtml += '<li>' + error + '</li>';
                    });
                    errorHtml += '</ul>';
                    row.find('.validationErrors').html(errorHtml);
                }
            },
            error: function () {
                alert('An error occurred while saving the entry.');
            }
        });
    });

    $(document).on('click', '.deleteRow', function () {
        var button = $(this);
        var id = button.data('id');
        var tableId = button.data('table-id');
        var url = '';

        switch (tableId) {
            case 'educationTable':
                url = '/CV/DeleteEducation';
                break;
            case 'workExperienceTable':
                url = '/CV/DeleteWorkExperience';
                break;
            case 'skillTable':
                url = '/CV/DeleteSkill';
                break;
            case 'projectTable':
                url = '/CV/DeleteProject';
                break;
            case 'certificationTable':
                url = '/CV/DeleteCertification';
                break;
            case 'languageTable':
                url = '/CV/DeleteLanguage';
                break;
            case 'interestTable':
                url = '/CV/DeleteInterest';
                break;
            default:
                alert('Unknown table id: ' + tableId);
                return;
        }

        if (confirm('Are you sure you want to delete this entry?')) {
            $.ajax({
                type: 'POST',
                url: url,
                data: { id: id },
                success: function (response) {
                    if (response.success) {
                        button.closest('tr').remove();
                        if ($('#' + tableId + ' tbody tr').length === 0) {
                            var colspan = $('#' + tableId + ' thead th').length;
                            $('#' + tableId + ' tbody').append('<tr id="noEntriesRow"><td colspan="' + colspan + '">No entries found.</td></tr>');
                        }
                    } else {
                        alert('An error occurred while deleting the entry: ' + response.message);
                    }
                },
                error: function () {
                    alert('An error occurred while deleting the entry.');
                }
            });
        }
    });

    function formatDate(dateStr) {
        if (!dateStr) return '';
        var date = new Date(dateStr);
        var day = ("0" + date.getDate()).slice(-2);
        var month = ("0" + (date.getMonth() + 1)).slice(-2);
        var year = date.getFullYear();
        return day + "/" + month + "/" + year;
    }

    function formatDateForInput(dateString) {
        if (!dateString) return '';
        var date = new Date(dateString);
        var day = ("0" + date.getDate()).slice(-2);
        var month = ("0" + (date.getMonth() + 1)).slice(-2);
        var year = date.getFullYear();
        return `${year}-${month}-${day}`;
    }
});
