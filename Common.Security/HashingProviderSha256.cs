using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace TechnoPro.Common.Security.Hashing
{
	// Token: 0x02000006 RID: 6
	public class HashingProviderSha256 : IHashingProvider
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002254 File Offset: 0x00000454
		public string CreateHash(string password, PasswordHashContext context = null)
		{
			string result;
			using (SHA256 sha = new SHA256Managed())
			{
				string empty = string.Empty;
				result = sha.ComputeHash(Encoding.UTF8.GetBytes(password), 0, Encoding.UTF8.GetByteCount(password)).Aggregate(empty, (string current, byte bit) => current + bit.ToString("x2"));
			}
			return result;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000022D0 File Offset: 0x000004D0
		public bool ValidatePassword(string password, string correctHash, PasswordHashContext context = null)
		{
			byte[] a = Convert.FromBase64String(correctHash);
			byte[] b = Convert.FromBase64String(this.CreateHash(password, context));
			return PasswordHashFactory.SlowEquals(a, b);
		}
	}
}
