using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Goke.Maths;

namespace Goke.Calculator
{
    public enum KeyType {None, Numeric, Arithmethic, Binary, Unary, Storage }
    public enum Key
    {
        None,
        One = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Zero, Point,
        Clear = 21, ClearExpression, Backspace, Equal, BracketOpen, BracketClose, EXP,
        Answer, Store, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
        Plus = 91, Minus, Multiply, Divide,
        Negate = 101, Percent, Reciprocal, Abs, Pi, e,
        Square, Cube, SquareRoot, CubeRoot, TenPower, Log, Ln, Factorial,
        Sine, Cosine, Tangent, ArcSine, ArcCosine, ArcTangent,
        Secant, Cosecant, Cotangent, ArcSecant, ArcCosecant, ArcCotangent,
        Sinh, Cosh, Tanh, ArcSinh, ArcCosh, ArcTanh,
        XPowerY = 201, YRootX, XModY,
    }

    public class KeySymbol
    {
        public Calculator.Key Key { get; set; }
        public String? Symbol { get; set; }

        public bool Italic { get; set; }
    };

    public class Input
    {
        public static List<KeySymbol> KEYS => [
            new(){Key=Calculator.Key.One, Symbol="1"},
            new(){Key=Calculator.Key.Two, Symbol="2"},
            new(){Key=Calculator.Key.Three, Symbol="3"},
            new(){Key=Calculator.Key.Four, Symbol="4"},
            new(){Key=Calculator.Key.Five, Symbol="5"},
            new(){Key=Calculator.Key.Six, Symbol="6"},
            new(){Key=Calculator.Key.Seven, Symbol="7"},
            new(){Key=Calculator.Key.Eight, Symbol="8"},
            new(){Key=Calculator.Key.Nine, Symbol="9"},
            new(){Key=Calculator.Key.Zero, Symbol="0"},
            new(){Key=Calculator.Key.Point, Symbol="&#x002e;"},

            new(){Key=Calculator.Key.Clear, Symbol="C"},
            new(){Key=Calculator.Key.ClearExpression, Symbol="CE"},
            new(){Key=Calculator.Key.Backspace, Symbol="<="},

            new(){Key=Calculator.Key.Equal, Symbol="&#x003d;"},
            new(){Key=Calculator.Key.BracketOpen, Symbol="("},
            new(){Key=Calculator.Key.BracketClose, Symbol=")"},
            new(){Key=Calculator.Key.Answer, Symbol="ans"},
            new(){Key=Calculator.Key.Store, Symbol="STO"},
            new(){Key=Calculator.Key.A, Symbol="A"},
            new(){Key=Calculator.Key.B, Symbol="B"},
            new(){Key=Calculator.Key.C, Symbol="C"},
            new(){Key=Calculator.Key.D, Symbol="D"},
            new(){Key=Calculator.Key.E, Symbol="E"},
            new(){Key=Calculator.Key.F, Symbol="F"},
            new(){Key=Calculator.Key.G, Symbol="G"},
            new(){Key=Calculator.Key.H, Symbol="H"},
            new(){Key=Calculator.Key.I, Symbol="I"},
            new(){Key=Calculator.Key.J, Symbol="J"},
            new(){Key=Calculator.Key.K, Symbol="K"},
            new(){Key=Calculator.Key.L, Symbol="L"},
            new(){Key=Calculator.Key.O, Symbol="O"},
            new(){Key=Calculator.Key.P, Symbol="P"},
            new(){Key=Calculator.Key.Q, Symbol="R"},
            new(){Key=Calculator.Key.R, Symbol="Q"},
            new(){Key=Calculator.Key.S, Symbol="S"},
            new(){Key=Calculator.Key.T, Symbol="T"},
            new(){Key=Calculator.Key.W, Symbol="W"},
            new(){Key=Calculator.Key.X, Symbol="X"},
            new(){Key=Calculator.Key.Y, Symbol="Y"},
            new(){Key=Calculator.Key.Z, Symbol="Z"},

            new(){Key=Calculator.Key.Plus, Symbol="&#x002b;"},
            new(){Key=Calculator.Key.Minus, Symbol="&#x2212;"},
            new(){Key=Calculator.Key.Multiply, Symbol="&#x00d7;"},
            new(){Key=Calculator.Key.Divide, Symbol="&#x00f7;"},

            new(){Key=Calculator.Key.Negate, Symbol="&#x00b1;"},
            new(){Key=Calculator.Key.Percent, Symbol="&#x0025;"},
            new(){Key=Calculator.Key.Reciprocal, Symbol="&#x00b9;/x"},
            new(){Key=Calculator.Key.Abs, Symbol="&#x2223;x&#x2223;"},
            new(){Key=Calculator.Key.Pi, Symbol="&#x03c0;", Italic=true},
            new(){Key=Calculator.Key.e, Symbol="&#x0435;", Italic=true},


            new(){Key=Calculator.Key.Square, Symbol="x&#x00b2;"},
            new(){Key=Calculator.Key.Cube, Symbol="x&#x00b3;"},
            new(){Key=Calculator.Key.SquareRoot, Symbol="&#x221a;"},
            new(){Key=Calculator.Key.CubeRoot, Symbol="&#x221b;"},
            new(){Key=Calculator.Key.TenPower, Symbol="10&#x207f;"},
            new(){Key=Calculator.Key.Log, Symbol="log"},
            new(){Key=Calculator.Key.Ln, Symbol="ln"},
            new(){Key=Calculator.Key.Factorial, Symbol="x!"},

            new(){Key=Calculator.Key.EXP, Symbol="exp", Italic=true},

            new(){Key=Calculator.Key.Sine, Symbol="sin", Italic=true},
            new(){Key=Calculator.Key.Cosine, Symbol="cos", Italic=true},
            new(){Key=Calculator.Key.Tangent, Symbol="tan", Italic=true},
            new(){Key=Calculator.Key.ArcSine, Symbol="sin&#x207b;&#x00b9;", Italic=true},
            new(){Key=Calculator.Key.ArcCosine, Symbol="cos&#x207b;&#x00b9;", Italic=true},
            new(){Key=Calculator.Key.ArcTangent, Symbol="tan&#x207b;&#x00b9;", Italic=true},
            new(){Key=Calculator.Key.Secant, Symbol="sec", Italic=true},
            new(){Key=Calculator.Key.Cosecant, Symbol="csc", Italic=true},
            new(){Key=Calculator.Key.Cotangent, Symbol="cot", Italic=true},
            new(){Key=Calculator.Key.ArcSecant, Symbol="sec&#x207b;&#x00b9;", Italic=true},
            new(){Key=Calculator.Key.ArcCosecant, Symbol="csc&#x207b;&#x00b9;", Italic=true},
            new(){Key=Calculator.Key.ArcCotangent, Symbol="cot&#x207b;&#x00b9;", Italic=true},
            new(){Key=Calculator.Key.Sinh, Symbol="sinh", Italic=true},
            new(){Key=Calculator.Key.Cosh, Symbol="cosh", Italic=true},
            new(){Key=Calculator.Key.Tanh, Symbol="tanh", Italic=true},
            new(){Key=Calculator.Key.ArcSinh, Symbol="sinh&#x207b;&#x00b9;", Italic=true},
            new(){Key=Calculator.Key.ArcCosh, Symbol="cosh&#x207b;&#x00b9;", Italic=true},
            new(){Key=Calculator.Key.ArcTanh, Symbol="tanh&#x207b;&#x00b9;", Italic=true},

            new(){Key=Calculator.Key.XPowerY, Symbol="x&#x207f;", Italic=true},
            new(){Key=Calculator.Key.YRootX, Symbol="&#x207f;&#x221a;x", Italic=true},
            new(){Key=Calculator.Key.XModY, Symbol="mod"},

    ];
        public static KeyType GetKeyType(Key key) => key switch
        {
            >= Key.One and <= Key.Point => KeyType.Numeric,
            >= Key.A and <= Key.Z => KeyType.Storage,
            >= Key.Plus and <= Key.Divide => KeyType.Arithmethic,
            >= Key.Negate and < Key.XPowerY => KeyType.Unary,
            >= Key.XPowerY => KeyType.Binary,
            _ => KeyType.None,
        };

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
                case Key.Store:
                    canBackspace = false;
                    Store();
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

