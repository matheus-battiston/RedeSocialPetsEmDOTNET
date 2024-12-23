import "./index.css"
import {Background, BotaoTexto, ContainerLogin, FormLogin} from "../../components";
import { useNavigate } from "react-router-dom";


export function Login(){
    const navigate = useNavigate()

    function cadastrar(){
        navigate('/cadastro')
    }

    return (
        <Background>
            <ContainerLogin>
                <FormLogin />
                <BotaoTexto onClick={cadastrar} texto="Nao tem uma conta? Crie aqui" valor="BotaoTexto" nome="BotaoTexto"/>
            </ContainerLogin>
        </Background>
    )
}