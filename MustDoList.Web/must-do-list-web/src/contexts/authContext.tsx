import axios from "axios";
import Router from "next/router";
import { parseCookies, setCookie } from "nookies";
import { createContext, ReactNode, useEffect, useState } from "react";
import { AuthRequest } from "../communication/authRequest";
import { AuthResponse } from "../communication/authResponse";
import { User } from "../models/user";
import { apiBase } from "../services/api";


type AuthContextType = {
    isAuthenticated: boolean;
    user: User | null;
    signIn: (authRequest: AuthRequest) => Promise<void>;
}


export const AuthContext = createContext({} as AuthContextType);



const AuthProvider = ({ children }: { children: any }) => {
    const [user, setUser] = useState<User | null>(null);

    const isAuthenticated = !!user;

    useEffect(() => {
        const { 'mustdotoken-token': token } = parseCookies();


    }, [])

    async function signIn(authRequest: AuthRequest) {
        var response = await apiBase.post(`/api/auth`, authRequest);
        var authResponse: AuthResponse = response.data;

        var user: User = new User(authResponse.userId, authResponse.userName, authResponse.userEmail, authResponse.refreshToken, authResponse.accessToken);

        setCookie(undefined, "mustdotoken-token", user.BearerToken, {
            maxAge: 60 * 60 * 1 // 1hora
        });

        setUser(user);

        Router.push('/');
    }

    return (
        <AuthContext.Provider value={{ user, signIn, isAuthenticated }}>
            {children}
        </AuthContext.Provider>
    )
}

export default AuthProvider;