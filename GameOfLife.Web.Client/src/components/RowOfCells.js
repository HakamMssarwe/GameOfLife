import React from "react";
import Cell from "./Cell";

export default function RowOfCells({Cells}) {
	return (
		<tr style={{border:"none"}}>
			{Cells.map(cell => (
				<td>
					<Cell isAlive={cell.isAlive} rowId={cell.rowId} columnId={cell.columnId} />
				</td>
			))}
		</tr>
	);
}
