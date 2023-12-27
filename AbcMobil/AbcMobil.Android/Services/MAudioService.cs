using AbcMobil.Services;
using Android.Content;
using Android.Media;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AbcMobil.Droid.Services
{
    public class MAudioService : IMAudioService
    {
        public void playCensus(int hiz)
        {
            InitSoundpool();
            soundPool.Play(soundmap[4], 1, 1, 0, hiz, 1);
        }
        public void playAnkaraBaglari()
        {
            InitSoundpool();
            soundPool.Play(soundmap[5], 1, 1, 0, 0, 1);
        }
        public void InitSoundpool()
        {
            if (soundmap == null)
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
        //AudioManager audioManager;
        //int x = 0;
        //public static SoundPool s;
        //public static SoundPool so;
        //public static SoundPool sou;
        //public static SoundPool soun;
        //public static SoundPool sound;
        //public static SoundPool soundp;
        //public static AudioAttributes audoAt;
        //public void playCensus(int adet)
        //{
        //    SoundPool s = new SoundPool.Builder().SetMaxStreams(1).SetAudioAttributes(new AudioAttributes.Builder().Build()).Build();
        //    s.Play(s.Load(MainActivity.context, Resource.Drawable.beep4, 1), 1, 1, 0, 0, 1);
        //}
        //public void playCensus()
        //{
        //    //switch (x)
        //    //{
        //    //    case 0:
        //    audoAt = new AudioAttributes.Builder().Build();
        //    s = new SoundPool.Builder().SetMaxStreams(1).SetAudioAttributes(audoAt).Build();
        //    int a = s.Load(MainActivity.context, Resource.Drawable.beep5, 1);
        //    s.Play(a, 1, 1, 0, 0, 1);
        //    // break;
        //    //case 1:
        //    //    so = new SoundPool.Builder().SetMaxStreams(1).SetAudioAttributes(new AudioAttributes.Builder().Build()).Build();
        //    //    so.Play(so.Load(MainActivity.context, Resource.Drawable.beep5, 1), 1, 1, 0, 0, 1);
        //    //    break;
        //    //case 2:
        //    //    sou = new SoundPool.Builder().SetMaxStreams(1).SetAudioAttributes(new AudioAttributes.Builder().Build()).Build();
        //    //    sou.Play(sou.Load(MainActivity.context, Resource.Drawable.beep5, 1), 1, 1, 0, 0, 1);
        //    //    break;
        //    //case 3:
        //    //    soun = new SoundPool.Builder().SetMaxStreams(1).SetAudioAttributes(new AudioAttributes.Builder().Build()).Build();
        //    //    soun.Play(soun.Load(MainActivity.context, Resource.Drawable.beep5, 1), 1, 1, 0, 0, 1);
        //    //    break;
        //    //case 4:
        //    //    sound = new SoundPool.Builder().SetMaxStreams(1).SetAudioAttributes(new AudioAttributes.Builder().Build()).Build();
        //    //    sound.Play(sound.Load(MainActivity.context, Resource.Drawable.beep5, 1), 1, 1, 0, 0, 1);
        //    //    break;
        //    //default:
        //    // soundp = new SoundPool.Builder().SetMaxStreams(1).SetAudioAttributes(new AudioAttributes.Builder().Build()).Build();
        //    // soundp.Play(soundp.Load(MainActivity.context, Resource.Drawable.beep5, 1), 1, 1, 0, 0, 1);
        //    // break;
        //    //}
        //    //x++;
        //    //if (x == 5)
        //    //    x = 0;



        //    //if (x == 0)
        //    ////    x = soundPool.Play(, 1, 1, 0, 0, 1.5f);
        //    //audioManager = (AudioManager)MainActivity.context.GetSystemService(Context.AudioService);
        //    //int maxMusic = audioManager.GetStreamMaxVolume(Stream.System);
        //    //int curMusic = audioManager.GetStreamVolume(Stream.System);
        //    //audioManager.SetStreamVolume(Stream.System, maxMusic, VolumeNotificationFlags.ShowUi);
        //    audioManager = (AudioManager)MainActivity.context.GetSystemService("AudioService");
        //    audioManager.Mode = Mode.Normal;
        //    AudioManager am = (AudioManager)MainActivity.context.GetSystemService("AudioService");
        //    ////
        //    ////float audioMaxVolume = audioManager.GetStreamMaxVolume(Stream.Music);
        //    ////float audioCurrentVolume = audioManager.GetStreamVolume(Stream.Music);
        //    ////float volumnRatio = audioCurrentVolume / audioMaxVolume;
        //    //Task<bool> t= Task.Run(()=>
        //    //{

        //    //    return true;
        //    //});
        //}
        //public void playAnkaraBaglari()
        //{
        //    SoundPool s = new SoundPool.Builder().SetMaxStreams(1).SetAudioAttributes(new AudioAttributes.Builder().Build()).Build();
        //    s.Play(s.Load(MainActivity.context, Resource.Drawable.ankaraninBaglari, 1), 1, 1, 0, 0, 1);
        //}
    }
}