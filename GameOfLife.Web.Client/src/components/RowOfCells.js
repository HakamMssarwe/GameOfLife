import React from "react";
import Cell from "./Cell";

export default function RowOfCells({Cells}) {
	return (
		<tr style={{border: "none"}}>
			{Cells?.map((cell, index) => (
				<td key={index}>
					<Cell isAlive={cell.isAlive} rowId={cell.rowId} columnId={cell.columnId} />
				</td>
			))}
		</tr>
	);
}
