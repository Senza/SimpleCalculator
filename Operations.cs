using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    public class Operations
    {
        

        CalcOperations op = CalcOperations.NONE;
        double firstNumber;
        double lastNumber;
        bool resetNum;
        bool lastOperation;

        public Operations()
        {
            Events.current.onNumberClick += ChangeNumber;
            Events.current.onCalcOperation += OperationOnNumber;
            Events.current.onGetResult += calculation;
            Events.current.onNumCheck += NumberCheck;
            Events.current.onRequestPercentage += Percentage;
            Events.current.onRequestDecimalPoint += DecimalPoint;
            Events.current.onNegPos += NegOrPos;

            Events.current.onReset += ResetAll;
            Events.current.onResetLastCall += ResetLastCall;
            Events.current.onDel += Del;
        }

        private void NumberCheck()
        {
            if (resetNum == false)
                resetNum = true;
            else if(lastOperation == true)
                resetNum = false;
        }

        private void ChangeNumber(double number)
        {
            if (resetNum == false)
                firstNumber = number;
            else
                lastNumber = number;

           
        }

        private void OperationOnNumber(CalcOperations op)
        {

            if (lastOperation == true)
            {
               
                calculation();
            }

            switch (op)
            {
                case CalcOperations.DIVIDE:
                    this.op = CalcOperations.DIVIDE;
                    break;
                case CalcOperations.MULTIPLICATION:
                    this.op = CalcOperations.MULTIPLICATION;
                    break;
                case CalcOperations.SUBSTRACTION:
                    this.op = CalcOperations.SUBSTRACTION;
                    break;
                case CalcOperations.ADDITION:
                    this.op = CalcOperations.ADDITION;
                    break;
            }

            if(this.op != CalcOperations.NONE)
            {
                Events.current.onDisplayResultUp(OperationResult());
            }
            lastOperation = true;
        }


        private string OperationResult()
        {  
            switch (op)
            {
                case CalcOperations.DIVIDE:
                    return "/";
                case CalcOperations.MULTIPLICATION:
                    return "x";
                case CalcOperations.SUBSTRACTION:
                    return "-";
                case CalcOperations.ADDITION:
                    return "+";
            }

            return "";
        }

        private void calculation()
        {
            lastOperation = false;
            double result = 0;
            switch (op)
            {
                case CalcOperations.DIVIDE:
                    result = firstNumber / lastNumber;
                    break;
                case CalcOperations.MULTIPLICATION:
                    result = firstNumber * lastNumber;
                    break;
                case CalcOperations.SUBSTRACTION:
                    result = firstNumber - lastNumber;
                    break;
                case CalcOperations.ADDITION:
                    result = firstNumber + lastNumber;
                    break;
            }

            firstNumber = result;
            Events.current.DisplayResult(result);
        }

        private void ResetAll()
        {
            op = CalcOperations.NONE;
            firstNumber = 0;
            lastNumber = 0;
            resetNum = false;
            lastOperation = false;
        }
        private void ResetLastCall()
        {
            ChangeNumber(0);
        }
        private void Del(string value)
        {   
            if (value.Length == 0)
                return;

            value = value.Substring(0, value.Length - 1);
            double.TryParse(value, out double result);
            ChangeNumber(result);
            Events.current.DisplayResult(value);
        }
        private void Percentage(string value)
        {
            if (value.Length == 0)
                return;

            double.TryParse(value, out double result);
            result /= 100;
            ChangeNumber(result);
            Events.current.DisplayResult($"{result}");
        }

        private void DecimalPoint(string value)
        {
            if (string.IsNullOrEmpty(value))
            {
                value = $"{0}.";
                
            }
            else if(!value.Contains("."))
            {
                value = $"{value}.";
            }
            double.TryParse(value, out double result);
            ChangeNumber(result);
            Events.current.DisplayResult(value);
        }

        private void NegOrPos(string value)
        {
            if (value.Length == 0)
                return;

            double.TryParse(value, out double result);
            result *= -1;
            ChangeNumber(result);
            Events.current.DisplayResult($"{result}");
        }
    }
}
