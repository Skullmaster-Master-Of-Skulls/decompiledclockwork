using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000C4 RID: 196
	public class CleanWord : HtmlEditorExtenderButton
	{
		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0000EC3F File Offset: 0x0000CE3F
		public override string CommandName
		{
			get
			{
				return "CleanWord";
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000597 RID: 1431 RVA: 0x0000EC46 File Offset: 0x0000CE46
		public override string Tooltip
		{
			get
			{
				return "Clean Word HTML";
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0000EC4D File Offset: 0x0000CE4D
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000234 RID: 564
		// (get) Token: 0x06000599 RID: 1433 RVA: 0x0000EC50 File Offset: 0x0000CE50
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
