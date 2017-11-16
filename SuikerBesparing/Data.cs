using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuikerBesparing
{


    public class Data
    {
        public string besparing { get; set; }
        public string water { get; set; }
        public List<Jaren> jaren { get; set; }
    }

    public class Jaren
    {
        public string jaar { get; set; }
        public string besparing { get; set; }
        public string water { get; set; }
        public List<Maanden> maanden { get; set; }
        public List<Weken> weken { get; set; }
    }

    public class Maanden
    {
        public string maand { get; set; }
        public string besparing { get; set; }
        public string water { get; set; }
        public List<Dagen> dagen { get; set; }
    }

    public class Dagen
    {
        public string dag { get; set; }
        public string besparing { get; set; }
        public string water { get; set; }
    }

    public class Weken
    {
        public string week { get; set; }
        public string besparing { get; set; }
        public string water { get; set; }
    }


}
