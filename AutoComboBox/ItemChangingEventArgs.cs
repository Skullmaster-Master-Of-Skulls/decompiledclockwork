using System;
using System.ComponentModel;

namespace AutoComboBox
{
	// Token: 0x02000101 RID: 257
	public class ItemChangingEventArgs : CancelEventArgs
	{
		// Token: 0x06000A1F RID: 2591 RVA: 0x0004E2A8 File Offset: 0x0004D2A8
		public ItemChangingEventArgs(int index)
		{
			this.m_index = index;
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x06000A20 RID: 2592 RVA: 0x0004E2BC File Offset: 0x0004D2BC
		public int Index
		{
			get
			{
				return this.m_index;
			}
		}

		// Token: 0x0400076B RID: 1899
		private int m_index;
	}
}
