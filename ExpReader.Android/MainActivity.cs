using Android.App;
using Android.Content.PM;
using Android.OS;
using Android.Runtime;
using ExpReader.Services;
using System;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: Dependency(typeof(ExpReader.Droid.MainActivity.Environment))]
namespace ExpReader.Droid
{
    [Activity(Label = "ExpReader", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }

        public class Environment : IEnvironment
        {
            [Obsolete]
            public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
            {
                if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
                    return;

                var activity = Xamarin.Essentials.Platform.CurrentActivity;
                var window = activity.Window;

                //this may not be necessary(but may be fore older than M)
                window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
                window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);


                if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
                {
                    var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
                    window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
                }

                window.SetStatusBarColor(color.ToPlatformColor());
            }
        }
    }
}