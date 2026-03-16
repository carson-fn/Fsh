using System.Collections.Generic;
using System.IO;

public static class EnvLoader
{
    public static Dictionary<string, string> Load()
    {
        string path = ".env";
        var dict = new Dictionary<string, string>();

        foreach (var line in File.ReadAllLines(path))
        {
            if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                continue;

            var parts = line.Split('=', 2);
            dict[parts[0]] = parts[1];
        }

        return dict;
    }
}