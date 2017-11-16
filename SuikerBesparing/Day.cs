using System.Linq;

namespace SuikerBesparing
{
    public class Day
    {
        private DataAction dataAction = new DataAction();
        private Year _year = new Year();
        private Month _month = new Month();

        public void Add(string year, string month, string day, string besparing, string water)
        {
            var data = dataAction.Get();

            if (!_year.Exists(year))
            {
                _year.Add(year);
            }

            if (!_month.Exists(year, month))
            {
                _month.Add(year, month);
            }

            if (Exists(year, month, day))
            {
                Update(year, month, day, besparing, water);
                return;
            }

            data = dataAction.Get();

            data[0].jaren.First(x => x.jaar == year)
                .maanden.First(x => x.maand == month)
                .dagen.Add(new Dagen
                {
                    dag = day,
                    besparing = besparing,
                    water = water
                });

            dataAction.Save(data);
        }

        public void Update(string year, string month, string day, string besparing, string water)
        {
            var data = dataAction.Get();

            data[0].jaren.First(x => x.jaar == year)
                .maanden.First(x => x.maand == month)
                .dagen.First(x => x.dag == day).besparing = besparing;

            data[0].jaren.First(x => x.jaar == year)
                .maanden.First(x => x.maand == month)
                .dagen.First(x => x.dag == day).water = water;

            dataAction.Save(data);
        }

        public bool Exists(string year, string month, string day)
        {
            var data = dataAction.Get();

            if (!_year.Exists(year) || !_month.Exists(year, month))
            {
                return false;
            }

            return data[0].jaren.First(x => x.jaar == year)
                .maanden.First(x => x.maand == month)
                .dagen.Any(x => x.dag == day);
        }
    }
}