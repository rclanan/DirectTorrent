(function () {
	var stream = require('stream');
	var NullStream = function (o) {
		stream.Writable.call(this);
		this._write = function (c, e, cb) { cb && cb(); };
	}
	require('util').inherits(NullStream, stream.Writable);
	var nullStream = new NullStream();
	process.__defineGetter__('stdout', function () { return nullStream; });
	process.__defineGetter__('stderr', function () { return nullStream; });
})();


var http = require('http');
var fs = require('fs');
var  util = require('util');
var WebTorrent = require('webtorrent');

return function(data, cb){
	var client = new WebTorrent();
	var magnetUri = 'MATIJACUPIC';
	client.add(magnetUri, function (torrent) {
		// Got torrent metadata! 
		//console.log('Torrent info hash:', torrent.infoHash);

		// Let's say the first file is a webm (vp8) or mp4 (h264) video... 
		var file = torrent.files[0];
		var total = file.length;


		var server = http.createServer(function (req, res) {
			if (req.url == '/shutdown.html') { 
				res.end('Server shutdown!');
				process.exit();
			} else if (req.headers['range']) {
				var range = req.headers.range;
				var parts = range.replace(/bytes=/, "").split("-");
				var partialstart = parts[0];
				var partialend = parts[1];

				var start = parseInt(partialstart, 10);
				var end = partialend ? parseInt(partialend, 10) : total-1;
				var chunksize = (end-start)+1;
				console.log('RANGE: ' + start + ' - ' + end + ' = ' + chunksize);

				var result = file.createReadStream({start: start, end: end});
				res.writeHead(206, { 'Content-Range': 'bytes ' + start + '-' + end + '/' + total, 'Accept-Ranges': 'bytes', 'Content-Length': chunksize, 'Content-Type': 'video/mp4' });
				result.pipe(res);
			} else {
				console.log('ALL: ' + total);
				res.writeHead(200, { 'Content-Length': total, 'Content-Type': 'video/mp4' });
				file.createReadStream().pipe(res);
			}}).listen(1337);
			fs.writeFile('hash.txt', 'Torrent info hash:' + torrent.infoHash);
});
};