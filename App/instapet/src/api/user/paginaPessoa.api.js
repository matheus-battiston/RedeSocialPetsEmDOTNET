//localhost:8080/usuarios/listarAmigos?texto=cwi
import { axiosInstance } from '../_base/axios-instance';

export async function getPaginaPessoal(id, pagina) {
  const response = await axiosInstance.get(
    'usuarios/pagina-usuario/' + id + `?size=2&page=${pagina}`
  );
  return response.data;
}
