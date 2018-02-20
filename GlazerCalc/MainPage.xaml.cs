using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Radios;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace GlazerCalc
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        double width, height, woodLength, glassArea;
        private double woodLengthTotal, glassAreaTotal;
        const double MIN_WIDTH = 0.5;
        const double MAX_WIDTH = 5.0;
        const double MIN_HEIGHT = 0.75;
        const double MAX_HEIGHT = 3.0;
        string tintColor;
        int slideQuantity;
        DateTime orderDate = DateTime.Now;

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void WidthValidation(object sender, KeyRoutedEventArgs e)
        {
            string errMsg = "";

            // Width must be above minimum and below maximum
            if (Convert.ToDouble(WidthInput.Text) < MIN_WIDTH || Convert.ToDouble(WidthInput.Text) > MAX_WIDTH)
            {
                errMsg = "The width of the window must be between " + MIN_WIDTH + " and " + MAX_WIDTH;
            }
            // Width cannot be empty
            else if (WidthInput.Text == "")
            {
                errMsg = "Please enter a value - Field cannot be empty";
            }
            else if (Convert.ToDouble(WidthInput.Text) >= MIN_WIDTH || Convert.ToDouble(WidthInput.Text) <= MAX_WIDTH)
            {
                errMsg = "";
            }
            // Width must be a valid number
            else
            {
                errMsg = "Please enter a valid number (see range above)";
            }
            // Error into WidthInput TextBox
            WidthError.Text = errMsg;
        }

        private void HeightValidation(object sender, KeyRoutedEventArgs e)
        {
            string errMsg;

            // Height must be above minimum and below maximum
            if (Convert.ToDouble(HeightInput.Text) < MIN_HEIGHT || Convert.ToDouble(HeightInput.Text) > MAX_HEIGHT)
            {
                errMsg = "The height of the window must be between " + MIN_HEIGHT + " and " + MAX_HEIGHT;
            }
            // Height cannot be empty
            else if (HeightInput.Text == "")
            {
                errMsg = "Please enter a value - Field cannot be empty";
            }
            else if (Convert.ToDouble(HeightInput.Text) >= MIN_HEIGHT || Convert.ToDouble(HeightInput.Text) <= MAX_HEIGHT)
            {
                errMsg = "";
            }
            // Height must be a valid number
            else
            {
                errMsg = "Please enter a valid number (see range above)";
            }

            // Error into HeightInput TextBox
            HeightError.Text = errMsg;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Double.TryParse(WidthInput.Text, out width);
            Double.TryParse(HeightInput.Text, out height);

            // When click event occurs, values must be placed within the fields, else error
            if (width == 0)
            {
                WidthError.Text = "Please enter a value for the width";
            }

            else if (height == 0)
            {
                HeightError.Text = "Please enter a value for the height";
            }
            // Calculation event (button click) must check to make sure values of width and height within set parameters
            else if ((width >= MIN_WIDTH && width <= MAX_WIDTH) &&
                (height >= MIN_HEIGHT && height <= MAX_HEIGHT))
            {
                // RadioTint1.ToString() does not work - you need to extract the CONTENT of the Radio Button
                if (RadioTint1.IsChecked == true)
                {
                    tintColor = RadioTint1.Content.ToString();
                }
                else if (RadioTint2.IsChecked == true)
                {
                    tintColor = RadioTint2.Content.ToString();
                }
                else if (RadioTint3.IsChecked == true)
                {
                    tintColor = RadioTint3.Content.ToString();
                }

                // Remember: Int32 for Integers, as that is what I'm working with
                slideQuantity = Convert.ToInt32(SlideCount.Text);

                woodLength = 2 * (width + height) * 3.25; // meters to feet
                glassArea = 2 * (width * height);

                woodLengthTotal = woodLength * slideQuantity;
                glassAreaTotal = glassArea * slideQuantity;

                WoodTotalOutput.Text = woodLengthTotal.ToString();
                GlassTotalOutput.Text = glassAreaTotal.ToString();
                WindowTintOutput.Text = tintColor;

                WoodUnitOutput.Text = woodLength.ToString();
                GlassUnitOutput.Text = glassArea.ToString();
                // pre-specified current date now in month/day/year string
                OrderDateOutput.Text = orderDate.ToString("MM/dd/yyyy");
            }
        }
    }
}
