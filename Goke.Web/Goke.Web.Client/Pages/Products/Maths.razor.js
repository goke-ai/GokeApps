export function plotQuadratic(element, data) {
    const plot = Plot.plot({
        grid: true,
        marks: [
            Plot.lineY(data, { x: "x", y: "y" })
        ]
    });
    const div = document.querySelector(element);
    //div.append(plot);
    div.replaceChildren(plot);
}