using System;

namespace System.IO.Ports
{
	// Token: 0x02000413 RID: 1043
	public class SerialDataReceivedEventArgs : EventArgs
	{
		// Token: 0x06002723 RID: 10019 RVA: 0x000B4010 File Offset: 0x000B2210
		internal SerialDataReceivedEventArgs(SerialData eventCode)
		{
			this.receiveType = eventCode;
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x06002724 RID: 10020 RVA: 0x000B401F File Offset: 0x000B221F
		public SerialData EventType
		{
			get
			{
				return this.receiveType;
			}
		}

		// Token: 0x04002144 RID: 8516
		internal SerialData receiveType;
	}
}
