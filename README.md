# Stormworks Image To Paintblock Converter
A program to convert an images to both types of stormworks paintblocks.

## v1.5.0 Changes
### New Features/Changes
* The converter now works with transparent images.
* The converter now supports paintable indicators, and allows setting seperate images for the background and glowing parts of the block. The boolean and electrical logic for them is also automatically generated.
* Vehicle generation is run on a seperate thread from the window, so the window will not freeze during generation. The "Generate XML" button also changes to "Generating..." when a vehicle is generating.
* The settings window is also run on a seperate thread.
### Bugfixes
* Clicking the button to save settings crashing the program.
* Window looking messed up when screen scaling is not 125%.
* Generate XML button enabled when no mode is selected.

## Usage/Tutorial
![ImageConverterGUI](https://user-images.githubusercontent.com/99307745/177545181-5d521895-9be5-4cae-bf57-22b3e91f9023.png)
1. Run the program.
2. If you plan to generate paintable indicators, check the ```Glow?``` checkbox.
3. Choose a mode for the paintblock converter with the dropdown in the top left. (The different modes are explained below this tutorial in "Conversion Modes And Image Resizing", but you will most likely be using ```Custom Width```) 
#### If you're not using paintable indicators:
4. Click ```Select File``` and choose the image to import in the file chooser.
5. Set the size of the generated image, in the width/height, what the programs allows to be edited depends on which mode you choose.
6. Check the ```Optimize?``` checkbox if you want the program to optimize the generated image with regular blocks (helps with lag by using regular blocks instead of paintblocks for parts of the image where all the pixels are the same/similar color). If you choose to optimize the image, set the threshold for considering a block of pixels a single color and replacing it with a block. (More information in "How Optimizing Works")
7. Click ```Generate XML```, and the XML file will be generated and placed directly into your stormworks vehicle folder. Generating usually takes less than a second, but for image hundreds of blocks in size it can take a couple seconds.
#### If you're using paintable indicators:
4. Click ```Select Background``` to set the image shown on the background of a paintable indicator, and ```Select Glow``` to set the image shown when the paintable indicator is turned on. Setting the background image is optional.
5. Set the size of the generated image, in the width/height, what the programs allows to be edited depends on which mode you choose.
6. Click ```Generate XML```, and the XML file will be generated and placed directly into your stormworks vehicle folder. Generating usually takes less than a second, but for image hundreds of blocks in size it can take a couple seconds.

## Conversion Modes And Image Resizing
1. ```Custom Dimensions``` - Width and Height can be choosen by user, and the image is stretched to fit the new dimensions.
2. ```Custom Width``` - Custom Width only allows a width to be entered, and calculates a new height to keep the aspect ratio of the image. This mode can leave a black bar at the bottom of the generated paintblocks if the height didn't evenly fit into paintblocks.
3. ```Don't Resize``` - Copies the image pixel for pixel onto paintblocks, and can leave black bars on the bottom and side of the generated paintblocks if it didn't evenly fit.
* If you are generating paintable indicators, there is a chance your background image and glow image will have different resolutions/aspect ratios. If a background image is selected, the glow image is resized and stretched to be the same sized as the background image. If no background image is selected, the glow image will be resized directly instead.

## How Optimizing Works
* Note: This feature is only available for generating regular paintblocks, not paintable indicators.

Vehicles are optimized by replacing paintblocks with regular blocks. It can significantly reduce the lag of a vehicle in the editor and the filesize/spawn time. Optimizing works by checking if the difference between the min/max values of colors in a paintblock are within a threshold, which is set by the user. If they are, a block is generated instead of a paintblock, and its color is set to an average of the pixel colors.

This feature is most useful for importing blueprints, which usually have large blank spaces. I recommend to keep the threshold at 1 for blueprints which have no noise/compression artifacts in the image, and 15-25 for images with noise, an example the ship blueprint below, where I used a threshold of 15.

## Settings File
* The program uses a xml file to store a file path to the stormworks vehicle folder, as well as the name for a generated vehicle, and is automatically generated in the same directory as the program if it is not found. The default file path is ``C:\Users\%username%\AppData\Roaming\Stormworks\data\vehicles\``. (The username is filled in when generated, you don't have to change it yourself.)

## Examples Of Optimization Used
### B-52 Bomber with threshold set to 1, and blocks painted black.
Really good example of the benifits of the optimization setting with blueprints.
![B52 Imported](https://user-images.githubusercontent.com/99307745/177476542-5cd5221a-34c7-4d00-9e14-1254c2156e4f.png)

### Threshold set to 15
![15threshold](https://user-images.githubusercontent.com/99307745/159141304-6ea2b50d-d12c-49a3-91d0-cfcaac7f2195.png)

### Threshold set to 15 (with gridlines turned off)
![15thresholdnogridlines](https://user-images.githubusercontent.com/99307745/159141307-2778b1a4-9222-4f25-a191-81a2eec769f1.png)

### Optimization Not Used
For comparison on the loss of quality with the previous images.
![0threshold](https://user-images.githubusercontent.com/99307745/159141301-bdbf06d6-5dce-4ba9-8caf-7bc47678e8f0.png)
