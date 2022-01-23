function setOnScroll(dotnet, methodName) {
    window.onscroll = function () {
        dotnet.invokeMethodAsync(methodName);
    };
}

function getDocumentElementScrollTop() {
    return document.documentElement.scrollTop;
}

function getDocumentBodyScrollTop() {
    return document.body.scrollTop;
}