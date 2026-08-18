using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.ServiceModel.Security;
using System.Threading;
using System.Xml;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009FB RID: 2555
	internal abstract class PeerFlooderBase<TFloodContract, TLinkContract> : IFlooderForThrottle, IPeerFlooderContract<TFloodContract, TLinkContract> where TFloodContract : Message
	{
		// Token: 0x14000040 RID: 64
		// (add) Token: 0x0600654E RID: 25934 RVA: 0x00179B14 File Offset: 0x00177D14
		// (remove) Token: 0x0600654F RID: 25935 RVA: 0x00179B4C File Offset: 0x00177D4C
		public event EventHandler ThrottleReached;

		// Token: 0x14000041 RID: 65
		// (add) Token: 0x06006550 RID: 25936 RVA: 0x00179B84 File Offset: 0x00177D84
		// (remove) Token: 0x06006551 RID: 25937 RVA: 0x00179BBC File Offset: 0x00177DBC
		public event EventHandler SlowNeighborKilled;

		// Token: 0x14000042 RID: 66
		// (add) Token: 0x06006552 RID: 25938 RVA: 0x00179BF4 File Offset: 0x00177DF4
		// (remove) Token: 0x06006553 RID: 25939 RVA: 0x00179C2C File Offset: 0x00177E2C
		public event EventHandler ThrottleReleased;

		// Token: 0x06006554 RID: 25940 RVA: 0x00179C64 File Offset: 0x00177E64
		public PeerFlooderBase(PeerNodeConfig config, PeerNeighborManager neighborManager)
		{
			this.neighborManager = neighborManager;
			this.neighbors = new List<IPeerNeighbor>();
			this.config = config;
			this.neighbors = this.neighborManager.GetConnectedNeighbors();
			this.quotaHelper = new PeerFlooderBase<TFloodContract, TLinkContract>.PeerThrottleHelper(this, this.config.MaxPendingOutgoingCalls);
			this.OnMessageSentHandler = new EventHandler(this.OnMessageSent);
		}

		// Token: 0x06006555 RID: 25941 RVA: 0x00179CD8 File Offset: 0x00177ED8
		private void PruneNeighborCallback(IPeerNeighbor peer)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.Neighbors.Count <= 1)
				{
					return;
				}
				if (DiagnosticUtility.ShouldTraceWarning)
				{
					string @string = SR.GetString("PeerThrottlePruning", new object[]
					{
						this.config.MeshId
					});
					PeerThrottleTraceRecord extendedData = new PeerThrottleTraceRecord(this.config.MeshId, @string);
					TraceUtility.TraceEvent(TraceEventType.Warning, 262223, SR.GetString("TraceCodePeerFlooderReceiveMessageQuotaExceeded"), extendedData, this, null);
				}
			}
			try
			{
				peer.Abort(PeerCloseReason.NodeTooSlow, PeerCloseInitiator.LocalNode);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				if (PeerFlooderBase<TFloodContract, TLinkContract>.CloseNeighborIfKnownException(this.neighborManager, exception, peer) != null)
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
		}

		// Token: 0x06006556 RID: 25942 RVA: 0x00179DB8 File Offset: 0x00177FB8
		void IFlooderForThrottle.OnThrottleReached()
		{
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				string @string = SR.GetString("PeerThrottleWaiting", new object[]
				{
					this.config.MeshId
				});
				PeerThrottleTraceRecord extendedData = new PeerThrottleTraceRecord(this.config.MeshId, @string);
				TraceUtility.TraceEvent(TraceEventType.Information, 262223, SR.GetString("TraceCodePeerFlooderReceiveMessageQuotaExceeded"), extendedData, this, null);
			}
			IPeerNeighbor peerNeighbor = this.neighborManager.SlowestNeighbor();
			if (peerNeighbor == null)
			{
				return;
			}
			UtilityExtension utility = peerNeighbor.Utility;
			if (peerNeighbor.IsConnected && utility != null)
			{
				if (utility.PendingMessages > 32)
				{
					utility.BeginCheckPoint(new UtilityExtension.PruneNeighborCallback(this.PruneNeighborCallback));
				}
				this.FireReachedEvent();
			}
		}

		// Token: 0x06006557 RID: 25943 RVA: 0x00179E59 File Offset: 0x00178059
		void IFlooderForThrottle.OnThrottleReleased()
		{
			this.FireDequeuedEvent();
		}

		// Token: 0x06006558 RID: 25944 RVA: 0x00179E61 File Offset: 0x00178061
		public void FireDequeuedEvent()
		{
			this.FireEvent(this.ThrottleReleased);
		}

		// Token: 0x06006559 RID: 25945 RVA: 0x00179E6F File Offset: 0x0017806F
		public void FireReachedEvent()
		{
			this.FireEvent(this.ThrottleReached);
		}

		// Token: 0x0600655A RID: 25946 RVA: 0x00179E7D File Offset: 0x0017807D
		public void FireKilledEvent()
		{
			this.FireEvent(this.SlowNeighborKilled);
		}

		// Token: 0x0600655B RID: 25947 RVA: 0x00179E8B File Offset: 0x0017808B
		private void FireEvent(EventHandler handler)
		{
			if (handler != null)
			{
				handler(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600655C RID: 25948 RVA: 0x00179E9C File Offset: 0x0017809C
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public virtual IAsyncResult BeginFloodEncodedMessage(byte[] id, MessageBuffer encodedMessage, TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.RecordOutgoingMessage(id);
			SynchronizationContext currentSynchronizationContext = ThreadBehavior.GetCurrentSynchronizationContext();
			SynchronizationContext.SetSynchronizationContext(null);
			if (this.neighbors.Count == 0)
			{
				return new CompletedAsyncResult(callback, state);
			}
			IAsyncResult result;
			try
			{
				result = this.FloodMessageToNeighbors(encodedMessage, timeout, callback, state, -1, null, null, this.OnMessageSentHandler);
			}
			finally
			{
				SynchronizationContext.SetSynchronizationContext(currentSynchronizationContext);
			}
			return result;
		}

		// Token: 0x0600655D RID: 25949 RVA: 0x00179F04 File Offset: 0x00178104
		protected virtual IAsyncResult BeginFloodReceivedMessage(IPeerNeighbor sender, MessageBuffer messageBuffer, TimeSpan timeout, AsyncCallback callback, object state, int index, MessageHeader hopHeader)
		{
			this.quotaHelper.AcquireNoQueue();
			IAsyncResult result;
			try
			{
				result = this.FloodMessageToNeighbors(messageBuffer, timeout, callback, state, index, hopHeader, sender, this.OnMessageSentHandler);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				if (!(ex is QuotaExceededException) && (!(ex is CommunicationException) || !(ex.InnerException is QuotaExceededException)))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				if (DiagnosticUtility.ShouldTraceError)
				{
					PeerFlooderTraceRecord extendedData = new PeerFlooderTraceRecord(this.config.MeshId, sender.ListenAddress, ex);
					TraceUtility.TraceEvent(TraceEventType.Error, 262223, SR.GetString("TraceCodePeerFlooderReceiveMessageQuotaExceeded"), extendedData, this, null);
				}
				result = null;
			}
			return result;
		}

		// Token: 0x0600655E RID: 25950 RVA: 0x00179FB4 File Offset: 0x001781B4
		protected IAsyncResult BeginSendHelper(IPeerNeighbor neighbor, TimeSpan timeout, Message message, FloodAsyncResult fresult)
		{
			IAsyncResult asyncResult = null;
			bool flag = false;
			IAsyncResult result;
			try
			{
				UtilityExtension.OnMessageSent(neighbor);
				asyncResult = neighbor.BeginSend(message, timeout, Fx.ThunkCallback(new AsyncCallback(fresult.OnSendComplete)), message);
				fresult.AddResult(asyncResult, neighbor);
				if (asyncResult.CompletedSynchronously)
				{
					neighbor.EndSend(asyncResult);
					UtilityExtension.OnEndSend(neighbor, fresult);
				}
				result = asyncResult;
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					flag = true;
					throw;
				}
				if (PeerFlooderBase<TFloodContract, TLinkContract>.CloseNeighborIfKnownException(this.neighborManager, exception, neighbor) != null)
				{
					fresult.MarkEnd(false);
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				result = null;
			}
			finally
			{
				if ((asyncResult == null || asyncResult.CompletedSynchronously) && !flag)
				{
					message.Close();
				}
			}
			return result;
		}

		// Token: 0x0600655F RID: 25951 RVA: 0x0017A070 File Offset: 0x00178270
		public void OnMessageSent(object sender, EventArgs args)
		{
			this.quotaHelper.ItemDequeued();
		}

		// Token: 0x06006560 RID: 25952 RVA: 0x0017A080 File Offset: 0x00178280
		private void KillSlowNeighbor()
		{
			IPeerNeighbor peerNeighbor = this.neighborManager.SlowestNeighbor();
			if (peerNeighbor != null)
			{
				peerNeighbor.Abort(PeerCloseReason.NodeTooSlow, PeerCloseInitiator.LocalNode);
			}
		}

		// Token: 0x06006561 RID: 25953 RVA: 0x0017A0A8 File Offset: 0x001782A8
		protected virtual IAsyncResult FloodMessageToNeighbors(MessageBuffer messageBuffer, TimeSpan timeout, AsyncCallback callback, object state, int index, MessageHeader hopHeader, IPeerNeighbor except, EventHandler OnMessageSentCallback)
		{
			long num = Interlocked.Increment(ref this.messageSequence);
			FloodAsyncResult floodAsyncResult = new FloodAsyncResult(this.neighborManager, timeout, callback, state);
			floodAsyncResult.OnMessageSent += OnMessageSentCallback;
			List<IPeerNeighbor> list = this.Neighbors;
			foreach (IPeerNeighbor peerNeighbor in list)
			{
				if (!peerNeighbor.Equals(except) && PeerNeighborStateHelper.IsConnected(peerNeighbor.State))
				{
					Message message = messageBuffer.CreateMessage();
					if (index != -1)
					{
						message.Headers.ReplaceAt(index, hopHeader);
					}
					if (PeerNeighborStateHelper.IsConnected(peerNeighbor.State))
					{
						this.BeginSendHelper(peerNeighbor, timeout, message, floodAsyncResult);
					}
				}
			}
			floodAsyncResult.MarkEnd(true);
			return floodAsyncResult;
		}

		// Token: 0x06006562 RID: 25954 RVA: 0x0017A174 File Offset: 0x00178374
		public void Open()
		{
			this.OnOpen();
		}

		// Token: 0x06006563 RID: 25955 RVA: 0x0017A17C File Offset: 0x0017837C
		public void Close()
		{
			this.OnClose();
		}

		// Token: 0x06006564 RID: 25956
		public abstract void OnOpen();

		// Token: 0x06006565 RID: 25957
		public abstract void OnClose();

		// Token: 0x06006566 RID: 25958 RVA: 0x0017A184 File Offset: 0x00178384
		public virtual void OnNeighborConnected(IPeerNeighbor neighbor)
		{
			this.neighbors = this.neighborManager.GetConnectedNeighbors();
		}

		// Token: 0x06006567 RID: 25959 RVA: 0x0017A197 File Offset: 0x00178397
		public virtual void OnNeighborClosed(IPeerNeighbor neighbor)
		{
			this.neighbors = this.neighborManager.GetConnectedNeighbors();
		}

		// Token: 0x06006568 RID: 25960
		public abstract void ProcessLinkUtility(IPeerNeighbor neighbor, TLinkContract utilityInfo);

		// Token: 0x06006569 RID: 25961
		public abstract bool ShouldProcess(TFloodContract floodInfo);

		// Token: 0x0600656A RID: 25962
		public abstract void RecordOutgoingMessage(byte[] id);

		// Token: 0x0600656B RID: 25963 RVA: 0x0017A1AC File Offset: 0x001783AC
		private int UpdateHopCount(Message message, out MessageHeader hopHeader, out ulong currentValue)
		{
			int num = -1;
			currentValue = ulong.MaxValue;
			hopHeader = null;
			try
			{
				num = message.Headers.FindHeader("Hops", "http://schemas.microsoft.com/net/2006/05/peer/HopCount");
				if (num != -1)
				{
					currentValue = PeerMessageHelpers.GetHeaderULong(message.Headers, num);
					string name = "Hops";
					string ns = "http://schemas.microsoft.com/net/2006/05/peer/HopCount";
					ulong num2 = currentValue - 1UL;
					currentValue = num2;
					hopHeader = MessageHeader.CreateHeader(name, ns, num2, false);
				}
			}
			catch (MessageHeaderException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Warning);
			}
			catch (CommunicationException exception2)
			{
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Warning);
			}
			catch (SerializationException exception3)
			{
				DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Warning);
			}
			catch (XmlException exception4)
			{
				DiagnosticUtility.TraceHandledException(exception4, TraceEventType.Warning);
			}
			return num;
		}

		// Token: 0x0600656C RID: 25964 RVA: 0x0017A270 File Offset: 0x00178470
		public virtual IAsyncResult OnFloodedMessage(IPeerNeighbor neighbor, TFloodContract floodInfo, AsyncCallback callback, object state)
		{
			bool useful = false;
			MessageBuffer messageBuffer = null;
			Message message = null;
			int index = 0;
			ulong maxValue = ulong.MaxValue;
			MessageHeader messageHeader = null;
			bool flag = false;
			IAsyncResult result = null;
			try
			{
				PeerMessageProperty peerMessageProperty = (PeerMessageProperty)floodInfo.Properties["PeerProperty"];
				if (!peerMessageProperty.MessageVerified)
				{
					if (peerMessageProperty.CacheMiss > 2)
					{
						UtilityExtension.ReportCacheMiss(neighbor, peerMessageProperty.CacheMiss);
					}
					result = new CompletedAsyncResult(callback, state);
				}
				else
				{
					useful = true;
					messageBuffer = floodInfo.CreateBufferedCopy((int)this.config.MaxReceivedMessageSize);
					message = messageBuffer.CreateMessage();
					Uri peerVia = peerMessageProperty.PeerVia;
					Uri peerTo = peerMessageProperty.PeerTo;
					message.Headers.To = (message.Properties.Via = peerVia);
					index = this.UpdateHopCount(message, out messageHeader, out maxValue);
					PeerMessagePropagation peerMessagePropagation = PeerMessagePropagation.LocalAndRemote;
					if (peerMessageProperty.SkipLocalChannels)
					{
						peerMessagePropagation = PeerMessagePropagation.Remote;
					}
					else if (this.messageHandler.HasMessagePropagation)
					{
						using (Message message2 = messageBuffer.CreateMessage())
						{
							peerMessagePropagation = this.messageHandler.DetermineMessagePropagation(message2, PeerMessageOrigination.Remote);
						}
					}
					if ((peerMessagePropagation & PeerMessagePropagation.Remote) != PeerMessagePropagation.None && maxValue == 0UL)
					{
						peerMessagePropagation &= (PeerMessagePropagation)(-3);
					}
					if ((peerMessagePropagation & PeerMessagePropagation.Remote) != PeerMessagePropagation.None)
					{
						result = this.BeginFloodReceivedMessage(neighbor, messageBuffer, PeerTransportConstants.ForwardTimeout, callback, state, index, messageHeader);
					}
					else
					{
						result = new CompletedAsyncResult(callback, state);
					}
					if ((peerMessagePropagation & PeerMessagePropagation.Local) != PeerMessagePropagation.None)
					{
						this.messageHandler.HandleIncomingMessage(messageBuffer, peerMessagePropagation, index, messageHeader, peerVia, peerTo);
					}
				}
				UtilityExtension.UpdateLinkUtility(neighbor, useful);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					flag = true;
					throw;
				}
				if (PeerFlooderBase<TFloodContract, TLinkContract>.CloseNeighborIfKnownException(this.neighborManager, exception, neighbor) != null)
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			finally
			{
				if (!flag)
				{
					if (message != null)
					{
						message.Close();
					}
					if (messageBuffer != null)
					{
						messageBuffer.Close();
					}
				}
			}
			return result;
		}

		// Token: 0x0600656D RID: 25965 RVA: 0x0017A470 File Offset: 0x00178670
		public virtual void EndFloodMessage(IAsyncResult result)
		{
			if (result is CompletedAsyncResult)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			FloodAsyncResult floodAsyncResult = result as FloodAsyncResult;
			floodAsyncResult.End();
		}

		// Token: 0x17001872 RID: 6258
		// (get) Token: 0x0600656E RID: 25966 RVA: 0x0017A499 File Offset: 0x00178699
		protected long MaxReceivedMessageSize
		{
			get
			{
				return this.config.MaxReceivedMessageSize;
			}
		}

		// Token: 0x17001873 RID: 6259
		// (get) Token: 0x0600656F RID: 25967 RVA: 0x0017A4A6 File Offset: 0x001786A6
		protected MessageEncoder MessageEncoder
		{
			get
			{
				return this.config.MessageEncoder;
			}
		}

		// Token: 0x17001874 RID: 6260
		// (get) Token: 0x06006570 RID: 25968 RVA: 0x0017A4B3 File Offset: 0x001786B3
		protected object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x17001875 RID: 6261
		// (get) Token: 0x06006571 RID: 25969 RVA: 0x0017A4BB File Offset: 0x001786BB
		protected List<IPeerNeighbor> Neighbors
		{
			get
			{
				return this.neighbors;
			}
		}

		// Token: 0x06006572 RID: 25970 RVA: 0x0017A4C4 File Offset: 0x001786C4
		internal static Exception CloseNeighborIfKnownException(PeerNeighborManager neighborManager, Exception exception, IPeerNeighbor peer)
		{
			Exception result;
			try
			{
				if (exception is ObjectDisposedException)
				{
					result = null;
				}
				else if ((exception is CommunicationException && !(exception.InnerException is QuotaExceededException)) || exception is TimeoutException || exception is InvalidOperationException || exception is MessageSecurityException)
				{
					neighborManager.CloseNeighbor(peer, PeerCloseReason.InternalFailure, PeerCloseInitiator.LocalNode, exception);
					result = null;
				}
				else
				{
					result = exception;
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				result = ex;
			}
			return result;
		}

		// Token: 0x06006573 RID: 25971 RVA: 0x0017A544 File Offset: 0x00178744
		public static void EndFloodEncodedMessage(IAsyncResult result)
		{
			CompletedAsyncResult completedAsyncResult = result as CompletedAsyncResult;
			if (completedAsyncResult != null)
			{
				CompletedAsyncResult.End(result);
				return;
			}
			FloodAsyncResult floodAsyncResult = result as FloodAsyncResult;
			if (floodAsyncResult == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("result", SR.GetString("InvalidAsyncResult"));
			}
			floodAsyncResult.End();
		}

		// Token: 0x06006574 RID: 25972 RVA: 0x0017A58C File Offset: 0x0017878C
		public void EndFloodReceivedMessage(IAsyncResult result)
		{
			FloodAsyncResult floodAsyncResult = result as FloodAsyncResult;
		}

		// Token: 0x04003A1A RID: 14874
		protected PeerNodeConfig config;

		// Token: 0x04003A1B RID: 14875
		protected PeerNeighborManager neighborManager;

		// Token: 0x04003A1C RID: 14876
		protected List<IPeerNeighbor> neighbors;

		// Token: 0x04003A1D RID: 14877
		private object thisLock = new object();

		// Token: 0x04003A1E RID: 14878
		internal IPeerNodeMessageHandling messageHandler;

		// Token: 0x04003A1F RID: 14879
		internal PeerFlooderBase<TFloodContract, TLinkContract>.PeerThrottleHelper quotaHelper;

		// Token: 0x04003A20 RID: 14880
		private long messageSequence;

		// Token: 0x04003A24 RID: 14884
		public EventHandler OnMessageSentHandler;

		// Token: 0x02000E5A RID: 3674
		public class PeerThrottleHelper
		{
			// Token: 0x06008345 RID: 33605 RVA: 0x001E5E45 File Offset: 0x001E4045
			public PeerThrottleHelper(IFlooderForThrottle flooder, int outgoingLimit)
			{
				this.outgoingQuota = outgoingLimit;
				this.flooder = flooder;
			}

			// Token: 0x06008346 RID: 33606 RVA: 0x001E5E66 File Offset: 0x001E4066
			public void ItemDequeued()
			{
				Interlocked.Decrement(ref this.outgoingEnqueuedCount);
			}

			// Token: 0x06008347 RID: 33607 RVA: 0x001E5E74 File Offset: 0x001E4074
			public void AcquireNoQueue()
			{
				int num = Interlocked.Increment(ref this.outgoingEnqueuedCount);
				if (num >= this.outgoingQuota)
				{
					this.flooder.OnThrottleReached();
				}
			}

			// Token: 0x04004AB9 RID: 19129
			private int outgoingEnqueuedCount;

			// Token: 0x04004ABA RID: 19130
			private int outgoingQuota = 128;

			// Token: 0x04004ABB RID: 19131
			private IFlooderForThrottle flooder;
		}
	}
}
