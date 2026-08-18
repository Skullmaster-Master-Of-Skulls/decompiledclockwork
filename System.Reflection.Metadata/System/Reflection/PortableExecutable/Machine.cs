using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x0200001E RID: 30
	public enum Machine : ushort
	{
		// Token: 0x040000B6 RID: 182
		Unknown,
		// Token: 0x040000B7 RID: 183
		I386 = 332,
		// Token: 0x040000B8 RID: 184
		WceMipsV2 = 361,
		// Token: 0x040000B9 RID: 185
		Alpha = 388,
		// Token: 0x040000BA RID: 186
		SH3 = 418,
		// Token: 0x040000BB RID: 187
		SH3Dsp,
		// Token: 0x040000BC RID: 188
		SH3E,
		// Token: 0x040000BD RID: 189
		SH4 = 422,
		// Token: 0x040000BE RID: 190
		SH5 = 424,
		// Token: 0x040000BF RID: 191
		Arm = 448,
		// Token: 0x040000C0 RID: 192
		Thumb = 450,
		// Token: 0x040000C1 RID: 193
		ArmThumb2 = 452,
		// Token: 0x040000C2 RID: 194
		AM33 = 467,
		// Token: 0x040000C3 RID: 195
		PowerPC = 496,
		// Token: 0x040000C4 RID: 196
		PowerPCFP,
		// Token: 0x040000C5 RID: 197
		IA64 = 512,
		// Token: 0x040000C6 RID: 198
		MIPS16 = 614,
		// Token: 0x040000C7 RID: 199
		Alpha64 = 644,
		// Token: 0x040000C8 RID: 200
		MipsFpu = 870,
		// Token: 0x040000C9 RID: 201
		MipsFpu16 = 1126,
		// Token: 0x040000CA RID: 202
		Tricore = 1312,
		// Token: 0x040000CB RID: 203
		Ebc = 3772,
		// Token: 0x040000CC RID: 204
		Amd64 = 34404,
		// Token: 0x040000CD RID: 205
		M32R = 36929
	}
}
