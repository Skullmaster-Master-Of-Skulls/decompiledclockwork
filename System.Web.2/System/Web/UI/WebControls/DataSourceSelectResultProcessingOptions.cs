using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000366 RID: 870
	public class DataSourceSelectResultProcessingOptions
	{
		// Token: 0x17000B2C RID: 2860
		// (get) Token: 0x0600284E RID: 10318 RVA: 0x0008238D File Offset: 0x0008058D
		// (set) Token: 0x0600284F RID: 10319 RVA: 0x00082395 File Offset: 0x00080595
		public bool AutoPage { get; set; }

		// Token: 0x17000B2D RID: 2861
		// (get) Token: 0x06002850 RID: 10320 RVA: 0x0008239E File Offset: 0x0008059E
		// (set) Token: 0x06002851 RID: 10321 RVA: 0x000823A6 File Offset: 0x000805A6
		public bool AutoSort { get; set; }

		// Token: 0x17000B2E RID: 2862
		// (get) Token: 0x06002852 RID: 10322 RVA: 0x000823AF File Offset: 0x000805AF
		// (set) Token: 0x06002853 RID: 10323 RVA: 0x000823B7 File Offset: 0x000805B7
		public Type ModelType { get; set; }
	}
}
