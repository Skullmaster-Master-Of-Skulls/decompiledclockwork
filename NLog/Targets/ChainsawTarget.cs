using System;

namespace NLog.Targets
{
	// Token: 0x0200014A RID: 330
	[Target("Chainsaw")]
	public class ChainsawTarget : NLogViewerTarget
	{
		// Token: 0x06000BCE RID: 3022 RVA: 0x0001B7EB File Offset: 0x000199EB
		public ChainsawTarget()
		{
			base.IncludeNLogData = false;
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0001B7FA File Offset: 0x000199FA
		public ChainsawTarget(string name) : this()
		{
			base.Name = name;
		}
	}
}
