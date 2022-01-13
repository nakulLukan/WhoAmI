import '../modules/babylonjs/babylon.js'

export function initialiseScene() {
    const canvas = document.getElementById("footer-canvas");
    const engine = new BABYLON.Engine(canvas, true);
    const createScene = function () {

        const scene = new BABYLON.Scene(engine);

        BABYLON.SceneLoader.ImportMeshAsync("", "../assets/", "cude.babylon");

        const camera = new BABYLON.ArcRotateCamera("camera", -Math.PI / 2, Math.PI / 2.5, 15, new BABYLON.Vector3(0, 0, 0));
        camera.attachControl(canvas, true);
        const light = new BABYLON.HemisphericLight("light", new BABYLON.Vector3(1, 1, 0));

        console.log(scene.meshes);

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
