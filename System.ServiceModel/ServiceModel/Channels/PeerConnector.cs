using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Runtime;
using System.ServiceModel.Description;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009F4 RID: 2548
	internal class PeerConnector : IPeerConnectorContract
	{
		// Token: 0x060064FE RID: 25854 RVA: 0x00178830 File Offset: 0x00176A30
		public PeerConnector(PeerNodeConfig config, PeerNeighborManager neighborManager, PeerMaintainer maintainer)
		{
			this.thisLock = new object();
			this.config = config;
			this.neighborManager = neighborManager;
			this.maintainer = maintainer;
			this.timerTable = new Dictionary<IPeerNeighbor, IOThreadTimer>();
			this.state = PeerConnector.State.Created;
		}

		// Token: 0x17001865 RID: 6245
		// (get) Token: 0x060064FF RID: 25855 RVA: 0x0017886A File Offset: 0x00176A6A
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x17001866 RID: 6246
		// (get) Token: 0x06006500 RID: 25856 RVA: 0x00178872 File Offset: 0x00176A72
		internal TypedMessageConverter ConnectInfoMessageConverter
		{
			get
			{
				if (this.connectInfoMessageConverter == null)
				{
					this.connectInfoMessageConverter = TypedMessageConverter.Create(typeof(ConnectInfo), "http://schemas.microsoft.com/net/2006/05/peer/Connect");
				}
				return this.connectInfoMessageConverter;
			}
		}

		// Token: 0x17001867 RID: 6247
		// (get) Token: 0x06006501 RID: 25857 RVA: 0x0017889C File Offset: 0x00176A9C
		internal TypedMessageConverter DisconnectInfoMessageConverter
		{
			get
			{
				if (this.disconnectInfoMessageConverter == null)
				{
					this.disconnectInfoMessageConverter = TypedMessageConverter.Create(typeof(DisconnectInfo), "http://schemas.microsoft.com/net/2006/05/peer/Disconnect");
				}
				return this.disconnectInfoMessageConverter;
			}
		}

		// Token: 0x17001868 RID: 6248
		// (get) Token: 0x06006502 RID: 25858 RVA: 0x001788C6 File Offset: 0x00176AC6
		internal TypedMessageConverter RefuseInfoMessageConverter
		{
			get
			{
				if (this.refuseInfoMessageConverter == null)
				{
					this.refuseInfoMessageConverter = TypedMessageConverter.Create(typeof(RefuseInfo), "http://schemas.microsoft.com/net/2006/05/peer/Refuse");
				}
				return this.refuseInfoMessageConverter;
			}
		}

		// Token: 0x17001869 RID: 6249
		// (get) Token: 0x06006503 RID: 25859 RVA: 0x001788F0 File Offset: 0x00176AF0
		internal TypedMessageConverter WelcomeInfoMessageConverter
		{
			get
			{
				if (this.welcomeInfoMessageConverter == null)
				{
					this.welcomeInfoMessageConverter = TypedMessageConverter.Create(typeof(WelcomeInfo), "http://schemas.microsoft.com/net/2006/05/peer/Welcome");
				}
				return this.welcomeInfoMessageConverter;
			}
		}

		// Token: 0x06006504 RID: 25860 RVA: 0x0017891C File Offset: 0x00176B1C
		private bool AddTimer(IPeerNeighbor neighbor)
		{
			bool result = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state == PeerConnector.State.Opened && neighbor.State == PeerNeighborState.Connecting)
				{
					IOThreadTimer iothreadTimer = new IOThreadTimer(new Action<object>(this.OnConnectTimeout), neighbor, true);
					iothreadTimer.Set(this.config.ConnectTimeout);
					this.timerTable.Add(neighbor, iothreadTimer);
					result = true;
				}
			}
			return result;
		}

		// Token: 0x06006505 RID: 25861 RVA: 0x001789A0 File Offset: 0x00176BA0
		private void SendMessageToNeighbor(IPeerNeighbor neighbor, Message message, PeerMessageHelpers.CleanupCallback cleanupCallback)
		{
			bool flag = false;
			try
			{
				neighbor.Send(message);
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					flag = true;
					throw;
				}
				if (!(ex is CommunicationException) && !(ex is QuotaExceededException) && !(ex is ObjectDisposedException) && !(ex is TimeoutException))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				if (cleanupCallback != null)
				{
					cleanupCallback(neighbor, PeerCloseReason.InternalFailure, ex);
				}
			}
			finally
			{
				if (!flag)
				{
					message.Close();
				}
			}
		}

		// Token: 0x06006506 RID: 25862 RVA: 0x00178A28 File Offset: 0x00176C28
		private void CleanupOnConnectFailure(IPeerNeighbor neighbor, PeerCloseReason reason, Exception exception)
		{
			if (this.RemoveTimer(neighbor))
			{
				this.neighborManager.CloseNeighbor(neighbor, reason, PeerCloseInitiator.LocalNode, exception);
			}
		}

		// Token: 0x06006507 RID: 25863 RVA: 0x00178A44 File Offset: 0x00176C44
		public void Close()
		{
			object obj = this.ThisLock;
			Dictionary<IPeerNeighbor, IOThreadTimer> dictionary;
			lock (obj)
			{
				dictionary = this.timerTable;
				this.timerTable = null;
				this.state = PeerConnector.State.Closed;
			}
			if (dictionary != null)
			{
				foreach (IOThreadTimer iothreadTimer in dictionary.Values)
				{
					iothreadTimer.Cancel();
				}
			}
		}

		// Token: 0x06006508 RID: 25864 RVA: 0x00178ADC File Offset: 0x00176CDC
		public void Closing()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.state = PeerConnector.State.Closing;
			}
		}

		// Token: 0x06006509 RID: 25865 RVA: 0x00178B20 File Offset: 0x00176D20
		private void CompleteTerminateMessageProcessing(IPeerNeighbor neighbor, PeerCloseReason closeReason, IList<Referral> referrals)
		{
			if (neighbor.TrySetState(PeerNeighborState.Disconnected))
			{
				this.neighborManager.CloseNeighbor(neighbor, closeReason, PeerCloseInitiator.RemoteNode);
			}
			else if (neighbor.State < PeerNeighborState.Disconnected)
			{
				throw Fx.AssertAndThrow("Unexpected neighbor state");
			}
			this.maintainer.AddReferrals(referrals, neighbor);
		}

		// Token: 0x0600650A RID: 25866 RVA: 0x00178B5D File Offset: 0x00176D5D
		private void OnConnectFailure(IPeerNeighbor neighbor, PeerCloseReason reason, Exception exception)
		{
			this.CleanupOnConnectFailure(neighbor, reason, exception);
		}

		// Token: 0x0600650B RID: 25867 RVA: 0x00178B68 File Offset: 0x00176D68
		private void OnConnectTimeout(object asyncState)
		{
			this.CleanupOnConnectFailure((IPeerNeighbor)asyncState, PeerCloseReason.ConnectTimedOut, null);
		}

		// Token: 0x0600650C RID: 25868 RVA: 0x00178B78 File Offset: 0x00176D78
		public void OnNeighborClosed(IPeerNeighbor neighbor)
		{
			this.RemoveTimer(neighbor);
		}

		// Token: 0x0600650D RID: 25869 RVA: 0x00178B82 File Offset: 0x00176D82
		public void OnNeighborClosing(IPeerNeighbor neighbor, PeerCloseReason closeReason)
		{
			if (neighbor.IsConnected)
			{
				this.SendTerminatingMessage(neighbor, "http://schemas.microsoft.com/net/2006/05/peer/Disconnect", closeReason);
			}
		}

		// Token: 0x0600650E RID: 25870 RVA: 0x00178B9C File Offset: 0x00176D9C
		public void OnNeighborAuthenticated(IPeerNeighbor neighbor)
		{
			if (this.state == PeerConnector.State.Created)
			{
				throw Fx.AssertAndThrow("Connector not expected to be in Created state");
			}
			if (!PeerNeighborStateHelper.IsAuthenticatedOrClosed(neighbor.State))
			{
				throw Fx.AssertAndThrow(string.Format(CultureInfo.InvariantCulture, "Neighbor state expected to be Authenticated or Closed, actual state: {0}", new object[]
				{
					neighbor.State
				}));
			}
			if (neighbor.TrySetState(PeerNeighborState.Connecting))
			{
				if (this.AddTimer(neighbor) && neighbor.IsInitiator)
				{
					if (this.neighborManager.ConnectedNeighborCount < this.config.MaxNeighbors)
					{
						this.SendConnect(neighbor);
						return;
					}
					this.neighborManager.CloseNeighbor(neighbor, PeerCloseReason.NodeBusy, PeerCloseInitiator.LocalNode);
				}
				return;
			}
			if (neighbor.State < PeerNeighborState.Faulted)
			{
				throw Fx.AssertAndThrow(string.Format(CultureInfo.InvariantCulture, "Neighbor state expected to be Faulted or Closed, actual state: {0}", new object[]
				{
					neighbor.State
				}));
			}
		}

		// Token: 0x0600650F RID: 25871 RVA: 0x00178C70 File Offset: 0x00176E70
		public void Open()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state != PeerConnector.State.Created)
				{
					throw Fx.AssertAndThrow("Connector expected to be in Created state");
				}
				this.state = PeerConnector.State.Opened;
			}
		}

		// Token: 0x06006510 RID: 25872 RVA: 0x00178CC4 File Offset: 0x00176EC4
		public void Connect(IPeerNeighbor neighbor, ConnectInfo connectInfo)
		{
			if (this.state != PeerConnector.State.Opened)
			{
				return;
			}
			PeerCloseReason peerCloseReason = PeerCloseReason.None;
			if (neighbor.IsInitiator || !connectInfo.HasBody() || (neighbor.State != PeerNeighborState.Connecting && neighbor.State != PeerNeighborState.Closed))
			{
				peerCloseReason = PeerCloseReason.InvalidNeighbor;
			}
			else if (this.RemoveTimer(neighbor))
			{
				if (this.neighborManager.ConnectedNeighborCount >= this.config.MaxNeighbors)
				{
					peerCloseReason = PeerCloseReason.NodeBusy;
				}
				else if (!PeerValidateHelper.ValidNodeAddress(connectInfo.Address))
				{
					peerCloseReason = PeerCloseReason.InvalidNeighbor;
				}
				else
				{
					string action = "http://schemas.microsoft.com/net/2006/05/peer/Refuse";
					IPeerNeighbor peerNeighbor;
					PeerCloseReason peerCloseReason2;
					this.ValidateNeighbor(neighbor, connectInfo.NodeId, out peerNeighbor, out peerCloseReason2, out action);
					if (neighbor != peerNeighbor)
					{
						this.SendWelcome(neighbor);
						try
						{
							neighbor.ListenAddress = connectInfo.Address;
						}
						catch (ObjectDisposedException exception)
						{
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						}
						if (!neighbor.TrySetState(PeerNeighborState.Connected) && neighbor.State < PeerNeighborState.Disconnecting)
						{
							throw Fx.AssertAndThrow("Neighbor state expected to be >= Disconnecting; it is " + neighbor.State.ToString());
						}
						if (peerNeighbor != null)
						{
							this.SendTerminatingMessage(peerNeighbor, action, peerCloseReason2);
							this.neighborManager.CloseNeighbor(peerNeighbor, peerCloseReason2, PeerCloseInitiator.LocalNode);
						}
					}
					else
					{
						peerCloseReason = peerCloseReason2;
					}
				}
			}
			if (peerCloseReason != PeerCloseReason.None)
			{
				this.SendTerminatingMessage(neighbor, "http://schemas.microsoft.com/net/2006/05/peer/Refuse", peerCloseReason);
				this.neighborManager.CloseNeighbor(neighbor, peerCloseReason, PeerCloseInitiator.LocalNode);
			}
		}

		// Token: 0x06006511 RID: 25873 RVA: 0x00178E08 File Offset: 0x00177008
		public void Disconnect(IPeerNeighbor neighbor, DisconnectInfo disconnectInfo)
		{
			if (this.state != PeerConnector.State.Opened)
			{
				return;
			}
			PeerCloseReason closeReason = PeerCloseReason.InvalidNeighbor;
			IList<Referral> referrals = null;
			if (disconnectInfo.HasBody() && neighbor.State >= PeerNeighborState.Connected && PeerConnectorHelper.IsDefined(disconnectInfo.Reason))
			{
				closeReason = (PeerCloseReason)disconnectInfo.Reason;
				referrals = disconnectInfo.Referrals;
			}
			this.CompleteTerminateMessageProcessing(neighbor, closeReason, referrals);
		}

		// Token: 0x06006512 RID: 25874 RVA: 0x00178E58 File Offset: 0x00177058
		public void Refuse(IPeerNeighbor neighbor, RefuseInfo refuseInfo)
		{
			if (this.state != PeerConnector.State.Opened)
			{
				return;
			}
			PeerCloseReason closeReason = PeerCloseReason.InvalidNeighbor;
			IList<Referral> referrals = null;
			if (refuseInfo.HasBody() && neighbor.IsInitiator && (neighbor.State == PeerNeighborState.Connecting || neighbor.State == PeerNeighborState.Closed))
			{
				this.RemoveTimer(neighbor);
				if (PeerConnectorHelper.IsDefined(refuseInfo.Reason))
				{
					closeReason = (PeerCloseReason)refuseInfo.Reason;
					referrals = refuseInfo.Referrals;
				}
			}
			this.CompleteTerminateMessageProcessing(neighbor, closeReason, referrals);
		}

		// Token: 0x06006513 RID: 25875 RVA: 0x00178EC4 File Offset: 0x001770C4
		public void Welcome(IPeerNeighbor neighbor, WelcomeInfo welcomeInfo)
		{
			if (this.state != PeerConnector.State.Opened)
			{
				return;
			}
			PeerCloseReason peerCloseReason = PeerCloseReason.None;
			if (!neighbor.IsInitiator || !welcomeInfo.HasBody() || (neighbor.State != PeerNeighborState.Connecting && neighbor.State != PeerNeighborState.Closed))
			{
				peerCloseReason = PeerCloseReason.InvalidNeighbor;
			}
			else if (this.RemoveTimer(neighbor))
			{
				string action = "http://schemas.microsoft.com/net/2006/05/peer/Refuse";
				IPeerNeighbor peerNeighbor;
				PeerCloseReason peerCloseReason2;
				this.ValidateNeighbor(neighbor, welcomeInfo.NodeId, out peerNeighbor, out peerCloseReason2, out action);
				if (neighbor != peerNeighbor)
				{
					if (this.maintainer.AddReferrals(welcomeInfo.Referrals, neighbor))
					{
						if (!neighbor.TrySetState(PeerNeighborState.Connected) && neighbor.State < PeerNeighborState.Faulted)
						{
							throw Fx.AssertAndThrow("Neighbor state expected to be >= Faulted; it is " + neighbor.State.ToString());
						}
						if (peerNeighbor != null)
						{
							this.SendTerminatingMessage(peerNeighbor, action, peerCloseReason2);
							this.neighborManager.CloseNeighbor(peerNeighbor, peerCloseReason2, PeerCloseInitiator.LocalNode);
						}
					}
					else
					{
						peerCloseReason = PeerCloseReason.InvalidNeighbor;
					}
				}
				else
				{
					peerCloseReason = peerCloseReason2;
				}
			}
			if (peerCloseReason != PeerCloseReason.None)
			{
				this.SendTerminatingMessage(neighbor, "http://schemas.microsoft.com/net/2006/05/peer/Disconnect", peerCloseReason);
				this.neighborManager.CloseNeighbor(neighbor, peerCloseReason, PeerCloseInitiator.LocalNode);
			}
		}

		// Token: 0x06006514 RID: 25876 RVA: 0x00178FBC File Offset: 0x001771BC
		private bool RemoveTimer(IPeerNeighbor neighbor)
		{
			IOThreadTimer iothreadTimer = null;
			bool flag = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state == PeerConnector.State.Opened && this.timerTable.TryGetValue(neighbor, out iothreadTimer))
				{
					flag = this.timerTable.Remove(neighbor);
				}
			}
			if (iothreadTimer != null)
			{
				iothreadTimer.Cancel();
				if (!flag)
				{
					throw Fx.AssertAndThrow("Neighbor key should have beeen removed from the table");
				}
			}
			return flag;
		}

		// Token: 0x06006515 RID: 25877 RVA: 0x0017903C File Offset: 0x0017723C
		private void SendConnect(IPeerNeighbor neighbor)
		{
			if (neighbor.State == PeerNeighborState.Connecting && this.state == PeerConnector.State.Opened)
			{
				PeerNodeAddress listenAddress = this.config.GetListenAddress(true);
				if (listenAddress != null)
				{
					ConnectInfo typedMessage = new ConnectInfo(this.config.NodeId, listenAddress);
					Message message = this.ConnectInfoMessageConverter.ToMessage(typedMessage, MessageVersion.Soap12WSAddressing10);
					this.SendMessageToNeighbor(neighbor, message, new PeerMessageHelpers.CleanupCallback(this.OnConnectFailure));
				}
			}
		}

		// Token: 0x06006516 RID: 25878 RVA: 0x001790A4 File Offset: 0x001772A4
		private void SendTerminatingMessage(IPeerNeighbor neighbor, string action, PeerCloseReason closeReason)
		{
			if (this.state != PeerConnector.State.Opened || closeReason == PeerCloseReason.InvalidNeighbor)
			{
				return;
			}
			if (neighbor.TrySetState(PeerNeighborState.Disconnecting))
			{
				Referral[] referrals = this.maintainer.GetReferrals();
				Message message;
				if (action == "http://schemas.microsoft.com/net/2006/05/peer/Disconnect")
				{
					DisconnectInfo typedMessage = new DisconnectInfo((DisconnectReason)closeReason, referrals);
					message = this.DisconnectInfoMessageConverter.ToMessage(typedMessage, MessageVersion.Soap12WSAddressing10);
				}
				else
				{
					RefuseInfo typedMessage2 = new RefuseInfo((RefuseReason)closeReason, referrals);
					message = this.RefuseInfoMessageConverter.ToMessage(typedMessage2, MessageVersion.Soap12WSAddressing10);
				}
				this.SendMessageToNeighbor(neighbor, message, null);
				return;
			}
			if (neighbor.State < PeerNeighborState.Disconnecting)
			{
				throw Fx.AssertAndThrow("Neighbor state expected to be >= Disconnecting; it is " + neighbor.State.ToString());
			}
		}

		// Token: 0x06006517 RID: 25879 RVA: 0x00179150 File Offset: 0x00177350
		private void SendWelcome(IPeerNeighbor neighbor)
		{
			if (this.state == PeerConnector.State.Opened)
			{
				Referral[] referrals = this.maintainer.GetReferrals();
				WelcomeInfo typedMessage = new WelcomeInfo(this.config.NodeId, referrals);
				Message message = this.WelcomeInfoMessageConverter.ToMessage(typedMessage, MessageVersion.Soap12WSAddressing10);
				this.SendMessageToNeighbor(neighbor, message, new PeerMessageHelpers.CleanupCallback(this.OnConnectFailure));
			}
		}

		// Token: 0x06006518 RID: 25880 RVA: 0x001791AC File Offset: 0x001773AC
		private void ValidateNeighbor(IPeerNeighbor neighbor, ulong neighborNodeId, out IPeerNeighbor neighborToClose, out PeerCloseReason closeReason, out string action)
		{
			neighborToClose = null;
			closeReason = PeerCloseReason.None;
			action = null;
			if (neighborNodeId == 0UL)
			{
				neighborToClose = neighbor;
				closeReason = PeerCloseReason.InvalidNeighbor;
			}
			else if (neighborNodeId == this.config.NodeId)
			{
				neighborToClose = neighbor;
				closeReason = PeerCloseReason.DuplicateNodeId;
			}
			else
			{
				try
				{
					neighbor.NodeId = neighborNodeId;
				}
				catch (ObjectDisposedException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					return;
				}
				IPeerNeighbor peerNeighbor = this.neighborManager.FindDuplicateNeighbor(neighborNodeId, neighbor);
				if (peerNeighbor != null && this.neighborManager.PingNeighbor(peerNeighbor))
				{
					closeReason = PeerCloseReason.DuplicateNeighbor;
					if (neighbor.IsInitiator == peerNeighbor.IsInitiator)
					{
						neighborToClose = neighbor;
					}
					else if (this.config.NodeId > neighborNodeId)
					{
						neighborToClose = (neighbor.IsInitiator ? neighbor : peerNeighbor);
					}
					else
					{
						neighborToClose = (neighbor.IsInitiator ? peerNeighbor : neighbor);
					}
				}
			}
			if (neighborToClose != null && neighborToClose != neighbor)
			{
				if (neighborToClose.State == PeerNeighborState.Connected)
				{
					action = "http://schemas.microsoft.com/net/2006/05/peer/Disconnect";
					return;
				}
				if (!neighborToClose.IsInitiator && neighborToClose.State == PeerNeighborState.Connecting)
				{
					action = "http://schemas.microsoft.com/net/2006/05/peer/Refuse";
				}
			}
		}

		// Token: 0x040039FC RID: 14844
		private PeerNodeConfig config;

		// Token: 0x040039FD RID: 14845
		private PeerMaintainer maintainer;

		// Token: 0x040039FE RID: 14846
		private PeerNeighborManager neighborManager;

		// Token: 0x040039FF RID: 14847
		private PeerConnector.State state;

		// Token: 0x04003A00 RID: 14848
		private object thisLock;

		// Token: 0x04003A01 RID: 14849
		private TypedMessageConverter connectInfoMessageConverter;

		// Token: 0x04003A02 RID: 14850
		private TypedMessageConverter disconnectInfoMessageConverter;

		// Token: 0x04003A03 RID: 14851
		private TypedMessageConverter refuseInfoMessageConverter;

		// Token: 0x04003A04 RID: 14852
		private TypedMessageConverter welcomeInfoMessageConverter;

		// Token: 0x04003A05 RID: 14853
		private Dictionary<IPeerNeighbor, IOThreadTimer> timerTable;

		// Token: 0x02000E59 RID: 3673
		private enum State
		{
			// Token: 0x04004AB5 RID: 19125
			Created,
			// Token: 0x04004AB6 RID: 19126
			Opened,
			// Token: 0x04004AB7 RID: 19127
			Closed,
			// Token: 0x04004AB8 RID: 19128
			Closing
		}
	}
}
