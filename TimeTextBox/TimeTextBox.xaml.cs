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
    * Luokka joka huolehtii ajan vastaanottamisesta
    * Graafisten käyttöliittymien ohjelmoinnin viikkotehtävä 4
    * @author Hannes Laukkanen
    * @version 18.8.2016
    */
    public partial class TimeTextBox : UserControl
    {

        /// <summary>
        /// Kontrukori
        /// </summary>
        public TimeTextBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Säilyttää syötetyn aika-arvon
        /// </summary>
        public TimeSpan TimeValue
        {
            get { return (TimeSpan)GetValue(BoxValue); }
            set { SetValue(BoxValue, value); }
        }

        /// <summary>
        /// Dependency property aika arvon säilyttämiseen
        /// </summary>
        public static readonly DependencyProperty BoxValue =
            DependencyProperty.Register(
                "TimeValue", // avain jolla sisältö haetaan
                typeof(TimeSpan), // propertyn sisällön tyyppi
                typeof(TimeTextBox), // luokka jonka ominaisuus
                new FrameworkPropertyMetadata(new TimeSpan(0,0,0), // oletusarvo
                FrameworkPropertyMetadataOptions.AffectsRender, // vaikuttaa luokan ulkoasuun (textbox päivittyy)
                new PropertyChangedCallback(DoAfterValueChange),  // kutsutaan propertyn arvon muuttumisen jälkeen
                new CoerceValueCallback(DoBeforeValueChange))); // kutsutaan ennen propertyn arvon muutosta; 

        /// <summary>
        /// Tehdään ennen arvon muutosta
        /// </summary>
        /// <param name="d"></param>
        /// <param name="baseValue"></param>
        /// <returns></returns>
        private static object DoBeforeValueChange(DependencyObject d, object baseValue)
        {
            TimeSpan luku = (TimeSpan)baseValue;
            return luku;
        }

        /// <summary>
        /// Suroitetaan timepropertyn arvon muutoksen jälkeen
        /// </summary>
        /// <param name="d"></param>
        /// <param name="e"></param>
        private static void DoAfterValueChange(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            // ei käytetä    
        }
    }

    /// <summary>
    /// Luokka jota kätyetään syötetyn aika-arvon kunnollisuuden tarkistamiseen
    /// </summary>
    public class TimeFormatRule : ValidationRule
    {
        private TimeSpan vMin = new TimeSpan(0);
        private TimeSpan vMax = new TimeSpan(23,59,59);

        /// <summary>
        /// Minimiarvo joka hyväksytään
        /// </summary>
        public TimeSpan Min
        {
            get { return vMin; }
            set { vMin = value; }
        }

        /// <summary>
        /// Maksimiarvo joka hyväksytään
        /// </summary>
        public TimeSpan Max
        {
            get { return vMax; }
            set { vMax = value; }
        }

        /// <summary>
        /// Tarkistaa syötteen oikeellisuuden
        /// </summary>
        /// <param name="value">syötetty arvo</param>
        /// <param name="cultureInfo"></param>
        /// <returns>Tarkistuksen tulos</returns>
        public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
        {
            TimeSpan parsedValue;

            try
            {
                // Typecastaa valuen merkkijonoksi tai jos ei onnistu niin nulliksi (onnistuu tässä aina)
                string valueAsString = value as string;

                // lasketaan onko merkkijonossa pyydetty määrä kaksoispisteitä
                if (2 !=valueAsString.Count(x => ':'.Equals(x))) return new ValidationResult(false,
                    "Syötteen on oltava muodossa tt:mm:ss");

                parsedValue = TimeSpan.Parse(valueAsString);
                                

                if (parsedValue < Min || parsedValue > Max) return new ValidationResult(false,
                    string.Format("Syötteen on oltava väliltä {0} ja {1}", Min, Max ));
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "Syöte ei ole oikeassa muodossa");
            }
            catch (OverflowException)
            {
                return new ValidationResult(false, "Syöte ei ole oikeassa muodossa");
            }

            return ValidationResult.ValidResult;
        }
    }
}