function addToLocalStorage(key, value) { localStorage[key] = value; }
function readLocalStorage(key) { return localStorage[key]; }

function checkThemeSwitch() {
    
    const checkbox = document.querySelector("input[id=theme-switch]");
    const themeIcon = document.querySelector("span[id=theme-icon]");

    if (checkbox == null) return;

    if (storedTheme === "light-mode") {
        themeIcon.className = "oi oi-sun theme-icon";
    }
    else {
        themeIcon.className = "oi oi-moon theme-icon";
    }

    checkbox.addEventListener('change', function () {

        if (document.body.className == "light-mode") {
            document.body.className = 'dark-mode';
            themeIcon.className = "oi oi-moon theme-icon";
            addToLocalStorage("theme", "dark-mode");
        } else {
            document.body.className = 'light-mode';
            themeIcon.className = "oi oi-sun theme-icon";
            addToLocalStorage("theme", "light-mode");
        }
    });
}

const storedTheme = readLocalStorage("theme");

if (storedTheme == null) {
    document.body.classList.toggle('dark-mode');
}
else {
    document.body.classList.toggle(storedTheme);
};

