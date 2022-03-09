import React from "react";
import {Container, Table} from "react-bootstrap";
import RowOfCells from "./RowOfCells";

export default function Hero({cells, setCells}) {
	return (
		<Container style={{width: "700px", height: "700px", display:"flex",justifyContent:"center", alignItems:"center",background: "#0B5ED7", marginTop: 50, padding: 0}}>
			<Table style={{width: "100%", height: "100%", margin:"0 auto", tableLayout:"auto"}}>
				{cells?.map((row, index) => (
					<RowOfCells key={index} Cells={row} />
				))}
			</Table>
		</Container>
	);
}
