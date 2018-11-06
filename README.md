# Sprite-sheeter features  

* Combine multiple sprites into a sprite sheet (and description file)  
* Combine multiple folders into one sprite sheet (and description file)    
* Split an existing sheet where all sprites are the same size into multiple sprites  

Made for easy sprite sheet making in gamedev pipeline.  
Created by Henrik Länsman.  

# Usage  

Build the project and run the exe file.  
There are four different commands in this mode:  
1. Combine all images in ONE folder to spritesheet
    * Prompts for input directory where your sprites are and then a directory to write output to.
2. Combine all images in all subfolders of entered path
    * Prompts for a folder and reads all sub directories of that folder and combines everything from those sub folders and writes output to the directory specified.
3. Split an image into all its [X by Y] components (e.g. 32x32)
    * Splits an image in even squares. Prompts for a size (once, e.g. 32) and then for an input file. It will write where the output folder is located.
4. Set default export filetype (e.g. json, plist)
    * This can be used to change the type of output you want. Default is my own format I use but PList and simple JSON is also available.
  
There is also the possibility run it with arguments.  
For now it will then run command nr 1 with either a config file as parameter or the folders and optionally the filetype.  

```
SpriteSheetPacker.exe C:\temp\folder_with_sprites C:\temp\output_folder EngineFormat
```

```
SpriteSheetPacker.exe C:\temp\some_config_file.[something]
```

Example file:  
```
C:\temp\some_folder_with_sprites\  
C:\temp\folder_to_put_sprite_sheet_and_data_file\  
EngineFormat
```

# Extend  

To create new exporters (output type) you need to do these three things:

1. Implement the IMappingFile interface.
2. Add the type to the FileType enum.
3. Add the mapping from filetype to the mapper in ExportFileTypeFactory.

# License  

[MIT](https://github.com/koniin/Sprite-sheeter/blob/master/LICENSE)
