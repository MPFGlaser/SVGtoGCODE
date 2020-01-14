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
            SettingBoxSizeX.Text = Properties.Settings.Default.SizeX.ToString();
            SettingBoxSizeY.Text = Properties.Settings.Default.SizeY.ToString();
            SettingBoxOffsetX.Text = Properties.Settings.Default.OffsetX.ToString();
            SettingBoxOffsetY.Text = Properties.Settings.Default.OffsetY.ToString();
            SettingBoxPrintHeight.Text = Properties.Settings.Default.PrintHeight.ToString();
            SettingBoxMoveHeight.Text = Properties.Settings.Default.MoveHeight.ToString();
            SettingBoxPrintSpeed.Text = Properties.Settings.Default.PrintSpeed.ToString();
            SettingBoxMoveSpeed.Text = Properties.Settings.Default.MoveSpeed.ToString();
        }
        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SettingBoxSizeX_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.SizeX = int.Parse(SettingBoxSizeX.Text);
        }

        private void SettingBoxSizeY_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.SizeY = int.Parse(SettingBoxSizeY.Text);
        }

        private void SettingBoxOffsetX_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.OffsetX = int.Parse(SettingBoxOffsetX.Text);
        }

        private void SettingBoxOffsetY_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.OffsetY = int.Parse(SettingBoxOffsetY.Text);
        }

        private void SettingBoxPrintHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.PrintHeight = int.Parse(SettingBoxPrintHeight.Text);
        }

        private void SettingBoxMoveHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.MoveHeight = int.Parse(SettingBoxMoveHeight.Text);
        }

        private void SettingBoxPrintSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.PrintSpeed = int.Parse(SettingBoxPrintSpeed.Text);
        }

        private void SettingBoxMoveSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            Properties.Settings.Default.MoveSpeed = int.Parse(SettingBoxMoveSpeed.Text);
        }
    }
}
