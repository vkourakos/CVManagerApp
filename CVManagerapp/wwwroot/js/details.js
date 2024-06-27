﻿$(document).ready(function () {
    var tempEntries = {};

    $('.addRow').click(function () {
        var button = $(this);
        var tableId = button.data('table-id');
        var templateId = button.data('row-template-id');
        var template = $('#' + templateId).html();

        $('#' + tableId + ' tbody').append(template);
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
        var tempId = Date.now().toString(); // Temporary ID for the new entry

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

        tempEntries[tempId] = data; // Store the data temporarily

        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: function (response) {
                if (response.success) {
                    var entryId = response.id; // Get the actual ID from the server response
                    tempEntries[tempId].Id = entryId; // Update the temporary entry with the actual ID

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
                    newRow += '<td><i class="bi bi-pencil-square addEditRow" style="cursor: pointer;" data-id="' + entryId + '" data-institution="' + data.Institution + '" data-degree="' + data.Degree + '" data-field-of-study="' + data.FieldOfStudy + '" data-start-date="' + data.StartDate + '" data-end-date="' + data.EndDate + '" data-table-id="' + tableId + '" data-row-edit-template-id="educationRowEditTemplate"></i></td></tr>';
                    $('#' + tableId + ' tbody').append(newRow);
                    $('.addRow').prop('disabled', false);
                    delete tempEntries[tempId]; // Remove the temporary entry after adding the actual entry
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

        var id = button.data('id');
        var institution = button.data('institution');
        var degree = button.data('degree');
        var fieldOfStudy = button.data('field-of-study');
        var startDate = button.data('start-date');
        var endDate = button.data('end-date');

        var editRow = $(template);
        editRow.find('input[name="Id"]').val(id);
        editRow.find('input[name="Institution"]').val(institution);
        editRow.find('input[name="Degree"]').val(degree);
        editRow.find('input[name="FieldOfStudy"]').val(fieldOfStudy);
        editRow.find('input[name="StartDate"]').val(formatDateForInput(startDate));
        editRow.find('input[name="EndDate"]').val(formatDateForInput(endDate));

        button.closest('tr').after(editRow).remove();
        $('.addRow').prop('disabled', true);
    });

    $(document).on('click', '.cancelEditRow', function () {
        var row = $(this).closest('tr');
        var tableId = row.closest('table').attr('id');

        var institution = row.find('input[name="Institution"]').val();
        var degree = row.find('input[name="Degree"]').val();
        var fieldOfStudy = row.find('input[name="FieldOfStudy"]').val();
        var startDate = row.find('input[name="StartDate"]').val();
        var endDate = row.find('input[name="EndDate"]').val();

        var originalRow = '<tr>';
        originalRow += '<td>' + institution + '</td>';
        originalRow += '<td>' + degree + '</td>';
        originalRow += '<td>' + fieldOfStudy + '</td>';
        originalRow += '<td>' + formatDate(startDate) + '</td>';
        originalRow += '<td>' + formatDate(endDate) + '</td>';
        originalRow += '<td><i class="bi bi-pencil-square addEditRow" style="cursor: pointer;" data-id="' + row.find('input[name="Id"]').val() + '" data-institution="' + institution + '" data-degree="' + degree + '" data-field-of-study="' + fieldOfStudy + '" data-start-date="' + startDate + '" data-end-date="' + endDate + '" data-table-id="' + tableId + '" data-row-edit-template-id="educationRowEditTemplate"></i></td>';
        originalRow += '</tr>';

        row.after(originalRow).remove();
        $('.addRow').prop('disabled', false);
    });

    $(document).on('click', '.saveEditRow', function () {
        var row = $(this).closest('tr');
        var tableId = row.closest('table').attr('id');

        var data = {
            CVId: $('#CVId').val()
        };
        var url = '';
        switch (tableId) {
            case 'educationTable':
                data.Id = row.find('input[name="Id"]').val();
                data.Institution = row.find('input[name="Institution"]').val();
                data.Degree = row.find('input[name="Degree"]').val();
                data.FieldOfStudy = row.find('input[name="FieldOfStudy"]').val();
                data.StartDate = formatDateForInput(row.find('input[name="StartDate"]').val());
                data.EndDate = formatDateForInput(row.find('input[name="EndDate"]').val());
                url = '/CV/EditEducation';
                break;
            case 'workExperienceTable':
                data.Id = row.find('input[name="Id"]').val();
                data.Company = row.find('input[name="Company"]').val();
                data.Position = row.find('input[name="Position"]').val();
                data.StartDate = formatDateForInput(row.find('input[name="StartDate"]').val());
                data.EndDate = formatDateForInput(row.find('input[name="EndDate"]').val());
                data.Description = row.find('input[name="Description"]').val();
                url = '/CV/EditWorkExperience';
                break;
            case 'skillTable':
                data.Id = row.find('input[name="Id"]').val();
                data.Name = row.find('input[name="Name"]').val();
                url = '/CV/EditSkill';
                break;
            case 'projectTable':
                data.Id = row.find('input[name="Id"]').val();
                data.Title = row.find('input[name="Title"]').val();
                data.Description = row.find('input[name="Description"]').val();
                data.StartDate = formatDateForInput(row.find('input[name="StartDate"]').val());
                data.EndDate = formatDateForInput(row.find('input[name="EndDate"]').val());
                url = '/CV/EditProject';
                break;
            case 'certificationTable':
                data.Id = row.find('input[name="Id"]').val();
                data.Name = row.find('input[name="Name"]').val();
                data.IssueDate = formatDateForInput(row.find('input[name="IssueDate"]').val());
                data.IssuingOrganization = row.find('input[name="IssuingOrganization"]').val();
                url = '/CV/EditCertification';
                break;
            case 'languageTable':
                data.Id = row.find('input[name="Id"]').val();
                data.Name = row.find('input[name="Name"]').val();
                data.ProficiencyLevel = row.find('input[name="ProficiencyLevel"]').val();
                url = '/CV/EditLanguage';
                break;
            case 'interestTable':
                data.Id = row.find('input[name="Id"]').val();
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
                    newRow += '<td><i class="bi bi-pencil-square addEditRow" style="cursor: pointer;" data-id="' + data.Id + '" data-institution="' + data.Institution + '" data-degree="' + data.Degree + '" data-field-of-study="' + data.FieldOfStudy + '" data-start-date="' + data.StartDate + '" data-end-date="' + data.EndDate + '" data-table-id="' + tableId + '" data-row-edit-template-id="educationRowEditTemplate"></i></td>';
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
