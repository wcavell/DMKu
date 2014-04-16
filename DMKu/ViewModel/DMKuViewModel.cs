using DMKu.Comm;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace DMKu.ViewModel
{
    public class DMKuViewModel:ViewModelBase
    {
        #region Property
        public List<AccentColorMenuData> AccentColors { get; set; }

        private string _SelectVFrom;
        public string SelectVFrom
        {
            get { return _SelectVFrom; }
            set
            {
                if (value != null)
                {
                    _SelectVFrom = value;
                    RaisePropertyChanged("SelectVFrom");
                    if (value == "A")
                        App.config.VideoInfoSource = "A";
                    else if (value == "B")
                        App.config.VideoInfoSource = "B";
                    else if (value == "C")
                        App.config.VideoInfoSource = "C";
                }
            }
        }

        private string _SeachText;
        public string SeachText
        {
            get
            {
                return _SeachText;
            }
            set
            {
                _SeachText = value;
                RaisePropertyChanged("SeachText");
            }
        }

        private string WHomeData;
        public string HomeData
        {
            get;
            set;
        }
        #endregion 
        public DMKuViewModel()
        {
            ThemeDarkCommand = new SimpleCommand<object, object>(ThemeDarkClickCommand);
            ThemeLightCommand = new SimpleCommand<object, object>(ThemeLightClickCommand);
            //DetailedSettingCommand = new SimpleCommand<object, object>(SettingClickCommand);
            //AboutCommand = new SimpleCommand<object, object>(AboutClickCommand);
            SeachCommand = new SimpleCommand<object, object>(SeachContentClickCommand);
            this.AccentColors = ThemeManager.DefaultAccents
                                           .Select(a => new AccentColorMenuData() { Name = a.Name, ColorBrush = a.Resources["AccentColorBrush"] as Brush })
                                           .ToList();
            RefContentCommand = new SimpleCommand<object, object>(RefContentClickCommand);
            InitTheme();
            InitChange();
            InitData();
        }
        #region Method
        public void InitTheme()
        {
            var accent = ThemeManager.DefaultAccents.First(x => x.Name == App.config.AccentColor);
            if (App.config.Theme == "Light")
            {
                ThemeManager.ChangeTheme(Application.Current, accent, Theme.Light);
            }
            if (App.config.Theme == "Dark")
            {
                ThemeManager.ChangeTheme(Application.Current, accent, Theme.Dark);
            }
        }

        public async void InitData()
        {
            /**
            IHomeDate Data;
            #region switch
            switch (App.config.VideoInfoSource)
            {
                case "A":
                    Data = new AcHomeDate();
                    break;
                case "B":
                    Data = new BiliHomeDate();
                    break;
                case "C":
                    Data = new TuCaoHomeDate();
                    break;
                default:
                    Data = new BiliHomeDate();
                    break;
            }
            #endregion
            var s = await Data.GetData();
             */
        }

        private void InitChange()
        {
            SelectVFrom = App.config.VideoInfoSource;
        }
        #endregion

        #region ICommand
        public ICommand SeachCommand { get; private set; }
        public ICommand RefContentCommand { get; set; }

        public ICommand ThemeDarkCommand { get; set; }
        public ICommand ThemeLightCommand { get; set; }
        public ICommand DetailedSettingCommand { get; set; }
        public ICommand AboutCommand { get; set; }
        #endregion
        #region event
        public void ThemeDarkClickCommand(object sender)
        {
            var theme = ThemeManager.DetectTheme(Application.Current);
            ThemeManager.ChangeTheme(Application.Current, theme.Item2, Theme.Dark);
            App.config.Theme = "Dark";
        }
        public void ThemeLightClickCommand(object sender)
        {
            var theme = ThemeManager.DetectTheme(Application.Current);
            ThemeManager.ChangeTheme(Application.Current, theme.Item2, Theme.Light);
            App.config.Theme = "Light";
        }

        public void RefContentClickCommand(object sender)
        {

        }
        private void SeachContentClickCommand(object sender)
        {
            TextBox tb = sender as TextBox;
            int reslut = 0;
            if (int.TryParse(tb.Text, out reslut))
            {

            }
            else
            {
                string xpath = "[a-z]{1,2}[0-9]{1,8}";
                Regex regex = new Regex(xpath);
                if (regex.IsMatch(tb.Text))
                {
                    int i = tb.Text.IndexOf("ac");
                    if (i == 0)
                    {
                        //acfun
                        int s;
                        if (int.TryParse(tb.Text.Substring(2), out s))
                        {

                        }
                    }
                    else if (i < 0)
                    {
                        i = tb.Text.IndexOf("av");
                        if (i == 0)
                        {

                        }
                    }
                }
                else
                {


                }

            }
        }
        /*
        private ConfigSetting win;
        public void SettingClickCommand(object sender)
        {
            if (win == null)
            {
                win = new ConfigSetting();
                win.Closed += (o, args) => win = null;
            }
            if (win.IsVisible)
                win.Hide();
            else
                win.Show();
        }

        private AboutWindow abwin;
        public void AboutClickCommand(object sender)
        {
            if (abwin == null)
            {
                abwin = new AboutWindow();
                abwin.Closed += (o, args) => abwin = null;
            }
            if (abwin.IsVisible)
                abwin.Hide();
            else
                abwin.Show();
        }
         */
        #endregion
    }
}
