using System;
using System.Text;

namespace ClockWorkWebAPI.ClockWorkAPIReplacement
{
	// Token: 0x0200005E RID: 94
	public class HtmlPdfPage
	{
		// Token: 0x060004C7 RID: 1223 RVA: 0x000218A2 File Offset: 0x0001FAA2
		public HtmlPdfPage()
		{
			this._Html = new StringBuilder();
		}

		// Token: 0x060004C8 RID: 1224 RVA: 0x000218B7 File Offset: 0x0001FAB7
		public virtual void AppendHtml(string content, params object[] values)
		{
			this._Html.AppendFormat(content, values);
		}

		// Token: 0x04000286 RID: 646
		internal StringBuilder _Html;
	}
}
