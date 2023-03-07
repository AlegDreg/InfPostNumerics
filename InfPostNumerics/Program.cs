using InfPostNumerics;

var v = NumbersOps.GetPostfixLine("5 * 3 ^ (4 - 2)");

var r = NumbersOps.CalculatePostfixLine(v);