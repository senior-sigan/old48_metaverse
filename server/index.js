const WebSocket = require('ws');
const crypto = require('node:crypto');

const PORT = process.env.PORT || 8000;
const DELTA_TIME = process.env.DELTA_TIME || 1000; // 10 fps

const wss = new WebSocket.Server({ port: PORT });

const players = {};

wss.on('error', (err) => {
  console.error('[ERROR] WebSocket: ', err);
});

wss.on('connection', (client) => {
  const clientId = crypto.randomUUID();

  console.log('[INFO] new client: ', clientId);

  client.clientId = clientId;
  players[clientId] = {
    position: { x: 0, y: 0, z: 0 },
    timestamp: Date.now(),
  };

  client.send(
    JSON.stringify({
      id: clientId,
      players: players,
    }),
  );

  client.on('message', (msg) => {
    if (!players[clientId]) {
      console.log(`[WARN] a message from deleted user? ${clientId}: `, msg);
      return;
    }

    const data = JSON.parse(msg);
    players[clientId] = {
      position: {
        x: data.position.x,
        y: data.position.y,
        z: data.position.z,
      },
      timestamp: Date.now(),
    };
  });

  client.on('close', (code, reason) => {
    console.log(`[INFO] close ${code}`, clientId);
    delete players[clientId];
  });

  client.on('error', (err) => {
    console.error('[ERROR] client error: ', err);
    delete players[clientId];
  });
});

function broadcastUpdate() {
  console.log(
    'Broadcast to ',
    [...wss.clients].map((c) => c.clientId),
    players,
  );
  wss.clients.forEach((client) => {
    if (client.readyState !== WebSocket.OPEN) return;
    client.send(
      JSON.stringify({
        id: client.clientId,
        players: players,
      }),
    );
  });
}

setInterval(broadcastUpdate, DELTA_TIME);
