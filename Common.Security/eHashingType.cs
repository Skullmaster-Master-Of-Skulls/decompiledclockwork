using System;

namespace TechnoPro.Common.Security.Hashing
{
	// Token: 0x0200000A RID: 10
	[Serializable]
	public enum eHashingType
	{
		// Token: 0x04000009 RID: 9
		[HashingType("HashingProviderPBKDF2SHA1")]
		ClockWorkDefault,
		// Token: 0x0400000A RID: 10
		[HashingType("HashingProviderPBKDF2SHA1")]
		PBKDF2_SHA1,
		// Token: 0x0400000B RID: 11
		[HashingType("HashingProviderHMACSHA1")]
		HMAC_SHA1,
		// Token: 0x0400000C RID: 12
		[HashingType("HashingProviderMD5")]
		MD5,
		// Token: 0x0400000D RID: 13
		[HashingType("HashingProviderSha256")]
		SHA256
	}
}
