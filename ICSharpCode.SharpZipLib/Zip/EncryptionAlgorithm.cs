using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000014 RID: 20
	public enum EncryptionAlgorithm
	{
		// Token: 0x0400007D RID: 125
		None,
		// Token: 0x0400007E RID: 126
		PkzipClassic,
		// Token: 0x0400007F RID: 127
		Des = 26113,
		// Token: 0x04000080 RID: 128
		RC2,
		// Token: 0x04000081 RID: 129
		TripleDes168,
		// Token: 0x04000082 RID: 130
		TripleDes112 = 26121,
		// Token: 0x04000083 RID: 131
		Aes128 = 26126,
		// Token: 0x04000084 RID: 132
		Aes192,
		// Token: 0x04000085 RID: 133
		Aes256,
		// Token: 0x04000086 RID: 134
		RC2Corrected = 26370,
		// Token: 0x04000087 RID: 135
		Blowfish = 26400,
		// Token: 0x04000088 RID: 136
		Twofish,
		// Token: 0x04000089 RID: 137
		RC4 = 26625,
		// Token: 0x0400008A RID: 138
		Unknown = 65535
	}
}
