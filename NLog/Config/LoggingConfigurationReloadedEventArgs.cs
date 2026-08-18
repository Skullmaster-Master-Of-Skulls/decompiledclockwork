using System;

namespace NLog.Config
{
	// Token: 0x02000050 RID: 80
	public class LoggingConfigurationReloadedEventArgs : EventArgs
	{
		// Token: 0x060001A1 RID: 417 RVA: 0x0000613B File Offset: 0x0000433B
		internal LoggingConfigurationReloadedEventArgs(bool succeeded, Exception exception)
		{
			this.Succeeded = succeeded;
			this.Exception = exception;
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x060001A2 RID: 418 RVA: 0x00006151 File Offset: 0x00004351
		// (set) Token: 0x060001A3 RID: 419 RVA: 0x00006159 File Offset: 0x00004359
		public bool Succeeded { get; private set; }

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060001A4 RID: 420 RVA: 0x00006162 File Offset: 0x00004362
		// (set) Token: 0x060001A5 RID: 421 RVA: 0x0000616A File Offset: 0x0000436A
		public Exception Exception { get; private set; }
	}
}
