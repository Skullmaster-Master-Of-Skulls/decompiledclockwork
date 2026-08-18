using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000C2C RID: 3116
	internal class SpanPanel : Panel
	{
		// Token: 0x17002675 RID: 9845
		// (get) Token: 0x06007647 RID: 30279 RVA: 0x001B7680 File Offset: 0x001B5880
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Span;
			}
		}
	}
}
