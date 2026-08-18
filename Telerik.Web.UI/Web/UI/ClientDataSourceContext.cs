using System;
using System.Collections;

namespace Telerik.Web.UI
{
	// Token: 0x020000FE RID: 254
	public class ClientDataSourceContext
	{
		// Token: 0x1700039D RID: 925
		// (get) Token: 0x06000AA1 RID: 2721 RVA: 0x000267B5 File Offset: 0x000249B5
		// (set) Token: 0x06000AA2 RID: 2722 RVA: 0x000267BD File Offset: 0x000249BD
		public ClientDataSourceFilterExpression FilterExpression { get; set; }

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x06000AA3 RID: 2723 RVA: 0x000267C6 File Offset: 0x000249C6
		// (set) Token: 0x06000AA4 RID: 2724 RVA: 0x000267CE File Offset: 0x000249CE
		public ClientDataSourceSortExpressionCollection SortExpressions { get; set; }

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x06000AA5 RID: 2725 RVA: 0x000267D7 File Offset: 0x000249D7
		// (set) Token: 0x06000AA6 RID: 2726 RVA: 0x000267DF File Offset: 0x000249DF
		public int PageSize { get; set; }

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000267E8 File Offset: 0x000249E8
		// (set) Token: 0x06000AA8 RID: 2728 RVA: 0x000267F0 File Offset: 0x000249F0
		public int CurrentPageIndex { get; set; }

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x000267F9 File Offset: 0x000249F9
		// (set) Token: 0x06000AAA RID: 2730 RVA: 0x00026801 File Offset: 0x00024A01
		public string CommandName { get; set; }

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000AAB RID: 2731 RVA: 0x0002680A File Offset: 0x00024A0A
		// (set) Token: 0x06000AAC RID: 2732 RVA: 0x00026812 File Offset: 0x00024A12
		public Hashtable OldValues { get; set; }

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000AAD RID: 2733 RVA: 0x0002681B File Offset: 0x00024A1B
		// (set) Token: 0x06000AAE RID: 2734 RVA: 0x00026823 File Offset: 0x00024A23
		public Hashtable NewValues { get; set; }

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000AAF RID: 2735 RVA: 0x0002682C File Offset: 0x00024A2C
		// (set) Token: 0x06000AB0 RID: 2736 RVA: 0x00026834 File Offset: 0x00024A34
		public Hashtable IDKeys { get; set; }
	}
}
