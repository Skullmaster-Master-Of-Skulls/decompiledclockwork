using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020012D9 RID: 4825
	internal class MonthViewCellWrapper : WebControl
	{
		// Token: 0x0600CA93 RID: 51859 RVA: 0x002D37C6 File Offset: 0x002D19C6
		internal MonthViewCellWrapper(int zIndex)
		{
			this.CssClass = "rsWrap";
			base.Style["z-index"] = zIndex.ToString();
		}

		// Token: 0x17004178 RID: 16760
		// (get) Token: 0x0600CA94 RID: 51860 RVA: 0x002D37F0 File Offset: 0x002D19F0
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}
	}
}
