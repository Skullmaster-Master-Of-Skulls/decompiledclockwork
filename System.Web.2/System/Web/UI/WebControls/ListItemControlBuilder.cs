using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000455 RID: 1109
	public class ListItemControlBuilder : ControlBuilder
	{
		// Token: 0x060035AA RID: 13738 RVA: 0x00007722 File Offset: 0x00005922
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		// Token: 0x060035AB RID: 13739 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool HtmlDecodeLiterals()
		{
			return true;
		}
	}
}
