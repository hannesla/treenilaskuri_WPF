using System.Windows;
using System.Windows.Input;


namespace Viikkotehtava4
{
    /**
     * Viikkotehtävän pääohjelma
     * Graafisten käyttöliittymien ohjelmoinnin viikkotehtävä 4
     * @author Hannes Laukkanen
     * @version 18.8.2016
     */
    public partial class MainWindow : Window
    {
        private Exercise lastExercise;

        public MainWindow()
        {
            InitializeComponent();
            lastExercise = new Exercise();
            ExerciseStack.Children.Add(lastExercise);
        }

        /// <summary>
        /// Luodaan uusi Exercise new painikkeesta.
        /// </summary>
        /// <param name="target"></param>
        /// <param name="e"></param>
        private void NewCmdExecuted(object target, ExecutedRoutedEventArgs e)
        {
            lastExercise = new Exercise();
            ExerciseStack.Children.Add(lastExercise);
        }

        /// <summary>
        /// Suljetaan ohjelma
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CloseButton_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Annetaan lupa uuden harjoituksen luomiseen, kun edellinen ilmoittaa olevansa valmis
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void NewCmdCanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (lastExercise == null) return; 

            e.CanExecute = lastExercise.ExercuseReady;
        }
    }

}
