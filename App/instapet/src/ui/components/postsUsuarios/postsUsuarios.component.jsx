import { useGetPostsAmigos } from '../../../hook/useGetPosts/useGetPosts.hook'
import './index.css'
import { ContainerPost } from '../containerPost/containerPost.component'
import { useEffect, useState } from 'react'
import { BotaoTexto } from '../botaoTexto/botaoTexto.component'

export function PostsUsuarios({setPagina, posts, atualizar}){

    function exibirPosts(){
        return (
            <>
            {posts.map(post => <ContainerPost atualizar={atualizar} postsDosAmigos={post} />)}
            </>
        )
    }

    return (
        <section className="listaDePosts centralizar">
           {posts? exibirPosts() : null}
            <section className='seletorPagina'>
                <BotaoTexto onClick={setPagina} valor={+1} texto="Ver Mais" classeAdicional="maior" />
            </section>
        </section>
    )
}