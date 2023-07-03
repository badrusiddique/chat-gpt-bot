namespace ChatgptBot;

public class Program
{
    public static void Main(string[] args)
    {
        var client = new ChatGptClient("YOUR_API_KEY_HERE");    // Replace with your ChatGPT API key

        Console.WriteLine("Welcome to the ChatBot!!! anytime type 'exit' to quit.");
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("You: ");
            Console.ResetColor();
            var input = Console.ReadLine() ?? string.Empty;
                
            if (input.ToLower() == "exit")
                break;
                    
            var response = client.SendMessage(input);
                
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("Bot: ");
            Console.ResetColor();
            Console.WriteLine(response);
        }
    }
}