using System;
using System.Collections.Generic;

namespace NLog.Targets
{
	// Token: 0x02000163 RID: 355
	[Target("Memory")]
	public sealed class MemoryTarget : TargetWithLayout
	{
		// Token: 0x06000D8E RID: 3470 RVA: 0x00020C86 File Offset: 0x0001EE86
		public MemoryTarget()
		{
			this.Logs = new List<string>();
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00020C99 File Offset: 0x0001EE99
		public MemoryTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x1700025E RID: 606
		// (get) Token: 0x06000D90 RID: 3472 RVA: 0x00020CA8 File Offset: 0x0001EEA8
		// (set) Token: 0x06000D91 RID: 3473 RVA: 0x00020CB0 File Offset: 0x0001EEB0
		public IList<string> Logs { get; private set; }

		// Token: 0x06000D92 RID: 3474 RVA: 0x00020CBC File Offset: 0x0001EEBC
		protected override void Write(LogEventInfo logEvent)
		{
			string item = this.Layout.Render(logEvent);
			this.Logs.Add(item);
		}
	}
}
