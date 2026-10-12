// (() => {
//     Prevent duplicate initialization if the script is included twice.
//     if (window.vmouChatbotInitialized) return;
//     window.vmouChatbotInitialized = true;

//     document.addEventListener("DOMContentLoaded", initializeChatbot);

//     async function initializeChatbot() {
//         const root = document.getElementById("vmou-chatbot");
//         if (!root) return;

//         const toggle = root.querySelector("#chatbotToggle");
//         const close = root.querySelector("#chatbotClose");
//         const windowPanel = root.querySelector("#chatbotWindow");
//         const messages = root.querySelector("#chatbotMessages");
//         const categoriesBox = root.querySelector("#chatbotCategories");

//         function setOpen(isOpen) {
//             windowPanel.classList.toggle("is-open", isOpen);
//             windowPanel.setAttribute("aria-hidden", String(!isOpen));
//             toggle.setAttribute("aria-expanded", String(isOpen));
//             toggle.setAttribute(
//                 "aria-label",
//                 isOpen ? "Close chatbot" : "Open chatbot"
//             );
//         }

//         toggle.addEventListener("click", () => {
//             setOpen(!windowPanel.classList.contains("is-open"));
//         });

//         close.addEventListener("click", () => setOpen(false));

//         function scrollToBottom() {
//             messages.scrollTop = messages.scrollHeight;
//         }

//         function addMessage(text, type = "bot-message") {
//             const message = document.createElement("div");
//             message.className = `chatbot-message ${type}`;

//             Use textContent so database content is rendered as text.
//             message.textContent = text;

//             messages.appendChild(message);
//             scrollToBottom();
//             return message;
//         }

//         function addOptions(container, options, onSelect) {
//             container.replaceChildren();

//             options.forEach(option => {
//                 const button = document.createElement("button");
//                 button.type = "button";
//                 button.className = "chatbot-option";
//                 button.textContent = option.label;

//                 button.addEventListener("click", () => {
//                     onSelect(option);
//                 });

//                 container.appendChild(button);
//             });
//         }

//         function addRestartButton() {
//             const button = document.createElement("button");
//             button.type = "button";
//             button.className = "chatbot-restart";
//             button.textContent = "← Back to all topics";

//             button.addEventListener("click", loadCategories);

//             messages.appendChild(button);
//             scrollToBottom();
//         }

//         async function getJson(url) {
//             const response = await fetch(url, {
//                 headers: { "Accept": "application/json" }
//             });

//             if (!response.ok) {
//                 throw new Error(`Request failed (${response.status})`);
//             }

//             return response.json();
//         }

//         function showError(container, message) {
//             container.replaceChildren();

//             const error = document.createElement("p");
//             error.className = "chatbot-error";
//             error.textContent = message;

//             container.appendChild(error);
//         }

//         async function loadCategories() {
//             messages.replaceChildren();

//             addMessage(
//                 "Welcome to VMOU! How can I help you today? Select a topic."
//             );

//             const optionsBox = document.createElement("div");
//             optionsBox.className = "chatbot-options";
//             messages.appendChild(optionsBox);

//             try {
//                 const categories = await getJson("/api/chatbot/categories");

//                 if (!categories.length) {
//                     showError(optionsBox, "No help topics are available yet.");
//                     return;
//                 }

//                 addOptions(
//                     optionsBox,
//                     categories.map(category => ({
//                         label: category.name,
//                         id: category.id
//                     })),
//                     selectCategory
//                 );
//             } catch (error) {
//                 showError(
//                     optionsBox,
//                     "Unable to load help topics. Please try again later."
//                 );
//                 console.error("Chatbot categories:", error);
//             }

//             scrollToBottom();
//         }

//         async function selectCategory(category) {
//             addMessage(category.label, "user-message");

//             const loading = addMessage("Loading questions...");

//             try {
//                 const faqs = await getJson(
//                     `/api/chatbot/categories/${category.id}/faqs`
//                 );

//                 loading.remove();

//                 if (!faqs.length) {
//                     addMessage("No questions are available for this topic yet.");
//                     addRestartButton();
//                     return;
//                 }

//                 const optionsBox = document.createElement("div");
//                 optionsBox.className = "chatbot-options";
//                 messages.appendChild(optionsBox);

//                 addOptions(
//                     optionsBox,
//                     faqs.map(faq => ({
//                         label: faq.question,
//                         id: faq.id
//                     })),
//                     selectFAQ
//                 );

//                 addRestartButton();
//                 scrollToBottom();
//             } catch (error) {
//                 loading.remove();
//                 addMessage("Unable to load questions. Please try again.");
//                 addRestartButton();
//                 console.error("Chatbot FAQs:", error);
//             }
//         }

//         async function selectFAQ(faqOption) {
//             addMessage(faqOption.label, "user-message");

//             const loading = addMessage("Finding your answer...");

//             try {
//                 const faq = await getJson(
//                     `/api/chatbot/faqs/${faqOption.id}`
//                 );

//                 loading.remove();
//                 addMessage(faq.answer);

//                 const followUps = (faq.followUpOptions || [])
//                     .filter(option => option.nextFaqId)
//                     .map(option => ({
//                         label: option.optionText,
//                         id: option.nextFaqId
//                     }));

//                 if (followUps.length) {
//                     const optionsBox = document.createElement("div");
//                     optionsBox.className = "chatbot-options";
//                     messages.appendChild(optionsBox);

//                     addOptions(optionsBox, followUps, selectFAQ);
//                 }

