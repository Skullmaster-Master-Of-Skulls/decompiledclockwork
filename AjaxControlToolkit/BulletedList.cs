using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace AjaxControlToolkit
{
	// Token: 0x0200016A RID: 362
	[ToolboxItem(false)]
	public class BulletedList : WebControl
	{
		// Token: 0x170003B0 RID: 944
		// (get) Token: 0x060009B8 RID: 2488 RVA: 0x00018E24 File Offset: 0x00017024
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Ul;
			}
		}
	}
}
