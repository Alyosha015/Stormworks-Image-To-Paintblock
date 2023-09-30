# Stormworks Image To Paintblock Converter
A windows program to convert images into both types of stormworks paintblocks.

## Tutorial
![1 8 0 Image Converter GUI](https://github.com/Alyosha015/Stormworks-Image-To-Paintblock/assets/99307745/aaf74b77-36d2-426a-bae3-558d6266add5)
### How To Download
Click on "releases" on the right bar. On the latest release, click on "ImageConverter.zip" to download it, (the first file listed in assets). After downloading, extract the files from the zip folder to where you want to store the program.

### How To Use (Fast version, doesn't explain any of the features):
1. Click ```Select File``` and choose the image to import in the file chooser.

2. Click the ```Select Mode``` dropdown and select a mode for how the image is resized. The modes are:
> * Custom Dimensions - Allows stretching the image with any width and height values.
> * Custom Width - Allows entering a width for the image, the height is calculated automatically to keep the aspect ratio.
> * Custom Height - Allows entering a height for the image, the width is calculated automatically to keep the aspect ratio.
> * Don't Resize - The image isn't resized, and is copied pixel for pixel onto paintblocks.
3. Depending on which mode you chose, enter the width and/or height for the output image in blocks.
4. Click ```Generate XML```, and the vehicle will be generated and saved to the vehicle folder automatically. This is usually almost instant, but for images hundreds of blocks in width and height it can take a couple seconds. When generating the ```Generate XML``` button will switch to ```Generating...``` and be disabled, and switch back to normal when finished.
5. If you scroll down you can see example images of what the other features do, which are explained in the more detailed tutorial.

### How To Use (explains more):
1. If you plan to generate paintable indicators, check the ```Glow?``` checkbox.

2. **(If you're generating paintable signs)** Click ```Select File``` and choose the image to import in the file chooser.
3. **(If you're generating paintable indicators)** Click ```Select Background``` to choose the image shown on the background of a paintable indicator, and ```Select Glow``` to choose the image shown when the paintable indicator is turned on. Setting the background image is optional. Additionally, if you don't want the glow image to be darkened (which helps with how bright they are) uncheck the ```Darken?``` checkbox.
4. Choose a mode for the paintblock converter with the dropdown in the top left. These control how the size in blocks for the generated xml is set. (The names should be mostly self-explanatory, but the different modes are explained below in "Resizing Modes").
5. **(If you're manually setting size)** Set the size of the generated image in the width/height fields, what the programs allows to be edited depends on which mode you choose.
6. **(If you're using Scale By Segment)** The Scale By Segment feature allows setting the size of an image by drawing a line segment over the image and setting the desired size of that segment. To use it, set the resize mode to either ```Custom Width``` or ```Custom Height```, and click on ```Scale By Segment```. Then, draw a segment by clicking and dragging. If you hold the shift key the segment will snap to the cardinal directions. With the segment drawn, enter the desired length for it in the top left field, and click ```Apply```. For the size to be as accurate as possible, if the segment you draw is more horizontal than vertical set the resize mode to custom width, and if it's more vertical set the resize mode to custom height before you do this step. Otherwise, it may be inaccurate by a few pixels.
7. Check the ```Optimize?``` checkbox if you want the program to optimize the generated image with regular blocks (helps with lag by using regular blocks instead of paintblocks for parts of the image where all the pixels are the same/similar color). If you choose to optimize the image, set the threshold for the program to consider a block of pixels a single color and replacing it with a block. (More information in "Paintblock Optimizing").
8. **(If you're generating paintable signs and have optimization turned on)** Check the ```Cutout Background``` checkbox if you want the program to cutout the background of a blueprint image, and only generate blocks for the actual drawings. This only works for blueprints with a pure white background. (An example of this feature used is shown below if in the example pictures).
9. Click ```Generate XML```, and the vehicle will be generated and saved to the vehicle folder automatically. This is usually almost instant, but for images hundreds of blocks in width and height it can take a couple seconds. When generating the ```Generate XML``` button will switch to ```Generating...``` and be disabled, and switch back to normal when finished.
10. If you are generating a blueprint, in many cases the centerline of the vehicle drawn on them isn't aligned with the center of the paintblocks, making building with symmetry difficult. This can be fixed by changing the X and Y inputs under the ```Open Settings``` button, which shift the image on the paintblocks by up to 8 pixels in any direction. A positive X value moves the image to the right, and a negative value to the left. A positive Y value moves the image down, and a negative value up.

## Resizing Modes
1. ```Custom Dimensions``` - Width and Height can be choosen by user, and the image is stretched to fit the new dimensions.
2. ```Custom Width``` - Custom Width only allows a width to be entered, and calculates a new height to keep the aspect ratio of the image. This mode can leave a black bar on the bottom of the generated paintblocks if the height of the image didn't evenly fit.
3. ```Custom Height``` - Custom Height only allows a height to be entered, and calculates a new width to keep the aspect ratio of the image. This mode can leave a black bar on the right side of the generated paintblocks if the width of the image didn't evenly fit.
4. ```Don't Resize``` - Copies the image pixel for pixel onto paintblocks, and can leave black bars on the bottom and right side of the generated paintblocks if it didn't evenly fit.
* If you are generating paintable indicators, there is a chance your background image and glow image will have different resolutions/aspect ratios. If a background image is selected, the glow image is resized and stretched to be the same sized as the background image. If no background image is selected, the glow image will be resized directly instead.

## Paintblock Optimizing
* Note: This feature is only available for generating paintable signs, not paintable indicators.

Vehicles are optimized by replacing paintblocks with regular blocks. It can significantly reduce the lag of a vehicle in the editor and the filesize/spawn time. This feature is most useful for importing blueprints, which usually have large blank spaces.

### Recommendations For Threshold
The threshold can be kept at 0 for blueprints which have no noise/compression artifacts in the image, and around 10-25 for images with noise/artifacts (usually jpgs). For huge blueprints where you don't need fine detail and only the shapes (like ship hulls/superstructures), you can set the threshold to 255 which will convert the entire image to only regular blocks, and have virtually no lag in the editor.

### Some Detail On How Optimizing Works
Whether or not to use a block is determined by checking if the difference between the min/max rgb values of colors in a paintblock are within a threshold, which is set in the program. If they are, a block is generated instead of a paintblock, and its color is set to an average of the pixels colors. For example, if the smallest red value is 14 and the highest is 25, the variation between them 11. If the threshold was set to 11 or greater, and the variation for the green and blue colors was also less than or equal to 11, a block will be used.

## Program Settings
A description of all the buttons/settings in the settings window.
* (The top textbox) - The file path to the stormworks vehicle folder.
* (The small textbox) - Name of the generated xml.
* ```Use Image Filename Instead?``` Checkbox - If checked, the name of the generated vehicle will be the name of the image file used to make it.
* ```Backup Vehicles?``` Checkbox - If checked, any time the program overwrites a vehicle file after generating one a backup of the overwritten file will be stored in the backups folder. (backup0.xml is the newest backup, backup1.xml is the second newest, and so on).
* ```Max Backups``` Number Input - The amount of backups to keep, after that number is reached the oldest backup stored will be deleted when a new one is made.
* ```Open Backups Folder``` Button - Opens the backups folder in file explorer.
* ```Load Window Centered?``` Checkbox - If checked, the program's window will load in the center of the monitor it was last closed on, if it is not checked it will remember where it was on the monitor and load there instead next time it is opened.
* ```Check For Updates``` Button - Checks if there is a new versions of this program released in this github repository and informs the user of it in a popup. The popup also has a button to open the page for the latest release, where the new version can be downloaded to replace the version used (updating has to be done manually by the user).
* ```Close``` Button - Closes the window, doesn't save settings (you could also x out of the window instead).
* ```Save``` Button - Saves the settings.
* ```Reset``` Button - Resets all settings to defaults.

## Examples Of Features Being Used
### B-52 Bomber with background cutout by program.
![b52Cutout](https://user-images.githubusercontent.com/99307745/208194721-e170d6cf-5ea0-4071-9360-a1cc571276ba.png)

### B-52 Bomber with threshold set to 0, and blocks repainted black.
An example of the optimization setting reducing paintblock count.
![B52Optimized](https://user-images.githubusercontent.com/99307745/177476542-5cd5221a-34c7-4d00-9e14-1254c2156e4f.png)


### Screenshot from Scale By Segment window.
Notice the line segment drawing across the length of the bomber. When apply is clicked, the width for the whole image will be set so that the part of the image with the line segment is 194 blocks long.
![ScaleBySegmentWindow](https://github.com/Alyosha015/Stormworks-Image-To-Paintblock/assets/99307745/8e87e5ad-76bf-47d1-9998-67e7a4d1f64a)

### Threshold set to 15
![15threshold](https://user-images.githubusercontent.com/99307745/159141304-6ea2b50d-d12c-49a3-91d0-cfcaac7f2195.png)

### Threshold set to 15 (with gridlines turned off)
![15thresholdnogridlines](https://user-images.githubusercontent.com/99307745/159141307-2778b1a4-9222-4f25-a191-81a2eec769f1.png)

### Optimization Not Used
For comparison on the loss of quality with the previous images.
![0threshold](https://user-images.githubusercontent.com/99307745/159141301-bdbf06d6-5dce-4ba9-8caf-7bc47678e8f0.png)
