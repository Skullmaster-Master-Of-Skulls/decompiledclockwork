using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000B7 RID: 183
	public class InsertOrderedList : HtmlEditorExtenderButton
	{
		// Token: 0x170001FF RID: 511
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x0000E9B2 File Offset: 0x0000CBB2
		public override string CommandName
		{
			get
			{
				return "insertOrderedList";
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x06000558 RID: 1368 RVA: 0x0000E9B9 File Offset: 0x0000CBB9
		public override string Tooltip
		{
			get
			{
				return "Insert Ordered List";
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x0000E9C0 File Offset: 0x0000CBC0
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"ol",
						new string[0]
					},
					{
						"li",
						new string[0]
					}
				};
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x0600055A RID: 1370 RVA: 0x0000E9F6 File Offset: 0x0000CBF6
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
