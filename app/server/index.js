const WebSocket = require('ws')
const wss = new WebSocket.Server({ port: 8080 }, () => {
	console.log('server started')
})
wss.on('connection', function connection(ws) {
	ws.on('message', (data) => {
		const readableData = data.toString('utf8');
		console.log('data received \n %o', readableData);
		ws.send(readableData);
	})
})
wss.on('listening', () => {
	console.log('listening on 8080')
})