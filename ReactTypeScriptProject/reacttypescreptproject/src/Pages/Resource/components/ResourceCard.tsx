import { FC, ReactElement } from "react";
import { IResource } from "../../../interfaces/resource";
import { Card, CardActionArea, CardContent, CardMedia, Typography } from "@mui/material";



export const ResourceCard: FC<IResource> = (props): ReactElement => {


	return (
		<Card sx={{ height: 150, width: 250, backgroundColor: props.color }} >
			<CardActionArea>
				<CardMedia   />

				<CardContent  >
					<Typography noWrap gutterBottom variant="h5" component="div" >
						{props.name}
					</Typography>
					<Typography variant="body1" color="text.secondary">
						Year: {props.year}  
					</Typography>
					<Typography variant="body1" color="text.secondary">
						 Value: {props.pantone_value}
					</Typography>
				</CardContent>		
			</CardActionArea>
		</Card>
	)

}