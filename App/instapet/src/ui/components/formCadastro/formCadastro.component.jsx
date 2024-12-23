import './index.css'
import {TextField, Button} from "@mui/material";
import SendIcon from '@mui/icons-material/Send';
import {useState, useEffect} from "react";
import {useNavigate} from "react-router-dom";
import { cadastro } from '../../../api/user/cadastro.api';


export function FormCadastro(){

    const [error, setError] = useState(null)
    const [formInput, setFormInput] = useState({nome: '', email: '', apelido: '', url: '', cep: '', senha: '', dataNascimento: ''})
    const [response, setResponse ] = useState(null)
    const navigate = useNavigate()


    function mudancaInput(event){
        const {name, value} = event.target
        setFormInput(oldFormInput => ({...oldFormInput, [name]: value}))
        console.log(formInput)
    }

    async function cadastrar(event) {
        event.preventDefault()
        try {
            const response = await cadastro({   nome: formInput.nome,
                email : formInput.email,
                apelido : formInput.apelido,
                dataNscimento : formInput.dataNascimento,
                cep : formInput.cep,
                urlFotoPerfil : formInput.url,
                senha : formInput.senha, })
            setResponse({response})
        } catch(error){
            setError("Usuario ou senha invalido")
        }
    }

    useEffect( () => {
        if (response){
            navigate("/home")
        }
    }, [response])

    return (
        <form className="formCadastroStyle" onSubmit={cadastrar}>
            <TextField id="email" label="Email" variant="outlined" onChange={mudancaInput} name="email"/>
            <TextField id="senha" label="Senha" variant="outlined" type="password" margin="dense" onChange={mudancaInput} name="senha"/>
            <TextField id="apelido" label="Apelido" variant="outlined" onChange={mudancaInput} name="apelido"/>
            <TextField id="url" label="Url" variant="outlined" onChange={mudancaInput} name="url"/>
            <TextField id="cep" label="Cep" variant="outlined" onChange={mudancaInput} name="cep"/>
            <TextField id="nome" label="Nome" variant="outlined" onChange={mudancaInput} name="nome"/>
            <TextField id="dataNascimento" label="" type='date' variant="outlined" onChange={mudancaInput} name="dataNascimento"/>

            {error? <p className="error">{error}</p> : null}
            <section className="botao">
                <Button onClick={cadastrar} color="secondary" variant="contained" endIcon={<SendIcon />}>
                    Cadastrar
                </Button>
            </section>

        </form>
    )
}