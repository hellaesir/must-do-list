// Next.js API route support: https://nextjs.org/docs/api-routes/introduction
import type { NextApiRequest, NextApiResponse } from 'next'
import {  ApiService } from '../../services/ApiService';

const Auth = async (req: NextApiRequest, res: NextApiResponse) => {
  try {
    var apiClient = new ApiService('API', req); 
    var response = await apiClient.Post(`/auth/login`, req.body);

    res.status(200).json(response);
  } catch (ex) {
    res.status(500).send(ex);
  }
}

export default Auth;