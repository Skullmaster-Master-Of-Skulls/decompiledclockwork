using System;

namespace NLog.Targets
{
	// Token: 0x02000157 RID: 343
	[Target("Debug")]
	public sealed class DebugTarget : TargetWithLayout
	{
		// Token: 0x06000C74 RID: 3188 RVA: 0x0001CEE2 File Offset: 0x0001B0E2
		public DebugTarget()
		{
			this.LastMessage = string.Empty;
			this.Counter = 0;
		}

		// Token: 0x06000C75 RID: 3189 RVA: 0x0001CEFC File Offset: 0x0001B0FC
		public DebugTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x17000212 RID: 530
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0001CF0B File Offset: 0x0001B10B
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x0001CF13 File Offset: 0x0001B113
		public int Counter { get; private set; }

		// Token: 0x17000213 RID: 531
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0001CF1C File Offset: 0x0001B11C
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0001CF24 File Offset: 0x0001B124
		public string LastMessage { get; private set; }

		// Token: 0x06000C7A RID: 3194 RVA: 0x0001CF2D File Offset: 0x0001B12D
		protected override void Write(LogEventInfo logEvent)
		{
			this.Counter++;
			this.LastMessage = this.Layout.Render(logEvent);
		}
	}
}
