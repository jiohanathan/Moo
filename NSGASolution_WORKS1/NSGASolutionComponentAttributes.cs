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
            variablesSliders = MyComponent.readSlidersList();
            NSGAIIProblem problem = new NSGAIIProblem("ArrayReal", MyComponent, solutionsCounter);
            NSGAIIRunner runner = new NSGAIIRunner(null, problem, null, MyComponent);

            return base.RespondToMouseDoubleClick(sender, e);
        }

    }
}
