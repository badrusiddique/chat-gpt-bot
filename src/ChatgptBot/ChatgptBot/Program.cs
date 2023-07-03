namespace ChatgptBot;

public class Program
{
    public static void Main(string[] args)
    {
        var apiKey = "YOUR_API_KEY_HERE";   // // Replace with your ChatGPT API key
        var client = new ChatGPTClient(apiKey);
            
        Console.WriteLine("Welcome to the ChatGPT bot! Type 'exit' to quit.");
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
            Console.Write("Chatbot: ");
            Console.ResetColor();
            Console.WriteLine(response);
        }
    }
}