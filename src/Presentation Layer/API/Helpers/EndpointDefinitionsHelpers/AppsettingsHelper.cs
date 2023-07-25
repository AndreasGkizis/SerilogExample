using Newtonsoft;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

namespace API.Helpers;
public static class SettingsHelpers
{
    public static void AddOrUpdateAppSettingLogging(string connectionStringPath)
    {
        try
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            string json = File.ReadAllText(filePath);
            //dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);

            JObject? jsonObj = JObject.Parse(json);
            JToken? jsonToken = (JToken)jsonObj;


            var connectionStringValue = FindValueRecursively(connectionStringPath, jsonObj);

            //because i have 4 sinks to the DB I need to update 4 connection strings?
            for (int i = 7; i < 11; i++)
            {
                string sectionPathKey = $"Serilog:WriteTo[{i}]:Args:configureLogger:WriteTo[0]:Args:connectionString";
                // configuration[$"Serilog:WriteTo:7:Args:configureLogger:WriteTo:0:Args:connectionString"] = logConnectionString;

                SetValueRecursively(sectionPathKey, jsonToken, connectionStringValue);

            }


            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
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
            var jsonObj = (JObject)jsonToken;
            // Split the string at the first ':' character
            var remainingSections = sectionPathKey.Split(":", 2);
            var currentSection = remainingSections[0];

            if (IsArrayNotation(currentSection, out var arrayIndex))
            {
                GetArrayName(currentSection, out var arrayName);

                var array = jsonObj[arrayName];

                var arrayPositionObject = array[arrayIndex];

                var nextSection = remainingSections[1];

                SetValueRecursively(nextSection, arrayPositionObject, value);

            }
            else
            {
                jsonToken = jsonObj[currentSection];
            }

            if (remainingSections.Length > 1)
            {
                var nextSection = remainingSections[1];
                SetValueRecursively(nextSection, jsonToken, value);
            }
            else
            {
                // We've got to the end of the tree, set the value!!
                if (jsonToken.Type == JTokenType.String)
                {
                    var propToChange = jsonToken.Parent as JProperty;
                    propToChange.Value = value;
                }
            }

            //else if (jsonToken.Type == JTokenType.Array)
            //{
            //    var jsonArray = (JArray)jsonToken;
            //    foreach (var item in jsonArray)
            //    {
            //        SetValueRecursively(sectionPathKey, item, value);
            //    }
            //}
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
}
