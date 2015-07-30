using JMetalCSharp.Core;
using JMetalCSharp.Encoding.SolutionType;
using JMetalCSharp.Utils;
using JMetalCSharp.Utils.Wrapper;
using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using System.Windows.Forms;


namespace NSGASolution_WORKS1
{
    /// <summary>
    /// 
    /// </summary>
    public class NSGAIIProblem : Problem
    {
        NSGASolutionComponent component = null;
        List<GH_NumberSlider> variablesSliders = new List<GH_NumberSlider>();
        
        #region Constructors

        /// <summary>
        /// Constructor.
        /// Creates a new multiobjective problem instance.
        /// </summary>
        /// <param name="solutionType">The solution type must "Real" or "BinaryReal", and "ArrayReal".</param>
        /// <param name="numberOfVariables">Number of variables</param>
        public NSGAIIProblem(string solutionType, NSGASolutionComponent comp, int solutionsCounter)
        {
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
            double[] storeVar = new double[NumberOfVariables];
            double[] storeObj = new double[NumberOfObjectives];
            XReal x = new XReal(solution);

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
            }

        }

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
