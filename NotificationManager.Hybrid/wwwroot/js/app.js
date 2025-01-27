function toggleBackgroundColor(cssClass, clientX, clientY) {
    var element = document.elementFromPoint(clientX, clientY);
    if (element) {
        element.classList.toggle(cssClass);
    }
}