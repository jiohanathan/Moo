using JMetalCSharp.Core;
using JMetalCSharp.Encoding.SolutionType;
using JMetalCSharp.Utils;
using JMetalCSharp.Utils.Wrapper;
using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using System.Windows.Forms;
using System.IO;


namespace NSGASolution_WORKS1
{
    /// <summary>
    /// 
    /// </summary>
    public class NSGAIIProblem : Problem
    {
        double[] storeDesign;
        List<double[]> design = new List<double[]>();
        NSGASolutionComponent component = null;
        List<GH_NumberSlider> variablesSliders = new List<GH_NumberSlider>();
        List<double> objectives = new List<double>();
        int solutionsCounter;
        //StreamWriter file = new StreamWriter(@"C:\Moo\LogFile.txt");
            
        
        #region Constructors

        /// <summary>
        /// Constructor.
        /// Creates a new multiobjective problem instance.
        /// </summary>
        /// <param name="solutionType">The solution type must "Real" or "BinaryReal", and "ArrayReal".</param>
        /// <param name="numberOfVariables">Number of variables</param>
        public NSGAIIProblem(string solutionType, NSGASolutionComponent comp, int solutionsCounter)
        {
            this.solutionsCounter = solutionsCounter;
            this.component = comp;
            NumberOfVariables = comp.readSlidersList().Count;
            NumberOfObjectives = comp.objectives.Count;
            NumberOfConstraints = 0;
            ProblemName = "Multiobjective";
           
            UpperLimit = new double[NumberOfVariables];
            LowerLimit = new double[NumberOfVariables];

            for (int i = 0; i < NumberOfVariables; i++)
            {
                GH_NumberSlider curSlider = comp.readSlidersList()[i];

                LowerLimit[i] = (double)curSlider.Slider.Minimum;
                UpperLimit[i] = (double)curSlider.Slider.Maximum;
            }

            if (solutionType == "BinaryReal")
            {
                SolutionType = new BinaryRealSolutionType(this);
            }
            else if (solutionType == "Real")
            {
                SolutionType = new RealSolutionType(this);
            }
            else if (solutionType == "ArrayReal")
            {
                SolutionType = new ArrayRealSolutionType(this);
            }
            else
            {
                Console.WriteLine("Error: solution type " + solutionType + " is invalid");
                //Logger.Log.Error("Solution type " + solutionType + " is invalid");
                return;
            }

        }

        #endregion

        public override void Evaluate(Solution solution)
        {
            XReal x = new XReal(solution);
            storeDesign = new double[NumberOfVariables + NumberOfObjectives];

            // Reading x values
            double[] xValues = new double[NumberOfVariables];
            for (int i = 0; i < NumberOfVariables; i++)
            {
                xValues[i] = x.GetValue(i);
                storeDesign[i] = x.GetValue(i);
            }

            GH_NumberSlider currentSlider = null;
            for (int i = 0; i < component.readSlidersList().Count; i++)
            {
                currentSlider = component.readSlidersList()[i];
                currentSlider.SetSliderValue((decimal)x.GetValue(i));
                Grasshopper.Instances.ActiveCanvas.Document.NewSolution(true);
            }


            for (int i = 0; i < component.objectives.Count; i++)
            {
                solution.Objective[i] = component.objectives[i];
                storeDesign[NumberOfVariables + i] = component.objectives[i];
            }
            design.Add(storeDesign);

        }

        //public void PrintDesign()
        //{
        //    //file = new StreamWriter(@"" + component.outputPath + "allSolutions" + component.fileName);

        //    for (int j = 0; j < design.Count; j++)
        //    {
        //        double[] data = design[j];
        //        string line = "";
        //        for (int i = 0; i < (NumberOfVariables + NumberOfObjectives); i++)
        //        {
        //            line = line + data[i] + ", ";
        //        }
        //        file.WriteLine(line);
        //    }
        //    file.Close();
        //}



        #region Private Region

        /// <summary>
        /// 
        /// </summary>
        /// <param name="x"></param>
        /// <returns></returns>
        public double EvalG(XReal x)
        {
            double g = 0.0;
            for (int i = 1; i < x.GetNumberOfDecisionVariables(); i++)
            {
                g += x.GetValue(i);
            }
            double constant = (9.0 / (NumberOfVariables - 1));
            g = constant * g;
            g = g + 1.0;
            return g;
        }

        public double EvalH(double f, double g)
        {
            double h = 0.0;
            h = 1.0 - Math.Sqrt(f / g) - (f / g) * Math.Sin(10.0 * Math.PI * f);
            return h;
        }

        #endregion


    }
}
