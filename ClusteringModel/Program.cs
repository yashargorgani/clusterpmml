using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Windows.Forms;
using PMML;

namespace ClusteringModel
{
    class Program
    {
        static void Main(string[] args)
        {
            ClusterModel model = new ClusterModel("E:/IT/PMML/Clustering/Model Files/K-Means.xml");

            Dictionary<string, object> input = new Dictionary<string, object>();
            input.Add("value", 42.712);
            input.Add("pmethod", "CHEQUE");
            input.Add("sex", "M");
            input.Add("homeown", "NO");
            input.Add("income", 27000);
            input.Add("age", 46);
            
            Dictionary<string, double> cluster = new Dictionary<string, double>();
            cluster = model.DetectCluster(input);
        }
    }
}

