using System;
using NLog.Config;

namespace NLog.Targets
{
	// Token: 0x0200016E RID: 366
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class TargetAttribute : NameBaseAttribute
	{
		// Token: 0x06000DD6 RID: 3542 RVA: 0x0002164C File Offset: 0x0001F84C
		public TargetAttribute(string name) : base(name)
		{
		}

		// Token: 0x17000271 RID: 625
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x00021655 File Offset: 0x0001F855
		// (set) Token: 0x06000DD8 RID: 3544 RVA: 0x0002165D File Offset: 0x0001F85D
		public bool IsWrapper { get; set; }

		// Token: 0x17000272 RID: 626
		// (get) Token: 0x06000DD9 RID: 3545 RVA: 0x00021666 File Offset: 0x0001F866
		// (set) Token: 0x06000DDA RID: 3546 RVA: 0x0002166E File Offset: 0x0001F86E
		public bool IsCompound { get; set; }
	}
}
