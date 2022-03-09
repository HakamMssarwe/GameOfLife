import {useEffect, useState} from "react";
import Header from "./components/Header";
import Hero from "./components/Hero";
import {API_BASE, HUB_BASE} from "./constants/server";
import {HubConnectionBuilder} from "@microsoft/signalr";

function App() {
	const [connection, setConnection] = useState(null);
	const [boardId, setBoardId] = useState(null);
	const [gameIsRunning, setGameIsRunning] = useState(false);
	const [boardSize, setBoardSize] = useState({name: "Size", value: null});
	const [population, setPopulation] = useState({name: "Seed", value: null});
	const [rule, setRule] = useState({name: "Policy", value: null});
	const [growthSpeed, setGrowthSpeed] = useState({name: "Speed", value: null});
	const [cells, setCells] = useState([]);

	useEffect(() => {
		console.clear();
		const newConnection = new HubConnectionBuilder().withUrl(HUB_BASE).withAutomaticReconnect().build();
		setConnection(newConnection);
	}, []);

	useEffect(() => {
		handleHubConnection();
	}, [connection]);

	const handleHubConnection = async () => {
		if (connection) {
			try {
				//Register the methods
				connection.on("UpdateGeneration", function (newGenerationCells) {
					setCells([...newGenerationCells]);
				});

				connection.on("DisconnectClient", function () {
					connection.stop();
				});

				//Start the connection
				await connection.start();
				console.log("Connected!");

				//Call other methods
				var res = await connection.invoke("GetConnectionId");
				setBoardId(res);
			} catch (err) {
				console.log("Connection failed!");
				setTimeout(handleHubConnection, 5000);
			}
		}
	};

	const initiateGame = async () => {
		if (boardId == null) {
			alert("Please wait for the server to connect.");
			return;
		}

		if (boardSize.value == null || population.value == null || rule.value == null || growthSpeed.value == null) {
			alert("Make sure you configure all the settings in order to start the game.");
			return;
		}

		try {
			setCells([]);

			const requestOptions = {
				method: "POST",
				headers: {"Content-Type": "application/json"},
				body: JSON.stringify({Id: boardId, Rows: boardSize.value, Columns: boardSize.value, GrowthSpeed: growthSpeed.value, Population: population.value, GameRule: rule.value}),
			};
			var response = await (await fetch(`${API_BASE}/InitateGame`, requestOptions)).json();

			setCells([...response.cells]);
			setGameIsRunning(true);

			startGame();
		} catch {
			alert("Something went wrong.");
			return;
		}
	};

	const startGame = async () => {
		try {
			var response = await fetch(`${API_BASE}/StartGame/${boardId}`);
		} catch {
			alert("Something went wrong.");
			return;
		}
	};

	const stopGame = async () => {
		console.log("Stopping game.");
		try {
			const requestOptions = {
				method: "POST",
				headers: {"Content-Type": "application/json"},
			};

			var response = await fetch(`${API_BASE}/StopGame/${boardId}`, requestOptions);
			setGameIsRunning(null);
		} catch {
			alert("Something went wrong.");
			return;
		}
	};
	return (
		<>
			<Header gameIsRunning={gameIsRunning} initiateGame={initiateGame} stopGame={stopGame} boardSize={boardSize} setBoardSize={setBoardSize} population={population} setPopulation={setPopulation} rule={rule} setRule={setRule} growthSpeed={growthSpeed} setGrowthSpeed={setGrowthSpeed} />
			{cells != null && <Hero cells={cells} setCells={setCells} />}
		</>
	);
}

export default App;
