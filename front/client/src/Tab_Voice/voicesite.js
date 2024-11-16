import React,{ useState, useEffect, createContext } from 'react';
import SpeechRecognition, { useSpeechRecognition } from 'react-speech-recognition';
import styles from './voicesite.module.css'
import TextToSpeechComponent from './TextToSpeechComponent';
import { AudioOutlined, AudioMutedOutlined, CheckOutlined, DeleteOutlined } from '@ant-design/icons';
import { Button, Card } from 'antd';
import OpenAI from 'openai';
import axiosService from '../api/axiosService';
import RecordToAdd from './recordToAdd';




  export default function Voicesite(){

    const [readyToSpeak, setReadyToSpeak] = useState(false);
    const [cardText, setCardText] = useState(null);
    const [dataOai, setDataOai] = useState(null);
    const [conversationHistory, setConversationHistory] = useState([]);
    const [matchedDishes, setMatchedDishes] = useState([]);



    useEffect(() => {
      loadOpenAi();
    }, []);

    const { transcript, listening, browserSupportsSpeechRecognition, resetTranscript } = useSpeechRecognition();
    const startListening = () => SpeechRecognition.startListening({ continuous: true,  language: 'pl'  });
    const stopListening = () => {SpeechRecognition.stopListening();}

  
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
        setCardText("Witamy w naszej restauracji. W czym mogę pomóc?");
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

          
          //połacz z asystentem 
          const assistant_id = process.env.REACT_APP_ASSISTENT_ID;
          const myAssistant = await dataOai.beta.assistants.retrieve(assistant_id);

          //nowy thread 
          const threadResponse = await dataOai.beta.threads.create();
  
          //przekazanie transcript do wiadomosci 
          await dataOai.beta.threads.messages.create(threadResponse.id ,{
              role: "user",
              content: transcript
          });
  
          //run
          const run = await dataOai.beta.threads.runs.create(
              threadResponse.id,
              { assistant_id:  assistant_id }
            );
          
          let runStatus = await dataOai.beta.threads.runs.retrieve(
              threadResponse.id,
              run.id
            );
  
          //co 2sec sprawdzenie
          while (runStatus.status !== 'completed') {
              await new Promise(resolve => setTimeout(resolve, 2000));
              runStatus = await dataOai.beta.threads.runs.retrieve(threadResponse.id, run.id);
          }

          //pobranie odpowiedzi gdy gotowa
          const messagesResponse = await dataOai.beta.threads.messages.list(threadResponse.id, {
            order: 'desc',
            limit: 1
          });

          const aiMessages = messagesResponse.data.filter(msg => msg.role === 'assistant');

          const robotText_temp = aiMessages[aiMessages.length - 1].content[0].text.value;
          console.log(robotText_temp);
          const robotText = robotText_temp.replace(/【\d+†source】/g, '');
          console.log(robotText);


          //analiza tekstu i znajdywanie dań
          const regex = /\*(.*?)\*/g;
          let matches = [];
          let match;
          while ((match = regex.exec(robotText)) !== null) {
            matches.push(match[1]);
          }

          // check database
          let dishesData;
          console.log(matches);
          const filteredMatches = matches.filter(element => element.length >= 3); //przefiltrowanie smieci
          await axiosService.getDishes().then((res) => {
            dishesData = res.data;
          })
          .catch((err) => {
            console.log(err);
          });

          const matchedDishes = dishesData.filter(dish => {
            return filteredMatches.some(match => dish.name.toLowerCase() === match.toLowerCase());
          }).map(dish => {
            return { id: dish.id, name: dish.name, srcPic: dish.srcPic, price: dish.price };
          });
          setMatchedDishes(matchedDishes);

          setCardText(robotText.replace(/\*/g, ''));

          setReadyToSpeak(true); 
          //return cardText;
      } catch (error) {
          console.error('Error:', error.response ? error.response.data : error);
          return 'Błąd podczas przetwarzania zapytania';
      }
  }

  async function test(){//not used - only to test - to DELETE
    try
      {
        const robotText = "Na podstawie przeglądu menu, oto pizze, które są dostępne:- *Pizza Neapolitana*: Koncentrat pomidorowy, Oliwa, Mozzarella, Bazyli - *Pizza Amerykańska*: Koncentrat pomidorowy, Szynka, Cebula, Ser żółty, Kurczak- *Pizza Salami*: Koncentrat pomidorowy, Ser żółty, Oliwa, Salami【17†source】.";
        //test to delete

        const cleanedText = robotText.replace(/【\d+†source】/g, '');
        console.log(cleanedText)

        setCardText(robotText.replace(/\*/g, ''));
     
        console.log(robotText);
        //analyze text and find correct dishes
        const regex = /\*(.*?)\*/g;
        const matches = [];
        let match;
        while ((match = regex.exec(robotText)) !== null) {
          matches.push(match[1]);
        }

        // check database
        let dishesData;
        const filteredMatches = matches.filter(element => element.length >= 3); // filter rubbish
        await axiosService.getDishes().then((res) => {
          dishesData = res.data;
        })
        .catch((err) => {
          console.log(err);
        });

        const matchedDishes = dishesData.filter(dish => {
          return filteredMatches.some(match => dish.name.toLowerCase() === match.toLowerCase());
        }).map(dish => {
          return { id: dish.id, name: dish.name, srcPic: dish.srcPic, price: dish.price };
        });
        setMatchedDishes(matchedDishes);

        //to test
        setReadyToSpeak(true); 

      }
      catch{

      }
  }
  async function test2(){//not used - only to test - to DELETE
    try
      {
        const robotText = "hello *Burger Drwala*";
        //test to delete
        setCardText(robotText.replace(/\*/g, ''));
        console.log(robotText);
        //analyze text and find correct dishes
        const regex = /\*(.*?)\*/g;
        const matches = [];
        let match;
        while ((match = regex.exec(robotText)) !== null) {
          matches.push(match[1]);
        }

        // check database
        let dishesData;
        const filteredMatches = matches.filter(element => element.length >= 3); // filter rubbish
        await axiosService.getDishes().then((res) => {
          dishesData = res.data;
        })
        .catch((err) => {
          console.log(err);
        });

        const matchedDishes = dishesData.filter(dish => {
          return filteredMatches.some(match => dish.name.toLowerCase() === match.toLowerCase());
        }).map(dish => {
          return { id: dish.id, name: dish.name, srcPic: dish.srcPic, price: dish.price };
        });
        setMatchedDishes(matchedDishes);

        //to test
        setReadyToSpeak(true); 
      }
      catch{

      }
  }

    return (
      <div>

          <div id={styles.speechtToText}>

          <div id={styles.speechtToTextLabel}>Porozmawiaj z robotem</div>

          <Button className={styles.microbutton} type="primary" icon={<AudioOutlined/>} onClick={startListening} ></Button>
          <Button className={styles.microbutton} type="primary" icon={<AudioMutedOutlined/>} onClick={stopListening} ></Button>

          <div id={styles.listInfo}>Mikrofon: {listening ? 'on' : 'off'}</div>

          <Card id={styles.inputBox}>{transcript}</Card>

          <div id={styles.confirmation}>
            <Button className={styles.confbutton} type="default" icon={<CheckOutlined/>} onClick={askGPT}> Potwierdź</Button>
            <Button className={styles.confbutton} type="default" icon={<DeleteOutlined/>} onClick={resetTranscript}> Usuń</Button>
          </div>

          <Card id={styles.outputBox}>{cardText}</Card>

          </div>

          <div id={styles.textToSpeech}>
            <div id={styles.textToSpeechLabel}>
              <TextToSpeechComponent readyToSpeak={readyToSpeak} cardText={cardText} />
            </div>
          </div>

          <div>

          {matchedDishes.length > 0 && (
          <div id={styles.dishList}>
            {matchedDishes.map(dish => (
              <RecordToAdd key={dish.id} {...dish} />
            ))}
          </div>
          )}


          {/*<button id ="asd" onClick={test}>click</button>
          <button id ="asdasd" onClick={test2}>click</button>*/}

          </div>


      </div>
    );



  };

