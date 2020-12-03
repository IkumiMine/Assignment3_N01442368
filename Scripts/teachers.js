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

    var TeacherData = {
        "TeacherFname": TeacherFname,
        "TeacherLname": TeacherLname
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