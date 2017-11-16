using System;
using System.Linq;

namespace SuikerBesparing
{
    public class Week
    {
        private DataAction dataAction = new DataAction();
        private Year _year = new Year();
        private Day _day = new Day();

        public void Add(string year, string week)
        {
            var data = dataAction.Get();

            if (!_year.Exists(year))
            {
                _year.Add(year);
            }

            data[0].jaren.First(x => x.jaar == year)
                .weken.Add(new Weken
                {
                    week = week,
                    besparing = null,
                    water = null
                });

            dataAction.Save(data);
        }

        public void Update(string year, string week)
        {
            var dagen = WeekDays(int.Parse(year), int.Parse(week));
            var data = dataAction.Get();
            double besparing = 0;
            double water = 0;

            if (!Exists(year, week))
            {
                Add(year, week);
                data = dataAction.Get();
            }

            foreach (var dag in dagen)
            {

                if (!_day.Exists(year, dag.ToString("MMMM"), dag.Day.ToString()))
                {
                    _day.Add(year, dag.ToString("MMMM"), dag.Day.ToString(), null, null);
                    data = dataAction.Get();
                }

                besparing = besparing + double.Parse(data[0].jaren.First(x => x.jaar == year)
                                                         .maanden.First(x => x.maand == dag.ToString("MMMM"))
                                                         .dagen.First(x => x.dag == dag.Day.ToString()).besparing ?? 0.ToString());

                water = water + double.Parse(data[0].jaren.First(x => x.jaar == year)
                                                 .maanden.First(x => x.maand == dag.ToString("MMMM"))
                                                 .dagen.First(x => x.dag == dag.Day.ToString()).water ?? 0.ToString());
            }

            data = dataAction.Get();

            data[0].jaren.First(x => x.jaar == year)
                .weken.First(x => x.week == week).besparing = besparing.ToString();

            data[0].jaren.First(x => x.jaar == year)
                .weken.First(x => x.week == week).water = water.ToString();

            dataAction.Save(data);

        }

        public bool Exists(string year, string week)
        {
            var data = dataAction.Get();

            if (!_year.Exists(year))
            {
                return false;
            }

            return data[0].jaren.First(x => x.jaar == year)
                .weken.Any(x => x.week == week);
        }

        private static DateTime[] WeekDays(int Year, int WeekNumber)
        {
            DateTime start = new DateTime(Year, 1, 4);
            start = start.AddDays(-((int)start.DayOfWeek));
            start = start.AddDays(7 * (WeekNumber - 1));
            return Enumerable.Range(1, 7).Select(num => start.AddDays(num)).ToArray();
        }
    }
}