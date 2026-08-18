using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x0200015F RID: 351
	public class WSHttpBinding : WSHttpBindingBase
	{
		// Token: 0x06000A1F RID: 2591 RVA: 0x00026B82 File Offset: 0x00024D82
		public WSHttpBinding(string configName) : this()
		{
			this.ApplyConfiguration(configName);
		}

		// Token: 0x06000A20 RID: 2592 RVA: 0x00026B91 File Offset: 0x00024D91
		public WSHttpBinding()
		{
			this.security = new WSHttpSecurity();
			base..ctor();
		}

		// Token: 0x06000A21 RID: 2593 RVA: 0x00026BA4 File Offset: 0x00024DA4
		public WSHttpBinding(SecurityMode securityMode) : this(securityMode, false)
		{
		}

		// Token: 0x06000A22 RID: 2594 RVA: 0x00026BAE File Offset: 0x00024DAE
		public WSHttpBinding(SecurityMode securityMode, bool reliableSessionEnabled)
		{
			this.security = new WSHttpSecurity();
			base..ctor(reliableSessionEnabled);
			this.security.Mode = securityMode;
		}

		// Token: 0x06000A23 RID: 2595 RVA: 0x00026BCE File Offset: 0x00024DCE
		internal WSHttpBinding(WSHttpSecurity security, bool reliableSessionEnabled)
		{
			this.security = new WSHttpSecurity();
			base..ctor(reliableSessionEnabled);
			this.security = ((security == null) ? new WSHttpSecurity() : security);
		}

		// Token: 0x17000295 RID: 661
		// (get) Token: 0x06000A24 RID: 2596 RVA: 0x00026BF3 File Offset: 0x00024DF3
		// (set) Token: 0x06000A25 RID: 2597 RVA: 0x00026C00 File Offset: 0x00024E00
		[DefaultValue(false)]
		public bool AllowCookies
		{
			get
			{
				return base.HttpTransport.AllowCookies;
			}
			set
			{
				base.HttpTransport.AllowCookies = value;
				base.HttpsTransport.AllowCookies = value;
			}
		}

		// Token: 0x17000296 RID: 662
		// (get) Token: 0x06000A26 RID: 2598 RVA: 0x00026C1A File Offset: 0x00024E1A
		// (set) Token: 0x06000A27 RID: 2599 RVA: 0x00026C22 File Offset: 0x00024E22
		public WSHttpSecurity Security
		{
			get
			{
				return this.security;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("value"));
				}
				this.security = value;
			}
		}

		// Token: 0x06000A28 RID: 2600 RVA: 0x00026C44 File Offset: 0x00024E44
		private void ApplyConfiguration(string configurationName)
		{
			WSHttpBindingCollectionElement bindingCollectionElement = WSHttpBindingCollectionElement.GetBindingCollectionElement();
			WSHttpBindingElement wshttpBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (wshttpBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"wsHttpBinding"
				})));
			}
			wshttpBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x06000A29 RID: 2601 RVA: 0x00026C9C File Offset: 0x00024E9C
		public override IChannelFactory<TChannel> BuildChannelFactory<TChannel>(BindingParameterCollection parameters)
		{
			if (this.security.Mode == SecurityMode.Transport && this.security.Transport.ClientCredentialType == HttpClientCredentialType.InheritedFromHost)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("HttpClientCredentialTypeInvalid", new object[]
				{
					this.security.Transport.ClientCredentialType
				})));
			}
			return base.BuildChannelFactory<TChannel>(parameters);
		}

		// Token: 0x06000A2A RID: 2602 RVA: 0x00026D09 File Offset: 0x00024F09
		public override BindingElementCollection CreateBindingElements()
		{
			if (base.ReliableSession.Enabled && this.security.Mode == SecurityMode.Transport)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("WSHttpDoesNotSupportRMWithHttps")));
			}
			return base.CreateBindingElements();
		}

		// Token: 0x06000A2B RID: 2603 RVA: 0x00026D48 File Offset: 0x00024F48
		internal static bool TryCreate(SecurityBindingElement sbe, TransportBindingElement transport, ReliableSessionBindingElement rsbe, TransactionFlowBindingElement tfbe, out Binding binding)
		{
			bool flag = rsbe != null;
			binding = null;
			HttpTransportSecurity defaultHttpTransportSecurity = WSHttpSecurity.GetDefaultHttpTransportSecurity();
			UnifiedSecurityMode mode;
			if (!WSHttpBinding.GetSecurityModeFromTransport(transport, defaultHttpTransportSecurity, out mode))
			{
				return false;
			}
			HttpsTransportBindingElement httpsTransportBindingElement = transport as HttpsTransportBindingElement;
			if (httpsTransportBindingElement != null && httpsTransportBindingElement.MessageSecurityVersion != null && httpsTransportBindingElement.MessageSecurityVersion.SecurityPolicyVersion != WSHttpBinding.WSMessageSecurityVersion.SecurityPolicyVersion)
			{
				return false;
			}
			WSHttpSecurity wshttpSecurity;
			if (WSHttpBinding.TryCreateSecurity(sbe, mode, defaultHttpTransportSecurity, flag, out wshttpSecurity))
			{
				WSHttpBinding wshttpBinding = new WSHttpBinding(wshttpSecurity, flag);
				bool allowCookies;
				if (!WSHttpBinding.TryGetAllowCookiesFromTransport(transport, out allowCookies))
				{
					return false;
				}
				wshttpBinding.AllowCookies = allowCookies;
				binding = wshttpBinding;
			}
			return (rsbe == null || rsbe.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005) && (tfbe == null || tfbe.TransactionProtocol == TransactionProtocol.WSAtomicTransactionOctober2004) && binding != null;
		}

		// Token: 0x06000A2C RID: 2604 RVA: 0x00026DF8 File Offset: 0x00024FF8
		protected override TransportBindingElement GetTransport()
		{
			if (this.security.Mode == SecurityMode.None || this.security.Mode == SecurityMode.Message)
			{
				base.HttpTransport.ExtendedProtectionPolicy = this.security.Transport.ExtendedProtectionPolicy;
				return base.HttpTransport;
			}
			this.security.ApplyTransportSecurity(base.HttpsTransport);
			return base.HttpsTransport;
		}

		// Token: 0x06000A2D RID: 2605 RVA: 0x00026E59 File Offset: 0x00025059
		internal static bool GetSecurityModeFromTransport(TransportBindingElement transport, HttpTransportSecurity transportSecurity, out UnifiedSecurityMode mode)
		{
			mode = UnifiedSecurityMode.None;
			if (transport is HttpsTransportBindingElement)
			{
				mode = (UnifiedSecurityMode.Transport | UnifiedSecurityMode.TransportWithMessageCredential);
				WSHttpSecurity.ApplyTransportSecurity((HttpsTransportBindingElement)transport, transportSecurity);
			}
			else
			{
				if (!(transport is HttpTransportBindingElement))
				{
					return false;
				}
				mode = (UnifiedSecurityMode.None | UnifiedSecurityMode.Message);
			}
			return true;
		}

		// Token: 0x06000A2E RID: 2606 RVA: 0x00026E8C File Offset: 0x0002508C
		internal static bool TryGetAllowCookiesFromTransport(TransportBindingElement transport, out bool allowCookies)
		{
			HttpTransportBindingElement httpTransportBindingElement = transport as HttpTransportBindingElement;
			if (httpTransportBindingElement == null)
			{
				allowCookies = false;
				return false;
			}
			allowCookies = httpTransportBindingElement.AllowCookies;
			return true;
		}

		// Token: 0x06000A2F RID: 2607 RVA: 0x00026EB1 File Offset: 0x000250B1
		protected override SecurityBindingElement CreateMessageSecurity()
		{
			return this.security.CreateMessageSecurity(base.ReliableSession.Enabled, WSHttpBinding.WSMessageSecurityVersion);
		}

		// Token: 0x06000A30 RID: 2608 RVA: 0x00026ECE File Offset: 0x000250CE
		private static bool TryCreateSecurity(SecurityBindingElement sbe, UnifiedSecurityMode mode, HttpTransportSecurity transportSecurity, bool isReliableSession, out WSHttpSecurity security)
		{
			return WSHttpSecurity.TryCreate(sbe, mode, transportSecurity, isReliableSession, out security) && SecurityElementBase.AreBindingsMatching(security.CreateMessageSecurity(isReliableSession, WSHttpBinding.WSMessageSecurityVersion), sbe);
		}

		// Token: 0x06000A31 RID: 2609 RVA: 0x00026EF3 File Offset: 0x000250F3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.Security.InternalShouldSerialize();
		}

		// Token: 0x04000BAC RID: 2988
		private static readonly MessageSecurityVersion WSMessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;

		// Token: 0x04000BAD RID: 2989
		private WSHttpSecurity security;
	}
}
