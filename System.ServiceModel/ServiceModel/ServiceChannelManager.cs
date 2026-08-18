using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using System.ServiceModel.Channels;
using System.Threading;

namespace System.ServiceModel
{
	// Token: 0x02000116 RID: 278
	internal class ServiceChannelManager : LifetimeManager
	{
		// Token: 0x06000713 RID: 1811 RVA: 0x0001DCA5 File Offset: 0x0001BEA5
		public ServiceChannelManager(InstanceContext instanceContext) : this(instanceContext, null)
		{
		}

		// Token: 0x06000714 RID: 1812 RVA: 0x0001DCAF File Offset: 0x0001BEAF
		public ServiceChannelManager(InstanceContext instanceContext, InstanceContextEmptyCallback emptyCallback) : base(instanceContext.ThisLock)
		{
			this.instanceContext = instanceContext;
			this.emptyCallback = emptyCallback;
		}

		// Token: 0x170001D2 RID: 466
		// (get) Token: 0x06000715 RID: 1813 RVA: 0x0001DCCB File Offset: 0x0001BECB
		public int ActivityCount
		{
			get
			{
				return this.activityCount;
			}
		}

		// Token: 0x170001D3 RID: 467
		// (get) Token: 0x06000716 RID: 1814 RVA: 0x0001DCD3 File Offset: 0x0001BED3
		public ICollection<IChannel> IncomingChannels
		{
			get
			{
				this.EnsureIncomingChannelCollection();
				return this.incomingChannels;
			}
		}

