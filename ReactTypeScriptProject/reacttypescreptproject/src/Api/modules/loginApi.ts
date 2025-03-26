import { ILogin, ILoginCreate } from "../../interfaces/login";
import { apiClient } from "../client";


export const loginApi = (data: ILogin) => apiClient({
    path: `login`,
    method: 'POST',
    data: data
})

export const loginCreateApi = (data: ILoginCreate) => apiClient({
    path: `register`,
    method: 'POST',
    data: data
})
