using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Web.Security;
using Telerik.Web.UI.Common;

namespace Telerik.Web.UI
{
	// Token: 0x020016A9 RID: 5801
	internal class CryptoService : ICryptoService
	{
		// Token: 0x0600E009 RID: 57353 RVA: 0x0031D8B1 File Offset: 0x0031BAB1
		private CryptoService(string customKey)
		{
			this.customEncryptionKey = customKey;
		}

		// Token: 0x0600E00A RID: 57354 RVA: 0x0031D8C0 File Offset: 0x0031BAC0
		public static ICryptoService GetService(string owner = "")
		{
			if (string.IsNullOrEmpty(owner))
			{
				owner = "Telerik.AsyncUpload.ConfigurationEncryptionKey";
			}
			ICryptoService result;
			lock (CryptoService.thisLock)
			{
				if (!CryptoService.services.ContainsKey(owner))
				{
					CryptoService.services[owner] = new CryptoService(owner);
				}
				result = CryptoService.services[owner];
			}
			return result;
		}

		// Token: 0x0600E00B RID: 57355 RVA: 0x0031D950 File Offset: 0x0031BB50
		public string Encrypt(string plainString)
		{
			string password = this.GetEncryptionKey();
			if (string.IsNullOrEmpty(password))
			{
				return this.EncryptWithMachineKey(plainString);
			}
			return CryptoService.exceptionThrower.ThrowIfFails<string>(() => CryptoService.Encrypt(plainString, password));
		}

		// Token: 0x0600E00C RID: 57356 RVA: 0x0031D9C4 File Offset: 0x0031BBC4
		public string Decrypt(string encryptedString)
		{
			string password = this.GetEncryptionKey();
			if (string.IsNullOrEmpty(password))
			{
				return this.DecryptWithMachineKey(encryptedString);
			}
			return CryptoService.exceptionThrower.ThrowIfFails<string>(() => CryptoService.Decrypt(encryptedString, password));
		}

		// Token: 0x0600E00D RID: 57357 RVA: 0x0031DA44 File Offset: 0x0031BC44
		public void CheckWhitelistTypes(Type type, string allowedCustomMetaTypes, string uploadMetaDataFullName)
		{
			CryptoService.exceptionThrower.ThrowIfFails<bool>(() => this.CheckWhitelist(type, allowedCustomMetaTypes, uploadMetaDataFullName));
		}

		// Token: 0x0600E00E RID: 57358 RVA: 0x0031DA8C File Offset: 0x0031BC8C
		private bool CheckWhitelist(Type type, string allowedCustomMetaTypes, string uploadMetaDataFullName)
		{
			if (type.FullName == uploadMetaDataFullName)
			{
				return true;
			}
			if (allowedCustomMetaTypes != null)
			{
				foreach (string text in allowedCustomMetaTypes.Split(new char[]
				{
					';'
				}))
				{
					if (type.FullName == text.Trim())
					{
						return true;
					}
				}
			}
			throw new Exception();
		}

		// Token: 0x0600E00F RID: 57359 RVA: 0x0031DAF5 File Offset: 0x0031BCF5
		private string GetEncryptionKey()
		{
			return ConfigurationManager.AppSettings.Get(this.customEncryptionKey);
		}

		// Token: 0x0600E010 RID: 57360 RVA: 0x0031DB08 File Offset: 0x0031BD08
		internal static string Encrypt(string clearText, string password)
		{
			byte[] bytes = Encoding.Unicode.GetBytes(clearText);
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, CryptoService.SALT);
			byte[] inArray = CryptoService.Encrypt(bytes, rfc2898DeriveBytes.GetBytes(32), rfc2898DeriveBytes.GetBytes(16));
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x0600E011 RID: 57361 RVA: 0x0031DB4C File Offset: 0x0031BD4C
		private static byte[] Encrypt(byte[] clearData, byte[] key, byte[] iv)
		{
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, new AesCryptoServiceProvider
			{
				Key = key,
				IV = iv
			}.CreateEncryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(clearData, 0, clearData.Length);
			cryptoStream.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x0600E012 RID: 57362 RVA: 0x0031DB9C File Offset: 0x0031BD9C
		internal static string Decrypt(string encryptedString, string password)
		{
			byte[] encryptedBytes = Convert.FromBase64String(encryptedString);
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, CryptoService.SALT);
			byte[] bytes = CryptoService.Decrypt(encryptedBytes, rfc2898DeriveBytes.GetBytes(32), rfc2898DeriveBytes.GetBytes(16));
			return Encoding.Unicode.GetString(bytes);
		}

		// Token: 0x0600E013 RID: 57363 RVA: 0x0031DBE0 File Offset: 0x0031BDE0
		private static byte[] Decrypt(byte[] encryptedBytes, byte[] key, byte[] iv)
		{
			MemoryStream memoryStream = new MemoryStream();
			CryptoStream cryptoStream = new CryptoStream(memoryStream, new AesCryptoServiceProvider
			{
				Key = key,
				IV = iv
			}.CreateDecryptor(), CryptoStreamMode.Write);
			cryptoStream.Write(encryptedBytes, 0, encryptedBytes.Length);
			cryptoStream.Close();
			return memoryStream.ToArray();
		}

		// Token: 0x0600E014 RID: 57364 RVA: 0x0031DC30 File Offset: 0x0031BE30
		public string EncryptWithMachineKey(string clearText)
		{
			byte[] bytes = Encoding.UTF8.GetBytes(clearText);
			byte[] inArray = MachineKey.Protect(bytes, new string[]
			{
				CryptoService.purpose
			});
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x0600E015 RID: 57365 RVA: 0x0031DC68 File Offset: 0x0031BE68
		public string DecryptWithMachineKey(string encryptedText)
		{
			byte[] protectedData = Convert.FromBase64String(encryptedText);
			byte[] bytes = MachineKey.Unprotect(protectedData, new string[]
			{
				CryptoService.purpose
			});
			return Encoding.UTF8.GetString(bytes);
		}

		// Token: 0x040040CB RID: 16587
		private const int ENCRYPTION_KEY_SIZE = 32;

		// Token: 0x040040CC RID: 16588
		private const int INTERVAL_VECTOR_SIZE = 16;

		// Token: 0x040040CD RID: 16589
		private static readonly string purpose = "CloudUploadMachineKeyEncryptionPurpose";

		// Token: 0x040040CE RID: 16590
		private static readonly Dictionary<string, ICryptoService> services = new Dictionary<string, ICryptoService>();

		// Token: 0x040040CF RID: 16591
		private static object thisLock = new object();

		// Token: 0x040040D0 RID: 16592
		private static readonly ICryptoExceptionThrower exceptionThrower = new CryptoExceptionThrower();

		// Token: 0x040040D1 RID: 16593
		private readonly string customEncryptionKey;

		// Token: 0x040040D2 RID: 16594
		private static readonly byte[] SALT = new byte[]
		{
			58,
			84,
			91,
			25,
			10,
			34,
			29,
			68,
			60,
			88,
			44,
			51,
			1
		};
	}
}
