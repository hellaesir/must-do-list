import { AppBar, Button, FormControlLabel, IconButton, Switch, Toolbar, Typography } from '@material-ui/core'
import TextField from '@material-ui/core/TextField'
import Head from 'next/head'
import styles from '../styles/Home.module.css'
import MenuIcon from '@mui/icons-material/Menu';

export default function Home() {
  return (<>
    <AppBar position="static">
      <Toolbar variant="dense">
        <IconButton edge={'start'} color={'inherit'} aria-label="menu">
          <MenuIcon></MenuIcon>
        </IconButton>
        <Typography variant="h6" color="inherit" component="div">
          Must Do List
        </Typography>
      </Toolbar>
    </AppBar>
    <div className={styles.container}>
      <Head>
        <title>Create Next App</title>
        <meta name="description" content="Generated by create next app" />
        <link rel="icon" href="/favicon.ico" />
      </Head>

      <main className={styles.main}>
        <TextField id="txtTitulo" label="Título" variant="standard" />
        <FormControlLabel control={<Switch></Switch>} label={`Finalizado`}></FormControlLabel>
        <Button variant={'contained'} color={'primary'}>Salvar</Button>
      </main>
    </div>
  </>
  )
}