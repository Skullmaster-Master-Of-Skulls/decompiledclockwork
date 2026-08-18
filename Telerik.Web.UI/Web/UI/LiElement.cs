using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200054F RID: 1359
	internal class LiElement : WebControl
	{
		// Token: 0x17000F83 RID: 3971
		// (get) Token: 0x0600302D RID: 12333 RVA: 0x0009DA4A File Offset: 0x0009BC4A
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}
	}
}
