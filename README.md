# Stormworks Image To Paintblock Converter
A program to convert an image to a stormworks vehicle file of paintblocks, and a feature to replace unnecessary paintblocks with regular blocks.

## Usage
* Run the program.
* Click ```Select File```, which will open a file chooser. Choose the file you want to convert to paintblocks.
* Choose a mode for the paintblock converter. They are explained here:

**Conversion Modes**
1. ```Custom Dimensions``` - Width and Height can be choosen by user, and the image is stretched to fit the new dimensions.
2. ```Custom Width``` - The only option of previous versions, Custom Width only allows a width to be entered, and calculates a new height to keep the aspect ratio of the image. This mode can leave a black bar at the bottom of the generated paintblocks if the height didn't evenly fit into paintblocks.
3. ```Don't Resize``` - Copies the image pixel for pixel onto paintblocks, and can leave black bars on the bottom and side of the generated paintblocks if it didn't evenly fit.

* Click the checkbox ```Optimize Paintblocks``` if you want the program to optimize the generated image with regular blocks (helps with lag by using regular blocks instead of paintblocks for parts of the image where all the pixels are the same/similar color), see images below for examples. This feature is most useful for generating blueprints, which usually have large blank spaces and can help significantly decrease lag and vehicle file size.
* If you choose to optimize the image, choose the threshold for considering a block of pixels a single color and replacing it with a block. For example, if it's set to 5, the red, green, and blue rgb values can individually vary by up to 5 in the 9x9 block of pixels, and still be replaced with a regular block with an average of the colors. (I recommend 15-30 for blueprints)
* Click ```Generate XML```, and the XML file will be generated and placed directly into your stormworks vehicle folder. (If you are generating extremely large images hundreds of blocks in width and height the program will freeze for 10-20 seconds while it generates, but it should be almost instant for anything else)

## Settings File
* The program stores the filepath to the stormworks vehicle folder in a xml file, which is automatically generated in the same directory as the program if it is not found. The default file path is ``C:\Users\[YOUR_USER_NAME]\AppData\Roaming\Stormworks\data\vehicles\``. (The username is filled in when generated, you don't have to change it yourself.)

## Example Of Using Threshold Setting
**Threshold set to 0**
![0threshold](https://user-images.githubusercontent.com/99307745/159141301-bdbf06d6-5dce-4ba9-8caf-7bc47678e8f0.png)

**Threshold set to 15**
![15threshold](https://user-images.githubusercontent.com/99307745/159141304-6ea2b50d-d12c-49a3-91d0-cfcaac7f2195.png)

**Threshold set to 15 (with gridlines turned off)**
![15thresholdnogridlines](https://user-images.githubusercontent.com/99307745/159141307-2778b1a4-9222-4f25-a191-81a2eec769f1.png)
