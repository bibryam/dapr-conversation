using Dapr.AI.Conversation.Extensions;

class Program
{
  private const string ConversationComponentName = "echo";

  static async Task Main(string[] args)
  {
    const string prompt = "What is Dapr in one sentence?";

    var builder = WebApplication.CreateBuilder(args);
    builder.Services.AddDaprConversationClient();
    var app = builder.Build();

    //Instantiate Dapr Conversation Client
    var conversationClient = app.Services.GetRequiredService<DaprConversationClient>();

    try
    {
      // Send a request to the echo mock LLM component
      var response = await conversationClient.ConverseAsync(ConversationComponentName, [new(prompt, DaprConversationRole.Generic)]);
      Console.WriteLine("Input sent: " + prompt);

      if (response != null)
      {
        Console.Write("Output response:");
        foreach (var resp in response.Outputs)
        {
          Console.WriteLine($" {resp.Result}");
        }
      }
    }
    catch (Exception ex)
    {
      Console.WriteLine("Error: " + ex.Message);
    }
  }
}