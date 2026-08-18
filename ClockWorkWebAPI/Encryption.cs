using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;

namespace ClockWorkWebAPI
{
	// Token: 0x02000017 RID: 23
	[Obsolete("Use ClockWork EncryptionFactory")]
	[Serializable]
	public class Encryption
	{
		// Token: 0x06000161 RID: 353 RVA: 0x0000A7D4 File Offset: 0x000089D4
		public static string AESEncrypt(string PlainText, string Password, string Salt, string HashAlgorithm, int PasswordIterations, string InitialVector, int KeySize)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(InitialVector);
			byte[] bytes2 = Encoding.ASCII.GetBytes(Salt);
			byte[] bytes3 = Encoding.UTF8.GetBytes(PlainText);
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, bytes2, HashAlgorithm, PasswordIterations);
			byte[] bytes4 = passwordDeriveBytes.GetBytes(KeySize / 8);
			ICryptoTransform transform = new RijndaelManaged
			{
				Mode = CipherMode.CBC
			}.CreateEncryptor(bytes4, bytes);
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write);
			cryptoStream.Write(bytes3, 0, bytes3.Length);
			cryptoStream.FlushFinalBlock();
			byte[] inArray = memoryStream.ToArray();
			memoryStream.Close();
			cryptoStream.Close();
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x06000162 RID: 354 RVA: 0x0000A888 File Offset: 0x00008A88
		public static byte[] AESEncrypt2(byte[] PlainTextBytes, string Password, string Salt, string HashAlgorithm, int PasswordIterations, string InitialVector, int KeySize)
		{
			return PlainTextBytes;
		}

		// Token: 0x06000163 RID: 355 RVA: 0x0000A8A0 File Offset: 0x00008AA0
		public static string AESDecrypt(string CipherText, string Password, string Salt, string HashAlgorithm, int PasswordIterations, string InitialVector, int KeySize)
		{
			byte[] bytes = Encoding.ASCII.GetBytes(InitialVector);
			byte[] bytes2 = Encoding.ASCII.GetBytes(Salt);
			byte[] array = Convert.FromBase64String(CipherText);
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(Password, bytes2, HashAlgorithm, PasswordIterations);
			byte[] bytes3 = passwordDeriveBytes.GetBytes(KeySize / 8);
			ICryptoTransform transform = new RijndaelManaged
			{
				Mode = CipherMode.CBC
			}.CreateDecryptor(bytes3, bytes);
			MemoryStream memoryStream = new MemoryStream(array);
			CryptoStream cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Read);
			byte[] array2 = new byte[array.Length];
			int count = cryptoStream.Read(array2, 0, array2.Length);
			memoryStream.Close();
			cryptoStream.Close();
			return Encoding.UTF8.GetString(array2, 0, count);
		}

		// Token: 0x06000164 RID: 356 RVA: 0x0000A954 File Offset: 0x00008B54
		public static byte[] AESDecrypt2(byte[] CipherTextBytes, string Password, string Salt, string HashAlgorithm, int PasswordIterations, string InitialVector, int KeySize)
		{
			return CipherTextBytes;
		}

		// Token: 0x06000165 RID: 357 RVA: 0x0000A96C File Offset: 0x00008B6C
		private static string CreateSalt(int size)
		{
			RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider();
			byte[] array = new byte[size];
			rngcryptoServiceProvider.GetBytes(array);
			return Convert.ToBase64String(array);
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000A99C File Offset: 0x00008B9C
		private static string CreatePasswordHash(string pwd, string salt)
		{
			string password = pwd + salt;
			string str = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "SHA1");
			return str + salt;
		}

		// Token: 0x06000167 RID: 359 RVA: 0x0000A9CC File Offset: 0x00008BCC
		public static string CreatePasswordHash(string pwd)
		{
			int size = 5;
			string salt = Encryption.CreateSalt(size);
			return Encryption.CreatePasswordHash(pwd, salt);
		}

		// Token: 0x06000168 RID: 360 RVA: 0x0000A9F0 File Offset: 0x00008BF0
		public static bool VerifyPassword(string pwd, string pwdHash)
		{
			int num = 5;
			int num2 = pwdHash.Length - num;
			bool flag = num2 > 0;
			bool result;
			if (flag)
			{
				string pwd2 = pwdHash.Substring(num2);
				string text = Encryption.CreatePasswordHash(pwd2);
				result = (text.CompareTo(pwdHash) == 0);
			}
			else
			{
				result = false;
			}
			return result;
		}
	}
}
