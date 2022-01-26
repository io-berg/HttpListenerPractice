const canvasElem = document.querySelector("#canvas");
const colorPicker = document.querySelector("#colorInput");
const resetBtn = document.querySelector("#resetBtn");
const rainbowBtn = document.querySelector("#rainbowBtn");
const sizeAdjustBtn = document.querySelector("#sizeAdjustBtn");
const canvasX = document.querySelector("#canvasXSize");
const canvasY = document.querySelector("#canvasYSize");

let colorMode = 1; // 1 for colorpicker, 2 for rainbow mode.
let currentColor = "black" 
rainbowBtn.addEventListener('click', () => colorMode == 1 ? colorMode = 2 : colorMode = 1);
colorPicker.addEventListener("input", changeColor);

let canvasWidth = 16;
let canvasHeight = 16;

createPixels();

sizeAdjustBtn.addEventListener('click', adjustCanvasSize);

function adjustCanvasSize() {
    canvasHeight = canvasY.value;
    canvasWidth = canvasX.value;

    canvasElem.style.gridTemplateColumns = `repeat(${canvasWidth}, auto)`
    canvasElem.textContent = "";

    createPixels();
    let pixels = canvasWidth * canvasHeight;
    document.querySelector("#title").textContent = pixels + "-pixel Paint"
}


function changeColor() {
    if (colorMode == 2)
        colorMode = 1;
    
    currentColor = colorPicker.value;
}

function randomColor() {
    let red = Math.floor(Math.random() * 257);
    let green = Math.floor(Math.random() * 257);
    let blue = Math.floor(Math.random() * 257);

    return rgbToHex(red, green, blue);
}

function rgbToHex(r, g, b) {
  return "#" + ((1 << 24) + (r << 16) + (g << 8) + b).toString(16).slice(1);
}

resetBtn.addEventListener("click", clearCanvas);

function clearCanvas() {
    let pixels = document.querySelectorAll(".canvasPixel");
    pixels.forEach((e) => e.style.backgroundColor = "white");
}

function createPixels() {
    for (let i = 0; i < canvasHeight; i++) {
        for (let j = 0; j < canvasWidth; j++) {
            let newDiv = document.createElement('div');
            newDiv.classList += "canvasPixel";
            newDiv.addEventListener('mouseover', paintPixel);
            canvasElem.appendChild(newDiv);
        }
    }
}

function paintPixel(e) {
    if (colorMode === 2) {
        let color = randomColor();
        colorPicker.value = color;
        currentColor = color;
    }
    e.target.style.backgroundColor = currentColor;
}
