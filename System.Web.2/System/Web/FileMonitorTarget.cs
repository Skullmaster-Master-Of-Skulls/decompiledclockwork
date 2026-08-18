using System;

namespace System.Web
{
	// Token: 0x0200006E RID: 110
	internal sealed class FileMonitorTarget
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x0000A7FF File Offset: 0x000089FF
		internal FileMonitorTarget(FileChangeEventHandler callback, string alias)
		{
			this.Callback = callback;
			this.Alias = alias;
			this.UtcStartMonitoring = DateTime.UtcNow;
			this._refs = 1;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0000A827 File Offset: 0x00008A27
		internal int AddRef()
		{
			this._refs++;
			return this._refs;
		}

		// Token: 0x06000681 RID: 1665 RVA: 0x0000A83D File Offset: 0x00008A3D
		internal int Release()
		{
			this._refs--;
			return this._refs;
		}

		// Token: 0x040001FB RID: 507
		internal readonly FileChangeEventHandler Callback;

		// Token: 0x040001FC RID: 508
		internal readonly string Alias;

		// Token: 0x040001FD RID: 509
		internal readonly DateTime UtcStartMonitoring;

		// Token: 0x040001FE RID: 510
		private int _refs;
	}
}
