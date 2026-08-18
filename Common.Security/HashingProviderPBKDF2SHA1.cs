using System;
using System.Security.Cryptography;

namespace TechnoPro.Common.Security.Hashing
{
	// Token: 0x02000005 RID: 5
	public class HashingProviderPBKDF2SHA1 : IHashingProvider
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002180 File Offset: 0x00000380
		public string CreateHash(string password, PasswordHashContext context = null)
		{
			RandomNumberGenerator randomNumberGenerator = new RNGCryptoServiceProvider();
			byte[] array = new byte[24];
			randomNumberGenerator.GetBytes(array);
			byte[] inArray = HashingProviderPBKDF2SHA1.PBKDF2(password, array, 1000, 24);
			return string.Concat(new string[]
			{
				1000.ToString(),
				":",
				Convert.ToBase64String(array),
				":",
				Convert.ToBase64String(inArray)
			});
		}

		// Token: 0x0600000C RID: 12 RVA: 0x000021EC File Offset: 0x000003EC
		public bool ValidatePassword(string password, string correctHash, PasswordHashContext context = null)
		{
			char[] separator = new char[]
			{
				':'
			};
			string[] array = correctHash.Split(separator);
			int iterations = int.Parse(array[0]);
			byte[] salt = Convert.FromBase64String(array[1]);
			byte[] array2 = Convert.FromBase64String(array[2]);
			byte[] b = HashingProviderPBKDF2SHA1.PBKDF2(password, salt, iterations, array2.Length);
			return PasswordHashFactory.SlowEquals(array2, b);
		}

		// Token: 0x0600000D RID: 13 RVA: 0x0000223B File Offset: 0x0000043B
		private static byte[] PBKDF2(string password, byte[] salt, int iterations, int outputBytes)
		{
			return new Rfc2898DeriveBytes(password, salt)
			{
				IterationCount = iterations
			}.GetBytes(outputBytes);
		}

		// Token: 0x04000001 RID: 1
		public const int SALT_BYTE_SIZE = 24;

		// Token: 0x04000002 RID: 2
		public const int HASH_BYTE_SIZE = 24;

		// Token: 0x04000003 RID: 3
		public const int PBKDF2_ITERATIONS = 1000;

		// Token: 0x04000004 RID: 4
		public const int ITERATION_INDEX = 0;

		// Token: 0x04000005 RID: 5
		public const int SALT_INDEX = 1;

		// Token: 0x04000006 RID: 6
		public const int PBKDF2_INDEX = 2;
	}
}
