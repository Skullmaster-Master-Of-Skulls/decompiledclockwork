using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000B6 RID: 182
	public class JustifyFull : HtmlEditorExtenderButton
	{
		// Token: 0x170001FB RID: 507
		// (get) Token: 0x06000552 RID: 1362 RVA: 0x0000E8FE File Offset: 0x0000CAFE
		public override string CommandName
		{
			get
			{
				return "JustifyFull";
			}
		}

		// Token: 0x170001FC RID: 508
		// (get) Token: 0x06000553 RID: 1363 RVA: 0x0000E905 File Offset: 0x0000CB05
		public override string Tooltip
		{
			get
			{
				return "Justify Full";
			}
		}

		// Token: 0x170001FD RID: 509
		// (get) Token: 0x06000554 RID: 1364 RVA: 0x0000E90C File Offset: 0x0000CB0C
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

		// Token: 0x170001FE RID: 510
		// (get) Token: 0x06000555 RID: 1365 RVA: 0x0000E960 File Offset: 0x0000CB60
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
							"justify"
						}
					}
				};
			}
		}
	}
}
