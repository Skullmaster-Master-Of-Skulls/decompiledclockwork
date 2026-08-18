using System;

namespace TechnoPro.Common.Public.Entities.FileStorage
{
	// Token: 0x02000340 RID: 832
	[Serializable]
	public class FileIdentifier
	{
		// Token: 0x17000AB8 RID: 2744
		// (get) Token: 0x060019D5 RID: 6613 RVA: 0x0001E2A1 File Offset: 0x0001C4A1
		// (set) Token: 0x060019D6 RID: 6614 RVA: 0x0001E2A9 File Offset: 0x0001C4A9
		public Guid? FileUniqueId { get; set; }

		// Token: 0x17000AB9 RID: 2745
		// (get) Token: 0x060019D7 RID: 6615 RVA: 0x0001E2B2 File Offset: 0x0001C4B2
		// (set) Token: 0x060019D8 RID: 6616 RVA: 0x0001E2BA File Offset: 0x0001C4BA
		public eFileSource Source { get; set; }

		// Token: 0x17000ABA RID: 2746
		// (get) Token: 0x060019D9 RID: 6617 RVA: 0x0001E2C3 File Offset: 0x0001C4C3
		// (set) Token: 0x060019DA RID: 6618 RVA: 0x0001E2CB File Offset: 0x0001C4CB
		public int LegacyId { get; set; }
	}
}
