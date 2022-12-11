import Router from "next/router";
import { parseCookies, setCookie } from "nookies";
import { createContext, ReactNode, useEffect, useState } from "react";
import { AuthRequest } from "../communication/authRequest";
import { User } from "../models/user";


type AuthContextType = {
    isAuthenticated: boolean;
    user: User | null;
    signIn: (authRequest: AuthRequest) => Promise<void>;
}


export const AuthContext = createContext({} as AuthContextType);



const AuthProvider = ({children} :{children:any}) => {
    const [user, setUser] = useState<User | null>(null);
    
    const isAuthenticated = !!user;

    useEffect(() => {
        const  {'mustdotoken-token': token} = parseCookies();


    }, [])

    async function signIn(authRequest: AuthRequest) {
        await fetch(`/api/auth`,
            {
                method: "POST",
                body: JSON.stringify(authRequest)
            })
            .then(res => res.json())
            .then(f => console.log(f));

        setCookie(undefined, "mustdotoken-token", "", {
            maxAge: 60 * 60 * 1 // 1hora
        });

        setUser(new User());

        Router.push('/');
    }

    return (
        <AuthContext.Provider value={{ user, signIn, isAuthenticated }}>
            {children}
        </AuthContext.Provider>
    )
}

export default AuthProvider;