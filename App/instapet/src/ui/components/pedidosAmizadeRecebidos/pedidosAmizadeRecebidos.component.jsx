import './index.css'
import {useGetPedidosPendentes} from "../../../hook/useGetPedidosPendentes/useGetPedidosPendetes.hook";
import {Avatar} from "@mui/material";
import { useAceitarAmizade } from '../../../hook/useAceitarAmizade/useAceitarAmizade.hook';
import { useEffect } from 'react';
import { BotaoTexto } from '../botaoTexto/botaoTexto.component';
import { useRecusarAmizade } from '../../../hook/useRecusarAmizade/useRecusarAmizade.hook';
import { useNavigate } from 'react-router-dom';

const ACEITAR = "aceitar"
const RECUSAR = "recusar"

export function PedidosAmizadeRecebidos(){
    const {pedidosPendentes, getPedidos} = useGetPedidosPendentes()
    const { aceitar, sucesso } = useAceitarAmizade()
    const {recusar, sucessoRecusar} = useRecusarAmizade()
    const navigate = useNavigate()

    async function onClick(event){
        const {name, value} = event.target
        if (name === ACEITAR){
            aceitar(value)
        } else if (name == RECUSAR) {
            recusar(value)
        }
    }

    function paginaUsuario(event){
        const {value} = event.target
        navigate('/pagina-usuario/' + value)
    }

    useEffect(() => {
        if (sucesso === true){
            getPedidos()
        }
    }, [sucesso])

    useEffect(() => {
        if (sucessoRecusar === true){
            getPedidos()
        }
    }, [sucessoRecusar])

    return (
        <section className="pedidosAmizade">
            {pedidosPendentes?.map(pedido => {
                return (
                    <div className="containerPedidoAmizade">
                        <Avatar sx={{ width: 50, height: 50 }} alt="Remy Sharp" src={pedido.requerente.urlFotoPerfil} />
                        <div className="nomeAcao">
                            <BotaoTexto onClick={paginaUsuario} valor={pedido.requerente.id} nome={ACEITAR} texto={pedido.requerente.nome} classeAdicional="maior"/>
                            <section className='acoes'>
                                <BotaoTexto onClick={onClick} valor={pedido.idPedido} nome={ACEITAR} texto="Aceitar"/>
                                <BotaoTexto onClick={onClick} valor={pedido.idPedido} nome={RECUSAR} texto="Recusar"/>
                            </section>
                        </div>
                    </div>
                )
            })}
        </section>
    )
}