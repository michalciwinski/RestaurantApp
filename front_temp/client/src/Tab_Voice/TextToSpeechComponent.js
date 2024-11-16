import React, { useState, useEffect } from "react";

const TextToSpeech = ({ cardText }) => {
  const [utterance, setUtterance] = useState(null);
  const [voices, setVoices] = useState([]);

  useEffect(() => {
    const synth = window.speechSynthesis;

    const loadVoices = () => {
      const voices = synth.getVoices();
      setVoices(voices);
    };

    if (synth.onvoiceschanged !== undefined) {
      synth.onvoiceschanged = loadVoices;
    } else {
      loadVoices();
    }

    const u = new SpeechSynthesisUtterance();
    u.lang = 'pl-PL'; // set the language to Polish

    const handleEnd = () => {
      u.text = '';
    };

    u.addEventListener("end", handleEnd);
    setUtterance(u);


    return () => {
      synth.cancel();
      u.removeEventListener("end", handleEnd);
    };
  }, []);

  useEffect(() => {
    if (cardText && utterance) {
      const voice = voices.find(voice => voice.lang === 'pl-PL');
      if (voice) {
        utterance.voice = voice;
      }
      handlePlay();
    }
  }, [cardText, utterance, voices]);

  /*const handlePlay = () => {
    const synth = window.speechSynthesis; 
    if (synth.speaking) {
      synth.cancel();
    }

    utterance.text = cardText;
    synth.speak(utterance);
  };*/

  const handlePlay = () => {
    const synth = window.speechSynthesis;
    if (synth.speaking) {
      synth.cancel();
    }

    // clean up the text by removing the unwanted pattern
    const cleanedText = cardText.replace(/【\d+†source】/g, '');
    console.log("Cleaned Text:", cleanedText);

    // split the text into sentences using multiple delimiters (e.g., periods, colons)
    const sentences = cleanedText.split(/[.-]/).map(sentence => sentence.trim()).filter(sentence => sentence.length > 0);

    const playSentence = (index) => {
      if (index < sentences.length) {
        const sentenceUtterance = new SpeechSynthesisUtterance(sentences[index]);
        const voice = voices.find(voice => voice.lang === 'pl-PL');
        if (voice) {
          sentenceUtterance.voice = voice;
        }
        sentenceUtterance.lang = 'pl-PL';
        sentenceUtterance.onend = () => {
          console.log(`Finished sentence ${index + 1}/${sentences.length}`);
          playSentence(index + 1);
        };
        sentenceUtterance.onerror = (e) => {
          console.error("Speech synthesis error:", e);
        };
        console.log(`Speaking sentence ${index + 1}/${sentences.length}:`, sentences[index]);
        synth.speak(sentenceUtterance);
      }
    };

    playSentence(0);
  };





  const handlePause = () => {//not used
    const synth = window.speechSynthesis;
    if (synth.speaking) {
      synth.pause();
    }
  };

  const handleResume = () => {//not used
    const synth = window.speechSynthesis;
    if (synth.paused) {
      synth.resume();
    }
  };

  const handleStop = () => {//not used
    const synth = window.speechSynthesis;
    if (synth.speaking || synth.paused) {
      synth.cancel();
    }
  };

  return (
    <div>
      {/*<button onClick={handlePlay}>Play</button>
      <button onClick={handlePause}>Pause</button>
      <button onClick={handleResume}>Resume</button>
      <button onClick={handleStop}>Stop</button>*/}
    </div>
  );
};

export default TextToSpeech;

