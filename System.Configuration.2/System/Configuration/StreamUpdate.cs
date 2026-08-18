using System;

namespace System.Configuration
{
	// Token: 0x0200008E RID: 142
	internal class StreamUpdate
	{
		// Token: 0x060005D3 RID: 1491 RVA: 0x0001C60A File Offset: 0x0001A80A
		internal StreamUpdate(string newStreamname)
		{
			this._newStreamname = newStreamname;
		}

		// Token: 0x170001DE RID: 478
		// (get) Token: 0x060005D4 RID: 1492 RVA: 0x0001C619 File Offset: 0x0001A819
		internal string NewStreamname
		{
			get
			{
				return this._newStreamname;
			}
		}

		// Token: 0x170001DF RID: 479
		// (get) Token: 0x060005D5 RID: 1493 RVA: 0x0001C621 File Offset: 0x0001A821
		// (set) Token: 0x060005D6 RID: 1494 RVA: 0x0001C629 File Offset: 0x0001A829
		internal bool WriteCompleted
		{
			get
			{
				return this._writeCompleted;
			}
			set
			{
				this._writeCompleted = value;
			}
		}

		// Token: 0x04000349 RID: 841
		private string _newStreamname;

		// Token: 0x0400034A RID: 842
		private bool _writeCompleted;
	}
}
