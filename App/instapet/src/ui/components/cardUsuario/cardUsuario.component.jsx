import { Avatar } from "@mui/material"
import IconButton from '@mui/material/IconButton';
import AddIcon from '@mui/icons-material/Add';
import { useNavigate } from "react-router-dom";
import { BotaoTexto } from "../botaoTexto/botaoTexto.component";
import "./index.css"

export function CardUsuario({amigo, adicionarAmigo}){

    const navigate = useNavigate()

    function paginaUsuario(event){
        const {value} = event.target
        navigate ('/pagina-usuario/' + value)
    }

    function adicionaAmigo(){
        adicionarAmigo(amigo.id)
    }

    return (
        <div className="estiloCardAmigo">
            <Avatar src={amigo.urlFotoPerfil} sx={{ width: 100, height: 100 }}/>
            <BotaoTexto onClick={paginaUsuario} valor={amigo.id} texto={amigo.nome} classeAdicional="gigante"/>

            <IconButton onClick={adicionaAmigo}>
                <AddIcon sx={{ width: 50, height: 50 }}/>
            </IconButton>
        </div>
    )
}