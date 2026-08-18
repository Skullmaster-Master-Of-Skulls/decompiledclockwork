using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000B5 RID: 181
	public class JustifyCenter : HtmlEditorExtenderButton
	{
		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600054D RID: 1357 RVA: 0x0000E84A File Offset: 0x0000CA4A
		public override string CommandName
		{
			get
			{
				return "JustifyCenter";
			}
		}

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600054E RID: 1358 RVA: 0x0000E851 File Offset: 0x0000CA51
		public override string Tooltip
		{
			get
			{
				return "Justify Center";
			}
		}

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600054F RID: 1359 RVA: 0x0000E858 File Offset: 0x0000CA58
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"p",
						new string[]
						{
							"align"
						}
					},
					{
						"div",
						new string[]
						{
							"style",
							"align"
						}
					}
				};
			}
		}

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x06000550 RID: 1360 RVA: 0x0000E8AC File Offset: 0x0000CAAC
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"style",
						new string[]
						{
							"text-align"
						}
					},
					{
						"align",
						new string[]
						{
							"center"
						}
					}
				};
			}
		}
	}
}
