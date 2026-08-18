using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Runtime;
using System.Security;
using System.Security.Permissions;
using System.ServiceModel.Diagnostics;
using Microsoft.Win32;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000A1C RID: 2588
	internal class PeerMaintainerBase<TConnectAlgorithms> : IPeerMaintainer where TConnectAlgorithms : IConnectAlgorithms, new()
	{
		// Token: 0x14000049 RID: 73
		// (add) Token: 0x06006656 RID: 26198 RVA: 0x0017CA90 File Offset: 0x0017AC90
		// (remove) Token: 0x06006657 RID: 26199 RVA: 0x0017CAC8 File Offset: 0x0017ACC8
		public event ReferralsAddedHandler ReferralsAdded;

		// Token: 0x170018A0 RID: 6304
		// (get) Token: 0x06006658 RID: 26200 RVA: 0x0017CAFD File Offset: 0x0017ACFD
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06006659 RID: 26201 RVA: 0x0017CB08 File Offset: 0x0017AD08
		public PeerMaintainerBase(PeerNodeConfig config, PeerNeighborManager neighborManager, PeerFlooder flooder)
		{
			this.neighborManager = neighborManager;
			this.flooder = flooder;
			this.config = config;
			this.thisLock = new object();
			this.referralCache = new Dictionary<EndpointAddress, Referral>();
			this.maintainerTimer = new IOThreadTimer(new Action<object>(this.OnMaintainerTimer), this, false);
		}

		// Token: 0x0600665A RID: 26202 RVA: 0x0017CB60 File Offset: 0x0017AD60
		public bool AddReferrals(IList<Referral> referrals, IPeerNeighbor neighbor)
		{
			bool flag = true;
			bool flag2 = false;
			try
			{
				flag2 = this.config.Resolver.CanShareReferrals;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(SR.GetString("ResolverException"), ex);
			}
			if (referrals != null && flag2)
			{
				foreach (Referral referral in referrals)
				{
					if (referral == null || referral.NodeId == 0UL || !PeerValidateHelper.ValidNodeAddress(referral.Address) || !PeerValidateHelper.ValidReferralNodeAddress(referral.Address))
					{
						flag = false;
						break;
					}
				}
				if (flag)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						foreach (Referral referral2 in referrals)
						{
							EndpointAddress endpointAddress = referral2.Address.EndpointAddress;
							if (this.referralCache.Count <= this.config.MaxReferralCacheSize && !this.referralCache.ContainsKey(endpointAddress))
							{
								this.referralCache.Add(endpointAddress, referral2);
							}
						}
					}
					ReferralsAddedHandler referralsAdded = this.ReferralsAdded;
					if (referralsAdded != null)
					{
						this.ReferralsAdded(referrals, neighbor);
					}
				}
			}
			return flag;
		}

		// Token: 0x0600665B RID: 26203 RVA: 0x0017CCE4 File Offset: 0x0017AEE4
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public void Close()
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				this.isOpen = false;
			}
			this.maintainerTimer.Cancel();
			SystemEvents.PowerModeChanged -= this.SystemEvents_PowerModeChanged;
			MaintainerClosedHandler maintainerClosed = this.MaintainerClosed;
			if (maintainerClosed != null)
			{
				maintainerClosed();
			}
		}

		// Token: 0x0600665C RID: 26204 RVA: 0x0017CD54 File Offset: 0x0017AF54
		private void InitialConnection(object dummy)
		{
			if (this.isOpen)
			{
				bool flag = false;
				if (!this.isRunningMaintenance)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (!this.isRunningMaintenance)
						{
							this.isRunningMaintenance = true;
							flag = true;
						}
					}
				}
				if (flag)
				{
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						PeerMaintainerTraceRecord extendedData = new PeerMaintainerTraceRecord(SR.GetString("PeerMaintainerInitialConnect", new object[]
						{
							this.config.MeshId
						}));
						TraceUtility.TraceEvent(TraceEventType.Information, 262225, SR.GetString("TraceCodePeerMaintainerActivity"), extendedData, this, null);
					}
					TimeoutHelper timeoutHelper = new TimeoutHelper(this.config.MaintainerTimeout);
					Exception ex = null;
					try
					{
						this.maintainerTimer.Cancel();
						using (IConnectAlgorithms connectAlgorithms = Activator.CreateInstance<TConnectAlgorithms>())
						{
							connectAlgorithms.Initialize(this, this.config, this.config.MinNeighbors, this.referralCache);
							if (this.referralCache.Count == 0)
							{
								ReadOnlyCollection<PeerNodeAddress> src = this.ResolveNewAddresses(timeoutHelper.RemainingTime(), false);
								connectAlgorithms.UpdateEndpointsCollection(src);
							}
							if (this.isOpen)
							{
								connectAlgorithms.Connect(timeoutHelper.RemainingTime());
							}
						}
					}
					catch (Exception ex2)
					{
						if (Fx.IsFatal(ex2))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
						ex = ex2;
					}
					if (this.isOpen)
					{
						try
						{
							object obj2 = this.ThisLock;
							lock (obj2)
							{
								if (this.isOpen)
								{
									if (this.neighborManager.ConnectedNeighborCount < 1)
									{
										this.maintainerTimer.Set(this.config.MaintainerRetryInterval);
									}
									else
									{
										this.maintainerTimer.Set(this.config.MaintainerInterval);
									}
								}
							}
						}
						catch (Exception ex3)
						{
							if (Fx.IsFatal(ex3))
							{
								throw;
							}
							DiagnosticUtility.TraceHandledException(ex3, TraceEventType.Information);
							if (ex == null)
							{
								ex = ex3;
							}
						}
					}
					object obj3 = this.ThisLock;
					lock (obj3)
					{
						this.isRunningMaintenance = false;
					}
					if (this.connectCallback != null)
					{
						this.connectCallback(ex);
					}
				}
			}
		}

		// Token: 0x0600665D RID: 26205 RVA: 0x0017CFD0 File Offset: 0x0017B1D0
		private void MaintainConnections(object dummy)
		{
			if (this.isOpen)
			{
				bool flag = false;
				if (!this.isRunningMaintenance)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (!this.isRunningMaintenance)
						{
							this.isRunningMaintenance = true;
							flag = true;
						}
					}
				}
				if (flag)
				{
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						PeerMaintainerTraceRecord extendedData = new PeerMaintainerTraceRecord(SR.GetString("PeerMaintainerStarting", new object[]
						{
							this.config.MeshId
						}));
						TraceUtility.TraceEvent(TraceEventType.Information, 262225, SR.GetString("TraceCodePeerMaintainerActivity"), extendedData, this, null);
					}
					TimeoutHelper timeoutHelper = new TimeoutHelper(this.config.MaintainerTimeout);
					try
					{
						this.maintainerTimer.Cancel();
						int connectedNeighborCount = this.neighborManager.ConnectedNeighborCount;
						if (connectedNeighborCount != this.config.IdealNeighbors)
						{
							using (IConnectAlgorithms connectAlgorithms = Activator.CreateInstance<TConnectAlgorithms>())
							{
								connectAlgorithms.Initialize(this, this.config, this.config.IdealNeighbors, this.referralCache);
								if (connectedNeighborCount > this.config.IdealNeighbors)
								{
									if (DiagnosticUtility.ShouldTraceInformation)
									{
										PeerMaintainerTraceRecord extendedData2 = new PeerMaintainerTraceRecord(SR.GetString("PeerMaintainerPruneMode", new object[]
										{
											this.config.MeshId
										}));
										TraceUtility.TraceEvent(TraceEventType.Information, 262225, SR.GetString("TraceCodePeerMaintainerActivity"), extendedData2, this, null);
									}
									connectAlgorithms.PruneConnections();
								}
								connectedNeighborCount = this.neighborManager.ConnectedNeighborCount;
								if (connectedNeighborCount < this.config.IdealNeighbors)
								{
									if (this.referralCache.Count == 0)
									{
										ReadOnlyCollection<PeerNodeAddress> src = this.ResolveNewAddresses(timeoutHelper.RemainingTime(), true);
										connectAlgorithms.UpdateEndpointsCollection(src);
									}
									if (DiagnosticUtility.ShouldTraceInformation)
									{
										PeerMaintainerTraceRecord extendedData3 = new PeerMaintainerTraceRecord(SR.GetString("PeerMaintainerConnectMode", new object[]
										{
											this.config.MeshId
										}));
										TraceUtility.TraceEvent(TraceEventType.Information, 262225, SR.GetString("TraceCodePeerMaintainerActivity"), extendedData3, this, null);
									}
									connectAlgorithms.Connect(timeoutHelper.RemainingTime());
								}
							}
						}
					}
					catch (Exception exception)
					{
						if (Fx.IsFatal(exception))
						{
							throw;
						}
						DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					}
					finally
					{
						if (DiagnosticUtility.ShouldTraceInformation)
						{
							PeerMaintainerTraceRecord extendedData4 = new PeerMaintainerTraceRecord("Maintainer cycle finish");
							TraceUtility.TraceEvent(TraceEventType.Information, 262225, SR.GetString("TraceCodePeerMaintainerActivity"), extendedData4, this, null);
						}
					}
					this.ResetMaintenance();
				}
			}
		}

		// Token: 0x0600665E RID: 26206 RVA: 0x0017D28C File Offset: 0x0017B48C
		private void OnMaintainerTimer(object state)
		{
			ActionItem.Schedule(new Action<object>(this.MaintainConnections), null);
		}

		// Token: 0x0600665F RID: 26207 RVA: 0x0017D2A0 File Offset: 0x0017B4A0
		public void RefreshConnection()
		{
			if (this.isOpen)
			{
				bool flag = false;
				if (!this.isRunningMaintenance)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (!this.isRunningMaintenance)
						{
							this.isRunningMaintenance = true;
							flag = true;
						}
					}
				}
				if (flag)
				{
					try
					{
						TimeoutHelper timeoutHelper = new TimeoutHelper(this.config.MaintainerTimeout);
						this.maintainerTimer.Cancel();
						using (IConnectAlgorithms connectAlgorithms = Activator.CreateInstance<TConnectAlgorithms>())
						{
							ReadOnlyCollection<PeerNodeAddress> readOnlyCollection = this.ResolveNewAddresses(timeoutHelper.RemainingTime(), true);
							connectAlgorithms.Initialize(this, this.config, this.neighborManager.ConnectedNeighborCount + 1, new Dictionary<EndpointAddress, Referral>());
							if (readOnlyCollection.Count > 0 && this.isOpen)
							{
								connectAlgorithms.UpdateEndpointsCollection(readOnlyCollection);
								connectAlgorithms.Connect(timeoutHelper.RemainingTime());
							}
						}
					}
					finally
					{
						this.ResetMaintenance();
					}
				}
			}
		}

		// Token: 0x06006660 RID: 26208 RVA: 0x0017D3C0 File Offset: 0x0017B5C0
		private void ResetMaintenance()
		{
			if (this.isOpen)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (this.isOpen)
					{
						try
						{
							this.maintainerTimer.Set(this.config.MaintainerInterval);
						}
						catch (Exception exception)
						{
							if (Fx.IsFatal(exception))
							{
								throw;
							}
							DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
						}
					}
				}
			}
			object obj2 = this.ThisLock;
			lock (obj2)
			{
				this.isRunningMaintenance = false;
			}
		}

		// Token: 0x06006661 RID: 26209 RVA: 0x0017D47C File Offset: 0x0017B67C
		public void ScheduleConnect(PeerMaintainerBase<TConnectAlgorithms>.ConnectCallback connectCallback)
		{
			this.connectCallback = connectCallback;
			ActionItem.Schedule(new Action<object>(this.InitialConnection), null);
		}

		// Token: 0x06006662 RID: 26210 RVA: 0x0017D498 File Offset: 0x0017B698
		public Referral[] GetReferrals()
		{
			bool flag = false;
			try
			{
				flag = this.config.Resolver.CanShareReferrals;
			}
			catch (Exception ex)
			{
				if (Fx.IsFatal(ex))
				{
					throw;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperCallback(SR.GetString("ResolverException"), ex);
			}
			Referral[] array;
			if (flag)
			{
				List<IPeerNeighbor> connectedNeighbors = this.neighborManager.GetConnectedNeighbors();
				int num = Math.Min(this.config.MaxReferrals, connectedNeighbors.Count);
				array = new Referral[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = new Referral(connectedNeighbors[i].NodeId, connectedNeighbors[i].ListenAddress);
				}
			}
			else
			{
				array = new Referral[0];
			}
			return array;
		}

		// Token: 0x06006663 RID: 26211 RVA: 0x0017D55C File Offset: 0x0017B75C
		public virtual void OnNeighborClosed(IPeerNeighbor neighbor)
		{
			if (this.isOpen)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (neighbor != null && neighbor.ListenAddress != null)
					{
						EndpointAddress endpointAddress = neighbor.ListenAddress.EndpointAddress;
					}
					if (this.isOpen && !this.isRunningMaintenance && this.neighborManager.ConnectedNeighborCount < this.config.MinNeighbors)
					{
						this.maintainerTimer.Set(0);
					}
				}
			}
			NeighborClosedHandler neighborClosed = this.NeighborClosed;
			if (neighborClosed != null)
			{
				neighborClosed(neighbor);
			}
		}

		// Token: 0x06006664 RID: 26212 RVA: 0x0017D600 File Offset: 0x0017B800
		public virtual void OnNeighborConnected(IPeerNeighbor neighbor)
		{
			NeighborConnectedHandler neighborConnected = this.NeighborConnected;
			if (neighborConnected != null)
			{
				neighborConnected(neighbor);
			}
		}

		// Token: 0x06006665 RID: 26213 RVA: 0x0017D620 File Offset: 0x0017B820
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		public void Open()
		{
			this.traceRecord = new PeerNodeTraceRecord(this.config.NodeId);
			if (this.isRunningMaintenance)
			{
				return;
			}
			object obj = this.ThisLock;
			lock (obj)
			{
				SystemEvents.PowerModeChanged += this.SystemEvents_PowerModeChanged;
				this.isOpen = true;
			}
		}

		// Token: 0x06006666 RID: 26214 RVA: 0x0017D698 File Offset: 0x0017B898
		private ReadOnlyCollection<PeerNodeAddress> ResolveNewAddresses(TimeSpan timeLeft, bool retryResolve)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeLeft);
			Dictionary<string, PeerNodeAddress> dictionary = new Dictionary<string, PeerNodeAddress>();
			List<PeerNodeAddress> list = new List<PeerNodeAddress>();
			PeerNodeAddress listenAddress = this.config.GetListenAddress(true);
			dictionary.Add(listenAddress.ServicePath, listenAddress);
			int num = retryResolve ? 2 : 1;
			if (DiagnosticUtility.ShouldTraceInformation)
			{
				PeerMaintainerTraceRecord extendedData = new PeerMaintainerTraceRecord("Resolving");
				TraceUtility.TraceEvent(TraceEventType.Information, 262225, SR.GetString("TraceCodePeerMaintainerActivity"), extendedData, this, null);
			}
			int num2 = 0;
			while (num2 < num && list.Count < this.config.MaxResolveAddresses && this.isOpen && timeoutHelper.RemainingTime() > TimeSpan.Zero)
			{
				ReadOnlyCollection<PeerNodeAddress> readOnlyCollection;
				try
				{
					readOnlyCollection = this.config.Resolver.Resolve(this.config.MeshId, this.config.MaxResolveAddresses, timeoutHelper.RemainingTime());
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					if (DiagnosticUtility.ShouldTraceInformation)
					{
						PeerMaintainerTraceRecord extendedData2 = new PeerMaintainerTraceRecord("Resolve exception " + ex.Message);
						TraceUtility.TraceEvent(TraceEventType.Information, 262225, SR.GetString("TraceCodePeerMaintainerActivity"), extendedData2, this, null);
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("ResolverException"), ex));
				}
				if (readOnlyCollection != null)
				{
					foreach (PeerNodeAddress peerNodeAddress in readOnlyCollection)
					{
						if (!dictionary.ContainsKey(peerNodeAddress.ServicePath))
						{
							dictionary.Add(peerNodeAddress.ServicePath, peerNodeAddress);
							if (((IPeerMaintainer)this).FindDuplicateNeighbor(peerNodeAddress) == null)
							{
								list.Add(peerNodeAddress);
							}
						}
					}
				}
				num2++;
			}
			return new ReadOnlyCollection<PeerNodeAddress>(list);
		}

		// Token: 0x1400004A RID: 74
		// (add) Token: 0x06006667 RID: 26215 RVA: 0x0017D864 File Offset: 0x0017BA64
		// (remove) Token: 0x06006668 RID: 26216 RVA: 0x0017D89C File Offset: 0x0017BA9C
		public event NeighborClosedHandler NeighborClosed;

		// Token: 0x1400004B RID: 75
		// (add) Token: 0x06006669 RID: 26217 RVA: 0x0017D8D4 File Offset: 0x0017BAD4
		// (remove) Token: 0x0600666A RID: 26218 RVA: 0x0017D90C File Offset: 0x0017BB0C
		public event NeighborConnectedHandler NeighborConnected;

		// Token: 0x1400004C RID: 76
		// (add) Token: 0x0600666B RID: 26219 RVA: 0x0017D944 File Offset: 0x0017BB44
		// (remove) Token: 0x0600666C RID: 26220 RVA: 0x0017D97C File Offset: 0x0017BB7C
		public event MaintainerClosedHandler MaintainerClosed;

		// Token: 0x0600666D RID: 26221 RVA: 0x0017D9B1 File Offset: 0x0017BBB1
		void IPeerMaintainer.CloseNeighbor(IPeerNeighbor neighbor, PeerCloseReason closeReason)
		{
			this.neighborManager.CloseNeighbor(neighbor, closeReason, PeerCloseInitiator.LocalNode);
		}

		// Token: 0x0600666E RID: 26222 RVA: 0x0017D9C1 File Offset: 0x0017BBC1
		IPeerNeighbor IPeerMaintainer.FindDuplicateNeighbor(PeerNodeAddress address)
		{
			return this.neighborManager.FindDuplicateNeighbor(address);
		}

		// Token: 0x0600666F RID: 26223 RVA: 0x0017D9CF File Offset: 0x0017BBCF
		PeerNodeAddress IPeerMaintainer.GetListenAddress()
		{
			return this.config.GetListenAddress(true);
		}

		// Token: 0x06006670 RID: 26224 RVA: 0x0017D9E0 File Offset: 0x0017BBE0
		IPeerNeighbor IPeerMaintainer.GetLeastUsefulNeighbor()
		{
			IPeerNeighbor result = null;
			uint num = uint.MaxValue;
			foreach (IPeerNeighbor peerNeighbor in this.neighborManager.GetConnectedNeighbors())
			{
				UtilityExtension utilityExtension = peerNeighbor.Extensions.Find<UtilityExtension>();
				if (utilityExtension != null && utilityExtension.IsAccurate && utilityExtension.LinkUtility < num && !peerNeighbor.IsClosing)
				{
					num = utilityExtension.LinkUtility;
					result = peerNeighbor;
				}
			}
			return result;
		}

		// Token: 0x06006671 RID: 26225 RVA: 0x0017DA6C File Offset: 0x0017BC6C
		IAsyncResult IPeerMaintainer.BeginOpenNeighbor(PeerNodeAddress address, TimeSpan timeout, AsyncCallback callback, object asyncState)
		{
			object obj = this.ThisLock;
			lock (obj)
			{
				EndpointAddress endpointAddress = address.EndpointAddress;
				if (this.referralCache.ContainsKey(endpointAddress))
				{
					this.referralCache.Remove(endpointAddress);
				}
			}
			return this.neighborManager.BeginOpenNeighbor(address, timeout, callback, asyncState);
		}

		// Token: 0x06006672 RID: 26226 RVA: 0x0017DAD8 File Offset: 0x0017BCD8
		IPeerNeighbor IPeerMaintainer.EndOpenNeighbor(IAsyncResult result)
		{
			return this.neighborManager.EndOpenNeighbor(result);
		}

		// Token: 0x170018A1 RID: 6305
		// (get) Token: 0x06006673 RID: 26227 RVA: 0x0017DAE6 File Offset: 0x0017BCE6
		int IPeerMaintainer.ConnectedNeighborCount
		{
			get
			{
				return this.neighborManager.ConnectedNeighborCount;
			}
		}

		// Token: 0x170018A2 RID: 6306
		// (get) Token: 0x06006674 RID: 26228 RVA: 0x0017DAF3 File Offset: 0x0017BCF3
		int IPeerMaintainer.NonClosingNeighborCount
		{
			get
			{
				return this.neighborManager.NonClosingNeighborCount;
			}
		}

		// Token: 0x170018A3 RID: 6307
		// (get) Token: 0x06006675 RID: 26229 RVA: 0x0017DB00 File Offset: 0x0017BD00
		bool IPeerMaintainer.IsOpen
		{
			get
			{
				return this.isOpen;
			}
		}

		// Token: 0x06006676 RID: 26230 RVA: 0x0017DB0A File Offset: 0x0017BD0A
		public void PingConnections()
		{
			this.neighborManager.PingNeighbors();
		}

		// Token: 0x06006677 RID: 26231 RVA: 0x0017DB17 File Offset: 0x0017BD17
		public void PingAndRefresh(object state)
		{
			this.PingConnections();
			if (this.neighborManager.ConnectedNeighborCount < this.config.IdealNeighbors)
			{
				this.MaintainConnections(null);
			}
		}

		// Token: 0x06006678 RID: 26232 RVA: 0x0017DB3E File Offset: 0x0017BD3E
		[SecuritySafeCritical]
		[PermissionSet(SecurityAction.Demand, Unrestricted = true)]
		private void SystemEvents_PowerModeChanged(object sender, PowerModeChangedEventArgs e)
		{
			if (e.Mode != PowerModes.Resume)
			{
				return;
			}
			if (!this.isOpen)
			{
				return;
			}
			ActionItem.Schedule(new Action<object>(this.PingAndRefresh), null);
		}

		// Token: 0x04003AE9 RID: 15081
		private PeerMaintainerBase<TConnectAlgorithms>.ConnectCallback connectCallback;

		// Token: 0x04003AEA RID: 15082
		private PeerNodeConfig config;

		// Token: 0x04003AEB RID: 15083
		private PeerFlooder flooder;

		// Token: 0x04003AEC RID: 15084
		private PeerNeighborManager neighborManager;

		// Token: 0x04003AED RID: 15085
		private Dictionary<EndpointAddress, Referral> referralCache;

		// Token: 0x04003AEE RID: 15086
		private object thisLock;

		// Token: 0x04003AEF RID: 15087
		private PeerNodeTraceRecord traceRecord;

		// Token: 0x04003AF0 RID: 15088
		private volatile bool isRunningMaintenance;

		// Token: 0x04003AF1 RID: 15089
		private volatile bool isOpen;

		// Token: 0x04003AF2 RID: 15090
		private IOThreadTimer maintainerTimer;

		// Token: 0x02000E60 RID: 3680
		// (Invoke) Token: 0x0600835E RID: 33630
		public delegate void ConnectCallback(Exception e);
	}
}
