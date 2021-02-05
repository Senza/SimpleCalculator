using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleCalculator
{
    public class Events
    {
        public static Events current;

        public Operations op;
        
        public Events()
        {
            current = this;
            op = new Operations();
        }

        public Action<double> onNumberClick;
        public void NumberClick(double numValue) => onNumberClick(numValue);

        public Action<CalcOperations> onCalcOperation;
        public void OperationClick(CalcOperations op) => onCalcOperation(op);

        public Action<double> onDisplayResult;
        public void DisplayResult(double result) => onDisplayResult(result);

        public Action<string> onDisplayResultString;
        public void DisplayResult(string result) => onDisplayResultString(result);

        public Action<string> onDisplayResultUp;
        public void DisplayResultUp(string result) => onDisplayResultUp(result);


        public Action onNumCheck;
        public void NumberChecker() => onNumCheck();

        public Action onGetResult;
        public void GetResult() => onGetResult();

        public Action onReset;
        public void Reset() => onReset();

        public Action onResetLastCall;
        public void ResetLastCall() => onResetLastCall();

        public Action<string> onDel;
        public void Del(string value) => onDel(value);

        public Action<string> onRequestPercentage;
        public void Percentage(string value) => onRequestPercentage(value);

        public Action<string> onRequestDecimalPoint;
        public void DecimalPoint(string value) => onRequestDecimalPoint(value);

        public Action<string> onNegPos;
        public void NegPos(string value) => onNegPos(value);
    }
}
