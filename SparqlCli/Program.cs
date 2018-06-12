using System;
using System.IO;
using VDS.RDF;
using VDS.RDF.Parsing;
using VDS.RDF.Query;
using VDS.RDF.Query.Datasets;

namespace SparqlCli
{
    class Program
    {
        static void Main(string[] args)
        {
            var g = new Graph();
            var projectPath = Path.GetDirectoryName(Path.GetDirectoryName(Environment.CurrentDirectory)) ?? "";
            var file = Path.Combine(projectPath, "sn-twitter.owl");
            g.LoadFromFile(file);
            ISparqlDataset ds = new InMemoryDataset(g);
            var processor = new LeviathanQueryProcessor(ds);
            var parser = new SparqlQueryParser();

            // Test queries
            //"SELECT (COUNT(?s) AS ?triples) WHERE { ?s ?p ?o }"
            //"SELECT DISTINCT ?type WHERE { ?s a ?type. }"

            while (true)
            {
                Console.WriteLine("Shkruaj query: ");
                var inputQuery = Console.ReadLine();
                try
                {
                    var query = parser.ParseFromString(inputQuery);
                    var results = (SparqlResultSet)processor.ProcessQuery(query);
                    Console.WriteLine("Duke shfaqur " + results.Count + " rezultate.");
                    foreach (var result in results)
                    {
                        Console.WriteLine(result.ToString());
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }
    }
}