                default:
                    canBackspace = false;
                    if (key != lastKey || !IsBinary(key))
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
            if (IsStorage(key))
            {
                Storage(key);
            }
            else if (IsUnary(key))
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
            else if (IsBinary(key))
            {
                if (expOn)
                {
                    ExpClose();
                }

                if (IsBinary(lastKey))
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

                if (IsArithmetic(key))
                {
                    ExpressionListAdd(KEYS.First(f => f.Key == key).Symbol!);
                }
                else
                {
                    var symbol = KEYS.First(f => f.Key == key).Symbol;
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
                ExpressionListAdd($"{KEYS.First(f => f.Key == Key.Equal).Symbol!} ");

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
                        ExpressionListAdd($"{KEYS.First(f => f.Key == Key.Equal).Symbol!} ");

                        CurrentValue = double.Parse(Text);
                        PreviousValue = CurrentValue;
                    }
                    else
                    {
                        ExpressionListAdd(Text);
                        ExpressionListAdd(KEYS.First(f => f.Key == PreviousOperator).Symbol!);
                        ExpressionListAdd(CurrentValue.ToString());
                        ExpressionListAdd($"{KEYS.First(f => f.Key == Key.Equal).Symbol!} ");
                    }
                }
                else
                {
                    ExpressionListAdd(Text);
                    ExpressionListAdd($"{KEYS.First(f => f.Key == Key.Equal).Symbol!} ");

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
            canStore = true;

        }

