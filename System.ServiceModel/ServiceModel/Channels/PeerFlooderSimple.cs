using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Security;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009FC RID: 2556
	internal class PeerFlooderSimple : PeerFlooderBase<Message, UtilityInfo>
	{
		// Token: 0x06006575 RID: 25973 RVA: 0x0017A5A0 File Offset: 0x001787A0
		internal PeerFlooderSimple(PeerNodeConfig config, PeerNeighborManager neighborManager) : base(config, neighborManager)
		{
			this.messageIds = new PeerFlooderSimple.ListManager(5U);
		}

		// Token: 0x06006576 RID: 25974 RVA: 0x0017A5B6 File Offset: 0x001787B6
		public override bool ShouldProcess(Message message)
		{
			return message.Properties.ContainsKey("MessageVerified");
		}

		// Token: 0x06006577 RID: 25975 RVA: 0x0017A5C8 File Offset: 0x001787C8
		public bool IsNotSeenBefore(Message message, out byte[] id, out int cacheHit)
		{
			cacheHit = -1;
			id = PeerNodeImplementation.DefaultId;
			if (message is SecurityVerifiedMessage)
			{
				id = (message as SecurityVerifiedMessage).PrimarySignatureValue;
			}
			else
			{
				UniqueId headerUniqueId = PeerMessageHelpers.GetHeaderUniqueId(message.Headers, "MessageID", "http://schemas.microsoft.com/net/2006/05/peer");
				if (headerUniqueId == null)
				{
					return false;
				}
				if (!headerUniqueId.IsGuid)
				{
					return false;
				}
				id = new byte[16];
				headerUniqueId.TryGetGuid(id, 0);
			}
			cacheHit = this.messageIds.AddForLookup(id);
			return cacheHit == -1;
		}

		// Token: 0x06006578 RID: 25976 RVA: 0x0017A64D File Offset: 0x0017884D
		public override void RecordOutgoingMessage(byte[] id)
		{
			this.messageIds.AddForFlood(id);
		}

		// Token: 0x06006579 RID: 25977 RVA: 0x0017A65C File Offset: 0x0017885C
		public override void OnOpen()
		{
		}

		// Token: 0x0600657A RID: 25978 RVA: 0x0017A65E File Offset: 0x0017885E
		public override void OnClose()
		{
			this.messageIds.Close();
		}

		// Token: 0x0600657B RID: 25979 RVA: 0x0017A66B File Offset: 0x0017886B
		public override IAsyncResult OnFloodedMessage(IPeerNeighbor neighbor, Message floodInfo, AsyncCallback callback, object state)
		{
			return base.OnFloodedMessage(neighbor, floodInfo, callback, state);
		}

		// Token: 0x0600657C RID: 25980 RVA: 0x0017A678 File Offset: 0x00178878
		public override void EndFloodMessage(IAsyncResult result)
		{
			base.EndFloodMessage(result);
		}

		// Token: 0x0600657D RID: 25981 RVA: 0x0017A684 File Offset: 0x00178884
		public override void ProcessLinkUtility(IPeerNeighbor neighbor, UtilityInfo utilityInfo)
		{
			if (!PeerNeighborStateHelper.IsConnected(neighbor.State))
			{
				neighbor.Abort(PeerCloseReason.InvalidNeighbor, PeerCloseInitiator.LocalNode);
				return;
			}
			try
			{
				UtilityExtension.ProcessLinkUtility(neighbor, utilityInfo);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (PeerFlooderBase<Message, UtilityInfo>.CloseNeighborIfKnownException(this.neighborManager, exception, neighbor) != null)
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
		}

		// Token: 0x04003A25 RID: 14885
		private PeerFlooderSimple.ListManager messageIds;

		// Token: 0x04003A26 RID: 14886
		private const uint MaxBuckets = 5U;

		// Token: 0x02000E5B RID: 3675
		private class ListManager
		{
			// Token: 0x06008348 RID: 33608 RVA: 0x001E5EA4 File Offset: 0x001E40A4
			public ListManager(uint buckets)
			{
				if (buckets <= 1U)
				{
					throw Fx.AssertAndThrow("ListManager should be used atleast with 2 buckets");
				}
				this.buckets = buckets;
				this.tables = new Dictionary<byte[], bool>[buckets];
				for (uint num = 0U; num < buckets; num += 1U)
				{
					this.tables[(int)num] = this.NewCache(PeerFlooderSimple.ListManager.InitialCount);
				}
				this.messagePruningTimer = new IOThreadTimer(new Action<object>(this.OnTimeout), null, false);
				this.messagePruningTimer.Set(PeerFlooderSimple.ListManager.PruningTimout);
				this.active = 0U;
				this.disposed = false;
				this.thisLock = new object();
			}

			// Token: 0x17001D0C RID: 7436
			// (get) Token: 0x06008349 RID: 33609 RVA: 0x001E5F3C File Offset: 0x001E413C
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x0600834A RID: 33610 RVA: 0x001E5F44 File Offset: 0x001E4144
			public int AddForLookup(byte[] key)
			{
				if (this.disposed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerFlooderDisposed")));
				}
				object obj = this.ThisLock;
				int result;
				lock (obj)
				{
					int num;
					if ((num = this.Contains(key)) == -1)
					{
						this.tables[(int)this.active].Add(key, false);
					}
					result = num;
				}
				return result;
			}

			// Token: 0x0600834B RID: 33611 RVA: 0x001E5FC8 File Offset: 0x001E41C8
			public bool AddForFlood(byte[] key)
			{
				if (this.disposed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerFlooderDisposed")));
				}
				object obj = this.ThisLock;
				bool result;
				lock (obj)
				{
					if (this.UpdateFloodEntry(key))
					{
						result = true;
					}
					else
					{
						result = false;
					}
				}
				return result;
			}

			// Token: 0x0600834C RID: 33612 RVA: 0x001E6038 File Offset: 0x001E4238
			internal void Close()
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.disposed)
					{
						this.messagePruningTimer.Cancel();
						this.messagePruningTimer = null;
						this.tables = null;
						this.disposed = true;
					}
				}
			}

			// Token: 0x0600834D RID: 33613 RVA: 0x001E60A4 File Offset: 0x001E42A4
			internal bool UpdateFloodEntry(byte[] key)
			{
				bool flag = false;
				uint num = this.buckets;
				while (num > 0U)
				{
					if (this.tables[(int)((this.active + num) % this.buckets)].TryGetValue(key, out flag))
					{
						if (!flag)
						{
							this.tables[(int)((this.active + num) % this.buckets)][key] = true;
							return true;
						}
						return false;
					}
					else
					{
						num -= 1U;
					}
				}
				this.tables[(int)this.active].Add(key, true);
				return true;
			}

			// Token: 0x0600834E RID: 33614 RVA: 0x001E6120 File Offset: 0x001E4320
			internal int Contains(byte[] key)
			{
				int num = -1;
				uint num2;
				for (num2 = this.buckets; num2 > 0U; num2 -= 1U)
				{
					if (this.tables[(int)((this.active + num2) % this.buckets)].ContainsKey(key))
					{
						num = (int)num2;
					}
				}
				if (num < 0)
				{
					return num;
				}
				return (int)((this.active + this.buckets - num2) % this.buckets);
			}

			// Token: 0x0600834F RID: 33615 RVA: 0x001E6180 File Offset: 0x001E4380
			private void OnTimeout(object state)
			{
				if (this.disposed)
				{
					return;
				}
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.disposed)
					{
						this.active = (this.active + 1U) % this.buckets;
						this.tables[(int)this.active] = this.NewCache(this.tables[(int)this.active].Count);
						this.messagePruningTimer.Set(PeerFlooderSimple.ListManager.PruningTimout);
					}
				}
			}

			// Token: 0x06008350 RID: 33616 RVA: 0x001E621C File Offset: 0x001E441C
			private Dictionary<byte[], bool> NewCache(int capacity)
			{
				return new Dictionary<byte[], bool>(capacity, PeerFlooderSimple.ListManager.keyComparer);
			}

			// Token: 0x04004ABC RID: 19132
			private uint active;

			// Token: 0x04004ABD RID: 19133
			private readonly uint buckets;

			// Token: 0x04004ABE RID: 19134
			private volatile bool disposed;

			// Token: 0x04004ABF RID: 19135
			private IOThreadTimer messagePruningTimer;

			// Token: 0x04004AC0 RID: 19136
			private static readonly int PruningTimout = 60000;

			// Token: 0x04004AC1 RID: 19137
			private static readonly int InitialCount = 1000;

			// Token: 0x04004AC2 RID: 19138
			private Dictionary<byte[], bool>[] tables;

			// Token: 0x04004AC3 RID: 19139
			private object thisLock;

			// Token: 0x04004AC4 RID: 19140
			private static InMemoryNonceCache.NonceCacheImpl.NonceKeyComparer keyComparer = new InMemoryNonceCache.NonceCacheImpl.NonceKeyComparer();

			// Token: 0x04004AC5 RID: 19141
			private const int NotFound = -1;
		}
	}
}
