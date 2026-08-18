using System;
using System.Collections.Generic;
using NLog.Common;
using NLog.Conditions;
using NLog.Config;

namespace NLog.Targets.Wrappers
{
	// Token: 0x02000180 RID: 384
	[Target("PostFilteringWrapper", IsWrapper = true)]
	public class PostFilteringTargetWrapper : WrapperTargetBase
	{
		// Token: 0x06000E63 RID: 3683 RVA: 0x0002304E File Offset: 0x0002124E
		public PostFilteringTargetWrapper() : this(null)
		{
			this.Rules = new List<FilteringRule>();
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x00023062 File Offset: 0x00021262
		public PostFilteringTargetWrapper(Target wrappedTarget)
		{
			this.Rules = new List<FilteringRule>();
			base.WrappedTarget = wrappedTarget;
		}

		// Token: 0x06000E65 RID: 3685 RVA: 0x0002307C File Offset: 0x0002127C
		public PostFilteringTargetWrapper(string name, Target wrappedTarget) : this(wrappedTarget)
		{
			base.Name = name;
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0002308C File Offset: 0x0002128C
		// (set) Token: 0x06000E67 RID: 3687 RVA: 0x00023094 File Offset: 0x00021294
		public ConditionExpression DefaultFilter { get; set; }

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000E68 RID: 3688 RVA: 0x0002309D File Offset: 0x0002129D
		// (set) Token: 0x06000E69 RID: 3689 RVA: 0x000230A5 File Offset: 0x000212A5
		[ArrayParameter(typeof(FilteringRule), "when")]
		public IList<FilteringRule> Rules { get; private set; }

		// Token: 0x06000E6A RID: 3690 RVA: 0x000230B0 File Offset: 0x000212B0
		protected override void Write(AsyncLogEventInfo[] logEvents)
		{
			ConditionExpression conditionExpression = null;
			InternalLogger.Trace("Running {0} on {1} events", new object[]
			{
				this,
				logEvents.Length
			});
			for (int i = 0; i < logEvents.Length; i++)
			{
				foreach (FilteringRule filteringRule in this.Rules)
				{
					object obj = filteringRule.Exists.Evaluate(logEvents[i].LogEvent);
					if (PostFilteringTargetWrapper.boxedTrue.Equals(obj))
					{
						InternalLogger.Trace("Rule matched: {0}", new object[]
						{
							filteringRule.Exists
						});
						conditionExpression = filteringRule.Filter;
						break;
					}
				}
				if (conditionExpression != null)
				{
					break;
				}
			}
			if (conditionExpression == null)
			{
				conditionExpression = this.DefaultFilter;
			}
			if (conditionExpression == null)
			{
				base.WrappedTarget.WriteAsyncLogEvents(logEvents);
				return;
			}
			InternalLogger.Trace("Filter to apply: {0}", new object[]
			{
				conditionExpression
			});
			List<AsyncLogEventInfo> list = new List<AsyncLogEventInfo>();
			for (int j = 0; j < logEvents.Length; j++)
			{
				object obj2 = conditionExpression.Evaluate(logEvents[j].LogEvent);
				if (PostFilteringTargetWrapper.boxedTrue.Equals(obj2))
				{
					list.Add(logEvents[j]);
				}
				else
				{
					logEvents[j].Continuation(null);
				}
			}
			InternalLogger.Trace("After filtering: {0} events.", new object[]
			{
				list.Count
			});
			if (list.Count > 0)
			{
				InternalLogger.Trace("Sending to {0}", new object[]
				{
					base.WrappedTarget
				});
				base.WrappedTarget.WriteAsyncLogEvents(list.ToArray());
			}
		}

		// Token: 0x04000414 RID: 1044
		private static object boxedTrue = true;
	}
}
