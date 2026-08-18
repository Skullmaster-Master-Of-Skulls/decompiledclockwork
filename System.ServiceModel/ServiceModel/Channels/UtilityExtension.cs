using System;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Description;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A58 RID: 2648
	internal class UtilityExtension : IExtension<IPeerNeighbor>
	{
		// Token: 0x1400005A RID: 90
		// (add) Token: 0x06006889 RID: 26761 RVA: 0x0018664C File Offset: 0x0018484C
		// (remove) Token: 0x0600688A RID: 26762 RVA: 0x00186684 File Offset: 0x00184884
		public event EventHandler UtilityInfoReceived;

		// Token: 0x1400005B RID: 91
		// (add) Token: 0x0600688B RID: 26763 RVA: 0x001866BC File Offset: 0x001848BC
		// (remove) Token: 0x0600688C RID: 26764 RVA: 0x001866F4 File Offset: 0x001848F4
		public event EventHandler UtilityInfoSent;

		// Token: 0x0600688D RID: 26765 RVA: 0x0018672C File Offset: 0x0018492C
		private UtilityExtension()
		{
			this.ackTimer = new IOThreadTimer(new Action<object>(this.AcknowledgeLoop), null, false);
			this.pendingSends = 0;
			this.pruneTimer = new IOThreadTimer(new Action<object>(this.VerifyCheckPoint), null, false);
			this.pruneInterval = TimeSpan.FromMilliseconds((double)(10000 + new Random(Process.GetCurrentProcess().Id).Next(10000)));
		}

		// Token: 0x170018FE RID: 6398
		// (get) Token: 0x0600688E RID: 26766 RVA: 0x001867B9 File Offset: 0x001849B9
		public bool IsAccurate
		{
			get
			{
				return this.updateCount >= 32U;
			}
		}

		// Token: 0x170018FF RID: 6399
		// (get) Token: 0x0600688F RID: 26767 RVA: 0x001867C8 File Offset: 0x001849C8
		public uint LinkUtility
		{
			get
			{
				return this.linkUtility;
			}
		}

		// Token: 0x17001900 RID: 6400
		// (get) Token: 0x06006890 RID: 26768 RVA: 0x001867D0 File Offset: 0x001849D0
		internal TypedMessageConverter MessageConverter
		{
			get
			{
				if (this.messageConverter == null)
				{
					this.messageConverter = TypedMessageConverter.Create(typeof(UtilityInfo), "http://schemas.microsoft.com/net/2006/05/peer/LinkUtility");
				}
				return this.messageConverter;
			}
		}

		// Token: 0x06006891 RID: 26769 RVA: 0x001867FA File Offset: 0x001849FA
		public void Attach(IPeerNeighbor host)
		{
			this.owner = host;
			this.ackTimer.Set(30000);
		}

		// Token: 0x06006892 RID: 26770 RVA: 0x00186813 File Offset: 0x00184A13
		public static void OnNeighborConnected(IPeerNeighbor neighbor)
		{
			neighbor.Extensions.Add(new UtilityExtension());
		}

		// Token: 0x06006893 RID: 26771 RVA: 0x00186828 File Offset: 0x00184A28
		public static void OnNeighborClosed(IPeerNeighbor neighbor)
		{
			UtilityExtension utilityExtension = neighbor.Extensions.Find<UtilityExtension>();
			if (utilityExtension != null)
			{
				neighbor.Extensions.Remove(utilityExtension);
			}
		}

		// Token: 0x06006894 RID: 26772 RVA: 0x00186854 File Offset: 0x00184A54
		public void Detach(IPeerNeighbor host)
		{
			this.ackTimer.Cancel();
			this.owner = null;
			object obj = this.throttleLock;
			lock (obj)
			{
				this.pruneTimer.Cancel();
			}
		}

		// Token: 0x17001901 RID: 6401
		// (get) Token: 0x06006895 RID: 26773 RVA: 0x001868B0 File Offset: 0x00184AB0
		public object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06006896 RID: 26774 RVA: 0x001868B8 File Offset: 0x00184AB8
		public static void OnMessageSent(IPeerNeighbor neighbor)
		{
			UtilityExtension utilityExtension = neighbor.Extensions.Find<UtilityExtension>();
			if (utilityExtension != null)
			{
				utilityExtension.OnMessageSent();
			}
		}

		// Token: 0x06006897 RID: 26775 RVA: 0x001868DC File Offset: 0x00184ADC
		private void OnMessageSent()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.outTotal++;
			}
			Interlocked.Increment(ref this.pendingSends);
		}

		// Token: 0x06006898 RID: 26776 RVA: 0x00186930 File Offset: 0x00184B30
		public static void OnEndSend(IPeerNeighbor neighbor, FloodAsyncResult fresult)
		{
			if (neighbor.State >= PeerNeighborState.Disconnecting)
			{
				return;
			}
			UtilityExtension utility = neighbor.Utility;
			if (utility == null)
			{
				return;
			}
			utility.OnEndSend(fresult);
		}

		// Token: 0x06006899 RID: 26777 RVA: 0x00186959 File Offset: 0x00184B59
		public void OnEndSend(FloodAsyncResult fresult)
		{
			Interlocked.Decrement(ref this.pendingSends);
		}

		// Token: 0x0600689A RID: 26778 RVA: 0x00186968 File Offset: 0x00184B68
		private void AcknowledgeLoop(object state)
		{
			IPeerNeighbor peerNeighbor = this.owner;
			if (peerNeighbor == null || !peerNeighbor.IsConnected)
			{
				return;
			}
			this.FlushAcknowledge();
			if (this.owner != null)
			{
				this.ackTimer.Set(30000);
			}
		}

		// Token: 0x0600689B RID: 26779 RVA: 0x001869A8 File Offset: 0x00184BA8
		public static void ProcessLinkUtility(IPeerNeighbor neighbor, UtilityInfo umessage)
		{
			UtilityExtension utilityExtension = neighbor.Extensions.Find<UtilityExtension>();
			if (utilityExtension != null)
			{
				utilityExtension.ProcessLinkUtility(umessage.Useful, umessage.Total);
			}
		}

		// Token: 0x0600689C RID: 26780 RVA: 0x001869D8 File Offset: 0x00184BD8
		private void ProcessLinkUtility(uint useful, uint total)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (total > 32U || useful > total || this.outTotal < (int)total)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerLinkUtilityInvalidValues", new object[]
					{
						useful,
						total
					})));
				}
				uint num;
				for (num = 0U; num < useful; num += 1U)
				{
					this.linkUtility = this.Calculate(this.linkUtility, true);
				}
				while (num < total)
				{
					this.linkUtility = this.Calculate(this.linkUtility, false);
					num += 1U;
				}
				this.outTotal -= (int)total;
			}
			if (this.UtilityInfoReceived != null)
			{
				this.UtilityInfoReceived(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600689D RID: 26781 RVA: 0x00186ABC File Offset: 0x00184CBC
		private uint Calculate(uint current, bool increase)
		{
			uint num = current * 31U / 32U;
			if (increase)
			{
				num += 128U;
			}
			if (num > 4096U)
			{
				throw Fx.AssertAndThrow("Link utility should not exceed " + 4096U.ToString());
			}
			if (!this.IsAccurate)
			{
				this.updateCount += 1U;
			}
			return num;
		}

		// Token: 0x0600689E RID: 26782 RVA: 0x00186B1C File Offset: 0x00184D1C
		public static uint UpdateLinkUtility(IPeerNeighbor neighbor, bool useful)
		{
			uint result = 0U;
			UtilityExtension utilityExtension = neighbor.Extensions.Find<UtilityExtension>();
			if (utilityExtension != null)
			{
				result = utilityExtension.UpdateLinkUtility(useful);
			}
			return result;
		}

		// Token: 0x0600689F RID: 26783 RVA: 0x00186B44 File Offset: 0x00184D44
		public uint UpdateLinkUtility(bool useful)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.inTotal += 1U;
				if (useful)
				{
					this.inUseful += 1U;
				}
				this.linkUtility = this.Calculate(this.linkUtility, useful);
				if (this.inTotal == 32U)
				{
					this.FlushAcknowledge();
				}
			}
			return this.linkUtility;
		}

		// Token: 0x060068A0 RID: 26784 RVA: 0x00186BC8 File Offset: 0x00184DC8
		public void FlushAcknowledge()
		{
			if (this.inTotal == 0U)
			{
				return;
			}
			uint useful = 0U;
			uint total = 0U;
			object obj = this.ThisLock;
			lock (obj)
			{
				useful = this.inUseful;
				total = this.inTotal;
				this.inUseful = 0U;
				this.inTotal = 0U;
			}
			this.SendUtilityMessage(useful, total);
		}

		// Token: 0x060068A1 RID: 26785 RVA: 0x00186C34 File Offset: 0x00184E34
		private void SendUtilityMessage(uint useful, uint total)
		{
			IPeerNeighbor peerNeighbor = this.owner;
			if (peerNeighbor == null || !PeerNeighborStateHelper.IsConnected(peerNeighbor.State) || total == 0U)
			{
				return;
			}
			UtilityInfo utilityInfo = new UtilityInfo(useful, total);
			IAsyncResult asyncResult = null;
			Message message = this.MessageConverter.ToMessage(utilityInfo, MessageVersion.Soap12WSAddressing10);
			bool flag = false;
			try
			{
				asyncResult = peerNeighbor.BeginSend(message, Fx.ThunkCallback(new AsyncCallback(this.UtilityMessageSent)), new UtilityExtension.AsyncUtilityState(message, utilityInfo));
				if (asyncResult.CompletedSynchronously)
				{
					peerNeighbor.EndSend(asyncResult);
					EventHandler utilityInfoSent = this.UtilityInfoSent;
					if (utilityInfoSent != null)
					{
						utilityInfoSent(this, EventArgs.Empty);
					}
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					flag = true;
					throw;
				}
				if (this.HandleSendException(peerNeighbor, ex, utilityInfo) != null)
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			finally
			{
				if (!flag && (asyncResult == null || asyncResult.CompletedSynchronously))
				{
					message.Close();
				}
			}
		}

		// Token: 0x060068A2 RID: 26786 RVA: 0x00186D20 File Offset: 0x00184F20
		private void UtilityMessageSent(IAsyncResult result)
		{
			if (result == null || result.AsyncState == null)
			{
				return;
			}
			IPeerNeighbor peerNeighbor = this.owner;
			if (peerNeighbor == null || !PeerNeighborStateHelper.IsConnected(peerNeighbor.State))
			{
				return;
			}
			if (result.CompletedSynchronously)
			{
				return;
			}
			UtilityExtension.AsyncUtilityState asyncUtilityState = (UtilityExtension.AsyncUtilityState)result.AsyncState;
			Message message = asyncUtilityState.message;
			UtilityInfo info = asyncUtilityState.info;
			bool flag = false;
			if (info == null)
			{
				throw Fx.AssertAndThrow("expecting a UtilityInfo message in the AsyncState!");
			}
			try
			{
				peerNeighbor.EndSend(result);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					flag = true;
					throw;
				}
				if (this.HandleSendException(peerNeighbor, ex, info) != null)
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
			}
			finally
			{
				if (!flag)
				{
					message.Close();
				}
			}
			EventHandler utilityInfoSent = this.UtilityInfoSent;
			if (utilityInfoSent != null)
			{
				utilityInfoSent(this, EventArgs.Empty);
			}
		}

		// Token: 0x060068A3 RID: 26787 RVA: 0x00186DF8 File Offset: 0x00184FF8
		private Exception HandleSendException(IPeerNeighbor host, Exception e, UtilityInfo umessage)
		{
			if (!(e is ObjectDisposedException) && !(e is TimeoutException) && !(e is CommunicationException))
			{
				return e;
			}
			if (e.InnerException is QuotaExceededException)
			{
				throw Fx.AssertAndThrow("insufficient quota for sending messages!");
			}
			object obj = this.ThisLock;
			lock (obj)
			{
				this.inTotal += umessage.Total;
				this.inUseful += umessage.Useful;
			}
			return null;
		}

		// Token: 0x060068A4 RID: 26788 RVA: 0x00186E8C File Offset: 0x0018508C
		internal static void ReportCacheMiss(IPeerNeighbor neighbor, int missedBy)
		{
			if (!neighbor.IsConnected)
			{
				return;
			}
			UtilityExtension utilityExtension = neighbor.Extensions.Find<UtilityExtension>();
			if (utilityExtension != null)
			{
				utilityExtension.ReportCacheMiss(missedBy);
			}
		}

		// Token: 0x060068A5 RID: 26789 RVA: 0x00186EB8 File Offset: 0x001850B8
		private void ReportCacheMiss(int missedBy)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				for (int i = 0; i < missedBy; i++)
				{
					this.linkUtility = this.Calculate(this.linkUtility, false);
				}
			}
		}

		// Token: 0x17001902 RID: 6402
		// (get) Token: 0x060068A6 RID: 26790 RVA: 0x00186F14 File Offset: 0x00185114
		public int PendingMessages
		{
			get
			{
				return this.pendingSends;
			}
		}

		// Token: 0x060068A7 RID: 26791 RVA: 0x00186F1C File Offset: 0x0018511C
		public void BeginCheckPoint(UtilityExtension.PruneNeighborCallback pruneCallback)
		{
			if (this.isMonitoring)
			{
				return;
			}
			object obj = this.throttleLock;
			lock (obj)
			{
				if (!this.isMonitoring)
				{
					this.checkPointPendingSends = this.pendingSends;
					this.pruneNeighbor = pruneCallback;
					this.expectedClearance = this.pendingSends / 2;
					this.isMonitoring = true;
					if (this.owner != null)
					{
						this.pruneTimer.Set(this.pruneInterval);
					}
				}
			}
		}

		// Token: 0x060068A8 RID: 26792 RVA: 0x00186FAC File Offset: 0x001851AC
		private void VerifyCheckPoint(object state)
		{
			IPeerNeighbor peerNeighbor = this.owner;
			if (peerNeighbor == null || !peerNeighbor.IsConnected)
			{
				return;
			}
			object obj = this.throttleLock;
			int num;
			int num2;
			lock (obj)
			{
				num = this.pendingSends;
				num2 = this.checkPointPendingSends;
			}
			if (num <= 8)
			{
				object obj2 = this.throttleLock;
				lock (obj2)
				{
					this.isMonitoring = false;
					return;
				}
			}
			if (num + this.expectedClearance >= num2)
			{
				this.pruneNeighbor(peerNeighbor);
				return;
			}
			object obj3 = this.throttleLock;
			lock (obj3)
			{
				if (this.owner != null)
				{
					this.checkPointPendingSends = this.pendingSends;
					this.expectedClearance /= 2;
					this.pruneTimer.Set(this.pruneInterval);
				}
			}
		}

		// Token: 0x04003BEF RID: 15343
		private uint linkUtility;

		// Token: 0x04003BF0 RID: 15344
		private uint updateCount;

		// Token: 0x04003BF1 RID: 15345
		private IOThreadTimer ackTimer;

		// Token: 0x04003BF2 RID: 15346
		private const uint linkUtilityIncrement = 128U;

		// Token: 0x04003BF3 RID: 15347
		private const uint maxLinkUtility = 4096U;

		// Token: 0x04003BF4 RID: 15348
		private int outTotal;

		// Token: 0x04003BF5 RID: 15349
		private uint inTotal;

		// Token: 0x04003BF6 RID: 15350
		private uint inUseful;

		// Token: 0x04003BF7 RID: 15351
		private IPeerNeighbor owner;

		// Token: 0x04003BF8 RID: 15352
		private object thisLock = new object();

		// Token: 0x04003BF9 RID: 15353
		private object throttleLock = new object();

		// Token: 0x04003BFC RID: 15356
		private TypedMessageConverter messageConverter;

		// Token: 0x04003BFD RID: 15357
		public const int AcceptableMissDistance = 2;

		// Token: 0x04003BFE RID: 15358
		private int pendingSends;

		// Token: 0x04003BFF RID: 15359
		private int checkPointPendingSends;

		// Token: 0x04003C00 RID: 15360
		private bool isMonitoring;

		// Token: 0x04003C01 RID: 15361
		private int expectedClearance;

		// Token: 0x04003C02 RID: 15362
		private IOThreadTimer pruneTimer;

		// Token: 0x04003C03 RID: 15363
		private const int PruneIntervalMilliseconds = 10000;

		// Token: 0x04003C04 RID: 15364
		private TimeSpan pruneInterval;

		// Token: 0x04003C05 RID: 15365
		private const int MinimumPendingMessages = 8;

		// Token: 0x04003C06 RID: 15366
		private UtilityExtension.PruneNeighborCallback pruneNeighbor;

		// Token: 0x02000E91 RID: 3729
		// (Invoke) Token: 0x06008418 RID: 33816
		public delegate void PruneNeighborCallback(IPeerNeighbor peer);

		// Token: 0x02000E92 RID: 3730
		private class AsyncUtilityState
		{
			// Token: 0x0600841B RID: 33819 RVA: 0x001E8606 File Offset: 0x001E6806
			public AsyncUtilityState(Message message, UtilityInfo info)
			{
				this.message = message;
				this.info = info;
			}

			// Token: 0x04004B9C RID: 19356
			public Message message;

			// Token: 0x04004B9D RID: 19357
			public UtilityInfo info;
		}
	}
}
