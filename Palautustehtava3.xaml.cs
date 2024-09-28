using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Input;

namespace Palautustehtava_3
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        // Word that player has to guess.
        private readonly string wordToGuess = "H I R S I P U U";

        private string _word;
        public string Word { get { return _word; } set { _word = value; OnPropertyChanged(); } }

        // Collection of booleans that are bound to geometry elements in xaml.
        private ObservableCollection<bool> _visibilityBools;
        public ObservableCollection<bool> VisibilityBools { get { return _visibilityBools; } set { _visibilityBools = value; OnPropertyChanged(); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainWindow()
        {
            InitializeComponent();
            SetVisibilityBools();
            this.Word = "* * * * * * * *";
        }

        // Raises the PropertyChanged event to notify the binding system that a property value has changed.
        private void OnPropertyChanged([CallerMemberName] string name = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }

        // Creates and populates the collection.
        private void SetVisibilityBools()
        {
            this.VisibilityBools = new() { false, false, false, false, false, false, false, false, false, false, false, };
        }

        // Method for keypress event.
        private void KeyPressed(object sender, KeyEventArgs e)
        {
            if ((int)e.Key >= 44 && (int)e.Key <= 69)
            {
                CheckGuess(e.Key.ToString());
            }
        }

        // Check if the word that player has to guess contains the letter and if it does,
        // update the Word string to reveal the letter's position(s) and check if the player has won the game.
        // If the letter is not found in the word,
        // reveal the next part of the hangman image and check if the player has lost the game.
        private void CheckGuess(string letter)
        {
            if (this.wordToGuess.Contains(letter.ToUpper()))
            {
                var charArray = this.Word.ToCharArray();

                for (int i = 0; i < wordToGuess.Length; i++)
                {
                    if (wordToGuess[i].ToString() == letter.ToUpper())
                    {
                        charArray[i] = char.Parse(letter.ToUpper());
                    }
                }

                this.Word = new string(charArray);

                CheckWin();
            }
            else
            {
                this.VisibilityBools[this.VisibilityBools.IndexOf(false)] = true;

                CheckLose();
            }
        }

        // Checks if the player won the game
        private void CheckWin()
        {
            if (!this.Word.Contains('*'))
            {
                MessageBox.Show("Voitit pelin :)");
                App.Current.Shutdown();
            }
        }

        // Checks if the player lost the game
        private void CheckLose()
        {
            if (this.VisibilityBools.Last() == true)
            {
                MessageBox.Show("Hävisit Pelin :(");
                App.Current.Shutdown();
            }
        }
    }
}