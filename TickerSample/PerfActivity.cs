using Android.OS;
using Robinhood.Ticker;
using Android.App;
using Android.Support.V7.Widget;
using Android.Views;
using System;
using System.Collections.Generic;

namespace TickerSample
{
    using static Resource;

    [Activity]
    public class PerfActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SetContentView(Layout.activity_perf);

            var recyclerView = FindViewById<RecyclerView>(Id.recyclerView);
            recyclerView.SetLayoutManager(new LinearLayoutManager(this));
            recyclerView.HasFixedSize = true;

            recyclerView.SetAdapter(new TestAdapter(boundViewHolders, GetRandomNumber));
        }

        protected override void OnUpdate()
        {
            boundViewHolders.ForEach(p => p.Update(true));
        }

        static char[] CharList = TickerUtils.GetDefaultNumberList();
        List<TickerViewHolder> boundViewHolders = new List<TickerViewHolder>();

        class TestAdapter : RecyclerView.Adapter
        {
            public TestAdapter(List<TickerViewHolder> boundViewHolders, Func<int, string> getRandomNumber) : base()
            {
                this.boundViewHolders = boundViewHolders;
                this.getRandomNumber = getRandomNumber;
            }

            public override int ItemCount => 1000;

            public override void OnBindViewHolder(RecyclerView.ViewHolder holder, int position)
            {
                var localHolder = (TickerViewHolder)holder;
                boundViewHolders.Add(localHolder);
                localHolder.Update(false);
            }

            public override RecyclerView.ViewHolder OnCreateViewHolder(ViewGroup parent, int viewType)
            {
                inflater = inflater ?? LayoutInflater.From(parent.Context);
                return new TickerViewHolder(inflater.Inflate(Layout.row_perf, parent, false), getRandomNumber);
            }

            LayoutInflater inflater;
            private readonly List<TickerViewHolder> boundViewHolders;
            private readonly Func<int, string> getRandomNumber;
        }

        class TickerViewHolder : RecyclerView.ViewHolder
        {
            public TickerViewHolder(View itemView, Func<int, string> getRandomNumber) : base(itemView)
            {
                ticker1 = itemView.FindViewById<TickerView>(Id.ticker1);
                ticker2 = itemView.FindViewById<TickerView>(Id.ticker2);
                ticker3 = itemView.FindViewById<TickerView>(Id.ticker3);
                ticker4 = itemView.FindViewById<TickerView>(Id.ticker4);

                ticker1.SetCharacterList(CharList);
                ticker2.SetCharacterList(CharList);
                ticker3.SetCharacterList(CharList);
                ticker4.SetCharacterList(CharList);
                this.getRandomNumber = getRandomNumber;
            }

            public void Update(bool animate)
            {
                ticker1.SetText(getRandomNumber(8), animate);
                ticker2.SetText(getRandomNumber(8), animate);
                ticker3.SetText(getRandomNumber(8), animate);
                ticker4.SetText(getRandomNumber(8), animate);
            }

            TickerView ticker1, ticker2, ticker3, ticker4;
            private readonly Func<int, string> getRandomNumber;
        }
    }
}