// Next.js API route support: https://nextjs.org/docs/api-routes/introduction
import type { NextApiRequest, NextApiResponse } from 'next'
import { User } from '../../src/models/user';

const Auth = async (req: NextApiRequest, res: NextApiResponse<User|null>) => {
  try {
    let rawResponse = await fetch(`${process.env.API_URL}/auth/login`, {
      method: "POST",
      headers: {
        'Accept': 'application/json',
        'Content-Type': 'application/json'
      },
      body: req.body
    });

    let content = await rawResponse.json();
    res.status(200).send(content);
  } catch (ex) {
    res.status(500).send(null);
  }
}

export default Auth;