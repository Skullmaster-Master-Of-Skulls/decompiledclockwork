using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000CB RID: 203
	public class RemoveFormat : HtmlEditorExtenderButton
	{
		// Token: 0x1700024D RID: 589
		// (get) Token: 0x060005B9 RID: 1465 RVA: 0x0000EF02 File Offset: 0x0000D102
		public override string CommandName
		{
			get
			{
				return "RemoveFormat";
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x060005BA RID: 1466 RVA: 0x0000EF09 File Offset: 0x0000D109
		public override string Tooltip
		{
			get
			{
				return "Remove Format";
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x060005BB RID: 1467 RVA: 0x0000EF10 File Offset: 0x0000D110
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x0000EF13 File Offset: 0x0000D113
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
