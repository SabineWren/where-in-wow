type Player = { Name: string, Score: number, Accuracy: number }
export const GetLeaderboard = (_isClassic: boolean) =>
	Promise.resolve([{ Name: "Not Implemented", Score: 0, Accuracy: 0.0 }] as Player[])

type GuessResponse = {
	distPct: number; lives: number; score: number; lat: number; lng: number
	mapID: number, locName: string; zoneName: string; result: number; location?: Md5 }
export const GuessMap = function(token: string, lat: number, lng: number, mapId: string | null) {
	const map = mapId ? { mapID: mapId } : {}
	const _req = { action: "guess", token: token, lat: lat, lng: lng, ...map }
	//return sendRequest(req)
	const res: GuessResponse = {
		distPct: 0.0, lives: 3, score: 1, lat: lat, lng: lng,
		mapID: 0, locName: "loc", zoneName: "zone",
		result: 2, location: "0a49eafe7ab1ebbc34b7a0756a7ff9b0",
	}
	return Promise.resolve(res)
}

export const InitSession = async function(_isClassic: boolean, _resume: string | null, _clear: string | null) {
	/*const init = { action: "init", mode: isClassic ? 2 : 1 }
	if (!resume && !clear) {
		return sendRequest(init)
	} else if (!clear) {
		const initReq = { resumeToken: resume, ...init }
		return sendRequest(initReq)
	} else {
		const initReq = { clearToken: clear, ...init }
		return sendRequest(initReq)
	}*/
	const location = await postNoPayload<LocationMeta>("/init")
	return { token: "", location: location.LocId }
}

export const Resume = (lastSession: string) =>
	sendRequest({ action: "resume", token: lastSession })

export const SubmitScore = (token: string, name: string, uid: string) =>
	sendRequest({ action: "submit", token: token, name: name, uid: uid })

const sendRequest = async (req) => {
	const res = await fetch("endpoint.php", {
		method: "POST",
		headers: {
			"Accept": "application/json",
			"Content-Type": "application/json",
		},
		body: JSON.stringify(req),
	})

	const json = await res.json()
	if (json.error) { console.error(json.error) }

	return json
}

const postNoPayload = async function<T>(route: string) {
	const init: RequestInit = {
		method: "POST",
		headers: { Accept: "application/json" },
	}
	const res = await fetch(route, init)
	const json = await res.json()
	if (json.error) { console.error(json.error) }
	return json as T
}
