using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Net;
using System.Runtime;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.Threading;

namespace System.ServiceModel.PeerResolvers
{
	// Token: 0x020001C5 RID: 453
	internal class PeerDefaultCustomResolverClient : PeerResolver
	{
		// Token: 0x06000ECE RID: 3790 RVA: 0x00035B94 File Offset: 0x00033D94
		internal PeerDefaultCustomResolverClient()
		{
			this.address = null;
			this.binding = null;
			this.defaultLifeTime = TimeSpan.FromHours(1.0);
			this.clientId = Guid.NewGuid();
			this.timer = new IOThreadTimer(new Action<object>(this.RegistrationExpired), this, false);
		}

		// Token: 0x17000393 RID: 915
		// (get) Token: 0x06000ECF RID: 3791 RVA: 0x00035BF4 File Offset: 0x00033DF4
		public override bool CanShareReferrals
		{
			get
			{
				if (this.shareReferrals != null)
				{
					return this.shareReferrals.Value;
				}
				if (this.referralPolicy == PeerReferralPolicy.Service && this.opened)
				{
					IPeerResolverClient proxy = this.GetProxy();
					try
					{
						ServiceSettingsResponseInfo serviceSettings = proxy.GetServiceSettings();
						this.shareReferrals = new bool?(!serviceSettings.ControlMeshShape);
						proxy.Close();
						goto IL_6E;
					}
					finally
					{
						proxy.Abort();
					}
				}
				this.shareReferrals = new bool?(PeerReferralPolicy.Share == this.referralPolicy);
				IL_6E:
				return this.shareReferrals.Value;
			}
		}

		// Token: 0x06000ED0 RID: 3792 RVA: 0x00035C8C File Offset: 0x00033E8C
		public override void Initialize(EndpointAddress address, Binding binding, ClientCredentials credentials, PeerReferralPolicy referralPolicy)
		{
			this.address = address;
			this.binding = binding;
			this.credentials = credentials;
			this.Validate();
			this.channelFactory = new ChannelFactory<IPeerResolverClient>(binding, address);
			this.channelFactory.Endpoint.Behaviors.Remove<ClientCredentials>();
			if (credentials != null)
			{
				this.channelFactory.Endpoint.Behaviors.Add(credentials);
			}
			this.channelFactory.Open();
			this.referralPolicy = referralPolicy;
			this.opened = true;
		}

		// Token: 0x06000ED1 RID: 3793 RVA: 0x00035D0A File Offset: 0x00033F0A
		private IPeerResolverClient GetProxy()
		{
			return this.channelFactory.CreateChannel();
		}

		// Token: 0x06000ED2 RID: 3794 RVA: 0x00035D17 File Offset: 0x00033F17
		private void Validate()
		{
			if (this.address == null || this.binding == null)
			{
				PeerExceptionHelper.ThrowArgument_InsufficientResolverSettings();
			}
		}

