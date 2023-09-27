import Link from 'next/link';
import Head from 'next/head'
import Image from 'next/image';
import Script from 'next/script';
import Layout from '../../components/layout';
import MenuList from "../api/getmenu";


const YourComponent = () => (
    <Image
      src="/images/drwal.jpg" // Route of the image file
      height={144} // Desired size with correct aspect ratio
      width={144} // Desired size with correct aspect ratio
      alt="burger drwala"
    />
  );


export default function FirstPost() {
  return (
    <>
    <Head>
            <title>Menu</title>
            <Script
            src="https://connect.facebook.net/en_US/sdk.js"
            strategy="lazyOnload"
            onLoad={() =>
            console.log(`script loaded correctly, window.FB has been populated`)
            }
            ></Script>
        </Head>
    <Layout>
        
        <MenuList/>      
        <h2>
            <Link href="/">Back to home</Link>
        </h2> 
    </Layout>
    </>    
  );
}