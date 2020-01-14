using Svg;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace SVGtoGCODE
{
    // inherit from vector
    public class FittedVector : Vector
    {
        private int originalHeight;
        private int originalWidth;
        private int fittedHeight;
        private int fittedWidth;
        private double scalingMultiplier;

        // Should hold the info for the instance of Vector which has been fitted in a specific printing/work space
        public FittedVector(XmlDocument vector)
        {
            Workspace workspace = new Workspace();
            fittedHeight = workspace.sizeY();
            fittedWidth = workspace.sizeX();
            GetDocumentSize(vector);
        }

        private void GetDocumentSize(XmlDocument vector)
        {
            // This works!
            // https://stackoverflow.com/questions/933687/read-xml-attribute-using-xmldocument

            // Reads <svg> tag from xml file, then saves the height and width attributes.
            XmlNodeList elemList = vector.GetElementsByTagName("svg");
            for (int i = 0; i < elemList.Count; i++)
            {
                originalHeight = int.Parse(elemList[i].Attributes["height"].Value);
                originalWidth = int.Parse(elemList[i].Attributes["width"].Value);
            }

            // Calculates scaling multiplier
            scalingMultiplier = (double)fittedHeight / (double)originalHeight;
            scalingMultiplier = Math.Floor(scalingMultiplier * 100d) / 100d;

        }
    }
}