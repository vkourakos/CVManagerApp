$(document).ready(function () {
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

        var data = {
            CVId: $('#CVId').val()
        };
        var url = '';
        switch (tableId) {
            case 'educationTable':
                data.Institution = row.find('input[name="Institution"]').val();
                data.Degree = row.find('input[name="Degree"]').val();
                data.FieldOfStudy = row.find('input[name="FieldOfStudy"]').val();
                data.StartDate = row.find('input[name="StartDate"]').val();
                data.EndDate = row.find('input[name="EndDate"]').val();
                url = '/CV/AddEducation';
                break;
            case 'workExperienceTable':
                data.Company = row.find('input[name="Company"]').val();
                data.Position = row.find('input[name="Position"]').val();
                data.StartDate = row.find('input[name="StartDate"]').val();
                data.EndDate = row.find('input[name="EndDate"]').val();
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
                data.StartDate = row.find('input[name="StartDate"]').val();
                data.EndDate = row.find('input[name="EndDate"]').val();
                url = '/CV/AddProject';
                break;
            case 'certificationTable':
                data.Name = row.find('input[name="Name"]').val();
                data.IssueDate = row.find('input[name="IssueDate"]').val();
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

        $.ajax({
            type: 'POST',
            url: url,
            data: data,
            success: function (response) {
                if (response.success) {
                    row.remove();
                    var rowToRemove = document.getElementById('noEntriesRow');
                    if (rowToRemove) {
                        rowToRemove.remove();
                    }
                    var newRow = '<tr>';
                    for (var key in data) {
                        if (key !== 'CVId') {
                            newRow += '<td>' + data[key] + '</td>';
                        }
                    }
                    newRow += '<td></td></tr>';
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

});