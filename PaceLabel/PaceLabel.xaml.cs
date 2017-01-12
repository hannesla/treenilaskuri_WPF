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
     * Luokka joka huolehtii nopeuden laskemisesta ja näyttämisestä
     * Graafisten käyttöliittymien ohjelmoinnin viikkotehtävä 4
     * @author Hannes Laukkanen
     * @version 18.8.2016
     */
    public partial class PaceLabel : UserControl
    {
        public event Fast fast;
        public event Average average;
        public event Slow slow;

        public event Ready ready;

        private TimeSpan timeValue;
        private double distanceValue;

        private double fastMinValue = 4;
        private double slowMinValue = 6;

        /// <summary>
        /// Säilöö syötetyn ajan
        /// </summary>
        public TimeSpan time
        {
            get { return timeValue; }
            set { timeValue = value; }
        }

        /// <summary>
        /// säilöö syötetyn matkan
        /// </summary>
        public double distance
        {
            get { return distanceValue; }
            set { distanceValue = value; }
        }

        /// <summary>
        /// Minimi nopeus jota pidetään nopeana
        /// </summary>
        public double FastMin
        {
            get { return fastMinValue; }
            set { fastMinValue = value; }
        }

        /// <summary>
        /// Minimi joka määrittelee mini nopeus jota pidetään Keskinkertaisena
        /// </summary>
        public double SlowMin
        {
            get { return slowMinValue; }
            set { slowMinValue = value; }
        }

        /// <summary>
        /// Laskee ja näyttää min/km distancen ja timen perusteella
        /// </summary>
        /// <returns>min/h</returns>
        public double ComputeKmMin()
        {
            double minKm;
            if (distance != 0 && time != new TimeSpan(0)) minKm = time.TotalMinutes / distance;
            else return 0;

            if (FastMin > minKm && fast != null) fast(this, new SpeedEventArgs(Brushes.Red));
            else if (SlowMin <= minKm && slow != null) slow(this, new SpeedEventArgs(Brushes.Green));
            else if (average != null) average(this, new SpeedEventArgs(Brushes.Yellow));

            Content = String.Format("{0:0.0} min/km", minKm);

            if (ready != null) ready(this, new EventArgs());

            return minKm;
        }        
    }

    /// <summary>
    /// Luokka jota käytetään nopeustapahtuman tietojen välittämiseen
    /// </summary>
    public class SpeedEventArgs : EventArgs
    {
        private Brush vSpeedColor;

        /// <summary>
        /// säilyttää värin joka vastaa laskettua nopeutta
        /// </summary>
        public Brush speedColor
        {
            get { return vSpeedColor; }
            set { vSpeedColor = value; }
        }

        /// <summary>
        /// Konstruktori joka säilöö värin tapahtuman propertyyn
        /// </summary>
        /// <param name="speedColor">Väri joka tapahtumalle annetaan</param>
        public SpeedEventArgs(Brush speedColor)
        {
            this.speedColor = speedColor;
        }
    }

    public delegate void Fast(object sender, SpeedEventArgs e);
    public delegate void Average(object sender, SpeedEventArgs e);
    public delegate void Slow(object sender, SpeedEventArgs e);
    public delegate void Ready(object sender, EventArgs e);
}