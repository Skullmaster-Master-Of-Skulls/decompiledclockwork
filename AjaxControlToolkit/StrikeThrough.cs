using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000B0 RID: 176
	public class StrikeThrough : HtmlEditorExtenderButton
	{
		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x06000534 RID: 1332 RVA: 0x0000E5F5 File Offset: 0x0000C7F5
		public override string CommandName
		{
			get
			{
				return "StrikeThrough";
			}
		}

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x06000535 RID: 1333 RVA: 0x0000E5FC File Offset: 0x0000C7FC
		public override string Tooltip
		{
			get
			{
				return "Strike Through";
			}
		}

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x06000536 RID: 1334 RVA: 0x0000E604 File Offset: 0x0000C804
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"strike",
						new string[]
						{
							"style"
						}
					}
				};
			}
		}

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x06000537 RID: 1335 RVA: 0x0000E634 File Offset: 0x0000C834
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"style",
						new string[0]
					}
				};
			}
		}
	}
}
