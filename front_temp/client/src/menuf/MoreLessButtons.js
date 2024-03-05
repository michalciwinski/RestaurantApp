import React from "react";
import Counter from "./Counter"
import { useState } from 'react';
import menupositionstyle from './menutableposition.module.css';


const CounterButtons = menupositionstyle['counterbuttons'];
const Buttons = menupositionstyle['buttons'];

function MoreLessButton(){

    var [count,setCount] = useState(0)

    function Add(){
            setCount(count + 1)
    }

    function Subtract (){
        if(count > 0) {
            setCount(count - 1)
        }
    }


    return (
        <div className={CounterButtons}>
            <button className = {Buttons} onClick={Add}>+</button>
            <Counter count={count}/>
            <button className = {Buttons} onClick={Subtract}>-</button>
        </div>
    )

}

export default MoreLessButton;