using Newtonsoft.Json;
using RestSharp;

namespace ChatgptBot;

public class ChatGPTClient
{
    private readonly string _apiKey;
    private readonly RestClient _client;
    
    public ChatGPTClient(string apiKey)
    {
        _apiKey = apiKey;
        _client = new RestClient("https://api.openai.com/v1/engines/text-davinci-003/completions");
    }
    
    public string SendMessage(string message)
    {
        if (string.IsNullOrWhiteSpace(message))
        {
            return "Sorry, I didn't receive any input. Please try again!";
        }

        try
        {
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");
            
            var requestBody = new
            {
                prompt = $"Summarize the text delimited by triple backticks into a single sentence. ```{ message}```",  // this is to avoid prompt injection
                max_tokens = 100,
                n = 1,
                stop = (string?)null,
                temperature = 0.7,  // this is the degree of randomness of the model's output
            };
            
            request.AddJsonBody(JsonConvert.SerializeObject(requestBody));
            
            var response = _client.Execute(request);
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response.Content ?? string.Empty);
            return jsonResponse?.choices[0]?.text?.ToString()?.Trim() ?? string.Empty;
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
            return "Sorry, there was an error processing your request. Please try again later.";
        }
    }
}
