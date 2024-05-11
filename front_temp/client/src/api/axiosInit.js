import axios from "axios";

export default axios.create({
  baseURL: "https://localhost:7197/api",
  headers: {
    "Content-type": "application/json"
  }
});