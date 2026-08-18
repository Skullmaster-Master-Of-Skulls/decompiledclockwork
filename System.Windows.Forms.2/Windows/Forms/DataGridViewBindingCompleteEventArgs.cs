using System;
using System.ComponentModel;

namespace System.Windows.Forms
{
	// Token: 0x020001A0 RID: 416
	public class DataGridViewBindingCompleteEventArgs : EventArgs
	{
		// Token: 0x06001CFB RID: 7419 RVA: 0x00087D7B File Offset: 0x00085F7B
		public DataGridViewBindingCompleteEventArgs(ListChangedType listChangedType)
		{
			this.listChangedType = listChangedType;
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06001CFC RID: 7420 RVA: 0x00087D8A File Offset: 0x00085F8A
		public ListChangedType ListChangedType
		{
			get
			{
				return this.listChangedType;
			}
		}

		// Token: 0x04000C84 RID: 3204
		private ListChangedType listChangedType;
	}
}
