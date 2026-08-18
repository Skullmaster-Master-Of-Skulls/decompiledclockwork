using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000B8 RID: 184
	public class InsertUnorderedList : HtmlEditorExtenderButton
	{
		// Token: 0x17000203 RID: 515
		// (get) Token: 0x0600055C RID: 1372 RVA: 0x0000EA01 File Offset: 0x0000CC01
		public override string CommandName
		{
			get
			{
				return "insertUnorderedList";
			}
		}

		// Token: 0x17000204 RID: 516
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x0000EA08 File Offset: 0x0000CC08
		public override string Tooltip
		{
			get
			{
				return "Insert Unordered List";
			}
		}

		// Token: 0x17000205 RID: 517
		// (get) Token: 0x0600055E RID: 1374 RVA: 0x0000EA10 File Offset: 0x0000CC10
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return new Dictionary<string, string[]>
				{
					{
						"ul",
						new string[0]
					},
					{
						"li",
						new string[0]
					}
				};
			}
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x0000EA46 File Offset: 0x0000CC46
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
