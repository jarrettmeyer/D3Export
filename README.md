# D3 Export

This application provides an example of how to export a client-generated SVG object to disk on the server.

## Tooling

- Visual Studio 2013
- [Inkscape](http://www.inkscape.org/) for Windows

Inkscape, via its command line interface, is used to perform the export to PDF functionality. This relies on an application variable called `PathToInkscapeExecutable` in `web.config`.


## Process Flow

1. Client requests home page. A SVG scatterplot is randomly generated on the client.
2. Click the save button. The content of the SVG data is sent back to the server.
3. Request is received to `POST /home/save`.
4. SVG is saved to disk. Inkscape process is started to export the SVG to PDF.
5. A successful result is returned to the client.