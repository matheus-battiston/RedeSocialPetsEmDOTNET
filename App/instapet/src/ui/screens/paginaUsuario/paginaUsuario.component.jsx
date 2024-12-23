import { useParams } from "react-router-dom";
import { Background, BotaoTexto, InfoUsuarioMenu, Menu, Posts } from "../../components";
import { useEffect, useState } from "react";
import { getPaginaPessoal } from "../../../api/user/paginaPessoa.api";
import { PostsUsuarios } from "../../components/postsUsuarios/postsUsuarios.component";
import { Avatar } from "@mui/material";
import './index.css'
import { removerAmigo } from "../../../api/user/removerAmigo.api";
import { pedidoAmizade } from "../../../api/user/novoPedidoAmizade.api";


export function PaginaUsuario(){
    const {id} = useParams()
    const [response, setResponse] = useState(null)
    const [pagina, setPagina] = useState(0)

    function desfazerAmizade(){
        removerAmigo(response.id)
    }

    function addAmigo(){
        pedidoAmizade(response.id)
    }

    function AcaoUsuario(){
        if (response.perfilPessoal === true){
            return (null)
        }
        else if (response.pedidoPendente === true){
            return (
                <p>Pedido pendente</p>
            )
        } else if (response.amigo === true){
            return (
                <BotaoTexto onClick={desfazerAmizade} nome="DesfazerAmizade" texto="Desfazer amizade" classeAdicional="medio"/>
            )
        } else {
            return (<BotaoTexto onClick={addAmigo} nome="AdicionarAmigo" texto="AdicionarAmigo" classeAdicional="medio"/>
)
        }
    }

    async function getPagina() {
        try {
            const resposta = await getPaginaPessoal(id, pagina)
            setResponse(resposta)
        } catch(error){
        }
    }

    function verMais(){
        setPagina(pagina + 1)
    }

    useEffect(() => {
        getPagina()
    },[])

    useEffect(() => {
        getPagina()
    }, [pagina])
    
    useEffect(() => {
        console.log(response)
    }, [response])

    return (
        <Background>
            <div className="menuPrincipal">
                <div className="infos_usuario_pagina_perfil">
                    <InfoUsuarioMenu usuario={response}/>
                    {response? AcaoUsuario() : null}

                </div>
            </div>
            <PostsUsuarios setPagina={verMais} posts={response?.posts} atualizar={getPagina}/>

        </Background>
    )
}