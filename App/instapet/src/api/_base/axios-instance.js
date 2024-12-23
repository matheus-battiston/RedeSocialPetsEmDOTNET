import axios from 'axios';

const URL_API = 'http://localhost:3000';

const token = JSON.parse(localStorage?.getItem('token'));
console.log('TOKE', token);

export const axiosInstance = axios.create({
  baseURL: URL_API,
  timeout: 5000,
  withCredentials: true,
});

if (token) {
  axiosInstance.defaults.headers.common[
    'Authorization'
  ] = `Bearer ${token.response.replace(/"/g, '')}`;
}
