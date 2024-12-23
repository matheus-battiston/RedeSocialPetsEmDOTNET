import './index.css'
import {Background} from "../../components";
import { useGetAmigos } from '../../../hook/useGetAmigos/useGetAmigos.hook';
import { useEffect, useState } from 'react';
import { CardAmigo } from '../../components/cardAmigo/cardAmigo.component';
import { useRemoverAmigo } from '../../../hook/useRemoverAmigo/useRemoverAmigo';

export function Amigo(){
    const { response, acharAmigos } = useGetAmigos()
    const  { responseRemover, removerAmigoFunc, setResponseRemover } = useRemoverAmigo()
    const [pesquisa, setPesquisa] = useState('')

    function removerAmigo(idAmigo){
        removerAmigoFunc(idAmigo)
    }

    function mudancaTexto(event){
        const {value} = event.target
        setPesquisa(value)
    }

    function pesquisar(){
        acharAmigos(pesquisa)
    }
    
    useEffect(() => {
        console.log(response)
    }, [response])

    useEffect(() => {
        pesquisar()
    }, [pesquisa])

    useEffect(() => {
        if (responseRemover === true){
            acharAmigos('')
            setResponseRemover(false)
        }
    }, [responseRemover])

    return (
        <Background>
            <div className="procurar">
                <form className="input_procurar" onSubmit={pesquisar}>
                    <input className="entrada_procura" type="text" onChange={mudancaTexto} placeholder="Pesquisa um usuario"/>
                </form>        
                <div className='listaDeUsuarios'>{
                    response? response.map(pessoa => {
                        return <CardAmigo amigo={pessoa} removerAmigo={removerAmigo}/>
                    }) :null}
                </div>
            </div>
        </Background>
    )
}