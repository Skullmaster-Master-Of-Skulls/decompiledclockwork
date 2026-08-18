using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Diagnostics;
using System.Security;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200054F RID: 1359
	internal class AsyncMethodInvoker : IOperationInvoker
	{
		// Token: 0x060033C3 RID: 13251 RVA: 0x000C776C File Offset: 0x000C596C
		public AsyncMethodInvoker(MethodInfo beginMethod, MethodInfo endMethod)
		{
			if (beginMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("beginMethod"));
			}
			if (endMethod == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("endMethod"));
			}
			this.beginMethod = beginMethod;
			this.endMethod = endMethod;
		}

		// Token: 0x17000C36 RID: 3126
		// (get) Token: 0x060033C4 RID: 13252 RVA: 0x000C77C9 File Offset: 0x000C59C9
		public MethodInfo BeginMethod
		{
			get
			{
				return this.beginMethod;
			}
		}

		// Token: 0x17000C37 RID: 3127
		// (get) Token: 0x060033C5 RID: 13253 RVA: 0x000C77D1 File Offset: 0x000C59D1
		public MethodInfo EndMethod
		{
			get
			{
				return this.endMethod;
			}
		}

		// Token: 0x17000C38 RID: 3128
		// (get) Token: 0x060033C6 RID: 13254 RVA: 0x000C77D9 File Offset: 0x000C59D9
		public bool IsSynchronous
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060033C7 RID: 13255 RVA: 0x000C77DC File Offset: 0x000C59DC
		public object[] AllocateInputs()
		{
			return EmptyArray.Allocate(this.InputParameterCount);
		}

		// Token: 0x060033C8 RID: 13256 RVA: 0x000C77E9 File Offset: 0x000C59E9
		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x060033C9 RID: 13257 RVA: 0x000C77FC File Offset: 0x000C59FC
		internal static void CreateActivityInfo(ref ServiceModelActivity activity, ref Activity boundActivity)
		{
			if (DiagnosticUtility.ShouldUseActivity)
			{
				activity = ServiceModelActivity.CreateAsyncActivity();
				TraceUtility.UpdateAsyncOperationContextWithActivity(activity);
				boundActivity = ServiceModelActivity.BoundOperation(activity, true);
				return;
			}
			if (TraceUtility.MessageFlowTracingOnly)
			{
				Guid receivedActivityId = TraceUtility.GetReceivedActivityId(OperationContext.Current);
				if (receivedActivityId != Guid.Empty)
				{
					DiagnosticTraceBase.ActivityId = receivedActivityId;
					return;
				}
			}
			else if (TraceUtility.ShouldPropagateActivity)
			{
				Guid guid = ActivityIdHeader.ExtractActivityId(OperationContext.Current.IncomingMessage);
				if (guid != Guid.Empty)
				{
					boundActivity = Activity.CreateActivity(guid);
				}
				TraceUtility.UpdateAsyncOperationContextWithActivity(guid);
			}
		}

		// Token: 0x060033CA RID: 13258 RVA: 0x000C7888 File Offset: 0x000C5A88
		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			if (instance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoServiceObject")));
			}
			if (inputs == null)
			{
				if (this.InputParameterCount > 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInputParametersToServiceNull", new object[]
					{
						this.InputParameterCount
					})));
				}
			}
			else if (inputs.Length != this.InputParameterCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInputParametersToServiceInvalid", new object[]
				{
					this.InputParameterCount,
					inputs.Length
				})));
			}
			AsyncMethodInvoker.StartOperationInvokePerformanceCounters(this.beginMethod.Name.Substring("Begin".Length));
			bool flag = true;
			bool flag2 = false;
			ServiceModelActivity activity = null;
			IAsyncResult result;
			try
			{
				Activity activity2 = null;
				AsyncMethodInvoker.CreateActivityInfo(ref activity, ref activity2);
				AsyncMethodInvoker.StartOperationInvokeTrace(this.beginMethod.Name);
				using (activity2)
				{
					if (DiagnosticUtility.ShouldUseActivity)
					{
						string @string;
						if (this.endMethod == null)
						{
							@string = SR.GetString("ActivityExecuteMethod", new object[]
							{
								this.beginMethod.DeclaringType.FullName,
								this.beginMethod.Name
							});
						}
						else
						{
							@string = SR.GetString("ActivityExecuteAsyncMethod", new object[]
							{
								this.beginMethod.DeclaringType.FullName,
								this.beginMethod.Name,
								this.endMethod.DeclaringType.FullName,
								this.endMethod.Name
							});
						}
						ServiceModelActivity.Start(activity, @string, ActivityType.ExecuteUserCode);
					}
					result = this.InvokeBeginDelegate(instance, inputs, callback, state);
					flag = false;
				}
			}
			catch (SecurityException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(AuthorizationBehavior.CreateAccessDeniedFaultException());
			}
			catch (Exception ex)
			{
				TraceUtility.TraceUserCodeException(ex, this.beginMethod);
				if (ex is FaultException)
				{
					flag2 = true;
					flag = false;
				}
				throw;
			}
			finally
			{
				ServiceModelActivity.Stop(activity);
				if (flag || flag2)
				{
					AsyncMethodInvoker.StopOperationInvokeTrace(flag, flag2, this.EndMethod.Name);
					AsyncMethodInvoker.StopOperationInvokePerformanceCounters(flag, flag2, this.endMethod.Name.Substring("End".Length));
				}
			}
			return result;
		}

		// Token: 0x060033CB RID: 13259 RVA: 0x000C7B24 File Offset: 0x000C5D24
		internal static void GetActivityInfo(ref ServiceModelActivity activity, ref Activity boundOperation)
		{
			if (TraceUtility.MessageFlowTracingOnly)
			{
				if (OperationContext.Current != null)
				{
					Guid receivedActivityId = TraceUtility.GetReceivedActivityId(OperationContext.Current);
					if (receivedActivityId != Guid.Empty)
					{
						DiagnosticTraceBase.ActivityId = receivedActivityId;
						return;
					}
				}
			}
			else if (DiagnosticUtility.ShouldUseActivity || TraceUtility.ShouldPropagateActivity)
			{
				object obj = TraceUtility.ExtractAsyncOperationContextActivity();
				if (obj != null)
				{
					if (DiagnosticUtility.ShouldUseActivity)
					{
						activity = (obj as ServiceModelActivity);
						boundOperation = ServiceModelActivity.BoundOperation(activity, true);
						return;
					}
					if (TraceUtility.ShouldPropagateActivity && obj is Guid)
					{
						Guid activityId = (Guid)obj;
						boundOperation = Activity.CreateActivity(activityId);
					}
				}
			}
		}

		// Token: 0x060033CC RID: 13260 RVA: 0x000C7BB0 File Offset: 0x000C5DB0
		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			if (instance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoServiceObject")));
			}
			outputs = EmptyArray.Allocate(this.OutputParameterCount);
			bool callFailed = true;
			bool callFaulted = false;
			ServiceModelActivity activity = null;
			object result2;
			try
			{
				Activity activity2 = null;
				AsyncMethodInvoker.GetActivityInfo(ref activity, ref activity2);
				using (activity2)
				{
					result2 = this.InvokeEndDelegate(instance, outputs, result);
					callFailed = false;
				}
			}
			catch (SecurityException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(AuthorizationBehavior.CreateAccessDeniedFaultException());
			}
			catch (FaultException)
			{
				callFaulted = true;
				callFailed = false;
				throw;
			}
			finally
			{
				ServiceModelActivity.Stop(activity);
				AsyncMethodInvoker.StopOperationInvokeTrace(callFailed, callFaulted, this.endMethod.Name);
				AsyncMethodInvoker.StopOperationInvokePerformanceCounters(callFailed, callFaulted, this.endMethod.Name.Substring("End".Length));
			}
			return result2;
		}

		// Token: 0x060033CD RID: 13261 RVA: 0x000C7CB0 File Offset: 0x000C5EB0
		internal static void StartOperationInvokeTrace(string methodName)
		{
			if (TD.OperationInvokedIsEnabled())
			{
				OperationContext operationContext = OperationContext.Current;
				EventTraceActivity eventTraceActivity = null;
				if (operationContext != null && operationContext.IncomingMessage != null)
				{
					eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(operationContext.IncomingMessage);
				}
				if (TD.OperationInvokedIsEnabled())
				{
					TD.OperationInvoked(eventTraceActivity, methodName, TraceUtility.GetCallerInfo(OperationContext.Current));
				}
				if (TD.OperationCompletedIsEnabled() || TD.OperationFaultedIsEnabled() || TD.OperationFailedIsEnabled())
				{
					TraceUtility.UpdateAsyncOperationContextWithStartTime(eventTraceActivity, DateTime.UtcNow.Ticks);
				}
			}
		}

		// Token: 0x060033CE RID: 13262 RVA: 0x000C7D24 File Offset: 0x000C5F24
		internal static void StopOperationInvokeTrace(bool callFailed, bool callFaulted, string methodName)
		{
			if (!TD.OperationCompletedIsEnabled() && !TD.OperationFaultedIsEnabled() && !TD.OperationFailedIsEnabled())
			{
				return;
			}
			EventTraceActivity eventTraceActivity;
			long startTicks;
			TraceUtility.ExtractAsyncOperationStartTime(out eventTraceActivity, out startTicks);
			long utcBasedDurationForTrace = TraceUtility.GetUtcBasedDurationForTrace(startTicks);
			if (callFailed)
			{
				if (TD.OperationFailedIsEnabled())
				{
					TD.OperationFailed(eventTraceActivity, methodName, utcBasedDurationForTrace);
					return;
				}
			}
			else if (callFaulted)
			{
				if (TD.OperationFaultedIsEnabled())
				{
					TD.OperationFaulted(eventTraceActivity, methodName, utcBasedDurationForTrace);
					return;
				}
			}
			else if (TD.OperationCompletedIsEnabled())
			{
				TD.OperationCompleted(eventTraceActivity, methodName, utcBasedDurationForTrace);
			}
		}

		// Token: 0x060033CF RID: 13263 RVA: 0x000C7D8C File Offset: 0x000C5F8C
		internal static void StartOperationInvokePerformanceCounters(string methodName)
		{
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				PerformanceCounters.MethodCalled(methodName);
			}
		}

		// Token: 0x060033D0 RID: 13264 RVA: 0x000C7D9B File Offset: 0x000C5F9B
		internal static void StopOperationInvokePerformanceCounters(bool callFailed, bool callFaulted, string methodName)
		{
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				if (callFailed)
				{
					PerformanceCounters.MethodReturnedError(methodName);
					return;
				}
				if (callFaulted)
				{
					PerformanceCounters.MethodReturnedFault(methodName);
					return;
				}
				PerformanceCounters.MethodReturnedSuccess(methodName);
			}
		}

		// Token: 0x17000C39 RID: 3129
		// (get) Token: 0x060033D1 RID: 13265 RVA: 0x000C7DBE File Offset: 0x000C5FBE
		private InvokeBeginDelegate InvokeBeginDelegate
		{
			get
			{
				this.EnsureIsInitialized();
				return this.invokeBeginDelegate;
			}
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060033D2 RID: 13266 RVA: 0x000C7DCC File Offset: 0x000C5FCC
		private InvokeEndDelegate InvokeEndDelegate
		{
			get
			{
				this.EnsureIsInitialized();
				return this.invokeEndDelegate;
			}
		}

		// Token: 0x17000C3B RID: 3131
		// (get) Token: 0x060033D3 RID: 13267 RVA: 0x000C7DDA File Offset: 0x000C5FDA
		private int InputParameterCount
		{
			get
			{
				this.EnsureIsInitialized();
				return this.inputParameterCount;
			}
		}

		// Token: 0x17000C3C RID: 3132
		// (get) Token: 0x060033D4 RID: 13268 RVA: 0x000C7DE8 File Offset: 0x000C5FE8
		private int OutputParameterCount
		{
			get
			{
				this.EnsureIsInitialized();
				return this.outputParameterCount;
			}
		}

		// Token: 0x060033D5 RID: 13269 RVA: 0x000C7DF8 File Offset: 0x000C5FF8
		private void EnsureIsInitialized()
		{
			if (this.invokeBeginDelegate == null)
			{
				int num;
				InvokeBeginDelegate invokeBeginDelegate = new InvokerUtil().GenerateInvokeBeginDelegate(this.beginMethod, out num);
				this.inputParameterCount = num;
				int num2;
				InvokeEndDelegate invokeEndDelegate = new InvokerUtil().GenerateInvokeEndDelegate(this.endMethod, out num2);
				this.outputParameterCount = num2;
				this.invokeEndDelegate = invokeEndDelegate;
				this.invokeBeginDelegate = invokeBeginDelegate;
			}
		}

		// Token: 0x040027A5 RID: 10149
		private MethodInfo beginMethod;

		// Token: 0x040027A6 RID: 10150
		private MethodInfo endMethod;

		// Token: 0x040027A7 RID: 10151
		private InvokeBeginDelegate invokeBeginDelegate;

		// Token: 0x040027A8 RID: 10152
		private InvokeEndDelegate invokeEndDelegate;

		// Token: 0x040027A9 RID: 10153
		private int inputParameterCount;

		// Token: 0x040027AA RID: 10154
		private int outputParameterCount;
	}
}
