// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

var btnChecks = document.querySelectorAll(".cc");
btnChecks.forEach(function (checkbox) {
    checkbox.addEventListener("change", function () {
        var aChecked = checkbox.nextElementSibling;
        aChecked.click();
    })
})