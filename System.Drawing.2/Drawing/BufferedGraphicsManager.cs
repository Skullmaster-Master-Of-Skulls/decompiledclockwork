using System;
using System.Runtime.ConstrainedExecution;

namespace System.Drawing
{
	// Token: 0x02000016 RID: 22
	public sealed class BufferedGraphicsManager
	{
		// Token: 0x06000112 RID: 274 RVA: 0x00003800 File Offset: 0x00001A00
		private BufferedGraphicsManager()
		{
		}

		// Token: 0x06000113 RID: 275 RVA: 0x00006E2F File Offset: 0x0000502F
		static BufferedGraphicsManager()
		{
			AppDomain.CurrentDomain.ProcessExit += BufferedGraphicsManager.OnShutdown;
			AppDomain.CurrentDomain.DomainUnload += BufferedGraphicsManager.OnShutdown;
			BufferedGraphicsManager.bufferedGraphicsContext = new BufferedGraphicsContext();
		}

		// Token: 0x1700009A RID: 154
		// (get) Token: 0x06000114 RID: 276 RVA: 0x00006E67 File Offset: 0x00005067
		public static BufferedGraphicsContext Current
		{
			get
			{
				return BufferedGraphicsManager.bufferedGraphicsContext;
			}
		}

		// Token: 0x06000115 RID: 277 RVA: 0x00006E6E File Offset: 0x0000506E
		[PrePrepareMethod]
		private static void OnShutdown(object sender, EventArgs e)
		{
			BufferedGraphicsManager.Current.Invalidate();
		}

		// Token: 0x04000144 RID: 324
		private static BufferedGraphicsContext bufferedGraphicsContext;
	}
}
