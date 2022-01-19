import '../lib/babylonjs/babylon.js'

export function initialiseScene(canvasElementName, assetName) {
    const canvas = document.getElementById(canvasElementName);
    const engine = new BABYLON.Engine(canvas, true);
    const createScene = function () {
        const scene = new BABYLON.Scene(engine);
        scene.clearColor = new BABYLON.Color3.FromHexString('#0082e6');
        BABYLON.SceneLoader.ImportMeshAsync("", "../assets/", assetName + '.babylon');

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
