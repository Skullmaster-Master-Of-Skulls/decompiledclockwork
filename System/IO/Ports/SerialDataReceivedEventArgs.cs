using System;

namespace System.IO.Ports
{
	// Token: 0x020007B4 RID: 1972
	public class SerialDataReceivedEventArgs : EventArgs
	{
		// Token: 0x06003CB1 RID: 15537 RVA: 0x001034F1 File Offset: 0x001024F1
		internal SerialDataReceivedEventArgs(SerialData eventCode)
		{
			this.receiveType = eventCode;
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x06003CB2 RID: 15538 RVA: 0x00103500 File Offset: 0x00102500
		public SerialData EventType
		{
			get
			{
				return this.receiveType;
			}
		}

		// Token: 0x04003573 RID: 13683
		internal SerialData receiveType;
	}
}
