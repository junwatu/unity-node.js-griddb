import { saveData, getAllData } from './griddbservice.js';

function setupWebSocket(wss) {
	wss.on('connection', function connection(ws) {
		ws.on('message', async (data) => {

			let readableData;

			// Check if the data is in JSON format
			try {
				readableData = JSON.parse(data.toString('utf8'));
			} catch (e) {
				console.log('Received data is not JSON. Ignoring...');
				return;
			}

			// If data is of type "save", then save it
			if (readableData.type === 'save') {
				const playerposition = {
					x: readableData.PlayerX,
					y: readableData.PlayerY,
					z: readableData.PlayerZ
				};
				const numberofthrows = readableData.NumberOfMeatThrows;
				const gameover = false; // Assuming you're not receiving this data for now.

				await saveData({ playerposition, numberofthrows, gameover });
				ws.send(JSON.stringify(readableData))
			} else if (readableData.type === 'getAll') {
				const allData = await getAllData();
				ws.send(JSON.stringify(allData));
			}

			console.log('data received \n %o', readableData);

		});
	});


	wss.on('listening', () => {
		console.log('WebSocket server is listening');
	});
};

export default setupWebSocket;
