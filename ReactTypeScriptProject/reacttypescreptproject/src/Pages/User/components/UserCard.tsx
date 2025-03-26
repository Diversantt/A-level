import { FC, ReactElement } from "react";
import { IUser } from "../../../interfaces/user";
import { Card, CardActionArea, CardContent, CardMedia, Container,  Typography } from "@mui/material";
import * as React from 'react';
import Button from '@mui/material/Button';
import { styled } from '@mui/material/styles';
import Dialog from '@mui/material/Dialog';
import DialogTitle from '@mui/material/DialogTitle';
import DialogContent from '@mui/material/DialogContent';
import DialogActions from '@mui/material/DialogActions';
import IconButton from '@mui/material/IconButton';
import CloseIcon from '@mui/icons-material/Close';
import UserUpdateForm from "./UserUpdate";
 
const BootstrapDialog = styled(Dialog)(({ theme }) => ({
    '& .MuiDialogContent-root': {
        padding: theme.spacing(2),
    },
    '& .MuiDialogActions-root': {
        padding: theme.spacing(1),
    },
}));

const UserCard: FC<IUser> = (props): ReactElement => {

    //const navigate = useNavigate()

    const [open, setOpen] = React.useState(false);
    const [open2, setOpen2] = React.useState(false);


    const handleClickOpen = () => {
        setOpen(true);
    };
    const handleClose = () => {
        setOpen(false);
        
    };

    const handleClickOpen2 = () => {       
        setOpen(false);
        setOpen2(true);
    };
    const handleClose2 = () => {
        setOpen2(false);
    };




    return (
        <React.Fragment>
        <Card sx={{
            backgroundColor: "#abcdef" 
        }}>
            <CardActionArea onClick={handleClickOpen}>
                <CardMedia
                    component="img"
                    height="250"
                    image={props.avatar}
                    alt={props.email}
                />
                <CardContent>
                    <Typography noWrap gutterBottom variant="h6" component="div">
                        {props.email}
                    </Typography>
                    <Typography variant="body2" color="text.secondary">
                        {props.first_name} {props.last_name }
                    </Typography>
                </CardContent>
            </CardActionArea>
        </Card>        
             
            <BootstrapDialog
                onClose={handleClose}
                aria-labelledby="customized-dialog-title"
                open={open}
                 
            >
                <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title">
                   User ID: {props.id}
                </DialogTitle>
                <IconButton
                    aria-label="close"
                    onClick={handleClose}
                    sx={{
                        position: 'absolute',
                        right: 8,
                        top: 8,
                        color: (theme) => theme.palette.grey[500],
                    }}
                >
                    <CloseIcon />
                </IconButton>
                <DialogContent dividers>
                    <Card sx={{
                        backgroundColor: "#abcdef"
                    }}>
                        <CardActionArea  >
                            <CardMedia
                                component="img"
                                height="250"
                                image={props.avatar}
                                alt={props.email}
                            />
                            <CardContent>
                                <Typography noWrap gutterBottom variant="h6" component="div">
                                    {props.email}
                                </Typography>
                                <Typography variant="body2" color="text.secondary">
                                    {props.first_name} {props.last_name}
                                </Typography>
                            </CardContent>
                        </CardActionArea>

                    </Card>
                </DialogContent>
                <DialogActions>
                    <Button autoFocus onClick={handleClickOpen2}>
                        Edit
                    </Button>
                </DialogActions>
            </BootstrapDialog>


            <BootstrapDialog
                onClose={handleClose2}
                aria-labelledby="customized-dialog-title2"
                open={open2}
            >
                <DialogTitle sx={{ m: 0, p: 2 }} id="customized-dialog-title2">
                    Update User ID : { props.id}
                </DialogTitle>
                <IconButton
                    aria-label="close"
                    onClick={handleClose2}
                    sx={{
                        position: 'absolute',
                        right: 8,
                        top: 8,
                        color: (theme) => theme.palette.grey[500],
                    }}
                >
                    <CloseIcon />
                </IconButton>
                <DialogContent dividers>
                    <Container >
                        <UserUpdateForm {...props} />
                    </Container>
                </DialogContent>
                
            </BootstrapDialog>

        </React.Fragment>

    )
}

     export default UserCard