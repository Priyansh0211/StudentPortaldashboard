using ignou.Models;
using Microsoft.EntityFrameworkCore;

namespace ignou.Data
{
    public static class ChatbotDataSeeder
    {
        public static async Task SeedAsync(AppDbContext db)
        {
            // Do not insert the initial data again if it already exists.
            if (await db.ChatbotCategories.AnyAsync())
            {
                return;
            }

            var general = new ChatbotCategory
            {
                Name = "General Help",
                Description = "General portal guidance"
            };

            var registration = new ChatbotCategory
            {
                Name = "Registration",
                Description = "Account creation and registration help"
            };

            var login = new ChatbotCategory
            {
                Name = "Login",
                Description = "Login and account access help"
            };

            var dashboard = new ChatbotCategory
            {
                Name = "Student Dashboard",
                Description = "Profile and dashboard guidance"
            };

            var assignments = new ChatbotCategory
            {
                Name = "Assignments",
                Description = "Assignment-related guidance"
            };

            var payments = new ChatbotCategory
            {
                Name = "Payments",
                Description = "Payment-related guidance"
            };

            db.ChatbotCategories.AddRange(
                general,
                registration,
                login,
                dashboard,
                assignments,
                payments
            );

            await db.SaveChangesAsync();

            // Initial FAQ questions and answers
            var faqData = new List<ChatbotFaq>
            {
                new()
                {
                    CategoryId = general.Id,
                    Question = "How can I get help?",
                    Answer = "Select a topic from the chatbot menu. "
                        + "You can then select a question to view its answer.",
                    DisplayOrder = 1
                },
                new()
                {
                    CategoryId = general.Id,
                    Question = "How do I navigate this portal?",
                    Answer = "Use the navigation links available on the "
                        + "current page to open the required portal section.",
                    DisplayOrder = 2
                },

                new()
                {
                    CategoryId = registration.Id,
                    Question = "How do I create an account?",
                    Answer = "Open the Register page, complete the required "
                        + "fields and submit the form. Follow the message "
                        + "shown by the portal after submission.",
                    DisplayOrder = 1
                },
                new()
                {
                    CategoryId = registration.Id,
                    Question = "Which details are required during registration?",
                    Answer = "Complete all fields marked as required on the "
                        + "registration form. Check the field validation "
                        + "messages if any information is missing or invalid.",
                    DisplayOrder = 2
                },
                new()
                {
                    CategoryId = registration.Id,
                    Question = "What should I do if registration fails?",
                    Answer = "Review the validation messages, check the "
                        + "information you entered and try again. If the "
                        + "problem continues, contact the portal administrator.",
                    DisplayOrder = 3
                },

                new()
                {
                    CategoryId = login.Id,
                    Question = "How do I log in?",
                    Answer = "Open the Login page, enter your registered "
                        + "email address and password, and select Sign In.",
                    DisplayOrder = 1
                },
                new()
                {
                    CategoryId = login.Id,
                    Question = "What should I do if my login fails?",
                    Answer = "Check your email address and password, then "
                        + "try again. If the issue continues, contact the "
                        + "portal administrator.",
                    DisplayOrder = 2
                },
                new()
                {
                    CategoryId = login.Id,
                    Question = "How can I get help with my password?",
                    Answer = "If password recovery is not available on the "
                        + "portal, contact the portal administrator for "
                        + "assistance. Never share your password with anyone.",
                    DisplayOrder = 3
                },

                new()
                {
                    CategoryId = dashboard.Id,
                    Question = "Where can I find my profile?",
                    Answer = "After logging in, open the Profile or My Profile "
                        + "section from the student dashboard.",
                    DisplayOrder = 1
                },
                new()
                {
                    CategoryId = dashboard.Id,
                    Question = "Where can I find assignments?",
                    Answer = "Open the Assignments section from the student "
                        + "dashboard, if it is available for your account.",
                    DisplayOrder = 2
                },
                new()
                {
                    CategoryId = dashboard.Id,
                    Question = "How do I open the payment section?",
                    Answer = "Open the Payment section of your dashboard, if that feature is available.",
                    DisplayOrder = 3
                },

                new()
                {
                    CategoryId = assignments.Id,
                    Question = "Where can I find assignment details?",
                    Answer = "Open the Assignments section in your dashboard "
                        + "to view the assignment information available to you.",
                    DisplayOrder = 1
                },
                new()
                {
                    CategoryId = assignments.Id,
                    Question = "What if assignment details are missing?",
                    Answer = "Check again later and contact the portal "
                        + "administrator if the required assignment details "
                        + "are still unavailable.",
                    DisplayOrder = 2
                },

                new()
                {
                    CategoryId = payments.Id,
                    Question = "Where can I view payment information?",
                    Answer = "Open the Payment section of your dashboard "
                        + "to view payment information, if available.",
                    DisplayOrder = 1
                },
                new()
                {
                    CategoryId = payments.Id,
                    Question = "What if my payment status is not updated?",
                    Answer = "Check the payment details again after some time. "
                        + "If the status remains incorrect, contact the "
                        + "portal administrator with your transaction details. "
                        + "Never share your password or OTP.",
                    DisplayOrder = 2
                }
            };

            db.ChatbotFAQs.AddRange(faqData);

            await db.SaveChangesAsync();

            // Map each question to its database record.
            var faqByQuestion = faqData.ToDictionary(
                f => f.Question,
                StringComparer.OrdinalIgnoreCase
            );

            // Option text is displayed as a clickable follow-up question.
            // NextFaqId identifies the FAQ opened after selection.
            var optionData = new List<ChatbotOption>
            {
                new()
                {
                    FaqId = faqByQuestion["How can I get help?"].Id,
                    OptionText = "How do I create an account?",
                    NextFaqId = faqByQuestion["How do I create an account?"].Id,
                    DisplayOrder = 1
                },
                new()
                {
                    FaqId = faqByQuestion["How can I get help?"].Id,
                    OptionText = "How do I log in?",
                    NextFaqId = faqByQuestion["How do I log in?"].Id,
                    DisplayOrder = 2
                },

                new()
                {
                    FaqId = faqByQuestion["How do I create an account?"].Id,
                    OptionText = "Which details are required?",
                    NextFaqId = faqByQuestion[
                        "Which details are required during registration?"
                    ].Id,
                    DisplayOrder = 1
                },
                new()
                {
                    FaqId = faqByQuestion["How do I create an account?"].Id,
                    OptionText = "Registration failed",
                    NextFaqId = faqByQuestion[
                        "What should I do if registration fails?"
                    ].Id,
                    DisplayOrder = 2
                },
                new()
                {
                    FaqId = faqByQuestion[
                        "Which details are required during registration?"
                    ].Id,
                    OptionText = "Registration failed",
                    NextFaqId = faqByQuestion[
                        "What should I do if registration fails?"
                    ].Id,
                    DisplayOrder = 1
                },

                new()
                {
                    FaqId = faqByQuestion["How do I log in?"].Id,
                    OptionText = "Login is not working",
                    NextFaqId = faqByQuestion[
                        "What should I do if my login fails?"
                    ].Id,
                    DisplayOrder = 1
                },
                new()
                {
                    FaqId = faqByQuestion["How do I log in?"].Id,
                    OptionText = "Password help",
                    NextFaqId = faqByQuestion[
                        "How can I get help with my password?"
                    ].Id,
                    DisplayOrder = 2
                },

                new()
                {
                    FaqId = faqByQuestion["Where can I find my profile?"].Id,
                    OptionText = "Where can I find assignments?",
                    NextFaqId = faqByQuestion[
                        "Where can I find assignments?"
                    ].Id,
                    DisplayOrder = 1
                },
                new()
                {
                    FaqId = faqByQuestion["Where can I find assignments?"].Id,
                    OptionText = "Assignment details are missing",
                    NextFaqId = faqByQuestion[
                        "Where can I find assignment details?"
                    ].Id,
                    DisplayOrder = 1
                },

                new()
                {
                    FaqId = faqByQuestion[
                        "Where can I view payment information?"
                    ].Id,
                    OptionText = "Payment status is not updated",
                    NextFaqId = faqByQuestion[
                        "What if my payment status is not updated?"
                    ].Id,
                    DisplayOrder = 1
                }
            };

            db.ChatbotOptions.AddRange(optionData);

            await db.SaveChangesAsync();
        }
    }
}
