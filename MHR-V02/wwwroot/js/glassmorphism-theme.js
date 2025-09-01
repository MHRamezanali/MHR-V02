// تعریف تم‌ها و رنگ‌های مختلف
const themes = {
    default: {
        primary: '#4fa3d1', // آبی ملایم
        secondary: '#8bc34a', // سبز روشن
        background: '#f7f7f7', // خاکی ملایم
        glassBg: 'rgba(255, 255, 255, 0.6)', // شفافیت پس‌زمینه
    },
    dark: {
        primary: '#333333', // خاکی تیره
        secondary: '#ff6f61', // نارنجی ملایم
        background: '#2e2e2e', // تیره
        glassBg: 'rgba(255, 255, 255, 0.15)', // شفافیت کم
    },
    light: {
        primary: '#f39c12', // زرد روشن
        secondary: '#e74c3c', // قرمز ملایم
        background: '#ecf0f1', // سفید مایل به خاکی
        glassBg: 'rgba(255, 255, 255, 0.8)', // شفافیت زیاد
    }
};

// تابعی برای اعمال تم
function applyTheme(theme) {
    document.documentElement.style.setProperty('--primary-color', theme.primary);
    document.documentElement.style.setProperty('--secondary-color', theme.secondary);
    document.documentElement.style.setProperty('--background-color', theme.background);
    document.documentElement.style.setProperty('--glass-bg', theme.glassBg);
}



// تابع برای ذخیره تم انتخابی در Local Storage برای یادآوری در بارگذاری‌های بعدی
function saveSelectedTheme(themeName) {
    localStorage.setItem('selected-theme', themeName);
}

// تابع برای بارگذاری تم ذخیره شده
function loadSavedTheme() {
    const savedTheme = localStorage.getItem('selected-theme');
    if (savedTheme && themes[savedTheme]) {
        applyTheme(themes[savedTheme]);
        themeSelector.value = savedTheme; // انتخاب مجدد تم از Dropdown
    }
}

// بارگذاری تم ذخیره شده در هنگام بارگذاری صفحه
loadSavedTheme();
