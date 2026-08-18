using System;

namespace System.Web.UI.WebControls
{
	// Token: 0x020004F6 RID: 1270
	public class TextBoxControlBuilder : ControlBuilder
	{
		// Token: 0x06003F35 RID: 16181 RVA: 0x00007722 File Offset: 0x00005922
		public override bool AllowWhitespaceLiterals()
		{
			return false;
		}

		// Token: 0x06003F36 RID: 16182 RVA: 0x000097B7 File Offset: 0x000079B7
		public override bool HtmlDecodeLiterals()
		{
			return true;
		}
	}
}
