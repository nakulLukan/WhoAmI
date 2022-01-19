function setOnScroll(dotnet, methodName) {
    window.onscroll = async function () {
        await dotnet.invokeMethodAsync(methodName);
    };
}

function getDocumentElementScrollTop() {
    return document.documentElement.scrollTop;
}

function getDocumentBodyScrollTop() {
    return document.body.scrollTop;
}