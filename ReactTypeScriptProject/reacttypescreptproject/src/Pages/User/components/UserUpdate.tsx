import { FC, ReactElement, useState } from "react";
import { IUser, IUserCreate } from "../../../interfaces/user";
import {  Button, Container, FormControl,  Grid, InputLabel, OutlinedInput } from "@mui/material";
import UserStore from "../UserStore";
import { observer } from "mobx-react-lite";

const store = new UserStore();

const UserUpdateForm: FC<IUser> = (props): ReactElement => {


    const [formData, setFormData] = useState<IUserCreate>(
        {
            name: props.first_name,
            job: props.email
        })
         
    const handleInputChange = (event: React.ChangeEvent<any>) => {
        const { name, value } = event.target;
        setFormData({ ...formData, [name]: value })
    }

    const handleSubmit = async (event: React.FormEvent<any>) => {
        event.preventDefault();        

        store.updateUser(props.id.toString(), formData)
    } 


    return (

        <Container > 
            <form autoComplete="off" onSubmit={handleSubmit}   >
                <Grid container direction="column"  > 
                    <FormControl sx={{ marginBottom: '1mm' }}  >
                        <InputLabel sx={{ marginLeft: 1 }} variant="standard" htmlFor="name">Name</InputLabel>
                        <OutlinedInput type="text" name="name" value={formData.name} onChange={handleInputChange} required /> 
                    </FormControl> 
                    <FormControl sx={{ marginBottom: '1mm' }}>
                        <InputLabel sx={{ marginLeft: 1 }} variant="standard" htmlFor="job">Job</InputLabel>
                        <OutlinedInput type="text" name="job" value={formData.job} onChange={handleInputChange} required />
                    </FormControl> 
                    <FormControl sx={{ marginBottom: '1mm' }}> 
                        <Button variant="contained" id="submit" type="submit" sx={{ height: '50px' }}>Update </Button> 
                    </FormControl> 
                </Grid >
            </form> 
        </Container> 
    ) 
}

export default observer(UserUpdateForm);