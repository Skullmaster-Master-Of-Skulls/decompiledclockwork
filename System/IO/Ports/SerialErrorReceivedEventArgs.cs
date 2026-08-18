using System;

namespace System.IO.Ports
{
	// Token: 0x020007AD RID: 1965
	public class SerialErrorReceivedEventArgs : EventArgs
	{
		// Token: 0x06003C53 RID: 15443 RVA: 0x00101A82 File Offset: 0x00100A82
		internal SerialErrorReceivedEventArgs(SerialError eventCode)
		{
			this.errorType = eventCode;
		}

		// Token: 0x17000E24 RID: 3620
		// (get) Token: 0x06003C54 RID: 15444 RVA: 0x00101A91 File Offset: 0x00100A91
		public SerialError EventType
		{
			get
			{
				return this.errorType;
			}
		}

		// Token: 0x04003537 RID: 13623
		private SerialError errorType;
	}
}
