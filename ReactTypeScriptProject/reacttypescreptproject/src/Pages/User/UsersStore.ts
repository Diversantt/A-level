import { makeAutoObservable, runInAction } from "mobx";
import { IUser, IUserCreate } from "../../interfaces/user";
import { createUserApi, getUsersByPage } from "../../Api/modules/userApi";

class UsersStore {

    users: IUser[] = [];
    totalPages:number = 0;
    currentPage:number = 1;

    constructor() {
        makeAutoObservable(this);
        runInAction (this.getUsers);
    }

    async changePage(page: number) {
        this.currentPage = page;
        await this.getUsers();
    }

    getUsers = async () => {
        try {
            
            const res = await getUsersByPage(this.currentPage);
           
            this.users = res.data;
            this.totalPages = res.total_pages;
        }
        catch (e) {
            if (e instanceof Error) {
                console.error(e.message)
            }
        }
    }


   

}

export default UsersStore;