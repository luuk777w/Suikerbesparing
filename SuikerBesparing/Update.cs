using System;
using System.Linq;

namespace SuikerBesparing
{
    public class Update
    {
        private Day _day = new Day();
        private Week _week = new Week();
        private Month _month = new Month();
        private Year _year = new Year();
        private Common _common = new Common();
        private DataAction dataAction = new DataAction();

        public void Total()
        {
            var data = dataAction.Get();
            double besparing = 0;
            double water = 0;

            var jaren = data[0].jaren;

            foreach (var jaar in jaren)
            {
                besparing = besparing + double.Parse(jaar.besparing ?? 0.ToString());
                water = water + double.Parse(jaar.water ?? 0.ToString());
            }

            data[0].besparing = besparing.ToString();
            data[0].water = water.ToString();

            dataAction.Save(data);
        }

        public void Labels(DateTime cdate)
        {
            var mainWindow = ((MainWindow) System.Windows.Application.Current.MainWindow);

            mainWindow.Date.Content = cdate.ToString("dd/MM/yyyy");
            mainWindow.DagLabel.Content = "Dag " + cdate.ToString("dd/MM/yyyy") + ":";
            mainWindow.WeekLabel.Content = "Week " + _common.WeekOfYear(cdate) + ":";
            mainWindow.MaandLabel.Content = "Maand " + cdate.ToString("MMMM") + ":";
            mainWindow.JaarLabel.Content = "Jaar " + cdate.Year + ":";
        }

        public void Values(string year, string month, string week, string day)
        {
            var mainWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);

            var data = dataAction.Get();

            string dagValue;
            string inputValue;
            string weekValue;
            string maandValue;
            string jaarValue;

            if (!_day.Exists(year, month, day) || data[0].jaren.First(x => x.jaar == year)
                    .maanden.First(x => x.maand == month)
                    .dagen.First(x => x.dag == day).water == null)
            {
                dagValue = "Geen data beschikbaar";
                inputValue = "";
            }
            else
            {
                dagValue = Math.Round(double.Parse(data[0].jaren.First(x => x.jaar == year)
                                          .maanden.First(x => x.maand == month)
                                          .dagen.First(x => x.dag == day).water) / 1000, 1)
                           + "l water - " +
                           Math.Round(double.Parse(data[0].jaren.First(x => x.jaar == year)
                               .maanden.First(x => x.maand == month)
                               .dagen.First(x => x.dag == day).besparing), 0) + "g suiker";

                inputValue = data[0].jaren.First(x => x.jaar == year)
                    .maanden.First(x => x.maand == month)
                    .dagen.First(x => x.dag == day).water;
            }

            if (!_week.Exists(year, week) || data[0].jaren.First(x => x.jaar == year)
                    .weken.First(x => x.week == week).water == null)
            {
                weekValue = "Geen data beschikbaar";
            }
            else
            {
                weekValue = Math.Round(double.Parse(data[0].jaren.First(x => x.jaar == year)
                                           .weken.First(x => x.week == week).water) / 1000, 1)
                            + "l water - " +
                            Math.Round(double.Parse(data[0].jaren.First(x => x.jaar == year)
                                .weken.First(x => x.week == week).besparing), 0) + "g suiker";
            }

            if (!_month.Exists(year, month) || data[0].jaren.First(x => x.jaar == year)
                    .maanden.First(x => x.maand == month).water == null)
            {
                maandValue = "Geen data beschikbaar";
            }
            else
            {
                maandValue = Math.Round(double.Parse(data[0].jaren.First(x => x.jaar == year)
                                            .maanden.First(x => x.maand == month).water) / 1000, 1)
                             + "l water - " +
                             Math.Round(double.Parse(data[0].jaren.First(x => x.jaar == year)
                                 .maanden.First(x => x.maand == month).besparing), 0) + "g suiker";
            }

            if (!_year.Exists(year) || data[0].jaren.First(x => x.jaar == year).water == null)
            {
                jaarValue = "Geen data beschikbaar";
            }
            else
            {
                jaarValue = Math.Round(double.Parse(data[0].jaren.First(x => x.jaar == year).water) / 1000, 1)
                            + "l water - " +
                            Math.Round(double.Parse(data[0].jaren.First(x => x.jaar == year).besparing) / 1000, 1) + "kg suiker";
            }

            string totaalValue = Math.Round(double.Parse(data[0].water ?? 0.ToString()) / 1000, 1) + "l water - " +
                                 Math.Round(double.Parse(data[0].besparing ?? 0.ToString()) / 1000, 1) + "kg suiker";

            mainWindow.DagValue.Content = dagValue;
            mainWindow.Input.Text = inputValue;
            mainWindow.WeekValue.Content = weekValue;
            mainWindow.MaandValue.Content = maandValue;
            mainWindow.JaarValue.Content = jaarValue;
            mainWindow.TotaalValue.Content = totaalValue;
        }
    }
}