namespace Goke.Web.Services
{
    public class GasificService
    {

        static readonly Dictionary<string, PhysicaPropertyData> PPD = new()    {
        {"C", new(0,"C","CARBON",12.0107,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0,0) },
        {"CO", new(46, "CO", "CARBON-MONOXIDE", 28.01, -205.1, -191.5, 132.9, 35.0, 0.093, 803.0, -192.0, 6046.0, 94.06, 48.9, -110.62, -137.37, 30.869, -0.01285, 2.7892e-05, -1.272e-08, 14.3686, 530.22, -13.15, -210.0, -165.0, 197.71, 5.619e-3, -1.19e-5, 6.383e-9, -1.846e-12, -4.891e2, 0.868, -6.131e-2)},
        {"CO2",new (48,"CO2","CARBON-DIOXIDE",44.01,-56.6,-78.5,304.2,73.8,0.094,777.0,20.0,17166.0,578.08,185.24,-393.77,-394.65,19.795,0.073436,-5.602e-05,1.7153e-08,22.5898,3103.39,-0.16,-119.0,-69.0, 213.47, -1.949e-2, 3.122e-5,-2.448e-8, 6.946e-12,-4.891e2, 5.27,-0.1207)},
        {"CH4", new (64,"CH4","METHANE",16.043,-182.5,-161.5,190.6,46.0,0.099,425.0,-161.0,8185.0,114.14,57.6,-74.86,-50.87,19.251,0.052126,1.1974e-05,-1.132e-08,15.2243,597.84,-7.16,-180.0,-153.0,186.26, -4.62e-2, 1.13e-5, 1.319e-8,-6.647e-12,-4.891e-2, 14.11, 0.2234)},
        {"C2H4", new(92, "C2H4", "ETHYLENE", 28.054, -169.2, -103.8, 282.4, 50.4, 0.129, 577.0, -110.0, 13553.0, 168.98, 93.94, 52.33, 68.16, 3.806, 0.15659, -8.348e-05, 1.7551e-08, 15.5368, 1347.01, -18.15, -153.0, -91.0, 219.24, -7.281e-2, 5.802e-5,-1.861e-8, 5.648e-13,-9.782e2,-0.32,-0.4076)},
        {"CH4O", new(65, "CH4O", "METHANOL", 32.042, -97.7, 64.6, 512.6, 81.0, 0.118, 791.0, 20.0, 35278.0, 555.3, 260.64, -201.3, -162.62, 21.152, 0.070924, 2.587e-05, -2.852e-08, 18.5875, 3626.55, -34.29, -16.0, 91.0, 218.57, -5.834e-2,2.07e-5,1.491e-8,-9.614e-12,-4.891e2,16.88,-0.2467)},
        {"H2O", new(20,"H2O","STEAM",18.015,0.0,100.0,647.3,220.5,0.056,998.0,20.0,40683.0,658.25,283.16,-242.0,-228.77,32.243,0.0019238,1.0555e-05,-3.596e-09,18.3036,3816.44,-46.13,11.0,168.0,69.96, -8.95e-3,-3.672e-6,5.209e-9,-1.478e-12,0,2.868,-0.0172)},
        {"H2Ow", new(20,"H2O","WATER",18.015,0.0,100.0,647.3,220.5,0.056,998.0,20.0,40683.0,658.25,283.16,-242.0,-228.77,32.243,0.0019238,1.0555e-05,-3.596e-09,18.3036,3816.44,-46.13,11.0,168.0,69.96, 0,0,0,0,0,0,0)},
        {"O2", new(34,"O2","OXYGEN",31.999,-218.8,-183.0,154.6,50.5,0.073,1149.0,-183.0,6824.0,85.68,51.5,0.0,0.0,28.106,-3.68e-06,1.7459e-05,-1.065e-08,15.4075,734.55,-6.45,-210.0,-173.0,204.82,0,0,0,0,0,0,0)},
        {"H2", new(19,"H2","HYDROGEN",2.016,-259.2,-252.8,33.2,13.0,0.065,71.0,-253.0,904.0,13.82,5.39,0.0,0.0,27.143,0.0092738,-1.381e-05,7.6451e-09,13.6333,164.9,3.19,-259.0,-248.0,130.46,0,0,0,0,0,0,0)},
        {"N2", new(31,"N2","NITROGEN",28.013,-209.9,-195.8,126.2,33.9,0.09,805.0,-195.0,5581.0,90.3,46.14,0.0,0.0,31.15,-0.01357,2.6796e-05,-1.168e-08,14.9542,588.72,-6.6,-219.0,-183.0,191.32,0,0,0,0,0,0,0)},
        };

