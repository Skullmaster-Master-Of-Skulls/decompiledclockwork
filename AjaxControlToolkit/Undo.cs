using System;
using System.Collections.Generic;

namespace AjaxControlToolkit
{
	// Token: 0x020000B9 RID: 185
	public class Undo : HtmlEditorExtenderButton
	{
		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000561 RID: 1377 RVA: 0x0000EA51 File Offset: 0x0000CC51
		public override string CommandName
		{
			get
			{
				return "Undo";
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x06000562 RID: 1378 RVA: 0x0000EA58 File Offset: 0x0000CC58
		public override Dictionary<string, string[]> ElementWhiteList
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x06000563 RID: 1379 RVA: 0x0000EA5B File Offset: 0x0000CC5B
		public override Dictionary<string, string[]> AttributeWhiteList
		{
			get
			{
				return null;
			}
		}
	}
}
