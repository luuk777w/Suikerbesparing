using System.Collections.Generic;
using System.Linq;

namespace SuikerBesparing
{
    public class Year
    {
        private DataAction dataAction = new DataAction();

        public void Add(string year)
        {
            var data = dataAction.Get();

            data[0].jaren.Add(new Jaren
            {
                jaar = year,
                besparing = null,
                water = null,
                maanden = new List<Maanden>(),
                weken = new List<Weken>()

            });

            dataAction.Save(data);
        }

        public void Update(string year)
        {
            var data = dataAction.Get();
            double besparing = 0;
            double water = 0;

            var maanden = data[0].jaren.First(x => x.jaar == year).maanden;

            foreach (var maand in maanden)
            {
                besparing = besparing + double.Parse(maand.besparing ?? 0.ToString());
                water = water + double.Parse(maand.water ?? 0.ToString());
            }

            data[0].jaren.First(x => x.jaar == year).besparing = besparing.ToString();
            data[0].jaren.First(x => x.jaar == year).water = water.ToString();

            dataAction.Save(data);
        }

        public bool Exists(string year)
        {
            var data = dataAction.Get();

            return data[0].jaren.Any(x => x.jaar == year);
        }
    }
}