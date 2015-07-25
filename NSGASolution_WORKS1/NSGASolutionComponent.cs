using System;
using System.Collections.Generic;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using Rhino.Geometry;
using NSGASolution_WORKS1;

namespace NSGASolution_WORKS1
{
    public class NSGASolutionComponent : GH_Component
    {
        public List<double> objectives;
        public List<GH_NumberSlider> slidersList = new List<GH_NumberSlider>();
        public int popSize = 0, maxIterations = 0; 
        public string outputPath = null, fileName = null;
        public string allSolutions = null;

        /// <summary>
        /// Each implementation of GH_Component must provide a public 
        /// constructor without any arguments.
        /// Category represents the Tab in which the component will appear, 
        /// Subcategory the panel. If you use non-existing tab or panel names, 
        /// new tabs/panels will automatically be created.
        /// </summary>
        public NSGASolutionComponent()
            : base("NSGAII", "NSGAII",
                "Construct an Archimedean, or arithmetic, spiral given its radii and number of turns.",
                "MIT", "Optimization")
        {
        }
        public override void CreateAttributes()
        {
            base.m_attributes = new NSGASolutionComponentAttributes(this);
        }

        public List<GH_NumberSlider> readSlidersList()
        {
            slidersList.Clear();

            foreach (IGH_Param param in Params.Input[0].Sources)
            {
                GH_NumberSlider slider = param as Grasshopper.Kernel.Special.GH_NumberSlider;
                slidersList.Add(slider);

            }
            return slidersList;
        }

        /// <summary>
        /// Registers all the input parameters for this component.
        /// </summary>
        protected override void RegisterInputParams(GH_Component.GH_InputParamManager pManager)
        {
            // :::: INNER PARAMETERS ::::
            pManager.AddNumberParameter("Variables", "Var", "Design Variables comes here", GH_ParamAccess.list); // Variables
            pManager.AddNumberParameter("Objectives", "Obj", "Design Objectives comes here", GH_ParamAccess.list); // Objectives
            // pManager.AddNumberParameter("Upper Limits", "UL", "Design Variables Higher Values", GH_ParamAccess.list); // Upper limits of variables
            //pManager.AddNumberParameter("Lower Limits", "LL", "Desing Variables Lower Values", GH_ParamAccess.list); // Lower Limits of variables
            pManager.AddIntegerParameter("Population Size", "Pop", "Population Size: number of solutions for each interation", GH_ParamAccess.item); // Population size
            pManager.AddIntegerParameter("Max Generation", "MaxGen", "Max number of generations: max number of set of solutions", GH_ParamAccess.item); // Max number of generations
            pManager.AddTextParameter("Output Path", "Out", "Address of output file", GH_ParamAccess.item); // Output destination
            pManager.AddTextParameter("File", "f", "File name + extension ('output.txt')", GH_ParamAccess.item); // FIle name

            // If you want to change properties of certain parameters, 
            // you can use the pManager instance to access them by index:
            //pManager[0].Optional = true;
        }

        /// <summary>
        /// Registers all the output parameters for this component.
        /// </summary>
        protected override void RegisterOutputParams(GH_Component.GH_OutputParamManager pManager)
        {
            // ::: OUTPUT PARAMETERS :::
            //pManager.AddNumberParameter("Out1", "Out1", "First output. For tests only", GH_ParamAccess.list); // Variables
            //pManager.AddNumberParameter("Out2", "Out", "Second output. For tests only", GH_ParamAccess.list); // Objectives
            //pManager.AddNumberParameter("Out3", "Out", "Second output. For tests only", GH_ParamAccess.list); // Objectives


            // Sometimes you want to hide a specific parameter from the Rhino preview.
            // You can use the HideParameter() method as a quick way:
            //pManager.HideParameter(0);
        }

        /// <summary>
        /// This is the method that actually does the work.
        /// </summary>
        /// <param name="DA">The DA object can be used to retrieve data from input parameters and 
        /// to store data in output parameters.</param>
        protected override void SolveInstance(IGH_DataAccess DA)
        {
            // How to Write a text file
            //string[] t = { c[0].ToString() };
            //System.IO.File.WriteAllLines(@"C:\Users\Jonathas\Documents\Visual Studio 2013\Projects\NSGASolution_WORKS1\NSGASolution_WORKS1\bin\text2.txt", t);

            // First, we need to retrieve all data from the input parameters.
            // We'll start by declaring variables and assigning them starting values.
            List<double> variables = new List<double>();
            objectives = new List<double>();


            // Then we need to access the input parameters individually. 
            // When data cannot be extracted from a parameter, we should abort this method.
            if (!DA.GetDataList(0, variables)) return;
            if (!DA.GetDataList(1, objectives)) return;
            if (!DA.GetData(2, ref popSize)) return;
            if (!DA.GetData(3, ref maxIterations)) return;
            if (!DA.GetData(4, ref outputPath)) return;
            if (!DA.GetData(5, ref fileName)) return;

            //this.objectives = objectives;

            //// Full path of output file
            //string fullPath = path + fileName;

            //// We should now validate the data and warn the user if invalid data is supplied.
            //if (objectives.Count < 2)
            //{
            //    AddRuntimeMessage(GH_RuntimeMessageLevel.Error, "NSGA-II is a Multiobjective optimization algorithm. Please, provide at least 2 objectives.");
            //    return;
            //}

            //// Reading Variables Sliders List
            //foreach (IGH_Param param in Params.Input[0].Sources)
            //{
            //    GH_NumberSlider slider = param as Grasshopper.Kernel.Special.GH_NumberSlider;
            //    variablesSliders.Add(slider);
            //}

            // ::: LOGIC :::



        }

        public List<double> getObjectives()
        {
            return objectives;
        }


        /// <summary>
        /// The Exposure property controls where in the panel a component icon 
        /// will appear. There are seven possible locations (primary to septenary), 
        /// each of which can be combined with the GH_Exposure.obscure flag, which 
        /// ensures the component will only be visible on panel dropdowns.
        /// </summary>
        public override GH_Exposure Exposure
        {
            get { return GH_Exposure.primary; }
        }

        /// <summary>
        /// Provides an Icon for every component that will be visible in the User Interface.
        /// Icons need to be 24x24 pixels.
        /// </summary>
        protected override System.Drawing.Bitmap Icon
        {
            get
            {
                // You can add image files to your project resources and access them like this:
                //return Resources.IconForThisComponent;
                return null;
            }
        }

        /// <summary>
        /// Each component must have a unique Guid to identify it. 
        /// It is vital this Guid doesn't change otherwise old ghx files 
        /// that use the old ID will partially fail during loading.
        /// </summary>
        public override Guid ComponentGuid
        {
            get { return new Guid("{9627d951-4b37-4e47-8229-71dfe710b903}"); }
        }


    }
}
