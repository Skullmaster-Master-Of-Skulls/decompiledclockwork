using System;
using System.Security.Cryptography;
using System.Text;

namespace Telerik.Licensing
{
	// Token: 0x02000425 RID: 1061
	internal class HashService : IHashingService
	{
		// Token: 0x0600261A RID: 9754 RVA: 0x0007D20A File Offset: 0x0007B40A
		private HashService()
		{
		}

		// Token: 0x0600261B RID: 9755 RVA: 0x0007D214 File Offset: 0x0007B414
		public static IHashingService GetInstance()
		{
			if (HashService.service == null)
			{
				lock (HashService.serviceLock)
				{
					if (HashService.service == null)
					{
						HashService.service = new HashService();
					}
				}
			}
			return HashService.service;
		}

		// Token: 0x0600261C RID: 9756 RVA: 0x0007D26C File Offset: 0x0007B46C
		public string Sha256(string input)
		{
			SHA1CryptoServiceProvider sha1CryptoServiceProvider = new SHA1CryptoServiceProvider();
			byte[] bytes = Encoding.UTF8.GetBytes(input);
			byte[] inArray = sha1CryptoServiceProvider.ComputeHash(bytes);
			return Convert.ToBase64String(inArray);
		}

		// Token: 0x040009B4 RID: 2484
		private static readonly object serviceLock = new object();

		// Token: 0x040009B5 RID: 2485
		private static IHashingService service;
	}
}
