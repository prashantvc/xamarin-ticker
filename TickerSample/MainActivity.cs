using Android.App;
using Android.Widget;
using Android.OS;
using Robinhood.Ticker;
using static TickerSample.Resource;
using System;
using Android.Content;

namespace TickerSample
{
    [Activity(Label = "TickerSample", MainLauncher = true, Icon = "@mipmap/icon")]
    public class MainActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Layout.activity_main);

            alphabetList = new char[53];
            alphabetList[0] = TickerUtils.EmptyChar;

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < 26; j++)
                {
                    // Add all lowercase characters first, then add the uppercase characters.
                    alphabetList[1 + i * 26 + j] = (char)((i == 0) ? j + 97 : j + 65);
                }
            }

            ticker1 = FindViewById<TickerView>(Id.ticker1);
            ticker2 = FindViewById<TickerView>(Id.ticker2);
            ticker3 = FindViewById<TickerView>(Id.ticker3);

            ticker1.SetCharacterList(NumberList);
            ticker2.SetCharacterList(CurrencyList);
            ticker3.SetCharacterList(alphabetList);

            var perfButton = FindViewById<Button>(Id.perfBtn);

            perfButton.Click += delegate {
                StartActivity(new Intent(this, typeof(PerfActivity)));
            };
        }

        protected override void OnUpdate()
        {
            //Set random number
            int digits = random.Next(2) + 6;
            ticker1.SetText(GetRandomNumber(digits));
            
            //Set random currency
            string currenyText = (random.NextDouble() * 100).ToString();
            ticker2.SetText($"${currenyText.Substring(0, Math.Min(digits, currenyText.Length))}");

            ticker3.SetText(GenerateChars(random, alphabetList, digits));
        }

        string GenerateChars(Random localRandom, char[] list, int numDigits)
        {
            char[] result = new char[numDigits];
            for (int i = 0; i < numDigits; i++)
            {
                result[i] = list[localRandom.Next(list.Length)];
            }

            return new string(result);
        }

        char[] alphabetList;
        TickerView ticker1, ticker2, ticker3;

        static char[] NumberList = TickerUtils.GetDefaultNumberList();
        static char[] CurrencyList = TickerUtils.GetDefaultListForUSCurrency();
    }
}

