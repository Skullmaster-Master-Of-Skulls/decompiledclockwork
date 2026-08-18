using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000C1 RID: 193
	public class Copy : HtmlEditorExtenderButton
	{
		// Token: 0x17000225 RID: 549
		// (get) Token: 0x06000587 RID: 1415 RVA: 0x0000EBEB File Offset: 0x0000CDEB
		public override string CommandName
		{
			get
			{
				return "Copy";
			}
		}

		// Token: 0x17000226 RID: 550
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x0000EBF2 File Offset: 0x0000CDF2
		public override string Tooltip
		{
			get
			{
				return "Copy";
			}
		}

		// Token: 0x17000227 RID: 551
		// (get) Token: 0x06000589 RID: 1417 RVA: 0x0000EBF9 File Offset: 0x0000CDF9
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x0000EBFC File Offset: 0x0000CDFC
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
