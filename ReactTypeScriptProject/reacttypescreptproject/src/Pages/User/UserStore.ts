import { createUserApi, getUserById, updateUserApi } from "../../Api/modules/userApi"
import { IUser, IUserCreate } from "../../interfaces/user"


class UserStore {
    user :  IUser = { 
    id : 0,
    email: "",
    first_name: "",
    last_name: "",
    avatar:""    
    }

    getUserByID = async (id:string) => {
        try {
            const res = await getUserById(id)

            this.user =
            {
                id: res.data.ID,
                email: res.data.email,
                first_name: res.data.first_name,
                last_name: res.data.last_name,
                avatar: res.data.avatar
            }
        }
        catch (e) {

            if (e instanceof Error) {
                console.error(e.message)
            }
        }
    }

    createUser = async (formData: IUserCreate) => {
        try {
            const res = await createUserApi(formData)
            console.log(res.name)
            alert("Create usert:\nID: " + res.id + "\nName: " + res.name + "\nJob: " + res.job + "\nDateCreate: " + res.createdAt);
        }
        catch (e) {

            if (e instanceof Error) {
                console.error(e.message)
            }
        }
    } 

    updateUser = async (id: string, formData: IUserCreate) => {
        try {
            const res = await updateUserApi(id, formData)

            alert("User updated:\nName: " + res.name + "\nJob: " + res.job + "\nDateUpdate: " + res.updatedAt);
        }
        catch (e) {

            if (e instanceof Error) {
                console.error(e.message)
            }
        }
    }
     


}

export default UserStore;