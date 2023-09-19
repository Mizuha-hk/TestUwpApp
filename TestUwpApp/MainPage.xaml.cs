using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.ApplicationModel.Appointments;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// 空白ページの項目テンプレートについては、https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x411 を参照してください

namespace TestUwpApp
{
    /// <summary>
    /// それ自体で使用できる空白ページまたはフレーム内に移動できる空白ページ。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            appointment.Subject = "Test";
            appointment.StartTime = DateTime.Now;
            appointment.Duration = TimeSpan.FromMinutes(30);
            appointment.Location = "Tokyo";
            appointment.Reminder = TimeSpan.FromMinutes(15);
            appointment.Details = "Test";

            Loaded += MainPage_Loaded;
        }

        private Appointment appointment = new Appointment();

        private async void MainPage_Loaded(object sender, RoutedEventArgs e)
        {
            var result = await AppointmentManager.ShowAddAppointmentAsync(appointment, new Rect(), Windows.UI.Popups.Placement.Default);
            OutputText.Text += result.ToString() + "\n";
            var Data = AppointmentManager.GetForUser(User.GetDefault());
            var Appoint = await Data.RequestStoreAsync(AppointmentStoreAccessType.AllCalendarsReadOnly);
            var OneDay = await Appoint.FindAppointmentsAsync(DateTime.Now, TimeSpan.FromDays(1));
            foreach (var item in OneDay)
            {
                OutputText.Text += item.RoamingId + "\n";
            }
        }

    }
}
