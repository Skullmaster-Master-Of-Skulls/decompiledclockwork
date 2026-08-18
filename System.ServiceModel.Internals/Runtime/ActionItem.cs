using System;
using System.Diagnostics;
using System.Runtime.Diagnostics;
using System.Security;
using System.Threading;

namespace System.Runtime
{
	// Token: 0x02000004 RID: 4
	internal abstract class ActionItem
	{
		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000023DE File Offset: 0x000005DE
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000023E6 File Offset: 0x000005E6
		public bool LowPriority
		{
			get
			{
				return this.lowPriority;
			}
			protected set
			{
				this.lowPriority = value;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023EF File Offset: 0x000005EF
		public static void Schedule(Action<object> callback, object state)
		{
			ActionItem.Schedule(callback, state, false);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023F9 File Offset: 0x000005F9
		[SecuritySafeCritical]
		public static void Schedule(Action<object> callback, object state, bool lowPriority)
		{
			if (PartialTrustHelpers.ShouldFlowSecurityContext || WaitCallbackActionItem.ShouldUseActivity || Fx.Trace.IsEnd2EndActivityTracingEnabled)
			{
				new ActionItem.DefaultActionItem(callback, state, lowPriority).Schedule();
				return;
			}
			ActionItem.ScheduleCallback(callback, state, lowPriority);
		}

		// Token: 0x06000013 RID: 19
		[SecurityCritical]
		protected abstract void Invoke();

		// Token: 0x06000014 RID: 20 RVA: 0x0000242C File Offset: 0x0000062C
		[SecurityCritical]
		protected void Schedule()
		{
			if (this.isScheduled)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.ActionItemIsAlreadyScheduled));
			}
			this.isScheduled = true;
			if (PartialTrustHelpers.ShouldFlowSecurityContext)
			{
				this.context = PartialTrustHelpers.CaptureSecurityContextNoIdentityFlow();
			}
			if (this.context != null)
			{
				this.ScheduleCallback(ActionItem.CallbackHelper.InvokeWithContextCallback);
				return;
			}
			this.ScheduleCallback(ActionItem.CallbackHelper.InvokeWithoutContextCallback);
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002490 File Offset: 0x00000690
		[SecurityCritical]
		protected void ScheduleWithContext(SecurityContext context)
		{
			if (context == null)
			{
				throw Fx.Exception.ArgumentNull("context");
			}
			if (this.isScheduled)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.ActionItemIsAlreadyScheduled));
			}
			this.isScheduled = true;
			this.context = context.CreateCopy();
			this.ScheduleCallback(ActionItem.CallbackHelper.InvokeWithContextCallback);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024EB File Offset: 0x000006EB
		[SecurityCritical]
		protected void ScheduleWithoutContext()
		{
			if (this.isScheduled)
			{
				throw Fx.Exception.AsError(new InvalidOperationException(InternalSR.ActionItemIsAlreadyScheduled));
			}
			this.isScheduled = true;
			this.ScheduleCallback(ActionItem.CallbackHelper.InvokeWithoutContextCallback);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x0000251C File Offset: 0x0000071C
		[SecurityCritical]
		private static void ScheduleCallback(Action<object> callback, object state, bool lowPriority)
		{
			if (lowPriority)
			{
				IOThreadScheduler.ScheduleCallbackLowPriNoFlow(callback, state);
				return;
			}
			IOThreadScheduler.ScheduleCallbackNoFlow(callback, state);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002530 File Offset: 0x00000730
		[SecurityCritical]
		private SecurityContext ExtractContext()
		{
			SecurityContext result = this.context;
			this.context = null;
			return result;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000254C File Offset: 0x0000074C
		[SecurityCritical]
		private void ScheduleCallback(Action<object> callback)
		{
			ActionItem.ScheduleCallback(callback, this, this.lowPriority);
		}

		// Token: 0x04000006 RID: 6
		[SecurityCritical]
		private SecurityContext context;

		// Token: 0x04000007 RID: 7
		private bool isScheduled;

		// Token: 0x04000008 RID: 8
		private bool lowPriority;

		// Token: 0x02000058 RID: 88
		[SecurityCritical]
		private static class CallbackHelper
		{
			// Token: 0x17000093 RID: 147
			// (get) Token: 0x0600036E RID: 878 RVA: 0x0001171D File Offset: 0x0000F91D
			public static Action<object> InvokeWithContextCallback
			{
				get
				{
					if (ActionItem.CallbackHelper.invokeWithContextCallback == null)
					{
						ActionItem.CallbackHelper.invokeWithContextCallback = new Action<object>(ActionItem.CallbackHelper.InvokeWithContext);
					}
					return ActionItem.CallbackHelper.invokeWithContextCallback;
				}
			}

			// Token: 0x17000094 RID: 148
			// (get) Token: 0x0600036F RID: 879 RVA: 0x0001173C File Offset: 0x0000F93C
			public static Action<object> InvokeWithoutContextCallback
			{
				get
				{
					if (ActionItem.CallbackHelper.invokeWithoutContextCallback == null)
					{
						ActionItem.CallbackHelper.invokeWithoutContextCallback = new Action<object>(ActionItem.CallbackHelper.InvokeWithoutContext);
					}
					return ActionItem.CallbackHelper.invokeWithoutContextCallback;
				}
			}

			// Token: 0x17000095 RID: 149
			// (get) Token: 0x06000370 RID: 880 RVA: 0x0001175B File Offset: 0x0000F95B
			public static ContextCallback OnContextAppliedCallback
			{
				get
				{
					if (ActionItem.CallbackHelper.onContextAppliedCallback == null)
					{
						ActionItem.CallbackHelper.onContextAppliedCallback = new ContextCallback(ActionItem.CallbackHelper.OnContextApplied);
					}
					return ActionItem.CallbackHelper.onContextAppliedCallback;
				}
			}

			// Token: 0x06000371 RID: 881 RVA: 0x0001177C File Offset: 0x0000F97C
			private static void InvokeWithContext(object state)
			{
				SecurityContext securityContext = ((ActionItem)state).ExtractContext();
				SecurityContext.Run(securityContext, ActionItem.CallbackHelper.OnContextAppliedCallback, state);
			}

			// Token: 0x06000372 RID: 882 RVA: 0x000117A1 File Offset: 0x0000F9A1
			private static void InvokeWithoutContext(object state)
			{
				((ActionItem)state).Invoke();
				((ActionItem)state).isScheduled = false;
			}

			// Token: 0x06000373 RID: 883 RVA: 0x000117A1 File Offset: 0x0000F9A1
			private static void OnContextApplied(object o)
			{
				((ActionItem)o).Invoke();
				((ActionItem)o).isScheduled = false;
			}

			// Token: 0x040001C7 RID: 455
			private static Action<object> invokeWithContextCallback;

			// Token: 0x040001C8 RID: 456
			private static Action<object> invokeWithoutContextCallback;

			// Token: 0x040001C9 RID: 457
			private static ContextCallback onContextAppliedCallback;
		}

		// Token: 0x02000059 RID: 89
		private class DefaultActionItem : ActionItem
		{
			// Token: 0x06000374 RID: 884 RVA: 0x000117BC File Offset: 0x0000F9BC
			[SecuritySafeCritical]
			public DefaultActionItem(Action<object> callback, object state, bool isLowPriority)
			{
				base.LowPriority = isLowPriority;
				this.callback = callback;
				this.state = state;
				if (WaitCallbackActionItem.ShouldUseActivity)
				{
					this.flowLegacyActivityId = true;
					this.activityId = DiagnosticTraceBase.ActivityId;
				}
				if (Fx.Trace.IsEnd2EndActivityTracingEnabled)
				{
					this.eventTraceActivity = EventTraceActivity.GetFromThreadOrCreate(false);
					if (TraceCore.ActionItemScheduledIsEnabled(Fx.Trace))
					{
						TraceCore.ActionItemScheduled(Fx.Trace, this.eventTraceActivity);
					}
				}
			}

			// Token: 0x06000375 RID: 885 RVA: 0x00011831 File Offset: 0x0000FA31
			[SecurityCritical]
			protected override void Invoke()
			{
				if (this.flowLegacyActivityId || Fx.Trace.IsEnd2EndActivityTracingEnabled)
				{
					this.TraceAndInvoke();
					return;
				}
				this.callback(this.state);
			}

			// Token: 0x06000376 RID: 886 RVA: 0x00011860 File Offset: 0x0000FA60
			[SecurityCritical]
			private void TraceAndInvoke()
			{
				if (this.flowLegacyActivityId)
				{
					Guid guid = DiagnosticTraceBase.ActivityId;
					try
					{
						DiagnosticTraceBase.ActivityId = this.activityId;
						this.callback(this.state);
						return;
					}
					finally
					{
						DiagnosticTraceBase.ActivityId = guid;
					}
				}
				Guid empty = Guid.Empty;
				bool flag = false;
				try
				{
					if (this.eventTraceActivity != null)
					{
						empty = Trace.CorrelationManager.ActivityId;
						flag = true;
						Trace.CorrelationManager.ActivityId = this.eventTraceActivity.ActivityId;
						if (TraceCore.ActionItemCallbackInvokedIsEnabled(Fx.Trace))
						{
							TraceCore.ActionItemCallbackInvoked(Fx.Trace, this.eventTraceActivity);
						}
					}
					this.callback(this.state);
				}
				finally
				{
					if (flag)
					{
						Trace.CorrelationManager.ActivityId = empty;
					}
				}
			}

			// Token: 0x040001CA RID: 458
			[SecurityCritical]
			private Action<object> callback;

			// Token: 0x040001CB RID: 459
			[SecurityCritical]
			private object state;

			// Token: 0x040001CC RID: 460
			private bool flowLegacyActivityId;

			// Token: 0x040001CD RID: 461
			private Guid activityId;

			// Token: 0x040001CE RID: 462
			private EventTraceActivity eventTraceActivity;
		}
	}
}
