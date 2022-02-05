using Microsoft.VisualStudio.TestTools.UnitTesting;
using SodukoSolver;
using System;

namespace Tests
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void Invalid()
        {
            try
            {
                ConsoleSudukoBoard sudukoBoard = new ConsoleSudukoBoard("1100000402000030");
                Solver solver = new Solver(sudukoBoard);
                solver.Solve();
                Assert.Fail();
            }
            catch
            {
            }
        }

        [TestMethod]
        public void InvalidEmpty()
        {
            try
            {
                ConsoleSudukoBoard sudukoBoard = new ConsoleSudukoBoard("");
                Solver solver = new Solver(sudukoBoard);
                solver.Solve();
                Assert.Fail();
            }
            catch
            {
            }
        }

        [TestMethod]
        public void Board9x9Extreme()
        {
            try
            {
                ConsoleSudukoBoard sudukoBoard = new ConsoleSudukoBoard("006000007970000040520000800000700500400003170050008006000301002000805000603902000");
                Solver solver = new Solver(sudukoBoard);
                solver.Solve();

            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Board16x16()
        {
            try
            {
                ConsoleSudukoBoard sudukoBoard = new ConsoleSudukoBoard("0000000000000000000000000000000000000000000000000000800000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000000400000000000000060000000000000002000000500000050000000000000000000000000?00000000000000000000000000500000");
                Solver solver = new Solver(sudukoBoard);
                solver.Solve();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Board4x4()
        {
            try
            {
                ConsoleSudukoBoard sudukoBoard = new ConsoleSudukoBoard("1000000402000030");
                Solver solver = new Solver(sudukoBoard);
                solver.Solve();

            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void EmptyBoard4x4()
        {
            try
            {
                ConsoleSudukoBoard sudukoBoard = new ConsoleSudukoBoard("0000000000000000");
                Solver solver = new Solver(sudukoBoard);
                solver.Solve();
            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void Board9x9()
        {
            try
            {
                ConsoleSudukoBoard sudukoBoard = new ConsoleSudukoBoard("002000000000000000000008000050001406000000170601000090000000000000009000003600000");
                Solver solver = new Solver(sudukoBoard);
                solver.Solve();
                Assert.Fail();
            }
            catch
            {
            }
        }

        public void Board16x16No2()
        {
            try
            {
                ConsoleSudukoBoard sudukoBoard = new ConsoleSudukoBoard("00:00?0000;0000001000:000000000000000000000000000000=00000000000000000000000000000500000000000000000000000600@00000000000000000000000000000;00000000000000000>000000000=0000000000000000000000000080000006000000000000000000000000000000000>00000000000000000000");
                Solver solver = new Solver(sudukoBoard);
                solver.Solve();

            }
            catch
            {
                Assert.Fail();
            }
        }

        [TestMethod]
        public void NoSoultion()
        {
            try
            {
                ConsoleSudukoBoard sudukoBoard = new ConsoleSudukoBoard("1000040200203004");
                Solver solver = new Solver(sudukoBoard);
                solver.Solve();
                Assert.Fail();
            }
            catch
            {
            }
        }
    }
}
