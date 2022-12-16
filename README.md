# Stormworks Image To Paintblock Converter
A windows program to convert images into both types of stormworks paintblocks.

## Tutorial
![v1 7 0ImageConverterGUI](https://user-images.githubusercontent.com/99307745/208195665-bbe24067-1dc4-4f67-9340-a9bd541eb361.png)
1. Extract the files from the zip folder to where you want to store the program, then run the exe.
2. If you plan to generate paintable indicators, check the ```Glow?``` checkbox.
3. **(If you're generating paintable signs)** Click ```Select File``` and choose the image to import in the file chooser.
4. **(If you're generating paintable indicators)** Click ```Select Background``` to set the image shown on the background of a paintable indicator, and ```Select Glow``` to set the image shown when the paintable indicator is turned on. Setting the background image is optional. Additionally, if you don't want the glow image to be darkened (which helps with how bright they are) uncheck the ```Darken?``` checkbox.
5. Choose a mode for the paintblock converter with the dropdown in the top left. (The names should be mostly self-explanatory, but the different modes are explained below in "Conversion Modes And Image Resizing").
6. Set the size of the generated image, in the width/height, what the programs allows to be edited depends on which mode you choose.
7. Check the ```Optimize?``` checkbox if you want the program to optimize the generated image with regular blocks (helps with lag by using regular blocks instead of paintblocks for parts of the image where all the pixels are the same/similar color). If you choose to optimize the image, set the threshold for the program to consider a block of pixels a single color and replacing it with a block. (More information in "How Optimizing Works").
8. Check the ```Cutout Background``` checkbox if you want the program to cutout the background of a blueprint image, and only generate blocks for the actuall drawings. This only works for blueprints with a white background, and I recomend using it with the image optimization.
9. Click ```Generate XML```, and the XML file will be generated and placed directly into your stormworks vehicle folder. Generating usually takes milliseconds, but for image hundreds of blocks in size it can take a couple seconds.

## Conversion Modes And Image Resizing
1. ```Custom Dimensions``` - Width and Height can be choosen by user, and the image is stretched to fit the new dimensions.
2. ```Custom Width``` - Custom Width only allows a width to be entered, and calculates a new height to keep the aspect ratio of the image. This mode can leave a black bar on the bottom of the generated paintblocks if the height of the image didn't evenly fit.
3. ```Custom Height``` - Custom Height only allows a height to be entered, and calculates a new width to keep the aspect ratio of the image. This mode can leave a black bar on the right side of the generated paintblocks if the width of the image didn't evenly fit.
4. ```Don't Resize``` - Copies the image pixel for pixel onto paintblocks, and can leave black bars on the bottom and right side of the generated paintblocks if it didn't evenly fit.
* If you are generating paintable indicators, there is a chance your background image and glow image will have different resolutions/aspect ratios. If a background image is selected, the glow image is resized and stretched to be the same sized as the background image. If no background image is selected, the glow image will be resized directly instead.

## How Optimizing Works
* Note: This feature is only available for generating paintable signs, not paintable indicators.

Vehicles are optimized by replacing paintblocks with regular blocks. It can significantly reduce the lag of a vehicle in the editor and the filesize/spawn time. Whether or not to use a block is determined by checking if the difference between the min/max rgb values of colors in a paintblock are within a threshold, which is set in the program. If they are, a block is generated instead of a paintblock, and its color is set to an average of the pixels colors. For example, if the smallest red value is 14 and the highest is 25, the variation between them 11. If the threshold was set to 11 or greater, and the variation for the green and blue colors was also less than or equal to 11, a block will be used.

This feature is most useful for importing blueprints, which usually have large blank spaces. I recommend to keep the threshold at 0 for blueprints which have no noise/compression artifacts in the image, and around 25 for images with noise, for example the ship blueprint below where I used a threshold of 15.

## Examples Of Optimization And Background Cutout Feature Used
### B-52 Bomber with background cutout by program.
![b52Cutout](https://user-images.githubusercontent.com/99307745/208194721-e170d6cf-5ea0-4071-9360-a1cc571276ba.png)

### B-52 Bomber with threshold set to 0, and blocks repainted black.
Really good example of the benifits of the optimization setting for blueprints.
![B52 Imported](https://user-images.githubusercontent.com/99307745/177476542-5cd5221a-34c7-4d00-9e14-1254c2156e4f.png)

### Threshold set to 15
![15threshold](https://user-images.githubusercontent.com/99307745/159141304-6ea2b50d-d12c-49a3-91d0-cfcaac7f2195.png)

### Threshold set to 15 (with gridlines turned off)
![15thresholdnogridlines](https://user-images.githubusercontent.com/99307745/159141307-2778b1a4-9222-4f25-a191-81a2eec769f1.png)

### Optimization Not Used
For comparison on the loss of quality with the previous images.
![0threshold](https://user-images.githubusercontent.com/99307745/159141301-bdbf06d6-5dce-4ba9-8caf-7bc47678e8f0.png)