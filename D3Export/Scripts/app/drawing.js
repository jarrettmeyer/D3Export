(function() {

    // Define the plot canvas.
    var canvasSelector = "#scatterplot-canvas";

    // Define the plot area.
    var canvasBackgroundColor = "#c0c0c0",
        canvasWidth = 800,
        canvasHeight = 600;

    // Define the point radius.
    var pointRadius = 3;

    // Define some mock data.
    var data = [],
        dataCount = parseInt(20 + Math.random() * 80);
    for (var i = 0; i < dataCount; i += 1) {
        var x = 20 + Math.random() * 20,
            y = 40 + Math.random() * 25;
        var datum = { i: i + 1, x: x, y: y };
        data.push(datum);
    }

    // Create the SVG canvas.
    var svg = d3.select(canvasSelector)
        .append("svg")
        .attr("width", canvasWidth)
        .attr("height", canvasHeight);

    // Draw a boundary rectangle
    svg.append("rect")
        .attr("stroke", "none")
        .attr("fill", canvasBackgroundColor)
        .attr("x", 0)
        .attr("y", 0)
        .attr("height", canvasHeight)
        .attr("width", canvasWidth);

    // Define the X & Y domains.
    var xDomain = d3.extent(data, function (d) { return d.x; }),
        yDomain = d3.extent(data, function (d) { return d.y; });

    // Define the X & Y scales.
    var xScale = d3.scale.linear()
        .domain(xDomain)
        .range([0, canvasWidth])
        .nice();
    var yScale = d3.scale.linear()
        .domain(yDomain)
        .range([canvasHeight, 0])
        .nice();

    // Create the points.
    svg.selectAll("circle")
        .data(data)
        .enter()
        .append("circle")
        .attr("class", "data")
        .attr("cx", function(d) { return xScale(d.x); })
        .attr("cy", function(d) { return yScale(d.y); })
        .attr("r", pointRadius);

})();