using Newtonsoft.Json;
using RestSharp;

namespace ChatgptBot;

public class ChatGptClient
{
    private readonly string _apiKey;
    
    public ChatGptClient(string apiKey)
    {
        _apiKey = apiKey;
    }
    
    public string SendMessage(string message)
    {
        try
        {
            var request = new RestRequest("", Method.Post);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {_apiKey}");
            request.AddJsonBody(JsonConvert.SerializeObject(new
            {
                n = 1,
                max_tokens = 100,
                stop = (string?)null,
                temperature = 0,  // this is the degree of randomness of the model's output
                prompt = $"Summarize the text delimited by triple backticks into a single sentence. ```{message}```",  // this is to avoid prompt injection
            }));
            
            var restClient = new RestClient("https://api.openai.com/v1/engines/text-davinci-003/completions");
            var response = restClient.Execute(request).Content ?? "";
            var jsonResponse = JsonConvert.DeserializeObject<dynamic>(response);
            return jsonResponse?.choices[0]?.text?.ToString()?.Trim() ?? "";
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {JsonConvert.SerializeObject(ex)}");
            return "Unhandled error while processing your request.";
        }
    }
}
