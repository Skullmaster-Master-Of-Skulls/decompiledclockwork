using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x0200016B RID: 363
	[ToolboxItem(false)]
	public class BulletedListItem : WebControl
	{
		// Token: 0x170003B1 RID: 945
		// (get) Token: 0x060009BA RID: 2490 RVA: 0x00018E30 File Offset: 0x00017030
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Li;
			}
		}
	}
}
