
import { SubmitHandler, useForm } from "react-hook-form"
import { ILogin, ILoginCreate } from "../../../interfaces/login";
import { Button, Container, FormControl, Grid, InputLabel, OutlinedInput } from "@mui/material";
import { FC } from "react";
import { loginCreateApi } from "../../../Api/modules/loginApi";
import UserToken from "../../../store/token";
import { observer } from "mobx-react-lite";


const LoginCreateForm: FC = () => {
    const { register, handleSubmit, formState: { errors }, formState } = useForm<ILoginCreate>({
        mode: 'onBlur',
    });


    const submit: SubmitHandler<ILogin> = data => {
        /*console.log(data)*/
        const createLogin = async () => {
            try {
                const res = await loginCreateApi(data)
                alert("Login Create : \nID: " + res.id +"\nToken: " + res.token);
                UserToken.setToken({ token: res.token, email: data.email })
            }
            catch (e) {
                alert("Login create failed");
                if (e instanceof Error) {
                    console.error(e.message)
                }
            }
        }
        createLogin()

    }
    console.log(errors, formState);


    return (
        <Container >

            <h2>Create Login </h2>
            {!!UserToken.token.token && (
                <p>{`Token is: ${UserToken.token.token}`}</p>
            )}
            <form autoComplete="off" onSubmit={handleSubmit(submit)}>
                <Grid container direction="column"  >

                    <FormControl sx={{ marginBottom: '1mm' }}  >
                        <InputLabel sx={{ marginLeft: 1 }} variant="standard" htmlFor="email">Email</InputLabel>
                        <OutlinedInput type="email"    {...register('email', { required: true })} />
                    </FormControl>
                    <FormControl sx={{ marginBottom: '1mm' }}  >
                        <InputLabel sx={{ marginLeft: 1 }} variant="standard" htmlFor="password">Password</InputLabel>
                        <OutlinedInput type="password"    {...register('password', { required: true })} />
                    </FormControl>
                    <FormControl sx={{ marginBottom: '1mm' }}>
                        <Button variant="contained" id="submit" type="submit" sx={{ height: '50px' }}>Registration </Button> 
                    </FormControl>
                </Grid >
            </form>

        </Container>
    )
}

export default observer(LoginCreateForm)