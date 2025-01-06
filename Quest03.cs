using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tkoa2024
{
    [TestClass]
    public sealed class Quest03
    {
        public string Calculate(IEnumerable<string> lines, bool checkDiagonals)
        {
            var grid = new List<List<int>>();
            grid.Add(new List<int>(new int[lines.First().Length + 2]));
            foreach (var line in lines)
            {
                var row = new List<int>();
                row.Add(0);
                foreach (var c in line)
                {
                    row.Add(c == '.' ? 0 : 1);
                }
                row.Add(0);
                grid.Add(row);
            }
            grid.Add(new List<int>(new int[lines.First().Length + 2]));


            var isUpdated = true;
            for (var level = 1; isUpdated; level++)
            {
                isUpdated = false;
                var newGrid = new List<List<int>>();
                for (var y = 0; y < grid.Count; y++)
                {
                    var newRow = new List<int>();
                    for (var x = 0; x < grid[y].Count; x++)
                    {
                        var count = 0;
                        for (var dy = -1; dy <= 1; dy++)
                        {
                            for (var dx = -1; dx <= 1; dx++)
                            {
                                if (!checkDiagonals && (Math.Abs(dx) == Math.Abs(dy)))
                                {
                                    continue;
                                }
                                if (dy == 0 && dx == 0)
                                {
                                    continue;
                                }
                                var ny = y + dy;
                                var nx = x + dx;
                                if (ny < 0 || ny >= grid.Count || nx < 0 || nx >= grid[y].Count)
                                {
                                    continue;
                                }
                                if (grid[ny][nx] == level)
                                {
                                    count++;
                                }
                            }
                        }
                        if (!checkDiagonals && count == 4)
                        {
                            newRow.Add(level + 1);
                            isUpdated = true;
                        }
                        else if (checkDiagonals && count == 8)
                        {
                            newRow.Add(level + 1);
                            isUpdated = true;
                        }
                        else
                        {
                            newRow.Add(grid[y][x]);
                        }
                    }
                    newGrid.Add(newRow);
                }
                grid = newGrid;

            }

            // print grid
            for (var y = 0; y < grid.Count; y++)
            {
                for (var x = 0; x < grid[y].Count; x++)
                {
                    Console.Write(grid[y][x] == 0 ? '.' : Convert.ToString(grid[y][x], 16));
                }
                Console.WriteLine();
            }

            return grid.Sum(row => row.Sum()).ToString();
        }

        [TestMethod]
        public void Quest03_Part1()
        {
            var result = Calculate(Common.Input("everybody_codes_e2024_q03_p1.txt"), false);
            Assert.AreEqual("130", result);
        }

        [TestMethod]
        public void Quest03_Part1_Example()
        {
            var lines = """
                ..........
                ..###.##..
                ...####...
                ..######..
                ..######..
                ...####...
                ..........
                """;
            var result = Calculate(Common.GetLines(lines), false);
            Assert.AreEqual("35", result);
        }

        
        [TestMethod]
        public void Quest03_Part2()
        {
            var result = Calculate(Common.Input("everybody_codes_e2024_q03_p2.txt"), false);
            Assert.AreEqual("2586", result);
        }

        [TestMethod]
        public void Quest03_Part3()
        {
            var result = Calculate(Common.Input("everybody_codes_e2024_q03_p3.txt"), true);
            Assert.AreNotEqual("10433", result); //first correct, length correct
            Assert.AreEqual("", result);
        }

        [TestMethod]
        public void Quest03_Part3_Example()
        {
            var lines = """
                ..........
                ..###.##..
                ...####...
                ..######..
                ..######..
                ...####...
                ..........
                """;
            var result = Calculate(Common.GetLines(lines), true);
            Assert.AreEqual("29", result);
        }

    }
}
