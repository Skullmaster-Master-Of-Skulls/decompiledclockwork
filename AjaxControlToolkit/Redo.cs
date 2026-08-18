using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000BA RID: 186
	public class Redo : HtmlEditorExtenderButton
	{
		// Token: 0x1700020A RID: 522
		// (get) Token: 0x06000565 RID: 1381 RVA: 0x0000EA66 File Offset: 0x0000CC66
		public override string CommandName
		{
			get
			{
				return "Redo";
			}
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x06000566 RID: 1382 RVA: 0x0000EA6D File Offset: 0x0000CC6D
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700020C RID: 524
		// (get) Token: 0x06000567 RID: 1383 RVA: 0x0000EA70 File Offset: 0x0000CC70
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