        private void Storage(Key key)
        {
            if (canStore)
            {
                ToStore(key);
                canStore = false;

            }
            else
            {
                FromStore(key);
            }
        }

        private void FromStore(Key key)
        {
            Text = STORE.TryGetValue(key.ToString(), out var value) ? value.ToString():"0";
            ExpressionListAdd(KEYS.First(f => f.Key == key).Symbol!);
        }

        private void ToStore(Key key)
        {
            Input.STORE[key.ToString()] = double.Parse(Text);
            ExpressionListAdd($"{KEYS.First(f => f.Key == key).Symbol!}({Text})");
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
        private bool canStore;

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
            else if (IsNumeric(lastKey))
            {
                ExpressionListAdd(Text);
                CurrentValue = double.Parse(Text);
                PreviousValue = CurrentValue;

                ExpressionListAdd(KEYS.First(f => f.Key == Key.Multiply).Symbol!);
                CurrentOperator = Key.Multiply;
                PreviousOperator = CurrentOperator;
            }
            else if (IsArithmetic(lastKey))
            {
                CurrentValue = double.Parse(Text);
                PreviousValue = CurrentValue;
                PreviousOperator = CurrentOperator;
            }

            ExpressionListAdd(KEYS.First(f => f.Key == Key.BracketOpen).Symbol!);

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
            ExpressionListAdd(KEYS.First(f => f.Key == Key.BracketClose).Symbol!);
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
            if (IsNumeric(lastKey))
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
                    Answer = Maths.Maths.Factorial((int)CurrentValue);
                    ExpressionListAdd($"{CurrentValue}!|");
                    break;
                case Key.EXP:
                    break;
                case Key.Sine:
                    var angleInRadian = DegreeToRadian();
                    Answer = Math.Sin(angleInRadian);
                    ExpressionListAdd($"sin({CurrentValue})|");
                    break;
                case Key.Cosine:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Cos(angleInRadian);
                    ExpressionListAdd($"cos({CurrentValue})|");
                    break;
                case Key.Tangent:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Tan(angleInRadian);
                    ExpressionListAdd($"tan({CurrentValue})|");
                    break;
                case Key.ArcSine:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Asin(angleInRadian);
                    ExpressionListAdd($"arcsin({CurrentValue})|");
                    break;
                case Key.ArcCosine:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Acos(angleInRadian);
                    ExpressionListAdd($"arccos({CurrentValue})|");
                    break;
                case Key.ArcTangent:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Atan(angleInRadian);
                    ExpressionListAdd($"arctan({CurrentValue})|");
                    break;
                case Key.Secant:
                    angleInRadian = DegreeToRadian();
                    Answer = 1/Math.Cos(angleInRadian);
                    ExpressionListAdd($"sec({CurrentValue})|");
                    break;
                case Key.Cosecant:
                    angleInRadian = DegreeToRadian();
                    Answer = 1/ Math.Sin(angleInRadian);
                    ExpressionListAdd($"csc({CurrentValue})|");
                    break;
                case Key.Cotangent:
                    angleInRadian = DegreeToRadian();
                    Answer = 1/Math.Tan(angleInRadian);
                    ExpressionListAdd($"cot({CurrentValue})|");
                    break;
                case Key.ArcSecant:
                    angleInRadian = DegreeToRadian();
                    Answer = 1/Math.Acos(angleInRadian);
                    ExpressionListAdd($"arcsec({CurrentValue})|");
                    break;
                case Key.ArcCosecant:
                    angleInRadian = DegreeToRadian();
                    Answer = 1/Math.Asin(angleInRadian);
                    ExpressionListAdd($"arccsc({CurrentValue})|");
                    break;
                case Key.ArcCotangent:
                    angleInRadian = DegreeToRadian();
                    Answer = 1/ Math.Atan(angleInRadian);
                    ExpressionListAdd($"arccot({CurrentValue})|");
                    break;
                case Key.Sinh:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Sinh(angleInRadian);
                    ExpressionListAdd($"sinh({CurrentValue})|");
                    break;
                case Key.Cosh:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Cosh(angleInRadian);
                    ExpressionListAdd($"cosh({CurrentValue})|");
                    break;
                case Key.Tanh:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Tanh(angleInRadian);
                    ExpressionListAdd($"tanh({CurrentValue})|");
                    break;
                case Key.ArcSinh:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Asinh(angleInRadian);
                    ExpressionListAdd($"arcsinh({CurrentValue})|");
                    break;
                case Key.ArcCosh:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Acosh(angleInRadian);
                    ExpressionListAdd($"arccosh({CurrentValue})|");
                    break;
                case Key.ArcTanh:
                    angleInRadian = DegreeToRadian();
                    Answer = Math.Atanh(angleInRadian);
                    ExpressionListAdd($"arctanh({CurrentValue})|");
                    break;
                default:
                    break;
            }
        }

        private double DegreeToRadian()
        {
            return Maths.Maths.DegreeToRadian(CurrentValue);
        }

        private bool IsFirstNumber()
        {
            return !(IsNumeric(lastKey) /*|| lastKey==Key.Backspace*/);
        }

        private static bool IsNumeric(Key key)
        {
            return GetKeyType(key) == KeyType.Numeric;
        }

        private static bool IsUnary(Key key)
        {
            return GetKeyType(key) == KeyType.Unary;
        }

        private static bool IsArithmetic(Key key)
        {
            return GetKeyType(key) == KeyType.Arithmethic;
        }

        private static bool IsBinary(Key key)
        {
            return GetKeyType(key) == KeyType.Binary || IsArithmetic(key);
        }

        private bool IsStorage(Key key)
        {
            return GetKeyType(key) == KeyType.Storage;
        }

    }
}
