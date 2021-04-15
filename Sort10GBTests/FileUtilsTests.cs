using Microsoft.VisualStudio.TestTools.UnitTesting;
using Sort;
using System;
using System.Collections.Generic;
using System.Text;

namespace Sort.Tests
{
    [TestClass()]
    public class FileUtilsTests
    {
        [TestMethod()]
        public void SortFileTest()
        {
            string file = "tests/test0.txt", outFile = "tests/out0.txt";
            FileUtils.SortFile(file, outFile);
            Assert.IsTrue(FileUtils.CheckSorted(outFile));
        }
    }
}