using System;

namespace System.Diagnostics
{
	// Token: 0x02000748 RID: 1864
	public class DataReceivedEventArgs : EventArgs
	{
		// Token: 0x060038EC RID: 14572 RVA: 0x000F0155 File Offset: 0x000EF155
		internal DataReceivedEventArgs(string data)
		{
			this._data = data;
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x060038ED RID: 14573 RVA: 0x000F0164 File Offset: 0x000EF164
		public string Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x0400327F RID: 12927
		internal string _data;
	}
}
