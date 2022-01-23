import '../lib/babylonjs/babylon.js'
/**
 * Initialises and renders scene in main thread.
 * @param {string} canvasElementName
 * @param {string} assetName
 */
export function initialiseScene(canvasElementName, assetName) {
    const canvas = document.getElementById(canvasElementName);
    const engine = new BABYLON.Engine(canvas, true);
    const createScene = function () {
        const scene = new BABYLON.Scene(engine);
        scene.clearColor = new BABYLON.Color3.FromHexString('#333333');
        BABYLON.SceneLoader.ImportMeshAsync("", "../assets/", assetName);
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

    window.addEventListener("resize", function () {
        engine.resize();
    });
}

/**
 * Initialises and renders scene off screen (not in main thread)
 * @param {string} canvasElementName
 * @param {string} assetName
 */
export function initialiseSceneOffScreen(canvasElementName, assetName) {
    var canvas = document.getElementById(canvasElementName);
    canvas.width = canvas.clientWidth;
    canvas.height = canvas.clientHeight;

    var offscreen = canvas.transferControlToOffscreen();
    var worker = new Worker("./js/worker.js");
    worker.postMessage({ canvas: offscreen, assetName }, [offscreen]);
}