        public static GasificResult? Simulate(double h2o, double air, double temperature, double pressure)
        {
            // ultimate
            var rawC = 43.19;
            var rawH = 5.92;
            var rawO = 50.17;
            var rawN = 0.59;

            // normalize
            double sum = rawC + rawH + rawO + rawN;

            var C = rawC / sum * 100;
            var H = rawH / sum * 100;
            var O = rawO / sum * 100;
            var N = rawN / sum * 100;

            //CHaObNc + dH2O + e(O2+3.76N2) -> n1C + n2H + n3CO + n4H2O + n5CO2 + n6CH4 + n7N2
            // // biomass formula
            // // hydrogen/carbon
            var a = H / C;
            // // oxygen/carbon
            var b = O / C;
            // // nitrogen/carbon
            var c = N / C;
            // H2O
            var d = h2o;
            // e(O2 + 3.76N2)
            var e = air;


            // simulation range
            // temperature
            //var TStart = 100;
            //var TEnd = 2000;
            //var TStep = 50;
            //// pressure
            //var PStart = 0;
            //var PEnd = 2;
            //var PStep = 0.5;
            //// steam
            //var SStart = 0;
            //var SEnd = 2;
            //var SStep = 1;
            //// Air
            //var AStart = 0;
            //var AEnd = 2;
            //var AStep = 1;

            double P = 0;
            double R = 8.3145e-3; //kJ/mol K
            double T = (temperature + 273.15); //K
            T = 2000; //K

            // // 202

            // // Carbon Reactions
            // // Boudouard
            // // R1: CO2 + C -> 2CO
            var hR1 = ComputeHeatOfReaction([PPD["CO2"].DELHF, PPD["C"].DELHF, PPD["CO"].DELHF], [-1, -1, 2]);
            var gfR1 = ComputeGibbs([PPD["CO2"].DELGF, PPD["C"].DELGF, PPD["CO"].DELGF], [-1, -1, 2]);
            var sfR1 = ComputeEntropy([PPD["CO2"].DELSF, PPD["C"].DELSF, PPD["CO"].DELSF], [-1, -1, 2]);
            var gfR1_2 = hR1 - sfR1 * T;
            var gfR1_4 = ComputeGibbs([PPD["CO2"].Gibbs(T), PPD["C"].Gibbs(T), PPD["CO"].Gibbs(T)], [-1, -1, 2]);

            // water-gas or steam
            // R2: C + H2O -> CO + H2
            // log10(Ke)=-2.4198 + 0.0003855T + 2180.6/T
            var hR2 = ComputeHeatOfReaction([PPD["C"].DELHF, PPD["H2O"].DELHF, PPD["H2"].DELHF, PPD["CO"].DELHF], [-1, -1, 1, 1]);
            var gfR2 = ComputeGibbs([PPD["C"].DELGF, PPD["H2O"].DELGF, PPD["H2"].DELGF, PPD["CO"].DELGF], [-1, -1, 1, 1]);
            var gfR2_4 = ComputeGibbs([PPD["C"].Gibbs(T), PPD["H2O"].Gibbs(T), PPD["H2"].Gibbs(T), PPD["CO"].Gibbs(T)], [-1, -1, 1, 1]);
            var sfR2 = ComputeEntropy([PPD["C"].DELSF, PPD["H2O"].DELSF, PPD["H2"].DELSF, PPD["CO"].DELSF], [-1, -1, 1, 1]);
            var gfR2_2 = hR2 - sfR2 * T;

            // hydrogasification
            // R3: C + 2H2 -> CH4
            var hR3 = ComputeHeatOfReaction([PPD["C"].DELHF, PPD["H2"].DELHF, PPD["CH4"].DELHF], [-1, -2, 1]);
            var gfR3 = ComputeGibbs([PPD["C"].DELGF, PPD["H2"].DELGF, PPD["CH4"].DELGF], [-1, -2, 1]);
            var gfR3_4 = ComputeGibbs([PPD["C"].Gibbs(T), PPD["H2"].Gibbs(T), PPD["CH4"].Gibbs(T)], [-1, -2, 1]);
            var sfR3 = ComputeEntropy([PPD["C"].DELSF, PPD["H2"].DELSF, PPD["CH4"].DELSF], [-1, -2, 1]);
            var gfR3_2 = hR3 - sfR3 * T;

            // R3_1: 2C + 2H2O -> CO2 + CH4
            // log10(Ke) = 0.9/2700(1/T) - 0.9
            var hR3_1 = ComputeHeatOfReaction([PPD["C"].DELHF, PPD["H2O"].DELHF, PPD["CO2"].DELHF, PPD["CH4"].DELHF], [-2, -2, 1, 1]);
            var gfR3_1 = ComputeGibbs([PPD["C"].DELGF, PPD["H2O"].DELGF, PPD["CO2"].DELGF, PPD["CH4"].DELGF], [-2, -2, 1, 1]);
            var gfR3_1_4 = ComputeGibbs([PPD["C"].Gibbs(T), PPD["H2O"].Gibbs(T), PPD["CO2"].Gibbs(T), PPD["CH4"].Gibbs(T)], [-2, -2, 1, 1]);
            var sfR3_1 = ComputeEntropy([PPD["C"].DELSF, PPD["H2O"].DELSF, PPD["CO2"].DELSF, PPD["CH4"].DELSF], [-2, -2, 1, 1]);
            var gfR3_1_2 = hR3_1 - sfR3_1 * T;

            // R4: C + 0.5 O2 -> CO
            var hR4 = ComputeHeatOfReaction([PPD["C"].DELHF, PPD["O2"].DELHF, PPD["CO"].DELHF], [-1, -.5, 1]);
            var gfR4 = ComputeGibbs([PPD["C"].DELGF, PPD["O2"].DELGF, PPD["CO"].DELGF], [-1, -.5, 1]);
            var gfR4_4 = ComputeGibbs([PPD["C"].Gibbs(T), PPD["O2"].Gibbs(T), PPD["CO"].Gibbs(T)], [-1, -.5, 1]);
            var sfR4 = ComputeEntropy([PPD["C"].DELSF, PPD["O2"].DELSF, PPD["CO"].DELSF], [-1, -.5, 1]);
            var gfR4_2 = hR4 - sfR4 * T;

            // Oxidation Reactions
            // R5: C + O2 -> CO2
            var hR5 = ComputeHeatOfReaction([PPD["C"].DELHF, PPD["O2"].DELHF, PPD["CO2"].DELHF], [-1, -1, 1]);
            var gfR5 = ComputeGibbs([PPD["C"].DELGF, PPD["O2"].DELGF, PPD["CO2"].DELGF], [-1, -1, 1]);
            var gfR5_4 = ComputeGibbs([PPD["C"].Gibbs(T), PPD["O2"].Gibbs(T), PPD["CO2"].Gibbs(T)], [-1, -1, 1]);
            var sfR5 = ComputeEntropy([PPD["C"].DELSF, PPD["O2"].DELSF, PPD["CO2"].DELSF], [-1, -1, 1]);
            var gfR5_2 = hR5 - sfR5 * T;

            // R6: CO + 0.5 O2 -> CO2
            var hR6 = ComputeHeatOfReaction([PPD["CO"].DELHF, PPD["O2"].DELHF, PPD["CO2"].DELHF], [-1, -.5, 1]);
            var gfR6_4 = ComputeGibbs([PPD["CO"].Gibbs(T), PPD["O2"].Gibbs(T), PPD["CO2"].Gibbs(T)], [-1, -.5, 1]);
            var sfR6 = ComputeEntropy([PPD["CO"].DELSF, PPD["O2"].DELSF, PPD["CO2"].DELSF], [-1, -.5, 1]);
            var gfR6_2 = hR6 - sfR6 * T;

            // R7: CH4 + 2O2 -> CO2 + 2H2O
            var hR7 = ComputeHeatOfReaction([PPD["CH4"].DELHF, PPD["O2"].DELHF, PPD["CO2"].DELHF, PPD["H2O"].DELHF], [-1, -2, 1, 2]);
            var gfR7_4 = ComputeGibbs([PPD["CH4"].Gibbs(T), PPD["O2"].Gibbs(T), PPD["CO2"].Gibbs(T), PPD["H2O"].Gibbs(T)], [-1, -2, 1, 2]);
            var sfR7 = ComputeEntropy([PPD["CH4"].DELSF, PPD["O2"].DELSF, PPD["CO2"].DELSF, PPD["H2O"].DELSF], [-1, -2, 1, 2]);
            var gfR7_2 = hR7 - sfR7 * T;

            // R8: H2 + 0.5O2 -> H2O
            var hR8 = ComputeHeatOfReaction([PPD["H2"].DELHF, PPD["O2"].DELHF, PPD["H2O"].DELHF], [-1, -.5, 1]);
            var gfR8_4 = ComputeGibbs([PPD["H2"].Gibbs(T), PPD["O2"].Gibbs(T), PPD["H2O"].Gibbs(T)], [-1, -.5, 1]);
            var sfR8 = ComputeEntropy([PPD["H2"].DELSF, PPD["O2"].DELSF, PPD["H2O"].DELSF], [-1, -.5, 1]);
            var gfR8_2 = hR8 - sfR8 * T;

            // Gas Shift Reactions
            // R9: CO + H2O -> CO2 + H2
            var hR9 = ComputeHeatOfReaction([PPD["CO"].DELHF, PPD["H2O"].DELHF, PPD["CO2"].DELHF, PPD["H2"].DELHF], [-1, -1, 1, 1]);
            var gfR9 = ComputeGibbs([PPD["CO"].DELGF, PPD["H2O"].DELGF, PPD["CO2"].DELGF, PPD["H2"].DELGF], [-1, -1, 1, 1]);
            var gfR9_4 = ComputeGibbs([PPD["CO"].Gibbs(T), PPD["H2O"].Gibbs(T), PPD["CO2"].Gibbs(T), PPD["H2"].Gibbs(T)], [-1, -1, 1, 1]);
            var sfR9 = ComputeEntropy([PPD["CO"].DELSF, PPD["H2O"].DELSF, PPD["CO2"].DELSF, PPD["H2"].DELSF], [-1, -1, 1, 1]);
            var gfR9_2 = hR9 - sfR9 * T;

            // Methanation Reactions
            // R10: 2CO + 2H2 -> CH4 + CO2
            var hR10 = ComputeHeatOfReaction([PPD["CO"].DELHF, PPD["H2"].DELHF, PPD["CH4"].DELHF, PPD["CO2"].DELHF], [-2, -2, 1, 1]);
            var gfR10_4 = ComputeGibbs([PPD["CO"].Gibbs(T), PPD["H2"].Gibbs(T), PPD["CH4"].Gibbs(T), PPD["CO2"].Gibbs(T)], [-2, -2, 1, 1]);
            var sfR10 = ComputeEntropy([PPD["CO"].DELSF, PPD["H2"].DELSF, PPD["CH4"].DELSF, PPD["CO2"].DELSF], [-2, -2, 1, 1]);
            var gfR10_2 = hR10 - sfR10 * T;

            // R11: CO + 3H2 -> CH4 + H2O
            var hR11 = ComputeHeatOfReaction([PPD["CO"].DELHF, PPD["H2"].DELHF, PPD["CH4"].DELHF, PPD["H2O"].DELHF], [-1, -3, 1, 1]);
            var gfR11_4 = ComputeGibbs([PPD["CO"].Gibbs(T), PPD["H2"].Gibbs(T), PPD["CH4"].Gibbs(T), PPD["H2O"].Gibbs(T)], [-1, -3, 1, 1]);
            var sfR11 = ComputeEntropy([PPD["CO"].DELSF, PPD["H2"].DELSF, PPD["CH4"].DELSF, PPD["H2O"].DELSF], [-1, -3, 1, 1]);
            var gfR11_2 = hR11 - sfR11 * T;

            // R14: CO2 + 4H2 -> CH4 + 2H2O
            var hR14 = ComputeHeatOfReaction([PPD["CO"].DELHF, PPD["H2"].DELHF, PPD["CH4"].DELHF, PPD["H2O"].DELHF], [-1, -4, 1, 2]);
            var gfR14_4 = ComputeGibbs([PPD["CO"].Gibbs(T), PPD["H2"].Gibbs(T), PPD["CH4"].Gibbs(T), PPD["H2O"].Gibbs(T)], [-1, -4, 1, 2]);
            var sfR14 = ComputeEntropy([PPD["CO"].DELSF, PPD["H2"].DELSF, PPD["CH4"].DELSF, PPD["H2O"].DELSF], [-1, -4, 1, 2]);
            var gfR14_2 = hR14 - sfR14 * T;

            // Steam-Reforming Reactions
            // R12: CH4 + H2O -> CO + 3H2
            var hR12 = ComputeHeatOfReaction([PPD["CH4"].DELHF, PPD["H2O"].DELHF, PPD["CO"].DELHF, PPD["H2"].DELHF], [-1, -1, 1, 3]);
            var gfR12_4 = ComputeGibbs([PPD["CH4"].Gibbs(T), PPD["H2O"].Gibbs(T), PPD["CO"].Gibbs(T), PPD["H2"].Gibbs(T)], [-1, -1, 1, 3]);
            var sfR12 = ComputeEntropy([PPD["CH4"].DELSF, PPD["H2O"].DELSF, PPD["CO"].DELSF, PPD["H2"].DELSF], [-1, -1, 1, 3]);
            var gfR12_2 = hR12 - sfR12 * T;

            // R13: CH4 + 0.5O2 -> CO + 2H2
            var hR13 = ComputeHeatOfReaction([PPD["CH4"].DELHF, PPD["O2"].DELHF, PPD["CO"].DELHF, PPD["H2"].DELHF], [-1, -.5, 1, 2]);
            var gfR13_4 = ComputeGibbs([PPD["CH4"].Gibbs(T), PPD["O2"].Gibbs(T), PPD["CO"].Gibbs(T), PPD["H2"].Gibbs(T)], [-1, -.5, 1, 2]);
            var sfR13 = ComputeEntropy([PPD["CH4"].DELSF, PPD["O2"].DELSF, PPD["CO"].DELSF, PPD["H2"].DELSF], [-1, -.5, 1, 2]);
            var gfR13_2 = hR13 - sfR13 * T;

            // Split Reactions
            // R15: CO2 -> CO + 0.5O2
            var hR15 = ComputeHeatOfReaction([PPD["CO2"].DELHF, PPD["CO"].DELHF, PPD["O2"].DELHF], [-1, 1, 0.5]);
            var gfR15 = ComputeGibbs([PPD["CO2"].DELGF, PPD["CO"].DELGF, PPD["O2"].DELGF], [-1, 1, 0.5]);
            var gfR15_4 = ComputeGibbs([PPD["CO2"].Gibbs(T), PPD["CO"].Gibbs(T), PPD["O2"].Gibbs(T)], [-1, 1, 0.5]);
            var sfR15 = ComputeEntropy([PPD["CO2"].DELSF, PPD["CO"].DELSF, PPD["O2"].DELSF], [-1, 1, 0.5]);

            var gfR15_2 = hR15 - sfR15 * T;


            double ke1 = EquilibrumConstant(gfR1_4, R, T);
            double ke2 = EquilibrumConstant(gfR2_4, R, T);
            double ke3 = EquilibrumConstant(gfR3_4, R, T);
            double ke3_1 = EquilibrumConstant(gfR3_1_4, R, T);

            //ke1 = Math.Pow(10, (9141/T + 0.000224 * T - 9.595));
            //ke2 = Math.Pow(10, (-2.4198 + 0.0003855 * T + 2180.6 / T));
            //ke3 = Math.Pow(10, ((0.9 / 2700)*(1 / T) - 0.9));

            Func<double[], double>[] F =
            [
            (n) => n[0] + n[2] + n[4] + n[5] - 1,
            (n) => 2*n[1] + 2*n[3] + 4*n[5] - a - 2*d,
            (n) => n[2] + n[3] + 2*n[4] - b - d - 2*e,
            (n) => n[6] - c - 7.52*e,
            (n) => n[1] + n[2] + n[3] + n[4] + n[5] + n[6] - n[7],
            (n) => n[4]*n[7]*ke1 - (n[2]*n[2])*P,
            (n) => n[3]*n[7]*ke2 - n[2]*n[1]*P,
            //(n) => (n[1]*n[1])*ke3*P - n[5]*n[7],
            (n) => (n[3]*n[3])*ke3*P - n[4]*n[5],
            ];

            Func<double[], double>[,] J =
            {
            { (n)=>1, (n)=>0, (n)=>1, (n)=>0, (n)=>1, (n)=>1, (n)=>0, (n)=>0 },
            { (n)=>0, (n)=>2, (n)=>0, (n)=>2, (n)=>0, (n)=>4, (n)=>0, (n)=>0 },
            { (n)=>0, (n)=>0, (n)=>1, (n)=>1, (n)=>2, (n)=>0, (n)=>0, (n)=>0 },
            { (n)=>0, (n)=>0, (n)=>0, (n)=>0, (n)=>0, (n)=>0, (n)=>1, (n)=>0 },
            { (n)=>0, (n)=>1, (n)=>1, (n)=>1, (n)=>1, (n)=>1, (n)=>1, (n)=>-1 },
            { (n)=>0, (n)=>0, (n)=>-2*n[2]*P, (n)=>0, (n)=>n[7]*ke1, (n)=>0, (n)=>0, (n)=>0 },
            { (n)=>0, (n)=>-n[2]*P, (n)=>-n[1]*P, (n)=>n[7]*ke2, (n)=>0, (n)=>0, (n)=>0, (n)=>0 },
            //{ (n)=>0, (n)=>2*n[1]*ke3*P, (n)=>0, (n)=>0, (n)=>0, (n)=>-n[7], (n)=>0, (n)=>-n[5] },
            { (n)=>0, (n)=>0, (n)=>0, (n)=>2*n[3]*ke3_1*P, (n)=>-n[5], (n)=>-n[4], (n)=>0, (n)=>0 },
            };

            var errorLimit = 0.001;
            var numIteration = 100;
            double[] x = Goke.Maths.NonLinearSystems.Newton(F, J, errorLimit, numIteration);

            // answer x[i]
            return new GasificResult(1, a, b, c, d, e, x);
        }

