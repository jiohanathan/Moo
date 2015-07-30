using Grasshopper.GUI.Canvas;
using Grasshopper.Kernel;
using Grasshopper.Kernel.Special;
using JMetalCSharp.Core;
using JMetalCSharp.Utils;
using System;
using System.Collections.Generic;
using System.Drawing;
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
            problem.PrintDesign();

            return base.RespondToMouseDoubleClick(sender, e);
        }

        //protected override void Render(GH_Canvas canvas, Graphics graphics, GH_CanvasChannel channel)
        //{
        //    Grasshopper.GUI.Canvas.GH_PaletteStyle styleStandard = null;
        //    Grasshopper.GUI.Canvas.GH_PaletteStyle styleSelected = null;

        //    if (channel == GH_CanvasChannel.Objects)
        //    {
        //        // Cache the current styles.
        //        styleStandard = GH_Skin.palette_normal_standard;
        //        styleSelected = GH_Skin.palette_normal_selected;
        //        GH_Skin.palette_normal_standard = new GH_PaletteStyle(Color.Maroon, Color.Black, Color.DarkRed);
        //        GH_Skin.palette_normal_selected = new GH_PaletteStyle(Color.SkyBlue, Color.DarkBlue, Color.Black);
        //    }

        //    // Allow the base class to render itself.
        //    //base.Render(canvas, graphics, channel);
        //    base.RenderComponentCapsule(canvas, graphics);

        //    if (channel == GH_CanvasChannel.Objects)
        //    {
                
        //        // Restore the cached styles.
        //        GH_Skin.palette_normal_standard = styleStandard;
        //        GH_Skin.palette_normal_selected = styleSelected;
        //    }
        //}
    }
}
