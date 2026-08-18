using System;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using System.Web.Helpers.Resources;

namespace System.Web.Helpers
{
	// Token: 0x0200000B RID: 11
	public static class Crypto
	{
		// Token: 0x06000065 RID: 101 RVA: 0x000034CC File Offset: 0x000016CC
		internal static byte[] GenerateSaltInternal(int byteLength = 16)
		{
			byte[] array = new byte[byteLength];
			using (RNGCryptoServiceProvider rngcryptoServiceProvider = new RNGCryptoServiceProvider())
			{
				rngcryptoServiceProvider.GetBytes(array);
			}
			return array;
		}

		// Token: 0x06000066 RID: 102 RVA: 0x0000350C File Offset: 0x0000170C
		public static string GenerateSalt(int byteLength = 16)
		{
			return Convert.ToBase64String(Crypto.GenerateSaltInternal(byteLength));
		}

		// Token: 0x06000067 RID: 103 RVA: 0x00003519 File Offset: 0x00001719
		public static string Hash(string input, string algorithm = "sha256")
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return Crypto.Hash(Encoding.UTF8.GetBytes(input), algorithm);
		}

		// Token: 0x06000068 RID: 104 RVA: 0x0000353C File Offset: 0x0000173C
		public static string Hash(byte[] input, string algorithm = "sha256")
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			string result;
			using (HashAlgorithm hashAlgorithm = HashAlgorithm.Create(algorithm))
			{
				if (hashAlgorithm == null)
				{
					throw new InvalidOperationException(string.Format(CultureInfo.InvariantCulture, HelpersResources.Crypto_NotSupportedHashAlg, new object[]
					{
						algorithm
					}));
				}
				byte[] data = hashAlgorithm.ComputeHash(input);
				result = Crypto.BinaryToHex(data);
			}
			return result;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000035B0 File Offset: 0x000017B0
		public static string SHA1(string input)
		{
			return Crypto.Hash(input, "sha1");
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000035BD File Offset: 0x000017BD
		public static string SHA256(string input)
		{
			return Crypto.Hash(input, "sha256");
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000035CC File Offset: 0x000017CC
		public static string HashPassword(string password)
		{
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			byte[] salt;
			byte[] bytes;
			using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, 16, 1000))
			{
				salt = rfc2898DeriveBytes.Salt;
				bytes = rfc2898DeriveBytes.GetBytes(32);
			}
			byte[] array = new byte[49];
			Buffer.BlockCopy(salt, 0, array, 1, 16);
			Buffer.BlockCopy(bytes, 0, array, 17, 32);
			return Convert.ToBase64String(array);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00003648 File Offset: 0x00001848
		public static bool VerifyHashedPassword(string hashedPassword, string password)
		{
			if (hashedPassword == null)
			{
				throw new ArgumentNullException("hashedPassword");
			}
			if (password == null)
			{
				throw new ArgumentNullException("password");
			}
			byte[] array = Convert.FromBase64String(hashedPassword);
			if (array.Length != 49 || array[0] != 0)
			{
				return false;
			}
			byte[] array2 = new byte[16];
			Buffer.BlockCopy(array, 1, array2, 0, 16);
			byte[] array3 = new byte[32];
			Buffer.BlockCopy(array, 17, array3, 0, 32);
			byte[] bytes;
			using (Rfc2898DeriveBytes rfc2898DeriveBytes = new Rfc2898DeriveBytes(password, array2, 1000))
			{
				bytes = rfc2898DeriveBytes.GetBytes(32);
			}
			return Crypto.ByteArraysEqual(array3, bytes);
		}

		// Token: 0x0600006D RID: 109 RVA: 0x000036EC File Offset: 0x000018EC
		internal static string BinaryToHex(byte[] data)
		{
			char[] array = new char[data.Length * 2];
			for (int i = 0; i < data.Length; i++)
			{
				byte b = (byte)(data[i] >> 4);
				array[i * 2] = (char)((b > 9) ? (b + 55) : (b + 48));
				b = (data[i] & 15);
				array[i * 2 + 1] = (char)((b > 9) ? (b + 55) : (b + 48));
			}
			return new string(array);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003754 File Offset: 0x00001954
		[MethodImpl(MethodImplOptions.NoOptimization)]
		private static bool ByteArraysEqual(byte[] a, byte[] b)
		{
			if (object.ReferenceEquals(a, b))
			{
				return true;
			}
			if (a == null || b == null || a.Length != b.Length)
			{
				return false;
			}
			bool flag = true;
			for (int i = 0; i < a.Length; i++)
			{
				flag &= (a[i] == b[i]);
			}
			return flag;
		}

		// Token: 0x0400002B RID: 43
		private const int PBKDF2IterCount = 1000;

		// Token: 0x0400002C RID: 44
		private const int PBKDF2SubkeyLength = 32;

		// Token: 0x0400002D RID: 45
		private const int SaltSize = 16;
	}
}
