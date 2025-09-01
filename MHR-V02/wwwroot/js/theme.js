const themes = {
    pastelGlass: {
        primary: '#f5a3c7',
        accent: '#97d7d3',
        background: 'linear-gradient(135deg, #f5a3c7, #97d7d3)',
        glassBg: 'rgba(255, 255, 255, 0.4)',
        glassBorder: 'rgba(255, 255, 255, 0.2)',
        glassShadow: 'rgba(0, 0, 0, 0.15)',
    },
    summerGlass: {
        primary: '#ffd791',
        accent: '#ff6f61',
        background: 'linear-gradient(135deg, #ffd791, #ff6f61)',
        glassBg: 'rgba(255, 255, 255, 0.5)',
        glassBorder: 'rgba(255, 255, 255, 0.2)',
        glassShadow: 'rgba(0, 0, 0, 0.2)',
    },
};

const applyTheme = (theme) => {
    document.documentElement.style.setProperty('--primary-color', theme.primary);
    document.documentElement.style.setProperty('--accent-color', theme.accent);
    document.documentElement.style.setProperty('--background-color', theme.background);
    document.documentElement.style.setProperty('--glass-bg', theme.glassBg);
    document.documentElement.style.setProperty('--glass-border', theme.glassBorder);
    document.documentElement.style.setProperty('--glass-shadow', theme.glassShadow);
};

document.getElementById('theme-selector').addEventListener('change', (e) => {
    const selectedTheme = themes[e.target.value];
    if (selectedTheme) applyTheme(selectedTheme);
});
