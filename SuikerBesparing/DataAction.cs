using System;
using System.Collections.Generic;
using System.IO;
using System.Windows;
using Newtonsoft.Json;

namespace SuikerBesparing
{
    public class DataAction
    {
        
        public List<Data> Get()
        {
            try
            {
                var file = File.ReadAllText(MainWindow.JsonLocation);

                if (file == "")
                {
                    FactoryFormat();
                }

                return JsonConvert.DeserializeObject<List<Data>>(file);
            }
            catch (Exception e)
            {
                MessageBox.Show("Json file locatie onjuist, Json onjuist of bestand is niet leeg.");
                Environment.Exit(0);
                return new List<Data>();
            }

        }

        public void Save(List<Data> data)
        {
            var json = JsonConvert.SerializeObject(data, Formatting.Indented);
            File.WriteAllText(MainWindow.JsonLocation, json);
        }

        private void FactoryFormat()
        {
            List<Data> data = new List<Data>
            {
                new Data()
                {
                    water = null,
                    besparing = null,
                    jaren = new List<Jaren>()
                }
            };

            Save(data);
        }
    }
}