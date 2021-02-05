/// <summary>
/// A simple calculator using events 
/// </summary>

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

namespace SimpleCalculator
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        int operationButtonCount = 0;
        bool cantDel;

        public MainWindow()
        {
            Events events = new Events();
            Listeners();
            InitializeComponent();
        }

        private void Listeners()
        {
            Events.current.onDisplayResult += DisplayResult;
            Events.current.onDisplayResultString += DisplayResult;
            Events.current.onDisplayResultUp += DisplayResultUp;
        }
       


        #region numerical buttons
        private void number0_Click(object sender, RoutedEventArgs e) => DisplayView2(0);
        private void number1_Click(object sender, RoutedEventArgs e) => DisplayView2(1);
        private void number2_Click(object sender, RoutedEventArgs e) => DisplayView2(2);
        private void number3_Click(object sender, RoutedEventArgs e) => DisplayView2(3);
        private void number4_Click(object sender, RoutedEventArgs e) => DisplayView2(4);
        private void number5_Click(object sender, RoutedEventArgs e) => DisplayView2(5);
        private void number6_Click(object sender, RoutedEventArgs e) => DisplayView2(6);
        private void number7_Click(object sender, RoutedEventArgs e) => DisplayView2(7);
        private void number8_Click(object sender, RoutedEventArgs e) => DisplayView2(8);
        private void number9_Click(object sender, RoutedEventArgs e) => DisplayView2(9);

        #endregion

        private void numberdel_Click(object sender, RoutedEventArgs e)
        {
            if (cantDel)
                return;
            Events.current.Del(calDisplay2.Text);
        }

        private void btn_State_Click(object sender, RoutedEventArgs e) => Events.current.onNegPos(calDisplay2.Text);
        private void btn_percent_Click(object sender, RoutedEventArgs e) => Events.current.Percentage(calDisplay2.Text);
        private void btn_dot_Click(object sender, RoutedEventArgs e) => Events.current.DecimalPoint(calDisplay2.Text);
        private void btnAC_Click(object sender, RoutedEventArgs e) => Events.current.ResetLastCall();
      

        private void numberdivision_Click(object sender, RoutedEventArgs e) => Events.current.OperationClick(CalcOperations.DIVIDE);
        private void numbermultiplication_Click(object sender, RoutedEventArgs e) => Events.current.OperationClick(CalcOperations.MULTIPLICATION);
        private void numberminus_Click(object sender, RoutedEventArgs e) => Events.current.OperationClick(CalcOperations.SUBSTRACTION);
        private void numberAddition_Click(object sender, RoutedEventArgs e) => Events.current.OperationClick(CalcOperations.ADDITION);
        private void numberequals_Click(object sender, RoutedEventArgs e) 
        {
            cantDel = true;
            Events.current.GetResult();
        }

        private void btnCe_Click(object sender, RoutedEventArgs e)
        {
            calDisplay1.Text = "";
            calDisplay2.Text = "";
            Events.current.Reset();

        }
        private void DisplayResult(double result)
        {
            calDisplay1.Text = $"{calDisplay1.Text}{calDisplay2.Text}=";
            calDisplay2.Text = $"{result}";
        }
        private void DisplayResult(string result)
        {
            calDisplay2.Text = $"{result}";
           
        }
        private void DisplayResultUp(string value)
        {
            Events.current.NumberChecker();
            cantDel = false;
            if (operationButtonCount == 0)
            {
                calDisplay1.Text = $"{calDisplay1.Text} {calDisplay2.Text} {value}";
                calDisplay2.Text = "";
                operationButtonCount = 1;
               
                return;
            }

            string c = calDisplay1.Text.Remove(calDisplay1.Text.Count() -1);
            calDisplay1.Text = $"{c} {value}";

           


        }
        private void DisplayView2(double number)
        {
            calDisplay2.Text = $"{calDisplay2.Text}{number}";
            double.TryParse(calDisplay2.Text, out double result);
            Events.current.NumberClick(result);
            operationButtonCount = 0;
        }

    }
}
