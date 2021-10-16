namespace SpriteSheeter.Cli {
    public class DefaultUserSettingsReader {

        private const string FileTypeSetting = "defaultfiletype";

        //public UserSettings() {
        //    var appSettings = ConfigurationManager.AppSettings;
        //    ExportFileType = (FileType)Enum.Parse(typeof(FileType), appSettings[FileTypeSetting]);
        //}

        //public void Save() {
        //    var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
        //    configFile.AppSettings.Settings[FileTypeSetting].Value = ExportFileType.ToString();
        //    configFile.Save(ConfigurationSaveMode.Modified);
        //    ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
        //}
    }
}