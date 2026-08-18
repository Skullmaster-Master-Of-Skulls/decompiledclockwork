using System;
using NLog.Common;
using NLog.Conditions;
using NLog.Config;

namespace NLog.Targets.Wrappers
{
	// Token: 0x0200017C RID: 380
	[Target("FilteringWrapper", IsWrapper = true)]
	public class FilteringTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x00022CB2 File Offset: 0x00020EB2
		public FilteringTargetWrapper()
		{
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00022CBA File Offset: 0x00020EBA
		public FilteringTargetWrapper(string name, Target wrappedTarget, ConditionExpression condition) : this(wrappedTarget, condition)
		{
			base.Name = name;
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00022CCB File Offset: 0x00020ECB
		public FilteringTargetWrapper(Target wrappedTarget, ConditionExpression condition)
		{
			base.WrappedTarget = wrappedTarget;
			this.Condition = condition;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x06000E45 RID: 3653 RVA: 0x00022CE1 File Offset: 0x00020EE1
		// (set) Token: 0x06000E46 RID: 3654 RVA: 0x00022CE9 File Offset: 0x00020EE9
		[RequiredParameter]
		public ConditionExpression Condition { get; set; }

		// Token: 0x06000E47 RID: 3655 RVA: 0x00022CF4 File Offset: 0x00020EF4
		protected override void Write(AsyncLogEventInfo logEvent)
		{
			object obj = this.Condition.Evaluate(logEvent.LogEvent);
			if (FilteringTargetWrapper.boxedBooleanTrue.Equals(obj))
			{
				base.WrappedTarget.WriteAsyncLogEvent(logEvent);
				return;
			}
			logEvent.Continuation(null);
		}

		// Token: 0x04000406 RID: 1030
		private static readonly object boxedBooleanTrue = true;
	}
}
