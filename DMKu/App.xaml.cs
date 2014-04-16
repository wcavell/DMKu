using Microsoft.Practices.Prism.Events;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;

namespace DMKu
{
    /// <summary>
    /// App.xaml 的交互逻辑
    /// </summary>
    public partial class App : Application
    {
        public const string CachePathTip = "建议使用默认，即不修改路径";
        public const string ImagePathTip = "建议把背景图放到Skins文件夹下";
        public static IEventAggregator EventAggregator { get; set; }
        public static CfSet config;
        public static ConfigOperte cfOperte;
        public static string Type { get; set; }
        static App()
        {
            EventAggregator = new EventAggregator();
        }
        public App()
        {
            #region 
            /**
             * h
            Guid ownGuid = new Guid(((GuidAttribute)Attribute.GetCustomAttribute(Assembly.GetExecutingAssembly(), typeof(GuidAttribute))).Value);
            Guid proGUID;
            int ownPID = Process.GetCurrentProcess().Id;
            int proPID;
            foreach (Process p in Process.GetProcesses())
            {
                try
                {
                    proGUID = new Guid(((GuidAttribute)Attribute.GetCustomAttribute(Assembly.LoadFile(p.MainModule.FileName), typeof(GuidAttribute))).Value);
                    proPID = p.Id;
                    if (proGUID.Equals(ownGuid) && proPID != ownPID)
                    {
                        MessageBox.Show("程序已运行");
                        //Environment.Exit(Environment.ExitCode);
                        Application.Current.Shutdown();
                    }
                }
                catch
                {
                    continue;
                }
            }
            **/
            #endregion
            cfOperte = new ConfigOperte();
            config = cfOperte.Load();
            //App.Type = config.VideoInfoSource;
        }
    }
}
