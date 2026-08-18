using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000C3 RID: 195
	public class Paste : HtmlEditorExtenderButton
	{
		// Token: 0x1700022D RID: 557
		// (get) Token: 0x06000591 RID: 1425 RVA: 0x0000EC23 File Offset: 0x0000CE23
		public override string CommandName
		{
			get
			{
				return "Paste";
			}
		}

		// Token: 0x1700022E RID: 558
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x0000EC2A File Offset: 0x0000CE2A
		public override string Tooltip
		{
			get
			{
				return "Paste";
			}
		}

		// Token: 0x1700022F RID: 559
		// (get) Token: 0x06000593 RID: 1427 RVA: 0x0000EC31 File Offset: 0x0000CE31
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0000EC34 File Offset: 0x0000CE34
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
