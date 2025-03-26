 
import { Box,  Grid,  Pagination} from "@mui/material";
import { FC, ReactElement, useEffect, useState } from "react";
import {getUsersByPage } from "../../Api/modules/userApi";
import { IUser } from "../../interfaces/user";
import UserCard from "./components/UserCard";
import { observer } from "mobx-react-lite";
import UsersStore from "./UsersStore";



const store = new UsersStore();

const Users: FC<any> = (): ReactElement => {
    return (
        <Box
            sx={{
                display: "flex",
                justifyContent: "flex-start",
                flexDirection: "row",
                padding: 5,
                margin: -3,
                backgroundColor: "#d4f1f0"
            }}
        >
            <Box  >
                <Grid container spacing={4} justifyContent="center" my={4} >
                <>
                        {store.users?.map((item) => (
                            <Grid key={item.id} item lg={2} md={3} xs={6} >
                            <UserCard {...item } />
                        </Grid>
                    )) }
                </>
                </Grid>
                <Box sx={{display: 'flex',justifyContent:'center' }}>
                    <Pagination variant="outlined" color="secondary" count={store.totalPages} page={store.currentPage} onChange={ async (event,page) => await store.changePage(page) } />
                </Box>
            </Box> 
           
        </Box>
    );
};

export default observer(Users);