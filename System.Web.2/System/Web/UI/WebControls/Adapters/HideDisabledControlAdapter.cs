using System;

namespace System.Web.UI.WebControls.Adapters
{
	// Token: 0x020005BF RID: 1471
	public class HideDisabledControlAdapter : WebControlAdapter
	{
		// Token: 0x06004AA0 RID: 19104 RVA: 0x000F7FF9 File Offset: 0x000F61F9
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (!base.Control.Enabled)
			{
				return;
			}
			base.Control.Render(writer);
		}
	}
}
