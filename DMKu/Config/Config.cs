using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;

namespace DMKu
{
    public class ConfigOperte
    {
        XmlDocument doc;
        public ConfigOperte()
        {
            doc = new XmlDocument();
            doc.Load("Config//UserConfig.xml");
        }
        public CfSet Load()
        {
            CfSet cf = new CfSet();
            var root = doc.DocumentElement;
            try
            {
                cf.AccentColor = root.SelectSingleNode("AccentColor").InnerText;
                cf.AutoUpdate = root.SelectSingleNode("AutoUpdate").InnerText;
                cf.BackgoundImage = root.SelectSingleNode("BackgoundImage").InnerText;
                cf.CachePath = root.SelectSingleNode("CachePath").InnerText;
                cf.CacheSaveTime = root.SelectSingleNode("CacheSaveTime").InnerText;
                cf.VideoInfoSource = root.SelectSingleNode("VideoInfoFrom").InnerText;
                cf.Theme = root.SelectSingleNode("Theme").InnerText;
                cf.Version = root.SelectSingleNode("Version").InnerText;
                cf.VideoCache = root.SelectSingleNode("VideoCache").InnerText;
                switch (root.SelectSingleNode("VideoDefinition").InnerText)
                {
                    case "Default":
                        cf.VideoDefinition = VideoDefinition.Default;
                        break;
                    case "Dregs":
                        cf.VideoDefinition = VideoDefinition.Dregs;
                        break;
                    case "720P":
                        cf.VideoDefinition = VideoDefinition.W720P;
                        break;
                    case "1080P":
                        cf.VideoDefinition = VideoDefinition.W1080P;
                        break;
                }
            }
            catch { }
            return cf;
        }
        public void Save()
        {
            if (App.config != null)
            {
                var root = doc.DocumentElement;
                try
                {
                    root.SelectSingleNode("AccentColor").InnerText = App.config.AccentColor;
                    root.SelectSingleNode("AutoUpdate").InnerText = App.config.AutoUpdate;
                    root.SelectSingleNode("BackgoundImage").InnerText = App.config.BackgoundImage;
                    root.SelectSingleNode("CachePath").InnerText = App.config.CachePath;
                    root.SelectSingleNode("CacheSaveTime").InnerText = App.config.CacheSaveTime;
                    root.SelectSingleNode("VideoInfoFrom").InnerText = App.config.VideoInfoSource;
                    root.SelectSingleNode("Theme").InnerText = App.config.Theme;
                    root.SelectSingleNode("Version").InnerText = App.config.Version;
                    root.SelectSingleNode("VideoCache").InnerText = App.config.VideoCache;
                    switch (App.config.VideoDefinition)
                    {
                        case VideoDefinition.Default:
                            root.SelectSingleNode("VideoDefinition").InnerText = "Default";
                            break;
                        case VideoDefinition.Dregs:
                            root.SelectSingleNode("VideoDefinition").InnerText = "Dregs";
                            break;
                        case VideoDefinition.W720P:
                            root.SelectSingleNode("VideoDefinition").InnerText = "720P";
                            break;
                        case VideoDefinition.W1080P:
                            root.SelectSingleNode("VideoDefinition").InnerText = "1080P";
                            break;
                        default:
                            root.SelectSingleNode("VideoDefinition").InnerText = "Default";
                            break;
                    }
                }
                catch { }
                doc.Save("Config//UserConfig.xml");
            }
        }
    }
    public class CfSet
    {
        public string Version { get; set; }
        public string BackgoundImage { get; set; }
        public string VideoInfoSource { get; set; }
        public VideoDefinition VideoDefinition { get; set; }
        public string CacheSaveTime { get; set; }
        public string CachePath { get; set; }
        public string VideoCache { get; set; }
        public string AutoUpdate { get; set; }
        public string Theme { get; set; }
        public string AccentColor { get; set; }
    }

    public enum VideoDefinition
    {
        Default,
        Dregs,
        W720P,
        W1080P
    }
}
