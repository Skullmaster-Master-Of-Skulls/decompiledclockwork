using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000C6 RID: 198
	public class FontSizeSelector : HtmlEditorExtenderButton
	{
		// Token: 0x17000239 RID: 569
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x0000ECC9 File Offset: 0x0000CEC9
		public override string CommandName
		{
			get
			{
				return "FontSize";
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x060005A1 RID: 1441 RVA: 0x0000ECD0 File Offset: 0x0000CED0
		public override string Tooltip
		{
			get
			{
				return "Font Size";
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x0000ECD8 File Offset: 0x0000CED8
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"font",
						new string[]
						{
							"size"
						}
					}
				};
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060005A3 RID: 1443 RVA: 0x0000ED08 File Offset: 0x0000CF08
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"size",
						new string[0]
					}
				};
			}
		}
	}
}
