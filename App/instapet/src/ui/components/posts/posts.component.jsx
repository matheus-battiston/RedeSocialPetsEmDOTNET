import { useGetPostsAmigos } from '../../../hook/useGetPosts/useGetPosts.hook'
import './index.css'
import { ContainerPost } from '../containerPost/containerPost.component'
import { useEffect, useState } from 'react'
import { BotaoTexto } from '../botaoTexto/botaoTexto.component'

export function Posts(){
    const {postsDosAmigos, getPosts} = useGetPostsAmigos()
    const [pagina, setPagina] = useState(0)
    const [desabilitarProx, setDesabilitarProx] = useState(false)
    const [desabilitarAnterior, setDesabilitarAnterior] = useState(false)


    useEffect(() => {
        getPosts(pagina)
        if(postsDosAmigos?.totalPages === pagina + 1){
            setDesabilitarProx(true)
        } else {
            setDesabilitarProx(false)
        }

        if (pagina === 0){
            setDesabilitarAnterior(true)
        } else{
            setDesabilitarAnterior(false)
        }
    }, [pagina])


    function clickPagina(event){
        const {value} = event.target
        const novaPagina = pagina + parseInt(value)
        setPagina(novaPagina)
    }

    function exibirPosts(){
        return (
            <>
            {postsDosAmigos.map(post => <ContainerPost atualizar={atualizar} postsDosAmigos={post} />)}
            </>
        )
    }
    
    function atualizar(){
        getPosts(pagina)
    }

    useEffect(() => {
        console.log("AQUI", postsDosAmigos)
    }, [postsDosAmigos])

    return (
        <section className="listaDePosts centralizar">
           {postsDosAmigos? exibirPosts() : null}
            <section className='seletorPagina'>
                <BotaoTexto onClick={clickPagina} valor={+1} texto="Ver Mais" classeAdicional="maior" disabled={desabilitarProx}/>
            </section>
        </section>
    )
}