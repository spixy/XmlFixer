using System.IO;
using NUnit.Framework;

namespace Tests
{
    public class XmlParserTests
    {
        private string xml;

        [SetUp]
        public void Setup()
        {
            xml = File.ReadAllText(@"C:\Dev\XmlFixer\XmlParserTests\test.xml");
        }
        
        [Test]
        public void Test_IsValid()
        {
            Assert.That(XmlParser.XmlParser.IsValid(xml), Is.False);
        }
        
        [Test]
        public void Test_RepairByOne()
        {
            string repairedXml = XmlParser.XmlParser.RepairByOne(xml);

            Assert.That(repairedXml, Is.Not.Empty);
            Assert.That(XmlParser.XmlParser.IsValid(repairedXml), Is.True);
        }

        [Test]
        public void Test_RepairByBatch()
        {
            string repairedXml = XmlParser.XmlParser.RepairByBatch(xml);

            Assert.That(repairedXml, Is.Not.Empty);
            Assert.That(XmlParser.XmlParser.IsValid(repairedXml), Is.True);
        }
        
        [Test]
        public void Test_RepairConsistency()
        {
            string repairedXml1 = XmlParser.XmlParser.RepairByOne(xml);
            string repairedXml2 = XmlParser.XmlParser.RepairByBatch(xml);

            Assert.That(repairedXml1, Is.EqualTo(repairedXml2));
        }
    }
}