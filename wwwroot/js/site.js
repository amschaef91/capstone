// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.
// Get the button element
const toggleThemeBtn = document.querySelector('#toggle-theme-btn');

// Get the <html> element
const html = document.querySelector('html');

// Listen for a click event on the button
toggleThemeBtn.addEventListener('click', () => {
    // Toggle the data-bs-theme attribute on the <html> element
    if (html.getAttribute('data-bs-theme') === 'light') {
        html.setAttribute('data-bs-theme', 'dark');
    } else {
        html.setAttribute('data-bs-theme', 'light');
    }
});

$(document).ready(function () {
    var cleave = new Cleave('#phone', {
        delimiters: ["(", ") ", "-"],
        blocks: [0, 3, 3, 4],
        numericOnly: true
    });
})