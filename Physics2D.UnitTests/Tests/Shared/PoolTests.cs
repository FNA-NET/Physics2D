using System.Collections.Generic;
using System.Linq;
using Physics2D.Shared;
using Physics2D.UnitTests.Code;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Physics2D.UnitTests.Shared;

[TestClass]
public class PoolTests
{
    [TestMethod]
    public void GetWhileAdding()
    {
        Pool<PoolObject> pool = new Pool<PoolObject>(() => new PoolObject(), x => x.Reset(), 1);

        bool first = true;

        //We use the fact that it is lazy loaded to re-add the same item
        IEnumerable<PoolObject> many = pool.GetManyFromPool(10);
        foreach (PoolObject obj in many)
        {
            if (first)
            {
                Assert.IsTrue(obj.IsNew);
                first = false;
            }
            else
                Assert.IsFalse(obj.IsNew);

            pool.ReturnToPool(obj);
        }
    }

    [TestMethod]
    public void GetManyAcrossBoundary()
    {
        Pool<PoolObject> pool = new Pool<PoolObject>(() => new PoolObject(), null, 6);

        //We get twice as many as in pool
        List<PoolObject> many = pool.GetManyFromPool(12).ToList();
        foreach (PoolObject obj in many)
        {
            Assert.IsTrue(obj.IsNew);
        }

        Assert.AreEqual(12, many.Count);
    }

    [TestMethod]
    public void GetManyNewAndPooled()
    {
        Pool<PoolObject> pool = new Pool<PoolObject>(() => new PoolObject(), x => x.Reset(), 10);

        //Empty whole pool
        List<PoolObject> many = pool.GetManyFromPool(10).ToList();

        foreach (PoolObject obj in many)
        {
            Assert.IsTrue(obj.IsNew);
        }

        Assert.AreEqual(10, many.Count);

        many = pool.GetManyFromPool(10).ToList();
        foreach (PoolObject obj in many)
        {
            Assert.IsTrue(obj.IsNew);
            pool.ReturnToPool(obj);
        }

        Assert.AreEqual(10, many.Count);

        many = pool.GetManyFromPool(10).ToList();
        foreach (PoolObject obj in many)
        {
            Assert.IsFalse(obj.IsNew);
        }

        Assert.AreEqual(10, many.Count);
    }

    [TestMethod]
    public void GetOnePooled()
    {
        Pool<PoolObject> pool = new Pool<PoolObject>(() => new PoolObject(), x => x.Reset(), 1);
        PoolObject obj = pool.GetFromPool();

        Assert.IsTrue(obj.IsNew);

        pool.ReturnToPool(obj);
        obj = pool.GetFromPool();

        Assert.IsFalse(obj.IsNew);
    }

    [TestMethod]
    public void GetOneNew()
    {
        Pool<PoolObject> pool = new Pool<PoolObject>(() => new PoolObject(), x => x.Reset(), 0);
        PoolObject obj = pool.GetFromPool();

        Assert.IsTrue(obj.IsNew);

        obj = pool.GetFromPool();
        Assert.IsTrue(obj.IsNew);
    }
}
