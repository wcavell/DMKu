using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interactivity;

namespace DMKu.Comm
{
    public class DMCommandAction : TargetedTriggerAction<UIElement>
    {
        public static readonly DependencyProperty CommandProperty =
            DependencyProperty.RegisterAttached("Command", typeof(ICommand), typeof(DMCommandAction), new FrameworkPropertyMetadata(null));

        public static readonly DependencyProperty CommandParameterProperty =
            DependencyProperty.RegisterAttached("CommandParameter", typeof(object), typeof(DMCommandAction), new FrameworkPropertyMetadata(null));
        /// <summary> 命令  
        /// </summary>  
        public ICommand Command
        {
            get { return (ICommand)GetValue(CommandProperty); }
            set { SetValue(CommandProperty, value); }
        }

        /// <summary> 参数  
        /// </summary>  
        public object CommandParameter
        {
            get { return GetValue(CommandParameterProperty); }
            set { SetValue(CommandParameterProperty, value); }
        }

        /// <summary>由给定的路由事件触发，参数是事件的发送方  
        /// </summary>  
        protected override void Invoke(object parameter)
        {
            if (Command == null) return;
            if (Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }
    }
}
