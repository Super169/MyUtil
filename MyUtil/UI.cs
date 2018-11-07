using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyUtil
{
    public static partial class UI
    {
        public static void UpdateTextbox(System.Windows.Controls.TextBox tb, string xml, bool async = false)
        {
            if (System.Windows.Threading.Dispatcher.FromThread(System.Threading.Thread.CurrentThread) == null)
            {
                if (async)
                {
                    System.Windows.Application.Current.Dispatcher.BeginInvoke(
                      System.Windows.Threading.DispatcherPriority.Normal,
                      (Action)(() => UpdateTextbox(tb, xml)));
                }
                else
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(
                      System.Windows.Threading.DispatcherPriority.Normal,
                      (Action)(() => UpdateTextbox(tb, xml)));
                }
                return;
            }
            tb.Text = xml;
        }
    }
}
