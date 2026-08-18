using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000BD RID: 189
	public class SelectAll : HtmlEditorExtenderButton
	{
		// Token: 0x17000215 RID: 533
		// (get) Token: 0x06000573 RID: 1395 RVA: 0x0000EB05 File Offset: 0x0000CD05
		public override string CommandName
		{
			get
			{
				return "SelectAll";
			}
		}

		// Token: 0x17000216 RID: 534
		// (get) Token: 0x06000574 RID: 1396 RVA: 0x0000EB0C File Offset: 0x0000CD0C
		public override string Tooltip
		{
			get
			{
				return "Select All";
			}
		}

		// Token: 0x17000217 RID: 535
		// (get) Token: 0x06000575 RID: 1397 RVA: 0x0000EB13 File Offset: 0x0000CD13
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000218 RID: 536
		// (get) Token: 0x06000576 RID: 1398 RVA: 0x0000EB16 File Offset: 0x0000CD16
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
