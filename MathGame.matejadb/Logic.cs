namespace MathGame.matejadb;

public class Logic
{
    private int _gameNumber = 0;
    List<History> _histories = new List<History>();
    private static readonly Random _random = new Random();
    public void Game()
    {
        GameMenu();
    }


    void GameMenu()
    {
        Console.WriteLine("==================================");
        Console.WriteLine("Welcome to the Math Game!");
        Console.WriteLine("1. New Random Game");
        Console.WriteLine("2. Practice Addition");
        Console.WriteLine("3. Practice Subtraction");
        Console.WriteLine("4. Practice Multiplication");
        Console.WriteLine("5. Practice Division");
        Console.WriteLine("6. Game History");
        Console.WriteLine("7. Exit");
        Console.WriteLine("==================================");

        SelectOption();
    }

    void SelectOption()
    {
        Console.Write("Select an option: ");
        string? option = Console.ReadLine();

        switch (option)
        {
            case "1":
                Console.Clear();
                NewGame();
                break;
            case "2":
                Console.Clear();
                NewGame("+");
                break;
            case "3":
                Console.Clear();
                NewGame("-");
                break;
            case "4":
                Console.Clear();
                NewGame("*");
                break;
            case "5":
                Console.Clear();
                NewGame("/");
                break;
            case "6":
                Console.Clear();
                ShowHistory();
                GameMenu();
                break;
            case "7":
                Console.WriteLine("See you soon!");
                Environment.Exit(0);
                break;
            default:
                Console.WriteLine("Unknown action! Please try again!");
                GameMenu();
                break;
        }
    }

    void ShowHistory()
    {
        Console.WriteLine("==============HISTORY==============");
        foreach (History history in _histories)
        {
            Console.WriteLine($"Game {history.GameNumber}: {history.Answer}. Points recieved: {history.Points}\n");
        }
        Console.WriteLine("==================================\n");
    }

    void NewGame(string userSelection = null)
    {
        Console.WriteLine("==================================");
        Console.WriteLine("New Game Started. Good luck!");
        Console.WriteLine("==================================");
        _gameNumber++;
        int totalPoints = 0;
        int numberOfRounds = 5;


        for (int i = 0; i < numberOfRounds; i++)
        {
            Console.WriteLine("==================================");
            Console.WriteLine($"Round {i + 1} of {numberOfRounds} \t\t Points: {totalPoints}");

            Question question = GenerateQuestion(userSelection);
            string questionStr = ConvertQuestionToString(question);

            Console.Write($"{i + 1}. {questionStr}");

            
            string? answer = Console.ReadLine();
            bool correctAnswer = CheckAnswer(answer, question);

            totalPoints += correctAnswer ? 1 : 0;

            _histories.Add(new History() { GameNumber = _gameNumber, Answer = $"{questionStr}{answer}", Points = correctAnswer ? 1 : 0});
        }


        Console.WriteLine($"Game ended! Your total points: {totalPoints}/{numberOfRounds}");
        GameMenu();
    }

    static bool CheckAnswer(string answer, Question question)
    {
        double result = 0;

        switch (question.Mark)
        {
            case '+':
                result = question.Number1 + question.Number2;
                break;
            case '-':
                result = question.Number1 - question.Number2;
                break;
            case '*':
                result = question.Number1 * question.Number2;
                break;
            case '/':
                result = question.Number1 / question.Number2;
                break;
            default:
                Console.WriteLine("Something went wrong.");
                break;
        }

        if (int.TryParse(answer, out var userAnswer) && userAnswer == result) return true;

        return false;
    }

    static string ConvertQuestionToString(Question question) {
        return $"{question.Number1} {question.Mark} {question.Number2} = ";
    }

    static Question GenerateQuestion(string selectedOperation)
    {
        char[] marks = ['+', '-', '*', '/'];

        double num1 = _random.Next(0, 101);
        double num2 = _random.Next(1, 101);
        char mark = selectedOperation != null ? Convert.ToChar(selectedOperation) : marks[_random.Next(marks.Length)];

        if (mark == '/' && num1 % num2 != 0)
        {
            return GenerateQuestion(selectedOperation);
        }

        return new Question(num1, num2, mark);


    }
}
