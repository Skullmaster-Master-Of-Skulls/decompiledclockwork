using System;
using System.IO;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionClassLibrary
{
	// Token: 0x02000007 RID: 7
	public class DPAPIEncryptionV2
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00003610 File Offset: 0x00001810
		static DPAPIEncryptionV2()
		{
			using (Stream manifestResourceStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("EncryptionClassLibrary.Resources.clock.ico"))
			{
				DPAPIEncryptionV2.aditional_entropy = new byte[manifestResourceStream.Length];
				manifestResourceStream.Read(DPAPIEncryptionV2.aditional_entropy, 0, (int)manifestResourceStream.Length);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00003674 File Offset: 0x00001874
		public static byte[] ProtectData(string plain_text, ProtectionScope dpScope)
		{
			byte[] data = DPAPIEncryptionV2.StringToByteArray(plain_text);
			return DPAPIEncryptionV2.ProtectData(data, dpScope);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00003694 File Offset: 0x00001894
		public static byte[] ProtectData(byte[] data, ProtectionScope dpScope)
		{
			byte[] result;
			try
			{
				result = ProtectedData.Protect(data, DPAPIEncryptionV2.aditional_entropy, (DataProtectionScope)dpScope);
			}
			catch (CryptographicException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000036CC File Offset: 0x000018CC
		public static string ProtectDataString(byte[] data, ProtectionScope dpScope)
		{
			byte[] array = DPAPIEncryptionV2.ProtectData(data, dpScope);
			return (array == null) ? string.Empty : DPAPIEncryptionV2.ByteArrayToString(array);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000036F8 File Offset: 0x000018F8
		public static string ProtectDataString(string plain_text, ProtectionScope dpScope)
		{
			byte[] array = DPAPIEncryptionV2.ProtectData(plain_text, dpScope);
			return (array == null) ? string.Empty : DPAPIEncryptionV2.ByteArrayToString(array);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00003724 File Offset: 0x00001924
		public static byte[] UnProtectData(byte[] encData, ProtectionScope dpScope)
		{
			byte[] result;
			try
			{
				result = ProtectedData.Unprotect(encData, DPAPIEncryptionV2.aditional_entropy, (DataProtectionScope)dpScope);
			}
			catch (CryptographicException)
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x0000375C File Offset: 0x0000195C
		public static string UnProtectDataString(string encText, ProtectionScope dpScope)
		{
			byte[] encData = DPAPIEncryptionV2.StringToByteArray(encText);
			return DPAPIEncryptionV2.UnProtectDataString(encData, dpScope);
		}

		// Token: 0x0600003C RID: 60 RVA: 0x0000377C File Offset: 0x0000197C
		public static string UnProtectDataString(byte[] encData, ProtectionScope dpScope)
		{
			byte[] array = DPAPIEncryptionV2.UnProtectData(encData, dpScope);
			bool flag = array == null;
			string result;
			if (flag)
			{
				result = string.Empty;
			}
			else
			{
				try
				{
					result = DPAPIEncryptionV2.ByteArrayToString(array);
				}
				catch
				{
					result = string.Empty;
				}
			}
			return result;
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000037C8 File Offset: 0x000019C8
		public static string UnProtectData(string encText, ProtectionScope dpScope)
		{
			string result;
			try
			{
				byte[] encData = DPAPIEncryptionV2.StringToByteArray(encText);
				byte[] encData2 = DPAPIEncryptionV2.UnProtectData(encData, dpScope);
				result = DPAPIEncryptionV2.ByteArrayToString(encData2);
			}
			catch
			{
				result = string.Empty;
			}
			return result;
		}

		// Token: 0x0600003E RID: 62 RVA: 0x0000380C File Offset: 0x00001A0C
		public static string ProtectDataBase64String(string plain_text, ProtectionScope dpScope)
		{
			byte[] inArray = DPAPIEncryptionV2.ProtectData(DPAPIEncryptionV2.StringToByteArray(plain_text), dpScope);
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00003834 File Offset: 0x00001A34
		public static string UnProtectDataBase64String(string encText, ProtectionScope dpScope)
		{
			byte[] encData = Convert.FromBase64String(encText);
			return DPAPIEncryptionV2.ByteArrayToString(DPAPIEncryptionV2.UnProtectData(encData, dpScope));
		}

		// Token: 0x06000040 RID: 64 RVA: 0x0000385C File Offset: 0x00001A5C
		public static byte[] StringToByteArray(string plain_text)
		{
			bool flag = string.IsNullOrEmpty(plain_text);
			byte[] result;
			if (flag)
			{
				result = new byte[0];
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				result = utf8Encoding.GetBytes(plain_text);
			}
			return result;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00003890 File Offset: 0x00001A90
		public static string ByteArrayToString(byte[] encData)
		{
			bool flag = encData == null;
			string result;
			if (flag)
			{
				result = "";
			}
			else
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding();
				result = utf8Encoding.GetString(encData);
			}
			return result;
		}

		// Token: 0x04000015 RID: 21
		private const string ENTROPY_KEY = "EncryptionClassLibrary.Resources.clock.ico";

		// Token: 0x04000016 RID: 22
		private static byte[] aditional_entropy;
	}
}
