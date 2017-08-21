using System;
using Android.OS;
using Android.Support.V7.App;
using Java.Lang;

namespace TickerSample
{
    public abstract class BaseActivity : AppCompatActivity
    {

        protected override void OnResume()
        {
            base.OnResume();
            _resumed = true;

            handler.Post(CreateRunnable());
        }

        protected override void OnPause()
        {
            _resumed = false;
            base.OnPause();
        }

        Runnable CreateRunnable() =>
         new Runnable(delegate
         {
             OnUpdate();
             if (_resumed)
             {
                 handler.PostDelayed(CreateRunnable(), random.Next(1750) + 250);
             }
         });


        protected string GetRandomNumber(int digits)
        {
            int digitsInPowerOf10 = (int)System.Math.Pow(10, digits);
            return (random.Next(digitsInPowerOf10) + digitsInPowerOf10 + (random.Next(8) + 1)).ToString();
        }

        protected abstract void OnUpdate();

        protected static Random random = new Random(CurrentTimeMillis);


        Handler handler = new Handler();
        bool _resumed;

        private static readonly DateTime Jan1st1970 =
            new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        static int CurrentTimeMillis =>
         (int)(DateTime.UtcNow - Jan1st1970).TotalMilliseconds;

    }
}