		// Token: 0x06000ED3 RID: 3795 RVA: 0x00035D34 File Offset: 0x00033F34
		public override object Register(string meshId, PeerNodeAddress nodeAddress, TimeSpan timeout)
		{
			if (this.opened)
			{
				long num = -1L;
				bool flag = false;
				if (nodeAddress.IPAddresses.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("MustRegisterMoreThanZeroAddresses")));
				}
				foreach (IPAddress ipaddress in nodeAddress.IPAddresses)
				{
					if (ipaddress.IsIPv6LinkLocal)
					{
						if (num == -1L)
						{
							num = ipaddress.ScopeId;
						}
						else if (num != ipaddress.ScopeId)
						{
							flag = true;
							break;
						}
					}
				}
				List<IPAddress> list = new List<IPAddress>();
				foreach (IPAddress ipaddress2 in nodeAddress.IPAddresses)
				{
					if (!flag || (!ipaddress2.IsIPv6LinkLocal && !ipaddress2.IsIPv6SiteLocal))
					{
						list.Add(ipaddress2);
					}
				}
				if (list.Count == 0)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new CommunicationException(SR.GetString("AmbiguousConnectivitySpec")));
				}
				ReadOnlyCollection<IPAddress> ipAddresses = new ReadOnlyCollection<IPAddress>(list);
				this.meshId = meshId;
				this.nodeAddress = new PeerNodeAddress(nodeAddress.EndpointAddress, ipAddresses);
				RegisterInfo registerInfo = new RegisterInfo(this.clientId, meshId, this.nodeAddress);
				IPeerResolverClient proxy = this.GetProxy();
				try
				{
					proxy.OperationTimeout = timeout;
					RegisterResponseInfo registerResponseInfo = proxy.Register(registerInfo);
					this.registrationId = registerResponseInfo.RegistrationId;
					this.timer.Set(registerResponseInfo.RegistrationLifetime);
					this.defaultLifeTime = registerResponseInfo.RegistrationLifetime;
					proxy.Close();
				}
				finally
				{
					proxy.Abort();
				}
			}
			return this.registrationId;
		}

		// Token: 0x06000ED4 RID: 3796 RVA: 0x00035F00 File Offset: 0x00034100
		private void RegistrationExpired(object state)
		{
			if (!this.opened)
			{
				return;
			}
			try
			{
				IPeerResolverClient proxy = this.GetProxy();
				try
				{
					if (Interlocked.Exchange(ref this.updateSuccessful, 1) == 0)
					{
						this.SendUpdate(new UpdateInfo(this.registrationId, this.clientId, this.meshId, this.nodeAddress), ServiceDefaults.SendTimeout);
					}
					else
					{
						RefreshInfo refreshInfo = new RefreshInfo(this.meshId, this.registrationId);
						RefreshResponseInfo refreshResponseInfo = proxy.Refresh(refreshInfo);
						if (refreshResponseInfo.Result == RefreshResult.RegistrationNotFound)
						{
							RegisterInfo registerInfo = new RegisterInfo(this.clientId, this.meshId, this.nodeAddress);
							RegisterResponseInfo registerResponseInfo = proxy.Register(registerInfo);
							this.registrationId = registerResponseInfo.RegistrationId;
							this.defaultLifeTime = registerResponseInfo.RegistrationLifetime;
						}
						proxy.Close();
					}
				}
				finally
				{
					proxy.Abort();
					this.timer.Set(this.defaultLifeTime);
				}
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				DiagnosticUtility.TraceHandledException(exception2, TraceEventType.Information);
			}
		}

		// Token: 0x06000ED5 RID: 3797 RVA: 0x00036028 File Offset: 0x00034228
		public override void Unregister(object registrationId, TimeSpan timeout)
		{
			if (this.opened)
			{
				UnregisterInfo unregisterInfo = new UnregisterInfo(this.meshId, this.registrationId);
				try
				{
					IPeerResolverClient proxy = this.GetProxy();
					try
					{
						proxy.OperationTimeout = timeout;
						proxy.Unregister(unregisterInfo);
						proxy.Close();
					}
					finally
					{
						proxy.Abort();
					}
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				finally
				{
					this.opened = false;
					this.timer.Cancel();
				}
			}
		}

		// Token: 0x06000ED6 RID: 3798 RVA: 0x000360BC File Offset: 0x000342BC
		public override void Update(object registrationId, PeerNodeAddress updatedNodeAddress, TimeSpan timeout)
		{
			if (this.opened)
			{
				UpdateInfo updateInfo = new UpdateInfo(this.registrationId, this.clientId, this.meshId, updatedNodeAddress);
				this.nodeAddress = updatedNodeAddress;
				this.SendUpdate(updateInfo, timeout);
			}
		}

		// Token: 0x06000ED7 RID: 3799 RVA: 0x000360FC File Offset: 0x000342FC
		private void SendUpdate(UpdateInfo updateInfo, TimeSpan timeout)
		{
			try
			{
				IPeerResolverClient proxy = this.GetProxy();
				try
				{
					proxy.OperationTimeout = timeout;
					RegisterResponseInfo registerResponseInfo = proxy.Update(updateInfo);
					proxy.Close();
					this.registrationId = registerResponseInfo.RegistrationId;
					this.defaultLifeTime = registerResponseInfo.RegistrationLifetime;
					Interlocked.Exchange(ref this.updateSuccessful, 1);
					this.timer.Set(this.defaultLifeTime);
				}
				finally
				{
					proxy.Abort();
				}
			}
			catch (CommunicationException exception)
			{
				DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				Interlocked.Exchange(ref this.updateSuccessful, 0);
			}
			catch (Exception exception2)
			{
				if (Fx.IsFatal(exception2))
				{
					throw;
				}
				Interlocked.Exchange(ref this.updateSuccessful, 0);
				throw;
			}
		}

		// Token: 0x06000ED8 RID: 3800 RVA: 0x000361C0 File Offset: 0x000343C0
		public override ReadOnlyCollection<PeerNodeAddress> Resolve(string meshId, int maxAddresses, TimeSpan timeout)
		{
			ResolveResponseInfo resolveResponseInfo = null;
			IList<PeerNodeAddress> list = null;
			List<PeerNodeAddress> list2 = new List<PeerNodeAddress>();
			if (this.opened)
			{
				ResolveInfo resolveInfo = new ResolveInfo(this.clientId, meshId, maxAddresses);
				try
				{
					IPeerResolverClient proxy = this.GetProxy();
					try
					{
						proxy.OperationTimeout = timeout;
						resolveResponseInfo = proxy.Resolve(resolveInfo);
						proxy.Close();
					}
					finally
					{
						proxy.Abort();
					}
					if (resolveResponseInfo != null && resolveResponseInfo.Addresses != null)
					{
						list = resolveResponseInfo.Addresses;
					}
				}
				catch (CommunicationException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
				}
				catch (Exception exception2)
				{
					if (Fx.IsFatal(exception2))
					{
						throw;
					}
					this.opened = false;
					throw;
				}
			}
			if (list != null)
			{
				foreach (PeerNodeAddress peerNodeAddress in list)
				{
					bool flag = true;
					long num = -1L;
					if (peerNodeAddress != null)
					{
						foreach (IPAddress ipaddress in peerNodeAddress.IPAddresses)
						{
							if (ipaddress.IsIPv6LinkLocal)
							{
								if (num == -1L)
								{
									num = ipaddress.ScopeId;
								}
								else if (num != ipaddress.ScopeId)
								{
									flag = false;
									break;
								}
							}
						}
						if (flag)
						{
							list2.Add(peerNodeAddress);
						}
					}
				}
			}
			return new ReadOnlyCollection<PeerNodeAddress>(list2);
		}

		// Token: 0x17000394 RID: 916
		// (get) Token: 0x06000ED9 RID: 3801 RVA: 0x00036338 File Offset: 0x00034538
		// (set) Token: 0x06000EDA RID: 3802 RVA: 0x00036340 File Offset: 0x00034540
		internal string BindingName
		{
			get
			{
				return this.bindingName;
			}
			set
			{
				this.bindingName = value;
			}
		}

		// Token: 0x17000395 RID: 917
		// (get) Token: 0x06000EDB RID: 3803 RVA: 0x00036349 File Offset: 0x00034549
		// (set) Token: 0x06000EDC RID: 3804 RVA: 0x00036351 File Offset: 0x00034551
		internal string BindingConfigurationName
		{
			get
			{
				return this.bindingName;
			}
			set
			{
				this.bindingConfigurationName = value;
			}
		}

		// Token: 0x06000EDD RID: 3805 RVA: 0x0003635C File Offset: 0x0003455C
		public override bool Equals(object other)
		{
			PeerDefaultCustomResolverClient peerDefaultCustomResolverClient = other as PeerDefaultCustomResolverClient;
			if (peerDefaultCustomResolverClient == null || this.referralPolicy != peerDefaultCustomResolverClient.referralPolicy || !this.address.Equals(peerDefaultCustomResolverClient.address))
			{
				return false;
			}
			if (this.BindingName != null || this.BindingConfigurationName != null)
			{
				return this.BindingName == peerDefaultCustomResolverClient.BindingName && this.BindingConfigurationName == peerDefaultCustomResolverClient.BindingConfigurationName;
			}
			return this.binding.Equals(peerDefaultCustomResolverClient.binding);
		}

		// Token: 0x06000EDE RID: 3806 RVA: 0x000363DE File Offset: 0x000345DE
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x04001786 RID: 6022
		private EndpointAddress address;

		// Token: 0x04001787 RID: 6023
		private Binding binding;

		// Token: 0x04001788 RID: 6024
		private TimeSpan defaultLifeTime;

		// Token: 0x04001789 RID: 6025
		private ClientCredentials credentials;

		// Token: 0x0400178A RID: 6026
		private Guid clientId;

		// Token: 0x0400178B RID: 6027
		private Guid registrationId;

		// Token: 0x0400178C RID: 6028
		private IOThreadTimer timer;

		// Token: 0x0400178D RID: 6029
		private bool opened;

		// Token: 0x0400178E RID: 6030
		private string meshId;

		// Token: 0x0400178F RID: 6031
		private PeerNodeAddress nodeAddress;

		// Token: 0x04001790 RID: 6032
		private ChannelFactory<IPeerResolverClient> channelFactory;

		// Token: 0x04001791 RID: 6033
		private PeerReferralPolicy referralPolicy;

		// Token: 0x04001792 RID: 6034
		private string bindingName;

		// Token: 0x04001793 RID: 6035
		private string bindingConfigurationName;

		// Token: 0x04001794 RID: 6036
		private bool? shareReferrals;

		// Token: 0x04001795 RID: 6037
		private int updateSuccessful = 1;
	}
}
