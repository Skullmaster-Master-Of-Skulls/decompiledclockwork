using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000CA RID: 202
	public class Outdent : HtmlEditorExtenderButton
	{
		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060005B4 RID: 1460 RVA: 0x0000EEE6 File Offset: 0x0000D0E6
		public override string CommandName
		{
			get
			{
				return "Outdent";
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060005B5 RID: 1461 RVA: 0x0000EEED File Offset: 0x0000D0ED
		public override string Tooltip
		{
			get
			{
				return "Outdent";
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060005B6 RID: 1462 RVA: 0x0000EEF4 File Offset: 0x0000D0F4
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060005B7 RID: 1463 RVA: 0x0000EEF7 File Offset: 0x0000D0F7
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
