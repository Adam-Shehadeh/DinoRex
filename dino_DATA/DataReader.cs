using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.XPath;
using System.Xml.Serialization;

namespace dino_ENTITY
{
    public class DataReader
    {

        private static string settingsFile = "rex.xml";
        private static string fullpath = Environment.CurrentDirectory + "\\" + settingsFile;
        private ApplicationSettings defaultSettings = new ApplicationSettings( 1, 20, true );

        public DataReader() {
            Initialize();
        }
        public void Initialize() {
            if (!File.Exists(settingsFile)) {
                SaveSettingsToXML(defaultSettings); //Default settings if file does not exist.
            }
        }
        public ApplicationSettings ReadSettingsFromXML() {
            XmlSerializer reader = new XmlSerializer(typeof(ApplicationSettings));
            StreamReader file = new StreamReader(fullpath);
            ApplicationSettings local = (ApplicationSettings)reader.Deserialize(file);
            file.Close();
            return local;
        }
        public void SaveSettingsToXML(ApplicationSettings ap) {
            XmlSerializer writer = new XmlSerializer(typeof(ApplicationSettings));
            FileStream file = File.Create(fullpath);
            writer.Serialize(file, ap);
            file.Close();
        }
    }
}
