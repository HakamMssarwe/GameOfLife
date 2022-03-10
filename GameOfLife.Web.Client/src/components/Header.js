import React, {useState} from "react";
import {Button, Container, Nav, Navbar, NavDropdown} from "react-bootstrap";

export default function Header({gameIsRunning, initiateGame, stopGame, boardSize, setBoardSize, population, setPopulation, rule, setRule, growthSpeed, setGrowthSpeed}) {
	return (
		<Navbar bg="light" expand="lg" style={{paddingTop: 20, paddingBottom: 20}}>
			<Container>
				<Navbar.Brand href="/">Game of life</Navbar.Brand>
				<Navbar.Toggle aria-controls="basic-navbar-nav" />
				<Navbar.Collapse id="basic-navbar-nav">
					<Nav className="me-auto">
						<NavDropdown title={boardSize.name} id="boardSize">
							<NavDropdown.Item onClick={e => setBoardSize({name: "6x6", value: 6})}>6x6</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setBoardSize({name: "8x8", value: 8})}>8x8</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setBoardSize({name: "10x10", value: 10})}>10x10</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setBoardSize({name: "15x15", value: 15})}>15x15</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setBoardSize({name: "20x20", value: 20})}>20x20</NavDropdown.Item>
							{/* <NavDropdown.Item onClick={e => setBoardSize({name: "30x30", value: 30})}>30x30</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setBoardSize({name: "50x50", value: 50})}>50x50</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setBoardSize({name: "75x75", value: 75})}>75x75</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setBoardSize({name: "100x100", value: 100})}>100x100</NavDropdown.Item> */}
						</NavDropdown>
						<NavDropdown title={population.name} id="population">
							<NavDropdown.Item onClick={e => setPopulation({name: "Small", value: 0})}>Small</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setPopulation({name: "Medium", value: 1})}>Medium</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setPopulation({name: "Large", value: 2})}>Large</NavDropdown.Item>
						</NavDropdown>
						<NavDropdown title={rule.name} id="rule">
							<NavDropdown.Item onClick={e => setRule({name: "Conway", value: 0})}>Conway</NavDropdown.Item>
						</NavDropdown>
						<NavDropdown title={growthSpeed.name} id="rule">
							<NavDropdown.Item onClick={e => setGrowthSpeed({name: "Very Slow", value: 0})}>Very Slow</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setGrowthSpeed({name: "Slow", value: 1})}>Slow</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setGrowthSpeed({name: "Normal", value: 2})}>Normal</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setGrowthSpeed({name: "Fast", value: 3})}>Fast</NavDropdown.Item>
							<NavDropdown.Item onClick={e => setGrowthSpeed({name: "Very Fast", value: 4})}>Very Fast</NavDropdown.Item>
						</NavDropdown>
					</Nav>
					<div style={{display: "flex"}}>
						{gameIsRunning == null ? (
							<Button
								onClick={e => {
									window.location.reload();
								}}
								variant="warning"
								style={{color: "white", width: "8em"}}>
								Refresh
							</Button>
						) : !gameIsRunning ? (
							<Button onClick={initiateGame} variant="primary" style={{width: "8em", marginRight: 5}}>
								Start
							</Button>
						) : (
							<Button onClick={stopGame} variant="danger" style={{color: "white", width: "8em"}}>
								Stop
							</Button>
						)}
					</div>
				</Navbar.Collapse>
			</Container>
		</Navbar>
	);
}
