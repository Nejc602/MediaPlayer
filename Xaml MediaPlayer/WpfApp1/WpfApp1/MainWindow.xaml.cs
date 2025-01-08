using Microsoft.Win32;
using System.Text;
using System.IO;
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

namespace WpfApp1
{
    public partial class MainWindow : Window
    {
        public string songPath = @"C:\Users\Uporabnik\Documents\GitHub\MediaPlayer\Xaml MediaPlayer\WpfApp1\Songs";
        public string imagePath = @"C:\Users\Uporabnik\Documents\GitHub\MediaPlayer\Xaml MediaPlayer\WpfApp1\Images";
        private List<string> playlistPaths = new List<string>();

        public DispatcherTimer timer;

        public MainWindow()
        {
            InitializeComponent();

            timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += updateProgress;
        }

        public void importMenu(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Audio/Video Files|*.mp3;*.wav;*.wma;*.aac;*.ogg;*.flac;*.mp4;*.avi;*.mov;*.mkv|All Files|*.*"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                string filePath = openFileDialog.FileName;
                string fileExtension = System.IO.Path.GetExtension(filePath).ToLower();

                bool isVideo = fileExtension == ".mp4" || fileExtension == ".avi" || fileExtension == ".mov" || fileExtension == ".mkv";
                if (isVideo)
                {
                    MediaPlayer.Play();
                    NowPlaying.Text = "Currently Playing Video: " + System.IO.Path.GetFileNameWithoutExtension(filePath);

                    CurrentSongArt.Visibility = Visibility.Collapsed;
                }

                MediaPlayer.Source = new Uri(filePath);

                NowPlaying.Text = "Currently Playing: " + System.IO.Path.GetFileNameWithoutExtension(filePath);

                MediaPlayer.Play();
                timer.Start();
            }
        }

        private void updateProgress(object sender, EventArgs e)
        {
            if (MediaPlayer.NaturalDuration.HasTimeSpan)
            {
                ProgressSlider.Maximum = MediaPlayer.NaturalDuration.TimeSpan.TotalSeconds;
                ProgressSlider.Value = MediaPlayer.Position.TotalSeconds;

                TimeLabel.Content = MediaPlayer.Position.ToString(@"mm\:ss");
            }
        }
        
        public void exportMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Export menu clicked");
        }
        public void exitMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Exit menu clicked");
        }
        public void addMenu(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Audio/Video Files|*.mp3;*.wav;*.wma;*.aac;*.ogg;*.flac;*.mp4;*.avi;*.mov;*.mkv|All Files|*.*",
                Multiselect = true
            };

            if (openFileDialog.ShowDialog() == true)
            {
                foreach (string filePath in openFileDialog.FileNames)
                {
                    playlistPaths.Add(filePath);
                    Playlist.Items.Add(System.IO.Path.GetFileName(filePath));
                }
            }
        }
        public void deleteMenu(object sender, RoutedEventArgs e)
        {
            if (Playlist.SelectedIndex >= 0)
            {
                playlistPaths.RemoveAt(Playlist.SelectedIndex);
                Playlist.Items.RemoveAt(Playlist.SelectedIndex);
            }
        }
        public void editMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Edit menu clicked");
        }
        public void settingsMenu(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Settings menu clicked");
        }

        private void StopClick(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
            timer.Stop();
            ProgressSlider.Value = 0;
            TimeLabel.Content = "00:00";
        }

        private void PlayClick(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Play();
            timer.Start();
        }

        private void PauseClick(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Pause();
            timer.Stop();
        }

        private void PreviousClick(object sender, RoutedEventArgs e)
        {
 
        }

        private void NextClick(object sender, RoutedEventArgs e)
        {


        }

        private void RepeatClick(object sender, RoutedEventArgs e)
        {
            MediaPlayer.Stop();
            ProgressSlider.Value = 0;
            TimeLabel.Content = "00.00";
            MediaPlayer.Play();
        }

        private void ShuffleClick(object sender, RoutedEventArgs e)
        {

        }


        private void ClearPlaylist_Click(object sender, RoutedEventArgs e)
        {
            playlistPaths.Clear();
            Playlist.Items.Clear();
            NowPlaying.Text = "Currently Playing: ";
            CurrentSongArt.Source = null;
            CurrentSongArt.Visibility = Visibility.Collapsed;
        }

        private void Playlist_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Playlist.SelectedIndex >= 0)
            {
                string selectedFile = playlistPaths[Playlist.SelectedIndex];
                MediaPlayer.Source = new Uri(selectedFile);

                NowPlaying.Text = "Currently Playing: " + System.IO.Path.GetFileNameWithoutExtension(selectedFile);

                string imageFile = System.IO.Path.Combine(imagePath, System.IO.Path.GetFileNameWithoutExtension(selectedFile) + ".jpg");
                if (File.Exists(imageFile))
                {
                    CurrentSongArt.Source = new BitmapImage(new Uri(imageFile));
                    CurrentSongArt.Visibility = Visibility.Visible;
                }
                else
                {
                    CurrentSongArt.Visibility = Visibility.Collapsed;
                }

                MediaPlayer.Play();
                timer.Start();
            }
        }
    }
}
