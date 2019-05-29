using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace MediaPlayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DispatcherTimer timer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void videClipPlayHandler(object sender, RoutedEventArgs e)
        {
            videoClip.Play();
            if (timer != null)
            timer.Start();
        }

        private void videClipPauseHandler(object sender, RoutedEventArgs e)
        {
            videoClip.Pause();
            if (timer != null)
                timer.Stop();
        }

        private void videClipStopHandler(object sender, RoutedEventArgs e)
        {
            videoClip.Stop();
            if (timer != null)
                timer.Stop();
        }

        private void videoClip_MediaFailed(object sender, ExceptionRoutedEventArgs e)
        {
            MessageBox.Show(e.ErrorException.Message);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            videoClip.ScrubbingEnabled = true;
            videoClip.Stop();
        }

        private void videoClip_MediaOpened(object sender, RoutedEventArgs e)
        {
            TimerSlider.Maximum = videoClip.NaturalDuration.TimeSpan.TotalSeconds;

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += timer_Tick;

            totalTimeOfVideo.Content = videoClip.NaturalDuration.TimeSpan.ToString(@"hh\:mm\:ss");

            videoClip.Play();
            videoClip.Pause();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            TimerSlider.Value = videoClip.Position.TotalSeconds;
        }

        private void TimerSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            videoClip.Position = TimeSpan.FromSeconds(TimerSlider.Value);
        }

        private void TimerSlider_DragStarted(object sender, System.Windows.Controls.Primitives.DragStartedEventArgs e)
        {
            videoClip.Pause();
            if(timer!=null)
            timer.Stop();
        }

        private void TimerSlider_DragCompleted(object sender, System.Windows.Controls.Primitives.DragCompletedEventArgs e)
        {
            videoClip.Play();
            timer.Start();
        }
    }
}
