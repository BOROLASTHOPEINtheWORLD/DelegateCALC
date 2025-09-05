using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// Содержит логику вычислений и состояние калькулятора
namespace DelegateCALC.Models
{
    public class CalculatorModel : ObservableObject
    {
        //поля для хранения операций
        private string _currentInput = "0";
        private string _previousInput = "";
        private string _currentOperation = "";
        private bool _isNewInput = true;

        public delegate double MathOperation(double a, double b);

        public Dictionary<string, MathOperation> Operations { get; private set; }

        //конструктор
        public CalculatorModel()
        {
            InitializeOperations();
        }
        //инициализация операций
        private void InitializeOperations()
        {
            Operations = new Dictionary<string, MathOperation>() 
            {
                {"+", (a,b) => a+b },
                {"-", (a,b) => a-b },
                {"*", (a,b) => a*b },
                {"/", (a,b) => a/b },
                {"%", (a,b) => a%b }
            };

        }

        //свойства с логикой
        public string CurrentInput
        {
            get { return _currentInput; }
            set {
                _currentInput = value; 
                OnPropertyChanged(nameof(CurrentInput));
            }
        }

        public void AppendNumber(string number)
        {
            if (number == ",") number = ".";
            if (_isNewInput)
            {
                CurrentInput = number; //начинаем новое число
                _isNewInput = false;
            }
            else  
            {
                CurrentInput += number;
            }
            //особые случаи
            if (CurrentInput == ".")
            {
                CurrentInput = "0";
            }
            else if (CurrentInput.StartsWith("0") && CurrentInput.Length > 1 && !CurrentInput.Contains(".")) 
            {
                CurrentInput = CurrentInput.TrimStart('0');
            }   
        }
        public void SetOperation(string operation)
        {
            if(operation == "-" && _isNewInput)
            {
                AppendNumber("-");
                return;
            }
            if (!string.IsNullOrEmpty(_currentOperation) && !_isNewInput)
            {
                Calculate();

            }
            _previousInput = _currentInput;
            _currentOperation = operation;
            _isNewInput = true;
        }
        public void Calculate()
        {
            if (string.IsNullOrEmpty(_currentOperation) && string.IsNullOrEmpty(_previousInput)) return;
            if (!Operations.ContainsKey(_currentOperation))
                return;

            double a = double.Parse(_previousInput, CultureInfo.InvariantCulture);
            double b = double.Parse(_currentInput, CultureInfo.InvariantCulture);


            if (Operations.ContainsKey(_currentOperation))
            {

                double result = Operations[_currentOperation](a, b);

                CurrentInput = result.ToString(CultureInfo.InvariantCulture);
                _currentOperation = "";
                _isNewInput = true;
            }
        }
        public void Erase()
        {
            if (_currentInput.Length > 1)
            {
                CurrentInput = _currentInput.Substring(0, _currentInput.Length - 1);
            }
            else
            {
                CurrentInput = "0";
                _isNewInput = true;
            }
        }
        public void Negate()
        {
            if(_currentInput != "0")
            {
                if (_currentInput.StartsWith("-"))
                    {
                    CurrentInput = _currentInput.Substring(1);
                } else
                {
                    CurrentInput = "-" + _currentInput;
                }
            }
        }
        public void Clear()
        {
            _currentInput = "0";
            _previousInput = "";
            _currentOperation = "";
            _isNewInput = true;
        }
    }
}
