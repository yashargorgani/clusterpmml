using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using PMML;

namespace ClusteringModel
{
    class Fields
    {
        public double[] derivedFields;
        public string[] compareFunction;

        NodeList DerivedField;
        NodeList ClusteringField;

        public Fields(Dictionary<string, object> inputDic, Pmml doc)
        {
            DerivedField = doc.getList("DerivedField");
            ClusteringField = doc.getList("ClusteringField");
            derivedFields = new double[DerivedField.Size()];

            
            for (int i = 0; i < DerivedField.Size(); i++)
            {
                switch (DerivedField.getNode(i).Child(0).Name())
                {
                    case "NormContinuous":
                        {
                            object inp = inputDic[DerivedField.getNode(i).Child(0).Attribute("field")];
                            derivedFields[i] = LinearNorm(Convert.ToDouble(inp), DerivedField.getNode(i));
                        }
                        break;
                    case "MapValues":
                        {
                            object inp = inputDic[DerivedField.getNode(i).Child(0).Child(0, "FieldColumnPair").Attribute("field")];
                            derivedFields[i] = MapValue(Convert.ToString(inp), DerivedField.getNode(i).Child(0));
                        }
                        break;
                }
            }

            ClusteringField = doc.getList("ClusteringField");
            List<string> tmp = new List<string>();
            for (int i = 0; i < ClusteringField.Size(); i++)
            {
                if (ClusteringField.getNode(i).Attribute("isCenterField") != null && ClusteringField.getNode(i).Attribute("isCenterField") == "true")
                {
                    tmp.Add(ClusteringField.getNode(i).Attribute("compareFunction"));
                }
            }
            compareFunction = tmp.ToArray();
        }

        public double MapValue(string value, Node field) //field is MapValues tag
        {
            double r = Convert.ToDouble(field.Attribute("defaultValue"));

            Node inLine = field.Child(0, "InlineTable");

            for (int i = 0; i < inLine.ChildCount("row"); i++)
            {
                Node row = inLine.Child(i, "row");
                if (row.Child(0, "in").Value() == value)
                {
                    r = Convert.ToDouble(row.Child(0,"out").Value());
                    break;
                }
            }
            return r;
        }

        private double LinearNorm(double value, Node field)
        {
            Node NormContinuous = field.Child(0);

            double norm0 = double.Parse(NormContinuous.Child(0, "LinearNorm").Attribute("norm"));
            double norm1 = double.Parse(NormContinuous.Child(1, "LinearNorm").Attribute("norm"));
            double orig0 = double.Parse(NormContinuous.Child(0, "LinearNorm").Attribute("orig"));
            double orig1 = double.Parse(NormContinuous.Child(1, "LinearNorm").Attribute("orig"));

            double normValue = norm0 + (value - orig0) / (orig1 - orig0) * (norm1 - norm0);

            return normValue;
        }
    }


}
