using AbcMobil.Services;
using Android.App;
using Android.Content;
using Android.Media;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AbcMobil.Droid.Service
{
    public class MAudioService : IMAudioService
    {
        public void playCensus()
        {
            InitSoundpool();
            soundPool.Play(soundmap[4], 1, 1, 0, 0, 1);
        }
        public void playAnkaraBaglari()
        {
            InitSoundpool();
            soundPool.Play(soundmap[5], 1, 1, 0, 0, 2);
        }
        public void InitSoundpool()
        {
            if(soundmap== null)
            {
                AudioAttributes audioAttributes = new AudioAttributes.Builder().Build();
                soundPool = new SoundPool.Builder().SetMaxStreams(1).SetAudioAttributes(audioAttributes).Build();
                soundmap = new List<int>();
                //soundmap.Add(soundPool.Load(MainActivity.context, Resource.Drawable.beepSound, 1));
                soundmap.Add(soundPool.Load(MainActivity.context, Resource.Drawable.beep1, 1));
                soundmap.Add(soundPool.Load(MainActivity.context, Resource.Drawable.beep2, 1));
                soundmap.Add(soundPool.Load(MainActivity.context, Resource.Drawable.beep3, 1));
                soundmap.Add(soundPool.Load(MainActivity.context, Resource.Drawable.beep4, 1));
                soundmap.Add(soundPool.Load(MainActivity.context, Resource.Drawable.beep5, 1));
                soundmap.Add(soundPool.Load(MainActivity.context, Resource.Drawable.ankaraninBaglari, 1));
            }
        }
        public static SoundPool soundPool;
        public static IList<int> soundmap;
    }
}