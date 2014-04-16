using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Prism.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DMKu.ViewModel
{
    public class ViewModelBase : NotificationObject
    {
        /// <summary>
        /// 事件聚合器
        /// </summary>
        protected IEventAggregator EventAggregator
        {
            get
            {
                return App.EventAggregator;
            }
        }
    }
}
