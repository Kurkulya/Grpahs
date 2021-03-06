﻿using System;
using NUnit.Framework;
using Grpahs;
using Grpahs.Exceptions;
using Grpahs.Oriented;
using System.Collections.Generic;
using System.Linq;

namespace GraphTests
{

    [TestFixture(typeof(GraphD))]
    [TestFixture(typeof(GraphM))]
    [TestFixture(typeof(GraphH))]
    public class Tests<TGraph> where TGraph : IGraph, new()
    {
        IGraph graph;

        [SetUp]
        public void SetUp()
        {
            graph = new TGraph();
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestNumVertexes(int length)
        {
            for (int i = 0; i < length; i++)
            {
                graph.AddVertex("A" + i);
            }
            Assert.AreEqual(length, graph.Vertexes);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestInputEdgeCount(int count)
        {
            graph.AddVertex("A");
            for (int i = 0; i < count; i++)
            {
                graph.AddVertex("A" + i);
                graph.AddEdge("A" + i, "A", 4);
            }
            Assert.AreEqual(count, graph.GetInputEdgeCount("A"));
        }


        [Test]
        public void TestInputEdgeCountEx()
        {
            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.GetInputEdgeCount("A"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestOutputEdgeCountEx()
        {
            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.GetOutputEdgeCount("A"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestInputVertexNamesEx()
        {
            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.GetInputVertexNames("A"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestOutputVertexNamesEx()
        {
            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.GetOutputVertexNames("A"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [TestCase("A")]
        [TestCase("A B")]
        [TestCase("A B C D E")]
        public void TestInputVertexNames(string names)
        {                      
            List<string> result = names.Split(' ').ToList();

            graph.AddVertex("T");

            foreach(string name in result)
            {
                graph.AddVertex(name);             
                graph.AddEdge(name, "T", 4);
            }
            Assert.AreEqual(result, graph.GetInputVertexNames("T"));
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestOutputEdgeCount(int count)
        {
            graph.AddVertex("A");
            for (int i = 0; i < count; i++)
            {
                graph.AddVertex("A" + i);
                graph.AddEdge("A", "A" + i, 4);
            }
            Assert.AreEqual(count, graph.GetOutputEdgeCount("A"));
        }

        [TestCase("A")]
        [TestCase("A B")]
        [TestCase("A B C D E")]
        public void TestOutputVertexNames(string names)
        {
            List<string> result = names.Split(' ').ToList();

            graph.AddVertex("T");

            foreach (string name in result)
            {
                graph.AddVertex(name);
                graph.AddEdge("T", name, 4);
            }
            Assert.AreEqual(result, graph.GetOutputVertexNames("T"));
        }


        [TestCase(0)]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestNumEdges(int length)
        {
            string name = "A";
            for (int i = 0; i < length; i++)
            {
                string temp = name;
                graph.AddVertex(temp);

                name = "A" + i;
                graph.AddVertex(name);

                graph.AddEdge(temp, name, 4);
            }
            Assert.AreEqual(length, graph.Edges);
        }

        [TestCase(0)]
        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestAddEdge(int length)
        {
            string name = "A";
            for (int i = 0; i < length; i++)
            {
                string temp = name;
                graph.AddVertex(temp);

                name = "A" + i;
                graph.AddVertex(name);

                graph.AddEdge(temp, name, 4);
            }

            Assert.AreEqual(length, graph.Edges);
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestAddExistingEdge(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            for (int i = 0; i < length; i++)
            {
                graph.AddEdge("A", "B", 4);
            }
            Assert.AreEqual(1, graph.Edges);
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestAddEdgeEx(string name)
        {
            graph.AddVertex(name);

            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.AddEdge("A", "B", 1));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }


        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestGetEdge(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddEdge("A", "B", length);
            Assert.AreEqual(length, graph.GetEdge("A", "B"));
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestGetEdgeEx_NoVertex(string name)
        {
            graph.AddVertex(name);

            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.GetEdge("A", "B"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestGetEdgeEx_NoEdges()
        {
            graph.AddVertex("A");
            graph.AddVertex("B");

            var ex = Assert.Throws<EdgeDoesNotExistException>(() => graph.GetEdge("A", "B"));
            Assert.AreEqual(typeof(EdgeDoesNotExistException), ex.GetType());
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestDelEdge(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddEdge("A", "B", length);
            Assert.AreEqual(length, graph.DelEdge("A", "B"));
            Assert.AreEqual(0, graph.Edges);
            
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestDelEdgeEx_NoVertex(string name)
        {
            graph.AddVertex(name);

            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.DelEdge("A", "B"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestDelEdgeEx_NoEdge()
        {
            graph.AddVertex("A");
            graph.AddVertex("B");

            var ex = Assert.Throws<EdgeDoesNotExistException>(() => graph.DelEdge("A", "B"));
            Assert.AreEqual(typeof(EdgeDoesNotExistException), ex.GetType());
        }

        [TestCase(1)]
        [TestCase(5)]
        [TestCase(16)]
        public void TestSetEdge(int length)
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddEdge("A", "B", 458);
            graph.SetEdge("A", "B", length);
            Assert.AreEqual(length, graph.GetEdge("A","B"));
        }

        [TestCase("A")]
        [TestCase("B")]
        public void TestSetEdgeEx_NoVertex(string name)
        {
            graph.AddVertex(name);

            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.SetEdge("A", "B", 2));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestSetEdgeEx_NoEdges()
        {
            graph.AddVertex("A");
            graph.AddVertex("B");

            var ex = Assert.Throws<EdgeDoesNotExistException>(() => graph.SetEdge("A", "B", 2));
            Assert.AreEqual(typeof(EdgeDoesNotExistException), ex.GetType());
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestAddVertex(int length)
        {
            for (int i = 0; i < length; i++)
            {
                graph.AddVertex("A" + i);
            }
            Assert.AreEqual(length, graph.Vertexes);
        }

        [TestCase(1)]
        [TestCase(2)]
        [TestCase(16)]
        public void TestAddSimilarVertex(int length)
        {
            for (int i = 0; i < length; i++)
            {
                graph.AddVertex("A");
            }
            Assert.AreEqual(1, graph.Vertexes);
        }

        [TestCase("A")]
        [TestCase("Bdfd")]
        [TestCase("")]
        public void TestDelVertex(string name)
        {
            graph.AddVertex(name);
            graph.DelVertex(name);
            Assert.AreEqual(0,graph.Vertexes);
        }

        [Test]
        public void TestDelVertexEx_NoVertex()
        {
            graph.AddVertex("Test");

            var ex = Assert.Throws<VertexDoesNotExistException>(() => graph.DelVertex("DelTest"));
            Assert.AreEqual(typeof(VertexDoesNotExistException), ex.GetType());
        }

        [Test]
        public void TestDelVertex_Edges()
        {
            graph.AddVertex("A");
            graph.AddVertex("B");
            graph.AddVertex("C");

            graph.AddEdge("A", "B", 1);
            graph.AddEdge("B", "C", 2);
            graph.AddEdge("C", "A", 3);

            graph.DelVertex("B");

            Assert.AreEqual(1, graph.Edges);
        }



        [TestCase("Днепр", "Киев", new string[] { "Днепр", "Кременчуг", "Киев" })]
        [TestCase("Днепр", "Кременчуг", new string[] { "Днепр", "Кременчуг" })]
        [TestCase("Днепр", "Полтава", new string[] { "Днепр", "Полтава" })]
        [TestCase("Киев", "Днепр", new string[] { "Киев", "Кременчуг","Днепр" })]
        [TestCase("Киев", "Кременчуг", new string[] { "Киев", "Кременчуг" })]
        [TestCase("Киев", "Полтава", new string[] { "Киев", "Полтава" })]
        [TestCase("Кременчуг", "Киев", new string[] { "Кременчуг", "Киев" })]
        [TestCase("Кременчуг", "Днепр", new string[] { "Кременчуг", "Днепр" })]
        [TestCase("Кременчуг", "Полтава", new string[] { "Кременчуг", "Полтава" })]
        [TestCase("Полтава", "Киев", new string[] { "Полтава", "Киев" })]
        [TestCase("Полтава", "Кременчуг", new string[] { "Полтава", "Кременчуг" })]
        [TestCase("Полтава", "Днепр", new string[] { "Полтава", "Днепр" })]
        public void TestShortestPath(string from, string to, string[] result)
        {
            graph.AddVertex("Днепр");
            graph.AddVertex("Киев");
            graph.AddVertex("Кременчуг");
            graph.AddVertex("Полтава");
            graph.AddEdge("Днепр", "Киев", 453);
            graph.AddEdge("Днепр", "Кременчуг", 161);
            graph.AddEdge("Днепр", "Полтава", 202);
            graph.AddEdge("Киев", "Днепр", 491);
            graph.AddEdge("Киев", "Кременчуг", 292);
            graph.AddEdge("Киев", "Полтава", 343);
            graph.AddEdge("Полтава", "Киев", 341);
            graph.AddEdge("Полтава", "Кременчуг", 113);
            graph.AddEdge("Полтава", "Днепр", 182);
            graph.AddEdge("Кременчуг", "Киев", 291);
            graph.AddEdge("Кременчуг", "Днепр", 161);
            graph.AddEdge("Кременчуг", "Полтава", 113);


            Assert.AreEqual(result.ToList(), graph.GetShortestPath(from, to));
        }

        [TestCase("Кременчуг", "Кременчуг")]
        [TestCase("Полтава", "Полтава")]
        [TestCase("Днепр", "Днепр")]
        [TestCase("Киев", "Киев")]
        public void TestShortestPath_Similar(string from, string to)
        {
            graph.AddVertex("Днепр");
            graph.AddVertex("Киев");
            graph.AddVertex("Кременчуг");
            graph.AddVertex("Полтава");
            graph.AddEdge("Днепр", "Киев", 453);
            graph.AddEdge("Днепр", "Кременчуг", 161);
            graph.AddEdge("Днепр", "Полтава", 202);
            graph.AddEdge("Киев", "Днепр", 491);
            graph.AddEdge("Киев", "Кременчуг", 292);
            graph.AddEdge("Киев", "Полтава", 343);
            graph.AddEdge("Полтава", "Киев", 341);
            graph.AddEdge("Полтава", "Кременчуг", 113);
            graph.AddEdge("Полтава", "Днепр", 182);
            graph.AddEdge("Кременчуг", "Киев", 291);
            graph.AddEdge("Кременчуг", "Днепр", 161);
            graph.AddEdge("Кременчуг", "Полтава", 113);

            Assert.AreEqual(0, graph.GetShortestPath(from, to).Count);
        }

        [TestCase("Париж", "Днепр")]
        [TestCase("Париж", "Киев")]
        [TestCase("Париж", "Полтава")]
        [TestCase("Париж", "Кременчуг")]
        [TestCase("Днепр", "Париж")]
        [TestCase("Киев", "Париж")]
        [TestCase("Полтава", "Париж")]
        [TestCase("Кременчуг", "Париж")]
        public void TestShortestPathEx(string from, string to)
        {
            graph.AddVertex("Днепр");
            graph.AddVertex("Киев");
            graph.AddVertex("Кременчуг");
            graph.AddVertex("Полтава");
            graph.AddVertex("Париж");
            graph.AddEdge("Днепр", "Киев", 453);
            graph.AddEdge("Днепр", "Кременчуг", 161);
            graph.AddEdge("Днепр", "Полтава", 202);
            graph.AddEdge("Киев", "Днепр", 491);
            graph.AddEdge("Киев", "Кременчуг", 292);
            graph.AddEdge("Киев", "Полтава", 343);
            graph.AddEdge("Полтава", "Киев", 341);
            graph.AddEdge("Полтава", "Кременчуг", 113);
            graph.AddEdge("Полтава", "Днепр", 182);
            graph.AddEdge("Кременчуг", "Киев", 291);
            graph.AddEdge("Кременчуг", "Днепр", 161);
            graph.AddEdge("Кременчуг", "Полтава", 113);

            var ex = Assert.Throws<PathDoesNotExistException>(() => graph.GetShortestPath(from, to));
            Assert.AreEqual(typeof(PathDoesNotExistException), ex.GetType());
        }
    }
}
