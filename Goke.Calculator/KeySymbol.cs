namespace Goke.Calculator
{   
    public enum Key
    {
        None,
        One = 1, Two, Three, Four, Five, Six, Seven, Eight, Nine, Zero, Point,
        Clear = 21, ClearExpression, Backspace, Equal, BracketOpen, BracketClose, EXP,
        Answer,
        STORE, A, B, C, D, E, F, G, H, I, J, K, L, M, N, O, P, Q, R, S, T, U, V, W, X, Y, Z,
        Plus = 91, Minus, Multiply, Divide,
        Negate = 101, Percent, Reciprocal, Abs, Pi, e,
        Square, Cube, SquareRoot, CubeRoot, TenPower, Log, Ln, Factorial,
        DEGREE, RADIAN, Sine, Cosine, Tangent, ArcSine, ArcCosine, ArcTangent,
        Secant, Cosecant, Cotangent, ArcSecant, ArcCosecant, ArcCotangent,
        Sinh, Cosh, Tanh, ArcSinh, ArcCosh, ArcTanh,
        XPowerY = 151, YRootX, XModY,
        COMPUTE = 200, Quadratic,
    }

    public enum KeyType { None, Numeric, Arithmethic, Binary, Unary, Trigonometric, Storage, Parametric }

    public class KeySymbol
    {
        static KeyType GetKeyType(Key key) => key switch
        {
            >= Key.One and <= Key.Point => KeyType.Numeric,
            >= Key.A and <= Key.Z => KeyType.Storage,
            >= Key.Plus and <= Key.Divide => KeyType.Arithmethic,
            >= Key.DEGREE and <= Key.ArcTanh => KeyType.Trigonometric,
            >= Key.Negate and < Key.XPowerY => KeyType.Unary,
            >= Key.XPowerY and < Key.Quadratic => KeyType.Binary,
            >= Key.Quadratic => KeyType.Parametric,
            _ => KeyType.None,
        };

        public Calculator.Key Key { get; set; }
        public String? Symbol { get; set; }

        public bool Italic { get; set; }

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
            new(){Key=Calculator.Key.Backspace, Symbol="&#x21d0;"},

            new(){Key=Calculator.Key.Equal, Symbol="&#x003d;"},
            new(){Key=Calculator.Key.BracketOpen, Symbol="("},
            new(){Key=Calculator.Key.BracketClose, Symbol=")"},
            new(){Key=Calculator.Key.Answer, Symbol="ans"},
            new(){Key=Calculator.Key.STORE, Symbol="STO"},
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

            new(){Key=Calculator.Key.DEGREE, Symbol="DEG"},
            new(){Key=Calculator.Key.RADIAN, Symbol="RAD"},
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

            new(){Key=Calculator.Key.COMPUTE, Symbol="=>"},
            new(){Key=Calculator.Key.Quadratic, Symbol="Quadratic"},

    ];
        

        public static bool IsNumeric(Key key)
        {
            return GetKeyType(key) == KeyType.Numeric;
        }

        public static bool IsUnary(Key key)
        {
            return GetKeyType(key) == KeyType.Unary;
        }

        public static bool IsArithmetic(Key key)
        {
            return GetKeyType(key) == KeyType.Arithmethic;
        }

        public static bool IsBinary(Key key)
        {
            return GetKeyType(key) == KeyType.Binary || IsArithmetic(key);
        }

        public static bool IsStorage(Key key)
        {
            return GetKeyType(key) == KeyType.Storage;
        }

        public static bool IsTrigonometric(Key key)
        {
            return GetKeyType(key) == KeyType.Trigonometric;
        }

        public static bool IsParametric(Key key)
        {
            return GetKeyType(key) == KeyType.Parametric;
        }
    };
}
