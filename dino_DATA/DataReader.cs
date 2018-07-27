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
        private static string fullpath = AppDomain.CurrentDomain.BaseDirectory + settingsFile;    //Absolute path to app directory even when on shortcut
        //private static string fullpath = Environment.CurrentDirectory + "\\" + settingsFile;        //directory of what we execute
        private ApplicationSettings defaultSettings = new ApplicationSettings() {
            interval1 = 1,
            interval2 = 20,
            counter = 0,
            status = "Off",
            enabled = true,
            secondsTilNext = 20,
            selectedCharacter = "Bongo"
        };

        public DataReader() {
            Initialize();
            
        }
        public void Initialize() {
            if (!File.Exists(fullpath)) {
                SaveSettingsToXML(defaultSettings); //Default settings if file does not exist.
            }
        }
        public ApplicationSettings ReadSettingsFromXML() {
            XmlSerializer reader = new XmlSerializer(typeof(ApplicationSettings));
            StreamReader file = new StreamReader(@fullpath);
            ApplicationSettings local = (ApplicationSettings)reader.Deserialize(file);
            file.Close();
            return local;
        }
        public void SaveSettingsToXML(ApplicationSettings ap) {
            XmlSerializer writer = new XmlSerializer(typeof(ApplicationSettings));
            FileStream file = File.Create(@fullpath);
            writer.Serialize(file, ap);
            file.Close();
        }
    }
}
