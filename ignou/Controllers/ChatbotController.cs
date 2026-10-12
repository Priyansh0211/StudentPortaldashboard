using ignou.Services;
using Microsoft.AspNetCore.Mvc;

namespace ignou.Controllers
{
    [ApiController]
    [Route("api/chatbot")]
    public class ChatbotController : ControllerBase
    {
        private readonly ChatbotService _chatbotService;

        public ChatbotController(ChatbotService chatbotService)
        {
            _chatbotService = chatbotService;
        }

        // GET: /api/chatbot/categories
        [HttpGet("categories")]
        public async Task<IActionResult> GetCategories()
        {
            var categories =
                await _chatbotService.GetCategoriesAsync();

            return Ok(categories.Select(c => new
            {
                c.Id,
                c.Name,
                c.Description
            }));
        }

        // GET: /api/chatbot/categories/1/faqs
        [HttpGet("categories/{categoryId:int}/faqs")]
        public async Task<IActionResult> GetFAQs(int categoryId)
        {
            if (categoryId <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid category ID."
                });
            }

            var faqs =
                await _chatbotService.GetFAQsByCategoryAsync(categoryId);

            return Ok(faqs.Select(f => new
            {
                f.Id,
                f.Question,
                f.Answer
            }));
        }

        // GET: /api/chatbot/faqs/1
        [HttpGet("faqs/{faqId:int}")]
        public async Task<IActionResult> GetFAQ(int faqId)
        {
            if (faqId <= 0)
            {
                return BadRequest(new
                {
                    message = "Invalid FAQ ID."
                });
            }

            var faq = await _chatbotService.GetFAQByIdAsync(faqId);

            if (faq == null)
            {
                return NotFound(new
                {
                    message = "Question not found."
                });
            }

            return Ok(new
            {
                faq.Id,
                faq.Question,
                faq.Answer,
                FollowUpOptions = faq.FollowUpOptions
                    .Select(o => new
                    {
                        o.Id,
                        o.OptionText,
                        o.NextFaqId
                    })
            });
        }
    }
}