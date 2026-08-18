using System;

namespace System.IO.Ports
{
	// Token: 0x0200040C RID: 1036
	public class SerialErrorReceivedEventArgs : EventArgs
	{
		// Token: 0x060026C4 RID: 9924 RVA: 0x000B24D9 File Offset: 0x000B06D9
		internal SerialErrorReceivedEventArgs(SerialError eventCode)
		{
			this.errorType = eventCode;
		}

		// Token: 0x17000992 RID: 2450
		// (get) Token: 0x060026C5 RID: 9925 RVA: 0x000B24E8 File Offset: 0x000B06E8
		public SerialError EventType
		{
			get
			{
				return this.errorType;
			}
		}

		// Token: 0x04002108 RID: 8456
		private SerialError errorType;
	}
}
