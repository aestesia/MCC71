$(document).ready(function () {
    $('#tableDepartment').DataTable({
        ajax: {
            url: 'https://localhost:44393/api/Department',
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
                data: "divisionId"
            },
            {
                data: null,
                render: function (data, type, row, meta) {
                    return `
                        <button type="button" id="detailButton" class="btn btn-primary" onclick="detailDept('${data.id}')" data-toggle="modal" data-target="#detailModal">Edit</button> |
                        <button type="button" id="deleteButton" class="btn btn btn-danger" data-toggle="modal" data-id="${data.id}" data-target="#deleteDeptModal">Delete</button>`
                }
            }
        ],
        dom: 'Bfrtip',
        buttons: [
            {
                text: 'Create New',
                action: function (e, node, config) {
                    $('#createDeptModal').modal('show')
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
function createDept() {
    if ($("#createDeptForm").valid()) {

        const departmentName = $("#departmentNameCreate").val();
        const divisionId = $("#divisionIdCreate").val();

        const data = {
            name: departmentName,
            divisionId: divisionId
        }

        $.ajax({
            url: 'https://localhost:44393/api/Department',
            type: 'POST',
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(data),
            success: function () {
                //alert("Add Data Successfull");
                //location.reload();
                Swal.fire({
                    title: 'Success!',
                    text: 'Data has been added',
                    type: 'success'
                }
                ).then(function () {
                    location.reload();
                });
            }
        })
    }
}


//DETAIL & EDIT
function detailDept(id) {
    $.ajax({
        url: `https://localhost:44393/api/Department/${id}`,
        type: "GET",
        dataType: 'json'
    }).done((res) => {
        let tempDetail =
            `<form id="detailDeptForm">
                <div>
                    <label for="id">ID:</label><br>
                    <input type="text" class="form-control" id="deptId" value="${res.data.id}" readonly></input><br>
                </div>
                <div>
                    <label for="name">Department Name:</label><br>
                    <input type="text" class="form-control" id="deptName" value="${res.data.name}" placeholder="Enter Department Name" required></input><br>
                </div>
                <div>
                    <label for="name">Division ID:</label><br>
                    <input type="text" class="form-control" id="divId" value="${res.data.divisionId}" placeholder="Enter Division ID" required></input><br><br>
                </div>
                <button type="button" class="btn btn-primary" onclick="editDept()">Edit</button>
            </form>`;
        $("#detailBody").html(tempDetail);
    });
}

function editDept() {
    if ($("#detailDivForm").valid()) {

        const deptId = $("#deptId").val();
        const deptName = $("#deptName").val();
        const divId = $("#divId").val();

        const data = {
            id: deptId,
            name: deptName,
            divisionId: divId
        }

        //console.log(data);
        $.ajax({
            url: `https://localhost:44393/api/Department`,
            type: "PUT",
            contentType: 'application/json',
            dataType: 'json',
            data: JSON.stringify(data),
            success: function () {
                //alert("Edit Data Successfull");
                //location.reload();
                Swal.fire({
                    title: 'Success!',
                    text: 'Data has been edited',
                    type: 'success'
                }
                ).then(function () {
                    location.reload();
                });
            }
        })
    }
}

//DELETE 
$("#deleteDeptModal").on('show.bs.modal', function (e) {
    var triggerLink = $(e.relatedTarget);
    var id = triggerLink.data("id");
    var tempButtons = `<button type="button" onclick="deleteDept('${id}')" class="btn btn-primary">Delete</button>
                      <button type="button" class="btn btn-secondary" data-dismiss="modal">Cancel</button>`
    $(this).find(".modal-footer").html(tempButtons);   
});

function deleteDept(id) {
    $.ajax({
        url: `https://localhost:44393/api/Department?id=${id}`,
        type: "DELETE",
        dataType: "json",
        success: function () {            
            //alert("Data Has Been Deleted");
            //location.reload();
            Swal.fire({
                title: 'Success!',
                text: 'Data has been deleted',
                type: 'success'
            }
            ).then(function () {
                location.reload();
            });
        }
    })    
}