using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Linq;
using System.Xml.Serialization;
using System.Runtime.Serialization;
using Main.Core.Localization.Models;

namespace Main.Core.Localization
{
    public class LocalizationConfig
    {
        public static readonly string Location = @"./SourceFiles/";
        public static readonly Dictionary<string,string> Files = new Dictionary<string, string> 
        {
            {"",      "Main.xml" },
            {"en-US",    "Main.xml"},
            {"en-GB",    "Main.xml"},
            {"en-AU",    "Main.xml"},
            {"zh-CN",    "Main-zh-CN.xml"},
            {"zh-TW",    "Main-zh-TW.xml"},
            {"zh-HK",    "Main-zh-HK.xml"},
            {"es",    "Main-es.xml"},
            {"fr",    "Main-fr.xml"},
            {"hi-IN",    "Main-hi-IN.xml"},
            {"ar",    "Main-ar.xml"},
            {"pt-BR",    "Main-pt-BR.xml"},
            {"ko-KR",    "Main-ko-KR.xml"},
            {"ta-IN",    "Main-ta-IN.xml"},
            {"ru-RU",    "Main-ru.xml"},
            {"de-DE",    "Main-de.xml"},
            {"ja-JP", "Main-ja.xml"}

        };

        public List<Culture> GetCultures()
        {
            List<Culture> cultures = new List<Culture>();
            foreach (var file in Files)
            {
                string filename = string.Format("{0}{1}", Location, file.Value);
                cultures.Add(GetCulture(file.Key, filename));
            }
            return cultures;
        }

        public Culture GetCulture(string name, string filename)
        {
            return new Culture
            {
                Name = name,
                Resources = GetResources(name, filename)
            };
        }


        public List<Resource> GetResources(string name, string filename)
        {
            List<Resource> resources = XmlToResources(filename);
            return resources;
        }


        public List<Resource> XmlToResources(string filepath)
        {

            XmlSerializer serializer = new XmlSerializer(typeof(LocalizationDictionary));
            LocalizationDictionary ld;
            using (FileStream xmlStream = new FileStream(filepath, FileMode.Open,FileAccess.ReadWrite))
            {
                ld = (LocalizationDictionary)serializer.Deserialize(xmlStream);
            }
            

            return ld.LanguageTexts.Select(x=>new Resource(){ Key = x.Name, Value = x.Value}).ToList();
        }
        /*
        public object Deserialize(string xml, Type type)
        {
            XmlSerializer _serializer = new XmlSerializer(typeof(Request));
            XDocument doc = XDocument.Parse(xml);
            using (var reader = new StringReader(xml))
            {
                obj = (Request)_serializer.Deserialize(reader);
            }
            return result;
        }

        public T Deserialize<T>(string xml)
        {
            return (T)Deserialize(xml, typeof(T));
        }*/



        }
    }
