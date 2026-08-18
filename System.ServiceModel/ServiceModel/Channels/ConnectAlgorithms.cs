using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x020009EC RID: 2540
	internal sealed class ConnectAlgorithms : IConnectAlgorithms, IDisposable
	{
		// Token: 0x06006490 RID: 25744 RVA: 0x0017737C File Offset: 0x0017557C
		public void Initialize(IPeerMaintainer maintainer, PeerNodeConfig config, int wantedConnectionCount, Dictionary<EndpointAddress, Referral> referralCache)
		{
			this.maintainer = maintainer;
			this.config = config;
			this.wantedConnectionCount = wantedConnectionCount;
			this.UpdateEndpointsCollection(referralCache.Values);
			maintainer.NeighborClosed += this.OnNeighborClosed;
			maintainer.NeighborConnected += this.OnNeighborConnected;
			maintainer.MaintainerClosed += this.OnMaintainerClosed;
			maintainer.ReferralsAdded += this.OnReferralsAdded;
		}

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x06006491 RID: 25745 RVA: 0x001773F3 File Offset: 0x001755F3
		private object ThisLock
		{
			get
			{
				return this.thisLock;
			}
		}

		// Token: 0x06006492 RID: 25746 RVA: 0x001773FC File Offset: 0x001755FC
		public void Connect(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			this.addNeighbor.Set();
			List<IAsyncResult> list = new List<IAsyncResult>();
			List<WaitHandle> list2 = new List<WaitHandle>();
			while (list.Count != 0 || ((this.nodeAddresses.Count != 0 || this.pendingConnectedNeighbor.Count != 0) && this.maintainer.IsOpen && this.maintainer.ConnectedNeighborCount < this.wantedConnectionCount))
			{
				try
				{
					list2.Clear();
					foreach (IAsyncResult asyncResult in list)
					{
						list2.Add(asyncResult.AsyncWaitHandle);
					}
					list2.Add(this.welcomeReceived);
					list2.Add(this.maintainerClosed);
					list2.Add(this.addNeighbor);
					int num = WaitHandle.WaitAny(list2.ToArray(), this.config.ConnectTimeout, false);
					if (num == list.Count)
					{
						this.welcomeReceived.Reset();
					}
					else
					{
						if (num == list.Count + 1)
						{
							this.maintainerClosed.Reset();
							object obj = this.ThisLock;
							lock (obj)
							{
								this.nodeAddresses.Clear();
								continue;
							}
						}
						if (num == list.Count + 2)
						{
							if (this.nodeAddresses.Count > 0 && this.pendingConnectedNeighbor.Count + this.maintainer.ConnectedNeighborCount < this.wantedConnectionCount)
							{
								PeerNodeAddress peerNodeAddress = null;
								object obj2 = this.ThisLock;
								lock (obj2)
								{
									if (this.nodeAddresses.Count == 0 || !this.maintainer.IsOpen)
									{
										this.addNeighbor.Reset();
										continue;
									}
									int num2 = ConnectAlgorithms.random.Next() % this.nodeAddresses.Count;
									ICollection<Uri> keys = this.nodeAddresses.Keys;
									int num3 = 0;
									Uri key = null;
									foreach (Uri uri in keys)
									{
										if (num3++ == num2)
										{
											key = uri;
											break;
										}
									}
									peerNodeAddress = this.nodeAddresses[key];
									this.nodeAddresses.Remove(key);
								}
								if (this.maintainer.FindDuplicateNeighbor(peerNodeAddress) == null && !this.pendingConnectedNeighbor.ContainsKey(ConnectAlgorithms.GetEndpointUri(peerNodeAddress)))
								{
									object obj3 = this.ThisLock;
									lock (obj3)
									{
										this.pendingConnectedNeighbor.Add(ConnectAlgorithms.GetEndpointUri(peerNodeAddress), peerNodeAddress);
									}
									try
									{
										if (this.maintainer.IsOpen)
										{
											if (DiagnosticUtility.ShouldTraceInformation)
											{
												PeerMaintainerTraceRecord extendedData = new PeerMaintainerTraceRecord(SR.GetString("PeerMaintainerConnect", new object[]
												{
													peerNodeAddress,
													this.config.MeshId
												}));
												TraceUtility.TraceEvent(TraceEventType.Information, 262225, SR.GetString("TraceCodePeerMaintainerActivity"), extendedData, this, null);
											}
											IAsyncResult item = this.maintainer.BeginOpenNeighbor(peerNodeAddress, timeoutHelper.RemainingTime(), null, peerNodeAddress);
											list.Add(item);
										}
									}
									catch (Exception ex)
									{
										if (Fx.IsFatal(ex))
										{
											throw;
										}
										if (DiagnosticUtility.ShouldTraceInformation)
										{
											PeerMaintainerTraceRecord extendedData2 = new PeerMaintainerTraceRecord(SR.GetString("PeerMaintainerConnectFailure", new object[]
											{
												peerNodeAddress,
												this.config.MeshId,
												ex.Message
											}));
											TraceUtility.TraceEvent(TraceEventType.Information, 262225, SR.GetString("TraceCodePeerMaintainerActivity"), extendedData2, this, null);
										}
										this.pendingConnectedNeighbor.Remove(ConnectAlgorithms.GetEndpointUri(peerNodeAddress));
										if (!(ex is ObjectDisposedException))
										{
											throw;
										}
										DiagnosticUtility.TraceHandledException(ex, TraceEventType.Information);
									}
								}
							}
							if (this.nodeAddresses.Count == 0 || this.pendingConnectedNeighbor.Count + this.maintainer.ConnectedNeighborCount == this.wantedConnectionCount)
							{
								this.addNeighbor.Reset();
							}
						}
						else
						{
							if (num != 258)
							{
								IAsyncResult asyncResult2 = list[num];
								list.RemoveAt(num);
								try
								{
									IPeerNeighbor peerNeighbor = this.maintainer.EndOpenNeighbor(asyncResult2);
									continue;
								}
								catch (Exception exception)
								{
									if (Fx.IsFatal(exception))
									{
										throw;
									}
									this.pendingConnectedNeighbor.Remove(ConnectAlgorithms.GetEndpointUri((PeerNodeAddress)asyncResult2.AsyncState));
									throw;
								}
							}
							this.pendingConnectedNeighbor.Clear();
							list.Clear();
							this.addNeighbor.Set();
						}
					}
				}
				catch (CommunicationException exception2)
				{
					DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
					this.addNeighbor.Set();
				}
				catch (TimeoutException ex2)
				{
					if (TD.OpenTimeoutIsEnabled())
					{
						TD.OpenTimeout(ex2.Message);
					}
					DiagnosticUtility.TraceHandledException(ex2, TraceEventType.Information);
					this.addNeighbor.Set();
				}
			}
		}

		// Token: 0x06006493 RID: 25747 RVA: 0x001779A4 File Offset: 0x00175BA4
		void IDisposable.Dispose()
		{
			if (!this.disposed)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.disposed)
					{
						this.disposed = true;
						this.maintainer.ReferralsAdded -= this.OnReferralsAdded;
						this.maintainer.MaintainerClosed -= this.OnMaintainerClosed;
						this.maintainer.NeighborClosed -= this.OnNeighborClosed;
						this.maintainer.NeighborConnected -= this.OnNeighborConnected;
						this.addNeighbor.Close();
						this.maintainerClosed.Close();
						this.welcomeReceived.Close();
					}
				}
			}
		}

		// Token: 0x06006494 RID: 25748 RVA: 0x00177A78 File Offset: 0x00175C78
		private static Uri GetEndpointUri(PeerNodeAddress address)
		{
			return address.EndpointAddress.Uri;
		}

		// Token: 0x06006495 RID: 25749 RVA: 0x00177A88 File Offset: 0x00175C88
		public void PruneConnections()
		{
			while (this.maintainer.NonClosingNeighborCount > this.config.IdealNeighbors && this.maintainer.IsOpen)
			{
				IPeerNeighbor leastUsefulNeighbor = this.maintainer.GetLeastUsefulNeighbor();
				if (leastUsefulNeighbor == null)
				{
					break;
				}
				this.maintainer.CloseNeighbor(leastUsefulNeighbor, PeerCloseReason.NotUsefulNeighbor);
			}
		}

		// Token: 0x06006496 RID: 25750 RVA: 0x00177AD8 File Offset: 0x00175CD8
		public void UpdateEndpointsCollection(ICollection<PeerNodeAddress> src)
		{
			if (src != null)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					foreach (PeerNodeAddress address in src)
					{
						this.UpdateEndpointsCollection(address);
					}
				}
			}
		}

		// Token: 0x06006497 RID: 25751 RVA: 0x00177B4C File Offset: 0x00175D4C
		public void UpdateEndpointsCollection(ICollection<Referral> src)
		{
			if (src != null)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					foreach (Referral referral in src)
					{
						this.UpdateEndpointsCollection(referral.Address);
					}
				}
			}
		}

		// Token: 0x06006498 RID: 25752 RVA: 0x00177BC4 File Offset: 0x00175DC4
		private void UpdateEndpointsCollection(PeerNodeAddress address)
		{
			if (PeerValidateHelper.ValidNodeAddress(address))
			{
				Uri endpointUri = ConnectAlgorithms.GetEndpointUri(address);
				if (!this.nodeAddresses.ContainsKey(endpointUri) && endpointUri != ConnectAlgorithms.GetEndpointUri(this.maintainer.GetListenAddress()))
				{
					this.nodeAddresses[endpointUri] = address;
				}
			}
		}

		// Token: 0x06006499 RID: 25753 RVA: 0x00177C14 File Offset: 0x00175E14
		private void OnNeighborClosed(IPeerNeighbor neighbor)
		{
			if (neighbor.ListenAddress != null)
			{
				Uri endpointUri = ConnectAlgorithms.GetEndpointUri(neighbor.ListenAddress);
				if (!this.disposed)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (!this.disposed && endpointUri != null && this.pendingConnectedNeighbor.ContainsKey(endpointUri))
						{
							this.pendingConnectedNeighbor.Remove(endpointUri);
							this.addNeighbor.Set();
						}
					}
				}
			}
		}

		// Token: 0x0600649A RID: 25754 RVA: 0x00177CA4 File Offset: 0x00175EA4
		private void OnNeighborConnected(IPeerNeighbor neighbor)
		{
			Uri endpointUri = ConnectAlgorithms.GetEndpointUri(neighbor.ListenAddress);
			if (!this.disposed)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.disposed)
					{
						if (endpointUri != null && this.pendingConnectedNeighbor.ContainsKey(endpointUri))
						{
							this.pendingConnectedNeighbor.Remove(endpointUri);
						}
						this.welcomeReceived.Set();
					}
				}
			}
		}

		// Token: 0x0600649B RID: 25755 RVA: 0x00177D2C File Offset: 0x00175F2C
		private void OnMaintainerClosed()
		{
			if (!this.disposed)
			{
				object obj = this.ThisLock;
				lock (obj)
				{
					if (!this.disposed)
					{
						this.maintainerClosed.Set();
					}
				}
			}
		}

		// Token: 0x0600649C RID: 25756 RVA: 0x00177D84 File Offset: 0x00175F84
		private void OnReferralsAdded(IList<Referral> referrals, IPeerNeighbor neighbor)
		{
			bool flag = false;
			foreach (Referral referral in referrals)
			{
				if (!this.disposed)
				{
					object obj = this.ThisLock;
					lock (obj)
					{
						if (!this.disposed)
						{
							if (!this.maintainer.IsOpen)
							{
								return;
							}
							Uri endpointUri = ConnectAlgorithms.GetEndpointUri(referral.Address);
							if (endpointUri != ConnectAlgorithms.GetEndpointUri(this.maintainer.GetListenAddress()) && !this.nodeAddresses.ContainsKey(endpointUri) && !this.pendingConnectedNeighbor.ContainsKey(endpointUri) && this.maintainer.FindDuplicateNeighbor(referral.Address) == null)
							{
								this.nodeAddresses[endpointUri] = referral.Address;
								flag = true;
							}
						}
					}
				}
			}
			if (flag && this.maintainer.ConnectedNeighborCount < this.wantedConnectionCount)
			{
				this.addNeighbor.Set();
			}
		}

		// Token: 0x040039D4 RID: 14804
		private static Random random = new Random();

		// Token: 0x040039D5 RID: 14805
		private int wantedConnectionCount;

		// Token: 0x040039D6 RID: 14806
		private EventWaitHandle addNeighbor = new EventWaitHandle(true, EventResetMode.ManualReset);

		// Token: 0x040039D7 RID: 14807
		private EventWaitHandle maintainerClosed = new EventWaitHandle(false, EventResetMode.ManualReset);

		// Token: 0x040039D8 RID: 14808
		private EventWaitHandle welcomeReceived = new EventWaitHandle(false, EventResetMode.ManualReset);

		// Token: 0x040039D9 RID: 14809
		private Dictionary<Uri, PeerNodeAddress> nodeAddresses = new Dictionary<Uri, PeerNodeAddress>();

		// Token: 0x040039DA RID: 14810
		private PeerNodeConfig config;

		// Token: 0x040039DB RID: 14811
		private Dictionary<Uri, PeerNodeAddress> pendingConnectedNeighbor = new Dictionary<Uri, PeerNodeAddress>();

		// Token: 0x040039DC RID: 14812
		private object thisLock = new object();

		// Token: 0x040039DD RID: 14813
		private IPeerMaintainer maintainer;

		// Token: 0x040039DE RID: 14814
		private bool disposed;
	}
}
