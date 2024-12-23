import {createBrowserRouter} from "react-router-dom";
import {Login, Home, Amigo, ProcurarUsuario, Cadastro, PaginaUsuario} from "../ui/screens";
import {PrivateRoute} from "./private-route.component";

export const router = createBrowserRouter([
    {
        path : "/",
        element : <Login />
    },

    {
        path:"/home",
        element: <PrivateRoute Screen={Home} />
    },

    {
        path: "/amigos",
        element : <PrivateRoute Screen={Amigo} />
    },
    {
        path: "/procurar",
        element: <PrivateRoute Screen={ProcurarUsuario} />
    },
    {
        path: "/cadastro",
        element : <Cadastro />
    },
    {
        path:"/pagina-usuario/:id",
        element: <PrivateRoute Screen={PaginaUsuario} />
    }
])