import Head from 'next/head'
import Image from 'next/image'
import { Inter } from 'next/font/google'
import styles from '@/styles/Home.module.css'
import Link from 'next/link'
import Navigationtemplate from './navigationtemplate/navigationtemplate.js'

const inter = Inter({ subsets: ['latin'] })


export default function Home() {
  return (
    <div>
      <Head>
        <title>Strona główna</title>
      </Head>
        <header>
            
        </header>
      <Navigationtemplate></Navigationtemplate> 
      

    </div>
  )
}
