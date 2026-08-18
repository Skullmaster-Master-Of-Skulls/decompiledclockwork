using System;

namespace MailBee.Security
{
	// Token: 0x02000103 RID: 259
	public enum DomainKeysVerifyResult
	{
		// Token: 0x040006E3 RID: 1763
		OK,
		// Token: 0x040006E4 RID: 1764
		MessageNotSigned,
		// Token: 0x040006E5 RID: 1765
		SignatureInvalidTag,
		// Token: 0x040006E6 RID: 1766
		SignatureExpired,
		// Token: 0x040006E7 RID: 1767
		DomainInvalid,
		// Token: 0x040006E8 RID: 1768
		DnsQueryFailed,
		// Token: 0x040006E9 RID: 1769
		DnsEntryInvalidTag,
		// Token: 0x040006EA RID: 1770
		SignatureInvalid,
		// Token: 0x040006EB RID: 1771
		PublicKeyBadFormat,
		// Token: 0x040006EC RID: 1772
		Sha256NotSupported
	}
}
