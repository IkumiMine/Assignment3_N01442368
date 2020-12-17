//AJAX for Add Teacher function
//This file is connected to the project via Shared/_Layout.cshtml

function AddTeacher() {

    //goal: send a request which like this:
    //POST: http://localhost:61589/api/TeacherData/AddTeacher
    //with POST data of teacher first and last name

    var URL = "http://localhost:61589/api/TeacherData/AddTeacher/";

    var rq = new XMLHttpRequest();

    var TeacherFname = document.getElementById("TeacherFname").value;
    var TeacherLname = document.getElementById("TeacherLname").value;
    var TeacherNumber = document.getElementById("TeacherNumber").value;
    var TeacherSalary = document.getElementById('TeacherSalary').value;
    var HireDate = document.getElementById('HireDate').value;

    var TeacherData = {
        "TeacherFname": TeacherFname,
        "TeacherLname": TeacherLname,
        "TeacherNumber": TeacherNumber,
        "TeacherSalary": TeacherSalary,
        "HireDate" : HireDate
    };

    rq.open("POST", URL, true);
    rq.setRequestHeader("Content-Type", "application/json");
    rq.onreadystatechange = function () {
        if (rq.readyState == 4 && rq.status == 200) {
            //request is successful and the request is finished
            //nothing to render, the method returns nothing.
        }
    }
    rq.send(JSON.stringify(TeacherData));
}

function UpdateTeacher(TeacherId) {

    console.log("hello");

    //goal: send a request which looks like this:
    //POST: http://localhost:61589/api/TeacherData/UpdateTeacher/{id}
    //with POST data of teacher first and last name, employee number.

    var URL = "http://localhost:61589/api/TeacherData/UpdateTeacher/" + TeacherId;

    var rq = new XMLHttpRequest();

    var TeacherFname = document.getElementById('TeacherFname').value;
    var TeacherLname = document.getElementById('TeacherLname').value;
    var TeacherNumber = document.getElementById('TeacherNumber').value;
    var TeacherSalary = document.getElementById('TeacherSalary').value;
    var HireDate = document.getElementById('HireDate').value;

    var TeacherData = {
        "TeacherFname": TeacherFname,
        "TeacherLname": TeacherLname,
        "TeacherNumber": TeacherNumber,
        "TeacherSalary": TeacherSalary,
        "HireDate": HireDate
    };

    rq.open("POST", URL, true);
    rq.setRequestHeader("Content-Type", "application/json");
    rq.onreadystatechange = function () {
        if (rq.readyState == 4 && rq.status == 200) {
            //request is successful and the request is finished
            //nothing to render, the method returns nothing.
        }
    }
    rq.send(JSON.stringify(TeacherData));

}
//NOT WORKING
function validationName() {
    alert("hello");
 /*   var TeacherFname = document.getElementById("TeacherFname");
    var TeacherLname = document.getElementById("TeacherLname");
    var cautionFirst = document.getElementById("cautionFirst");
    var cautionLast = document.getElementById("cautionLast");

    //valifation for first name
    if (TeacherFname.value === "" || TeacherFname.value === null) {
        TeacherFname.focus();
        cautionFirst.style.display = "block";
        return false;
    }

    //valifation for last name
    if (TeacherLname.value === "" || TeacherLname.value === null) {
        TeacherLname.focus();
        cautionLast.style.display = "block";
        return false;
    }*/
}