//                 addRestartButton();
//                 scrollToBottom();
//             } catch (error) {
//                 loading.remove();
//                 addMessage("Unable to load the answer. Please try again.");
//                 addRestartButton();
//                 console.error("Chatbot answer:", error);
//             }
//         }

//         categoriesBox.replaceChildren();

//         Start loading when the page is ready.
//         await loadCategories();
//     }
// })();


(() => {
    // Prevent duplicate initialization if the script is included twice.
    if (window.vmouChatbotInitialized) return;
    window.vmouChatbotInitialized = true;

    document.addEventListener("DOMContentLoaded", initializeChatbot);

    async function initializeChatbot() {
        const root = document.getElementById("vmou-chatbot");
        if (!root) return;

        const toggle = root.querySelector("#chatbotToggle");
        const close = root.querySelector("#chatbotClose");
        const windowPanel = root.querySelector("#chatbotWindow");
        const messages = root.querySelector("#chatbotMessages");
        const categoriesBox = root.querySelector("#chatbotCategories");

        function setOpen(isOpen) {
            windowPanel.classList.toggle("is-open", isOpen);
            windowPanel.setAttribute("aria-hidden", String(!isOpen));
            toggle.setAttribute("aria-expanded", String(isOpen));
            toggle.setAttribute(
                "aria-label",
                isOpen ? "Close chatbot" : "Open chatbot"
            );
        }

        toggle.addEventListener("click", () => {
            setOpen(!windowPanel.classList.contains("is-open"));
        });

        close.addEventListener("click", () => setOpen(false));

        function scrollToBottom() {
            messages.scrollTop = messages.scrollHeight;
        }

        function addMessage(text, type = "bot-message") {
            const message = document.createElement("div");
            message.className = `chatbot-message ${type}`;

            // Use textContent so database content is rendered as text.
            message.textContent = text;

            messages.appendChild(message);
            scrollToBottom();
            return message;
        }

        function addOptions(container, options, onSelect) {
            container.replaceChildren();

            options.forEach(option => {
                const button = document.createElement("button");
                button.type = "button";
                button.className = "chatbot-option";
                button.textContent = option.label;

                button.addEventListener("click", () => {
                    onSelect(option);
                });

                container.appendChild(button);
            });
        }

        function addRestartButton() {
            const button = document.createElement("button");
            button.type = "button";
            button.className = "chatbot-restart";
            button.textContent = "← Back to all topics";

            button.addEventListener("click", loadCategories);

            messages.appendChild(button);
            scrollToBottom();
        }

        async function getJson(url) {
            const response = await fetch(url, {
                headers: { "Accept": "application/json" }
            });

            if (!response.ok) {
                throw new Error(`Request failed (${response.status})`);
            }

            return response.json();
        }

        function showError(container, message) {
            container.replaceChildren();

            const error = document.createElement("p");
            error.className = "chatbot-error";
            error.textContent = message;

            container.appendChild(error);
        }

        async function loadCategories() {
            messages.replaceChildren();

            addMessage(
                "Welcome to VMOU! How can I help you today? Select a topic."
            );

            const optionsBox = document.createElement("div");
            optionsBox.className = "chatbot-options";
            messages.appendChild(optionsBox);

            try {
                const categories = await getJson("/api/chatbot/categories");

                if (!categories.length) {
                    showError(optionsBox, "No help topics are available yet.");
                    return;
                }

                addOptions(
                    optionsBox,
                    categories.map(category => ({
                        label: category.name,
                        id: category.id
                    })),
                    selectCategory
                );
            } catch (error) {
                showError(
                    optionsBox,
                    "Unable to load help topics. Please try again later."
                );
                console.error("Chatbot categories:", error);
            }

            scrollToBottom();
        }

        async function selectCategory(category) {
            addMessage(category.label, "user-message");

            const loading = addMessage("Loading questions...");

            try {
                const faqs = await getJson(
                    `/api/chatbot/categories/${category.id}/faqs`
                );

                loading.remove();

                if (!faqs.length) {
                    addMessage("No questions are available for this topic yet.");
                    addRestartButton();
                    return;
                }

                const optionsBox = document.createElement("div");
                optionsBox.className = "chatbot-options";
                messages.appendChild(optionsBox);

                addOptions(
                    optionsBox,
                    faqs.map(faq => ({
                        label: faq.question,
                        id: faq.id
                    })),
                    selectFAQ
                );

                addRestartButton();
                scrollToBottom();
            } catch (error) {
                loading.remove();
                addMessage("Unable to load questions. Please try again.");
                addRestartButton();
                console.error("Chatbot FAQs:", error);
            }
        }

        async function selectFAQ(faqOption) {
            addMessage(faqOption.label, "user-message");

            const loading = addMessage("Finding your answer...");

            try {
                const faq = await getJson(
                    `/api/chatbot/faqs/${faqOption.id}`
                );

                loading.remove();
                addMessage(faq.answer);

                const followUps = (faq.followUpOptions || [])
                    .filter(option => option.nextFaqId)
                    .map(option => ({
                        label: option.optionText,
                        id: option.nextFaqId
                    }));

                if (followUps.length) {
                    const optionsBox = document.createElement("div");
                    optionsBox.className = "chatbot-options";
                    messages.appendChild(optionsBox);

                    addOptions(optionsBox, followUps, selectFAQ);
                }

                addRestartButton();
                scrollToBottom();
            } catch (error) {
                loading.remove();
                addMessage("Unable to load the answer. Please try again.");
                addRestartButton();
                console.error("Chatbot answer:", error);
            }
        }

        categoriesBox.replaceChildren();

        // Start loading when the page is ready.
        await loadCategories();
    }
})();
