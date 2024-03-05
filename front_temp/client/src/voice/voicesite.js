import React,{ useState, useEffect } from 'react';
import SpeechRecognition, { useSpeechRecognition } from 'react-speech-recognition';
import styles from './voicesite.module.css'
import TextToSpeechComponent from './TextToSpeechComponent';
import { AudioOutlined, AudioMutedOutlined, CheckOutlined, DeleteOutlined } from '@ant-design/icons';
import { Button, Card } from 'antd';
import OpenAI from 'openai';


  export default function Voicesite(){

    const [readyToSpeak, setreadyToSpeak] = useState(false);
    const [cardText, setCardText] = useState(null);
    const [dataOai, setDataOai] = useState(null);
    const [conversationHistory, setConversationHistory] = useState([]);
    //const [thread, setThread] = useState(null);


    useEffect(() => {
      loadOpenAi();
    }, []);

    const { transcript, listening, browserSupportsSpeechRecognition, resetTranscript } = useSpeechRecognition();
    const startListening = () => SpeechRecognition.startListening({ continuous: true });
    const stopListening = () => {setreadyToSpeak(true); SpeechRecognition.stopListening();}
  
    if (!browserSupportsSpeechRecognition) {
      return <span>Browser doesn't support speech recognition.</span>;
    }

    async function loadOpenAi(){
      try {
        const OPENAI_API_KEY = process.env.REACT_APP_API_KEY;
        const openai = new OpenAI({
          apiKey: OPENAI_API_KEY, dangerouslyAllowBrowser: true
        });
        setDataOai(openai);
     } catch (error) {
        console.error(error);
     } finally {
        //
     }
    }
    
    async function askGPT() {
      try {

          const newItem = {"role": "user", "content": transcript};
          const addToConversationHistory = (newItem) => {
            setConversationHistory(prevHistory => [...prevHistory, newItem]);
          };
          addToConversationHistory(newItem);

          
          // Retrieve the Assistant
          const assistant_id = process.env.REACT_APP_ASSISTENT_ID;
          const myAssistant = await dataOai.beta.assistants.retrieve(assistant_id);

          // Create a new thread for each conversation
          const threadResponse = await dataOai.beta.threads.create();
  
          // Add a message to the thread with the user's question
          await dataOai.beta.threads.messages.create(threadResponse.id ,{
              role: "user",
              content: transcript
          });
  
          // Run the assistant to get a response
          const run = await dataOai.beta.threads.runs.create(
              threadResponse.id,
              { assistant_id:  assistant_id }
            );
          
          let runStatus = await dataOai.beta.threads.runs.retrieve(
              threadResponse.id,
              run.id
            );
  
          // Polling for run completion
          while (runStatus.status !== 'completed') {
              await new Promise(resolve => setTimeout(resolve, 2000)); // Wait for 2 second
              runStatus = await dataOai.beta.threads.runs.retrieve(threadResponse.id, run.id);
          }

          // Retrieve the messages after the assistant run is complete
          const messagesResponse = await dataOai.beta.threads.messages.list(threadResponse.id, {
            order: 'desc',
            limit: 1
          });

          const aiMessages = messagesResponse.data.filter(msg => msg.role === 'assistant');

          setCardText(aiMessages[aiMessages.length - 1].content[0].text.value);
          return cardText;
      } catch (error) {
          console.error('Error in askGPT:', error.response ? error.response.data : error);
          return 'An error occurred while processing your request.'; // Placeholder response
      }
  }

    return (
      <div>

        <div id={styles.speechtToText}>

          <div id={styles.speechtToTextLabel}>TALK WITH ROBOT</div>

          <Button className={styles.microbutton} type="primary" icon={<AudioOutlined/>} onClick={startListening} ></Button>
          <Button className={styles.microbutton} type="primary" icon={<AudioMutedOutlined/>} onClick={stopListening} ></Button>

          <div id={styles.listInfo}>Microphone: {listening ? 'on' : 'off'}</div>

          <Card id={styles.inputBox}>{transcript}</Card>

          <div id={styles.confirmation}>
            <Button className={styles.confbutton} type="default" icon={<CheckOutlined/>} onClick={askGPT}> Confirm</Button>
            <Button className={styles.confbutton} type="default" icon={<DeleteOutlined/>} onClick={resetTranscript}> Delete</Button>
          </div>
          
          <Card id={styles.outputBox}>{cardText}</Card>

        </div>

        




        
        <div id={styles.textToSpeech}>
          <div id={styles.textToSpeechLabel}>
            Response from robot:
            <TextToSpeechComponent readyToSpeak={readyToSpeak}/>
          </div>
        </div>


      </div>
    );



  };

