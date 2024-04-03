using System.Diagnostics.Contracts;
using System.Text;
using System.Windows;
using System.Windows.Automation.Provider;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup.Localizer;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        double lastNumber, result;
        SelectedOperator selectedOperator;

        public MainWindow()
        {
            InitializeComponent();

            acButton.Click += AcButton_Click;
            plusorminusButton.Click += PlusorminusButton_Click;
            percentageButton.Click += PercentageButton_Click;
            equalButton.Click += EqualButton_Click;
        }

        private void NumberButton_Click(object sender, RoutedEventArgs e)
        {
            int selectedValue = int.Parse((sender as Button).Content.ToString());

            /*          if (sender == zeroButton)
                        {
                            selectedValue = 0;
                        }
                        if (sender == oneButton)
                        {
                            selectedValue = 1;
                        }
                        if (sender == twoButton)
                        {
                            selectedValue = 2;
                        }
                        if (sender == threeButton)
                        {
                            selectedValue = 3;
                        }
                        if (sender == fourButton)
                        {
                            selectedValue = 4;
                        }
                        if (sender == fiveButton)
                        {
                            selectedValue = 5;
                        }
                        if (sender == sixButton)
                        {
                            selectedValue = 6;
                        }
                        if (sender == sevenButton)
                        {
                            selectedValue = 7;
                        }
                        if (sender == eightButton)
                        {
                            selectedValue = 8;
                        }
                        if (sender == nineButton)
                        {
                            selectedValue = 9;
                        }*/

            /*          var sn = sender.ToString().Split(" ")[1];

                        switch (sn) {
                            case "0":
                                {
                                    selectedValue = 0;
                                    break;
                                }
                        }*/

            if (resultLable.Content.ToString() == "0")
            {
                resultLable.Content = $"{selectedValue}";
            }
            else
            {
                resultLable.Content = $"{resultLable.Content}{selectedValue}";
            }
        }

        private void AcButton_Click(object sender, RoutedEventArgs e)
        {
            resultLable.Content = "0";
            result = 0;
            lastNumber = 0;
        }

        private void PointButton_Click(object sender, RoutedEventArgs e)
        {
            if (!resultLable.Content.ToString().Contains("."))
            {
                resultLable.Content = $"{resultLable.Content}.";
            }
            else
            {
                //Do nothing because . is alredy in resultLable
            }
        }

        private void PercentageButton_Click(object sender, RoutedEventArgs e)
        {
            double tempNumber;
            if (double.TryParse(resultLable.Content.ToString(), out tempNumber))
            {
                tempNumber /= 100;
                if (lastNumber != 0)
                {
                    tempNumber *= lastNumber;
                }
                resultLable.Content = tempNumber.ToString();
            }
        }

        private void PlusorminusButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLable.Content.ToString(), out lastNumber))
            {
                lastNumber *= -1;
                resultLable.Content = lastNumber.ToString();
            }
        }

        private void OperationButton_Click(object sender, RoutedEventArgs e)
        {
            if (double.TryParse(resultLable.Content.ToString(), out lastNumber))
            {
                resultLable.Content = "0";
            }

            if (sender == plusButton)
            {
                selectedOperator = SelectedOperator.Addition;
            }
            else if (sender == minusButton)
            {
                selectedOperator = SelectedOperator.Subtraction;
            }
            else if (sender == multiplyButton)
            {
                selectedOperator = SelectedOperator.Multiplication;
            }
            else if (sender == divideButton)
            {
                selectedOperator = SelectedOperator.Division;
            }
        }

        private void EqualButton_Click(object sender, RoutedEventArgs e)
        {
            double newNumber;

            if (double.TryParse(resultLable.Content.ToString(), out newNumber) )
            {
                switch (selectedOperator)
                {
                    case SelectedOperator.Addition:
                        result = SimpleMath.Add(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Subtraction:
                        result = SimpleMath.Subtract(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Multiplication:
                        result = SimpleMath.Multiply(lastNumber, newNumber);
                        break;
                    case SelectedOperator.Division:
                        result = SimpleMath.Divide(lastNumber, newNumber);
                        break;
                }

                resultLable.Content = result.ToString();
            }
        }
    }

    public enum SelectedOperator
    {
        Addition,
        Subtraction,
        Multiplication,
        Division
    }

    public class SimpleMath
    {
        public static double Add(double n1, double n2)
        {
            return (n1 + n2);
        }
        public static double Subtract(double n1, double n2)
        {
            return (n1 - n2);
        }
        public static double Multiply(double n1, double n2)
        {
            return (n1 * n2);
        }
        public static double Divide(double n1, double n2)
        {
            if (n2 == 0)
            {
                MessageBox.Show("Division by 0 is not supported", "Wrong operation", MessageBoxButton.OK, MessageBoxImage.Error);
                return 0;
            }
                return (n1 / n2);
        }
    }
}