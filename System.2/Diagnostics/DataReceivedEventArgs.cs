using System;

namespace System.Diagnostics
{
	// Token: 0x020004C5 RID: 1221
	public class DataReceivedEventArgs : EventArgs
	{
		// Token: 0x06002DA3 RID: 11683 RVA: 0x000CD3C9 File Offset: 0x000CB5C9
		internal DataReceivedEventArgs(string data)
		{
			this._data = data;
		}

		// Token: 0x17000B07 RID: 2823
		// (get) Token: 0x06002DA4 RID: 11684 RVA: 0x000CD3D8 File Offset: 0x000CB5D8
		public string Data
		{
			get
			{
				return this._data;
			}
		}

		// Token: 0x04002736 RID: 10038
		internal string _data;
	}
}
