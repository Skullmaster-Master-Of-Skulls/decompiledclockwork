using System;
using System.Security.Cryptography;
using System.Text;

namespace EncryptionClassLibrary
{
	// Token: 0x0200000B RID: 11
	public static class EncryptionFactory
	{
		// Token: 0x06000049 RID: 73 RVA: 0x00003950 File Offset: 0x00001B50
		public static IEncryption GetEncryption(EncryptionType encryptionType, string password)
		{
			IEncryption result;
			switch (encryptionType)
			{
			case EncryptionType.TripleDES_128bit:
			{
				byte[][] bytesLegacy = EncryptionFactory.GetBytesLegacy(password, 16, 8);
				result = new TripleDES(bytesLegacy[0], bytesLegacy[1]);
				break;
			}
			case EncryptionType.TripleDES_192bit:
			{
				byte[][] bytesLegacy2 = EncryptionFactory.GetBytesLegacy(password, 24, 8);
				result = new TripleDES(bytesLegacy2[0], bytesLegacy2[1]);
				break;
			}
			case EncryptionType.TripleDES_192bit_RandomIv:
			{
				byte[][] bytesLegacy3 = EncryptionFactory.GetBytesLegacy(password, 24, 8);
				result = new TripleDESRandomIv(bytesLegacy3[0]);
				break;
			}
			case EncryptionType.AES_256bit:
			{
				byte[] bytes = EncryptionFactory.GetBytes(password, 32);
				byte[] bytes2 = EncryptionFactory.GetBytes(password, 16);
				result = new AESEncryption(bytes, bytes2);
				break;
			}
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x0600004A RID: 74 RVA: 0x000039F4 File Offset: 0x00001BF4
		public static IEncryption GetEncryption(EncryptionType encryptionType)
		{
			IEncryption result;
			switch (encryptionType)
			{
			case EncryptionType.TripleDES_128bit:
				result = new TripleDES();
				break;
			case EncryptionType.TripleDES_192bit:
				result = new TripleDES();
				break;
			case EncryptionType.TripleDES_192bit_RandomIv:
				result = new TripleDESRandomIv();
				break;
			case EncryptionType.AES_256bit:
				result = new AESEncryption();
				break;
			default:
				result = null;
				break;
			}
			return result;
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003A44 File Offset: 0x00001C44
		public static byte[] GetBytes(string password, int nBytes)
		{
			byte[] bytes = Encoding.ASCII.GetBytes("Cl0ckW0rk Ent3rpr1$3");
			Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, bytes);
			return rfc2898DeriveBytes.GetBytes(nBytes);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00003A78 File Offset: 0x00001C78
		public static byte[][] GetBytesLegacy(string password, int nKeyBytes, int nBlockBytes)
		{
			byte[] rgbSalt = new byte[]
			{
				0,
				1,
				2,
				3,
				4,
				5,
				6,
				7
			};
			PasswordDeriveBytes passwordDeriveBytes = new PasswordDeriveBytes(password, rgbSalt)
			{
				IterationCount = 1000,
				HashName = "SHA1"
			};
			byte[] bytes = passwordDeriveBytes.GetBytes(nKeyBytes);
			byte[] bytes2 = passwordDeriveBytes.GetBytes(nBlockBytes);
			return new byte[][]
			{
				bytes,
				bytes2
			};
		}
	}
}
