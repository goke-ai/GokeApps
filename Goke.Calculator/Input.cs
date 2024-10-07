using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goke.Maths;

namespace Goke.Calculator
{
    public class Input
    {       

        private Key lastKey;
        Input? parent;
        static Dictionary<string, double> STORE = [];

        private string text = "0";
        public string Text
        {
            get => text; 
            set
            {
                text = value;
                if (parent is not null)
                {
                    parent.Text = value;
                }
            }
        }

        public double PreviousValue { get; set; }
        public double CurrentValue { get; set; }
        public Key PreviousOperator { get; set; }
        public Key CurrentOperator { get; set; }

        private double answer;
        public double Answer
        {
            get => answer; 
            set
            {
                answer = value;
                Text = answer.ToString();
            }
        }

        public List<string> ExpressionList { get; set; } = [];
        public string ExpressionText => string.Join("", ExpressionList);

        private void ExpressionListRemoveLast()
        {
            var active = parent ?? this;
            active.ExpressionList.RemoveAt(ExpressionList.Count - 1);
        }

        private void ExpressionListAdd(string text)
        {
            //var active = parent ?? this;
            //active.ExpressionList.Add(text);
            if (parent is not null)
            {
                parent.ExpressionListAdd(text);
            }
            else
            {
                ExpressionList.Add(text);
            }
        }

        void ExpressionListClear()
        {
            var active = parent ?? this;
            active.ExpressionList.Clear();
        }

        public void SendKey(Calculator.Key key)
        {
            switch (key)
            {
                case Key.None:
                    break;
                case Key.One:
                    Number('1');
                    break;
                case Key.Two:
                    Number('2');
                    break;
                case Key.Three:
                    Number('3');
                    break;
                case Key.Four:
                    Number('4');
                    break;
                case Key.Five:
                    Number('5');
                    break;
                case Key.Six:
                    Number('6');
                    break;
                case Key.Seven:
                    Number('7');
                    break;
                case Key.Eight:
                    Number('8');
                    break;
                case Key.Nine:
                    Number('9');
                    break;
                case Key.Zero:
                    Number('0');
                    break;
                case Key.Point:
                    Number('.');
                    break;
                case Key.Clear:
                    canBackspace = false;
                    Clear();
                    break;
                case Key.ClearExpression:
                    canBackspace = false;
                    ClearExpression();
                    break;
                case Key.Backspace:
                    BackspaceOperator();
                    break;
                case Key.Equal:
                    canBackspace = false;
                    Equal();
                    break;
                case Key.Answer:
                    canBackspace = false;
                    AnswerOperator();
                    break;
                case Key.BracketOpen:
                    canBackspace = false;
                    BracketOpen();
                    break;
                case Key.BracketClose:
                    canBackspace = false;
                    BracketClose();
                    break;
                case Key.EXP:
                    canBackspace = false;
                    if (!expOn)
                    {
                        ExpOpen();
                    }
                    break;
                case Key.STORE:
                    canBackspace = false;
                    Store();
                    break;
                case Key.DEGREE:
                    canBackspace = false;
                    IsDegree = true;
                    break;
                case Key.RADIAN:
                    canBackspace = false;
                    IsDegree = false;
                    break;
                case Key.COMPUTE:
                    canBackspace = false;
                    CanCompute=!CanCompute;
                    break;

                default:
                    canBackspace = false;
                    if (key != lastKey || !KeySymbol.IsBinary(key))
                    {
                        Operator(key);
                    }
                    break;
            }

            lastKey = key;
        }

        private void Number(char c)
        {
            if(expOn)
            {
                ExpNumber(c);
                return;
            }

            if (IsFirstNumber())
            {
                if (c == '.')
                {
                    Text = "0" + c;
                }
                else
                {
                    Text = c.ToString();
                }
            }
            else
            {
                if (Text == "0")
                {
                    if (c == '.')
                    {
                        Text += c;
                    }
                    else
                    {
                        Text = c.ToString();
                    }
                }
                else
                {
                    if (c == '.')
                    {
                        if(!Text.Contains('.'))
                        {
                            Text += c;
                        }                                         
                    }
                    else
                    {
                        Text += c;
                    }
                }                
            }

            CurrentValue = double.Parse(Text);
            canBackspace = true;   
        }

