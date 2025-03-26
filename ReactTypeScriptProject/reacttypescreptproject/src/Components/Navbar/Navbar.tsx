import { Link, Box, IconButton, MenuItem, Typography, Menu } from "@mui/material";
import React ,{ FC, ReactElement } from "react";
import { routes } from "../../routes";
import { NavLink } from "react-router-dom";
import UserToken from "../../store/token";
import { observer } from "mobx-react-lite";
import MenuIcon from "@mui/icons-material/Menu";
import LoginMenu from "./LoginMenu";



const Navbar: FC = (): ReactElement => {

    const [anchorElNav, setAnchorElNav] = React.useState(null);

    const handleOpenNavMenu = (event: any) => {
        setAnchorElNav(event.currentTarget);
    };

    const handleCloseNavMenu = () => {
        setAnchorElNav(null);
    };

    return (
        <Box
            sx={{
                width: "100%",
                height: "auto",
                backgroundColor: "secondary.main",
            }}
        >
        <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "left",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "left",
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                display: { xs: "block", md: "none" },
              }}
            >
              {routes.map((page) => (
              !!page.enabled && <Link
                  key={page.key}
                  component={NavLink}
                  to={page.path}
                  color="black"
                  underline="none"
                  variant="button"
                >
                  <MenuItem onClick={handleCloseNavMenu}>
                    <Typography textAlign="center">{page.title}</Typography>
                  </MenuItem>
                </Link>
              ))}
            </Menu>
          </Box>
        <Box
            sx={{
                    display: { xs: "none", md: "flex" },
                flexDirection: "row",
                justifyContent: "flex-start",
                alignItems: "center",                
                width: "100%",
                heigh: "auto",
                backgroundColor: "secondary.light",
                 
            }}
        >
            <img alt="sds" src="favicon.ico" style={{ marginLeft: "2rem"}} />
            {
                routes.map((page) => (
                    !!page.enabled &&
                    <Link
                        key={page.key}
                        component={NavLink}
                        to={page.path}
                        color="black"
                        underline="none"
                        variant="button"
                        sx={{ fontSize: "medium", marginLeft: "2rem", marginBottom: "2rem", marginTop: "2rem"}}
                        >
                        {page.title }
                   </Link>

                ))}
                    <Box
                        sx={{
                            display: "flex",
                            flexDirection: "row",
                            justifyContent: "flex-end",
                            alignItems: "center",
                            width: "100%",
                            heigh: "auto",
                            backgroundColor: "secondary.light",
                            marginRight: "2rem"
                        }}
                    >
                        {!!UserToken.token.token && (
                                <>  <p>{` ${UserToken.token.email}`}  </p> <LoginMenu />  </>
                        )}             
                    </Box>
            </Box>
        </Box>

    );

};
export default observer (Navbar);