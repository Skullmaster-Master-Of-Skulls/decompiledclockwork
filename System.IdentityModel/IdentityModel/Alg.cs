using System;

namespace System.IdentityModel
{
	// Token: 0x0200009E RID: 158
	internal enum Alg
	{
		// Token: 0x0400047E RID: 1150
		Any,
		// Token: 0x0400047F RID: 1151
		ClassSignture = 8192,
		// Token: 0x04000480 RID: 1152
		ClassEncrypt = 24576,
		// Token: 0x04000481 RID: 1153
		ClassHash = 32768,
		// Token: 0x04000482 RID: 1154
		ClassKeyXch = 40960,
		// Token: 0x04000483 RID: 1155
		TypeRSA = 1024,
		// Token: 0x04000484 RID: 1156
		TypeBlock = 1536,
		// Token: 0x04000485 RID: 1157
		TypeStream = 2048,
		// Token: 0x04000486 RID: 1158
		TypeDH = 2560,
		// Token: 0x04000487 RID: 1159
		NameDES = 1,
		// Token: 0x04000488 RID: 1160
		NameRC2,
		// Token: 0x04000489 RID: 1161
		NameRC4 = 1,
		// Token: 0x0400048A RID: 1162
		NameSkipJack = 10,
		// Token: 0x0400048B RID: 1163
		NameSHA = 4,
		// Token: 0x0400048C RID: 1164
		NameDH_Ephem = 2,
		// Token: 0x0400048D RID: 1165
		Fortezza = 4
	}
}