        private void Operator(Key key)
        {
            if (KeySymbol.IsStorage(key))
            {
                Storage(key);
            }
            else if(KeySymbol.IsParametric(key))
            {
                Parametric(key);
            }
            else if (KeySymbol.IsUnary(key))
            {
                if (expOn)
                {
                    ExpNegate();
                    return;
                }

                CurrentValue = double.Parse(Text);

                UnaryOperation(key);
                CurrentValue = Answer;

                PreviousOperator = CurrentOperator;
                //CurrentOperator = key;
            }
            else if (KeySymbol.IsBinary(key))
            {
                if (expOn)
                {
                    ExpClose();
                }

                if (KeySymbol.IsBinary(lastKey))
                {
                    // change operator                    
                    CurrentOperator = key;
                    // remove last expression
                    ExpressionListRemoveLast();
                }
                else
                {
                    ExpressionListAdd(Text);

                    PreviousOperator = CurrentOperator;
                    CurrentOperator = key;

                    CurrentValue = double.Parse(Text);

                    if (PreviousOperator == Key.None || PreviousOperator == Key.Equal)
                    {
                        PreviousValue = CurrentValue;
                    }
                    else
                    {
                        BinaryOperation(PreviousOperator);
                        PreviousValue = Answer;
                    }
                    // CurrentValue = 0;                    
                }

                if (KeySymbol.IsArithmetic(key))
                {
                    ExpressionListAdd(KeySymbol.KEYS.First(f => f.Key == key).Symbol!);
                }
                else
                {
                    var symbol = KeySymbol.KEYS.First(f => f.Key == key).Symbol;
                    if (symbol != null)
                    {
                        symbol = symbol.Replace("x", "");
                        symbol = symbol.Replace("y", "");
                        symbol = symbol.Replace("(", "");
                        symbol = symbol.Replace(")", "");
                        ExpressionListAdd(symbol);
                    }
                }
            }
        }

       
        private void Parametric(Key key)
        {
            try
            {
                switch (key)
                {
                    case Key.Quadratic:
                        OnQuadratic();
                        break;
                    default:
                        break;
                }

            }
            catch (Exception ex)
            {
                Text = ex.Message;
            }
        }

        private void OnQuadratic()
        {
            if (CanCompute)
            {
                double value = 0;
                var a = STORE.TryGetValue("A", out value) ? value : 0;
                var b = STORE.TryGetValue("B", out value) ? value : 0;
                var c = STORE.TryGetValue("C", out value) ? value : 0;

                (var r1, var r2, var i1, var i2) = Maths.Roots.Formula(a, b, c);

                if (i1 == 0 && i2 == 0)
                {
                    STORE["D"] = r1;
                    STORE["E"] = r2;

                    Text = $"{r1}, {r2}";
                }
                else
                {
                    STORE["D"] = r1;
                    STORE["E"] = r2;
                    STORE["F"] = i1;
                    STORE["G"] = i2;

                    Text = $"{r1}+{i1}, {r2}+{i2}";
                }
                CanCompute = false;

                GraphData = Roots.Quadratic(a, b, c);
            }
            else
            {
                Text = $"STO(A, B, C) => D, E";
            }
        }

        private void Equal()
        {
            if (parent is not null)
            {
                OnBracketClose?.Invoke(this, EventArgs.Empty);
                return;
            }

            if (CurrentOperator == Key.None)
            {
                ExpressionListAdd(Text);
                ExpressionListAdd($"{KeySymbol.KEYS.First(f => f.Key == Key.Equal).Symbol!} ");

                CurrentValue = double.Parse(Text);
                PreviousValue = CurrentValue;
                Answer = PreviousValue;
            }
            else
            {
                if (CurrentOperator == Key.Equal)
                {
                    if (PreviousOperator == Key.None)
                    {
                        ExpressionListAdd(Text);
                        ExpressionListAdd($"{KeySymbol.KEYS.First(f => f.Key == Key.Equal).Symbol!} ");

                        CurrentValue = double.Parse(Text);
                        PreviousValue = CurrentValue;
                    }
                    else
                    {
                        ExpressionListAdd(Text);
                        ExpressionListAdd(KeySymbol.KEYS.First(f => f.Key == PreviousOperator).Symbol!);
                        ExpressionListAdd(CurrentValue.ToString());
                        ExpressionListAdd($"{KeySymbol.KEYS.First(f => f.Key == Key.Equal).Symbol!} ");
                    }
                }
                else
                {
                    ExpressionListAdd(Text);
                    ExpressionListAdd($"{KeySymbol.KEYS.First(f => f.Key == Key.Equal).Symbol!} ");

                    PreviousOperator = CurrentOperator;
                    CurrentValue = double.Parse(Text);
                }
                BinaryOperation(PreviousOperator);
                PreviousValue = Answer;
            }
            //CurrentValue = 0;
            //PreviousOperator = Key.None;
            CurrentOperator = Key.Equal;
        }

