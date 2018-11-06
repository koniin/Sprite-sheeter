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
4. Set default export filetype
    * This can be used to change the type of output you want. Default is my own format I use but PList and simple JSON is also available.
  
There is also the possibility run it with arguments.  
For now it will then run command nr 1 with either a config file as parameter or the folders and optionally the filetype.  

```
SpriteSheetPacker.exe C:\temp\folder_with_sprites C:\temp\output_folder SimpleData
```

```
SpriteSheetPacker.exe C:\temp\some_config_file[extension]
```

Example file:  
```
C:\temp\some_folder_with_sprites\  
C:\temp\folder_to_put_sprite_sheet_and_data_file\  
SimpleData
```

# Available export formats  

*  Json
*  SimpleData
*  Plist

Json example:  
```
{
    "image":"sprites.png", 
    "frameCount":2, 
    "frames": [
        { "name": "bullet_1.png", "id": 0, "x":0, "y":0, "width":16, "height":16 },
        { "name": "bullet_2.png", "id": 1, "x":16, "y":0, "width":16, "height":16 }
    ]
}
```
  
SimpleData example:  
```
sprites.png
2
0 bullet_1.png 0 0 16 16 
1 bullet_2.png 16 0 16 16 

```

Plist example:  
* too long to list but it works with Cocos2d-x

# Extend  

To create new exporters (output type) you need to do these three things:

1. Implement the IMappingFile interface.
2. Add the type to the FileType enum.
3. Add the mapping from filetype to the mapper in ExportFileTypeFactory.

# License  

[MIT](https://github.com/koniin/Sprite-sheeter/blob/master/LICENSE)
