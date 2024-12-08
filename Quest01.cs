using System.Linq;

namespace tkoa2024;

[TestClass]
public sealed class Quest01
{
    [TestMethod]
    public void Part1()
    {
        var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
        var fileName = Path.Combine(baseDir, "Input", $"Quest01_01.input");
        Assert.IsTrue(Path.Exists(fileName));
        var lines = File.ReadAllLines(fileName);
        Assert.AreEqual(1, lines.Length);
        var index = "ABC";
        var points = new[] { 0, 1, 3 };
        Assert.AreEqual(1339, lines[0].Sum(c => points[index.IndexOf(c)]));
    }

    [TestMethod]
    public void Part2()
    {
        var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
        var fileName = Path.Combine(baseDir, "Input", $"Quest01_02.input");
        Assert.IsTrue(Path.Exists(fileName));
        var lines = File.ReadAllLines(fileName);
        Assert.AreEqual(1, lines.Length);
        var index = "ABCD";
        var points = new[] { 0, 1, 3, 5 };
        var sum = 0;
        foreach (var chunk in lines[0].Chunk(2))
        {
            if (chunk.Contains('x'))
            {
                var other = chunk.Where(c => c != 'x');
                if (other.Any())
                {
                    sum += points[index.IndexOf(other.First())];
                }
            }
            else
            {
                sum += points[index.IndexOf(chunk[0])] + points[index.IndexOf(chunk[1])] + 2;
            }

        }
        Assert.AreEqual(5664, sum);
    }

    [TestMethod]
    public void Part3()
    {
        var baseDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "..", "..", "..");
        var fileName = Path.Combine(baseDir, "Input", $"Quest01_03.input");
        Assert.IsTrue(Path.Exists(fileName));
        var lines = File.ReadAllLines(fileName);

        Assert.AreEqual(1, lines.Length);
        var index = "ABCD";
        var points = new[] { 0, 1, 3, 5 };
        var sum = 0;
        foreach (var chunk in lines[0].Chunk(3))
        {
            if (chunk.Contains('x'))
            {
                var other = chunk.Where(c => c != 'x').ToArray(); 
                if (other.Count() == 1)
                {
                    sum += points[index.IndexOf(other[0])];
                }
                else if (other.Count() == 2)
                {
                    sum += points[index.IndexOf(other[0])] + points[index.IndexOf(other[1])] + 2;
                }
            }
            else
            {
                sum += points[index.IndexOf(chunk[0])] + points[index.IndexOf(chunk[1])] + points[index.IndexOf(chunk[2])] + 6;
            }

        }
        Assert.AreEqual(27814, sum);
    }
}
