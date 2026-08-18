using System;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000015 RID: 21
	[Flags]
	public enum GeneralBitFlags
	{
		// Token: 0x0400008C RID: 140
		Encrypted = 1,
		// Token: 0x0400008D RID: 141
		Method = 6,
		// Token: 0x0400008E RID: 142
		Descriptor = 8,
		// Token: 0x0400008F RID: 143
		ReservedPKware4 = 16,
		// Token: 0x04000090 RID: 144
		Patched = 32,
		// Token: 0x04000091 RID: 145
		StrongEncryption = 64,
		// Token: 0x04000092 RID: 146
		Unused7 = 128,
		// Token: 0x04000093 RID: 147
		Unused8 = 256,
		// Token: 0x04000094 RID: 148
		Unused9 = 512,
		// Token: 0x04000095 RID: 149
		Unused10 = 1024,
		// Token: 0x04000096 RID: 150
		UnicodeText = 2048,
		// Token: 0x04000097 RID: 151
		EnhancedCompress = 4096,
		// Token: 0x04000098 RID: 152
		HeaderMasked = 8192,
		// Token: 0x04000099 RID: 153
		ReservedPkware14 = 16384,
		// Token: 0x0400009A RID: 154
		ReservedPkware15 = 32768
	}
}
