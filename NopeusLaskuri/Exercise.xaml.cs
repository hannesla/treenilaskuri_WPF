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

namespace Viikkotehtava4
{
    /**
     * Kontrolli joka hallinnoi yhden harjoituksen toimintaa ja näyttämistä 
     * Graafisten käyttöliittymien ohjelmoinnin viikkotehtävä 4
     * @author Hannes Laukkanen
     * @version 18.8.2016
     */
    public partial class Exercise : UserControl
    {
        private bool _exerciseReady;
        public bool ExercuseReady { get { return _exerciseReady; } }

        /// <summary>
        /// Alustaa harjoituksen näkymän. Kuuntelee paceLabelia, jonka tapahtumat määrittelevät Exercisen taustavärin. 
        /// </summary>
        public Exercise()
        {
            InitializeComponent();

            paceLabel.ready += PaceLabelReady; 

            paceLabel.fast += SelectColor;
            paceLabel.average += SelectColor;
            paceLabel.slow += SelectColor;
        }

        /// <summary>
        /// Kun PaceLabel kertoo olevansa valmis, päivitetään tämä tieto Exercise attribuuttiin. New-command perustaa
        /// CanExecutensa tuon attribuutin arvoon.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void PaceLabelReady(object sender, EventArgs e)
        {
            _exerciseReady = true;
        }

        /// <summary>
        /// Suoritetaan kun PaceLabelissa on laskettu nopeus. Vaihdetaan Exercisen taustaväri eventissä saadun 
        /// arvon mukaan.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e">Tämä välittää tiedon siitä, mikä väri vastaa lakettua nopeutta.</param>
        private void SelectColor(object sender, SpeedEventArgs e)
        {
            Background = e.speedColor;
        }

        /// <summary>
        /// Painiketta klikatessa pyydetään pacelabelia laskemaan nopeuden ajan ja matkan perusteella, ja näyttämään sen
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void buttonCompute_Click(object sender, RoutedEventArgs e)
        {
            paceLabel.distance = kmNumberTextBox.DistanceValue;
            paceLabel.time = timeTextBox.TimeValue;
            paceLabel.ComputeKmMin();
        }
    }
}
