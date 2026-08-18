using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000B2 RID: 178
	public class Superscript : HtmlEditorExtenderButton
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x0600053E RID: 1342 RVA: 0x0000E6A0 File Offset: 0x0000C8A0
		public override string CommandName
		{
			get
			{
				return "Superscript";
			}
		}

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x0600053F RID: 1343 RVA: 0x0000E6A7 File Offset: 0x0000C8A7
		public override string Tooltip
		{
			get
			{
				return "Super Script";
			}
		}

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x06000540 RID: 1344 RVA: 0x0000E6B0 File Offset: 0x0000C8B0
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"sup",
						new string[0]
					}
				};
			}
		}

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x06000541 RID: 1345 RVA: 0x0000E6D5 File Offset: 0x0000C8D5
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
