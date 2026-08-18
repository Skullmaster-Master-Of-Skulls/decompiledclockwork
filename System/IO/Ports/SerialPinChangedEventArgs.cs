using System;

namespace System.IO.Ports
{
	// Token: 0x020007B0 RID: 1968
	public class SerialPinChangedEventArgs : EventArgs
	{
		// Token: 0x06003C59 RID: 15449 RVA: 0x00101A99 File Offset: 0x00100A99
		internal SerialPinChangedEventArgs(SerialPinChange eventCode)
		{
			this.pinChanged = eventCode;
		}

		// Token: 0x17000E25 RID: 3621
		// (get) Token: 0x06003C5A RID: 15450 RVA: 0x00101AA8 File Offset: 0x00100AA8
		public SerialPinChange EventType
		{
			get
			{
				return this.pinChanged;
			}
		}

		// Token: 0x0400353E RID: 13630
		private SerialPinChange pinChanged;
	}
}
