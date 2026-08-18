using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Runtime;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Dispatcher;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A1E RID: 2590
	internal class PeerNeighborManager
	{
		// Token: 0x1400004D RID: 77
		// (add) Token: 0x0600667A RID: 26234 RVA: 0x0017DB74 File Offset: 0x0017BD74
		// (remove) Token: 0x0600667B RID: 26235 RVA: 0x0017DBAC File Offset: 0x0017BDAC
		public event EventHandler<PeerNeighborCloseEventArgs> NeighborClosed;

		// Token: 0x1400004E RID: 78
		// (add) Token: 0x0600667C RID: 26236 RVA: 0x0017DBE4 File Offset: 0x0017BDE4
		// (remove) Token: 0x0600667D RID: 26237 RVA: 0x0017DC1C File Offset: 0x0017BE1C
		public event EventHandler<PeerNeighborCloseEventArgs> NeighborClosing;

		// Token: 0x1400004F RID: 79
		// (add) Token: 0x0600667E RID: 26238 RVA: 0x0017DC54 File Offset: 0x0017BE54
		// (remove) Token: 0x0600667F RID: 26239 RVA: 0x0017DC8C File Offset: 0x0017BE8C
		public event EventHandler NeighborConnected;

		// Token: 0x14000050 RID: 80
		// (add) Token: 0x06006680 RID: 26240 RVA: 0x0017DCC4 File Offset: 0x0017BEC4
		// (remove) Token: 0x06006681 RID: 26241 RVA: 0x0017DCFC File Offset: 0x0017BEFC
		public event EventHandler NeighborOpened;

		// Token: 0x14000051 RID: 81
		// (add) Token: 0x06006682 RID: 26242 RVA: 0x0017DD34 File Offset: 0x0017BF34
		// (remove) Token: 0x06006683 RID: 26243 RVA: 0x0017DD6C File Offset: 0x0017BF6C
		public event EventHandler Offline;

		// Token: 0x14000052 RID: 82
		// (add) Token: 0x06006684 RID: 26244 RVA: 0x0017DDA4 File Offset: 0x0017BFA4
		// (remove) Token: 0x06006685 RID: 26245 RVA: 0x0017DDDC File Offset: 0x0017BFDC
		public event EventHandler Online;

		// Token: 0x06006686 RID: 26246 RVA: 0x0017DE11 File Offset: 0x0017C011
		public PeerNeighborManager(PeerIPHelper ipHelper, PeerNodeConfig config) : this(ipHelper, config, null)
		{
		}

		// Token: 0x06006687 RID: 26247 RVA: 0x0017DE1C File Offset: 0x0017C01C
		public PeerNeighborManager(PeerIPHelper ipHelper, PeerNodeConfig config, IPeerNodeMessageHandling messageHandler)
		{
			this.neighborList = new List<PeerNeighborManager.PeerNeighbor>();
			this.connectedNeighborList = new List<IPeerNeighbor>();
			this.ipHelper = ipHelper;
			this.messageHandler = messageHandler;
			this.config = config;
			this.thisLock = new object();
			this.traceRecord = new PeerNodeTraceRecord(config.NodeId);
			this.state = PeerNeighborManager.State.Created;
		}

		// Token: 0x170018A4 RID: 6308
		// (get) Token: 0x06006688 RID: 26248 RVA: 0x0017DE7D File Offset: 0x0017C07D
		public int ConnectedNeighborCount
		{
			get
			{
				return this.connectedNeighborList.Count;
			}
		}

		// Token: 0x170018A5 RID: 6309
		// (get) Token: 0x06006689 RID: 26249 RVA: 0x0017DE8C File Offset: 0x0017C08C
		public int NonClosingNeighborCount
		{
			get
			{
				int num = 0;
				foreach (IPeerNeighbor peerNeighbor in this.connectedNeighborList)
				{
					PeerNeighborManager.PeerNeighbor peerNeighbor2 = (PeerNeighborManager.PeerNeighbor)peerNeighbor;
					if (!peerNeighbor2.IsClosing)
					{
						num++;
					}
				}
				return num;
			}
		}

		// Token: 0x170018A6 RID: 6310
		// (get) Token: 0x0600668A RID: 26250 RVA: 0x0017DEEC File Offset: 0x0017C0EC
		public bool IsOnline
		{
			get
			{
				return this.isOnline;
			}
		}

		// Token: 0x170018A7 RID: 6311
		// (get) Token: 0x0600668B RID: 26251 RVA: 0x0017DEF4 File Offset: 0x0017C0F4
		public int NeighborCount
		{
			get
			{
				return this.neighborList.Count;
			}
		}

		// Token: 0x170018A8 RID: 6312
		// (get) Token: 0x0600668C RID: 26252 RVA: 0x0017DF01 File Offset: 0x0017C101
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x0600668D RID: 26253 RVA: 0x0017DF0C File Offset: 0x0017C10C
		private void Abort(PeerNeighborManager.PeerNeighbor[] neighbors)
		{
			foreach (PeerNeighborManager.PeerNeighbor peerNeighbor in neighbors)
			{
				peerNeighbor.Abort(PeerCloseReason.LeavingMesh, PeerCloseInitiator.LocalNode);
			}
		}

		// Token: 0x0600668E RID: 26254 RVA: 0x0017DF38 File Offset: 0x0017C138
		public IAsyncResult BeginOpenNeighbor(PeerNodeAddress remoteAddress, TimeSpan timeout, AsyncCallback callback, object asyncState)
		{
			this.ThrowIfNotOpen();
			ReadOnlyCollection<IPAddress> ipAddresses = this.ipHelper.SortAddresses(remoteAddress.IPAddresses);
			PeerNodeAddress remoteAddress2 = new PeerNodeAddress(remoteAddress.EndpointAddress, ipAddresses);
			return this.BeginOpenNeighborInternal(remoteAddress2, timeout, callback, asyncState);
		}

		// Token: 0x0600668F RID: 26255 RVA: 0x0017DF78 File Offset: 0x0017C178
		internal IAsyncResult BeginOpenNeighborInternal(PeerNodeAddress remoteAddress, TimeSpan timeout, AsyncCallback callback, object asyncState)
		{
			PeerNeighborManager.PeerNeighbor neighbor = new PeerNeighborManager.PeerNeighbor(this.config, this.messageHandler);
			this.RegisterForNeighborEvents(neighbor);
			return new PeerNeighborManager.NeighborOpenAsyncResult(neighbor, remoteAddress, this.serviceBinding, this.service, new PeerNeighborManager.ClosedCallback(this.Closed), timeout, callback, asyncState);
		}

		// Token: 0x06006690 RID: 26256 RVA: 0x0017DFC4 File Offset: 0x0017C1C4
		private void Cleanup(bool graceful)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				if (graceful && this.shutdownEvent != null)
				{
					this.shutdownEvent.Close();
				}
				this.state = PeerNeighborManager.State.Shutdown;
			}
		}

		// Token: 0x06006691 RID: 26257 RVA: 0x0017E01C File Offset: 0x0017C21C
		private void ClearNeighborList()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.neighborList.Clear();
				this.connectedNeighborList.Clear();
			}
		}

		// Token: 0x06006692 RID: 26258 RVA: 0x0017E06C File Offset: 0x0017C26C
		public void Close()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.state = PeerNeighborManager.State.Closed;
			}
		}

		// Token: 0x06006693 RID: 26259 RVA: 0x0017E0B0 File Offset: 0x0017C2B0
		private bool Closed()
		{
			return this.state != PeerNeighborManager.State.Opened;
		}

		// Token: 0x06006694 RID: 26260 RVA: 0x0017E0BE File Offset: 0x0017C2BE
		public void CloseNeighbor(IPeerNeighbor neighbor, PeerCloseReason closeReason, PeerCloseInitiator closeInitiator)
		{
			this.CloseNeighbor(neighbor, closeReason, closeInitiator, null);
		}

		// Token: 0x06006695 RID: 26261 RVA: 0x0017E0CC File Offset: 0x0017C2CC
		public void CloseNeighbor(IPeerNeighbor neighbor, PeerCloseReason closeReason, PeerCloseInitiator closeInitiator, Exception closeException)
		{
			PeerNeighborManager.PeerNeighbor peerNeighbor = (PeerNeighborManager.PeerNeighbor)neighbor;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state == PeerNeighborManager.State.Created)
				{
					throw Fx.AssertAndThrow("Neighbor Manager is not expected to be in Created state");
				}
				if (!this.neighborList.Contains(peerNeighbor))
				{
					return;
				}
			}
			if (closeReason != PeerCloseReason.InvalidNeighbor)
			{
				if (!peerNeighbor.IsClosing)
				{
					this.InvokeAsyncNeighborClose(peerNeighbor, closeReason, closeInitiator, closeException, null);
					return;
				}
			}
			else
			{
				peerNeighbor.Abort(closeReason, closeInitiator);
			}
		}

		// Token: 0x06006696 RID: 26262 RVA: 0x0017E154 File Offset: 0x0017C354
		public IPeerNeighbor EndOpenNeighbor(IAsyncResult result)
		{
			return PeerNeighborManager.NeighborOpenAsyncResult.End(result);
		}

		// Token: 0x06006697 RID: 26263 RVA: 0x0017E15C File Offset: 0x0017C35C
		private static void FireEvent(EventHandler handler, PeerNeighborManager manager)
		{
			if (handler != null)
			{
				handler(manager, EventArgs.Empty);
			}
		}

		// Token: 0x06006698 RID: 26264 RVA: 0x0017E16D File Offset: 0x0017C36D
		private static void FireEvent(EventHandler handler, PeerNeighborManager.PeerNeighbor neighbor)
		{
			if (handler != null)
			{
				handler(neighbor, EventArgs.Empty);
			}
		}

		// Token: 0x06006699 RID: 26265 RVA: 0x0017E180 File Offset: 0x0017C380
		private static void FireEvent(EventHandler<PeerNeighborCloseEventArgs> handler, PeerNeighborManager.PeerNeighbor neighbor, PeerCloseReason closeReason, PeerCloseInitiator closeInitiator, Exception closeException)
		{
			if (handler != null)
			{
				PeerNeighborCloseEventArgs e = new PeerNeighborCloseEventArgs(closeReason, closeInitiator, closeException);
				handler(neighbor, e);
			}
		}

		// Token: 0x0600669A RID: 26266 RVA: 0x0017E1A2 File Offset: 0x0017C3A2
		public IPeerNeighbor FindDuplicateNeighbor(ulong nodeId)
		{
			return this.FindDuplicateNeighbor(nodeId, null);
		}

		// Token: 0x0600669B RID: 26267 RVA: 0x0017E1AC File Offset: 0x0017C3AC
		public IPeerNeighbor FindDuplicateNeighbor(ulong nodeId, IPeerNeighbor skipNeighbor)
		{
			PeerNeighborManager.PeerNeighbor result = null;
			if (nodeId != 0UL)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					foreach (PeerNeighborManager.PeerNeighbor peerNeighbor in this.neighborList)
					{
						if (peerNeighbor != (PeerNeighborManager.PeerNeighbor)skipNeighbor && peerNeighbor.NodeId == nodeId && !peerNeighbor.IsClosing && peerNeighbor.State < PeerNeighborState.Disconnecting)
						{
							result = peerNeighbor;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600669C RID: 26268 RVA: 0x0017E254 File Offset: 0x0017C454
		public bool PingNeighbor(IPeerNeighbor peer)
		{
			bool result = true;
			Message request = Message.CreateMessage(MessageVersion.Soap12WSAddressing10, "http://schemas.microsoft.com/net/2006/05/peer/Ping");
			try
			{
				peer.Ping(request);
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				peer.Abort(PeerCloseReason.InternalFailure, PeerCloseInitiator.LocalNode);
				result = false;
			}
			return result;
		}

		// Token: 0x0600669D RID: 26269 RVA: 0x0017E2AC File Offset: 0x0017C4AC
		public void PingNeighbors()
		{
			List<IPeerNeighbor> connectedNeighbors = this.GetConnectedNeighbors();
			foreach (IPeerNeighbor peer in connectedNeighbors)
			{
				this.PingNeighbor(peer);
			}
		}

		// Token: 0x0600669E RID: 26270 RVA: 0x0017E304 File Offset: 0x0017C504
		public IPeerNeighbor FindDuplicateNeighbor(PeerNodeAddress address)
		{
			return this.FindDuplicateNeighbor(address, null);
		}

		// Token: 0x0600669F RID: 26271 RVA: 0x0017E310 File Offset: 0x0017C510
		public IPeerNeighbor FindDuplicateNeighbor(PeerNodeAddress address, IPeerNeighbor skipNeighbor)
		{
			PeerNeighborManager.PeerNeighbor result = null;
			object obj = this.ThisLock;
			lock (obj)
			{
				foreach (PeerNeighborManager.PeerNeighbor peerNeighbor in this.neighborList)
				{
					if (peerNeighbor != (PeerNeighborManager.PeerNeighbor)skipNeighbor && peerNeighbor.ListenAddress != null && peerNeighbor.ListenAddress.ServicePath == address.ServicePath && !peerNeighbor.IsClosing && peerNeighbor.State < PeerNeighborState.Disconnecting)
					{
						result = peerNeighbor;
						break;
					}
				}
			}
			return result;
		}

		// Token: 0x060066A0 RID: 26272 RVA: 0x0017E3CC File Offset: 0x0017C5CC
		public List<IPeerNeighbor> GetConnectedNeighbors()
		{
			object obj = this.ThisLock;
			List<IPeerNeighbor> result;
			lock (obj)
			{
				result = new List<IPeerNeighbor>(this.connectedNeighborList);
			}
			return result;
		}

		// Token: 0x060066A1 RID: 26273 RVA: 0x0017E414 File Offset: 0x0017C614
		public IPeerNeighbor GetNeighborFromProxy(IPeerProxy proxy)
		{
			PeerNeighborManager.PeerNeighbor result = null;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state == PeerNeighborManager.State.Opened)
				{
					foreach (PeerNeighborManager.PeerNeighbor peerNeighbor in this.neighborList)
					{
						if (peerNeighbor.Proxy == proxy)
						{
							result = peerNeighbor;
							break;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x060066A2 RID: 26274 RVA: 0x0017E4A8 File Offset: 0x0017C6A8
		private void InvokeAsyncNeighborClose(PeerNeighborManager.PeerNeighbor neighbor, PeerCloseReason closeReason, PeerCloseInitiator closeInitiator, Exception closeException, IAsyncResult endResult)
		{
			try
			{
				if (endResult == null)
				{
					IAsyncResult asyncResult = neighbor.BeginClose(closeReason, closeInitiator, closeException, Fx.ThunkCallback(new AsyncCallback(this.OnNeighborClosedCallback)), neighbor);
					if (asyncResult.CompletedSynchronously)
					{
						neighbor.EndClose(asyncResult);
					}
				}
				else
				{
					neighbor.EndClose(endResult);
				}
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
				neighbor.TraceEventHelper(TraceEventType.Warning, 262195, SR.GetString("TraceCodePeerNeighborCloseFailed"), ex);
				if (!(ex is InvalidOperationException) && !(ex is CommunicationException) && !(ex is TimeoutException))
				{
					throw;
				}
				neighbor.Abort();
			}
		}

		// Token: 0x060066A3 RID: 26275 RVA: 0x0017E550 File Offset: 0x0017C750
		private void OnNeighborClosed(object source, EventArgs args)
		{
			this.RemoveNeighbor((PeerNeighborManager.PeerNeighbor)source);
		}

		// Token: 0x060066A4 RID: 26276 RVA: 0x0017E55E File Offset: 0x0017C75E
		private void OnNeighborClosedCallback(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				this.InvokeAsyncNeighborClose((PeerNeighborManager.PeerNeighbor)result.AsyncState, PeerCloseReason.None, PeerCloseInitiator.LocalNode, null, result);
			}
		}

		// Token: 0x060066A5 RID: 26277 RVA: 0x0017E580 File Offset: 0x0017C780
		private void OnNeighborClosing(object source, EventArgs args)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.connectedNeighborList.Remove((IPeerNeighbor)source);
			}
		}

		// Token: 0x060066A6 RID: 26278 RVA: 0x0017E5CC File Offset: 0x0017C7CC
		private void OnNeighborConnected(object source, EventArgs args)
		{
			PeerNeighborManager.PeerNeighbor peerNeighbor = (PeerNeighborManager.PeerNeighbor)source;
			bool flag = false;
			bool flag2 = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.neighborList.Contains(peerNeighbor))
				{
					flag = true;
					this.connectedNeighborList.Add(peerNeighbor);
					if (!this.isOnline)
					{
						this.isOnline = true;
						flag2 = true;
					}
				}
			}
			if (flag)
			{
				PeerNeighborManager.FireEvent(this.NeighborConnected, peerNeighbor);
			}
			else
			{
				peerNeighbor.TraceEventHelper(TraceEventType.Warning, 262198, SR.GetString("TraceCodePeerNeighborNotFound"));
			}
			if (flag2)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 262204, SR.GetString("TraceCodePeerNeighborManagerOnline"), this.traceRecord, this, null);
				}
				PeerNeighborManager.FireEvent(this.Online, this);
			}
		}

		// Token: 0x060066A7 RID: 26279 RVA: 0x0017E69C File Offset: 0x0017C89C
		private void OnNeighborOpened(object source, EventArgs args)
		{
			PeerNeighborManager.PeerNeighbor peerNeighbor = (PeerNeighborManager.PeerNeighbor)source;
			bool flag = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.state == PeerNeighborManager.State.Opened)
				{
					if (peerNeighbor.State != PeerNeighborState.Opened)
					{
						throw Fx.AssertAndThrow("Neighbor expected to be in Opened state");
					}
					this.neighborList.Add(peerNeighbor);
					flag = true;
				}
			}
			if (flag)
			{
				PeerNeighborManager.FireEvent(this.NeighborOpened, peerNeighbor);
				return;
			}
			peerNeighbor.Abort();
			peerNeighbor.TraceEventHelper(TraceEventType.Warning, 262197, SR.GetString("TraceCodePeerNeighborNotAccepted"));
		}

		// Token: 0x060066A8 RID: 26280 RVA: 0x0017E738 File Offset: 0x0017C938
		public void Open(Binding serviceBinding, PeerService service)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.service = service;
				this.serviceBinding = serviceBinding;
				if (this.state != PeerNeighborManager.State.Created)
				{
					throw Fx.AssertAndThrow("Neighbor Manager is expected to be in Created state");
				}
				this.state = PeerNeighborManager.State.Opened;
			}
		}

		// Token: 0x060066A9 RID: 26281 RVA: 0x0017E79C File Offset: 0x0017C99C
		public bool ProcessIncomingChannel(IClientChannel channel)
		{
			bool result = false;
			IPeerProxy callbackInstance = (IPeerProxy)channel;
			if (this.state == PeerNeighborManager.State.Opened)
			{
				PeerNeighborManager.PeerNeighbor peerNeighbor = new PeerNeighborManager.PeerNeighbor(this.config, this.messageHandler);
				this.RegisterForNeighborEvents(peerNeighbor);
				peerNeighbor.Open(callbackInstance);
				result = true;
			}
			else if (DiagnosticUtility.ShouldTraceWarning)
			{
				TraceUtility.TraceEvent(TraceEventType.Warning, 262197, SR.GetString("TraceCodePeerNeighborNotAccepted"), this.traceRecord, this, null);
			}
			return result;
		}

		// Token: 0x060066AA RID: 26282 RVA: 0x0017E804 File Offset: 0x0017CA04
		private void RegisterForNeighborEvents(PeerNeighborManager.PeerNeighbor neighbor)
		{
			neighbor.Opened += this.OnNeighborOpened;
			neighbor.Connected += this.OnNeighborConnected;
			neighbor.Closed += this.OnNeighborClosed;
			neighbor.Closing += this.NeighborClosing;
			neighbor.Disconnecting += this.OnNeighborClosing;
			neighbor.Disconnected += this.OnNeighborClosing;
		}

		// Token: 0x060066AB RID: 26283 RVA: 0x0017E878 File Offset: 0x0017CA78
		private void RemoveNeighbor(PeerNeighborManager.PeerNeighbor neighbor)
		{
			bool flag = false;
			bool flag2 = false;
			object obj = this.ThisLock;
			lock (obj)
			{
				if (this.neighborList.Contains(neighbor))
				{
					flag = true;
					this.neighborList.Remove(neighbor);
					this.connectedNeighborList.Remove(neighbor);
					if (this.isOnline && this.connectedNeighborList.Count == 0)
					{
						this.isOnline = false;
						flag2 = true;
					}
					if (this.neighborList.Count == 0 && this.shutdownEvent != null)
					{
						this.shutdownEvent.Set();
					}
				}
			}
			if (flag)
			{
				PeerNeighborManager.FireEvent(this.NeighborClosed, neighbor, neighbor.CloseReason, neighbor.CloseInitiator, neighbor.CloseException);
			}
			else if (DiagnosticUtility.ShouldTraceWarning)
			{
				neighbor.TraceEventHelper(TraceEventType.Warning, 262198, SR.GetString("TraceCodePeerNeighborNotFound"));
			}
			if (flag2)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 262203, SR.GetString("TraceCodePeerNeighborManagerOffline"), this.traceRecord, this, null);
				}
				PeerNeighborManager.FireEvent(this.Offline, this);
			}
		}

		// Token: 0x060066AC RID: 26284 RVA: 0x0017E994 File Offset: 0x0017CB94
		public void Shutdown(bool graceful, TimeSpan timeout)
		{
			PeerNeighborManager.PeerNeighbor[] array = null;
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			try
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.state == PeerNeighborManager.State.Shutdown || this.state == PeerNeighborManager.State.Closed)
					{
						return;
					}
					this.state = PeerNeighborManager.State.ShuttingDown;
					array = this.neighborList.ToArray();
					if (graceful && array.Length != 0)
					{
						this.shutdownEvent = new ManualResetEvent(false);
					}
				}
				if (graceful)
				{
					this.Shutdown(array, timeoutHelper.RemainingTime());
				}
				else
				{
					this.Abort(array);
				}
			}
			catch (Exception exception)
			{
				if (Fx.IsFatal(exception))
				{
					throw;
				}
				try
				{
					this.ClearNeighborList();
				}
				catch (Exception exception2)
				{
					if (Fx.IsFatal(exception2))
					{
						throw;
					}
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
				}
				throw;
			}
			finally
			{
				this.Cleanup(graceful);
			}
		}

		// Token: 0x060066AD RID: 26285 RVA: 0x0017EA8C File Offset: 0x0017CC8C
		private void Shutdown(PeerNeighborManager.PeerNeighbor[] neighbors, TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			foreach (PeerNeighborManager.PeerNeighbor neighbor in neighbors)
			{
				this.CloseNeighbor(neighbor, PeerCloseReason.LeavingMesh, PeerCloseInitiator.LocalNode, null);
			}
			if (neighbors.Length != 0 && !TimeoutHelper.WaitOne(this.shutdownEvent, timeoutHelper.RemainingTime()))
			{
				this.Abort(neighbors);
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new TimeoutException());
			}
		}

		// Token: 0x060066AE RID: 26286 RVA: 0x0017EAEE File Offset: 0x0017CCEE
		private void ThrowIfNotOpen()
		{
			if (this.state == PeerNeighborManager.State.Created)
			{
				throw Fx.AssertAndThrow("Neighbor manager not expected to be in Created state");
			}
			if (this.Closed())
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(this.ToString()));
			}
		}

		// Token: 0x060066AF RID: 26287 RVA: 0x0017EB24 File Offset: 0x0017CD24
		public IPeerNeighbor SlowestNeighbor()
		{
			List<IPeerNeighbor> connectedNeighbors = this.GetConnectedNeighbors();
			IPeerNeighbor result = null;
			int num = 32;
			foreach (IPeerNeighbor peerNeighbor in connectedNeighbors)
			{
				UtilityExtension utility = peerNeighbor.Utility;
				if (utility != null && peerNeighbor.IsConnected && utility.PendingMessages > num)
				{
					result = peerNeighbor;
					num = utility.PendingMessages;
				}
			}
			return result;
		}

		// Token: 0x04003AFD RID: 15101
		private PeerNodeConfig config;

		// Token: 0x04003AFE RID: 15102
		private List<IPeerNeighbor> connectedNeighborList;

		// Token: 0x04003AFF RID: 15103
		private bool isOnline;

		// Token: 0x04003B00 RID: 15104
		private PeerIPHelper ipHelper;

		// Token: 0x04003B01 RID: 15105
		private List<PeerNeighborManager.PeerNeighbor> neighborList;

		// Token: 0x04003B02 RID: 15106
		private ManualResetEvent shutdownEvent;

		// Token: 0x04003B03 RID: 15107
		private PeerNeighborManager.State state;

		// Token: 0x04003B04 RID: 15108
		private object thisLock;

		// Token: 0x04003B05 RID: 15109
		private PeerNodeTraceRecord traceRecord;

		// Token: 0x04003B06 RID: 15110
		private PeerService service;

		// Token: 0x04003B07 RID: 15111
		private Binding serviceBinding;

		// Token: 0x04003B08 RID: 15112
		private IPeerNodeMessageHandling messageHandler;

		// Token: 0x02000E61 RID: 3681
		// (Invoke) Token: 0x06008362 RID: 33634
		private delegate bool ClosedCallback();

		// Token: 0x02000E62 RID: 3682
		private enum State
		{
			// Token: 0x04004AD1 RID: 19153
			Created,
			// Token: 0x04004AD2 RID: 19154
			Opened,
			// Token: 0x04004AD3 RID: 19155
			ShuttingDown,
			// Token: 0x04004AD4 RID: 19156
			Shutdown,
			// Token: 0x04004AD5 RID: 19157
			Closed
		}

		// Token: 0x02000E63 RID: 3683
		private class PeerNeighbor : IPeerNeighbor, IExtensibleObject<IPeerNeighbor>, IInputSessionShutdown
		{
			// Token: 0x14000067 RID: 103
			// (add) Token: 0x06008365 RID: 33637 RVA: 0x001E6324 File Offset: 0x001E4524
			// (remove) Token: 0x06008366 RID: 33638 RVA: 0x001E635C File Offset: 0x001E455C
			public event EventHandler Closed;

			// Token: 0x14000068 RID: 104
			// (add) Token: 0x06008367 RID: 33639 RVA: 0x001E6394 File Offset: 0x001E4594
			// (remove) Token: 0x06008368 RID: 33640 RVA: 0x001E63CC File Offset: 0x001E45CC
			public event EventHandler<PeerNeighborCloseEventArgs> Closing;

			// Token: 0x14000069 RID: 105
			// (add) Token: 0x06008369 RID: 33641 RVA: 0x001E6404 File Offset: 0x001E4604
			// (remove) Token: 0x0600836A RID: 33642 RVA: 0x001E643C File Offset: 0x001E463C
			public event EventHandler Connected;

			// Token: 0x1400006A RID: 106
			// (add) Token: 0x0600836B RID: 33643 RVA: 0x001E6474 File Offset: 0x001E4674
			// (remove) Token: 0x0600836C RID: 33644 RVA: 0x001E64AC File Offset: 0x001E46AC
			public event EventHandler Disconnected;

			// Token: 0x1400006B RID: 107
			// (add) Token: 0x0600836D RID: 33645 RVA: 0x001E64E4 File Offset: 0x001E46E4
			// (remove) Token: 0x0600836E RID: 33646 RVA: 0x001E651C File Offset: 0x001E471C
			public event EventHandler Disconnecting;

			// Token: 0x1400006C RID: 108
			// (add) Token: 0x0600836F RID: 33647 RVA: 0x001E6554 File Offset: 0x001E4754
			// (remove) Token: 0x06008370 RID: 33648 RVA: 0x001E658C File Offset: 0x001E478C
			public event EventHandler Opened;

			// Token: 0x06008371 RID: 33649 RVA: 0x001E65C4 File Offset: 0x001E47C4
			public PeerNeighbor(PeerNodeConfig config, IPeerNodeMessageHandling messageHandler)
			{
				this.closeReason = PeerCloseReason.None;
				this.closeInitiator = PeerCloseInitiator.LocalNode;
				this.config = config;
				this.state = PeerNeighborState.Created;
				this.extensions = new ExtensionCollection<IPeerNeighbor>(this, this.thisLock);
				this.messageHandler = messageHandler;
			}

			// Token: 0x17001D0D RID: 7437
			// (get) Token: 0x06008372 RID: 33650 RVA: 0x001E6617 File Offset: 0x001E4817
			// (set) Token: 0x06008373 RID: 33651 RVA: 0x001E661F File Offset: 0x001E481F
			public IPAddress ConnectIPAddress
			{
				get
				{
					return this.connectIPAddress;
				}
				set
				{
					this.connectIPAddress = value;
				}
			}

			// Token: 0x17001D0E RID: 7438
			// (get) Token: 0x06008374 RID: 33652 RVA: 0x001E6628 File Offset: 0x001E4828
			public PeerCloseReason CloseReason
			{
				get
				{
					return this.closeReason;
				}
			}

			// Token: 0x17001D0F RID: 7439
			// (get) Token: 0x06008375 RID: 33653 RVA: 0x001E6630 File Offset: 0x001E4830
			public PeerCloseInitiator CloseInitiator
			{
				get
				{
					return this.closeInitiator;
				}
			}

			// Token: 0x17001D10 RID: 7440
			// (get) Token: 0x06008376 RID: 33654 RVA: 0x001E6638 File Offset: 0x001E4838
			public Exception CloseException
			{
				get
				{
					return this.closeException;
				}
			}

			// Token: 0x17001D11 RID: 7441
			// (get) Token: 0x06008377 RID: 33655 RVA: 0x001E6640 File Offset: 0x001E4840
			public IExtensionCollection<IPeerNeighbor> Extensions
			{
				get
				{
					return this.extensions;
				}
			}

			// Token: 0x17001D12 RID: 7442
			// (get) Token: 0x06008378 RID: 33656 RVA: 0x001E6648 File Offset: 0x001E4848
			public bool IsClosing
			{
				get
				{
					return this.isClosing;
				}
			}

			// Token: 0x17001D13 RID: 7443
			// (get) Token: 0x06008379 RID: 33657 RVA: 0x001E6650 File Offset: 0x001E4850
			public bool IsConnected
			{
				get
				{
					return PeerNeighborStateHelper.IsConnected(this.state);
				}
			}

			// Token: 0x17001D14 RID: 7444
			// (get) Token: 0x0600837A RID: 33658 RVA: 0x001E6660 File Offset: 0x001E4860
			// (set) Token: 0x0600837B RID: 33659 RVA: 0x001E6690 File Offset: 0x001E4890
			public PeerNodeAddress ListenAddress
			{
				get
				{
					PeerNodeAddress peerNodeAddress = this.listenAddress;
					if (peerNodeAddress != null)
					{
						return new PeerNodeAddress(peerNodeAddress.EndpointAddress, PeerIPHelper.CloneAddresses(peerNodeAddress.IPAddresses, true));
					}
					return peerNodeAddress;
				}
				set
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (this.initiator)
						{
							throw Fx.AssertAndThrow("Cannot be set for initiator neighbors");
						}
						this.ThrowIfClosed();
						if (value != null)
						{
							this.listenAddress = value;
						}
					}
				}
			}

			// Token: 0x17001D15 RID: 7445
			// (get) Token: 0x0600837C RID: 33660 RVA: 0x001E66F0 File Offset: 0x001E48F0
			public bool IsInitiator
			{
				get
				{
					return this.initiator;
				}
			}

			// Token: 0x17001D16 RID: 7446
			// (get) Token: 0x0600837D RID: 33661 RVA: 0x001E66F8 File Offset: 0x001E48F8
			// (set) Token: 0x0600837E RID: 33662 RVA: 0x001E6700 File Offset: 0x001E4900
			public ulong NodeId
			{
				get
				{
					return this.nodeId;
				}
				set
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						this.ThrowIfClosed();
						this.nodeId = value;
					}
				}
			}

			// Token: 0x17001D17 RID: 7447
			// (get) Token: 0x0600837F RID: 33663 RVA: 0x001E6748 File Offset: 0x001E4948
			// (set) Token: 0x06008380 RID: 33664 RVA: 0x001E6750 File Offset: 0x001E4950
			public IPeerProxy Proxy
			{
				get
				{
					return this.proxy;
				}
				set
				{
					this.proxy = value;
					this.proxyChannel = (IClientChannel)this.proxy;
					this.RegisterForChannelEvents();
				}
			}

			// Token: 0x17001D18 RID: 7448
			// (get) Token: 0x06008381 RID: 33665 RVA: 0x001E6770 File Offset: 0x001E4970
			// (set) Token: 0x06008382 RID: 33666 RVA: 0x001E6778 File Offset: 0x001E4978
			public PeerNeighborState State
			{
				get
				{
					return this.state;
				}
				set
				{
					if (!PeerNeighborStateHelper.IsSettable(value))
					{
						throw Fx.AssertAndThrow("A valid settable state is expected");
					}
					this.SetState(value, PeerNeighborManager.PeerNeighbor.SetStateBehavior.ThrowException);
				}
			}

			// Token: 0x17001D19 RID: 7449
			// (get) Token: 0x06008383 RID: 33667 RVA: 0x001E6796 File Offset: 0x001E4996
			private object ThisLock
			{
				get
				{
					return this.thisLock;
				}
			}

			// Token: 0x06008384 RID: 33668 RVA: 0x001E67A0 File Offset: 0x001E49A0
			public void Abort(PeerCloseReason reason, PeerCloseInitiator closeInit)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.isClosing)
					{
						this.isClosing = true;
						this.closeReason = reason;
						this.closeInitiator = closeInit;
					}
				}
				this.Abort();
			}

			// Token: 0x06008385 RID: 33669 RVA: 0x001E6800 File Offset: 0x001E4A00
			public void Abort()
			{
				if (this.channelFactory != null)
				{
					this.channelFactory.Abort();
					return;
				}
				this.proxyChannel.Abort();
			}

			// Token: 0x06008386 RID: 33670 RVA: 0x001E6824 File Offset: 0x001E4A24
			public IAsyncResult BeginClose(PeerCloseReason reason, PeerCloseInitiator closeInit, Exception exception, AsyncCallback callback, object asyncState)
			{
				bool flag = false;
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.isClosing)
					{
						flag = true;
						this.isClosing = true;
						this.closeReason = reason;
						this.closeInitiator = closeInit;
						this.closeException = exception;
					}
				}
				if (flag)
				{
					EventHandler<PeerNeighborCloseEventArgs> closing = this.Closing;
					if (closing != null)
					{
						try
						{
							PeerNeighborCloseEventArgs e = new PeerNeighborCloseEventArgs(reason, this.closeInitiator, exception);
							closing(this, e);
						}
						catch (Exception exception2)
						{
							if (Fx.IsFatal(exception2))
							{
								throw;
							}
							this.Abort();
							throw;
						}
					}
				}
				if (this.channelFactory != null)
				{
					return this.channelFactory.BeginClose(callback, asyncState);
				}
				return this.proxyChannel.BeginClose(callback, asyncState);
			}

			// Token: 0x06008387 RID: 33671 RVA: 0x001E68F8 File Offset: 0x001E4AF8
			public IAsyncResult BeginOpen(PeerNodeAddress remoteAddress, Binding binding, PeerService service, PeerNeighborManager.ClosedCallback closedCallback, TimeSpan timeout, AsyncCallback callback, object asyncState)
			{
				this.initiator = true;
				this.listenAddress = remoteAddress;
				return new PeerNeighborManager.PeerNeighbor.OpenAsyncResult(this, remoteAddress, binding, service, closedCallback, timeout, callback, this.state);
			}

			// Token: 0x06008388 RID: 33672 RVA: 0x001E6930 File Offset: 0x001E4B30
			public IAsyncResult BeginOpenProxy(EndpointAddress remoteAddress, Binding binding, InstanceContext instanceContext, TimeSpan timeout, AsyncCallback callback, object state)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				if (this.channelFactory != null)
				{
					this.Abort();
				}
				this.channelFactory = new DuplexChannelFactory<IPeerProxy>(instanceContext, binding, new EndpointAddressBuilder(remoteAddress)
				{
					Uri = this.config.GetMeshUri()
				}.ToEndpointAddress());
				this.channelFactory.Endpoint.Behaviors.Add(new ClientViaBehavior(remoteAddress.Uri));
				this.channelFactory.Endpoint.Behaviors.Add(new PeerNeighborManager.PeerNeighborBehavior(this));
				this.channelFactory.Endpoint.Contract.Behaviors.Add(new PeerOperationSelectorBehavior(this.messageHandler));
				this.config.SecurityManager.ApplyClientSecurity(this.channelFactory);
				this.channelFactory.Open(timeoutHelper.RemainingTime());
				this.Proxy = this.channelFactory.CreateChannel();
				IAsyncResult asyncResult = this.proxyChannel.BeginOpen(timeoutHelper.RemainingTime(), callback, state);
				if (asyncResult.CompletedSynchronously)
				{
					this.proxyChannel.EndOpen(asyncResult);
				}
				return asyncResult;
			}

			// Token: 0x06008389 RID: 33673 RVA: 0x001E6A44 File Offset: 0x001E4C44
			public IAsyncResult BeginSend(Message message, AsyncCallback callback, object asyncState)
			{
				return this.proxy.BeginSend(message, callback, asyncState);
			}

			// Token: 0x0600838A RID: 33674 RVA: 0x001E6A54 File Offset: 0x001E4C54
			public IAsyncResult BeginSend(Message message, TimeSpan timeout, AsyncCallback callback, object asyncState)
			{
				return this.proxy.BeginSend(message, timeout, callback, asyncState);
			}

			// Token: 0x0600838B RID: 33675 RVA: 0x001E6A66 File Offset: 0x001E4C66
			public void Send(Message message)
			{
				this.proxy.Send(message);
			}

			// Token: 0x0600838C RID: 33676 RVA: 0x001E6A74 File Offset: 0x001E4C74
			public void CleanupProxy()
			{
				this.channelFactory.Abort();
			}

			// Token: 0x0600838D RID: 33677 RVA: 0x001E6A81 File Offset: 0x001E4C81
			public void EndClose(IAsyncResult result)
			{
				if (this.channelFactory != null)
				{
					this.channelFactory.EndClose(result);
					return;
				}
				this.proxyChannel.EndClose(result);
			}

			// Token: 0x0600838E RID: 33678 RVA: 0x001E6AA4 File Offset: 0x001E4CA4
			public void EndOpen(IAsyncResult result)
			{
				PeerNeighborManager.PeerNeighbor.OpenAsyncResult.End(result);
			}

			// Token: 0x0600838F RID: 33679 RVA: 0x001E6AAC File Offset: 0x001E4CAC
			public void EndOpenProxy(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					this.proxyChannel.EndOpen(result);
				}
			}

			// Token: 0x06008390 RID: 33680 RVA: 0x001E6AC2 File Offset: 0x001E4CC2
			public void EndSend(IAsyncResult result)
			{
				this.proxy.EndSend(result);
			}

			// Token: 0x06008391 RID: 33681 RVA: 0x001E6AD0 File Offset: 0x001E4CD0
			public Message RequestSecurityToken(Message request)
			{
				return this.proxy.ProcessRequestSecurityToken(request);
			}

			// Token: 0x06008392 RID: 33682 RVA: 0x001E6ADE File Offset: 0x001E4CDE
			public void Ping(Message request)
			{
				this.proxy.Ping(request);
			}

			// Token: 0x06008393 RID: 33683 RVA: 0x001E6AEC File Offset: 0x001E4CEC
			private void OnChannelClosed(object source, EventArgs args)
			{
				if (this.state < PeerNeighborState.Closed)
				{
					this.OnChannelClosedOrFaulted(PeerCloseReason.Closed);
				}
				if (this.closeInitiator != PeerCloseInitiator.LocalNode && this.channelFactory != null)
				{
					this.channelFactory.Abort();
				}
			}

			// Token: 0x06008394 RID: 33684 RVA: 0x001E6B1C File Offset: 0x001E4D1C
			private void OnChannelClosedOrFaulted(PeerCloseReason reason)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					PeerNeighborState previousState = this.state;
					this.state = PeerNeighborState.Closed;
					if (!this.isClosing)
					{
						this.isClosing = true;
						this.closeReason = reason;
						this.closeInitiator = PeerCloseInitiator.RemoteNode;
					}
					this.TraceClosedEvent(previousState);
				}
				this.OnStateChanged(PeerNeighborState.Closed);
			}

			// Token: 0x06008395 RID: 33685 RVA: 0x001E6B90 File Offset: 0x001E4D90
			private void OnChannelFaulted(object source, EventArgs args)
			{
				try
				{
					this.OnChannelClosedOrFaulted(PeerCloseReason.Faulted);
				}
				finally
				{
					this.Abort();
				}
			}

			// Token: 0x06008396 RID: 33686 RVA: 0x001E6BC0 File Offset: 0x001E4DC0
			private void OnChannelOpened(object source, EventArgs args)
			{
				this.SetState(PeerNeighborState.Opened, PeerNeighborManager.PeerNeighbor.SetStateBehavior.TrySet);
			}

			// Token: 0x06008397 RID: 33687 RVA: 0x001E6BCC File Offset: 0x001E4DCC
			private void OnStateChanged(PeerNeighborState newState)
			{
				EventHandler eventHandler = null;
				switch (newState)
				{
				case PeerNeighborState.Opened:
					eventHandler = this.Opened;
					break;
				case PeerNeighborState.Connected:
					eventHandler = this.Connected;
					break;
				case PeerNeighborState.Disconnecting:
					eventHandler = this.Disconnecting;
					break;
				case PeerNeighborState.Disconnected:
					eventHandler = this.Disconnected;
					break;
				case PeerNeighborState.Closed:
					eventHandler = this.Closed;
					break;
				}
				if (eventHandler != null)
				{
					eventHandler(this, EventArgs.Empty);
				}
			}

			// Token: 0x06008398 RID: 33688 RVA: 0x001E6C3F File Offset: 0x001E4E3F
			public void Open(IPeerProxy callbackInstance)
			{
				this.initiator = false;
				this.Proxy = callbackInstance;
			}

			// Token: 0x06008399 RID: 33689 RVA: 0x001E6C50 File Offset: 0x001E4E50
			private void RegisterForChannelEvents()
			{
				this.state = PeerNeighborState.Created;
				this.proxyChannel.Opened += this.OnChannelOpened;
				this.proxyChannel.Closed += this.OnChannelClosed;
				this.proxyChannel.Faulted += this.OnChannelFaulted;
			}

			// Token: 0x0600839A RID: 33690 RVA: 0x001E6CAC File Offset: 0x001E4EAC
			private bool SetState(PeerNeighborState newState, PeerNeighborManager.PeerNeighbor.SetStateBehavior behavior)
			{
				bool flag = false;
				object obj = this.ThisLock;
				lock (obj)
				{
					PeerNeighborState peerNeighborState = this.State;
					if (behavior == PeerNeighborManager.PeerNeighbor.SetStateBehavior.ThrowException)
					{
						this.ThrowIfInvalidState(newState);
					}
					if (newState > this.state)
					{
						this.state = newState;
						flag = true;
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							this.TraceEventHelper(TraceEventType.Information, 262200, SR.GetString("TraceCodePeerNeighborStateChanged"), null, null, newState, peerNeighborState);
						}
					}
					else if (DiagnosticUtility.ShouldTraceInformation)
					{
						this.TraceEventHelper(TraceEventType.Information, 262201, SR.GetString("TraceCodePeerNeighborStateChangeFailed"), null, null, peerNeighborState, newState);
					}
				}
				if (flag)
				{
					this.OnStateChanged(newState);
				}
				return flag;
			}

			// Token: 0x0600839B RID: 33691 RVA: 0x001E6D5C File Offset: 0x001E4F5C
			public bool TrySetState(PeerNeighborState newState)
			{
				if (!PeerNeighborStateHelper.IsSettable(newState))
				{
					throw Fx.AssertAndThrow("A valid settable state is expected");
				}
				return this.SetState(newState, PeerNeighborManager.PeerNeighbor.SetStateBehavior.TrySet);
			}

			// Token: 0x0600839C RID: 33692 RVA: 0x001E6D79 File Offset: 0x001E4F79
			public void ThrowIfClosed()
			{
				if (this.state == PeerNeighborState.Closed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(this.ToString()));
				}
			}

			// Token: 0x0600839D RID: 33693 RVA: 0x001E6D9C File Offset: 0x001E4F9C
			private void ThrowIfInvalidState(PeerNeighborState newState)
			{
				if (this.state == PeerNeighborState.Closed)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(this.ToString()));
				}
				if (this.state >= newState)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("PeerNeighborInvalidState", new object[]
					{
						this.state.ToString(),
						newState.ToString()
					})));
				}
			}

			// Token: 0x0600839E RID: 33694 RVA: 0x001E6E18 File Offset: 0x001E5018
			public void TraceClosedEvent(PeerNeighborState previousState)
			{
				if (DiagnosticUtility.ShouldTraceInformation)
				{
					TraceEventType severity = TraceEventType.Information;
					PeerCloseReason peerCloseReason = this.closeReason;
					if (peerCloseReason != PeerCloseReason.InvalidNeighbor)
					{
						switch (peerCloseReason)
						{
						case PeerCloseReason.DuplicateNodeId:
							break;
						case PeerCloseReason.NodeBusy:
						case PeerCloseReason.Closed:
							goto IL_3F;
						case PeerCloseReason.ConnectTimedOut:
						case PeerCloseReason.Faulted:
						case PeerCloseReason.InternalFailure:
							severity = TraceEventType.Warning;
							goto IL_3F;
						default:
							goto IL_3F;
						}
					}
					severity = TraceEventType.Error;
					IL_3F:
					PeerNeighborCloseTraceRecord extendedData = new PeerNeighborCloseTraceRecord(this.nodeId, this.config.NodeId, null, null, this.GetHashCode(), this.initiator, PeerNeighborState.Closed.ToString(), previousState.ToString(), null, this.closeInitiator.ToString(), this.closeReason.ToString());
					TraceUtility.TraceEvent(severity, 262200, SR.GetString("TraceCodePeerNeighborStateChanged"), extendedData, this, this.closeException);
				}
			}

			// Token: 0x0600839F RID: 33695 RVA: 0x001E6EE8 File Offset: 0x001E50E8
			public void TraceEventHelper(TraceEventType severity, int traceCode, string traceDescription)
			{
				PeerNeighborState peerNeighborState = this.state;
				this.TraceEventHelper(severity, traceCode, traceDescription, null, null, peerNeighborState, peerNeighborState);
			}

			// Token: 0x060083A0 RID: 33696 RVA: 0x001E6F0C File Offset: 0x001E510C
			public void TraceEventHelper(TraceEventType severity, int traceCode, string traceDescription, Exception e)
			{
				PeerNeighborState peerNeighborState = this.state;
				this.TraceEventHelper(severity, traceCode, traceDescription, e, null, peerNeighborState, peerNeighborState);
			}

			// Token: 0x060083A1 RID: 33697 RVA: 0x001E6F30 File Offset: 0x001E5130
			public void TraceEventHelper(TraceEventType severity, int traceCode, string traceDescription, Exception e, string action, PeerNeighborState nbrState, PeerNeighborState previousOrAttemptedState)
			{
				if (DiagnosticUtility.ShouldTrace(severity))
				{
					string attemptedState = null;
					string previousState = null;
					PeerNodeAddress peerNodeAddress = null;
					IPAddress ipaddress = null;
					if (nbrState >= PeerNeighborState.Opened && nbrState <= PeerNeighborState.Connected)
					{
						peerNodeAddress = this.ListenAddress;
						ipaddress = this.ConnectIPAddress;
					}
					if (traceCode == 262201)
					{
						attemptedState = previousOrAttemptedState.ToString();
					}
					else if (traceCode == 262200)
					{
						previousState = previousOrAttemptedState.ToString();
					}
					PeerNeighborTraceRecord extendedData = new PeerNeighborTraceRecord(this.nodeId, this.config.NodeId, peerNodeAddress, ipaddress, this.GetHashCode(), this.initiator, nbrState.ToString(), previousState, attemptedState, action);
					if (severity == TraceEventType.Verbose && e != null)
					{
						severity = TraceEventType.Information;
					}
					TraceUtility.TraceEvent(severity, traceCode, traceDescription, extendedData, this, e);
				}
			}

			// Token: 0x060083A2 RID: 33698 RVA: 0x001E6FE6 File Offset: 0x001E51E6
			void IInputSessionShutdown.ChannelFaulted(IDuplexContextChannel channel)
			{
			}

			// Token: 0x060083A3 RID: 33699 RVA: 0x001E6FE8 File Offset: 0x001E51E8
			void IInputSessionShutdown.DoneReceiving(IDuplexContextChannel channel)
			{
				if (channel.State == CommunicationState.Opened)
				{
					channel.Close();
				}
			}

			// Token: 0x17001D1A RID: 7450
			// (get) Token: 0x060083A4 RID: 33700 RVA: 0x001E6FF9 File Offset: 0x001E51F9
			public UtilityExtension Utility
			{
				get
				{
					if (this.utility == null)
					{
						this.utility = this.Extensions.Find<UtilityExtension>();
					}
					return this.utility;
				}
			}

			// Token: 0x04004ADC RID: 19164
			private ChannelFactory<IPeerProxy> channelFactory;

			// Token: 0x04004ADD RID: 19165
			private Exception closeException;

			// Token: 0x04004ADE RID: 19166
			private PeerCloseInitiator closeInitiator;

			// Token: 0x04004ADF RID: 19167
			private PeerCloseReason closeReason;

			// Token: 0x04004AE0 RID: 19168
			private PeerNodeConfig config;

			// Token: 0x04004AE1 RID: 19169
			private IPAddress connectIPAddress;

			// Token: 0x04004AE2 RID: 19170
			private ExtensionCollection<IPeerNeighbor> extensions;

			// Token: 0x04004AE3 RID: 19171
			private bool initiator;

			// Token: 0x04004AE4 RID: 19172
			private bool isClosing;

			// Token: 0x04004AE5 RID: 19173
			private PeerNodeAddress listenAddress;

			// Token: 0x04004AE6 RID: 19174
			private ulong nodeId;

			// Token: 0x04004AE7 RID: 19175
			private IPeerProxy proxy;

			// Token: 0x04004AE8 RID: 19176
			private IClientChannel proxyChannel;

			// Token: 0x04004AE9 RID: 19177
			private PeerNeighborState state;

			// Token: 0x04004AEA RID: 19178
			private object thisLock = new object();

			// Token: 0x04004AEB RID: 19179
			private IPeerNodeMessageHandling messageHandler;

			// Token: 0x04004AEC RID: 19180
			private UtilityExtension utility;

			// Token: 0x02000F91 RID: 3985
			private enum SetStateBehavior
			{
				// Token: 0x04004F90 RID: 20368
				ThrowException,
				// Token: 0x04004F91 RID: 20369
				TrySet
			}

			// Token: 0x02000F92 RID: 3986
			private class OpenAsyncResult : AsyncResult
			{
				// Token: 0x06008862 RID: 34914 RVA: 0x001FAE14 File Offset: 0x001F9014
				public OpenAsyncResult(PeerNeighborManager.PeerNeighbor neighbor, PeerNodeAddress remoteAddress, Binding binding, PeerService service, PeerNeighborManager.ClosedCallback closedCallback, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.neighbor = neighbor;
					this.currentIndex = 0;
					this.completedSynchronously = true;
					this.remoteAddress = remoteAddress;
					this.service = service;
					this.binding = binding;
					this.onOpen = Fx.ThunkCallback(new AsyncCallback(this.OnOpen));
					this.closed = closedCallback;
					this.BeginOpen();
				}

				// Token: 0x06008863 RID: 34915 RVA: 0x001FAE88 File Offset: 0x001F9088
				private void BeginOpen()
				{
					try
					{
						while (this.currentIndex < this.remoteAddress.IPAddresses.Count)
						{
							EndpointAddress ipendpointAddress = PeerIPHelper.GetIPEndpointAddress(this.remoteAddress.EndpointAddress, this.remoteAddress.IPAddresses[this.currentIndex]);
							if (this.closed())
							{
								throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().ToString()));
							}
							try
							{
								this.neighbor.ConnectIPAddress = this.remoteAddress.IPAddresses[this.currentIndex];
								IAsyncResult asyncResult = this.neighbor.BeginOpenProxy(ipendpointAddress, this.binding, new InstanceContext(null, this.service, false), this.timeoutHelper.RemainingTime(), this.onOpen, null);
								if (!asyncResult.CompletedSynchronously)
								{
									return;
								}
								this.neighbor.EndOpenProxy(asyncResult);
								this.lastException = null;
								this.neighbor.isClosing = false;
								break;
							}
							catch (Exception exception)
							{
								if (Fx.IsFatal(exception))
								{
									throw;
								}
								try
								{
									this.neighbor.CleanupProxy();
								}
								catch (Exception exception2)
								{
									if (Fx.IsFatal(exception2))
									{
										throw;
									}
									DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
								}
								DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
								if (!this.ContinuableException(exception))
								{
									throw;
								}
							}
						}
					}
					catch (Exception exception3)
					{
						if (Fx.IsFatal(exception3))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
						this.lastException = exception3;
					}
					base.Complete(this.completedSynchronously, this.lastException);
				}

				// Token: 0x06008864 RID: 34916 RVA: 0x001FB050 File Offset: 0x001F9250
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<PeerNeighborManager.PeerNeighbor.OpenAsyncResult>(result);
				}

				// Token: 0x06008865 RID: 34917 RVA: 0x001FB05C File Offset: 0x001F925C
				private bool ContinuableException(Exception exception)
				{
					if ((exception is EndpointNotFoundException || exception is TimeoutException) && this.timeoutHelper.RemainingTime() > TimeSpan.Zero)
					{
						this.lastException = exception;
						this.currentIndex++;
						return true;
					}
					return false;
				}

				// Token: 0x06008866 RID: 34918 RVA: 0x001FB0A8 File Offset: 0x001F92A8
				private void OnOpen(IAsyncResult result)
				{
					Exception exception = null;
					bool flag = false;
					if (!result.CompletedSynchronously)
					{
						this.completedSynchronously = false;
						try
						{
							this.neighbor.EndOpenProxy(result);
							flag = true;
							this.neighbor.isClosing = false;
						}
						catch (Exception ex)
						{
							if (Fx.IsFatal(ex))
							{
								throw;
							}
							try
							{
								this.neighbor.CleanupProxy();
							}
							catch (Exception exception2)
							{
								if (Fx.IsFatal(exception2))
								{
									throw;
								}
								DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
							}
							exception = ex;
							if (this.ContinuableException(exception))
							{
								try
								{
									this.BeginOpen();
									goto IL_88;
								}
								catch (Exception exception3)
								{
									if (Fx.IsFatal(exception3))
									{
										throw;
									}
									DiagnosticUtility.TraceHandledException(exception3, TraceEventType.Information);
									goto IL_88;
								}
							}
							flag = true;
							IL_88:;
						}
					}
					if (flag)
					{
						base.Complete(this.completedSynchronously, exception);
					}
				}

				// Token: 0x04004F92 RID: 20370
				private bool completedSynchronously;

				// Token: 0x04004F93 RID: 20371
				private PeerNeighborManager.ClosedCallback closed;

				// Token: 0x04004F94 RID: 20372
				private int currentIndex;

				// Token: 0x04004F95 RID: 20373
				private PeerNeighborManager.PeerNeighbor neighbor;

				// Token: 0x04004F96 RID: 20374
				private PeerNodeAddress remoteAddress;

				// Token: 0x04004F97 RID: 20375
				private Binding binding;

				// Token: 0x04004F98 RID: 20376
				private PeerService service;

				// Token: 0x04004F99 RID: 20377
				private AsyncCallback onOpen;

				// Token: 0x04004F9A RID: 20378
				private Exception lastException;

				// Token: 0x04004F9B RID: 20379
				private TimeoutHelper timeoutHelper;
			}
		}

		// Token: 0x02000E64 RID: 3684
		private class NeighborOpenAsyncResult : AsyncResult
		{
			// Token: 0x060083A5 RID: 33701 RVA: 0x001E701C File Offset: 0x001E521C
			public NeighborOpenAsyncResult(PeerNeighborManager.PeerNeighbor neighbor, PeerNodeAddress remoteAddress, Binding binding, PeerService service, PeerNeighborManager.ClosedCallback closedCallback, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.neighbor = neighbor;
				IAsyncResult asyncResult = null;
				try
				{
					asyncResult = neighbor.BeginOpen(remoteAddress, binding, service, closedCallback, timeout, Fx.ThunkCallback(new AsyncCallback(this.OnOpen)), null);
					if (asyncResult.CompletedSynchronously)
					{
						neighbor.EndOpen(asyncResult);
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					neighbor.TraceEventHelper(TraceEventType.Warning, 262199, SR.GetString("TraceCodePeerNeighborOpenFailed"));
					throw;
				}
				if (asyncResult.CompletedSynchronously)
				{
					base.Complete(true);
				}
			}

			// Token: 0x060083A6 RID: 33702 RVA: 0x001E70B0 File Offset: 0x001E52B0
			private void OnOpen(IAsyncResult result)
			{
				if (!result.CompletedSynchronously)
				{
					Exception exception = null;
					try
					{
						this.neighbor.EndOpen(result);
					}
					catch (Exception ex)
					{
						if (Fx.IsFatal(ex))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
						this.neighbor.TraceEventHelper(TraceEventType.Warning, 262199, SR.GetString("TraceCodePeerNeighborOpenFailed"));
						exception = ex;
					}
					base.Complete(result.CompletedSynchronously, exception);
				}
			}

			// Token: 0x060083A7 RID: 33703 RVA: 0x001E7124 File Offset: 0x001E5324
			public static IPeerNeighbor End(IAsyncResult result)
			{
				PeerNeighborManager.NeighborOpenAsyncResult neighborOpenAsyncResult = AsyncResult.End<PeerNeighborManager.NeighborOpenAsyncResult>(result);
				return neighborOpenAsyncResult.neighbor;
			}

			// Token: 0x04004AED RID: 19181
			private PeerNeighborManager.PeerNeighbor neighbor;
		}

		// Token: 0x02000E65 RID: 3685
		private class PeerNeighborBehavior : IEndpointBehavior
		{
			// Token: 0x060083A8 RID: 33704 RVA: 0x001E713E File Offset: 0x001E533E
			public PeerNeighborBehavior(PeerNeighborManager.PeerNeighbor neighbor)
			{
				this.neighbor = neighbor;
			}

			// Token: 0x060083A9 RID: 33705 RVA: 0x001E714D File Offset: 0x001E534D
			public void Validate(ServiceEndpoint serviceEndpoint)
			{
			}

			// Token: 0x060083AA RID: 33706 RVA: 0x001E714F File Offset: 0x001E534F
			public void AddBindingParameters(ServiceEndpoint serviceEndpoint, BindingParameterCollection bindingParameters)
			{
			}

			// Token: 0x060083AB RID: 33707 RVA: 0x001E7151 File Offset: 0x001E5351
			public void ApplyDispatchBehavior(ServiceEndpoint serviceEndpoint, EndpointDispatcher endpointDispatcher)
			{
			}

			// Token: 0x060083AC RID: 33708 RVA: 0x001E7153 File Offset: 0x001E5353
			public void ApplyClientBehavior(ServiceEndpoint serviceEndpoint, ClientRuntime behavior)
			{
				behavior.DispatchRuntime.InputSessionShutdownHandlers.Add(this.neighbor);
			}

			// Token: 0x04004AEE RID: 19182
			private PeerNeighborManager.PeerNeighbor neighbor;
		}
	}
}
