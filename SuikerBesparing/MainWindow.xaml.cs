using System;
using System.IO;
using System.Windows;
using System.Windows.Controls;

namespace SuikerBesparing
{

    public partial class MainWindow
    {
        private Day _day = new Day();
        private Week _week = new Week();
        private Month _month = new Month();
        private Year _year = new Year();
        private Update _update = new Update();
        private Common _common = new Common();
        private DateTime SetDate;
        public static string JsonLocation;

        public MainWindow()
        {
            InitializeComponent();
            Init();
        }

        public void Init()
        {
            JsonLocation = File.ReadAllText("JsonLocation.txt");
            DateTime ndate = DateTime.Now.Date;
            _update.Labels(ndate);
            SetDate = ndate;
            _update.Values(SetDate.Year.ToString(), SetDate.ToString("MMMM"), _common.WeekOfYear(SetDate).ToString(), SetDate.Day.ToString());
        }

        private void Submit_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                string input = Input.Text;
                int water;
                bool isNumber = int.TryParse(input, out water);
                double besparing = water / 100.0 * 9.1;
                //Date = SetDate

                if (!isNumber)
                {
                    MessageBox.Show("Alleen cijfers.");
                }

                _day.Add(SetDate.Year.ToString(), SetDate.ToString("MMMM"), SetDate.Day.ToString(), besparing.ToString(), water.ToString());
                _week.Update(SetDate.Year.ToString(), _common.WeekOfYear(SetDate).ToString());
                _month.Update(SetDate.Year.ToString(), SetDate.ToString("MMMM"));
                _year.Update(SetDate.Year.ToString());
                _update.Total();
                _update.Values(SetDate.Year.ToString(), SetDate.ToString("MMMM"), _common.WeekOfYear(SetDate).ToString(), SetDate.Day.ToString());

            }
            catch (Exception exception)
            {
                MessageBox.Show("ERROR:" + exception.Message);
            }
        }

        public void Calendar_OnSelectedDatesChanged(object sender, SelectionChangedEventArgs e)
        {
            var mainWindow = ((MainWindow)System.Windows.Application.Current.MainWindow);

            try
            {
                if (mainWindow.Calendar.SelectedDate == null) return;
                DateTime cdate = (DateTime)mainWindow.Calendar.SelectedDate;
                _update.Labels(cdate);
                _update.Values(cdate.Year.ToString(), cdate.ToString("MMMM"), _common.WeekOfYear(cdate).ToString(), cdate.Day.ToString());
                SetDate = cdate;

            }
            catch (Exception exception)
            {
                MessageBox.Show("ERROR:" + exception.Message);
            }
        }

    }
}
