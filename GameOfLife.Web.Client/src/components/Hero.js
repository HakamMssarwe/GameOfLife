import React from "react";
import {Container, Table} from "react-bootstrap";
import RowOfCells from "./RowOfCells";

export default function Hero({cells, setCells}) {
	return (
		<Container style={{width: "50%", height: "70vh", background: "#0B5ED7", marginTop: 100, padding: 0}}>
			<Table style={{width: "100%", height: "100%"}}>
				<RowOfCells Cells={cells} />
			</Table>
		</Container>
	);
}
