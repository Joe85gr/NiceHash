function addToLocalStorage(key, value) { localStorage[key] = value; }
function readLocalStorage(key) { return localStorage[key]; }

function checkThemeSwitch() {
    
    const checkbox = document.querySelector("input[id=theme-switch]");

    if (checkbox == null) return;

    checkbox.addEventListener('change', function () {

        if (document.body.className == "light-mode") {
            document.body.className ='dark-mode';
            addToLocalStorage("theme", true);
        } else {
            document.body.className = 'light-mode';
            addToLocalStorage("theme", false);
        }
    });
}

var isDarkModeOn = readLocalStorage("theme");

if (isDarkModeOn == null) {
    document.body.classList.toggle('dark-mode');
}
else {
    if (isDarkModeOn === "true") {
        document.body.classList.toggle('dark-mode');
    }
    else {
        document.body.classList.toggle('light-mode');
    }
}

