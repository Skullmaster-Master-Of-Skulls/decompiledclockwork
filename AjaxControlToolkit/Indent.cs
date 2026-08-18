using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000C8 RID: 200
	public class Indent : HtmlEditorExtenderButton
	{
		// Token: 0x17000241 RID: 577
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x0000EDA1 File Offset: 0x0000CFA1
		public override string CommandName
		{
			get
			{
				return "Indent";
			}
		}

		// Token: 0x17000242 RID: 578
		// (get) Token: 0x060005AB RID: 1451 RVA: 0x0000EDA8 File Offset: 0x0000CFA8
		public override string Tooltip
		{
			get
			{
				return "Indent";
			}
		}

		// Token: 0x17000243 RID: 579
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x0000EDB0 File Offset: 0x0000CFB0
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"blockquote",
						new string[]
						{
							"style",
							"dir"
						}
					}
				};
			}
		}

		// Token: 0x17000244 RID: 580
		// (get) Token: 0x060005AD RID: 1453 RVA: 0x0000EDE8 File Offset: 0x0000CFE8
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
							"margin-right",
							"margin",
							"padding",
							"border"
						}
					},
					{
						"dir",
						new string[]
						{
							"ltr",
							"rtl",
							"auto"
						}
					}
				};
			}
		}
	}
}
