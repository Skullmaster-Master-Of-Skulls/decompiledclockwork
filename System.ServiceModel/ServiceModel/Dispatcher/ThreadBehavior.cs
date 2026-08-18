using System;
using System.Runtime;
using System.ServiceModel.Activation;
using System.Threading;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x0200054E RID: 1358
	internal class ThreadBehavior
	{
		// Token: 0x060033B6 RID: 13238 RVA: 0x000C75A1 File Offset: 0x000C57A1
		internal ThreadBehavior(DispatchRuntime dispatch)
		{
			this.context = dispatch.SynchronizationContext;
		}

		// Token: 0x17000C33 RID: 3123
		// (get) Token: 0x060033B7 RID: 13239 RVA: 0x000C75B5 File Offset: 0x000C57B5
		private SendOrPostCallback ThreadAffinityStartCallbackDelegate
		{
			get
			{
				if (this.threadAffinityStartCallback == null)
				{
					this.threadAffinityStartCallback = new SendOrPostCallback(this.SynchronizationContextStartCallback);
				}
				return this.threadAffinityStartCallback;
			}
		}

		// Token: 0x17000C34 RID: 3124
		// (get) Token: 0x060033B8 RID: 13240 RVA: 0x000C75D7 File Offset: 0x000C57D7
		private SendOrPostCallback ThreadAffinityEndCallbackDelegate
		{
			get
			{
				if (this.threadAffinityEndCallback == null)
				{
					this.threadAffinityEndCallback = new SendOrPostCallback(this.SynchronizationContextEndCallback);
				}
				return this.threadAffinityEndCallback;
			}
		}

		// Token: 0x17000C35 RID: 3125
		// (get) Token: 0x060033B9 RID: 13241 RVA: 0x000C75F9 File Offset: 0x000C57F9
		private static Action<object> CleanThreadCallbackDelegate
		{
			get
			{
				if (ThreadBehavior.cleanThreadCallback == null)
				{
					ThreadBehavior.cleanThreadCallback = new Action<object>(ThreadBehavior.CleanThreadCallback);
				}
				return ThreadBehavior.cleanThreadCallback;
			}
		}

		// Token: 0x060033BA RID: 13242 RVA: 0x000C7618 File Offset: 0x000C5818
		internal void BindThread(ref MessageRpc rpc)
		{
			this.BindCore(ref rpc, true);
		}

		// Token: 0x060033BB RID: 13243 RVA: 0x000C7622 File Offset: 0x000C5822
		internal void BindEndThread(ref MessageRpc rpc)
		{
			this.BindCore(ref rpc, false);
		}

		// Token: 0x060033BC RID: 13244 RVA: 0x000C762C File Offset: 0x000C582C
		private void BindCore(ref MessageRpc rpc, bool startOperation)
		{
			SynchronizationContext syncContext = this.GetSyncContext(rpc.InstanceContext);
			if (syncContext == null)
			{
				if (rpc.SwitchedThreads)
				{
					IResumeMessageRpc state = rpc.Pause();
					ActionItem.Schedule(ThreadBehavior.CleanThreadCallbackDelegate, state);
				}
				return;
			}
			IResumeMessageRpc state2 = rpc.Pause();
			if (startOperation)
			{
				syncContext.OperationStarted();
				syncContext.Post(this.ThreadAffinityStartCallbackDelegate, state2);
				return;
			}
			syncContext.Post(this.ThreadAffinityEndCallbackDelegate, state2);
		}

		// Token: 0x060033BD RID: 13245 RVA: 0x000C7690 File Offset: 0x000C5890
		private SynchronizationContext GetSyncContext(InstanceContext instanceContext)
		{
			return instanceContext.SynchronizationContext ?? this.context;
		}

		// Token: 0x060033BE RID: 13246 RVA: 0x000C76AF File Offset: 0x000C58AF
		private void SynchronizationContextStartCallback(object state)
		{
			this.ResumeProcessing((IResumeMessageRpc)state);
		}

		// Token: 0x060033BF RID: 13247 RVA: 0x000C76C0 File Offset: 0x000C58C0
		private void SynchronizationContextEndCallback(object state)
		{
			IResumeMessageRpc resumeMessageRpc = (IResumeMessageRpc)state;
			this.ResumeProcessing(resumeMessageRpc);
			SynchronizationContext syncContext = this.GetSyncContext(resumeMessageRpc.GetMessageInstanceContext());
			syncContext.OperationCompleted();
		}

		// Token: 0x060033C0 RID: 13248 RVA: 0x000C76F0 File Offset: 0x000C58F0
		private void ResumeProcessing(IResumeMessageRpc resume)
		{
			bool flag;
			resume.Resume(out flag);
			if (flag)
			{
				string @string = SR.GetString("SFxMultipleCallbackFromSynchronizationContext", new object[]
				{
					this.context.GetType().ToString()
				});
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(@string));
			}
		}

		// Token: 0x060033C1 RID: 13249 RVA: 0x000C7740 File Offset: 0x000C5940
		private static void CleanThreadCallback(object state)
		{
			bool flag;
			((IResumeMessageRpc)state).Resume(out flag);
		}

		// Token: 0x060033C2 RID: 13250 RVA: 0x000C775C File Offset: 0x000C595C
		internal static SynchronizationContext GetCurrentSynchronizationContext()
		{
			if (AspNetEnvironment.IsApplicationDomainHosted())
			{
				return null;
			}
			return SynchronizationContext.Current;
		}

		// Token: 0x040027A1 RID: 10145
		private SendOrPostCallback threadAffinityStartCallback;

		// Token: 0x040027A2 RID: 10146
		private SendOrPostCallback threadAffinityEndCallback;

		// Token: 0x040027A3 RID: 10147
		private static Action<object> cleanThreadCallback;

		// Token: 0x040027A4 RID: 10148
		private readonly SynchronizationContext context;
	}
}
