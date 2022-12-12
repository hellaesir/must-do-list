// Next.js API route support: https://nextjs.org/docs/api-routes/introduction
import type { NextApiRequest, NextApiResponse } from 'next'
import { AuthResponse } from '../../communication/authResponse';
import { apiApi } from '../../services/api';

const Auth = async (req: NextApiRequest, res: NextApiResponse) => {
  try {
    var response = await apiApi.post(`/auth/login`, req.body);
    res.status(200).json(response.data);
  } catch (ex) {
    res.status(500).send(ex);
  }
}

export default Auth;