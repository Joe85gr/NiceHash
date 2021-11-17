function addToLocalStorage(key, value) { localStorage[key] = value; }
function readLocalStorage(key) { return localStorage[key]; }

function checkThemeSwitch() {
    
    const checkbox = document.querySelector("input[id=dark-mode-switch]");
    const themeIcon = document.querySelector("span[id=theme-icon]");

    if (checkbox == null) return;

    if (document.body.className == "light-mode") {
        themeIcon.className = "oi oi-moon text-info";
    } else {
        themeIcon.className = "oi oi-sun text-title";
    }

    checkbox.addEventListener('change', function () {

        if (document.body.className == "light-mode") {
            document.body.className ='dark-mode';
            themeIcon.className = "oi oi-sun text-title";
            addToLocalStorage("theme", true);
        } else {
            document.body.className = 'light-mode';
            themeIcon.className = "oi oi-moon text-info";
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

