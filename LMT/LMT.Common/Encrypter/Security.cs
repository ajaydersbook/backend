using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace LMT.Common.Encrypter
{
	public class Security
	{
		public string EncryptionKey()
		{
			return "DersBook";
		}

		//Used for Password while user creation
		/// <summary>
		/// 
		/// </summary>
		/// <param name="key"></param>
		/// <param name="data"></param>
		/// <returns></returns>
		public string Encrypt(string key, string data)
		{
			string encData = null;
			byte[][] keys = GetHashKeys(key);

			try
			{
				encData = EncryptStringToBytes_Aes(data, keys[0], keys[1]);
			}
			catch (CryptographicException) { }
			catch (ArgumentNullException) { }

			return encData;
		}

		//Not used
		public string Decrypt(string key, string data)
		{
			string decData = null;
			byte[][] keys = GetHashKeys(key);

			try
			{
				decData = DecryptStringFromBytes_Aes(data, keys[0], keys[1]);
			}
			catch (CryptographicException) { }
			catch (ArgumentNullException) { }

			return decData;
		}

		private byte[][] GetHashKeys(string key)
		{
			byte[][] result = new byte[2][];
			Encoding enc = Encoding.UTF8;

			SHA256 sha2 = new SHA256CryptoServiceProvider();

			byte[] rawKey = enc.GetBytes(key);
			byte[] rawIV = enc.GetBytes(key);

			byte[] hashKey = sha2.ComputeHash(rawKey);
			byte[] hashIV = sha2.ComputeHash(rawIV);

			Array.Resize(ref hashIV, 16);

			result[0] = hashKey;
			result[1] = hashIV;

			return result;
		}

		//Not Used
		//source: https://msdn.microsoft.com/de-de/library/system.security.cryptography.aes(v=vs.110).aspx
		private static string EncryptStringToBytes_Aes(string plainText, byte[] Key, byte[] IV)
		{
			if (plainText == null || plainText.Length <= 0)
				throw new ArgumentNullException("plainText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");

			byte[] encrypted;

			using (AesManaged aesAlg = new AesManaged())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msEncrypt = new MemoryStream())
				{
					using (CryptoStream csEncrypt =
							new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
					{
						using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
						{
							swEncrypt.Write(plainText);
						}
						encrypted = msEncrypt.ToArray();
					}
				}
			}
			return Convert.ToBase64String(encrypted);
		}

		//Not Used
		//source: https://msdn.microsoft.com/de-de/library/system.security.cryptography.aes(v=vs.110).aspx
		private static string DecryptStringFromBytes_Aes(string cipherTextString, byte[] Key, byte[] IV)
		{
			byte[] cipherText = Convert.FromBase64String(cipherTextString);

			if (cipherText == null || cipherText.Length <= 0)
				throw new ArgumentNullException("cipherText");
			if (Key == null || Key.Length <= 0)
				throw new ArgumentNullException("Key");
			if (IV == null || IV.Length <= 0)
				throw new ArgumentNullException("IV");

			string plaintext = null;

			using (Aes aesAlg = Aes.Create())
			{
				aesAlg.Key = Key;
				aesAlg.IV = IV;

				ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);

				using (MemoryStream msDecrypt = new MemoryStream(cipherText))
				{
					using (CryptoStream csDecrypt =
							new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read))
					{
						using (StreamReader srDecrypt = new StreamReader(csDecrypt))
						{
							plaintext = srDecrypt.ReadToEnd();
						}
					}
				}
			}
			return plaintext;
		}

		//Not Used
		public static class EncryptionUtilities
		{
			private const int SALT_SIZE = 8;
			private const int NUM_ITERATIONS = 1000;

			private static readonly RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider();

			/// <summary>
			/// Creates a signature for a password.
			/// </summary>
			/// <param name="password">The password to hash.</param>
			/// <returns>the "salt:hash" for the password.</returns>
			public static string CreatePasswordSalt(string Password)
			{
				byte[] buf = new byte[SALT_SIZE];
				rng.GetBytes(buf);
				string salt = Convert.ToBase64String(buf);

				Rfc2898DeriveBytes deriver2898 = new Rfc2898DeriveBytes(Password.Trim(), buf, NUM_ITERATIONS);
				string hash = Convert.ToBase64String(deriver2898.GetBytes(16));
				return salt + '~' + hash;
			}

			//Not Used
			/// <summary>
			/// Validate if a password will generate the passed in salt:hash.
			/// </summary>
			/// <param name="Password">The password to validate.</param>
			/// <param name="saltHash">The "salt:hash" this password should generate.</param>
			/// <returns>true if we have a match.</returns>
			public static bool IsPasswordValid(string Password, string saltHash)
			{
				string[] parts = saltHash.Split(new[] { ':' }, StringSplitOptions.RemoveEmptyEntries);

				if (parts.Length != 2)

					return false;
				byte[] buf = Convert.FromBase64String(parts[0]);
				Rfc2898DeriveBytes deriver2898 = new Rfc2898DeriveBytes(Password.Trim(), buf, NUM_ITERATIONS);
				string computedHash = Convert.ToBase64String(deriver2898.GetBytes(16));
				return parts[1].Equals(computedHash);
			}
		}

		//Not Used
		private string Encrypt(string clearText)
		{
			string EncryptionKey = "SpAnDaNaInDiA1998";
			byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
			using (Aes encryptor = Aes.Create())
			{
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
					{
						cs.Write(clearBytes, 0, clearBytes.Length);
						cs.Close();
					}
					clearText = Convert.ToBase64String(ms.ToArray());
				}
			}
			return clearText;
		}

		//Not Used
		private string Decrypt(string cipherText)
		{
			string EncryptionKey = "SpAnDaNaInDiA1998";
			byte[] cipherBytes = Convert.FromBase64String(cipherText);
			using (Aes encryptor = Aes.Create())
			{
				Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
				encryptor.Key = pdb.GetBytes(32);
				encryptor.IV = pdb.GetBytes(16);
				using (MemoryStream ms = new MemoryStream())
				{
					using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
					{
						cs.Write(cipherBytes, 0, cipherBytes.Length);
						cs.Close();
					}
					cipherText = Encoding.Unicode.GetString(ms.ToArray());
				}
			}
			return cipherText;
		}

		public string CreateSalt(int size)
		{
			var rng = new System.Security.Cryptography.RNGCryptoServiceProvider();
			var buff = new byte[size];
			rng.GetBytes(buff);
			return Convert.ToBase64String(buff);
		}

		public string GenerateSHA256Hash(String input, String salt)
		{
			byte[] bytes = System.Text.Encoding.UTF8.GetBytes(input + salt);
			System.Security.Cryptography.SHA256Managed sHAHashString =
				new System.Security.Cryptography.SHA256Managed();
			byte[] hash = sHAHashString.ComputeHash(bytes);
			return ByteArrayToHexString(hash);
		}

		public static string ByteArrayToHexString(byte[] ba)
		{
			return BitConverter.ToString(ba).Replace("-", "");
		}


	}
}
