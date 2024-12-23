import { axiosInstance } from '../_base/axios-instance';

export async function login({ username, password }) {
  const response = await axiosInstance.post('/login', {
    username: username,
    password: password,
  });
  axiosInstance.defaults.headers.common[
    'Authorization'
  ] = `Bearer ${response.data.token}`;
  return response.data.token;
}
