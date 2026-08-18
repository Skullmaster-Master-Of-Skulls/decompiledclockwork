using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000C5 RID: 197
	public class FontNameSelector : HtmlEditorExtenderButton
	{
		// Token: 0x17000235 RID: 565
		// (get) Token: 0x0600059B RID: 1435 RVA: 0x0000EC5B File Offset: 0x0000CE5B
		public override string CommandName
		{
			get
			{
				return "FontName";
			}
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x0000EC62 File Offset: 0x0000CE62
		public override string Tooltip
		{
			get
			{
				return "Font Name";
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x0600059D RID: 1437 RVA: 0x0000EC6C File Offset: 0x0000CE6C
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
							"face"
						}
					}
				};
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x0000EC9C File Offset: 0x0000CE9C
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"face",
						new string[0]
					}
				};
			}
		}
	}
}
