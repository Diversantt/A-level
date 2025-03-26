
const url = "https://reqres.in/api/"

interface apiClientProps {
	path: string
	method: string
	data?: unknown
}

const handleResponce = async (response: Response) => {
	if (!response.ok) {
		const errorMessage = await response.json()
		throw Error(errorMessage.error || 'Request error')
	}
	return response.json()
}

export const apiClient = async ({ path, method, data }: apiClientProps) => {	
	const requestOptions = {
		method,
		headers: { 'Content-Type': 'application/json' },
		body: !!data ? JSON.stringify(data) : undefined
	}
	return await fetch(`${url}${path}`, requestOptions).then(handleResponce)
	
}