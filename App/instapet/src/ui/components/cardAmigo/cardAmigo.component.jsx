import { Avatar } from "@mui/material"
import DeleteIcon from '@mui/icons-material/Delete';
import IconButton from '@mui/material/IconButton';
import "./index.css"
import { useNavigate } from "react-router-dom";
import { BotaoTexto } from "../botaoTexto/botaoTexto.component";

export function CardAmigo({amigo, removerAmigo}){
    const navigate = useNavigate()

    function paginaUsuario(event){
        const {value} = event.target
        navigate ('/pagina-usuario/' + value)
    }

    function removeAmigo(){
        removerAmigo(amigo.id)
    }


    return (
        <div className="estiloCardAmigo">
            <Avatar src={amigo.urlFotoPerfil} sx={{ width: 100, height: 100 }}/>
            <BotaoTexto onClick={paginaUsuario} valor={amigo.id} texto={amigo.nome} classeAdicional="gigante"/>
            <IconButton onClick={removeAmigo}>
                <DeleteIcon sx={{ width: 50, height: 50 }}/>
            </IconButton>
        </div>
    )
}