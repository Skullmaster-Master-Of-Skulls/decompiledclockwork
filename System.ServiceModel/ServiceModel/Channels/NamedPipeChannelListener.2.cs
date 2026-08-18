using System;
using System.Collections.Generic;
using System.Security.Principal;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000842 RID: 2114
	internal abstract class NamedPipeChannelListener : ConnectionOrientedTransportChannelListener
	{
		// Token: 0x06004EFA RID: 20218 RVA: 0x0011F9E3 File Offset: 0x0011DBE3
		protected NamedPipeChannelListener(NamedPipeTransportBindingElement bindingElement, BindingContext context) : base(bindingElement, context)
		{
			base.SetIdleTimeout(bindingElement.ConnectionPoolSettings.IdleTimeout);
			base.InitializeMaxPooledConnections(bindingElement.ConnectionPoolSettings.MaxOutboundConnectionsPerEndpoint);
		}

		// Token: 0x170013AA RID: 5034
		// (get) Token: 0x06004EFB RID: 20219 RVA: 0x0011FA0F File Offset: 0x0011DC0F
		public override string Scheme
		{
			get
			{
				return Uri.UriSchemeNetPipe;
			}
		}

		// Token: 0x170013AB RID: 5035
		// (get) Token: 0x06004EFC RID: 20220 RVA: 0x0011FA16 File Offset: 0x0011DC16
		// (set) Token: 0x06004EFD RID: 20221 RVA: 0x0011FA20 File Offset: 0x0011DC20
		internal List<SecurityIdentifier> AllowedUsers
		{
			get
			{
				return this.allowedUsers;
			}
			set
			{
				object thisLock = base.ThisLock;
				lock (thisLock)
				{
					base.ThrowIfDisposedOrImmutable();
					this.allowedUsers = value;
				}
			}
		}

		// Token: 0x170013AC RID: 5036
		// (get) Token: 0x06004EFE RID: 20222 RVA: 0x0011FA68 File Offset: 0x0011DC68
		internal static UriPrefixTable<ITransportManagerRegistration> StaticTransportManagerTable
		{
			get
			{
				return NamedPipeChannelListener.transportManagerTable;
			}
		}

		// Token: 0x170013AD RID: 5037
		// (get) Token: 0x06004EFF RID: 20223 RVA: 0x0011FA6F File Offset: 0x0011DC6F
		internal override UriPrefixTable<ITransportManagerRegistration> TransportManagerTable
		{
			get
			{
				return NamedPipeChannelListener.transportManagerTable;
			}
		}

		// Token: 0x06004F00 RID: 20224 RVA: 0x0011FA76 File Offset: 0x0011DC76
		internal override ITransportManagerRegistration CreateTransportManagerRegistration(Uri listenUri)
		{
			return new ExclusiveNamedPipeTransportManager(listenUri, this);
		}

		// Token: 0x06004F01 RID: 20225 RVA: 0x0011FA7F File Offset: 0x0011DC7F
		protected override bool SupportsUpgrade(StreamUpgradeBindingElement upgradeBindingElement)
		{
			return !(upgradeBindingElement is SslStreamSecurityBindingElement);
		}

		// Token: 0x0400310F RID: 12559
		private List<SecurityIdentifier> allowedUsers;

		// Token: 0x04003110 RID: 12560
		private static UriPrefixTable<ITransportManagerRegistration> transportManagerTable = new UriPrefixTable<ITransportManagerRegistration>();
	}
}