        private void AnswerOperator()
        {
            Text = Answer.ToString();
        }

        private void Store()
        {
            CanStore = !CanStore;
        }

        private void Storage(Key key)
        {
            if (CanStore)
            {
                ToStore(key);
                CanStore = false;

            }
            else
            {
                FromStore(key);
            }
        }

        private void FromStore(Key key)
        {
            Text = STORE.TryGetValue(key.ToString(), out var value) ? value.ToString():"0";
            ExpressionListAdd(KeySymbol.KEYS.First(f => f.Key == key).Symbol!);
        }

        private void ToStore(Key key)
        {
            Input.STORE[key.ToString()] = double.Parse(Text);
            ExpressionListAdd($"{KeySymbol.KEYS.First(f => f.Key == key).Symbol!}({Text})");
        }

        private void Clear()
        {
            Text = "0";
            Answer = 0;
            PreviousValue = 0;
            CurrentValue = 0;
            PreviousOperator = 0;
            CurrentOperator = 0;
            parent = null;

            ExpressionListClear();
        }

        private void ClearExpression()
        {
            Text = "0";
            CurrentValue = 0;
            CurrentOperator = Key.ClearExpression;
        }

        bool canBackspace = false;

        public bool CanCompute { get; private set; }
        public (double[] x, double[] y)? GraphData { get; private set; }
        public bool IsDegree { get; private set; } = true;
        public bool CanStore { get; private set; }

        private void BackspaceOperator()
        {
            if (canBackspace)
            {
                // remove lastkey
                if (Text.Length == 1)
                {
                    Text = "0";
                }
                else
                {
                    Text = Text.Remove(Text.Length - 1);
                }
            }
        }

        public EventHandler? OnBracketClose { get; set; }

        public Input BracketOpen()
        {
            canBackspace = false;
            
            if (lastKey == Key.None)
            {
                PreviousValue = 1;
                PreviousOperator = Key.Multiply;
            }
            else if (lastKey == Key.BracketOpen)
            {
                PreviousValue = 1;
                PreviousOperator = Key.Multiply;
            }
            else if (KeySymbol.IsNumeric(lastKey))
            {
                ExpressionListAdd(Text);
                CurrentValue = double.Parse(Text);
                PreviousValue = CurrentValue;

                ExpressionListAdd(KeySymbol.KEYS.First(f => f.Key == Key.Multiply).Symbol!);
                CurrentOperator = Key.Multiply;
                PreviousOperator = CurrentOperator;
            }
            else if (KeySymbol.IsArithmetic(lastKey))
            {
                CurrentValue = double.Parse(Text);
                PreviousValue = CurrentValue;
                PreviousOperator = CurrentOperator;
            }

            ExpressionListAdd(KeySymbol.KEYS.First(f => f.Key == Key.BracketOpen).Symbol!);

            lastKey = Key.BracketOpen;

            // create 
            Input child = new()
            {
                Text = this.Text,
                parent = this,
            };


            return child;
        }

        public void BracketClose()
        {
            canBackspace = false;

            ExpressionListAdd(Text);
            ExpressionListAdd(KeySymbol.KEYS.First(f => f.Key == Key.BracketClose).Symbol!);
            ExpressionListAdd("|");

            // compute
            PreviousOperator = CurrentOperator;
            CurrentValue = double.Parse(Text);

            BinaryOperation(PreviousOperator);
            PreviousValue = Answer;

            // close
            if (parent is not null)
            {
                parent.CurrentValue = PreviousValue;
                parent.CurrentOperator = parent.PreviousOperator;
                parent.PreviousOperator = Key.BracketClose;
            }

            lastKey = Key.BracketOpen;
        }

