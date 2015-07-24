﻿using System;
using System.Drawing;
using Grasshopper.Kernel;

namespace NSGASolution_WORKS1
{
    public class NSGASolutionInfo : GH_AssemblyInfo
    {
        public override string Name
        {
            get
            {
                return "NSGAII";
            }
        }
        public override Bitmap Icon
        {
            get
            {
                //Return a 24x24 pixel bitmap to represent this GHA library.
                return null;
            }
        }
        public override string Description
        {
            get
            {
                //Return a short string describing the purpose of this GHA library.
                return "";
            }
        }
        public override Guid Id
        {
            get
            {
                return new Guid("672187ba-022e-48bd-aaa6-5ddb8fba64c3");
            }
        }

        public override string AuthorName
        {
            get
            {
                //Return a string identifying you or your company.
                return "Microsoft";
            }
        }
        public override string AuthorContact
        {
            get
            {
                //Return a string representing your preferred contact details.
                return "";
            }
        }
    }
}
