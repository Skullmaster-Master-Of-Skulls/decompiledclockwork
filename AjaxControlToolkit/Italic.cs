using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000AE RID: 174
	public class Italic : HtmlEditorExtenderButton
	{
		// Token: 0x170001DD RID: 477
		// (get) Token: 0x0600052C RID: 1324 RVA: 0x0000E511 File Offset: 0x0000C711
		public override string CommandName
		{
			get
			{
				return "Italic";
			}
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x0600052D RID: 1325 RVA: 0x0000E518 File Offset: 0x0000C718
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"i",
						new string[]
						{
							"style"
						}
					},
					{
						"em",
						new string[]
						{
							"style"
						}
					}
				};
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x0600052E RID: 1326 RVA: 0x0000E564 File Offset: 0x0000C764
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
