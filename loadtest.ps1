cat loadtest.js | docker run --rm -i -p 6565:6565 loadimpact/k6 run --vus 10 --duration 60s -