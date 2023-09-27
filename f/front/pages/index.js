import Head from 'next/head'
import Image from 'next/image'
import { Inter } from 'next/font/google'
//import styles from '@/styles/Home.module.css'
import Link from 'next/link'

const inter = Inter({ subsets: ['latin'] })

export default function Home() {
  return (
    <>
      <Head>
        <title>Strona główna</title>
      </Head>
        <h2>
          <Link href="/menuf/menu">Menu</Link>
        </h2>
        <h2>
          <Link href="/adminview/add">Admin</Link>
        </h2>
      
    </>
  )
}
