using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000C7 RID: 199
	public class ForeColorSelector : HtmlEditorExtenderButton
	{
		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0000ED35 File Offset: 0x0000CF35
		public override string CommandName
		{
			get
			{
				return "ForeColor";
			}
		}

		// Token: 0x1700023E RID: 574
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x0000ED3C File Offset: 0x0000CF3C
		public override string Tooltip
		{
			get
			{
				return "Fore Color";
			}
		}

		// Token: 0x1700023F RID: 575
		// (get) Token: 0x060005A7 RID: 1447 RVA: 0x0000ED44 File Offset: 0x0000CF44
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
							"color"
						}
					}
				};
			}
		}

		// Token: 0x17000240 RID: 576
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x0000ED74 File Offset: 0x0000CF74
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"color",
						new string[0]
					}
				};
			}
		}
	}
}
