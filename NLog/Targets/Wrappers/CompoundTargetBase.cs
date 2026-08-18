using System;
using System.Collections.Generic;
using System.Text;
using NLog.Common;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000179 RID: 377
	public abstract class CompoundTargetBase : Target
	{
		// Token: 0x06000E2F RID: 3631 RVA: 0x000228B8 File Offset: 0x00020AB8
		protected CompoundTargetBase(params Target[] targets)
		{
			this.Targets = new List<Target>(targets);
		}

		// Token: 0x17000285 RID: 645
		// (get) Token: 0x06000E30 RID: 3632 RVA: 0x000228CC File Offset: 0x00020ACC
		// (set) Token: 0x06000E31 RID: 3633 RVA: 0x000228D4 File Offset: 0x00020AD4
		public IList<Target> Targets { get; private set; }

		// Token: 0x06000E32 RID: 3634 RVA: 0x000228E0 File Offset: 0x00020AE0
		public override string ToString()
		{
			string value = string.Empty;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(base.ToString());
			stringBuilder.Append("(");
			foreach (Target target in this.Targets)
			{
				stringBuilder.Append(value);
				stringBuilder.Append(target.ToString());
				value = ", ";
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06000E33 RID: 3635 RVA: 0x00022978 File Offset: 0x00020B78
		protected override void Write(LogEventInfo logEvent)
		{
			throw new NotSupportedException("This target must not be invoked in a synchronous way.");
		}

		// Token: 0x06000E34 RID: 3636 RVA: 0x0002298D File Offset: 0x00020B8D
		protected override void FlushAsync(AsyncContinuation asyncContinuation)
		{
			AsyncHelpers.ForEachItemInParallel<Target>(this.Targets, asyncContinuation, delegate(Target t, AsyncContinuation c)
			{
				t.Flush(c);
			});
		}
	}
}
