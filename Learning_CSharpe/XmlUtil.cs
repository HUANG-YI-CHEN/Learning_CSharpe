using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;

namespace Learning_CSharpe
{
    class XmlUtil
    {
        public XElement xElement = new XElement(
            new XElement("BookStore",
                new XElement("Book",
                    new XElement("Name", "C#入門", new XAttribute("BookName", "C#")),
                    new XElement("Author", "Martin", new XAttribute("Name", "Martin")),
                    new XElement("Adress", "上海"),
                    new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd"))
                ),
                new XElement("Book",
                    new XElement("Name", "WCF入門", new XAttribute("BookName", "WCF")),
                    new XElement("Author", "Mary", new XAttribute("Name", "Mary")),
                    new XElement("Adress", "北京"),
                    new XElement("Date", DateTime.Now.ToString("yyyy-MM-dd"))
                )
            )
        );

        public static void CreateXml(string _xmlPath, XElement _xElement)
        {
            XmlWriterSettings settings = new XmlWriterSettings();
            settings.Encoding = new UTF8Encoding(false);
            settings.Indent = true;
            XmlWriter xw = XmlWriter.Create(_xmlPath, settings);
            _xElement.Save(xw);

            xw.Flush();
            xw.Close();
        }

        public static void AddNode(string _xmlPath, string _node, string _text)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);
            
            var root = xmlDoc.DocumentElement;
            XmlNode newNode = xmlDoc.CreateNode("element", _node, "");
            newNode.InnerText = _text;

            //新增為根元素的第一層子結點
            root.AppendChild(newNode);
            xmlDoc.Save(_xmlPath);
        }

        public static void AddAttr(string _xmlPath, string _nodePath, string _node, string _nodeValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);

            var root = xmlDoc.DocumentElement;
            XmlElement node = (XmlElement)xmlDoc.SelectSingleNode(_nodePath);
            
            node.SetAttribute(_node, _nodeValue);
            xmlDoc.Save(_xmlPath);
        }

        public static void DeleteNode(string _xmlPath, string _nodePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);

            var root = xmlDoc.DocumentElement;
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(_nodePath);

            root.RemoveChild(element);
            xmlDoc.Save(_xmlPath);
        }

        public static void DeleteAttr(string _xmlPath, string _nodePath, string _node)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);

            var root = xmlDoc.DocumentElement;
            XmlElement node = (XmlElement)xmlDoc.SelectSingleNode(_nodePath);

            node.RemoveAttribute(_node);
            xmlDoc.Save(_xmlPath);
        }

        public static void ModifyNode(string _xmlPath, string _nodePath, string _text)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);

            var root = xmlDoc.DocumentElement;
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(_nodePath);

            element.InnerText = _text;
            xmlDoc.Save(_xmlPath);
        }

        public static void ModifyAttr(string _xmlPath, string _nodePath, string _node, string _nodeValue)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);

            var root = xmlDoc.DocumentElement;
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(_nodePath);

            element.SetAttribute(_node, _nodeValue);
            xmlDoc.Save(_xmlPath);
        }
        public static XmlNode GetNode(string _xmlPath, string _nodePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);

            var root = xmlDoc.DocumentElement;
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(_nodePath);
            return element;
        }
        public static XmlNodeList GetAllNode(string _xmlPath, string _nodePath)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);

            var root = xmlDoc.DocumentElement;
            XmlNodeList nodes = xmlDoc.SelectNodes(_nodePath);
            return nodes;
        }

        public static string GetyAttr(string _xmlPath, string _nodePath, string _node)
        {
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.Load(_xmlPath);

            var root = xmlDoc.DocumentElement;
            XmlElement element = (XmlElement)xmlDoc.SelectSingleNode(_nodePath);

            string name = element.GetAttribute(_node);
            return name;
        }
    }
}
