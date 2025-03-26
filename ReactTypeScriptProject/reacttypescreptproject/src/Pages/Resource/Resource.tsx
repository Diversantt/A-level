import { Box } from "@mui/material";
import { FC, ReactElement } from "react";
import VerticalTabs from "../../Components/Navbar/TabPanelResource";


const Resource: FC<any> = (): ReactElement => {

    return (
        <Box
            sx={{
                display: "flex",
                justifyContent: "flex-start",
                flexDirection: "row"
            }}
        >           
            <VerticalTabs />            
        </Box>
    );
};

export default Resource;


 