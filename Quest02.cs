using System.Linq;

namespace tkoa2024;

[TestClass]
public sealed class Quest02
{
    [TestMethod]
    public void Part1()
    {
        var runic = new List<string>();
        var sum = 0;
        foreach (var line in Common.Input("Quest02_01.input"))
        {
            if (string.IsNullOrEmpty(line)) continue;

            if (line.Contains(':'))
            {
                var split = line.Split(':');
                runic = new List<string>(split[1].Split(',', StringSplitOptions.TrimEntries));
            } 
            else
            {
                foreach (var token in line.Split(' '))
                {
                    foreach (var rune in runic)
                    {
                        if (token.Contains(rune))
                        {
                            sum += 1;
                        }
                    }
                }
            }
        }
        Assert.AreNotEqual(35, sum);
        Assert.AreNotEqual(22, sum); // first character is correct, the number of characters is correct
        Assert.AreEqual(28, sum);
    }

    public string Part2(IEnumerable<string> input)
    {
        var runic = new List<string>();
        var sum = 0;
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line)) continue;

            if (line.Contains(':'))
            {
                var split = line.Split(':');
                runic = new List<string>(split[1].Split(',', StringSplitOptions.TrimEntries));
                runic.Select(r => string.Concat(r.Reverse())).ToList().ForEach(r => runic.Add(r));
            }
            else
            {
                foreach (var token in line.Split(' '))
                {
                    var matches = new List<bool>(new bool[token.Length]);

                    foreach (var rune in runic)
                    {
                        if (token.Contains(rune))
                        {
                            var index = token.IndexOf(rune);
                            while (index >= 0)
                            {
                                Enumerable.Range(index, rune.Length).ToList().ForEach(i => matches[i] = true);
                                index = token.IndexOf(rune, index + 1);
                            }
                        }
                    }

                    sum += matches.Count(m => m);
                }
            }
        }
        return sum.ToString();
    }

    [TestMethod]
    public void Quest2_Part2()
    {
        var result = Part2(Common.Input("everybody_codes_e2024_q02_p2.txt"));
        Assert.AreEqual("5264", result);
    }

    [TestMethod]
    public void Part2Example1()
    {
        var input = """
            WORDS:THE,OWE,MES,ROD,HER,QAQ

            AWAKEN THE POWE ADORNED WITH THE FLAMES BRIGHT IRE
            THE FLAME SHIELDED THE HEART OF THE KINGS
            POWE PO WER P OWE R
            THERE IS THE END
            QAQAQ
            """;
        var result = Part2(Common.GetLines(input));
        Assert.AreEqual("42", result);
    }


    public string Part3(IEnumerable<string> input)
    {
        var words = new List<string>();
        var grid = new List<string>();
        foreach (var line in input)
        {
            if (string.IsNullOrEmpty(line)) continue;
            if (line.Contains(':'))
            {
                var parts = line.Split(':');
                words = parts[1].Split(',', StringSplitOptions.TrimEntries).ToList();
            }
            else
            {
                grid.Add(line);
            }
        }
        var matches = new List<List<bool>>();
        for (var i = 0; i < grid.Count; i++)
        {
            matches.Add(new List<bool>(new bool[grid[i].Length]));
        }
        var dirs = new List<(int dx, int dy)> { (1, 0), (0, 1), (-1, 0), (0, -1) };
        for (int y = 0; y < grid.Count; y++)
        {
            for (int x = 0; x < grid[y].Length; x++)
            {
                foreach (var word in words)
                {
                    foreach (var (dx, dy) in dirs)
                    {
                        var isMatch = true;
                        var (px, py) = (x, y);

                        for (var i = 0; i < word.Length; i++)
                        {

                            if (grid[py][px] != word[i])
                            {
                                isMatch = false;
                                break;
                            }
                            px += dx;
                            py += dy;

                            if (px < 0)
                            {
                                px += grid[0].Length;
                            }
                            if (px >= grid[0].Length)
                            {
                                px -= grid[0].Length;
                            }
                            if (i == word.Length - 1)
                            {
                                break;
                            }
                            if (py < 0)
                            {
                                isMatch = false;
                                break;
                            }
                            if (py >= grid.Count)
                            {
                                isMatch = false;
                                break;
                            }
                        }

                        if (isMatch)
                        {
                            (px, py) = (x, y);
                            //Console.WriteLine($"dir={dx},{dy} pos={px},{py} word={word}");

                            for (var i = 0; i < word.Length; i++)
                            {

                                matches[py][px] = true;

                                px += dx;
                                py += dy;

                                if (px < 0)
                                {
                                    px += grid[0].Length;
                                }
                                if (px >= grid[0].Length)
                                {
                                    px -= grid[0].Length;
                                }
                            }
                        }
                    }
                }
            }
        }

        return matches.Sum(line => line.Count(x => x)).ToString();
    }

    [TestMethod]
    public void Quest2_Part3()
    {
        var result = Part3(Common.Input("everybody_codes_e2024_q02_p3.txt"));
        Assert.AreEqual("11333", result);
    }
    [TestMethod]
    public void Quest2_Part3_Example()
    {
        var input = """
            WORDS:THE,OWE,MES,ROD,RODEO

            HELWORLT
            ENIGWDXL
            TRODEOAL
            """;
        var result = Part3(Common.GetLines(input));
        Assert.AreEqual("10", result);
    }
}