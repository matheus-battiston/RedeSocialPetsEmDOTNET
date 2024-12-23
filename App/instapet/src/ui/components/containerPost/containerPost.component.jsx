import { Avatar } from '@mui/material'
import { useEffect, useState } from 'react'
import { useCurtirPost } from '../../../hook/useCurtirPost/useCurtirPost.hook'
import { useRemoverCurtida } from '../../../hook/useRemoverCurtida/useRemoverCurtida.hook'
import { BotaoTexto } from '../botaoTexto/botaoTexto.component'
import { ModalComentario } from '../modalComentario/modalComentario.component'
import {Modal} from '../modal/modal.component'
import './index.css'
import { useNavigate } from 'react-router-dom'

const CURTIR = "curtir"
const DESCURTIR = "descurtir"

export function ContainerPost({postsDosAmigos, atualizar}){
    const { response, curtir, setResponse } = useCurtirPost()
    const { responseRemover, removerCurtidaPost, setResponseRemover } = useRemoverCurtida()
    const [showModal, setShowModal] = useState(false)
    const [postParaMostrarComents, setPostParaMostrarComents] = useState()
    const [modalComentar, setModalComentar] = useState(false)
    const [postParaComentar, setPostParaComentar] = useState()
    const navigate = useNavigate()
    
    function clickCurtir(event){
        event.preventDefault()
        const {value, name} = event.target
        if (name === CURTIR){
            curtir(value)
        } else if (name === DESCURTIR){
            removerCurtidaPost(value)
        }
    }
    
    function comentario(event){
        const {value} = event.target
        setPostParaMostrarComents(value)
        setShowModal(true)
    }

    function comentar(event){
        const {value} = event.target
        setPostParaComentar(value)
        setModalComentar(true)
    }


    function paginaUsuario(event){
        const {value} = event.target
        navigate ('/pagina-usuario/' + value)
    }

    useEffect(() => {
        if (response === true || responseRemover === true){
            setResponse(false)
            setResponseRemover(false)
            atualizar()
        }
    }, [response, responseRemover])

    return (
        <>
            {modalComentar === true? <ModalComentario postId={postParaComentar} fechar={setModalComentar}/> : null}
            {showModal === true? <Modal postId={postParaMostrarComents} fechar={setShowModal}/> : null}
            <div className='containerPost'>
            <img className='imagemPost' src={postsDosAmigos.url}/>
            <div className='usuarioLegendaCurtir'>
                <div className='fotoNome'>
                    <Avatar src={postsDosAmigos.usuarioResponse.urlFotoPerfil} />
                    <BotaoTexto onClick={paginaUsuario} valor={postsDosAmigos.usuarioResponse.id} texto={postsDosAmigos.usuarioResponse.nome} classeAdicional="maior"/>
                </div>
                <p>{postsDosAmigos.legenda}</p>
                <div className='likes'>
                    <p>{postsDosAmigos.numeroLikes} likes</p>
                    {postsDosAmigos.curtido === true? 
                        <BotaoTexto onClick={clickCurtir} valor={postsDosAmigos.id} nome={DESCURTIR} texto="Descurtir" classeAdicional="medio"/> : 
                        <BotaoTexto onClick={clickCurtir} valor={postsDosAmigos.id} nome={CURTIR} texto="Curtir" classeAdicional="medio"/> }
                </div>
                <div className='comentarios'>
                    <BotaoTexto onClick={comentario} valor={postsDosAmigos.id} nome={"COMENTARIOS"} texto="mostrar comentarios"/>
                    <BotaoTexto onClick={comentar} valor={postsDosAmigos.id} nome={"COMENTAR"} texto="comentar"/>

                </div>

            </div>
        </div>
        </>
        
    )
}