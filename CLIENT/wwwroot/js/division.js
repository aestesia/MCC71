﻿var table;

$(document).ready(function () {
    table = $('#tableDivision').DataTable({
        ajax: {
            url: 'https://localhost:44393/api/Division',
            type: 'GET',
            dataType: 'json'
        },
        columns: [
            {
                data: null,
                render: function (data, type, row, meta) {
                    return meta.row + meta.settings._iDisplayStart + 1;
                }
            },
            {
                data: 'id'
            },
            {
                data: "name"
            },
            {
                data: null,
                render: function (data, type, row, meta) {
                    return `
                        <button type="button" id="detailButton" class="btn btn-primary" onclick="detailDiv('${data.id}')" data-toggle="modal" data-target="#detailModal">Edit</button> |
                        <button type="button" id="deleteButton" class="btn btn btn-danger" data-toggle="modal" data-id="${data.id}" data-target="#deleteModal">Delete</button>`
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            {
                text: 'Create New',
                action: function (e, node, config) {
                    $('#createDivModal').modal('show')
                }
            },
            'spacer',
            {
                extends: 'copy',
                text: 'Copy to Clipboard',
            },
            'spacer',
            {
                extends: 'excel',
                text: 'Export to Excel',
            },
            'spacer',
            {
                extends: 'pdf',
                text: 'Export to PDF',
            }
        ],
    });

});

//CREATE
function createDiv() {

    const divisionName = $("#divisionNameCreate").val();

    const data = {
        name: divisionName
    }
    
    $.ajax({
        url: 'https://localhost:44393/api/Division',
        type: 'POST',
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(data),
        success: function () {
            alert("Add Data Successfull");
            location.reload();
        }
    })

}

//DETAIL & EDIT
function detailDiv(id) {
    $.ajax({
        url: `https://localhost:44393/api/Division/${id}`,
        type: "GET",
        dataType: 'json'
    }).done((res) => {
        let tempDetail =
            `<form>
                <div>
                    <label for="id">ID:</label><br>
                    <input type="text" id="divId" value="${res.data.id}" readonly></input><br>
                </div>
                <div>
                    <label for="name">Division Name:</label><br>
                    <input type="text" id="divName" value="${res.data.name}"></input><br><br>
                </div>
                <button type="button" class="btn btn-primary" onclick="editDiv()">Edit</button>
            </form>`;
        $("#detailBody").html(tempDetail);
    });
}

function editDiv() {
    const divId = $("#divId").val();
    const divName = $("#divName").val();

    const data = {
        id: divId,
        name: divName
    }

    console.log(data);

    $.ajax({
        url: `https://localhost:44393/api/Division`,
        type: "PUT",
        contentType: 'application/json',
        dataType: 'json',
        data: JSON.stringify(data),
        success: function () {
            alert("Edit Data Successfull");
            location.reload();
        }
    })
}


//DELETE 
$("#deleteModal").on('show.bs.modal', function (e) {
    var triggerLink = $(e.relatedTarget);
    var id = triggerLink.data("id");
    var tempButton = `<button type="button" onclick="deleteDiv('${id}')" class="btn btn-primary">Delete</button>
                      <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>`
    $(this).find(".modal-footer").html(tempButton);
    /*$(this).find(".modal-body").html("<p>id: " + id + "</p>");*/
});

function deleteDiv(id) {
    $.ajax({
        url: `https://localhost:44393/api/Division?id=${id}`,
        type: "DELETE",
        dataType: "json",
        success: function () {            
            alert("Data Has Been Deleted");
        }
    })    
}