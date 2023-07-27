using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Nodes;

namespace API.Helpers;
public static class SettingsHelpers
{
    /// <summary>
    /// Reads the 'appsettings.json' file, retrieves the connection string value specified by 'connectionStringPath', and updates it for multiple sinks in the Serilog configuration.
    /// </summary>
    /// <param name="connectionStringPath">The path to the desired connection string in the 'appsettings.json' file.</param>
    public static void AddOrUpdateAppSettingLogging(string connectionStringPath)
    {
        try
        {
            var filePath = Path.Combine(
                Directory.GetCurrentDirectory(),
                "appsettings.json");
            string json = File.ReadAllText(filePath);

            JObject? jsonObj = JObject.Parse(json);

            var connectionStringValue = FindValueRecursively(connectionStringPath, jsonObj);

            //because i have 4 sinks to the DB I need to update 4 connection strings?
            for (int i = 7; i < 11; i++)
            {
                // TODO: Would like to improve this to be more dynamically adaptable to other formats.
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

    /// <summary>
    /// Recursively finds the value of a specified key within a nested data structure.
    /// </summary>
    /// <param name="sectionPathKey">The path to the desired key, separated by colons (':').</param>
    /// <param name="jsonObj">The hierarchical data structure (e.g., JSON object, dictionary) to search in.</param>
    /// <returns>The value of the specified key as a string, or null if the key is not found.</returns>
    private static string FindValueRecursively(string sectionPathKey, dynamic jsonObj)
    {
        // split the string at the first ':' character
        var remainingSections = sectionPathKey.Split(":", 2);

        var currentSection = remainingSections[0];
        if (remainingSections.Length > 1)
        {
            // continue with the process, moving down the tree
            var nextSection = remainingSections[1];
            return FindValueRecursively(nextSection, jsonObj[currentSection]);
        }
        else
        {
            return jsonObj[currentSection].ToString();
        }
    }
    /// <summary>
    /// Updates the provided object by following the key provided with the value 
    /// </summary>
    /// <param name="sectionPathKey">The key path inside the object</param>
    /// <param name="jsonToken">The object to be edited</param>
    /// <param name="value">The value to use</param>
    public static void SetValueRecursively(string sectionPathKey, JToken jsonToken, string value)
    {
        try
        {
            // Split the string at the first ':' character
            var remainingSections = sectionPathKey.Split(":", 2);
            var currentSection = remainingSections[0];

            // Initiate placeholder for the next json object
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
                var target = jsonToken[sectionPathKey];
                var propToChange = target.Parent as JProperty;
                propToChange.Value = value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while deserializing JSON: {ex.Message} at Json : {jsonToken}");
        }
    }
    /// <summary>
    /// Checks if the provided section contains array notation and extracts the index if present.
    /// </summary>
    /// <param name="section">The section string to check for array notation, e.g., "arrayName[index]".</param>
    /// <param name="index">Output parameter to store the extracted array index, if present.</param>
    /// <returns><see cref="bool"/> if the section contains valid array notation also <see cref="int"/> which is the index of the array</returns>
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
    /// <summary>
    /// Extracts the array name from the provided section string containing array notation.
    /// </summary>
    /// <param name="section">The section string with array notation, e.g., "arrayName[index]".</param>
    /// <param name="name">Output parameter to store the extracted array name.</param>
    private static void GetArrayName(string section, out string name)
    {
        var sections = section.Split("[", 2);
        name = sections[0];
    }
}
