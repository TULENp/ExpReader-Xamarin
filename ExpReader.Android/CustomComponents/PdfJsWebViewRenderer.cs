using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExpReader.CustomComponents;
using ExpReader.Droid.CustomComponents;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(PdfJsWebView), typeof(PdfJsWebViewRenderer))]
namespace ExpReader.Droid.CustomComponents
{
    public class PdfJsWebViewRenderer : WebViewRenderer
    {
        private PdfJsWebView _pdfJsWebView;

        public PdfJsWebViewRenderer(Context context) : base(context)
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<WebView> e)
        {
            base.OnElementChanged(e);

            if (Control == null)
                return;

            _pdfJsWebView = Element as PdfJsWebView;

            Control.Settings.JavaScriptEnabled = true;
            Control.Settings.BuiltInZoomControls = true;
            Control.Settings.AllowContentAccess = true;
            Control.Settings.AllowFileAccess = true;
            Control.Settings.AllowFileAccessFromFileURLs = true;
            Control.Settings.AllowUniversalAccessFromFileURLs = true;

            if (_pdfJsWebView.Uri != null)
            {
                Control.LoadUrl(
                    $"file:///android_asset/pdfjs/web/viewer.html?" +
                    $"file={WebUtility.UrlEncode(_pdfJsWebView.Uri)}");
            }
        }

        protected override void OnElementPropertyChanged(object sender,
            PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            if (e.PropertyName == nameof(PdfJsWebView.Uri)
                && _pdfJsWebView.Uri != null)
            {
                Control.LoadUrl(
                    $"file:///android_asset/pdfjs/web/viewer.html?" +
                    $"file={WebUtility.UrlEncode(_pdfJsWebView.Uri)}");
            }
        }
    }
}