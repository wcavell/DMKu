using DMKu.Comm;
using MahApps.Metro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace DMKu
{
    public class AccentColorMenuData
    {
        public string Name { get; set; }
        public Brush ColorBrush { get; set; }

        private ICommand changeAccentCommand;

        public ICommand ChangeAccentCommand
        {
            get
            {
                return this.changeAccentCommand ?? (changeAccentCommand = new SimpleCommand { CanExecuteDelegate = x => true, ExecuteDelegate = x => ChangeAccent(x) });
            }
        }

        private void ChangeAccent(object sender)
        {
            var theme = ThemeManager.DetectTheme(Application.Current);
            var accent = ThemeManager.DefaultAccents.First(x => x.Name == this.Name);
            ThemeManager.ChangeTheme(Application.Current, accent, theme.Item1);
            App.config.AccentColor = Name;
        }
    }
}
