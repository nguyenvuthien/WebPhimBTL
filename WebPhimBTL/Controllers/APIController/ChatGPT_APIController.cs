using Microsoft.AspNetCore.Mvc;
using OpenAI_API;
using OpenAI_API.Completions;
using WebPhimBTL.Models.ModelsAPI;

namespace WebPhimBTL.Controllers.APIController
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatGPT_APIController : ControllerBase
    {

        [HttpGet()]
        public async Task<GPT> ChatGPT(string query)
        {
            string a = "";
            OpenAIAPI openai = new OpenAIAPI("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");

            CompletionRequest completionRequest = new CompletionRequest
            {
                Model = OpenAI_API.Models.Model.DavinciText,
                Prompt = query,
                MaxTokens = 2000
            };
            var completion = openai.Completions.CreateCompletionAsync(completionRequest);
            foreach (var item in completion.Result.Completions)
            {
                a += item.Text;
            }

            return new GPT(a);
        }
    }
}
