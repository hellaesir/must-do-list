import { NextRequest, NextResponse } from "next/server";
import { useContext } from "react";
import { AuthContext } from "./contexts/authContext";

const middleware = (req: NextRequest) => {
    //https://www.reddit.com/r/nextjs/comments/rfjms6/next_js_12_middleware_and_context_hook/
    //const authContext = useContext(AuthContext);

    var token = req.cookies.get('mustdotoken-token');

    if (req.url == process.env.BASE_URL && req.url.indexOf("login") <= 0) {
        if (!token) {
            return NextResponse.redirect(new URL('/login', req.url))
        }
    }
}

export default middleware;