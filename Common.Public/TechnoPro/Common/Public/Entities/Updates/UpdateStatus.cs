using System;

namespace TechnoPro.Common.Public.Entities.Updates
{
	// Token: 0x02000153 RID: 339
	public class UpdateStatus
	{
		// Token: 0x170002ED RID: 749
		// (get) Token: 0x06000812 RID: 2066 RVA: 0x00011629 File Offset: 0x0000F829
		// (set) Token: 0x06000813 RID: 2067 RVA: 0x00011631 File Offset: 0x0000F831
		public int ID { get; set; }

		// Token: 0x170002EE RID: 750
		// (get) Token: 0x06000814 RID: 2068 RVA: 0x0001163A File Offset: 0x0000F83A
		// (set) Token: 0x06000815 RID: 2069 RVA: 0x00011642 File Offset: 0x0000F842
		public string FileType { get; set; }

		// Token: 0x170002EF RID: 751
		// (get) Token: 0x06000816 RID: 2070 RVA: 0x0001164B File Offset: 0x0000F84B
		// (set) Token: 0x06000817 RID: 2071 RVA: 0x00011653 File Offset: 0x0000F853
		public int AddressSize { get; set; }

		// Token: 0x170002F0 RID: 752
		// (get) Token: 0x06000818 RID: 2072 RVA: 0x0001165C File Offset: 0x0000F85C
		// (set) Token: 0x06000819 RID: 2073 RVA: 0x00011664 File Offset: 0x0000F864
		public bool IsPublic { get; set; }

		// Token: 0x170002F1 RID: 753
		// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001166D File Offset: 0x0000F86D
		// (set) Token: 0x0600081B RID: 2075 RVA: 0x00011675 File Offset: 0x0000F875
		public string Status { get; set; }

		// Token: 0x170002F2 RID: 754
		// (get) Token: 0x0600081C RID: 2076 RVA: 0x0001167E File Offset: 0x0000F87E
		// (set) Token: 0x0600081D RID: 2077 RVA: 0x00011686 File Offset: 0x0000F886
		public string Filename { get; set; }
	}
}
