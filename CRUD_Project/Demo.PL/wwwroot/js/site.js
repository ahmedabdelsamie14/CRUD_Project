// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var SearchInp = document.getElementById("SearchInp");
SearchInp.addEventListener("keyup", function () {
    var xttp = new XMLHttpRequest();
    let url = `https://localhost:44340/Employee/Index?SearchInp=${SearchInp.value}`;
    xttp.open("POST", url, true);
    xttp.onreadystatechange = function () {
        if (this.readyState == 4 && this.status == 200) {
            console.log(this.response);

        }
    }
        xttp.send();

}

)

