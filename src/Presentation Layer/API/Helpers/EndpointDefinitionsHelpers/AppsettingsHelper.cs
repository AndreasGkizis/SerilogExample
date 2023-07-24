using Newtonsoft;
namespace API.Helpers;
public static class SettingsHelpers
{
    public static void AddOrUpdateAppSettingLogging(string connectionStringPath)
    {
        try
        {
            var filePath = Path.Combine(AppContext.BaseDirectory, "appsettings.json");
            string json = File.ReadAllText(filePath);
            dynamic jsonObj = Newtonsoft.Json.JsonConvert.DeserializeObject(json);


            var connectionStringValue = FindValueRecursively(connectionStringPath, jsonObj);

            //because i have 4 sinks to the DB I need to update 4 connection strings?
            for (int i = 7; i < 11; i++)
            {
                string sectionPathKey = $"Serilog:WriteTo[{i}]:Args:configureLogger:WriteTo[0]:Args:connectionString";
                // configuration[$"Serilog:WriteTo:7:Args:configureLogger:WriteTo:0:Args:connectionString"] = logConnectionString;

                SetValueRecursively(sectionPathKey, jsonObj, connectionStringValue);

            }


            string output = Newtonsoft.Json.JsonConvert.SerializeObject(jsonObj, Newtonsoft.Json.Formatting.Indented);
            File.WriteAllText(filePath, output);
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error writing app settings | {0}, connection path given => {1}", ex.Message, connectionStringPath);
        }
    }

    private static void SetValueRecursively<T>(string sectionPathKey, dynamic jsonObj, T value)
    {
        try
        {

            // split the string at the first ':' character
            var remainingSections = sectionPathKey.Split(":", 2);

            var currentSection = remainingSections[0];
            // if (currentSection.ToString() == "WriteTo"){
            //     System.Console.WriteLine(  "gocha");



            // }
            if (remainingSections.Length > 1)
            {
                // continue with the procress, moving down the tree
                var nextSection = remainingSections[1];
                SetValueRecursively(nextSection, jsonObj[currentSection], value);
            }
            else
            {
                // we've got to the end of the tree, set the value
                jsonObj[currentSection] = value;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error while deserializing JSON: {ex.Message}");

        }
    }
    private static object FindValueRecursively(string sectionPathKey, dynamic jsonObj)
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
            // var bool1 =jsonObj[currentSection].ToString(); 
            // var whatisit = jsonObj[currentSection];
            // if (jsonObj[currentSection].ToString())
            // {

            // we've got to the end of the tree, get the value
            return jsonObj[currentSection].ToString();
            // }else
            // {
            //     return new KeyNotFoundException("The Connection string specified does not exist in appsettings.json, maybe you forgot to rebuild?");
            // }
        }
    }
}
