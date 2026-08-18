using System;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;

namespace Microsoft.AspNet.Identity
{
	// Token: 0x02000034 RID: 52
	internal static class Crypto
	{
		// Token: 0x060000E2 RID: 226 RVA: 0x00006568 File Offset: 0x00004768
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

		// Token: 0x060000E3 RID: 227 RVA: 0x000065E4 File Offset: 0x000047E4
		public static bool VerifyHashedPassword(string hashedPassword, string password)
		{
			if (hashedPassword == null)
			{
				return false;
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

		// Token: 0x060000E4 RID: 228 RVA: 0x00006680 File Offset: 0x00004880
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

		// Token: 0x04000027 RID: 39
		private const int PBKDF2IterCount = 1000;

		// Token: 0x04000028 RID: 40
		private const int PBKDF2SubkeyLength = 32;

		// Token: 0x04000029 RID: 41
		private const int SaltSize = 16;
	}
}
