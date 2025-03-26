import { Box  } from "@mui/material";
import { FC, ReactElement } from "react";
 

import VerticalTabs from "../../Components/Navbar/TabPanelUser"; 

const User: FC<any> = (): ReactElement => {

    return (
        <Box
            sx={{
                display: "flex",
                justifyContent: "flex-start",
                flexDirection: "row"
            }}
        >
            <Box
                sx={{borderBottom:1, borderColor:'divider'} }
            >
               
            </Box>
            <VerticalTabs /> 
        </Box>
    );
};

export default User;