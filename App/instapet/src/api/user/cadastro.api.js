import { axiosInstance } from '../_base/axios-instance';

export async function cadastro({
  nome,
  email,
  apelido,
  dataNscimento,
  cep,
  urlFotoPerfil,
  senha,
}) {
  const response = await axiosInstance.post('usuarios', {
    Nome: nome,
    Email: email,
    Apelido: apelido,
    DataNscimento: dataNscimento,
    cep: cep,
    UrlFotoPerfil: urlFotoPerfil,
    Senha: senha,
  });
  axiosInstance.defaults.headers.common[
    'Authorization'
  ] = `Bearer ${response.data.token}`;
  return response.data.token;
}
