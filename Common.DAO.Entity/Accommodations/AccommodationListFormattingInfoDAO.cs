using System;

namespace TechnoPro.Common.DAO.Entity.Accommodations
{
	// Token: 0x02000004 RID: 4
	public class AccommodationListFormattingInfoDAO
	{
		// Token: 0x06000006 RID: 6 RVA: 0x0000207C File Offset: 0x0000027C
		public AccommodationListFormattingInfoDAO()
		{
			this.itemHeader = "";
			this.itemFooter = "";
			this.itemPre = "\u0095 ";
			this.itemPost = "";
			this.itemNewline = "\v";
			this.emptyListString = "None.";
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020D9 File Offset: 0x000002D9
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020E1 File Offset: 0x000002E1
		public string itemHeader { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020EA File Offset: 0x000002EA
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020F2 File Offset: 0x000002F2
		public string itemFooter { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020FB File Offset: 0x000002FB
		// (set) Token: 0x0600000C RID: 12 RVA: 0x00002103 File Offset: 0x00000303
		public string itemPre { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x0000210C File Offset: 0x0000030C
		// (set) Token: 0x0600000E RID: 14 RVA: 0x00002114 File Offset: 0x00000314
		public string itemPost { get; set; }

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x0600000F RID: 15 RVA: 0x0000211D File Offset: 0x0000031D
		// (set) Token: 0x06000010 RID: 16 RVA: 0x00002125 File Offset: 0x00000325
		public string itemNewline { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000011 RID: 17 RVA: 0x0000212E File Offset: 0x0000032E
		// (set) Token: 0x06000012 RID: 18 RVA: 0x00002136 File Offset: 0x00000336
		public string emptyListString { get; set; }
	}
}
