using System.IO;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace XmlParserBenchmarks
{
    public class Benchmarks
    {
        private string xml;
        
        [GlobalSetup]
        public void Setup()
        {
            xml = File.ReadAllText(@"C:\Dev\XmlFixer\XmlParserBenchmarks\test.xml");
        }
        
        [Benchmark]
        public string RepairByOne()
        {
            return XmlParser.XmlParser.RepairByOne(xml);
        }

        [Benchmark]
        public string RepairByBatch()
        {
            return XmlParser.XmlParser.RepairByBatch(xml);
        }

        private static void Main()
        {
            BenchmarkRunner.Run<Benchmarks>();
        }
    }
}
