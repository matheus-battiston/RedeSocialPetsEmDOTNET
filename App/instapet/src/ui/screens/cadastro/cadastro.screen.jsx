import {Background, ContainerLogin} from "../../components";
import { FormCadastro } from "../../components/formCadastro/formCadastro.component";


export function Cadastro(){
    return (
        <Background>
            <ContainerLogin>
                <FormCadastro/>
            </ContainerLogin>
        </Background>
    )
}