using System;

namespace TechnoPro.Common.Public.Entities.Common
{
	// Token: 0x02000441 RID: 1089
	public class File
	{
		// Token: 0x17000DA0 RID: 3488
		// (get) Token: 0x06002107 RID: 8455 RVA: 0x0002534F File Offset: 0x0002354F
		// (set) Token: 0x06002108 RID: 8456 RVA: 0x00025357 File Offset: 0x00023557
		public string Filename { get; set; }

		// Token: 0x17000DA1 RID: 3489
		// (get) Token: 0x06002109 RID: 8457 RVA: 0x00025360 File Offset: 0x00023560
		// (set) Token: 0x0600210A RID: 8458 RVA: 0x00025368 File Offset: 0x00023568
		public byte[] FileBytes { get; set; }
	}
}
