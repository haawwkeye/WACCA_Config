using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yuu.Ini;

namespace WACCA_Config
{
    public class IniFileHandler
    {
        private static readonly IniParserConfiguration IniParserConfig = new();

        public static IniDocument ReadFile(string filePath, IniParserConfiguration? config = null)
        {
            config ??= IniParserConfig;
            string fileContents = File.ReadAllText(filePath);
            return IniParser.Parse(fileContents, config);
        }

        public static void WriteFile(string filePath, IniDocument Content)
        {
            File.WriteAllText(filePath, Content.ToString());
        }
    }
}
