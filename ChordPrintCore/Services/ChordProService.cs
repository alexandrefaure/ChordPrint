using System.Threading.Tasks;
using ChordPrintCore.Utils;

namespace ChordPrintCore.Services
{
    public class ChordProService
    {
        private readonly IConfigurationService ConfigurationService;

        public ChordProService(IConfigurationService configurationService)
        {
            ConfigurationService = configurationService;
        }

        public async Task ExecuteCommand(string arguments)
        {
            var exePath = "chordpro.exe";

            // Create temp config file path
            //var configurationFilePath = Path.GetTempPath() + Guid.NewGuid() + ".json";
            //File.WriteAllText(configurationFilePath, ConfigurationService.LoadConfigurationFileText());

            //var configFilePath = _globalContext.ConfigFilePath;
            //if (string.IsNullOrEmpty(configurationFilePath))
            //{
            //    await ShowErrorMessage("Chordpro config file not defined");
            //    return null;
            //}

            //var filename = Path.GetFileName(filePath);
            //if (string.IsNullOrEmpty(filename))
            //{
            //    await ShowErrorMessage("Input file not defined");
            //    return null;
            //}

            //var directoryPath = Path.GetDirectoryName(filePath);
            //if (string.IsNullOrEmpty(directoryPath))
            //{
            //    await ShowErrorMessage("Directory not found");
            //    return null;
            //}

            //var outputFilePath = directoryPath + "\\" + filename.Replace(".cho", ".pdf");
            //var arguments = $"\"{filePath}\" -o \"{outputFilePath}\" --cfg \"{configurationFilePath}\"";

            //if (!string.IsNullOrEmpty(TextSize))
            //{
            //    arguments += $" --text-size={TextSize}";
            //}

            var result = await ProcessAsyncHelper.ExecuteShellCommand(exePath, arguments, int.MaxValue);

            var error = result.Output;
            if (!string.IsNullOrEmpty(error))
            {
                //await ShowErrorMessage(error);
            }

            //return outputFilePath;
        }

        //private async Task<string> ConvertFileToPdf(string filePath)
        //{
        //    var exePath = "chordpro.exe";

        //    // Create temp config file path
        //    var configurationFilePath = Path.GetTempPath() + Guid.NewGuid() + ".json";
        //    File.WriteAllText(configurationFilePath, _configurationService.LoadConfigurationFileText());

        //    //var configFilePath = _globalContext.ConfigFilePath;
        //    if (string.IsNullOrEmpty(configurationFilePath))
        //    {
        //        await ShowErrorMessage("Chordpro config file not defined");
        //        return null;
        //    }

        //    var filename = Path.GetFileName(filePath);
        //    if (string.IsNullOrEmpty(filename))
        //    {
        //        await ShowErrorMessage("Input file not defined");
        //        return null;
        //    }

        //    var directoryPath = Path.GetDirectoryName(filePath);
        //    if (string.IsNullOrEmpty(directoryPath))
        //    {
        //        await ShowErrorMessage("Directory not found");
        //        return null;
        //    }

        //    var outputFilePath = directoryPath + "\\" + filename.Replace(".cho", ".pdf");
        //    var arguments = $"\"{filePath}\" -o \"{outputFilePath}\" --cfg \"{configurationFilePath}\"";

        //    if (!string.IsNullOrEmpty(TextSize))
        //    {
        //        arguments += $" --text-size={TextSize}";
        //    }

        //    var result = await ProcessAsyncHelper.ExecuteShellCommand(exePath, arguments, int.MaxValue);

        //    var error = result.Output;
        //    if (!string.IsNullOrEmpty(error))
        //    {
        //        await ShowErrorMessage(error);
        //        return null;
        //    }

        //    return outputFilePath;
        //}
    }
}