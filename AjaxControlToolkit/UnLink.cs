using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000BF RID: 191
	public class UnLink : HtmlEditorExtenderButton
	{
		// Token: 0x1700021D RID: 541
		// (get) Token: 0x0600057D RID: 1405 RVA: 0x0000EB3D File Offset: 0x0000CD3D
		public override string CommandName
		{
			get
			{
				return "UnLink";
			}
		}

		// Token: 0x1700021E RID: 542
		// (get) Token: 0x0600057E RID: 1406 RVA: 0x0000EB44 File Offset: 0x0000CD44
		public override string Tooltip
		{
			get
			{
				return "UnLink";
			}
		}

		// Token: 0x1700021F RID: 543
		// (get) Token: 0x0600057F RID: 1407 RVA: 0x0000EB4B File Offset: 0x0000CD4B
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000220 RID: 544
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x0000EB4E File Offset: 0x0000CD4E
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
