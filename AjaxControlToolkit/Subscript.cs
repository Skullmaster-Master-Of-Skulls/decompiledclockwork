using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000B1 RID: 177
	public class Subscript : HtmlEditorExtenderButton
	{
		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x06000539 RID: 1337 RVA: 0x0000E661 File Offset: 0x0000C861
		public override string CommandName
		{
			get
			{
				return "Subscript";
			}
		}

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x0600053A RID: 1338 RVA: 0x0000E668 File Offset: 0x0000C868
		public override string Tooltip
		{
			get
			{
				return "Sub Script";
			}
		}

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x0600053B RID: 1339 RVA: 0x0000E670 File Offset: 0x0000C870
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"sub",
						new string[0]
					}
				};
			}
		}

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x0600053C RID: 1340 RVA: 0x0000E695 File Offset: 0x0000C895
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
