using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000CC RID: 204
	public class HorizontalSeparator : HtmlEditorExtenderButton
	{
		// Token: 0x17000251 RID: 593
		// (get) Token: 0x060005BE RID: 1470 RVA: 0x0000EF1E File Offset: 0x0000D11E
		public override string CommandName
		{
			get
			{
				return "HorizontalSeparator";
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0000EF25 File Offset: 0x0000D125
		public override string Tooltip
		{
			get
			{
				return "Separator";
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x060005C0 RID: 1472 RVA: 0x0000EF2C File Offset: 0x0000D12C
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x060005C1 RID: 1473 RVA: 0x0000EF2F File Offset: 0x0000D12F
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