        bool expOn;
        double x = 0, y = 0;
        private void ExpOpen()
        {
            if (KeySymbol.IsNumeric(lastKey))
            {
                expOn = true;
                x = double.Parse(Text);

                Text = $"{x}e{y}";
            }
        }

        void ExpNumber(char c)
        {
            if(c != '.')
            {
                var s = y.ToString() + c;
                y = double.Parse(s);

                if (y < 0)
                {
                    Text = $"{x}e{y}";
                }
                else
                {
                    Text = $"{x}e+{y}";
                }
            }
        }

        void ExpNegate()
        {
            y = -y;
            //if (y < 0)
            //{
            //    Text = $"{x}e{y}";
            //}
            //else
            //{
            //    Text = $"{x}e+{y}";
            //}
            Text = $"{x}e{y}";
        }

        private void ExpClose()
        {
            ExpressionListAdd($"{Text}|");

            Answer = x * Math.Pow(10, y);
            // CurrentValue = Answer;
            expOn = false;
            x = 0;
            y = 0;
        }

        private void BinaryOperation(Key key)
        {
            switch (key)
            {
                case Key.Plus:
                    Answer = PreviousValue + CurrentValue;
                    break;
                case Key.Minus:
                    Answer = PreviousValue - CurrentValue;
                    break;
                case Key.Multiply:
                    Answer = PreviousValue * CurrentValue;
                    break;
                case Key.Divide:
                    Answer = PreviousValue / CurrentValue;
                    break;
                case Key.XPowerY:
                    Answer = Math.Pow(PreviousValue, CurrentValue);
                    break;
                case Key.XModY:
                    Answer = PreviousValue % CurrentValue;
                    break;
                case Key.YRootX:
                    Answer = Math.Pow(PreviousValue, (1/CurrentValue));
                    break;
                default:
                    break;
            }
        }

