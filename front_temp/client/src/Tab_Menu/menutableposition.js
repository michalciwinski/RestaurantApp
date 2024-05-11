import React from "react";
import menupositionstyle from './menutableposition.module.css';
import MoreLessButtons from './MoreLessButtons.js'


const tablerowListStyle = menupositionstyle['tablerow-list'];
const tablerowStyle = menupositionstyle['tablerow'];

function Menuposition({ position }) {
  return (
    <div >
        <ul className={tablerowListStyle}>
          <li className={tablerowStyle}>{position.id}</li>
          <li className={tablerowStyle}>{position.name}</li>
          <li className={tablerowStyle}>{position.description}</li>
          <li className={tablerowStyle}>{position.price}</li>
          <li className={tablerowStyle}>{position.dishType}</li>
          <li className={tablerowStyle}><MoreLessButtons/></li>
        </ul>
    </div>
    
  );
}

export default Menuposition;






