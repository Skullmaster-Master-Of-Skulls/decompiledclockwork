using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000C2 RID: 194
	public class Cut : HtmlEditorExtenderButton
	{
		// Token: 0x17000229 RID: 553
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x0000EC07 File Offset: 0x0000CE07
		public override string CommandName
		{
			get
			{
				return "Cut";
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x0600058D RID: 1421 RVA: 0x0000EC0E File Offset: 0x0000CE0E
		public override string Tooltip
		{
			get
			{
				return "Cut";
			}
		}

		// Token: 0x1700022B RID: 555
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x0000EC15 File Offset: 0x0000CE15
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700022C RID: 556
		// (get) Token: 0x0600058F RID: 1423 RVA: 0x0000EC18 File Offset: 0x0000CE18
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
