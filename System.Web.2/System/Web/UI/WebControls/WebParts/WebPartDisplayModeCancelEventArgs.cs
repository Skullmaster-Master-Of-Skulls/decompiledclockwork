using System;
using System.ComponentModel;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000594 RID: 1428
	public class WebPartDisplayModeCancelEventArgs : CancelEventArgs
	{
		// Token: 0x0600480A RID: 18442 RVA: 0x000ECCCD File Offset: 0x000EAECD
		public WebPartDisplayModeCancelEventArgs(WebPartDisplayMode newDisplayMode)
		{
			this._newDisplayMode = newDisplayMode;
		}

		// Token: 0x17001550 RID: 5456
		// (get) Token: 0x0600480B RID: 18443 RVA: 0x000ECCDC File Offset: 0x000EAEDC
		// (set) Token: 0x0600480C RID: 18444 RVA: 0x000ECCE4 File Offset: 0x000EAEE4
		public WebPartDisplayMode NewDisplayMode
		{
			get
			{
				return this._newDisplayMode;
			}
			set
			{
				this._newDisplayMode = value;
			}
		}

		// Token: 0x0400271F RID: 10015
		private WebPartDisplayMode _newDisplayMode;
	}
}
