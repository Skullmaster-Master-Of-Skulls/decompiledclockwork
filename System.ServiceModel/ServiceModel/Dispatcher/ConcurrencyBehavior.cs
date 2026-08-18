using System;
using System.Runtime;
using System.Threading;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x02000541 RID: 1345
	internal class ConcurrencyBehavior
	{
		// Token: 0x060032E1 RID: 13025 RVA: 0x000C4CA6 File Offset: 0x000C2EA6
		internal ConcurrencyBehavior(DispatchRuntime runtime)
		{
			this.concurrencyMode = runtime.ConcurrencyMode;
			this.enforceOrderedReceive = runtime.EnsureOrderedDispatch;
			this.supportsTransactedBatch = ConcurrencyBehavior.SupportsTransactedBatch(runtime.ChannelDispatcher);
		}

		// Token: 0x060032E2 RID: 13026 RVA: 0x000C4CD7 File Offset: 0x000C2ED7
		private static bool SupportsTransactedBatch(ChannelDispatcher channelDispatcher)
		{
			return channelDispatcher.IsTransactedReceive && channelDispatcher.MaxTransactedBatchSize > 0;
		}

		// Token: 0x060032E3 RID: 13027 RVA: 0x000C4CEC File Offset: 0x000C2EEC
		internal bool IsConcurrent(ref MessageRpc rpc)
		{
			return ConcurrencyBehavior.IsConcurrent(this.concurrencyMode, this.enforceOrderedReceive, rpc.Channel.HasSession, this.supportsTransactedBatch);
		}

		// Token: 0x060032E4 RID: 13028 RVA: 0x000C4D10 File Offset: 0x000C2F10
		internal static bool IsConcurrent(ConcurrencyMode concurrencyMode, bool ensureOrderedDispatch, bool hasSession, bool supportsTransactedBatch)
		{
			return !supportsTransactedBatch && (concurrencyMode != ConcurrencyMode.Single || (!hasSession && !ensureOrderedDispatch));
		}

		// Token: 0x060032E5 RID: 13029 RVA: 0x000C4D28 File Offset: 0x000C2F28
		internal static bool IsConcurrent(ChannelDispatcher runtime, bool hasSession)
		{
			bool flag = true;
			if (ConcurrencyBehavior.SupportsTransactedBatch(runtime))
			{
				return false;
			}
			foreach (EndpointDispatcher endpointDispatcher in runtime.Endpoints)
			{
				if (endpointDispatcher.DispatchRuntime.EnsureOrderedDispatch)
				{
					return false;
				}
				if (endpointDispatcher.DispatchRuntime.ConcurrencyMode != ConcurrencyMode.Single)
				{
					flag = false;
				}
			}
			return !flag || !hasSession;
		}

		// Token: 0x060032E6 RID: 13030 RVA: 0x000C4DA8 File Offset: 0x000C2FA8
		internal void LockInstance(ref MessageRpc rpc)
		{
			if (this.concurrencyMode != ConcurrencyMode.Multiple)
			{
				ConcurrencyInstanceContextFacet concurrency = rpc.InstanceContext.Concurrency;
				object thisLock = rpc.InstanceContext.ThisLock;
				lock (thisLock)
				{
					if (!concurrency.Locked)
					{
						concurrency.Locked = true;
					}
					else
					{
						ConcurrencyBehavior.MessageRpcWaiter waiter = new ConcurrencyBehavior.MessageRpcWaiter(rpc.Pause());
						concurrency.EnqueueNewMessage(waiter);
					}
				}
				if (this.concurrencyMode == ConcurrencyMode.Reentrant)
				{
					rpc.OperationContext.IsServiceReentrant = true;
				}
			}
		}

		// Token: 0x060032E7 RID: 13031 RVA: 0x000C4E38 File Offset: 0x000C3038
		internal void UnlockInstance(ref MessageRpc rpc)
		{
			if (this.concurrencyMode != ConcurrencyMode.Multiple)
			{
				ConcurrencyBehavior.UnlockInstance(rpc.InstanceContext);
			}
		}

		// Token: 0x060032E8 RID: 13032 RVA: 0x000C4E4E File Offset: 0x000C304E
		internal static void UnlockInstanceBeforeCallout(OperationContext operationContext)
		{
			if (operationContext != null && operationContext.IsServiceReentrant)
			{
				ConcurrencyBehavior.UnlockInstance(operationContext.InstanceContext);
			}
		}

		// Token: 0x060032E9 RID: 13033 RVA: 0x000C4E68 File Offset: 0x000C3068
		private static void UnlockInstance(InstanceContext instanceContext)
		{
			ConcurrencyInstanceContextFacet concurrency = instanceContext.Concurrency;
			object thisLock = instanceContext.ThisLock;
			lock (thisLock)
			{
				if (concurrency.HasWaiters)
				{
					ConcurrencyBehavior.IWaiter waiter = concurrency.DequeueWaiter();
					waiter.Signal();
				}
				else
				{
					concurrency.Locked = false;
				}
			}
		}

		// Token: 0x060032EA RID: 13034 RVA: 0x000C4EC8 File Offset: 0x000C30C8
		internal static void LockInstanceAfterCallout(OperationContext operationContext)
		{
			if (operationContext != null)
			{
				InstanceContext instanceContext = operationContext.InstanceContext;
				if (operationContext.IsServiceReentrant)
				{
					ConcurrencyInstanceContextFacet concurrency = instanceContext.Concurrency;
					ConcurrencyBehavior.ThreadWaiter threadWaiter = null;
					object thisLock = instanceContext.ThisLock;
					lock (thisLock)
					{
						if (!concurrency.Locked)
						{
							concurrency.Locked = true;
						}
						else
						{
							threadWaiter = new ConcurrencyBehavior.ThreadWaiter();
							concurrency.EnqueueCalloutMessage(threadWaiter);
						}
					}
					if (threadWaiter != null)
					{
						threadWaiter.Wait();
					}
				}
			}
		}

		// Token: 0x0400274C RID: 10060
		private ConcurrencyMode concurrencyMode;

		// Token: 0x0400274D RID: 10061
		private bool enforceOrderedReceive;

		// Token: 0x0400274E RID: 10062
		private bool supportsTransactedBatch;

		// Token: 0x02000C67 RID: 3175
		internal interface IWaiter
		{
			// Token: 0x060077DE RID: 30686
			void Signal();
		}

		// Token: 0x02000C68 RID: 3176
		private class MessageRpcWaiter : ConcurrencyBehavior.IWaiter
		{
			// Token: 0x060077DF RID: 30687 RVA: 0x001C09AC File Offset: 0x001BEBAC
			internal MessageRpcWaiter(IResumeMessageRpc resume)
			{
				this.resume = resume;
			}

			// Token: 0x060077E0 RID: 30688 RVA: 0x001C09BC File Offset: 0x001BEBBC
			void ConcurrencyBehavior.IWaiter.Signal()
			{
				try
				{
					bool flag;
					this.resume.Resume(out flag);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(ex);
				}
			}

			// Token: 0x04004478 RID: 17528
			private IResumeMessageRpc resume;
		}

		// Token: 0x02000C69 RID: 3177
		private class ThreadWaiter : ConcurrencyBehavior.IWaiter
		{
			// Token: 0x060077E1 RID: 30689 RVA: 0x001C0A04 File Offset: 0x001BEC04
			void ConcurrencyBehavior.IWaiter.Signal()
			{
				this.wait.Set();
			}

			// Token: 0x060077E2 RID: 30690 RVA: 0x001C0A12 File Offset: 0x001BEC12
			internal void Wait()
			{
				this.wait.WaitOne();
				this.wait.Close();
			}

			// Token: 0x04004479 RID: 17529
			private ManualResetEvent wait = new ManualResetEvent(false);
		}
	}
}
