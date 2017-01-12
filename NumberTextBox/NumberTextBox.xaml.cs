using System;
using System.Windows;
using System.Windows.Controls;

namespace Viikkotehtava4
{

    /**
     * Luokka joka huolehtii matkan pituus -tiedon vastaanottamisesta ja oikeellisuuden tarkistuksesta
     * Graafisten käyttöliittymien ohjelmoinnin viikkotehtävä 4
     * @author Hannes Laukkanen
     * @version 18.8.2016
     */
    public partial class NumberTextBox : UserControl
    {       
        /// <summary>
        /// Luo laatikon Xamlin pohjalta
        /// </summary>
        public NumberTextBox()
        {
            InitializeComponent();
        }
        
        /// <summary>
        /// Säilyttää ja vastaanottaa käyttäjän syötteen
        /// </summary>
        public double DistanceValue
        {
            get { return (double)GetValue(Distance); }
            set { SetValue(Distance, value); }
        }

        /// <summary>
        /// DependencyProperty matkaa varten
        /// </summary>
        public static readonly DependencyProperty Distance =
            DependencyProperty.Register(
                "Distance", // avain jolla sisältö haetaan
                typeof(double), // propertyn sisällön tyyppi
                typeof(NumberTextBox), // luokka jonka ominaisuus
                new FrameworkPropertyMetadata(0.0d, // oletusarvo
                FrameworkPropertyMetadataOptions.AffectsRender, // vaikuttaa luokan ulkoasuun (textbox päivittyy)
                new PropertyChangedCallback(DoAfterValueChange),  // kutsutaan propertyn arvon muuttumisen jälkeen
                new CoerceValueCallback(DoBeforeValueChange))); // kutsutaan ennen propertyn arvon muutosta; 

        /// <summary>
        /// Suoritetaan ennen distancen arvon muuttamista
        /// </summary>
        /// <param name="d"></param>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        private static object DoBeforeValueChange(DependencyObject d, object baseValue)
        {
            NumberTextBox ntb = (NumberTextBox)d;
            double luku = (double)baseValue;
            return luku;
        }

        /// <summary>
        /// Suoritetaan distancen arvon muuttamisen jälkeen
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void DoAfterValueChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
          // Ei käytetä
        }
    }

    /// <summary>
    /// Luokka jota käytetään käyttäjän syöttämän arvon validointiin
    /// </summary>
    public class DistanceFormRule : ValidationRule
    {
        private double vMin = 0;
        private double vMax = 100;

        /// <summary>
        /// Distance arvon minimi
        /// </summary>
        public double Min
        {
            get { return vMin; }
            set { vMin = value; }
        }

        /// <summary>
        /// distance arvon maksimi
        /// </summary>
        public double Max
        {
            get { return vMax; }
            set { vMax = value; }
        }

        /// <summary>
        /// Tarkistaa että syötetty arvo on hyväksyttävässä muodossa
        /// </summary>
        /// <param name="value"></param>
        /// <param name="cultureInfo"></param>
        /// <returns></returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            double parsedValue = 0;

            try
            {
                parsedValue = double.Parse((string)value);

                if (parsedValue > Max || parsedValue < Min) return new ValidationResult(false, string.Format("Syötteen oltava väliltä {0} ja {1}", Min, Max));
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "Syöte ei ole oikeassa muodossa");
            }
            
            return ValidationResult.ValidResult;            
        }
    }
}
