using System;
using System.Collections.ObjectModel;
using System.Runtime;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Discovery
{
	// Token: 0x02000004 RID: 4
	internal class AnnouncementDispatcherAsyncResult : AsyncResult
	{
		// Token: 0x0600004C RID: 76 RVA: 0x00002AEC File Offset: 0x00000CEC
		public AnnouncementDispatcherAsyncResult(Collection<AnnouncementEndpoint> endpoints, Collection<EndpointDiscoveryMetadata> metadatas, DiscoveryMessageSequenceGenerator discoveryMessageSequenceGenerator, bool online, AsyncCallback callback, object state) : base(callback, state)
		{
			if (metadatas.Count == 0)
			{
				base.Complete(true);
				return;
			}
			bool flag = false;
			this.cancelled = false;
			this.thisLock = new object();
			this.innerResults = new AnnouncementSendsAsyncResult[endpoints.Count];
			this.onAnnouncementSendsCompletedCallback = Fx.ThunkCallback(new AsyncCallback(this.OnAnnouncementSendsCompleted));
			Collection<UniqueId> messageIds = AnnouncementDispatcherAsyncResult.AllocateMessageIds(metadatas.Count);
			try
			{
				Random random = new Random();
				for (int i = 0; i < this.innerResults.Length; i++)
				{
					AnnouncementClient announcementClient = new AnnouncementClient(endpoints[i]);
					announcementClient.MessageSequenceGenerator = discoveryMessageSequenceGenerator;
					this.innerResults[i] = new AnnouncementSendsAsyncResult(announcementClient, metadatas, messageIds, online, endpoints[i].MaxAnnouncementDelay, random, this.onAnnouncementSendsCompletedCallback, this);
				}
				flag = true;
			}
			finally
			{
				if (!flag)
				{
					this.Cancel();
				}
			}
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002BD0 File Offset: 0x00000DD0
		public void Start(TimeSpan timeout, bool canCompleteSynchronously)
		{
			if (base.IsCompleted || this.cancelled)
			{
				return;
			}
			bool completedSynchronously = canCompleteSynchronously;
			Exception ex = null;
			bool flag = false;
			try
			{
				for (int i = 0; i < this.innerResults.Length; i++)
				{
					this.innerResults[i].Start(timeout);
					if (this.innerResults[i].CompletedSynchronously)
					{
						AnnouncementSendsAsyncResult.End(this.innerResults[i]);
						flag = (Interlocked.Increment(ref this.completions) == this.innerResults.Length);
					}
					else
					{
						completedSynchronously = false;
					}
				}
			}
			catch (Exception ex2)
			{
				if (Fx.IsFatal(ex2))
				{
					throw;
				}
				ex = ex2;
			}
			if (ex != null)
			{
				this.CallCompleteOnce(completedSynchronously, ex);
				return;
			}
			if (flag)
			{
				this.CallCompleteOnce(completedSynchronously, null);
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002C88 File Offset: 0x00000E88
		private void OnAnnouncementSendsCompleted(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				Exception ex = null;
				try
				{
					AnnouncementSendsAsyncResult.End(result);
				}
				catch (Exception ex2)
				{
					if (Fx.IsFatal(ex2))
					{
						throw;
					}
					ex = ex2;
				}
				if (ex != null)
				{
					this.CallCompleteOnce(false, ex);
					return;
				}
				if (Interlocked.Increment(ref this.completions) == this.innerResults.Length)
				{
					this.CallCompleteOnce(false, null);
				}
			}
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public void Cancel()
		{
			if (!this.cancelled)
			{
				bool flag = false;
				object obj = this.thisLock;
				lock (obj)
				{
					if (!this.cancelled)
					{
						flag = true;
						this.cancelled = true;
					}
				}
				if (flag)
				{
					for (int i = 0; i < this.innerResults.Length; i++)
					{
						if (this.innerResults[i] != null)
						{
							this.innerResults[i].Cancel();
						}
					}
					this.CompleteOnCancel();
				}
			}
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002D78 File Offset: 0x00000F78
		private void CompleteOnCancel()
		{
			if (Interlocked.Increment(ref this.completesCounter) == 1)
			{
				base.Complete(false, new OperationCanceledException());
			}
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002D94 File Offset: 0x00000F94
		private void CallCompleteOnce(bool completedSynchronously, Exception e)
		{
			if (Interlocked.Increment(ref this.completesCounter) == 1)
			{
				if (e != null)
				{
					this.Cancel();
				}
				base.Complete(completedSynchronously, e);
			}
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00002DB5 File Offset: 0x00000FB5
		public static void End(IAsyncResult result)
		{
			AsyncResult.End<AnnouncementDispatcherAsyncResult>(result);
		}

		// Token: 0x06000053 RID: 83 RVA: 0x00002DC0 File Offset: 0x00000FC0
		private static Collection<UniqueId> AllocateMessageIds(int count)
		{
			Collection<UniqueId> collection = new Collection<UniqueId>();
			for (int i = 0; i < count; i++)
			{
				collection.Add(new UniqueId());
			}
			return collection;
		}

		// Token: 0x0400000F RID: 15
		private readonly AnnouncementSendsAsyncResult[] innerResults;

		// Token: 0x04000010 RID: 16
		private int completions;

		// Token: 0x04000011 RID: 17
		private AsyncCallback onAnnouncementSendsCompletedCallback;

		// Token: 0x04000012 RID: 18
		private int completesCounter;

		// Token: 0x04000013 RID: 19
		private bool cancelled;

		// Token: 0x04000014 RID: 20
		private object thisLock;
	}
}
