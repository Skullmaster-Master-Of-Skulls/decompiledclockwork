using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000AC RID: 172
	public abstract class HtmlEditorExtenderButton
	{
		// Token: 0x170001D6 RID: 470
		// (get) Token: 0x06000523 RID: 1315
		public abstract string CommandName { get; }

		// Token: 0x170001D7 RID: 471
		// (get) Token: 0x06000524 RID: 1316 RVA: 0x0000E480 File Offset: 0x0000C680
		public virtual string Tooltip
		{
			get
			{
				return this.CommandName;
			}
		}

		// Token: 0x170001D8 RID: 472
		// (get) Token: 0x06000525 RID: 1317
		public abstract Dictionary<string, string[]> ElementWhiteList { get; }

		// Token: 0x170001D9 RID: 473
		// (get) Token: 0x06000526 RID: 1318
		public abstract Dictionary<string, string[]> AttributeWhiteList { get; }
	}
}
