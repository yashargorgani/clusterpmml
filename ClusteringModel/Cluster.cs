using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using PMML;

namespace ClusteringModel
{
    class Cluster
    {
        public double[] clusterCenter;
        public string clusterName;

        public Cluster(Node ClusterNode)
        {
            clusterName = ClusterNode.Attribute("name");
            string arrayValue = null;

                    arrayValue = ClusterNode.Child(0, "Array").Value();
            
            string[] tmp = arrayValue.Split(' ');
            clusterCenter = new double[tmp.GetLength(0)];

            for (int i = 0; i < tmp.GetLength(0); i++)
            {
                clusterCenter[i] = Convert.ToDouble(tmp[i]);
            }
        }
    }
}
