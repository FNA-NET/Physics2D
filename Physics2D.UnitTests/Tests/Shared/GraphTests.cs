using Physics2D.Shared;
using Physics2D.UnitTests.Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics2D.UnitTests.Shared;

[TestClass]
public class GraphTests
{
    [TestMethod]
    public void TestEmptyConstruction()
    {
        Graph<int> graph = new Graph<int>();
        Assert.AreEqual(0, graph.Count);
        Assert.IsNull(graph.First);

        Graph<Dummy> graph2 = new Graph<Dummy>();
        Assert.AreEqual(0, graph2.Count);
        Assert.IsNull(graph2.First);
    }

    [TestMethod]
    public void TestAddValueType()
    {
        Graph<int> graph = new Graph<int>();
        GraphNode<int> node = graph.Add(10);

        Assert.AreEqual(10, node.Item);
        Assert.AreEqual(node, node.Prev);
        Assert.AreEqual(node, node.Next);
    }

    [TestMethod]
    public void TestAddReferenceType()
    {
        Graph<Dummy> graph = new Graph<Dummy>();

        Dummy instance = new Dummy();

        GraphNode<Dummy> node = graph.Add(instance);

        Assert.AreEqual(instance, node.Item);
        Assert.AreEqual(node, node.Prev);
        Assert.AreEqual(node, node.Next);
    }

    [TestMethod]
    public void TestRemoveValueType()
    {
        Graph<int> graph = new Graph<int>();
        GraphNode<int> node = graph.Add(10);

        Assert.AreEqual(1, graph.Count);

        graph.Remove(node);

        Assert.AreEqual(0, graph.Count);

        //Check that the node was cleared;
        Assert.IsNull(node.Prev);
        Assert.IsNull(node.Next);
    }

    [TestMethod]
    public void TestRemoveReferenceType()
    {
        Graph<Dummy> graph = new Graph<Dummy>();
        GraphNode<Dummy> node = graph.Add(new Dummy());

        Assert.AreEqual(1, graph.Count);

        graph.Remove(node);

        Assert.AreEqual(0, graph.Count);

        //Check that the node was cleared;
        Assert.IsNull(node.Prev);
        Assert.IsNull(node.Next);
    }

    [TestMethod]
    public void TestIteration()
    {
        Graph<int> graph = new Graph<int>();

        for (int i = 0; i < 10; i++)
        {
            graph.Add(i);
        }

        Assert.AreEqual(10, graph.Count);

        int count = 0;

        foreach (int i in graph)
        {
            Assert.AreEqual(count++, i);
        }

        Assert.AreEqual(10, count);
    }
}
