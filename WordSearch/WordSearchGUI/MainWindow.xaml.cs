using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.DirectoryServices;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Datatypes;

namespace WordSearchGUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private string _DemoWordsInfoText;

        private bool _AreDemoWordsReady;

        private List<string> _SearchResult;

        private Trie m_searchTrie;

        public event PropertyChangedEventHandler? PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();

            _DemoWordsInfoText = string.Empty;
            DemoWordsInfoText = "No Demo Words Available";
            _SearchResult = new List<string>();
            SearchResult = new List<string>();
            _AreDemoWordsReady = false;
            AreDemoWordsReady = false;

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

        public bool AreDemoWordsReady
        {
            get { return _AreDemoWordsReady; }
            set
            {
                if (value != _AreDemoWordsReady)
                {
                    _AreDemoWordsReady = value;
                    OnPropertyChanged("AreDemoWordsReady");
                }
            }
        }

        public List<string> SearchResult
        {
            get { return _SearchResult; }
            set
            {
                if (value != _SearchResult)
                {
                    _SearchResult = value;
                    OnPropertyChanged("SearchResult");
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
            AreDemoWordsReady = true;
        }

        private void SearchWord_KeyUp(object sender, System.Windows.Input.KeyEventArgs e)
        {
            if (sender is not TextBox searchTextBox) return;

            List<string> alphabet = Util.GetAlphabet().Select(c => c.ToString()).ToList();

            foreach (char c in searchTextBox.Text)
            {
                if (!alphabet.Contains(c.ToString().ToUpper()))
                {
                    searchTextBox.Text = searchTextBox.Text.Replace(c.ToString(), "");
                }
            }

            searchTextBox.Text = searchTextBox.Text.ToUpper();
            searchTextBox.SelectionStart = searchTextBox.Text.Length;
            searchTextBox.SelectionLength = 0;

            SearchResult = m_searchTrie.FindWordsWithPrefix(searchTextBox.Text);
        }
    }
}
