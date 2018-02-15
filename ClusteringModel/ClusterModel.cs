using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using PMML;

namespace ClusteringModel
{
    class ClusterModel
    {
        List<Cluster> clusterList = new List<Cluster>();
        Pmml doc;
        string kind; //Comparison Measure Kind, Can be Distance or Similarity
        string cMeasure; //Comparison Measure Name

        public ClusterModel(string dir)
        {
            doc = new Pmml(dir);
            NodeList ClusteringModel = doc.getList("ClusteringModel");

            for (int i = 0; i < ClusteringModel.getNode(0).ChildCount("Cluster"); i++)
            {
                clusterList.Add(new Cluster(ClusteringModel.getNode(0).Child(i, "Cluster")));
            }

            NodeList ComparisonMeasure = doc.getList("ComparisonMeasure");
            kind = ComparisonMeasure.getNode(0).Attribute("kind");
            cMeasure = ComparisonMeasure.getNode(0).Child(0).Name();
        }


        public Dictionary<string, double> DetectCluster(Dictionary<string, object> inputDic)
        {
            Dictionary<string, double> clusters = new Dictionary<string, double>();

            Fields newFields = new Fields(inputDic, doc);
            double[] newData = newFields.derivedFields;

            for (int i = 0; i < clusterList.Count; i++)
            {
                double[] dist = Distance(newData, clusterList[i].clusterCenter, newFields.compareFunction);
                clusters.Add(clusterList[i].clusterName, Comparison(dist, cMeasure));
                Console.WriteLine(clusterList[i].clusterName);
                Console.WriteLine(Comparison(dist, cMeasure));
            }

            return clusters;
        }

        double[] Distance(double[] newData, double[] cCenter, string[] cFName)
        {
            double[] dist = new double[newData.GetLength(0)];
            for (int i = 0; i < newData.GetLength(0); i++)
            {
                switch (cFName[i])
                {
                    case "absDiff":
                        {
                            dist[i] = Math.Abs(newData[i] - cCenter[i]);
                        }
                        break;
                }
            }

            return dist;
        }

        double Comparison(double[] dist, string method)
        {
            double cmpr = 0;

            switch (method)
            {
                case "euclidean":
                    {
                        double sum = 0;
                        for (int i = 0; i < dist.GetLength(0); i++)
                        {
                            sum = sum + Math.Pow(dist[i], 2);
                        }
                        cmpr = Math.Sqrt(sum);
                    }
                    break;
                case "squaredEuclidean":
                    {
                        double sum = 0;
                        for (int i = 0; i < dist.GetLength(0); i++)
                        {
                            sum = sum + Math.Pow(dist[i], 2);
                        }
                        cmpr = sum;
                    }
                    break;
            }

            return cmpr;
        }
    }
}
