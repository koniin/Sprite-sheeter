using SpriteSheeter.Lib.ImageManipulation;
using SpriteSheeter.Lib.MappingFileFormats;
using SpriteSheeter.Lib.SpriteSheetPack;
using System;

namespace SpriteSheeter.Lib
{
    public class SpriteSheetMaker
    {
        private SpriteSheetPacker _spriteSheetPacker;
        private UserSettings _userSettings;
        private ExportFileTypeFactory _exportFileFactory;

        public FileType ExportFiletype => _userSettings.ExportFileType;

        public SpriteSheetMaker(UserSettings userSettings)
        {
            _spriteSheetPacker = new SpriteSheetPacker(new SquareFrameListCombiner());
            _userSettings = userSettings;
            _exportFileFactory = new ExportFileTypeFactory();
        }

        /// <summary>
        /// Returns status and the parsed commands 
        /// </summary>
        /// <param name="args"></param>
        public (string, string[]) ParseConfigFile(string[] args) {
            var commandFileParser = new CommandFileParser();
            return commandFileParser.Parse(args);
        }

        /// <summary>
        /// Combines all files in one folder and writes them into one spritesheet in output folder.
        /// Uses usersettings exportfiletype
        /// </summary>
        /// <param name="inputpath"></param>
        /// <param name="outputpath"></param>
        public string PackFolder(string inputpath, string outputpath) {
            return PackFolder(inputpath, outputpath, _userSettings.ExportFileType);
        }

        /// <summary>
        /// Combines all files in one folder and writes them into one spritesheet in output folder.
        /// </summary>
        /// <param name="inputpath"></param>
        /// <param name="outputpath"></param>
        public string PackFolder(string inputpath, string outputpath, FileType fileType)
        {
            var mappingFile = _exportFileFactory.Create(fileType);
            _spriteSheetPacker.PackImagesInFolder(inputpath, outputpath, mappingFile);
            return $"Created new sheet in {outputpath}";
        }

        /// <summary>
        /// Combine all sprites in all subfolders into one spritesheet at path
        /// Uses usersettings exportfiletype
        /// </summary>
        /// <param name="path"></param>
        public string CombineFromSubFolders(string path) {
            return CombineFromSubFolders(path, _userSettings.ExportFileType);
        }

        /// <summary>
        /// Combine all sprites in all subfolders into one spritesheet at path
        /// </summary>
        /// <param name="path"></param>
        public string CombineFromSubFolders(string path, FileType fileType) 
        {
            _spriteSheetPacker.PackImagesFromSubfolders(path, _exportFileFactory.Create(fileType));
            return $"Created new sheet @ {path}";
        }

        /// <summary>
        /// Split a sheet into frames of requestedSize x requestedSize
        /// </summary>
        /// <param name="size"></param>
        /// <param name="inputpath"></param>
        /// <returns></returns>
        public string SplitSheet(string size, string inputpath) {
            if (!short.TryParse(size, out short newSize)) {
                return $"size '{size}' is not a number (short)";
            }

            if (!System.IO.File.Exists(inputpath)) {
                return $"file @ {inputpath} does not exist";
            }

            var newPath = _spriteSheetPacker.SplitImage(inputpath, newSize);
            return $"Created new folder with images in {newPath}";
        }

        /// <summary>
        /// Creates black and white copes of all images in inputpath.
        /// </summary>
        /// <param name="inputpath"></param>
        /// <returns></returns>
        public string MakeBlackAndWhiteCopies(string inputpath) {
            _spriteSheetPacker.MakeBlackWhiteCopies(inputpath);
            return $"Made black and white copies in {inputpath}";
        }

        /// <summary>
        /// Resizes images to the requested size in the inputpath.
        /// </summary>
        /// <param name="size"></param>
        /// <param name="inputpath"></param>
        /// <returns></returns>
        public string ScaleImages(string size, string inputpath) {
            if (!int.TryParse(size, out int newSize)) {
                return $"size '{size}' is not a number";
            }

            if (!System.IO.Directory.Exists(inputpath)) {
                return $"dir @ {inputpath} does not exist";
            }

            var newPath =_spriteSheetPacker.ResizeImages(inputpath, newSize);
            return $"Created new images in: {newPath}";
        }

        /// <summary>
        /// Set the default export type (sets in environment variable).
        /// </summary>
        /// <param name="fileType"></param>
        /// <returns></returns>
        public string SetDefaultExportType(FileType fileType) {
            Environment.SetEnvironmentVariable(UserSettings.ENVIRONMENTVARIABLE_NAME, fileType.ToString());
            _userSettings.ExportFileType = fileType;
            return $"Export file type set to: {_userSettings.ExportFileType} in {UserSettings.ENVIRONMENTVARIABLE_NAME} environment variable.";
        }
    }
}