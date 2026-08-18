using System;
using System.Security.Cryptography;
using System.Text;

namespace TechnoPro.Common.Security.Hashing
{
	// Token: 0x02000003 RID: 3
	public class HashingProviderHMACSHA1 : IHashingProvider
	{
		// Token: 0x06000003 RID: 3 RVA: 0x00002060 File Offset: 0x00000260
		public string CreateHash(string password, PasswordHashContext context = null)
		{
			HashAlgorithm hashAlgorithm = new HMACSHA1(Encoding.UTF8.GetBytes(context.SecretKey));
			byte[] bytes = Encoding.UTF8.GetBytes(password);
			return Convert.ToBase64String(hashAlgorithm.ComputeHash(bytes));
		}

		// Token: 0x06000004 RID: 4 RVA: 0x0000209C File Offset: 0x0000029C
		public bool ValidatePassword(string password, string correctHash, PasswordHashContext context = null)
		{
			byte[] a = Convert.FromBase64String(correctHash);
			byte[] b = Convert.FromBase64String(this.CreateHash(password, context));
			return PasswordHashFactory.SlowEquals(a, b);
		}
	}
}
