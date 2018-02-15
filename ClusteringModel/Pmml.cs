using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PMML
{
    class Pmml
    {
        XmlDocument doc = new XmlDocument();

        public Pmml(string dir)
        {
            doc.Load(dir);
        }

        public NodeList getList(string name)
        {
            NodeList nList = new NodeList();
            XmlNodeList xNList = doc.GetElementsByTagName(name);

            nList.xmlNodeList = xNList;
            
            return nList;
        }
    }
}
