using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000BC RID: 188
	public class Delete : HtmlEditorExtenderButton
	{
		// Token: 0x17000211 RID: 529
		// (get) Token: 0x0600056E RID: 1390 RVA: 0x0000EAE9 File Offset: 0x0000CCE9
		public override string CommandName
		{
			get
			{
				return "Delete";
			}
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x0600056F RID: 1391 RVA: 0x0000EAF0 File Offset: 0x0000CCF0
		public override string Tooltip
		{
			get
			{
				return "Delete";
			}
		}

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000570 RID: 1392 RVA: 0x0000EAF7 File Offset: 0x0000CCF7
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x06000571 RID: 1393 RVA: 0x0000EAFA File Offset: 0x0000CCFA
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
