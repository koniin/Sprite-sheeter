# Sprite-sheeter features  

* cfg            Execute commands from a config file [filepath]
* combinefolder  Combines all sprites from input folder into a spritesheet in output folder [inputpath, outputpath]
* combinesub     Combines all sprites from subfolders in folder into a spritesheet in folder [inputpath]
* split          Split a sheet into frames of size x size [size, inputpath]
* bw             Creates black and white copes of all images in inputpath. [inputpath]
* scale          Resizes images to the size in the inputpath. [size, inputpath]
* filetype       Set the default export type (sets in environment variable). [filetype]

Made for easy sprite sheet making in gamedev pipeline.  
Created by Henrik Länsman.  

# Usage  

To run interactive mode:

```
dotnet build
dotnet run 
```

Or as single command with:

```
dotnet run --project .\SpriteSheeter.Cli\SpriteSheeter.Cli.csproj -- combinefolder c:\folderwithimages c:\folderwhereiwantmysheet
```

```
dotnet run --project .\SpriteSheeter.Cli\SpriteSheeter.Cli.csproj -- cfg c:\code\sprite-sheeter\examples\sample_config.cfg
```

Example file:  
```
combinefolder
C:\temp\sprites\player
C:\temp\sprites
```

# Available export formats  

*  Json
*  SimpleData
*  Plist

Set type using Environment variable (default is SimpleData):    
$env:SPRITESHEETER_FILETYPE = 'Json'    

You can also set it interactive by starting the application with no arguments.

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
