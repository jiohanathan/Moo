using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using JMetalCSharp.Core;
using JMetalCSharp.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NSGASolution_WORKS1
{
    class NSGASolutionComponentAttributes : Grasshopper.Kernel.Attributes.GH_ComponentAttributes
    {

        NSGASolutionComponent MyComponent;

        // Variables
        List<GH_NumberSlider> variablesSliders = new List<GH_NumberSlider>();
        int solutionsCounter; // Count designs evaluated
        public NSGASolutionComponentAttributes(IGH_Component component)
            : base(component)
        {
            MyComponent = (NSGASolutionComponent)component;
        }


        [STAThread]
        public override Grasshopper.GUI.Canvas.GH_ObjectResponse RespondToMouseDoubleClick(Grasshopper.GUI.Canvas.GH_Canvas sender, Grasshopper.GUI.GH_CanvasMouseEvent e)
        {
            solutionsCounter = 0;
            variablesSliders = MyComponent.readSlidersList();
            NSGAIIProblem problem = new NSGAIIProblem("ArrayReal", MyComponent, solutionsCounter);
            NSGAIIRunner runner = new NSGAIIRunner(null, problem, null, MyComponent);




            //GH_NumberSlider currentSlider = null;
            ////Grasshopper.Instances.ActiveCanvas.Document.ScheduleSolution(1, (GH_Document doc) =>
            ////{
            //if (variablesSliders.Count != 0)
            //{
            //    for (int j = 0; j < 5; j++)
            //    {
            //        for (int i = 0; i < variablesSliders.Count; i++)
            //        {
            //            currentSlider = variablesSliders[i];
            //            currentSlider.SetSliderValue((decimal) i * j * 4);
            //        }
            //        Grasshopper.Instances.ActiveCanvas.Document.NewSolution(true);
            //        MessageBox.Show("Objective" + MyComponent.objectives[0]);
            //    }
            //}
            //else
            //{
            //    MessageBox.Show("No inputs detected");
            //}

            //});
            //MyComponent.DesignView.InitialDesign = new DesignVM(MyComponent.DesignLines, false, true, MyComponent.Score, MyComponent.Design);
            //Window w = new StormCloudWindow(MyComponent.DesignView, MyComponent);
            //w.Show();
            return base.RespondToMouseDoubleClick(sender, e);
        }
    }
}
