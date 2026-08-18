using System;

namespace Org.BouncyCastle.Bcpg
{
	// Token: 0x0200055A RID: 1370
	public enum PacketTag
	{
		// Token: 0x0400206C RID: 8300
		Reserved,
		// Token: 0x0400206D RID: 8301
		PublicKeyEncryptedSession,
		// Token: 0x0400206E RID: 8302
		Signature,
		// Token: 0x0400206F RID: 8303
		SymmetricKeyEncryptedSessionKey,
		// Token: 0x04002070 RID: 8304
		OnePassSignature,
		// Token: 0x04002071 RID: 8305
		SecretKey,
		// Token: 0x04002072 RID: 8306
		PublicKey,
		// Token: 0x04002073 RID: 8307
		SecretSubkey,
		// Token: 0x04002074 RID: 8308
		CompressedData,
		// Token: 0x04002075 RID: 8309
		SymmetricKeyEncrypted,
		// Token: 0x04002076 RID: 8310
		Marker,
		// Token: 0x04002077 RID: 8311
		LiteralData,
		// Token: 0x04002078 RID: 8312
		Trust,
		// Token: 0x04002079 RID: 8313
		UserId,
		// Token: 0x0400207A RID: 8314
		PublicSubkey,
		// Token: 0x0400207B RID: 8315
		UserAttribute = 17,
		// Token: 0x0400207C RID: 8316
		SymmetricEncryptedIntegrityProtected,
		// Token: 0x0400207D RID: 8317
		ModificationDetectionCode,
		// Token: 0x0400207E RID: 8318
		Experimental1 = 60,
		// Token: 0x0400207F RID: 8319
		Experimental2,
		// Token: 0x04002080 RID: 8320
		Experimental3,
		// Token: 0x04002081 RID: 8321
		Experimental4
	}
}
