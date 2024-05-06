import { FC, useState } from "react";
import { IUserCreate } from "../../../interfaces/user";
import { Button, Container, FormControl,  Grid,  InputLabel, OutlinedInput } from "@mui/material";
import UserStore from "../UserStore";
import { observer } from "mobx-react-lite";


const store = new UserStore();

const UserCreateForm: FC = () => {


      const [formData, setFormData] = useState<IUserCreate>(
          {
        name: '',
        job:''
            })     
      
      const handleInputChange = (event: React.ChangeEvent<any>) => {
          const { name, value } = event.target;
          setFormData({...formData, [name]:value})
             }
 
      const handleSubmit = async (event: React.FormEvent<any>) => {
          event.preventDefault();

          store.createUser(formData);
       
      }     

    return (

        <Container >
            
            <h2>Create User</h2>
            <form autoComplete="off" onSubmit={handleSubmit}   >
                <Grid container direction="column"     >
                    <FormControl sx={{ marginBottom: '1mm' }} >
                        <InputLabel  sx={{ marginLeft: 1 }} variant="standard" htmlFor="name">Name</InputLabel>
                        <OutlinedInput type="text" name="name" value={formData.name} onChange={handleInputChange}    required   />
                    </FormControl> 
                    <FormControl sx={{ marginBottom: '1mm' }}>
                        <InputLabel sx={{ marginLeft: 1 }} variant="standard" htmlFor="job">Job</InputLabel>
                        <OutlinedInput type="text" name="job" value={formData.job} onChange={handleInputChange} required />
                    </FormControl>   
                    <FormControl sx={{ marginBottom: '1mm' }}>
                        <Button variant="contained" id="submit" type="submit" sx={{height:'50px'} }>Create </Button> 
                    </FormControl> 
                </Grid >
            </form>  
        </Container> 
        ) 
}

export default observer(UserCreateForm)