(function() {

    // Define the plot canvas.
    var canvasSelector = "#scatterplot-canvas",
        $scatterplotCanvas = $(canvasSelector);

    // Define the plot area.
    var padding = { top: 20, right: 20, bottom: 30, left: 40 },
        canvasWidth = $scatterplotCanvas.width(),
        canvasHeight = $scatterplotCanvas.height(),
        plotAreaWidth = canvasWidth - padding.left - padding.right,
        plotAreaHeight = canvasHeight - padding.top - padding.bottom;

    // Define the point radius.
    var pointRadius = 3;

    // Define some mock data.
    var data = [];
    for (var i = 0; i < 100; i += 1) {
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

    // Define the brush. This will capture mouse events.
    var brush = d3.svg.brush()
        .on("brushstart", onBrushStart)
        .on("brushend", onBrushEnd)
        .on("brush", onBrush);

    // Define the tooltip
    var tooltip = d3.select("body")
        .append("div")
        .attr("class", "tooltip")
        .text("");

    // Define the X & Y domains.
    var xDomain = d3.extent(data, function (d) { return d.x; }),
        yDomain = d3.extent(data, function (d) { return d.y; });

    // Define the X & Y scales.
    var xScale = d3.scale.linear()
        .domain(xDomain)
        .range([0, plotAreaWidth])
        .nice();
    var yScale = d3.scale.linear()
        .domain(yDomain)
        .range([plotAreaHeight, 0])
        .nice();

    // Apply the scales to the brush.
    brush.x(xScale).y(yScale);

    // Draw the brush.
    svg.append("g")
        .call(brush)
        .attr("class", "brush")
        .attr("transform", "translate(" + padding.left + "," + padding.top + ")");

    // Define the X & Y axes.
    var xAxis = d3.svg.axis()
        .scale(xScale)
        .orient("bottom");
    var yAxis = d3.svg.axis()
        .scale(yScale)
        .orient("left");

    // Draw the X axis.
    svg.append("g")
        .attr("class", "axis x-axis")
        .call(xAxis)
        .attr("transform", "translate(" + padding.left + "," + (canvasHeight - padding.bottom) + ")");

    // Draw the Y axis.
    svg.append("g")
        .attr("class", "axis y-axis")
        .call(yAxis)
        .attr("transform", "translate(" + padding.left + "," + padding.bottom + ")");

    // Create the points.
    svg.selectAll("circle")
        .data(data)
        .enter()
        .append("circle")
        .attr("class", "data")
        .attr("cx", function (d) { return xScale(d.x) + padding.left; })
        .attr("cy", function (d) { return yScale(d.y) + padding.top; })
        .attr("r", pointRadius)
        .attr("x-value", function (d) { return d.x; })
        .attr("y-value", function (d) { return d.y; })
        .on("mouseenter", onPointMouseenter)
        .on("mousemove", onPointMousemove)
        .on("mouseleave", onPointMouseleave);

    function onBrush() {
        var e = brush.extent();
        //console.log("brush extents: {" + e[0][0] + "," + e[0][1] + "}, {" + e[1][0] + "," + e[1][1] + "}");
        svg.selectAll("circle")
            .classed("active", function (d) {
                return e[0][0] <= d.x && d.x <= e[1][0] &&
                       e[0][1] <= d.y && d.y <= e[1][1];
            });

    }

    function onBrushEnd() {
        if (brush.empty()) {
            brush.clear();
            svg.selectAll("circle").classed("active", false);
        } else {
            var activeCircles = svg.selectAll("circle.active").data();
            //console.log("# of active circles: " + activeCircles.length);
            $(window).trigger("scatterplot-demo:brushend", { selection: activeCircles });
        }
    }

    function onBrushStart() {
    }

    function onPointMouseenter() {
        return tooltip.style("visibility", "visible");
    }

    function onPointMousemove() {
        var tooltipText = "X: " + parseFloat(d3.select(this).attr("x-value")).toFixed(5) + ", Y: " + parseFloat(d3.select(this).attr("y-value")).toFixed(5);
        return tooltip
            .text(tooltipText)
            .style("top", (event.pageY - 10) + "px")
            .style("left", (event.pageX + 10) + "px");
    }

    function onPointMouseleave() {
        return tooltip.style("visibility", "hidden");
    }

})();