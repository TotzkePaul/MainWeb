using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace Main.Core.Localization.Models
{
    [XmlType("localizationDictionary")]
    public class LocalizationDictionary
    {

        [XmlArray("texts")]
        [XmlArrayItem("text")]
        public List<LanguageTexts> LanguageTexts { get; set; }
    }

    public class LanguageTexts
    {
        [XmlAttribute("name")]
        public string Name { get; set; }
        [XmlAttribute("value")]
        public string Value { get; set; }
    }
}
