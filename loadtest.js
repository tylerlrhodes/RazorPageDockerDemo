import http from "k6/http";

export default function() {
  http.get("http://192.168.1.11:8080");
};