using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000B4 RID: 180
	public class JustifyRight : HtmlEditorExtenderButton
	{
		// Token: 0x170001F3 RID: 499
		// (get) Token: 0x06000548 RID: 1352 RVA: 0x0000E796 File Offset: 0x0000C996
		public override string CommandName
		{
			get
			{
				return "JustifyRight";
			}
		}

		// Token: 0x170001F4 RID: 500
		// (get) Token: 0x06000549 RID: 1353 RVA: 0x0000E79D File Offset: 0x0000C99D
		public override string Tooltip
		{
			get
			{
				return "Justify Right";
			}
		}

		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x0600054A RID: 1354 RVA: 0x0000E7A4 File Offset: 0x0000C9A4
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

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x0600054B RID: 1355 RVA: 0x0000E7F8 File Offset: 0x0000C9F8
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
							"right"
						}
					}
				};
			}
		}
	}
}
