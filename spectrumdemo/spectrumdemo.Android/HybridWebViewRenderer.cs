using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using static Android.Webkit.WebChromeClient;
using Java.Lang;
using Android.Provider;
using spectrumdemo.Controls;
using Xamarin.Essentials;
using System.Threading.Tasks;
using System.IO;
using spectrumdemo.Droid;
using static spectrumdemo.HybridWebViewRenderer.CameraFormsWebChromeClient;

[assembly: ExportRenderer(typeof(HybridWebView), typeof(HybridWebViewRenderer))]


// used for a known bug in the driod webview.... you can't take a pic in a xmarian webview but you can from the browser.

// also used for geeting data back from a webview.  The webview can make a javascript call and we can parse it with this code.

namespace spectrumdemo.Droid
{


    public class HybridWebViewRenderer : WebViewRenderer
    {
        private const string JavascriptFunction = "function invokeCSharpAction(data){jsBridge.invokeAction(data);}";
        Context _context;

        public HybridWebViewRenderer(Context context) : base(context)
        {
            _context = context;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null)
            {
                Control.RemoveJavascriptInterface("jsBridge");
                ((HybridWebView)Element).Cleanup();
            }
            if (e.NewElement != null)
            {
                // Setting the WebViewClient here overwrites the FormsWebViewClient which handles
                // web page navigation and raises WebNavigated event on the Forms WebView
                //Control.SetWebViewClient(new JavascriptWebViewClient($"javascript: {JavascriptFunction}"));
          //      Control.AddJavascriptInterface(new JsBridge(this), "jsBridge");

                //// No need this since we're loading dynamically generated HTML content
                //Control.LoadUrl($@"file:///android_asset/Content/{((HybridWebView)Element).Uri}");
            }
        }

        protected override FormsWebChromeClient GetFormsWebChromeClient()
        {
            return new CameraFormsWebChromeClient();
        }

       protected override Android.Webkit.WebViewClient GetWebViewClient()
       {
            return new JavascriptFormsWebViewClient(this, JavascriptFunction);
        }
    }

    public class JavascriptFormsWebViewClient : FormsWebViewClient
    {
        readonly string _javascript;
        public JavascriptFormsWebViewClient(WebViewRenderer renderer, string javascript) : base(renderer)
        {
            _javascript = javascript;
        }

        public override void OnPageFinished(Android.Webkit.WebView view, string url)
        {
            base.OnPageFinished(view, url);
            view.EvaluateJavascript(_javascript, null);
        }
    }
    
    public class CameraFormsWebChromeClient : FormsWebChromeClient
    {
        string _photoPath;
        public override bool OnShowFileChooser(Android.Webkit.WebView webView, Android.Webkit.IValueCallback filePathCallback, FileChooserParams fileChooserParams)
        {

            AlertDialog.Builder alertDialog = new AlertDialog.Builder((Context)MainActivity.InstanceCount);

            if (!Constants.isvideo)
            {
                alertDialog.SetTitle("Takea picture or choose a file");


                alertDialog.SetNeutralButton("Take picture", async (sender, alertArgs) =>
                {
                    try
                    {
                        var photo = await MediaPicker.CapturePhotoAsync();
                        var uri = await LoadPhotoAsync(photo);
                        filePathCallback.OnReceiveValue(uri);
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
                    }
                });
                alertDialog.SetNegativeButton("Choose picture", async (sender, alertArgs) =>
                {
                    try
                    {
                        var photo = await MediaPicker.PickPhotoAsync();
                        var uri = await LoadPhotoAsync(photo);
                        filePathCallback.OnReceiveValue(uri);
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine($"PickPhotoAsync THREW: {ex.Message}");
                    }
                });

            }
            else
            {
                alertDialog.SetTitle("Take a Video or choose a file");
                alertDialog.SetNeutralButton("Take Video", async (sender, alertArgs) =>
                {
                    try
                    {
                        var photo = await MediaPicker.CaptureVideoAsync();
                        var uri = await LoadPhotoAsync(photo);
                        filePathCallback.OnReceiveValue(uri);
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine($"CaptureCideoAsync THREW: {ex.Message}");
                    }
                });

                alertDialog.SetNegativeButton("Choose Video", async (sender, alertArgs) =>
                {
                    try
                    {
                        var photo = await MediaPicker.PickPhotoAsync();
                        var uri = await LoadPhotoAsync(photo);
                        filePathCallback.OnReceiveValue(uri);
                    }
                    catch (System.Exception ex)
                    {
                        System.Console.WriteLine($"PickVideoAsync THREW: {ex.Message}");
                    }
                });
            }
            alertDialog.SetPositiveButton("Cancel", (sender, alertArgs) =>
            {
                filePathCallback.OnReceiveValue(null);
            });
            Dialog dialog = alertDialog.Create();
            dialog.Show();
            return true;
        }
        async Task<Android.Net.Uri[]> LoadPhotoAsync(FileResult photo)
        {
            // canceled
            if (photo == null)
            {
                _photoPath = null;
                return null;
            }
            // save the file into local storage
            var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
            using (var stream = await photo.OpenReadAsync())
            using (var newStream = System.IO.File.OpenWrite(newFile))
                await stream.CopyToAsync(newStream);
            _photoPath = newFile;
            Android.Net.Uri uri = Android.Net.Uri.FromFile(new Java.IO.File(_photoPath));
            return new Android.Net.Uri[] { uri };
        }
    }
    

}