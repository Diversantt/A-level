import { FC, ReactElement, useEffect, useState } from "react";
import { IResource } from "../../interfaces/resource";
import {  getResourceByPage } from "../../Api/modules/resourceApi";
import { Box, Grid, Pagination } from "@mui/material";
import { ResourceCard } from "./components/ResourceCard";


const Resources: FC<any> = (): ReactElement => {
    const [resources, setResources] = useState<IResource[] | null>(null)
    const [totalPages, setTotalPages] = useState<number>(1)
    const [currentPages, setCurrentPages] = useState<number>(1)

    useEffect(() => {
        const getResource = async () => {
            try {
                const res = await getResourceByPage(currentPages)
                setResources(res.data)
                setTotalPages(res.total_pages)
            }
            catch (e) {
                if (e instanceof Error) {
                    console.error(e.message)
                }   
            }
        }
        getResource()
    }, [currentPages])

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
                        {resources?.map((item) => (
                            <Grid key={item.id} item lg={2} md={3} xs={6}>
                                <ResourceCard {...item} />
                            </Grid>
                        ))}
                    </>
                </Grid>
                <Box sx={{
                    display: 'flex',
                    justifyContent: 'center'
                }}>
                    <Pagination variant="outlined" color="secondary" count={totalPages} page={currentPages} onChange={(event, page) => setCurrentPages(page)} />
                </Box>
            </Box>
        </Box>
    );
}

export default Resources