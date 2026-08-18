using System;

namespace TechnoPro.Common.Public.Entities.Legacy.DynamicData
{
	// Token: 0x020002FA RID: 762
	public class LegacyDynamicDataItemItemsThatHaveBeenDecrypted : BusinessBase<int>
	{
		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x0600173B RID: 5947 RVA: 0x0001C460 File Offset: 0x0001A660
		// (set) Token: 0x0600173C RID: 5948 RVA: 0x0001C468 File Offset: 0x0001A668
		public string ControlValueDecryptedString { get; set; }

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x0600173D RID: 5949 RVA: 0x0001C471 File Offset: 0x0001A671
		// (set) Token: 0x0600173E RID: 5950 RVA: 0x0001C479 File Offset: 0x0001A679
		public string TextForLetter { get; set; }

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x0600173F RID: 5951 RVA: 0x0001C482 File Offset: 0x0001A682
		// (set) Token: 0x06001740 RID: 5952 RVA: 0x0001C48A File Offset: 0x0001A68A
		public string PrivateNote { get; set; }

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x06001741 RID: 5953 RVA: 0x0001C493 File Offset: 0x0001A693
		// (set) Token: 0x06001742 RID: 5954 RVA: 0x0001C49B File Offset: 0x0001A69B
		public string RecommendedToStudentButDeclinedDetail { get; set; }
	}
}
