namespace MathGame.matejadb;

internal class Question(double num1, double num2, char mark)
{
    public double Number1 { get; set; } = num1;
    public double Number2 { get; set; } = num2;
    public char Mark { get; set; } = mark;
}
