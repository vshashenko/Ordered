using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Ordered.Tests
{
    [TestClass]
    public sealed class OrderedExtensionsTest
    {
        [TestMethod]
        public void MergeGroupJoinOneToOne()
        {
            var outerCollection = new[] { 1, 3, 5 };
            var innerCollection = new[] { 1, 3, 5 };

            var expectedJoins = new[]
            {
                Tuple.Create(1, 1),
                Tuple.Create(3, 3),
                Tuple.Create(5, 5),
            };

            var joins = new List<Tuple<int, int>>();

            OrderedExtensions.GroupJoin(
                outerCollection, innerCollection, i => i, j => j, (i, j) => joins.Add(Tuple.Create(i, j)));

            CollectionAssert.AreEqual(expectedJoins, joins);
        }

        [TestMethod]
        public void MergeGroupJoinOneToMany()
        {
            var outerCollection = new[] { 1, 3, 5 };
            var innerCollection = new[] { 1, 1, 3, 5, 5, 5 };

            var expectedJoins = new[]
            {
                Tuple.Create(1, 1), 
                Tuple.Create(1, 1), 
                Tuple.Create(3, 3), 
                Tuple.Create(5, 5), 
                Tuple.Create(5, 5), 
                Tuple.Create(5, 5), 
            };

            var joins = new List<Tuple<int, int>>();

            OrderedExtensions.GroupJoin(
                outerCollection, innerCollection, i => i, j => j, (i, j) => joins.Add(Tuple.Create(i, j)));

            CollectionAssert.AreEqual(expectedJoins, joins);
        }

        [TestMethod]
        public void MergeGroupJoinOneToManyWithChildless()
        {
            var outerCollection = new[] { 1, 3, 4, 5 };
            var innerCollection = new[] { 1, 1, 3, 5, 5, 5 };

            var expectedJoins = new[]
                {
                    Tuple.Create(1, 1), 
                    Tuple.Create(1, 1), 
                    Tuple.Create(3, 3), 
                    Tuple.Create(5, 5), 
                    Tuple.Create(5, 5), 
                    Tuple.Create(5, 5), 
                };

            var joins = new List<Tuple<int, int>>();

            OrderedExtensions.GroupJoin(
                outerCollection, innerCollection, i => i, j => j, (i, j) => joins.Add(Tuple.Create(i, j)));

            CollectionAssert.AreEqual(expectedJoins, joins);
        }

        [TestMethod]
        public void MergeGroupJoinOneToManyWithOrphans()
        {
            var outerCollection = new[] { 1, 3, 5 };
            var innerCollection = new[] { 1, 1, 3, 4, 5, 5, 5 };

            var expectedJoins = new[]
                {
                    Tuple.Create(1, 1), 
                    Tuple.Create(1, 1), 
                    Tuple.Create(3, 3), 
                    Tuple.Create(5, 5), 
                    Tuple.Create(5, 5), 
                    Tuple.Create(5, 5), 
                };

            var joins = new List<Tuple<int, int>>();

            OrderedExtensions.GroupJoin(
                outerCollection, innerCollection, i => i, j => j, (i, j) => joins.Add(Tuple.Create(i, j)));

            CollectionAssert.AreEqual(expectedJoins, joins);
        }

        [TestMethod]
        public void MergeGroupJoinOneToManyWithChildlessAndOrphans()
        {
            var outerCollection = new[] { 1, 3, 5 };
            var innerCollection = new[] { 3, 3, 7, 7 };

            var expectedJoins = new[]
                {
                    Tuple.Create(3, 3), 
                    Tuple.Create(3, 3), 
                };

            var joins = new List<Tuple<int, int>>();

            OrderedExtensions.GroupJoin(
                outerCollection, innerCollection, i => i, j => j, (i, j) => joins.Add(Tuple.Create(i, j)));

            CollectionAssert.AreEqual(expectedJoins, joins);
        }

        [TestMethod]
        public void MergeGroupJoinOuterEmpty()
        {
            var outerCollection = new int[] { };
            var innerCollection = new[] { 3, 3, 5, 5 };

            var expectedJoins = new Tuple<int, int>[] { };

            var joins = new List<Tuple<int, int>>();

            OrderedExtensions.GroupJoin(
                outerCollection, innerCollection, i => i, j => j, (i, j) => joins.Add(Tuple.Create(i, j)));

            CollectionAssert.AreEqual(expectedJoins, joins);
        }

        [TestMethod]
        public void MergeGroupJoinInnerEmpty()
        {
            var outerCollection = new[] { 1, 3, 3, 5 };
            var innerCollection = new int[] { };

            var expectedJoins = new Tuple<int, int>[] { };

            var joins = new List<Tuple<int, int>>();

            OrderedExtensions.GroupJoin(
                outerCollection, innerCollection, i => i, j => j, (i, j) => joins.Add(Tuple.Create(i, j)));

            CollectionAssert.AreEqual(expectedJoins, joins);
        }

        [TestMethod]
        public void MergeGroupJoinNoMatch()
        {
            var outerCollection = new[] { 1, 3, 5 };
            var innerCollection = new[] { 2, 4, 7 };

            var expectedJoins = new Tuple<int, int>[] { };

            var joins = new List<Tuple<int, int>>();

            OrderedExtensions.GroupJoin(
                outerCollection, innerCollection, i => i, j => j, (i, j) => joins.Add(Tuple.Create(i, j)));

            CollectionAssert.AreEqual(expectedJoins, joins);
        }

        [TestMethod]
        public void MergeGroupJoinEmptyCollections()
        {
            var outerCollection = new int[] { };
            var innerCollection = new int[] { };

            var expectedJoins = new Tuple<int, int>[] { };

            var joins = new List<Tuple<int, int>>();

            OrderedExtensions.GroupJoin(
                outerCollection, innerCollection, i => i, j => j, (i, j) => joins.Add(Tuple.Create(i, j)));

            CollectionAssert.AreEqual(expectedJoins, joins);
        }
    }
}
