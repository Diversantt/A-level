import { IUserCreate } from "../../interfaces/user";
import { apiClient } from "../client";


export const getUserById = (id: string) => apiClient({
    path: `users/${id}`,
    method: 'GET'

})

export const getUsersByPage = (page: number) => apiClient({
    path: `users?page=${page}`,
    method: 'GET'
})

export const createUserApi = (data: IUserCreate) => apiClient({
    path: `users`,
    method: 'POST',
    data: data
})

export const updateUserApi = (id: string, data: IUserCreate) => apiClient({
    path: `users/${id}`,
    method: 'PUT',
    data: data
})