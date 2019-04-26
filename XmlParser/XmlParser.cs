using System;
using System.Text;
using System.Xml;

namespace XmlParser
{
    public class XmlParser
    {
        /// <summary>
        /// Check XML validity
        /// </summary>
        public static bool IsValid(string xml)
        {
            try
            {
                new XmlDocument().LoadXml(xml);
                return true;
            }
            catch (XmlException)
            {
                return false;
            }
        }

        /// <summary>
        /// Repair XML
        /// </summary>
        public static string RepairByBatch(string xml) => RepairXml(xml, RemoveInvalidCharsBatched);

        /// <summary>
        /// Repair XML
        /// </summary>
        public static string RepairByOne(string xml) => RepairXml(xml, RemoveInvalidCharsByOne);


        private static string RepairXml(string xml, Func<string, string> invalidCharRepairFunc)
        {
            var doc = new XmlDocument();

            while (true)
            {
                try
                {
                    doc.LoadXml(xml);
                    return doc.OuterXml;
                }
                catch (XmlException e) when (e.Message.Contains("invalid character"))
                {
                    xml = invalidCharRepairFunc(xml);
                }
                catch (XmlException e)
                {
                    throw new Exception($"Sorry, cannot repair XML ({e.Message}).", e);
                }
            }
        }
        
        private static string RemoveInvalidCharsByOne(string xml)
        {
            var sb = new StringBuilder();
            foreach (char c in xml)
            {
                if (XmlConvert.IsXmlChar(c))
                {
                    sb.Append(c);
                }
            }
            return sb.ToString();
        }

        private static string RemoveInvalidCharsBatched(string xml)
        {
            int xmlLength = xml.Length;
            var sb = new StringBuilder();
            int from = 0;

            for (int i = 0; i < xmlLength; i++)
            {
                if (!XmlConvert.IsXmlChar(xml[i]))
                {
                    if (from < i)
                    {
                        sb.Append(xml, from, i - from);
                    }
                    from = i + 1;
                }
            }
            if (from < xmlLength)
            {
                sb.Append(xml, from, xmlLength - from);
            }

            return sb.ToString();
        }
    }
}