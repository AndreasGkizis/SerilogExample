using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace API.Helpers;
public static class SettingsHelpers
{
    public static void AddOrUpdateAppSettingLogging(string connectionStringPath)
    {
        try
        {
            //var apipath = GetPathForApi();

            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "appsettings.json");
            string json = File.ReadAllText(filePath);
            //dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            JObject? jsonObj = JObject.Parse(json);
            JToken? jsonToken = (JToken)jsonObj;


            var connectionStringValue = FindValueRecursively(connectionStringPath, jsonObj);

            //because i have 4 sinks to the DB I need to update 4 connection strings?
            for (int i = 7; i < 11; i++)
            {
                string sectionPathKey = $"Serilog:WriteTo[{i}]:Args:configureLogger:WriteTo[0]:Args:connectionString";

                SetValueRecursively(sectionPathKey, jsonObj, connectionStringValue);

            }


            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);

            File.Create(filePath).Close();
            File.WriteAllText(filePath, output);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing app settings | {0}, connection path given => {1}", ex.Message, connectionStringPath);
        }
    }


    private static string FindValueRecursively(string sectionPathKey, dynamic jsonObj)
    {
        // split the string at the first ':' character
        var remainingSections = sectionPathKey.Split(":", 2);

        var currentSection = remainingSections[0];
        if (remainingSections.Length > 1)
        {
            // continue with the procress, moving down the tree
            var nextSection = remainingSections[1];
            return FindValueRecursively(nextSection, jsonObj[currentSection]);
        }
        else
        {
            return jsonObj[currentSection].ToString();
        }
    }


    public static void SetValueRecursively(string sectionPathKey, JToken jsonToken, string value)
    {
        try
        {
            //var jsonObj = (JObject)jsonToken;
            // Split the string at the first ':' character
            var remainingSections = sectionPathKey.Split(":", 2);
            var currentSection = remainingSections[0];

            JToken nextJsonObj;

            if (remainingSections.Length > 1)
            {
                if (IsArrayNotation(currentSection, out var arrayIndex))
                {
                    GetArrayName(currentSection, out var arrayName);
                    var arrayObj = jsonToken[arrayName];
                    nextJsonObj = arrayObj[arrayIndex];
                }
                else
                {
                    nextJsonObj = jsonToken[currentSection];
                }

                var nextSection = remainingSections[1];
                SetValueRecursively(nextSection, nextJsonObj, value);
            }
            else
            {
                // We've got to the end of the tree, set the value!!
                var test = jsonToken[sectionPathKey];
                var propToChange = test.Parent as JProperty;
                propToChange.Value = value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while deserializing JSON: {ex.Message} at Json : {jsonToken}");
        }
    }
    private static bool IsArrayNotation(string section, out int index)
    {
        index = -1;
        if (section.EndsWith("]") && section.Contains("["))
        {
            var startIndex = section.IndexOf('[') + 1;
            var endIndex = section.IndexOf(']');
            if (int.TryParse(section.Substring(startIndex, endIndex - startIndex), out index))
            {
                return true;
            }
        }
        return false;
    }

    private static void GetArrayName(string section, out string name)
    {
        var sections = section.Split("[", 2);
        name = sections[0];

    }

    private static string GetPathForApi()
    {
        // Get the current directory
        string currentDirectory = Directory.GetCurrentDirectory();

        // Move up three levels
        for (int i = 0; i < 3; i++)
        {
            currentDirectory = Path.GetDirectoryName(currentDirectory);
        }

        // At this point, 'currentDirectory' will contain the path three levels up from the original current directory
        return currentDirectory;
    }
}
