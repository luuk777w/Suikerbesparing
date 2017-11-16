using System.Collections.Generic;
using System.Linq;

namespace SuikerBesparing
{
    public class Month
    {
        private DataAction dataAction = new DataAction();
        private Year _year = new Year();
        public void Add(string year, string month)
        {
            var data = dataAction.Get();

            if (!_year.Exists(year))
            {
                _year.Add(year);
            }

            data[0].jaren.First(x => x.jaar == year)
                .maanden.Add(new Maanden()
                {
                    maand = month,
                    besparing = null,
                    water = null,
                    dagen = new List<Dagen>()
                });

            dataAction.Save(data);
        }

        public void Update(string year, string month)
        {
            var data = dataAction.Get();
            double besparing = 0;
            double water = 0;

            var dagen = data[0].jaren.First(x => x.jaar == year)
                .maanden.First(x => x.maand == month).dagen;

            foreach (var dag in dagen)
            {
                besparing = besparing + double.Parse(dag.besparing ?? 0.ToString());
                water = water + double.Parse(dag.water ?? 0.ToString());
            }

            data[0].jaren.First(x => x.jaar == year)
                .maanden.First(x => x.maand == month).besparing = besparing.ToString();

            data[0].jaren.First(x => x.jaar == year)
                .maanden.First(x => x.maand == month).water = water.ToString();

            dataAction.Save(data);
        }

        public bool Exists(string year, string month)
        {
            var data = dataAction.Get();

            if (!_year.Exists(year))
            {
                return false;
            }

            return data[0].jaren.First(x => x.jaar == year)
                .maanden.Any(x => x.maand == month);
        }
    }
}