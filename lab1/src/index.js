import http from 'http';

const PORT = 8080;

http.createServer((req, res) => {
    res.writeHead(200);
    res.end('Service3Pong!');
  })
  .listen(PORT, () => console.log(`Server listening on port ${PORT}`));
