using System;

namespace System.Net
{
	// Token: 0x02000545 RID: 1349
	[Flags]
	internal enum Alg
	{
		// Token: 0x0400280A RID: 10250
		Any = 0,
		// Token: 0x0400280B RID: 10251
		ClassSignture = 8192,
		// Token: 0x0400280C RID: 10252
		ClassEncrypt = 24576,
		// Token: 0x0400280D RID: 10253
		ClassHash = 32768,
		// Token: 0x0400280E RID: 10254
		ClassKeyXch = 40960,
		// Token: 0x0400280F RID: 10255
		TypeRSA = 1024,
		// Token: 0x04002810 RID: 10256
		TypeBlock = 1536,
		// Token: 0x04002811 RID: 10257
		TypeStream = 2048,
		// Token: 0x04002812 RID: 10258
		TypeDH = 2560,
		// Token: 0x04002813 RID: 10259
		NameDES = 1,
		// Token: 0x04002814 RID: 10260
		NameRC2 = 2,
		// Token: 0x04002815 RID: 10261
		Name3DES = 3,
		// Token: 0x04002816 RID: 10262
		NameAES_128 = 14,
		// Token: 0x04002817 RID: 10263
		NameAES_192 = 15,
		// Token: 0x04002818 RID: 10264
		NameAES_256 = 16,
		// Token: 0x04002819 RID: 10265
		NameAES = 17,
		// Token: 0x0400281A RID: 10266
		NameRC4 = 1,
		// Token: 0x0400281B RID: 10267
		NameMD5 = 3,
		// Token: 0x0400281C RID: 10268
		NameSHA = 4,
		// Token: 0x0400281D RID: 10269
		NameDH_Ephem = 2
	}
}
