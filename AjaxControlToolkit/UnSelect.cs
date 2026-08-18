using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000BE RID: 190
	public class UnSelect : HtmlEditorExtenderButton
	{
		// Token: 0x17000219 RID: 537
		// (get) Token: 0x06000578 RID: 1400 RVA: 0x0000EB21 File Offset: 0x0000CD21
		public override string CommandName
		{
			get
			{
				return "UnSelect";
			}
		}

		// Token: 0x1700021A RID: 538
		// (get) Token: 0x06000579 RID: 1401 RVA: 0x0000EB28 File Offset: 0x0000CD28
		public override string Tooltip
		{
			get
			{
				return "UnSelect";
			}
		}

		// Token: 0x1700021B RID: 539
		// (get) Token: 0x0600057A RID: 1402 RVA: 0x0000EB2F File Offset: 0x0000CD2F
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700021C RID: 540
		// (get) Token: 0x0600057B RID: 1403 RVA: 0x0000EB32 File Offset: 0x0000CD32
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
