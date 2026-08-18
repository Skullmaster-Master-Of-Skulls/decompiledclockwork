using System;
using NLog.Common;
using NLog.Config;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000174 RID: 372
	public abstract class WrapperTargetBase : Target
	{
		// Token: 0x1700027C RID: 636
		// (get) Token: 0x06000E00 RID: 3584 RVA: 0x00022078 File Offset: 0x00020278
		// (set) Token: 0x06000E01 RID: 3585 RVA: 0x00022080 File Offset: 0x00020280
		[RequiredParameter]
		public Target WrappedTarget { get; set; }

		// Token: 0x06000E02 RID: 3586 RVA: 0x0002208C File Offset: 0x0002028C
		public override string ToString()
		{
			return string.Concat(new object[]
			{
				base.ToString(),
				"(",
				this.WrappedTarget,
				")"
			});
		}

		// Token: 0x06000E03 RID: 3587 RVA: 0x000220C8 File Offset: 0x000202C8
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			this.WrappedTarget.Flush(asyncContinuation);
		}

		// Token: 0x06000E04 RID: 3588 RVA: 0x000220D6 File Offset: 0x000202D6
		protected sealed override void Write(LogEventInfo logEvent)
		{
			throw new NotSupportedException("This target must not be invoked in a synchronous way.");
		}
	}
}
