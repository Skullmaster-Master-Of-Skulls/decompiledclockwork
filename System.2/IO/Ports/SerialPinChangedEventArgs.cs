using System;

namespace System.IO.Ports
{
	// Token: 0x0200040F RID: 1039
	public class SerialPinChangedEventArgs : EventArgs
	{
		// Token: 0x060026CA RID: 9930 RVA: 0x000B24F0 File Offset: 0x000B06F0
		internal SerialPinChangedEventArgs(SerialPinChange eventCode)
		{
			this.pinChanged = eventCode;
		}

		// Token: 0x17000993 RID: 2451
		// (get) Token: 0x060026CB RID: 9931 RVA: 0x000B24FF File Offset: 0x000B06FF
		public SerialPinChange EventType
		{
			get
			{
				return this.pinChanged;
			}
		}

		// Token: 0x0400210F RID: 8463
		private SerialPinChange pinChanged;
	}
}
