var dataTable;

$(document).ready(function () {
    loadDataTable();
});

function loadDataTable() {
    dataTable = $('#tblData').DataTable({
        "ajax": { url: '/admin/user/getall' },
        "columns": [
            { data: 'name', "width": "20%" },
            { data: 'email', "width": "10%" },
            { data: 'phoneNumber', "width": "15%" },
            { data: 'company.name', "width": "10%" },
            { data: 'role', "width": "10%" },
            {
                data: { id: "id", lockoutEnd: "lockoutEnd" },
                "width":"35%",
                "render": function (data) {
                    var today = new Date().getTime();
                    var lockout = new Date(data.lockoutEnd).getTime();
                    if (lockout > today) {
                        return `
                        <div class="  ">
                            <a  onclick="LockUnlock('${data.id}')" class="mr-10 btn btn-success text-white" style="cursor:pointer; ">
                                <i class="bi bi-unlock-fill"></i>Unlock
                            </a>
                              <a href="/admin/user/RoleManagement?userId=${data.id}" class="btn btn-success text-white" style="cursor:pointer; ">
                                <i class="bi bi-pencil-square"></i>Permission
                            </a>
                        </div>`
                    }
                    else {
                        return `
                        <div class=" ">
                            <a onclick="LockUnlock('${data.id}')" class="btn btn-danger text-white" style="cursor:pointer;">
                                <i class="bi bi-unlock-fill"></i>Lock
                            </a>
                              <a href="/admin/user/RoleManagement?userId=${data.id}" class="btn btn-success text-white" style="cursor:pointer;">
                                <i class="bi bi-pencil-square"></i>Permission
                            </a>
                        </div>`
                    }
              
                },
                "width": "25%"
            }
        ]
    });
}

function LockUnlock(id) {
    $.ajax({
        type:"POST",
        url: '/admin/User/LockUnlock',
        data: JSON.stringify(id),
        contentType: "application/json",
        success: function (data) {
            if (data.success) {
                toastr.success(data.message);
                dataTable.ajax.reload();
            }
        }
    })
}