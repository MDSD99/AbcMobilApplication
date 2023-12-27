using AbcMobil;
using AbcMobil.Droid;
using Android.Content;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(EntryBorderLess), typeof(EntryBorderLessRenderer))]
namespace AbcMobil.Droid
{
    public class EntryBorderLessRenderer : EntryRenderer
    {
        public EntryBorderLessRenderer(Context context) : base(context)
        {

        }
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement == null)
            {
                Control.Background = null;
                var layoutparams = new MarginLayoutParams(Control.LayoutParameters);
                //layoutparams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutparams;
                Control.LayoutParameters = layoutparams;
                //Control.SetPadding(0, 0, 0, 0);
                //SetPadding(0, 0, 0, 0);
            }
        }
    }
}