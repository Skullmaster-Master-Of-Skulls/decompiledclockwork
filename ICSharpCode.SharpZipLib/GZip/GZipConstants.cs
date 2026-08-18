using System;

namespace ICSharpCode.SharpZipLib.GZip
{
	// Token: 0x02000032 RID: 50
	public sealed class GZipConstants
	{
		// Token: 0x060001C3 RID: 451 RVA: 0x00009EE4 File Offset: 0x00008EE4
		private GZipConstants()
		{
		}

		// Token: 0x04000117 RID: 279
		public const int GZIP_MAGIC = 8075;

		// Token: 0x04000118 RID: 280
		public const int FTEXT = 1;

		// Token: 0x04000119 RID: 281
		public const int FHCRC = 2;

		// Token: 0x0400011A RID: 282
		public const int FEXTRA = 4;

		// Token: 0x0400011B RID: 283
		public const int FNAME = 8;

		// Token: 0x0400011C RID: 284
		public const int FCOMMENT = 16;
	}
}
