
import { SubmitHandler, useForm } from "react-hook-form"
import { ILogin } from "../../../interfaces/login";
import { Button, Container, FormControl, Grid, InputLabel, OutlinedInput } from "@mui/material";
import { FC } from "react";
import { loginApi } from "../../../Api/modules/loginApi"; 
import UserToken from "../../../store/token";
import { observer } from "mobx-react-lite";


const LoginForm: FC = () =>   {
    const { register, handleSubmit, formState: { errors }, formState } = useForm<ILogin>({
        mode: 'onBlur',
         

    }); 

    const submit: SubmitHandler<ILogin> = data => {
        /*console.log(data)*/
        const Login = async () => {
            try {
                const res = await loginApi(data)                
                alert("LogIn:\nToken: " + res.token);                 
                UserToken.setToken({ token: res.token, email: data.email })                 
            }
            catch (e) {
                alert("LogIn failed");
                if (e instanceof Error) {
                    console.error(e.message)
                }
            }
        }
        Login()

    }
    console.log(errors, formState);

    
    return (
        <Container >
            
            <h2>Login </h2>
            {!!UserToken.token.token && (
                <p>{`Token is: ${UserToken.token.token}`}</p>
            )}
            <form autoComplete="off" onSubmit={handleSubmit(submit)}>
                <Grid container direction="column"  >
                
                    <FormControl sx={{ marginBottom: '1mm' }}  >
                         <InputLabel sx={{ marginLeft: 1 }} variant="standard" htmlFor="email">Email</InputLabel>
                        <OutlinedInput type="email"    {...register('email', {required:true})} />
                     </FormControl>
                     <FormControl sx={{ marginBottom: '1mm' }}  >
                        <InputLabel sx={{ marginLeft: 1 }} variant="standard" htmlFor="password">Password</InputLabel>
                         <OutlinedInput type="password"    {...register('password', { required: true })} />
                    </FormControl>
                    <FormControl sx={{ marginBottom: '1mm' }}>
                          <Button variant="contained" id="submit" type="submit" sx={{ height: '50px' }}>Login </Button> 
                     </FormControl>
                </Grid >
             </form>

        </Container>
    )
}

export default observer (LoginForm)