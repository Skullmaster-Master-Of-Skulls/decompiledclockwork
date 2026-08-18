using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200038F RID: 911
	internal class DivTableCell : TableCell
	{
		// Token: 0x17000A70 RID: 2672
		// (get) Token: 0x06001F6D RID: 8045 RVA: 0x00063666 File Offset: 0x00061866
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Div;
			}
		}
	}
}
