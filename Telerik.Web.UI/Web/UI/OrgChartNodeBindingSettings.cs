using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000BFF RID: 3071
	public class OrgChartNodeBindingSettings : IOrgChartBindingSettings
	{
		// Token: 0x17002619 RID: 9753
		// (get) Token: 0x060074E5 RID: 29925 RVA: 0x001B33FE File Offset: 0x001B15FE
		// (set) Token: 0x060074E6 RID: 29926 RVA: 0x001B3406 File Offset: 0x001B1606
		public string DataFieldParentID { get; set; }

		// Token: 0x1700261A RID: 9754
		// (get) Token: 0x060074E7 RID: 29927 RVA: 0x001B340F File Offset: 0x001B160F
		// (set) Token: 0x060074E8 RID: 29928 RVA: 0x001B3417 File Offset: 0x001B1617
		public object DataSource { get; set; }

		// Token: 0x1700261B RID: 9755
		// (get) Token: 0x060074E9 RID: 29929 RVA: 0x001B3420 File Offset: 0x001B1620
		// (set) Token: 0x060074EA RID: 29930 RVA: 0x001B3428 File Offset: 0x001B1628
		public string DataSourceID { get; set; }

		// Token: 0x1700261C RID: 9756
		// (get) Token: 0x060074EB RID: 29931 RVA: 0x001B3431 File Offset: 0x001B1631
		// (set) Token: 0x060074EC RID: 29932 RVA: 0x001B3439 File Offset: 0x001B1639
		public string DataFieldID { get; set; }

		// Token: 0x1700261D RID: 9757
		// (get) Token: 0x060074ED RID: 29933 RVA: 0x001B3442 File Offset: 0x001B1642
		// (set) Token: 0x060074EE RID: 29934 RVA: 0x001B344A File Offset: 0x001B164A
		public string DataCollapsedField { get; set; }

		// Token: 0x1700261E RID: 9758
		// (get) Token: 0x060074EF RID: 29935 RVA: 0x001B3453 File Offset: 0x001B1653
		// (set) Token: 0x060074F0 RID: 29936 RVA: 0x001B345B File Offset: 0x001B165B
		public string DataGroupCollapsedField { get; set; }
	}
}
