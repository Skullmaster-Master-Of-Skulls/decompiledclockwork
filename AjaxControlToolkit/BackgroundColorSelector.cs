using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000C0 RID: 192
	public class BackgroundColorSelector : HtmlEditorExtenderButton
	{
		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x0000EB59 File Offset: 0x0000CD59
		public override string CommandName
		{
			get
			{
				return "BackColor";
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000583 RID: 1411 RVA: 0x0000EB60 File Offset: 0x0000CD60
		public override string Tooltip
		{
			get
			{
				return "Back Color";
			}
		}

		// Token: 0x17000223 RID: 547
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x0000EB68 File Offset: 0x0000CD68
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
							"style"
						}
					},
					{
						"span",
						new string[]
						{
							"style"
						}
					}
				};
			}
		}

		// Token: 0x17000224 RID: 548
		// (get) Token: 0x06000585 RID: 1413 RVA: 0x0000EBB4 File Offset: 0x0000CDB4
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
							"background-color"
						}
					}
				};
			}
		}
	}
}
