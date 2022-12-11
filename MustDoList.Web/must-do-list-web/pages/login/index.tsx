import { Button, Card, CardContent, TextField } from '@material-ui/core';
import { useState, useContext } from 'react';
import { AuthRequest } from '../../src/communication/authRequest';
import { AuthContext } from '../../src/contexts/authContext';
import styles from './style.module.scss'

const Login = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const { signIn } = useContext(AuthContext);

    const Auth = () => {
        let authContext = new AuthRequest();
        authContext.email = email;
        authContext.password = password;

        try {
            signIn(authContext);
        } catch (e) {
            console.log(e);
        }
    }

    return (
        <div className={styles.loginPage}>
            <div className={styles.overlay}></div>
            <Card className={styles.widthCard}>
                <CardContent>
                    <div className={styles.cardcontenta}>
                        <TextField className={styles.text} label="E-mail" type={'email'} value={email} onChange={e => setEmail(e.target.value)}></TextField>
                        <TextField className={styles.text} label="Password" type={'password'} value={password} onChange={e => setPassword(e.target.value)}></TextField><br></br>
                        <Button variant={'contained'} color={'primary'} onClick={Auth}>Login</Button>
                    </div>
                </CardContent>
            </Card>
        </div>
    )
}

export default Login;