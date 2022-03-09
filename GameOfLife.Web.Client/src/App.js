import {useState} from "react";
import Header from "./components/Header";
import Hero from "./components/Hero";

function App() {
	const [cells, setCells] = useState([
		{isAlive: false, rowId: 1, columnId: 1},
		{isAlive: false, rowId: 1, columnId: 2},
		{isAlive: false, rowId: 1, columnId: 3},
		{isAlive: false, rowId: 1, columnId: 4},
		{isAlive: false, rowId: 2, columnId: 1},
		{isAlive: false, rowId: 2, columnId: 2},
		{isAlive: false, rowId: 2, columnId: 3},
		{isAlive: false, rowId: 2, columnId: 4},
		{isAlive: false, rowId: 3, columnId: 1},
		{isAlive: false, rowId: 3, columnId: 2},
		{isAlive: false, rowId: 3, columnId: 3},
		{isAlive: false, rowId: 3, columnId: 4},
		{isAlive: false, rowId: 4, columnId: 1},
		{isAlive: false, rowId: 4, columnId: 2},
		{isAlive: false, rowId: 4, columnId: 3},
		{isAlive: false, rowId: 4, columnId: 4},
	]);
	return (
		<>
			<Header />
			<Hero cells={cells} setCells={setCells} />
		</>
	);
}

export default App;
