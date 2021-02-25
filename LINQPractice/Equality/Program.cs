using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Equality
{
    [TestFixture]
    internal class Program
    {
        [Test]
        public void Test()
        {
            var a = new[] {1, 2, 3};
            var b = new List<int> {1, 2, 3};
            var c = new List<int> {3, 2, 1};
            Assert.That(a, Is.EqualTo(b));
            Assert.That(a, Is.EquivalentTo(c));
        }
        public static void Main(string[] args)
        {
            var arr1 = new[] {1, 2, 3};
            var arr2 = new[] {1, 2, 3};
            Console.WriteLine(arr1.Equals(arr2));
            Console.WriteLine(arr1.SequenceEqual(arr2));
        }
    }
}