import React from 'react'

export default function Cell({rowId, columnId,isAlive}) {
  return (
    <div style={{width:"100%",height:"100%",border:"1px solid #707071",background:isAlive?"royalblue":"white"}}></div>
  )
}
