import {API_HOST, HTTP} from "../GlobalConsts";

export async function selectAsync(dataRequest: object): Promise<[]> {
    return await fetchDataAsync(dataRequest, "Select");
}

export async function insertAsync(dataRequest: object): Promise<object> {
    return await fetchDataAsync(dataRequest, "Insert");
}

export async function updateAsync(dataRequest: object): Promise<object> {
    return await fetchDataAsync(dataRequest, "Update");
}

export async function deleteAsync(dataRequest: object): Promise<object> {
    return await fetchDataAsync(dataRequest, "Delete");
}

async function fetchDataAsync(dataRequest: object, endpoint: string): Promise<any> {
    let response = await fetch(`${HTTP}://${API_HOST}/Data/${endpoint}`, {
        method: 'POST',
        headers: {
            'Content-Type': 'application/json;charset=utf-8'
        },
        body: JSON.stringify(dataRequest)
    });

    let jsonResponse =  await response.json();
    return await JSON.parse(jsonResponse);
}