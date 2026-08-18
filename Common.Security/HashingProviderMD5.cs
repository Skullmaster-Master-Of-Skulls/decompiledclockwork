using System;
using System.Security.Cryptography;
using System.Text;

namespace TechnoPro.Common.Security.Hashing
{
	// Token: 0x02000004 RID: 4
	public class HashingProviderMD5 : IHashingProvider
	{
		// Token: 0x06000006 RID: 6 RVA: 0x000020CB File Offset: 0x000002CB
		public string CreateHash(string password, PasswordHashContext context = null)
		{
			return HashingProviderMD5.ToMd5Hash(password);
		}

		// Token: 0x06000007 RID: 7 RVA: 0x000020D4 File Offset: 0x000002D4
		public bool ValidatePassword(string password, string correctHash, PasswordHashContext context = null)
		{
			byte[] a = Convert.FromBase64String(correctHash);
			byte[] b = Convert.FromBase64String(this.CreateHash(password, context));
			return PasswordHashFactory.SlowEquals(a, b);
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020FB File Offset: 0x000002FB
		public static string ToMd5Hash(string input)
		{
			return HashingProviderMD5.ToMd5Hash(Encoding.UTF8.GetBytes(input));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00002110 File Offset: 0x00000310
		public static string ToMd5Hash(byte[] bytes)
		{
			string result;
			using (MD5 md = MD5.Create())
			{
				byte[] array = md.ComputeHash(bytes);
				StringBuilder stringBuilder = new StringBuilder();
				foreach (byte b in array)
				{
					stringBuilder.AppendFormat("{0:x2}", b);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}
	}
}
