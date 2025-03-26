import { Box, CssBaseline } from "@mui/material";
import { FC, ReactNode } from "react";
import Navbar from "../Navbar/Navbar";
import Footer from "../Footer/Footer";

interface LayoutProps {
    children: ReactNode;
}

const Layout: FC<LayoutProps> = ({ children }) => {

    return (
        <>
            <CssBaseline />
            <Box
                sx={{
                    display: "flex",
                    flexDirection: "column",
                    justifyContent: "flex-start",
                    minHeight: "100vh",
                    maxWidth: "100vw",
                     
                }}
            >
                <Navbar />
                <Box sx={{ flexGrow: 1 }}>
                    {children}
                </Box>
                <Footer />                 
            </Box>
        </>
    );
};

export default Layout;