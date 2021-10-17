# Sprite-sheeter features  

* Combine multiple sprites into a sprite sheet (and description file)  
* Combine multiple folders into one sprite sheet (and description file)    
* Split an existing sheet where all sprites are the same size into multiple sprites  

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

```
SpriteSheetPacker.exe C:\temp\some_config_file[extension]
```

Example file:  
```
combinefolder
C:\temp\sprites\player
C:\temp\sprites
SimpleData
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