		// Token: 0x170001D4 RID: 468
		// (get) Token: 0x06000717 RID: 1815 RVA: 0x0001DCE4 File Offset: 0x0001BEE4
		public ICollection<IChannel> OutgoingChannels
		{
			get
			{
				if (this.outgoingChannels == null)
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.outgoingChannels == null)
						{
							this.outgoingChannels = new ServiceChannelManager.ChannelCollection(this, base.ThisLock);
						}
					}
				}
				return this.outgoingChannels;
			}
		}

		// Token: 0x170001D5 RID: 469
		// (get) Token: 0x06000718 RID: 1816 RVA: 0x0001DD48 File Offset: 0x0001BF48
		public bool IsBusy
		{
			get
			{
				if (this.ActivityCount > 0)
				{
					return true;
				}
				if (base.BusyCount > 0)
				{
					return true;
				}
				ICollection<IChannel> collection = this.outgoingChannels;
				return collection != null && collection.Count > 0;
			}
		}

		// Token: 0x06000719 RID: 1817 RVA: 0x0001DD84 File Offset: 0x0001BF84
		public void AddIncomingChannel(IChannel channel)
		{
			bool flag = false;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (base.State == LifetimeState.Opened)
				{
					if (this.firstIncomingChannel == null)
					{
						if (this.incomingChannels == null)
						{
							this.firstIncomingChannel = channel;
							this.ChannelAdded(channel);
						}
						else
						{
							if (this.incomingChannels.Contains(channel))
							{
								return;
							}
							this.incomingChannels.Add(channel);
						}
					}
					else
					{
						this.EnsureIncomingChannelCollection();
						if (this.incomingChannels.Contains(channel))
						{
							return;
						}
						this.incomingChannels.Add(channel);
					}
					flag = true;
				}
			}
			if (!flag)
			{
				channel.Abort();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
			}
		}

		// Token: 0x0600071A RID: 1818 RVA: 0x0001DE50 File Offset: 0x0001C050
		public IAsyncResult BeginCloseInput(TimeSpan timeout, AsyncCallback callback, object state)
		{
			CloseCommunicationAsyncResult closeCommunicationAsyncResult = null;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.activityCount > 0)
				{
					closeCommunicationAsyncResult = new CloseCommunicationAsyncResult(timeout, callback, state, base.ThisLock);
					ICommunicationWaiter communicationWaiter = this.activityWaiter;
					this.activityWaiter = closeCommunicationAsyncResult;
					Interlocked.Increment(ref this.activityWaiterCount);
				}
			}
			if (closeCommunicationAsyncResult != null)
			{
				return closeCommunicationAsyncResult;
			}
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x0600071B RID: 1819 RVA: 0x0001DECC File Offset: 0x0001C0CC
		private void ChannelAdded(IChannel channel)
		{
			base.IncrementBusyCount();
			channel.Closed += this.OnChannelClosed;
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x0001DEE6 File Offset: 0x0001C0E6
		private void ChannelRemoved(IChannel channel)
		{
			channel.Closed -= this.OnChannelClosed;
			base.DecrementBusyCount();
		}

		// Token: 0x0600071D RID: 1821 RVA: 0x0001DF00 File Offset: 0x0001C100
		public void CloseInput(TimeSpan timeout)
		{
			SyncCommunicationWaiter syncCommunicationWaiter = null;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.activityCount > 0)
				{
					syncCommunicationWaiter = new SyncCommunicationWaiter(base.ThisLock);
					ICommunicationWaiter communicationWaiter = this.activityWaiter;
					this.activityWaiter = syncCommunicationWaiter;
					Interlocked.Increment(ref this.activityWaiterCount);
				}
			}
			if (syncCommunicationWaiter == null)
			{
				return;
			}
			CommunicationWaitResult communicationWaitResult = syncCommunicationWaiter.Wait(timeout, false);
			if (Interlocked.Decrement(ref this.activityWaiterCount) == 0)
			{
				syncCommunicationWaiter.Dispose();
				this.activityWaiter = null;
			}
			if (communicationWaitResult == CommunicationWaitResult.Expired)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException(SR.GetString("SfxCloseTimedOutWaitingForDispatchToComplete")));
			}
			if (communicationWaitResult != CommunicationWaitResult.Aborted)
			{
				return;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
		}

		// Token: 0x0600071E RID: 1822 RVA: 0x0001DFD0 File Offset: 0x0001C1D0
		public void DecrementActivityCount()
		{
			ICommunicationWaiter communicationWaiter = null;
			bool flag = false;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				int num = this.activityCount;
				int num2 = this.activityCount - 1;
				this.activityCount = num2;
				if (num2 == 0)
				{
					if (this.activityWaiter != null)
					{
						communicationWaiter = this.activityWaiter;
						Interlocked.Increment(ref this.activityWaiterCount);
					}
					if (base.BusyCount == 0)
					{
						flag = true;
					}
				}
			}
			if (communicationWaiter != null)
			{
				communicationWaiter.Signal();
				if (Interlocked.Decrement(ref this.activityWaiterCount) == 0)
				{
					communicationWaiter.Dispose();
					this.activityWaiter = null;
				}
			}
			if (flag && base.State == LifetimeState.Opened)
			{
				this.OnEmpty();
			}
		}

		// Token: 0x0600071F RID: 1823 RVA: 0x0001E088 File Offset: 0x0001C288
		public void EndCloseInput(IAsyncResult result)
		{
			if (result is CloseCommunicationAsyncResult)
			{
				CloseCommunicationAsyncResult.End(result);
				if (Interlocked.Decrement(ref this.activityWaiterCount) == 0)
				{
					this.activityWaiter.Dispose();
					this.activityWaiter = null;
					return;
				}
			}
			else
			{
				CompletedAsyncResult.End(result);
			}
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x0001E0C0 File Offset: 0x0001C2C0
		private void EnsureIncomingChannelCollection()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.incomingChannels == null)
				{
					this.incomingChannels = new ServiceChannelManager.ChannelCollection(this, base.ThisLock);
					if (this.firstIncomingChannel != null)
					{
						this.incomingChannels.Add(this.firstIncomingChannel);
						this.ChannelRemoved(this.firstIncomingChannel);
						this.firstIncomingChannel = null;
					}
				}
			}
		}

		// Token: 0x06000721 RID: 1825 RVA: 0x0001E140 File Offset: 0x0001C340
		public void IncrementActivityCount()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (base.State == LifetimeState.Closed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
				}
				this.activityCount++;
			}
		}

		// Token: 0x06000722 RID: 1826 RVA: 0x0001E1AC File Offset: 0x0001C3AC
		protected override void IncrementBusyCount()
		{
			base.IncrementBusyCount();
		}

		// Token: 0x06000723 RID: 1827 RVA: 0x0001E1B4 File Offset: 0x0001C3B4
		protected override void OnAbort()
		{
			IChannel[] array = this.SnapshotChannels();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Abort();
			}
			ICommunicationWaiter communicationWaiter = null;
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.activityWaiter != null)
				{
					communicationWaiter = this.activityWaiter;
					Interlocked.Increment(ref this.activityWaiterCount);
				}
			}
			if (communicationWaiter != null)
			{
				communicationWaiter.Signal();
				if (Interlocked.Decrement(ref this.activityWaiterCount) == 0)
				{
					communicationWaiter.Dispose();
					this.activityWaiter = null;
				}
			}
			base.OnAbort();
		}

		// Token: 0x06000724 RID: 1828 RVA: 0x0001E254 File Offset: 0x0001C454
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new ChainedAsyncResult(timeout, callback, state, new ChainedBeginHandler(this.BeginCloseInput), new ChainedEndHandler(this.EndCloseInput), new ChainedBeginHandler(this.OnBeginCloseContinue), new ChainedEndHandler(this.OnEndCloseContinue));
		}

		// Token: 0x06000725 RID: 1829 RVA: 0x0001E290 File Offset: 0x0001C490
		private IAsyncResult OnBeginCloseContinue(TimeSpan timeout, AsyncCallback callback, object state)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			return base.OnBeginClose(timeoutHelper.RemainingTime(), callback, state);
		}

		// Token: 0x06000726 RID: 1830 RVA: 0x0001E2B4 File Offset: 0x0001C4B4
		protected override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.CloseInput(timeoutHelper.RemainingTime());
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06000727 RID: 1831 RVA: 0x0001E2E3 File Offset: 0x0001C4E3
		protected override void OnEndClose(IAsyncResult result)
		{
			ChainedAsyncResult.End(result);
		}

		// Token: 0x06000728 RID: 1832 RVA: 0x0001E2EB File Offset: 0x0001C4EB
		private void OnEndCloseContinue(IAsyncResult result)
		{
			base.OnEndClose(result);
		}

		// Token: 0x06000729 RID: 1833 RVA: 0x0001E2F4 File Offset: 0x0001C4F4
		protected override void OnEmpty()
		{
			if (this.emptyCallback != null)
			{
				this.emptyCallback(this.instanceContext);
			}
		}

		// Token: 0x0600072A RID: 1834 RVA: 0x0001E30F File Offset: 0x0001C50F
		private void OnChannelClosed(object sender, EventArgs args)
		{
			this.RemoveChannel((IChannel)sender);
		}

		// Token: 0x0600072B RID: 1835 RVA: 0x0001E320 File Offset: 0x0001C520
		public bool RemoveChannel(IChannel channel)
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				if (this.firstIncomingChannel == channel)
				{
					this.firstIncomingChannel = null;
					this.ChannelRemoved(channel);
					return true;
				}
				if (this.incomingChannels != null && this.incomingChannels.Contains(channel))
				{
					this.incomingChannels.Remove(channel);
					return true;
				}
				if (this.outgoingChannels != null && this.outgoingChannels.Contains(channel))
				{
					this.outgoingChannels.Remove(channel);
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600072C RID: 1836 RVA: 0x0001E3C8 File Offset: 0x0001C5C8
		public IChannel[] SnapshotChannels()
		{
			object thisLock = base.ThisLock;
			lock (thisLock)
			{
				int num = (this.outgoingChannels != null) ? this.outgoingChannels.Count : 0;
				if (this.firstIncomingChannel != null)
				{
					IChannel[] array = new IChannel[1 + num];
					array[0] = this.firstIncomingChannel;
					if (num > 0)
					{
						this.outgoingChannels.CopyTo(array, 1);
					}
					return array;
				}
				if (this.incomingChannels != null)
				{
					IChannel[] array2 = new IChannel[this.incomingChannels.Count + num];
					this.incomingChannels.CopyTo(array2, 0);
					if (num > 0)
					{
						this.outgoingChannels.CopyTo(array2, this.incomingChannels.Count);
					}
					return array2;
				}
				if (num > 0)
				{
					IChannel[] array3 = new IChannel[num];
					this.outgoingChannels.CopyTo(array3, 0);
					return array3;
				}
			}
			return EmptyArray<IChannel>.Allocate(0);
		}

		// Token: 0x04000AB2 RID: 2738
		private int activityCount;

		// Token: 0x04000AB3 RID: 2739
		private ICommunicationWaiter activityWaiter;

		// Token: 0x04000AB4 RID: 2740
		private int activityWaiterCount;

		// Token: 0x04000AB5 RID: 2741
		private InstanceContextEmptyCallback emptyCallback;

		// Token: 0x04000AB6 RID: 2742
		private IChannel firstIncomingChannel;

		// Token: 0x04000AB7 RID: 2743
		private ServiceChannelManager.ChannelCollection incomingChannels;

		// Token: 0x04000AB8 RID: 2744
		private ServiceChannelManager.ChannelCollection outgoingChannels;

		// Token: 0x04000AB9 RID: 2745
		private InstanceContext instanceContext;

		// Token: 0x02000AEC RID: 2796
		private class ChannelCollection : ICollection<IChannel>, IEnumerable<IChannel>, IEnumerable
		{
			// Token: 0x170019EB RID: 6635
			// (get) Token: 0x06006F10 RID: 28432 RVA: 0x0019CF20 File Offset: 0x0019B120
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170019EC RID: 6636
			// (get) Token: 0x06006F11 RID: 28433 RVA: 0x0019CF24 File Offset: 0x0019B124
			public int Count
			{
				get
				{
					object obj = this.syncRoot;
					int count;
					lock (obj)
					{
						count = this.hashSet.Count;
					}
					return count;
				}
			}

			// Token: 0x06006F12 RID: 28434 RVA: 0x0019CF6C File Offset: 0x0019B16C
			public ChannelCollection(ServiceChannelManager channelManager, object syncRoot)
			{
				if (syncRoot == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("syncRoot"));
				}
				this.channelManager = channelManager;
				this.syncRoot = syncRoot;
			}

			// Token: 0x06006F13 RID: 28435 RVA: 0x0019CFA8 File Offset: 0x0019B1A8
			public void Add(IChannel channel)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					if (this.hashSet.Add(channel))
					{
						this.channelManager.ChannelAdded(channel);
					}
				}
			}

			// Token: 0x06006F14 RID: 28436 RVA: 0x0019CFFC File Offset: 0x0019B1FC
			public void Clear()
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					foreach (IChannel channel in this.hashSet)
					{
						this.channelManager.ChannelRemoved(channel);
					}
					this.hashSet.Clear();
				}
			}

			// Token: 0x06006F15 RID: 28437 RVA: 0x0019D088 File Offset: 0x0019B288
			public bool Contains(IChannel channel)
			{
				object obj = this.syncRoot;
				bool result;
				lock (obj)
				{
					if (channel != null)
					{
						result = this.hashSet.Contains(channel);
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x06006F16 RID: 28438 RVA: 0x0019D0D8 File Offset: 0x0019B2D8
			public void CopyTo(IChannel[] array, int arrayIndex)
			{
				object obj = this.syncRoot;
				lock (obj)
				{
					this.hashSet.CopyTo(array, arrayIndex);
				}
			}

			// Token: 0x06006F17 RID: 28439 RVA: 0x0019D120 File Offset: 0x0019B320
			public bool Remove(IChannel channel)
			{
				object obj = this.syncRoot;
				bool result;
				lock (obj)
				{
					bool flag2 = false;
					if (channel != null)
					{
						flag2 = this.hashSet.Remove(channel);
						if (flag2)
						{
							this.channelManager.ChannelRemoved(channel);
						}
					}
					result = flag2;
				}
				return result;
			}

			// Token: 0x06006F18 RID: 28440 RVA: 0x0019D180 File Offset: 0x0019B380
			IEnumerator IEnumerable.GetEnumerator()
			{
				object obj = this.syncRoot;
				IEnumerator result;
				lock (obj)
				{
					result = this.hashSet.GetEnumerator();
				}
				return result;
			}

			// Token: 0x06006F19 RID: 28441 RVA: 0x0019D1CC File Offset: 0x0019B3CC
			IEnumerator<IChannel> IEnumerable<IChannel>.GetEnumerator()
			{
				object obj = this.syncRoot;
				IEnumerator<IChannel> result;
				lock (obj)
				{
					result = this.hashSet.GetEnumerator();
				}
				return result;
			}

			// Token: 0x04003F35 RID: 16181
			private ServiceChannelManager channelManager;

			// Token: 0x04003F36 RID: 16182
			private object syncRoot;

			// Token: 0x04003F37 RID: 16183
			private HashSet<IChannel> hashSet = new HashSet<IChannel>();
		}
	}
}
