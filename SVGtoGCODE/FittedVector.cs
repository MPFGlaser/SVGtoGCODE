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
        // Should hold the info for the instance of Vector which has been fitted in a specific printing/work space
        public FittedVector(XmlDocument vector)
        {
            GetDocumentSize(vector);
        }

        private void GetDocumentSize(XmlDocument vector)
        {
            // This works!
            // https://stackoverflow.com/questions/933687/read-xml-attribute-using-xmldocument
            XmlNodeList elemList = vector.GetElementsByTagName("svg");
            for (int i = 0; i < elemList.Count; i++)
            {
                string attrVal = elemList[i].Attributes["height"].Value;
                string height = attrVal;
            }
        }
    }
}