using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000CD RID: 205
	public class InsertImage : HtmlEditorExtenderButton
	{
		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060005C3 RID: 1475 RVA: 0x0000EF3A File Offset: 0x0000D13A
		public override string CommandName
		{
			get
			{
				return "InsertImage";
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060005C4 RID: 1476 RVA: 0x0000EF41 File Offset: 0x0000D141
		public override string Tooltip
		{
			get
			{
				return "Insert Image";
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060005C5 RID: 1477 RVA: 0x0000EF48 File Offset: 0x0000D148
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"img",
						new string[]
						{
							"src"
						}
					}
				};
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060005C6 RID: 1478 RVA: 0x0000EF78 File Offset: 0x0000D178
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"src",
						new string[0]
					}
				};
			}
		}
	}
}
