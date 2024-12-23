import { useEffect, useState } from 'react';
import { getMe } from '../../api/user/getMe.api';
import useGlobalErro from '../../context/erro/erro.context';
import useGlobalUser from '../../context/user.context';
import axios from 'axios';

export function useGetMe() {
  const [me, setMe] = useState();
  const [, setErro] = useGlobalErro();
  const [_user] = useGlobalUser();

  useEffect(() => {
    getUsuarioLogado();
  }, []);

  async function getUsuarioLogado() {
    try {
      const respostaApi = await getMe(_user);
      setMe(respostaApi);
    } catch (error) {
      setErro(error.response.data.message || error.response.statusText);
    }
  }
  return { me, getUsuarioLogado };
}
