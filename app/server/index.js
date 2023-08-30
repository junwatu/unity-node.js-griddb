import express from 'express';
import http from 'http';
import cors from 'cors';
import path from 'path';
import 'dotenv/config';
import url from 'url';
import setupWebSocket from './websocket.js';
import { __dirname } from './libs/dirname.js';
import { getAllData } from "./griddbservice.js";
import { createRequire } from 'module'; // Bring in createRequire function


const require = createRequire(import.meta.url); // construct the require function
const WebSocket = require('ws'); // Now you can use require as you would in a CommonJS module

// Initialize a new Express application
const app = express();

// Parse GAME_SERVER_URL
const parsedUrl = url.parse(process.env.GAME_SERVER_URL);
const hostname = parsedUrl.hostname;
const port = parsedUrl.port || 8080;  // Default port if not specified

// Initialize an HTTP server
const server = http.createServer(app);

// Initialize a WebSocket server instance, attaching it to the HTTP server
const wss = new WebSocket.Server({ server });

// Setup WebSocket routes
setupWebSocket(wss);

const publicPath = path.resolve(`${__dirname}`, './public');

app.use(express.static(publicPath));
app.use(cors());

// HTTP route
app.get('/', (req, res) => {
	res.send('Unity Game Server!');
});


app.get('/api/gamedata', async (req, res) => {
	try {
		const data = await getAllData();
		res.json(data);  // Send data as JSON
	} catch (err) {
		console.error(err);
		res.status(500).send('Internal Server Error');
	}
});

// Start the server
server.listen(port, () => {
	console.log(`HTTP and WebSocket server running on http://${hostname}:${port}`);
});
