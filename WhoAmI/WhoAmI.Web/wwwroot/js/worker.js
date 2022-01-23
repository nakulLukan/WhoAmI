importScripts('../lib/babylonjs/babylon.js');
var engine;
var canvas;

onmessage = function (evt) {
    if (evt.data.canvas) {
        this.postMessage("initiating");
        initiateEngine(evt.data);
    }
    else if (evt.data.width) {
        this.postMessage("resizing");
        resizeCanvas(evt.data.width, evt.data.height);
    }
}

function initiateEngine(data) {
    canvas = data.canvas;
    engine = new BABYLON.Engine(canvas, true);
    
    const createScene = function () {
        const scene = new BABYLON.Scene(engine);

        scene.clearColor = new BABYLON.Color3.FromHexString('#333333');
        BABYLON.SceneLoader.ImportMeshAsync("", "../assets/", data.assetName);

        const camera = new BABYLON.ArcRotateCamera("camera", -Math.PI / 2, Math.PI / 2.5, 4, new BABYLON.Vector3(0, 0, 0));
        camera.attachControl(canvas, true);
        const light = new BABYLON.HemisphericLight("light", new BABYLON.Vector3(1, 1, 0));

        return scene;
    };
    const scene = createScene(); //Call the createScene function


    // Register a render loop to repeatedly render the scene
    engine.runRenderLoop(function () {
        scene.render();
    });
}

/**
 * Resize canvas
 * @param {number} width
 * @param {number} height
 */
function resizeCanvas(width, height) {
    canvas.width = width;
    canvas.height = height;
}
