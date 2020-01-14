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
            RetrieveSettings();
        }

        private void RetrieveSettings()
        {
            SettingBoxSizeX.Text = Properties.Settings.Default.SizeX.ToString();
            SettingBoxSizeY.Text = Properties.Settings.Default.SizeY.ToString();
            SettingBoxOffsetX.Text = Properties.Settings.Default.OffsetX.ToString();
            SettingBoxOffsetY.Text = Properties.Settings.Default.OffsetY.ToString();
            SettingBoxPrintHeight.Text = Properties.Settings.Default.PrintHeight.ToString();
            SettingBoxMoveHeight.Text = Properties.Settings.Default.MoveHeight.ToString();
            SettingBoxPrintSpeed.Text = Properties.Settings.Default.PrintSpeed.ToString();
            SettingBoxMoveSpeed.Text = Properties.Settings.Default.MoveSpeed.ToString();
        }

        private void DefaultSettings()
        {
            Properties.Settings.Default.SizeX = 148;
            Properties.Settings.Default.SizeY = 105;
            Properties.Settings.Default.OffsetX = -74;
            Properties.Settings.Default.OffsetY = 70;
            Properties.Settings.Default.PrintHeight = -50;
            Properties.Settings.Default.MoveHeight = -30;
            Properties.Settings.Default.PrintSpeed = 15;
            Properties.Settings.Default.MoveSpeed = 50;
            RetrieveSettings();
        }

        private void NumberValidationTextBox(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9-]+");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void SettingBoxSizeX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SettingBoxSizeX.Text.Length > 0)
            {
                Properties.Settings.Default.SizeX = int.Parse(SettingBoxSizeX.Text);
            }
        }

        private void SettingBoxSizeY_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SettingBoxSizeY.Text.Length > 0)
            {
                Properties.Settings.Default.SizeY = int.Parse(SettingBoxSizeY.Text);
            }
        }

        private void SettingBoxOffsetX_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SettingBoxOffsetX.Text.Length > 0)
            {
                Properties.Settings.Default.OffsetX = int.Parse(SettingBoxOffsetX.Text);
            }
        }

        private void SettingBoxOffsetY_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SettingBoxOffsetY.Text.Length > 0)
            {
                Properties.Settings.Default.OffsetY = int.Parse(SettingBoxOffsetY.Text);
            }
        }

        private void SettingBoxPrintHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SettingBoxPrintHeight.Text.Length > 0)
            {
                Properties.Settings.Default.PrintHeight = int.Parse(SettingBoxPrintHeight.Text);
            }
        }

        private void SettingBoxMoveHeight_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SettingBoxMoveHeight.Text.Length > 0)
            {
                Properties.Settings.Default.MoveHeight = int.Parse(SettingBoxMoveHeight.Text);
            }
        }

        private void SettingBoxPrintSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SettingBoxPrintSpeed.Text.Length > 0)
            {
                Properties.Settings.Default.PrintSpeed = int.Parse(SettingBoxPrintSpeed.Text);
            }
        }

        private void SettingBoxMoveSpeed_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (SettingBoxMoveSpeed.Text.Length > 0)
            {
                Properties.Settings.Default.MoveSpeed = int.Parse(SettingBoxMoveSpeed.Text);
            }
        }

        private void ResetButton_Click(object sender, RoutedEventArgs e)
        {
            DefaultSettings();
        }
    }
}
