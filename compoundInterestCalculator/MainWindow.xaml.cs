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

namespace compoundInterestCalculator
{
    /// <summary>
    /// Interaction logic for CompoundInterestCalulatorMain.xaml
    /// </summary>
    public partial class CompoundInterestCalulatorMain : Window
    {
        public CompoundInterestCalulatorMain()
        {
            InitializeComponent();
        }

        private void Button_Calculate(object sender, RoutedEventArgs e)
        {
            //colors
            var lightGreen = new SolidColorBrush(Color.FromRgb(152, 251, 152));

            //Components
            List<string> components = new List<string>();
            components.Add(Principal.Text);
            components.Add(InterestRateResult.Text);
            components.Add(YearsOfGrouth.Text);
            components.Add(Payment.Text);
            

            //Converted properties
            double principalResults;
            double interestRateResult; // devided by 100
            
            double yearsOfGrouthResult;
            double compoundFrequencyResult;
            double paymentResult;
            double PaymentFrequencyResult;

            ComboBoxItem compoundFrequencySelected = (ComboBoxItem)CompoundFrequency.SelectedItem;
            ComboBoxItem PaymentFrequencySelected = (ComboBoxItem)PaymentFrequency.SelectedItem;

            //parsing to doubles of properties
            double.TryParse(Principal.Text, out principalResults);
            double.TryParse(InterestRateResult.Text, out interestRateResult);
            double.TryParse(YearsOfGrouth.Text, out yearsOfGrouthResult);
            double.TryParse(compoundFrequencySelected.Tag.ToString(), out compoundFrequencyResult);
            double.TryParse(Payment.Text, out paymentResult);
            double.TryParse(PaymentFrequencySelected.Tag.ToString(), out PaymentFrequencyResult);


            //calculations
            double interestRateResultPercent = interestRateResult / 100;
            double principalResultsCombined = ((paymentResult * PaymentFrequencyResult)* yearsOfGrouthResult) + principalResults;

            double rate = Math.Pow(1 + (interestRateResultPercent / compoundFrequencyResult), compoundFrequencyResult / PaymentFrequencyResult) - 1;

            double nper = PaymentFrequencyResult * yearsOfGrouthResult;
            double accruedAmount = CompoundInterestWithPayment(principalResults, rate, nper, paymentResult);
            double paymentAmout = paymentResult * nper;
            //MessageBox.Show(principalAmout.ToString("0,000.00"));


            //total interest
            FutureInterestLabel.Background = lightGreen;
            FutureInterestLabel2.Background = lightGreen;
            FutureInterest.Background = lightGreen;
            FutureInterest.Text = (accruedAmount - (principalResults + paymentAmout)).ToString("0,000.00");

            //Total Payments
            TotalPaymentsLabel.Background = lightGreen;
            TotalPaymentsLabel2.Background = lightGreen;
            TotalPayments.Background = lightGreen;
            TotalPayments.Text = paymentAmout.ToString("0,000.00");

            //Payments + Principal
            PaymentInterestLabel.Background = lightGreen;
            PaymentInterestLabel2.Background = lightGreen;
            PaymentInterest.Background = lightGreen;
            PaymentInterest.Text = (principalResults + paymentAmout).ToString("0,000.00");
            


            //Future total
            FutureValueLabel.Background = Brushes.LightGreen;
            FutureValueLabel.Background = Brushes.LightGreen;
            FutureValueLabel2.Background = Brushes.LightGreen;
            FutureValue.Background = Brushes.LightGreen;
            FutureValue.Text = accruedAmount.ToString("0,000.00");

            CheckForLetters(components);


        }

        static double CompoundInterestWithPayment(double principal, double rate, double nper, double paymnet)
        {
            double calc1 = Math.Pow(1 + rate, nper);
            double calc2 = calc1 - 1;
            double calc3 = calc2 / rate;
            double calc4 = principal * calc1 + paymnet * calc3;

            return calc4;
            
        }

        static void CheckForLetters(List<string> list)
        {
            List<string> collection = new List<string>();
         

            foreach (string item in list)
            {
                double number;
                bool res = double.TryParse(item, out number);
                if (!res)
                {
                    collection.Add(item + " is not a number.");
                }
            }

            if (collection.Any())
            {
                collection.Add("This fields only support numbers.");
                MessageBox.Show(string.Join(Environment.NewLine, collection));
            }
            
            
        }

    }

}
