import React, { Component } from "react";
//chat wygenrowal kod do podlaczenia sie do chatu ;) na razie tylko do testow
class Chatbot extends Component {
  state = {
    messages: [],
    inputText: "",
  };

  handleInputChange = (e) => {
    this.setState({ inputText: e.target.value });
  };

  handleSendMessage = () => {
    const { inputText, messages } = this.state;

    this.setState({ messages: [...messages, `You: ${inputText}`] });

    const OPENAI_API_KEY = "takichuj"; // Zastąp to swoim kluczem API OpenAI

    const requestOptions = {
        method: "POST",
        headers: {
        "Content-Type": "application/json",
        Authorization: `Bearer ${OPENAI_API_KEY}`,
        },
        body: JSON.stringify({
        model: "gpt-3.5-turbo",
        messages: [
            { role: "user", content: inputText }, // Dodaj treść wiadomości od użytkownika
            { role: "assistant", content: "You are a helpful assistant." }, // Możesz zachować tę linijkę
        ],
        }),
    };

    fetch("https://api.openai.com/v1/engines/gpt-3.5-turbo/completions", requestOptions)
        .then((response) => response.json())
        .then((data) => {
        const chatbotResponse = `Chatbot: ${data.choices[0].message.content}`;
        this.setState({ messages: [...messages, chatbotResponse] });
        })
        .catch((error) => {
        console.error("Error:", error);
        });

    this.setState({ inputText: "" });
    };

  render() {
    const { messages, inputText } = this.state;

    return (
      <div className="chatbot">
        <div className="chat-messages">
          {messages.map((message, index) => (
            <div key={index} className="chat-message">
              {message}
            </div>
          ))}
        </div>
        <input
          type="text"
          value={inputText}
          onChange={this.handleInputChange}
          placeholder="Wpisz wiadomość..."
        />
        <button onClick={this.handleSendMessage}>Wyślij</button>
      </div>
    );
  }
}

export default Chatbot;