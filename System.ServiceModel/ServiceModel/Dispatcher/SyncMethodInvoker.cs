using System;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Diagnostics;
using System.Security;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005A8 RID: 1448
	internal class SyncMethodInvoker : IOperationInvoker
	{
		// Token: 0x06003885 RID: 14469 RVA: 0x000D9B5E File Offset: 0x000D7D5E
		public SyncMethodInvoker(MethodInfo method)
		{
			if (method == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("method"));
			}
			this.method = method;
		}

		// Token: 0x06003886 RID: 14470 RVA: 0x000D9B8C File Offset: 0x000D7D8C
		public SyncMethodInvoker(Type type, string methodName)
		{
			if (type == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("type"));
			}
			if (methodName == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("methodName"));
			}
			this.type = type;
			this.methodName = methodName;
		}

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x06003887 RID: 14471 RVA: 0x000D9BE3 File Offset: 0x000D7DE3
		public bool IsSynchronous
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x06003888 RID: 14472 RVA: 0x000D9BE6 File Offset: 0x000D7DE6
		public MethodInfo Method
		{
			get
			{
				if (this.method == null)
				{
					this.method = this.type.GetMethod(this.methodName);
				}
				return this.method;
			}
		}

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x06003889 RID: 14473 RVA: 0x000D9C13 File Offset: 0x000D7E13
		public string MethodName
		{
			get
			{
				if (this.methodName == null)
				{
					this.methodName = this.method.Name;
				}
				return this.methodName;
			}
		}

		// Token: 0x0600388A RID: 14474 RVA: 0x000D9C34 File Offset: 0x000D7E34
		public object[] AllocateInputs()
		{
			this.EnsureIsInitialized();
			return EmptyArray.Allocate(this.inputParameterCount);
		}

		// Token: 0x0600388B RID: 14475 RVA: 0x000D9C48 File Offset: 0x000D7E48
		public object Invoke(object instance, object[] inputs, out object[] outputs)
		{
			this.EnsureIsInitialized();
			if (instance == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxNoServiceObject")));
			}
			if (inputs == null)
			{
				if (this.inputParameterCount > 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInputParametersToServiceNull", new object[]
					{
						this.inputParameterCount
					})));
				}
			}
			else if (inputs.Length != this.inputParameterCount)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("SFxInputParametersToServiceInvalid", new object[]
				{
					this.inputParameterCount,
					inputs.Length
				})));
			}
			outputs = EmptyArray.Allocate(this.outputParameterCount);
			long num = 0L;
			long num2 = 0L;
			long num3 = 0L;
			bool flag = false;
			bool flag2 = false;
			if (PerformanceCounters.PerformanceCountersEnabled)
			{
				PerformanceCounters.MethodCalled(this.MethodName);
				try
				{
					if (UnsafeNativeMethods.QueryPerformanceCounter(out num) == 0)
					{
						num = -1L;
					}
				}
				catch (SecurityException ex)
				{
					DiagnosticUtility.TraceHandledException(ex, TraceEventType.Warning);
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityException(SR.GetString("PartialTrustPerformanceCountersNotEnabled"), ex));
				}
			}
			EventTraceActivity eventTraceActivity = null;
			if (TD.OperationCompletedIsEnabled() || TD.OperationFaultedIsEnabled() || TD.OperationFailedIsEnabled())
			{
				num3 = DateTime.UtcNow.Ticks;
				OperationContext operationContext = OperationContext.Current;
				if (operationContext != null && operationContext.IncomingMessage != null)
				{
					eventTraceActivity = EventTraceActivityHelper.TryExtractActivity(operationContext.IncomingMessage);
				}
			}
			object result;
			try
			{
				ServiceModelActivity serviceModelActivity = null;
				IDisposable disposable = null;
				if (DiagnosticUtility.ShouldUseActivity)
				{
					serviceModelActivity = ServiceModelActivity.CreateBoundedActivity(true);
					disposable = serviceModelActivity;
				}
				else if (TraceUtility.MessageFlowTracingOnly)
				{
					Guid receivedActivityId = TraceUtility.GetReceivedActivityId(OperationContext.Current);
					if (receivedActivityId != Guid.Empty)
					{
						DiagnosticTraceBase.ActivityId = receivedActivityId;
					}
				}
				else if (TraceUtility.ShouldPropagateActivity)
				{
					Guid guid = ActivityIdHeader.ExtractActivityId(OperationContext.Current.IncomingMessage);
					if (guid != Guid.Empty)
					{
						disposable = Activity.CreateActivity(guid);
					}
				}
				using (disposable)
				{
					if (DiagnosticUtility.ShouldUseActivity)
					{
						ServiceModelActivity.Start(serviceModelActivity, SR.GetString("ActivityExecuteMethod", new object[]
						{
							this.method.DeclaringType.FullName,
							this.method.Name
						}), ActivityType.ExecuteUserCode);
					}
					if (TD.OperationInvokedIsEnabled())
					{
						TD.OperationInvoked(eventTraceActivity, this.MethodName, TraceUtility.GetCallerInfo(OperationContext.Current));
					}
					result = this.invokeDelegate(instance, inputs, outputs);
					flag = true;
				}
			}
			catch (FaultException)
			{
				flag2 = true;
				throw;
			}
			catch (SecurityException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(AuthorizationBehavior.CreateAccessDeniedFaultException());
			}
			finally
			{
				if (PerformanceCounters.PerformanceCountersEnabled)
				{
					long time = 0L;
					if (num >= 0L && UnsafeNativeMethods.QueryPerformanceCounter(out num2) != 0)
					{
						time = num2 - num;
					}
					if (flag)
					{
						PerformanceCounters.MethodReturnedSuccess(this.MethodName, time);
					}
					else if (flag2)
					{
						PerformanceCounters.MethodReturnedFault(this.MethodName, time);
					}
					else
					{
						PerformanceCounters.MethodReturnedError(this.MethodName, time);
					}
				}
				if (num3 != 0L)
				{
					if (flag)
					{
						if (TD.OperationCompletedIsEnabled())
						{
							TD.OperationCompleted(eventTraceActivity, this.methodName, TraceUtility.GetUtcBasedDurationForTrace(num3));
						}
					}
					else if (flag2)
					{
						if (TD.OperationFaultedIsEnabled())
						{
							TD.OperationFaulted(eventTraceActivity, this.methodName, TraceUtility.GetUtcBasedDurationForTrace(num3));
						}
					}
					else if (TD.OperationFailedIsEnabled())
					{
						TD.OperationFailed(eventTraceActivity, this.methodName, TraceUtility.GetUtcBasedDurationForTrace(num3));
					}
				}
			}
			return result;
		}

		// Token: 0x0600388C RID: 14476 RVA: 0x000D9FE8 File Offset: 0x000D81E8
		public IAsyncResult InvokeBegin(object instance, object[] inputs, AsyncCallback callback, object state)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600388D RID: 14477 RVA: 0x000D9FF9 File Offset: 0x000D81F9
		public object InvokeEnd(object instance, out object[] outputs, IAsyncResult result)
		{
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotImplementedException());
		}

		// Token: 0x0600388E RID: 14478 RVA: 0x000DA00A File Offset: 0x000D820A
		private void EnsureIsInitialized()
		{
			if (this.invokeDelegate == null)
			{
				this.EnsureIsInitializedCore();
			}
		}

		// Token: 0x0600388F RID: 14479 RVA: 0x000DA01C File Offset: 0x000D821C
		private void EnsureIsInitializedCore()
		{
			int num;
			int num2;
			InvokeDelegate invokeDelegate = new InvokerUtil().GenerateInvokeDelegate(this.Method, out num, out num2);
			this.outputParameterCount = num2;
			this.inputParameterCount = num;
			this.invokeDelegate = invokeDelegate;
		}

		// Token: 0x04002996 RID: 10646
		private Type type;

		// Token: 0x04002997 RID: 10647
		private string methodName;

		// Token: 0x04002998 RID: 10648
		private MethodInfo method;

		// Token: 0x04002999 RID: 10649
		private InvokeDelegate invokeDelegate;

		// Token: 0x0400299A RID: 10650
		private int inputParameterCount;

		// Token: 0x0400299B RID: 10651
		private int outputParameterCount;
	}
}
