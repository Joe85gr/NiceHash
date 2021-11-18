function addToLocalStorage(key, value) { localStorage[key] = value; }
function readLocalStorage(key) { return localStorage[key]; }

function checkThemeSwitch() {
    
    const checkbox = document.querySelector("input[id=theme-switch]");
    const themeIcon = document.querySelector("span[id=theme-icon]");

    if (checkbox == null) return;

    console.log("isDarkModeOn: ", isDarkModeOn);

    if (isDarkModeOn === false) {
        themeIcon.className = "oi oi-moon theme-icon";
    }
    else {
        themeIcon.className = "oi oi-sun theme-icon";
    }

    checkbox.addEventListener('change', function () {

        if (document.body.className == "light-mode") {
            document.body.className = 'dark-mode';
            themeIcon.className = "oi oi-moon theme-icon";
            addToLocalStorage("theme", true);
        } else {
            document.body.className = 'light-mode';
            themeIcon.className = "oi oi-sun theme-icon";
            addToLocalStorage("theme", false);
        }
    });

    console.log(themeIcon);
}

const isDarkModeOn = readLocalStorage("theme");

if (isDarkModeOn === "true") {
    document.body.classList.toggle('dark-mode');
}
else {
    document.body.classList.toggle('light-mode');
};

