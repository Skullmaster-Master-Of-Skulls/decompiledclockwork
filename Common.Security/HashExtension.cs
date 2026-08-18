using System;

namespace TechnoPro.Common.Security.Hashing
{
	// Token: 0x02000002 RID: 2
	public static class HashExtension
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public static string ToMd5Hash(this byte[] bytes)
		{
			return HashingProviderMD5.ToMd5Hash(bytes);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002058 File Offset: 0x00000258
		public static string ToMd5Hash(this string inputString)
		{
			return HashingProviderMD5.ToMd5Hash(inputString);
		}
	}
}
