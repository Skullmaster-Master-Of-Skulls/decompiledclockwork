using System;

namespace TechnoPro.Common.Public.Entities.Files
{
	// Token: 0x02000337 RID: 823
	public class BinaryFile : BusinessBase<string>
	{
		// Token: 0x17000AB1 RID: 2737
		// (get) Token: 0x060019C0 RID: 6592 RVA: 0x0001E1AE File Offset: 0x0001C3AE
		// (set) Token: 0x060019C1 RID: 6593 RVA: 0x0001E1B6 File Offset: 0x0001C3B6
		public string FileName { get; set; }

		// Token: 0x17000AB2 RID: 2738
		// (get) Token: 0x060019C2 RID: 6594 RVA: 0x0001E1BF File Offset: 0x0001C3BF
		// (set) Token: 0x060019C3 RID: 6595 RVA: 0x0001E1C7 File Offset: 0x0001C3C7
		public int FileSize { get; set; }

		// Token: 0x17000AB3 RID: 2739
		// (get) Token: 0x060019C4 RID: 6596 RVA: 0x0001E1D0 File Offset: 0x0001C3D0
		// (set) Token: 0x060019C5 RID: 6597 RVA: 0x0001E1D8 File Offset: 0x0001C3D8
		public byte[] ByteArray { get; set; }
	}
}
