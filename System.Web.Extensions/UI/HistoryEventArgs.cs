using System;
using System.Collections.Specialized;

namespace System.Web.UI
{
	// Token: 0x02000050 RID: 80
	public class HistoryEventArgs : EventArgs
	{
		// Token: 0x060002FA RID: 762 RVA: 0x0001203F File Offset: 0x0001023F
		public HistoryEventArgs(NameValueCollection state)
		{
			this._state = state;
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x060002FB RID: 763 RVA: 0x0001204E File Offset: 0x0001024E
		public NameValueCollection State
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x04000119 RID: 281
		private NameValueCollection _state;
	}
}
