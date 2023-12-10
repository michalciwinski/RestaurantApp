import React,{ useState } from 'react';
import SpeechRecognition, { useSpeechRecognition } from 'react-speech-recognition';
import styles from './voicesite.module.css'
import TextToSpeechComponent from './TextToSpeechComponent';


  export default function voicesite(){
    const [readyToSpeak, setreadyToSpeak] = useState(false);

    const {
      transcript,
      listening,
      browserSupportsSpeechRecognition
    } = useSpeechRecognition();
    const startListening = () => SpeechRecognition.startListening({ continuous: true });
    const stopListening = () => {setreadyToSpeak(true); SpeechRecognition.stopListening();}
  
    if (!browserSupportsSpeechRecognition) {
      return <span>Browser doesn't support speech recognition.</span>;
    }
  
    return (
      <div>

        <div id={styles.speechtToText}>
          <div id={styles.speechtToTextLabel}>
            <div>Microphone: {listening ? 'on' : 'off'}</div>
            <button
              onTouchStart={startListening}
              onMouseDown={startListening}
              onTouchEnd={stopListening}  
              onMouseUp={stopListening}
                  >Hold to talk</button>
          </div>
          <div id='transcription' style ={{display: 'flex', justifyContent: 'center', alignItems: 'center'}} >{transcript}</div>
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

