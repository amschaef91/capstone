const toggleThemeBtn = document.querySelector('#toggle-theme-btn');
const html = document.querySelector('html');

toggleThemeBtn.addEventListener('click', () => {
    if (html.getAttribute('data-bs-theme') === 'light') {
        html.setAttribute('data-bs-theme', 'dark');
        localStorage.setItem('theme', 'dark');
    } else {
        html.setAttribute('data-bs-theme', 'light');
        localStorage.setItem('theme', 'light');
    }
});

document.addEventListener('DOMContentLoaded', () => {
    const theme = localStorage.getItem('theme');
    if (theme) {
        html.setAttribute('data-bs-theme', theme);
    }
});


$(document).ready(function () {
    var cleave = new Cleave('#phone', {
        delimiters: ["(", ") ", "-"],
        blocks: [0, 3, 3, 4],
        numericOnly: true
    });
})