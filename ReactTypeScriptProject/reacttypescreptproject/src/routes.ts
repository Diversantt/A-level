import { FC } from "react";
import User from "./Pages/User/User";
import Resource from "./Pages/Resource/Resource";
import Login from "./Pages/Login/Login";


interface Route {
    key: string,
    title: string,
    path: string,
    enabled: boolean,
    component: FC<{}>
}

export const routes: Array<Route> = [
    {
        key: 'user',
        title: 'Users',
        path: '/',
        enabled: true,
        component: User
    },
    {
        key: 'resource',
        title: 'Resources',
        path: '/resources',
        enabled: true,
        component: Resource
    },
    {
        key: 'login',
        title: 'Login',
        path: '/login',
        enabled: true,
        component: Login
    }
]