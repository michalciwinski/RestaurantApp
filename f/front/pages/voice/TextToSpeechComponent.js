import React, { useState, useEffect } from "react";

const TextToSpeech = ({readyToSpeak }) => {
  const [isPaused, setIsPaused] = useState(false);
  const [utterance, setUtterance] = useState(null);
  

  useEffect(() => {
    const synth = window.speechSynthesis;
    const u = new SpeechSynthesisUtterance();

    setUtterance(u);

    const handleEnd = () => {
      // Clear the content of the 'transcription' element after speaking
      const transcriptionElement = document.getElementById('transcription');
      if (transcriptionElement) {
        transcriptionElement.textContent = '';
      }

    };
    u.addEventListener("end", handleEnd);
    return () => {
      synth.cancel();
      u.removeEventListener("end", handleEnd);
    };
  }, []);

  useEffect(() => {
    console.log(readyToSpeak);
    if (readyToSpeak) {
      console.log('2');
      handlePlay();
      console.log('end');
    }
  }, [readyToSpeak]);

  const handlePlay = () => {
    const synth = window.speechSynthesis;
    const transcriptionElement = document.getElementById('transcription');
    const newText = transcriptionElement ? transcriptionElement.textContent : '';

    utterance.text = newText;

    if (isPaused) {
      synth.resume();
    }
    synth.speak(utterance);
    if (transcriptionElement) {
      transcriptionElement.textContent = '';
    }
    setIsPaused(true);
  };

  const handlePause = () => {
    const synth = window.speechSynthesis;
    synth.pause();
    setIsPaused(true);
  };

  const handleStop = () => {
    const synth = window.speechSynthesis;
    synth.cancel();
    setIsPaused(false);
  };

  return (
    <div>
      
    </div>
  );
};

export default TextToSpeech;















/*import React from "react";
import OpenAI from "openai";


const openai = new OpenAI();

const TextToSpeechComponent = ({ text }) => {
  const handleSpeak = async () => {
    try {
      const mp3 = await openai.audio.speech.create({
        model: "tts-1",
        voice: "alloy",
        input: text,
      });

      const audioBlob = new Blob([await mp3.arrayBuffer()], { type: "audio/mp3" });
      const audioUrl = URL.createObjectURL(audioBlob);

      const audio = new Audio(audioUrl);
      audio.play();
    } catch (error) {
      console.error("Error:", error);
    }
  };

  return (
    <div>
      <p>{text}</p>
      <button onClick={handleSpeak}>Speak</button>
    </div>
  );
};

export default TextToSpeechComponent;*/