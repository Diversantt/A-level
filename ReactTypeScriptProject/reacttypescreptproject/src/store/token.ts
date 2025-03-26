import { makeAutoObservable } from "mobx";
import { IToken } from "../interfaces/login";

class UserToken {
    token: IToken = {token:'', email:''}

    constructor() {
        makeAutoObservable(this)
    }

    setToken(token: IToken) {
        this.token = token;
    }

    getToken() {
        return this.token  ;
    }
}

export default new UserToken();