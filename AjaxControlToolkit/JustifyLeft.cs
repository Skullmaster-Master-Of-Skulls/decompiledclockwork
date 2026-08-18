using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000B3 RID: 179
	public class JustifyLeft : HtmlEditorExtenderButton
	{
		// Token: 0x170001EF RID: 495
		// (get) Token: 0x06000543 RID: 1347 RVA: 0x0000E6E0 File Offset: 0x0000C8E0
		public override string CommandName
		{
			get
			{
				return "JustifyLeft";
			}
		}

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000544 RID: 1348 RVA: 0x0000E6E7 File Offset: 0x0000C8E7
		public override string Tooltip
		{
			get
			{
				return "Justify Left";
			}
		}

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000545 RID: 1349 RVA: 0x0000E6F0 File Offset: 0x0000C8F0
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

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000546 RID: 1350 RVA: 0x0000E744 File Offset: 0x0000C944
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
							"left"
						}
					}
				};
			}
		}
	}
}