        public static double Ke(double G, double T, double R)
        {
            return Math.Exp(-G / R / T);
        }

        public static double ComputeGibbs(double[] G, double[] n)
        {
            var sum = 0.0;
            for (var i = 0; i < G.Length; i++)
            {
                sum += G[i] * n[i];
            }
            return sum;
        }

        public static double ComputeGibbs2(double H, double S, double T)
        {
            return H - T * S;
        }

        public static double ComputeEntropy(double[] S, double[] n)
        {
            var sum = 0.0;
            for (var i = 0; i < S.Length; i++)
            {
                sum += S[i] * n[i];
            }
            return sum/1000.0;
        }

        public static double ComputeHeatOfReaction(double[] H, double[] n)
        {
            var sum = 0.0;
            for (var i = 0; i < H.Length; i++)
            {
                sum += H[i] * n[i];
            }
            return sum;
        }

        public static double EquilibrumConstant(double G, double R, double T)
        {
            return Math.Exp(-G / R / T);
        }

        public record PhysicaPropertyData(int Id, string? Formula, string? CompoundName,
            double MOLWT, double TFP, double TBP, double TC, double PC, double VC,
            double LDEN, double TDEN, double HVAP, double VISA, double VISB, 
            double DELHF, double DELGF,
            double CPVAPA, double CPVAPB, double CPVAPC, double CPVAPD,
            double ANTA, double ANTB, double ANTC, double TMN, double TMX,
            double DELSF,
            double A, double B, double C, double D, double E, double F, double G)
        {

            public double SpecificHeatCapacity(double T) => CPVAPA + CPVAPB * T + CPVAPC * T * T + CPVAPD * T * T * T;

            public double SpecificEnthalpy(double T2, double T1)
            {
                var A = CPVAPA * (T2 - T1);
                var B = CPVAPB * (T2 * T2 - T1 * T1) / 2.0;
                var C = CPVAPC * (T2 * T2 * T2 - T1 * T1 * T1) / 3.0;
                var D = CPVAPD * (Math.Pow(T2,4.0) - Math.Pow(T1,4.0)) / 4.0;
                return A + B + C + D;
            }

            public double SpecificEntropy(double T2, double T1, double R= 8.3145e-3)
            {
                var A = CPVAPA * (Math.Log(T2) - Math.Log(T1));
                var B = CPVAPB * (T2 - T1);
                var C = CPVAPC * (T2 * T2 - T1 * T1) / 2.0;
                var D = CPVAPD * (T2 * T2 * T2 - T1 * T1 * T1) / 3.0;
                return (A + B + C + D) - R* (Math.Log(T2) - Math.Log(T1));
            }

            public double Gibbs(double T) => DELHF - A * T * Math.Log(T) - B * Math.Pow(T, 2) - (C / 2) * Math.Pow(T, 3) - (D / 3) * Math.Pow(T, 4) + (E / 2 / T) + F + G * T;
            
            public double HeatOfFormation(double T) => DELHF - A * T + B * Math.Pow(T, 2) + C * Math.Pow(T, 3) + D * Math.Pow(T, 4) + (E / T) + F;


        }


