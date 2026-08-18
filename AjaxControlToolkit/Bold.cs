using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000AD RID: 173
	public class Bold : HtmlEditorExtenderButton
	{
		// Token: 0x170001DA RID: 474
		// (get) Token: 0x06000528 RID: 1320 RVA: 0x0000E490 File Offset: 0x0000C690
		public override string CommandName
		{
			get
			{
				return "Bold";
			}
		}

		// Token: 0x170001DB RID: 475
		// (get) Token: 0x06000529 RID: 1321 RVA: 0x0000E498 File Offset: 0x0000C698
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"b",
						new string[]
						{
							"style"
						}
					},
					{
						"strong",
						new string[]
						{
							"style"
						}
					}
				};
			}
		}

		// Token: 0x170001DC RID: 476
		// (get) Token: 0x0600052A RID: 1322 RVA: 0x0000E4E4 File Offset: 0x0000C6E4
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"style",
						new string[0]
					}
				};
			}
		}
	}
}
