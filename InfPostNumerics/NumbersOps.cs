namespace InfPostNumerics
{
    public static class NumbersOps
    {
        private static List<string> ParseInput(string infixLine)
        {
            List<string> input = new List<string>();

            int fromid = -1;
            for (int i = 0; i < infixLine.Length; i++)
            {
                if (!infixLine[i].isDigit())
                {
                    if (fromid != -1)
                    {
                        var t = infixLine.Skip(fromid).Take(i - fromid).ToArray();
                        var r = "";
                        for (int j = 0; j < t.Count(); j++)
                        {
                            r += t[j];
                        }
                        input.Add(r);
                        fromid = -1;
                    }

                    input.Add(infixLine[i].ToString());
                }
                else
                {
                    if (fromid == -1)
                    {
                        fromid = i;
                    }
                }
            }

            if (fromid != -1)
            {
                var t1 = infixLine.Skip(fromid).Take(infixLine.Length - fromid).ToArray();
                var r1 = "";
                for (int j = 0; j < t1.Count(); j++)
                {
                    r1 += t1[j];
                }
                input.Add(r1);
            }

            return input;
        }

        public static List<string> GetPostfixLine(string infixLine)
        {
            infixLine = infixLine.Replace(" ", "");
            var input = ParseInput(infixLine);

            List<string> resline = new List<string>();
            List<string> res = new List<string>();

            for (int i = 0; i < input.Count; i++)
            {
                if (input[i].isDigit())
                {
                    resline.Add(input[i]);
                }
                else
                {
                    if (res.Count == 0 || input[i] == "(")
                    {
                        res.Add(input[i]);
                    }
                    else if (input[i] == ")")
                    {
                        PopItems(resline, res, new List<string> { "(" }, true);
                    }
                    else if (input[i] == "*" || input[i] == "/")
                    {
                        PopItems(resline, res, new List<string> { "+", "-" }, false);
                        res.Add(input[i]);
                    }
                    else
                    {
                        res.Add(input[i]);
                    }
                }
            }

            if (res.Count > 0)
            {
                PopItems(resline, res, new List<string>(), false);
            }

            return resline;
        }

        private static void PopItems(List<string> resline, List<string> res,
            List<string> stopItems, bool deleteLastItem)
        {
            for (int j = res.Count - 1; j >= 0; j--)
            {
                if (stopItems.Contains(res[j]))
                {
                    if (deleteLastItem)
                        res.RemoveAt(j);
                    break;
                }
                resline.Add(res[j]);
                res.RemoveAt(j);
            }
        }

        private static bool isDigit(this char c)
        {
            return Int32.TryParse(c.ToString(), out _);
        }

        private static bool isDigit(this string c)
        {
            return Int32.TryParse(c, out _);
        }

        private static int GetDigit(this string c)
        {
            return int.Parse(c);
        }

        public static int CalculatePostfixLine(List<string> infixLine)
        {
            List<string> res = new List<string>();

            for (int i = 0; i < infixLine.Count; i++)
            {
                if (infixLine[i].isDigit())
                {
                    res.Add(infixLine[i].ToString());
                }
                else
                {
                    var it1 = res[res.Count - 2];
                    var it2 = res[res.Count - 1];
                    res.RemoveAt(res.Count - 1);
                    res.RemoveAt(res.Count - 1);
                    int r = 0;
                    switch (infixLine[i])
                    {
                        case "+":
                            r = it1.GetDigit() + it2.GetDigit();
                            break;
                        case "-":
                            r = it1.GetDigit() - it2.GetDigit();
                            break;
                        case "*":
                            r = it1.GetDigit() * it2.GetDigit();
                            break;
                        case "/":
                            r = it1.GetDigit() / it2.GetDigit();
                            break;
                    }

                    if (r != 0)
                    {
                        res.Add(r.ToString());
                    }
                }
            }

            return res[0].GetDigit();
        }
    }
}