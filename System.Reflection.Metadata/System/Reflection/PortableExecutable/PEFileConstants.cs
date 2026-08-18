using System;

namespace System.Reflection.PortableExecutable
{
	// Token: 0x02000020 RID: 32
	internal static class PEFileConstants
	{
		// Token: 0x040000D1 RID: 209
		internal const ushort DosSignature = 23117;

		// Token: 0x040000D2 RID: 210
		internal const int PESignatureOffsetLocation = 60;

		// Token: 0x040000D3 RID: 211
		internal const uint PESignature = 17744U;

		// Token: 0x040000D4 RID: 212
		internal const int BasicPEHeaderSize = 60;

		// Token: 0x040000D5 RID: 213
		internal const int SizeofCOFFFileHeader = 20;

		// Token: 0x040000D6 RID: 214
		internal const int SizeofOptionalHeaderStandardFields32 = 28;

		// Token: 0x040000D7 RID: 215
		internal const int SizeofOptionalHeaderStandardFields64 = 24;

		// Token: 0x040000D8 RID: 216
		internal const int SizeofOptionalHeaderNTAdditionalFields32 = 68;

		// Token: 0x040000D9 RID: 217
		internal const int SizeofOptionalHeaderNTAdditionalFields64 = 88;

		// Token: 0x040000DA RID: 218
		internal const int NumberofOptionalHeaderDirectoryEntries = 16;

		// Token: 0x040000DB RID: 219
		internal const int SizeofOptionalHeaderDirectoriesEntries = 128;

		// Token: 0x040000DC RID: 220
		internal const int SizeofSectionHeader = 40;

		// Token: 0x040000DD RID: 221
		internal const int SizeofSectionName = 8;

		// Token: 0x040000DE RID: 222
		internal const int SizeofResourceDirectory = 16;

		// Token: 0x040000DF RID: 223
		internal const int SizeofResourceDirectoryEntry = 8;
	}
}
