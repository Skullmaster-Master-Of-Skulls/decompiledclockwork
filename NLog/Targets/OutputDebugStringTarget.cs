using System;
using NLog.Internal;

namespace NLog.Targets
{
	// Token: 0x0200016B RID: 363
	[Target("OutputDebugString")]
	public sealed class OutputDebugStringTarget : TargetWithLayout
	{
		// Token: 0x06000DBA RID: 3514 RVA: 0x0002107C File Offset: 0x0001F27C
		public OutputDebugStringTarget()
		{
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00021084 File Offset: 0x0001F284
		public OutputDebugStringTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x00021093 File Offset: 0x0001F293
		protected override void Write(LogEventInfo logEvent)
		{
			NativeMethods.OutputDebugString(this.Layout.Render(logEvent));
		}
	}
}
