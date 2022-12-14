import { NextApiRequest, NextApiResponse } from "next";
import { ApiService } from "../../services/ApiService";

const Todo = async (req: NextApiRequest, res: NextApiResponse) => {
    var api = new ApiService("API", req);
    var responseApi = await api.Get<any>(`/todo/list?pageNumber=1&pageSize=100`);

    console.log(responseApi);

    if (responseApi)
        res.status(200).send(responseApi);
    else
        res.status(500).send("error");
}

export default Todo;