using System;
using System.Diagnostics;

namespace System.Data.SqlClient
{
	// Token: 0x020001C2 RID: 450
	internal class SqlConnectionTimeoutPhaseDuration
	{
		// Token: 0x06001B90 RID: 7056 RVA: 0x000C0F70 File Offset: 0x000C0370
		internal void StartCapture()
		{
			this.swDuration.Start();
		}

		// Token: 0x06001B91 RID: 7057 RVA: 0x000C0F88 File Offset: 0x000C0388
		internal void StopCapture()
		{
			if (this.swDuration.IsRunning)
			{
				this.swDuration.Stop();
			}
		}

		// Token: 0x06001B92 RID: 7058 RVA: 0x000C0FB0 File Offset: 0x000C03B0
		internal long GetMilliSecondDuration()
		{
			return this.swDuration.ElapsedMilliseconds;
		}

		// Token: 0x04001000 RID: 4096
		private Stopwatch swDuration = new Stopwatch();
	}
}
