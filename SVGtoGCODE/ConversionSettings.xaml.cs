using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SVGtoGCODE
{
    /// <summary>
    /// Interaction logic for ConversionSettings.xaml
    /// </summary>
    public partial class ConversionSettings : Window
    {
        public ConversionSettings()
        {
            InitializeComponent();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SettingBoxSizeX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SettingBoxSizeY_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SettingBoxOffsetX_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SettingBoxOffsetY_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SettingBoxPrintHeight_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SettingBoxMoveHeight_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SettingBoxPrintSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void SettingBoxMoveSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