        public class GasificCompound
        {
            public string? Formula { get; set; }
            public string? Name { get; set; }
            public double MolarMass { get; set; }
            public double Inlet { get; set; }
            public double Outlet { get; set; }
            public double InletWeight => Inlet*MolarMass;
            public double OutletWeight => Outlet * MolarMass;



        }

        public class GasificResult
        {
            public GasificResult(double inletC, double a, double b, double c, double d, double e, double[] outlet)
            {
                Components = new()
                {
                    new GasificCompound{Formula=$"CH{a:0.00}O{b:0.00}N{c:0.00}", Name="Biomass",MolarMass=12.0107+a*1.008+b*16+c*14, Inlet=1},
                    new GasificCompound{Formula="H2O", Name="Water",MolarMass=18.015, Inlet=d, Outlet=outlet[3]},
                    new GasificCompound{Formula="O2", Name="Oxygen",MolarMass=31.999, Inlet=2*e},
                    new GasificCompound{Formula="N2", Name="Nitrogen",MolarMass=28.013,Inlet=3.76*e, Outlet=outlet[6]},
                    new GasificCompound{Formula="C", Name="Carbon",MolarMass=12.0107, Inlet=0, Outlet=outlet[0]},
                    new GasificCompound{Formula="H2", Name="Hydrogen",MolarMass=2.016,Inlet=0, Outlet=outlet[1]},
                    new GasificCompound{Formula="CO", Name="Carbon-monoxide",MolarMass=28.01, Outlet=outlet[2]},
                    new GasificCompound{Formula="CO2", Name="Carbon-dioxide",MolarMass=44.01, Outlet=outlet[4]},
                    new GasificCompound{Formula="CH4", Name="Methane",MolarMass=16.043, Outlet=outlet[5]},
                };                
            }

            public List<GasificCompound>? Components { get; }


        }

    }
}
