using System;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.InteropServices;
using System.ServiceModel.Diagnostics;
using System.Threading;

namespace System.ServiceModel.ComIntegration
{
	// Token: 0x02000213 RID: 531
	internal class ComPlusSynchronizationContext : SynchronizationContext
	{
		// Token: 0x06001036 RID: 4150 RVA: 0x0003A1E4 File Offset: 0x000383E4
		public ComPlusSynchronizationContext(IServiceActivity activity, bool postSynchronous)
		{
			this.activity = activity;
			this.postSynchronous = postSynchronous;
		}

		// Token: 0x06001037 RID: 4151 RVA: 0x0003A1FA File Offset: 0x000383FA
		public override void Send(SendOrPostCallback d, object state)
		{
		}

		// Token: 0x06001038 RID: 4152 RVA: 0x0003A1FC File Offset: 0x000383FC
		public override void Post(SendOrPostCallback d, object state)
		{
			ComPlusActivityTrace.Trace(TraceEventType.Verbose, 327701, "TraceCodeComIntegrationEnteringActivity");
			ComPlusSynchronizationContext.ServiceCall pIServiceCall = new ComPlusSynchronizationContext.ServiceCall(d, state);
			if (this.postSynchronous)
			{
				this.activity.SynchronousCall(pIServiceCall);
			}
			else
			{
				this.activity.AsynchronousCall(pIServiceCall);
			}
			ComPlusActivityTrace.Trace(TraceEventType.Verbose, 327703, "TraceCodeComIntegrationLeftActivity");
		}

		// Token: 0x06001039 RID: 4153 RVA: 0x0003A255 File Offset: 0x00038455
		public void Dispose()
		{
			while (Marshal.ReleaseComObject(this.activity) > 0)
			{
			}
		}

		// Token: 0x04001862 RID: 6242
		private IServiceActivity activity;

		// Token: 0x04001863 RID: 6243
		private bool postSynchronous;

		// Token: 0x02000B0D RID: 2829
		private class ServiceCall : IServiceCall
		{
			// Token: 0x06006F6B RID: 28523 RVA: 0x0019DB83 File Offset: 0x0019BD83
			public ServiceCall(SendOrPostCallback callback, object state)
			{
				this.callback = callback;
				this.state = state;
			}

			// Token: 0x06006F6C RID: 28524 RVA: 0x0019DB9C File Offset: 0x0019BD9C
			public void OnCall()
			{
				ServiceModelActivity serviceModelActivity = null;
				try
				{
					Guid empty = Guid.Empty;
					if (DiagnosticUtility.ShouldUseActivity)
					{
						IComThreadingInfo comThreadingInfo = (IComThreadingInfo)SafeNativeMethods.CoGetObjectContext(ComPlusActivityTrace.IID_IComThreadingInfo);
						if (comThreadingInfo != null)
						{
							comThreadingInfo.GetCurrentLogicalThreadId(out empty);
							serviceModelActivity = ServiceModelActivity.CreateBoundedActivity(empty);
						}
						ServiceModelActivity.Start(serviceModelActivity, SR.GetString("TransferringToComplus", new object[]
						{
							empty.ToString()
						}), ActivityType.TransferToComPlus);
					}
					ComPlusActivityTrace.Trace(TraceEventType.Verbose, 327702, "TraceCodeComIntegrationExecutingCall");
					this.callback(this.state);
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					DiagnosticUtility.InvokeFinalHandler(exception);
				}
				finally
				{
					if (serviceModelActivity != null)
					{
						serviceModelActivity.Dispose();
						serviceModelActivity = null;
					}
				}
			}

			// Token: 0x04003F9A RID: 16282
			private SendOrPostCallback callback;

			// Token: 0x04003F9B RID: 16283
			private object state;
		}
	}
}
