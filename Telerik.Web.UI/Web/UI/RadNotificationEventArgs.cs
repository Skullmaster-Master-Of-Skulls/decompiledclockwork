using System;

namespace Telerik.Web.UI
{
	// Token: 0x02000EB9 RID: 3769
	public class RadNotificationEventArgs : EventArgs
	{
		// Token: 0x06008FED RID: 36845 RVA: 0x00206CFA File Offset: 0x00204EFA
		public RadNotificationEventArgs(string val)
		{
			this._value = val;
		}

		// Token: 0x17002D94 RID: 11668
		// (get) Token: 0x06008FEE RID: 36846 RVA: 0x00206D09 File Offset: 0x00204F09
		// (set) Token: 0x06008FEF RID: 36847 RVA: 0x00206D11 File Offset: 0x00204F11
		public string Value
		{
			get
			{
				return this._value;
			}
			set
			{
				this._value = value;
			}
		}

		// Token: 0x0400280D RID: 10253
		private string _value;
	}
}
