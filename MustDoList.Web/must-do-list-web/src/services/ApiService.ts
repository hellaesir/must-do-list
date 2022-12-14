import { parseCookies } from "nookies";
import * as next from 'next';

export class ApiService {
    TokenJWT: string = "";
    UrlBase: string | undefined = "";

    constructor(urlType: 'API'|'INTERNAL', req?: next.NextApiRequest) {
        const { ["mustdotoken-token"]: tokenAux } = parseCookies({ req });
        this.TokenJWT = tokenAux;

        this.UrlBase = urlType == "API" ? process.env.API_URL : process.env.BASE_URL;
    }

    public Get<T>(path: string): Promise<T> {
        const options = {
            method: "GET",
            headers: {
                Accept: "application/json",
                Authorization: `Bearer ${this.TokenJWT}`,
            }
        };

        var ret = "";

        return fetch(this.UrlBase + path, options)
            .then(f => {
                if (!f.ok) {
                    throw new Error(f.statusText);
                }

                return f.json().then(data => data as T);
            });
    }

    public Post<T>(path: string, bodyData: any): Promise<T> {
        const options = {
            method: "POST",
            headers: {
                Accept: "application/json",
                "Content-Type": "application/json;charset=UTF-8",
                Authorization: `Bearer ${this.TokenJWT}`
            },
            body: JSON.stringify(bodyData),
        };

        return fetch(this.UrlBase + path, options)
            .then(f => { return f.json().then(data => data as T) });
    }
}