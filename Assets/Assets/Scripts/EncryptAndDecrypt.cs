using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace EncryptTest
{
	class EncryptAndDecrypt
	{
		public static void Main()
		{

			UnicodeEncoding ByteConverter = new UnicodeEncoding();
			RSACryptoServiceProvider RSA = new RSACryptoServiceProvider();
			byte[] plaintext;
			byte[] encryptedtext;
			string enc = " ";
			string dec = " ";

			try
			{

				//read in the original file
				string[] lines = File.ReadAllLines("C:/Users/tayma/OneDrive/Documents/TayStuff/CS3160 Concepts/Final_Project/OutputFiles/" + PlayerInfoInput.playername + "_" + DateTime.Today.ToString("MM_dd_yyy") + ".txt", Encoding.UTF8);

				//encrypt the current file
				foreach (string line in lines)
				{
					plaintext = ByteConverter.GetBytes(line);
					encryptedtext = Encryption(plaintext, RSA.ExportParameters(false), false);

					enc = ByteConverter.GetString(encryptedtext);

					//write the encrypted lines to the same file 
					File.WriteAllText("C:/Users/tayma/OneDrive/Documents/TayStuff/CS3160 Concepts/Final_Project/OutputFiles/" + PlayerInfoInput.playername + "_" + DateTime.Today.ToString("MM_dd_yyy") + ".txt", String.Empty);
					using (StreamWriter sw = File.AppendText("C:/Users/tayma/OneDrive/Documents/TayStuff/CS3160 Concepts/Final_Project/OutputFiles/" + PlayerInfoInput.playername + "_" + DateTime.Today.ToString("MM_dd_yyy") + ".txt"))
					{
						sw.WriteLine(enc);
					}

					//WARNING: Uncommenting the following lines will run the game with decrypted files rather than encrypted file. PROCEED WITH CAUTION

					//byte[] decryptedtex = Decryption(encryptedtext, RSA.ExportParameters(true), false);
					//dec = ByteConverter.GetString(decryptedtex);
					//using (StreamWriter s = File.AppendText("C:/Users/tayma/OneDrive/Documents/TayStuff/CS3160 Concepts/Final_Project/OutputFiles/" + PlayerInfoInput.playername + "_" + DateTime.Today.ToString("MM_dd_yyy") + "DECRYPTED.txt"))
					//{
					//	s.WriteLine(dec);
					//}
				}

			}
			catch (FileNotFoundException e)
			{
				Debug.Log("File could not be found: Exception: " + e);
			}
			catch (IOException e)
			{
				Debug.Log("File could not be opened: Exception: " + e);
			}

		}

		//encryption 
		static public byte[] Encryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
		{
			try
			{
				byte[] encryptedData;
				using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
				{
					RSA.ImportParameters(RSAKey);
					encryptedData = RSA.Encrypt(Data, DoOAEPPadding);
				}
				return encryptedData;
			}
			catch (CryptographicException e)
			{
				Console.WriteLine(e.Message);
				return null;
			}
		}

		//decryption
		static public byte[] Decryption(byte[] Data, RSAParameters RSAKey, bool DoOAEPPadding)
		{
			try
			{
				byte[] decryptedData;
				using (RSACryptoServiceProvider RSA = new RSACryptoServiceProvider())
				{
					RSA.ImportParameters(RSAKey);
					decryptedData = RSA.Decrypt(Data, DoOAEPPadding);
				}
				return decryptedData;
			}
			catch (CryptographicException e)
			{
				Console.WriteLine(e.ToString());
				return null;
			}
		}
	}
}
