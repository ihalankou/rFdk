using System;
using System.Collections.Generic;
using System.Linq;

namespace RHost
{
    class BarCursor
    {
        public BarCursor(BarData[] data)
        {
            _data = data;
        }


        int _position;

        static BarCursor()
        {
            var nowTime = DateTime.UtcNow;
            undefined = new BarData(nowTime, nowTime,
                double.NaN, double.NaN, double.NaN, double.NaN, double.NaN);

        }

        static readonly BarData undefined;
        private BarData[] _data;

        public bool CanContinue
        {
            get
            {
                return _position < _data.Length;
            }
        }

        public BarData Current {
            get
            {
                if (CanContinue)
                    return _data[_position];
                return undefined;
            }
        }

        public BarData Previous { get
            {
                if (_position == 0)
                    return undefined;
                return _data[_position - 1];
            }
        }

        public void CopyRange(List<BarData> resultBars,  BarData undefinedBar, bool isUndefinedBid)
        {
            BarData[] barsData = _data;
            int position = _position;

            for (var i = position; i < barsData.Length; i++)
            {
                if (isUndefinedBid)
                {
                    FdkBarsMerger.AddBarPairs(resultBars, undefinedBar, barsData[i]);
                }
                else
                {
                    FdkBarsMerger.AddBarPairs(resultBars, barsData[i], undefinedBar);
                }
            }
        }
        internal void Next()
        {
            _position++;
        }
    }
}