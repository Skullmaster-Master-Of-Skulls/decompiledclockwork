using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000C9 RID: 201
	public class InsertHorizontalRule : HtmlEditorExtenderButton
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x060005AF RID: 1455 RVA: 0x0000EE62 File Offset: 0x0000D062
		public override string CommandName
		{
			get
			{
				return "InsertHorizontalRule";
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x060005B0 RID: 1456 RVA: 0x0000EE69 File Offset: 0x0000D069
		public override string Tooltip
		{
			get
			{
				return "Insert Horizontal Rule";
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060005B1 RID: 1457 RVA: 0x0000EE70 File Offset: 0x0000D070
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"hr",
						new string[]
						{
							"size",
							"width"
						}
					}
				};
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060005B2 RID: 1458 RVA: 0x0000EEA8 File Offset: 0x0000D0A8
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"size",
						new string[0]
					},
					{
						"width",
						new string[0]
					}
				};
			}
		}
	}
}
