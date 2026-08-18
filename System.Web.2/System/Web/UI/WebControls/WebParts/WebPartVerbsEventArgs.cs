using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005B6 RID: 1462
	public class WebPartVerbsEventArgs : EventArgs
	{
		// Token: 0x060049ED RID: 18925 RVA: 0x000F5538 File Offset: 0x000F3738
		public WebPartVerbsEventArgs() : this(null)
		{
		}

		// Token: 0x060049EE RID: 18926 RVA: 0x000F5541 File Offset: 0x000F3741
		public WebPartVerbsEventArgs(WebPartVerbCollection verbs)
		{
			this._verbs = verbs;
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x060049EF RID: 18927 RVA: 0x000F5550 File Offset: 0x000F3750
		// (set) Token: 0x060049F0 RID: 18928 RVA: 0x000F5566 File Offset: 0x000F3766
		public WebPartVerbCollection Verbs
		{
			get
			{
				if (this._verbs == null)
				{
					return WebPartVerbCollection.Empty;
				}
				return this._verbs;
			}
			set
			{
				this._verbs = value;
			}
		}

		// Token: 0x040027C4 RID: 10180
		private WebPartVerbCollection _verbs;
	}
}
