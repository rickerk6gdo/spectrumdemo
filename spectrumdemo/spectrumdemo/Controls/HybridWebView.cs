using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace spectrumdemo.Controls
{
    public class HybridWebView : WebView
    {
        private Action<string> _action;

        public static readonly BindableProperty UriProperty = BindableProperty.Create(
       propertyName: "Uri",
       returnType: typeof(string),
       declaringType: typeof(HybridWebView),
       defaultValue: default(string));

        public string Uri
        {
            get { return (string)GetValue(UriProperty); }
            set { SetValue(UriProperty, value); }
        }

        public object Source1 { get; internal set; }

        public void RegisterAction(Action<string> callback)
        {
            _action = callback;
        }

        public void Cleanup()
        {
            _action = null;
        }

        public void InvokeAction(string data)
        {
            if (_action == null || data == null)
            {
                return;
            }
            _action.Invoke(data);
        }
    }
}
