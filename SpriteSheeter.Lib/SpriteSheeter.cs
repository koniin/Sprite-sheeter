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
        
        public SpriteSheetMaker(UserSettings userSettings)
        {
            _spriteSheetPacker = new SpriteSheetPacker(new SquareFrameListCombiner());
            _userSettings = userSettings;
            _exportFileFactory = new ExportFileTypeFactory();
        }

        /// <summary>
        /// Execute pack config file
        /// </summary>
        /// <param name="args"></param>
        public void ExecuteConfigFile(string[] args) {
            var commandFileParser = new CommandFileParser(
                _spriteSheetPacker,
                _userSettings,
                _exportFileFactory);

            commandFileParser.Execute(args);
        }

        /// <summary>
        /// Combines all files in one folder and writes them into one spritesheet in output folder.
        /// </summary>
        /// <param name="inputpath"></param>
        /// <param name="outputpath"></param>
        public string PackFolder(string inputpath, string outputpath)
        {
            _spriteSheetPacker.PackImagesInFolder(inputpath, outputpath, _exportFileFactory.Create(_userSettings.ExportFileType));
            return $"Created new sheet in {outputpath}";
        }

        /// <summary>
        /// Combine all sprites in all subfolders into one spritesheet at path
        /// </summary>
        /// <param name="path"></param>
        public string CombineFromSubFolders(string path) 
        {
            _spriteSheetPacker.PackImagesFromSubfolders(path, _exportFileFactory.Create(_userSettings.ExportFileType));
            return $"Created new sheet @ {path}";
        }

        /// <summary>
        /// Split a sheet into frames of requestedSize x requestedSize
        /// </summary>
        /// <param name="requestedSize"></param>
        /// <param name="inputpath"></param>
        /// <returns></returns>
        public string SplitSheet(string requestedSize, string inputpath) {
            short size;
            if(!short.TryParse(requestedSize, out size)) {
                return "requestedSize '{requestedSize}' is not a number (short)";
            }

            if(!System.IO.File.Exists(inputpath)) {
                return $"file @ {inputpath} does not exist";
            }

            var newPath = _spriteSheetPacker.SplitImage(inputpath, size);
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
        /// <param name="requestedSize"></param>
        /// <param name="inputpath"></param>
        /// <returns></returns>
        public string ScaleImages(string requestedSize, string inputpath) {
            int size;
            if (!int.TryParse(requestedSize, out size)) {
                return "requestedSize '{requestedSize}' is not a number (short)";
            }

            if (!System.IO.File.Exists(inputpath)) {
                return $"file @ {inputpath} does not exist";
            }

            var newPath =_spriteSheetPacker.ResizeImages(inputpath, size);
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