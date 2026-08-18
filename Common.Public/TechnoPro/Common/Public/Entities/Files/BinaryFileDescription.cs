using System;

namespace TechnoPro.Common.Public.Entities.Files
{
	// Token: 0x02000334 RID: 820
	public class BinaryFileDescription
	{
		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x060019AB RID: 6571 RVA: 0x0001E0D3 File Offset: 0x0001C2D3
		// (set) Token: 0x060019AC RID: 6572 RVA: 0x0001E0DB File Offset: 0x0001C2DB
		public string FileName { get; set; }

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x060019AD RID: 6573 RVA: 0x0001E0E4 File Offset: 0x0001C2E4
		// (set) Token: 0x060019AE RID: 6574 RVA: 0x0001E0EC File Offset: 0x0001C2EC
		public int FileSize { get; set; }
	}
}