        private void UnaryOperation(Key key)
        {
            switch (key)
            {
                case Key.Negate:
                    Answer = -CurrentValue;
                    ExpressionListAdd($"neg({CurrentValue})|");
                    break;
                case Key.Percent:
                    Answer = CurrentValue/100.0;
                    ExpressionListAdd($"%({CurrentValue})|");
                    break;
                case Key.Reciprocal:
                    Answer = 1/CurrentValue;
                    ExpressionListAdd($"1/({CurrentValue})|");
                    break;
                case Key.Abs:
                    Answer = CurrentValue < 0 ? -CurrentValue : CurrentValue;
                    ExpressionListAdd($"|{CurrentValue}||");
                    break;

                case Key.Pi:
                    Answer = Math.PI;
                    ExpressionListAdd("Pi|");
                    break;
                case Key.e:
                    Answer = Math.E;
                    ExpressionListAdd($"e|");
                    break;

                case Key.Square:
                    Answer = CurrentValue*CurrentValue;
                    ExpressionListAdd($"sq({CurrentValue})|");
                    break;
                case Key.Cube:
                    Answer = CurrentValue * CurrentValue * CurrentValue;
                    ExpressionListAdd($"cb({CurrentValue})|");
                    break;
                case Key.SquareRoot:
                    Answer = Math.Sqrt(CurrentValue);
                    ExpressionListAdd($"sqrt({CurrentValue})|");
                    break;
                case Key.CubeRoot:
                    Answer = Math.Cbrt(CurrentValue);
                    ExpressionListAdd($"cbrt({CurrentValue})|");
                    break;
                case Key.TenPower:
                    Answer = Math.Pow(10,CurrentValue);
                    ExpressionListAdd($"10^({CurrentValue})|");
                    break;
                case Key.Log:
                    Answer = Math.Log10(CurrentValue);
                    ExpressionListAdd($"log({CurrentValue})|");
                    break;
                case Key.Ln:
                    Answer = Math.Log(CurrentValue);
                    ExpressionListAdd($"ln({CurrentValue})|");
                    break;
                case Key.Factorial:
                    Answer = Maths.Functions.Factorial((int)CurrentValue);
                    ExpressionListAdd($"{CurrentValue}!|");
                    break;
                case Key.EXP:
                    break;
                case Key.Sine:
                    var angle = TrigonometryConvert();
                    Answer = (float)Math.Sin(angle);
                    ExpressionListAdd($"sin({CurrentValue})|");
                    break;
                case Key.Cosine:
                    angle = TrigonometryConvert();
                    Answer = (float)Math.Cos(angle);
                    ExpressionListAdd($"cos({CurrentValue})|");
                    break;
                case Key.Tangent:
                    angle = TrigonometryConvert();
                    Answer = (float)Math.Tan(angle);
                    ExpressionListAdd($"tan({CurrentValue})|");
                    break;
                case Key.ArcSine:
                    angle = Math.Asin(CurrentValue);
                    Answer = (float)TrigonometryAConvert(angle);
                    ExpressionListAdd($"arcsin({CurrentValue})|");
                    break;
                case Key.ArcCosine:
                    angle = Math.Acos(CurrentValue);
                    Answer = (float)TrigonometryAConvert(angle);
                    ExpressionListAdd($"arccos({CurrentValue})|");
                    break;
                case Key.ArcTangent:
                    angle = Math.Atan(CurrentValue);
                    Answer = (float)TrigonometryAConvert(angle);
                    ExpressionListAdd($"arctan({CurrentValue})|");
                    break;
                case Key.Secant:
                    angle = TrigonometryConvert();
                    Answer = 1 / (float)Math.Cos(angle);
                    ExpressionListAdd($"sec({CurrentValue})|");
                    break;
                case Key.Cosecant:
                    angle = TrigonometryConvert();
                    Answer = 1 / (float)Math.Sin(angle);
                    ExpressionListAdd($"csc({CurrentValue})|");
                    break;
                case Key.Cotangent:
                    angle = TrigonometryConvert();
                    Answer = 1 / (float)Math.Tan(angle);
                    ExpressionListAdd($"cot({CurrentValue})|");
                    break;
                case Key.ArcSecant:
                    angle = Math.Acos(CurrentValue);
                    Answer = 1 / (float)TrigonometryAConvert(angle);
                    ExpressionListAdd($"arcsec({CurrentValue})|");
                    break;
                case Key.ArcCosecant:
                    angle = Math.Asin(CurrentValue);
                    Answer = 1 / (float)TrigonometryAConvert(angle);
                    ExpressionListAdd($"arccsc({CurrentValue})|");
                    break;
                case Key.ArcCotangent:
                    angle = Math.Atan(CurrentValue);
                    Answer = 1 / (float)TrigonometryAConvert(angle); 
                    ExpressionListAdd($"arccot({CurrentValue})|");
                    break;
                case Key.Sinh:
                    angle = TrigonometryConvert();
                    Answer = (float)Math.Sinh(angle);
                    ExpressionListAdd($"sinh({CurrentValue})|");
                    break;
                case Key.Cosh:
                    angle = TrigonometryConvert();
                    Answer = (float)Math.Cosh(angle);
                    ExpressionListAdd($"cosh({CurrentValue})|");
                    break;
                case Key.Tanh:
                    angle = TrigonometryConvert();
                    Answer = (float)Math.Tanh(angle);
                    ExpressionListAdd($"tanh({CurrentValue})|");
                    break;
                case Key.ArcSinh:
                    angle = Math.Asinh(CurrentValue);
                    Answer = (float)TrigonometryAConvert(angle);
                    ExpressionListAdd($"arcsinh({CurrentValue})|");
                    break;
                case Key.ArcCosh:
                    angle = Math.Acosh(CurrentValue);
                    Answer = (float)TrigonometryAConvert(angle);
                    ExpressionListAdd($"arccosh({CurrentValue})|");
                    break;
                case Key.ArcTanh:
                    angle = Math.Atanh(CurrentValue);
                    Answer = (float)TrigonometryAConvert(angle);
                    ExpressionListAdd($"arctanh({CurrentValue})|");
                    break;
                default:
                    break;
            }
        }

        private double TrigonometryConvert()
        {
            if (IsDegree)
            {
                return Maths.Functions.DegreeToRadian(CurrentValue);
            }
            else
            {
                return CurrentValue;
            }
        }

        private double TrigonometryAConvert(double value)
        {
            if (IsDegree)
            {
                return Maths.Functions.RadianToDegree(value);
            }
            else
            {
                return value;
            }
        }

        private bool IsFirstNumber()
        {
            return !(KeySymbol.IsNumeric(lastKey) /*|| lastKey==Key.Backspace*/);
        }

        


    }
}
