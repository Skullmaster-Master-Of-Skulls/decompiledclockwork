using System;

namespace System.Web
{
	// Token: 0x02000076 RID: 118
	public class HtmlString : IHtmlString
	{
		// Token: 0x060006CE RID: 1742 RVA: 0x0000CB25 File Offset: 0x0000AD25
		public HtmlString(string value)
		{
			this._htmlString = value;
		}

		// Token: 0x060006CF RID: 1743 RVA: 0x0000CB34 File Offset: 0x0000AD34
		public string ToHtmlString()
		{
			return this._htmlString;
		}

		// Token: 0x060006D0 RID: 1744 RVA: 0x0000CB34 File Offset: 0x0000AD34
		public override string ToString()
		{
			return this._htmlString;
		}

		// Token: 0x04000232 RID: 562
		private string _htmlString;
	}
}
