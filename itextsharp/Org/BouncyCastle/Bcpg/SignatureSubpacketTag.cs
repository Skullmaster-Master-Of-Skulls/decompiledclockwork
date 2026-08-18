using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x02000253 RID: 595
	public enum SignatureSubpacketTag
	{
		// Token: 0x04000F83 RID: 3971
		CreationTime = 2,
		// Token: 0x04000F84 RID: 3972
		ExpireTime,
		// Token: 0x04000F85 RID: 3973
		Exportable,
		// Token: 0x04000F86 RID: 3974
		TrustSig,
		// Token: 0x04000F87 RID: 3975
		RegExp,
		// Token: 0x04000F88 RID: 3976
		Revocable,
		// Token: 0x04000F89 RID: 3977
		KeyExpireTime = 9,
		// Token: 0x04000F8A RID: 3978
		Placeholder,
		// Token: 0x04000F8B RID: 3979
		PreferredSymmetricAlgorithms,
		// Token: 0x04000F8C RID: 3980
		RevocationKey,
		// Token: 0x04000F8D RID: 3981
		IssuerKeyId = 16,
		// Token: 0x04000F8E RID: 3982
		NotationData = 20,
		// Token: 0x04000F8F RID: 3983
		PreferredHashAlgorithms,
		// Token: 0x04000F90 RID: 3984
		PreferredCompressionAlgorithms,
		// Token: 0x04000F91 RID: 3985
		KeyServerPreferences,
		// Token: 0x04000F92 RID: 3986
		PreferredKeyServer,
		// Token: 0x04000F93 RID: 3987
		PrimaryUserId,
		// Token: 0x04000F94 RID: 3988
		PolicyUrl,
		// Token: 0x04000F95 RID: 3989
		KeyFlags,
		// Token: 0x04000F96 RID: 3990
		SignerUserId,
		// Token: 0x04000F97 RID: 3991
		RevocationReason,
		// Token: 0x04000F98 RID: 3992
		Features,
		// Token: 0x04000F99 RID: 3993
		SignatureTarget,
		// Token: 0x04000F9A RID: 3994
		EmbeddedSignature
	}
}
