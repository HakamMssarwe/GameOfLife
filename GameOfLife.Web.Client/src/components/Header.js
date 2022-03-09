import React, { useState } from 'react'
import { Button, Container, Nav, Navbar, NavDropdown } from 'react-bootstrap'

export default function Header() {

const [boardSize, setBoardSize] = useState("Size");
const [population, setPopulation] = useState("Seed");
const [rule, setRule] = useState("Policy");
const [growthSpeed, setGrowthSpeed] = useState("Speed");


  return (
    <Navbar bg="light" expand="lg" style={{marginTop:20}}>
  <Container>
    <Navbar.Brand href="/">Game of life</Navbar.Brand>
    <Navbar.Toggle aria-controls="basic-navbar-nav" />
    <Navbar.Collapse id="basic-navbar-nav">
      <Nav className="me-auto">
        <NavDropdown title={boardSize} id="boardSize">
          <NavDropdown.Item onClick={e => setBoardSize("6x6")}>6x6</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setBoardSize("8x8")}>8x8</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setBoardSize("10x10")}>10x10</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setBoardSize("15x15")}>15x15</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setBoardSize("20x20")}>20x20</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setBoardSize("30x30")}>30x30</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setBoardSize("50x50")}>50x50</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setBoardSize("75x75")}>75x75</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setBoardSize("100x100")}>100x100</NavDropdown.Item>
        </NavDropdown>
        <NavDropdown title={population} id="population">
          <NavDropdown.Item onClick={e => setPopulation("Small")}>Small</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setPopulation("Medium")}>Medium</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setPopulation("Large")}>Large</NavDropdown.Item>
        </NavDropdown>
        <NavDropdown title={rule} id="rule">
          <NavDropdown.Item onClick={e => setRule("Conway")}>Conway</NavDropdown.Item>
        </NavDropdown>
        <NavDropdown title={growthSpeed} id="rule">
          <NavDropdown.Item onClick={e => setGrowthSpeed("Very Slow")}>Very Slow</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setGrowthSpeed("Slow")}>Slow</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setGrowthSpeed("Normal")}>Normal</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setGrowthSpeed("Fast")}>Fast</NavDropdown.Item>
          <NavDropdown.Item onClick={e => setGrowthSpeed("Very Fast")}>Very Fast</NavDropdown.Item>
        </NavDropdown>
      </Nav>
        <div style={{display:"flex"}}>
        <Button variant="primary" style={{width:"8em", marginRight:5}}>Start</Button>
        <Button variant="danger" style={{color:"white",  width:"8em"}}>Stop</Button>
        </div>
    </Navbar.Collapse>
  </Container>
</Navbar>
  )
}
