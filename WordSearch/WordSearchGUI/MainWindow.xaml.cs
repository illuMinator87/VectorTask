using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Windows;
using Datatypes;

namespace WordSearchGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _DemoWordsInfoText;

        private Trie m_searchTrie;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

            _DemoWordsInfoText = string.Empty;
            DemoWordsInfoText = "No Demo Words Available";
            m_searchTrie = new Trie(); 
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public string DemoWordsInfoText
        {
            get { return _DemoWordsInfoText; }
            set 
            {
                if (value != _DemoWordsInfoText)
                {
                    _DemoWordsInfoText = value;
                    OnPropertyChanged("DemoWordsInfoText");
                }
            }
        }

        private void GenerateDemoWords_Click(object sender, RoutedEventArgs e)
        {
            Stopwatch watch = Stopwatch.StartNew();
            List<string> demoWords = Util.GenerateDemoWords();
            watch.Stop();

            double demoWordsDuration = watch.Elapsed.TotalMilliseconds;

            watch.Restart();
            m_searchTrie = new Trie();
            m_searchTrie.InsertWordsMultithhreaded(demoWords);
            watch.Stop();

            double treeBuildingDuration = watch.Elapsed.TotalMilliseconds;

            DemoWordsInfoText = $"Generated {demoWords.Count} words in {demoWordsDuration} Milliseconds.\nBuilding Search Trie took {treeBuildingDuration} Milliseconds.";
        }
    }
}
