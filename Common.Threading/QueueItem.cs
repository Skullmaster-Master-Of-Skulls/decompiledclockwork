using System;
using System.ComponentModel;

namespace TechnoPro.Common.Threading
{
	// Token: 0x02000003 RID: 3
	public class QueueItem
	{
		// Token: 0x06000005 RID: 5 RVA: 0x0000215B File Offset: 0x0000035B
		public QueueItem(BackgroundWorker backgroundWorker, object argument)
		{
			this.BackgroundWorker = backgroundWorker;
			this.Argument = argument;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000006 RID: 6 RVA: 0x00002171 File Offset: 0x00000371
		// (set) Token: 0x06000007 RID: 7 RVA: 0x00002179 File Offset: 0x00000379
		public object Argument { get; private set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000008 RID: 8 RVA: 0x00002182 File Offset: 0x00000382
		// (set) Token: 0x06000009 RID: 9 RVA: 0x0000218A File Offset: 0x0000038A
		public BackgroundWorker BackgroundWorker { get; private set; }

		// Token: 0x0600000A RID: 10 RVA: 0x00002193 File Offset: 0x00000393
		public void RunWorkerAsync()
		{
			this.BackgroundWorker.RunWorkerAsync(this.Argument);
		}
	}
}
