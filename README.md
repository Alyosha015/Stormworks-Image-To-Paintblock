# Stormworks Image To Paintblock Converter
A program to convert an image to a stormworks vehicle file of paintblocks, and a feature to replace unnecessary paintblocks with regular blocks.

# Usage
* Run the program and select the image to be converted in the file chooser.
* Enter the width you want the image to be in blocks, the new height is calculated by the program.
* Choose if you want the program to replace unnessary paintblocks with regular blocks (helps with lag by using regular blocks instead of paintblocks for parts of the image where all the pixels are the same/similar color).
* (This is only asked if the previous option is used) Choose the threshold for considering a block of pixels a single color and replacing it with a block. For example, if it's set to 5, the red, green, and blue values can individually vary by up to 5 in the 9x9 block, and still be replaced with a regular block.

# Settings File
* When the program stores the filepath to the stormworks vehicle folder in a text file, which is automatically generated in the same directory as the program if it is not found. The default file path is ``C:\Users\[YOUR_USER_NAME]\AppData\Roaming\Stormworks\data\vehicles``. You dont have to enter the username yourself, it fills it in on generation.

### Example Of Using Threshold Setting
**Threshold set to 0**
![0threshold](https://user-images.githubusercontent.com/99307745/159141301-bdbf06d6-5dce-4ba9-8caf-7bc47678e8f0.png)

**Threashold set to 15**
![15threshold](https://user-images.githubusercontent.com/99307745/159141304-6ea2b50d-d12c-49a3-91d0-cfcaac7f2195.png)

**Threshold set to 15 (with gridlines turned off)**
![15thresholdnogridlines](https://user-images.githubusercontent.com/99307745/159141307-2778b1a4-9222-4f25-a191-81a2eec769f1.png)
