using System;
using System.ComponentModel;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x0200015A RID: 346
	public class WSFederationHttpBinding : WSHttpBindingBase
	{
		// Token: 0x060009F4 RID: 2548 RVA: 0x00026585 File Offset: 0x00024785
		public WSFederationHttpBinding(string configName) : this()
		{
			this.ApplyConfiguration(configName);
		}

		// Token: 0x060009F5 RID: 2549 RVA: 0x00026594 File Offset: 0x00024794
		public WSFederationHttpBinding()
		{
			this.security = new WSFederationHttpSecurity();
			base..ctor();
		}

		// Token: 0x060009F6 RID: 2550 RVA: 0x000265A7 File Offset: 0x000247A7
		public WSFederationHttpBinding(WSFederationHttpSecurityMode securityMode) : this(securityMode, false)
		{
		}

		// Token: 0x060009F7 RID: 2551 RVA: 0x000265B1 File Offset: 0x000247B1
		public WSFederationHttpBinding(WSFederationHttpSecurityMode securityMode, bool reliableSessionEnabled)
		{
			this.security = new WSFederationHttpSecurity();
			base..ctor(reliableSessionEnabled);
			this.security.Mode = securityMode;
		}

		// Token: 0x060009F8 RID: 2552 RVA: 0x000265D1 File Offset: 0x000247D1
		internal WSFederationHttpBinding(WSFederationHttpSecurity security, PrivacyNoticeBindingElement privacy, bool reliableSessionEnabled)
		{
			this.security = new WSFederationHttpSecurity();
			base..ctor(reliableSessionEnabled);
			this.security = security;
			if (privacy != null)
			{
				this.privacyNoticeAt = privacy.Url;
				this.privacyNoticeVersion = privacy.Version;
			}
		}

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x060009F9 RID: 2553 RVA: 0x00026607 File Offset: 0x00024807
		// (set) Token: 0x060009FA RID: 2554 RVA: 0x0002660F File Offset: 0x0002480F
		[DefaultValue(null)]
		public Uri PrivacyNoticeAt
		{
			get
			{
				return this.privacyNoticeAt;
			}
			set
			{
				this.privacyNoticeAt = value;
			}
		}

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x00026618 File Offset: 0x00024818
		// (set) Token: 0x060009FC RID: 2556 RVA: 0x00026620 File Offset: 0x00024820
		[DefaultValue(0)]
		public int PrivacyNoticeVersion
		{
			get
			{
				return this.privacyNoticeVersion;
			}
			set
			{
				this.privacyNoticeVersion = value;
			}
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x00026629 File Offset: 0x00024829
		// (set) Token: 0x060009FE RID: 2558 RVA: 0x00026631 File Offset: 0x00024831
		public WSFederationHttpSecurity Security
		{
			get
			{
				return this.security;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.security = value;
			}
		}

		// Token: 0x060009FF RID: 2559 RVA: 0x00026650 File Offset: 0x00024850
		private void ApplyConfiguration(string configurationName)
		{
			WSFederationHttpBindingCollectionElement bindingCollectionElement = WSFederationHttpBindingCollectionElement.GetBindingCollectionElement();
			WSFederationHttpBindingElement wsfederationHttpBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (wsfederationHttpBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"wsFederationHttpBinding"
				})));
			}
			wsfederationHttpBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x06000A00 RID: 2560 RVA: 0x000266A8 File Offset: 0x000248A8
		private PrivacyNoticeBindingElement CreatePrivacyPolicy()
		{
			PrivacyNoticeBindingElement privacyNoticeBindingElement = null;
			if (this.PrivacyNoticeAt != null)
			{
				privacyNoticeBindingElement = new PrivacyNoticeBindingElement();
				privacyNoticeBindingElement.Url = this.PrivacyNoticeAt;
				privacyNoticeBindingElement.Version = this.privacyNoticeVersion;
			}
			return privacyNoticeBindingElement;
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x000266E4 File Offset: 0x000248E4
		internal static bool TryCreate(SecurityBindingElement sbe, TransportBindingElement transport, PrivacyNoticeBindingElement privacy, ReliableSessionBindingElement rsbe, TransactionFlowBindingElement tfbe, out Binding binding)
		{
			bool flag = rsbe != null;
			binding = null;
			HttpTransportSecurity transportSecurity = new HttpTransportSecurity();
			WSFederationHttpSecurityMode mode;
			if (!WSFederationHttpBinding.GetSecurityModeFromTransport(transport, transportSecurity, out mode))
			{
				return false;
			}
			HttpsTransportBindingElement httpsTransportBindingElement = transport as HttpsTransportBindingElement;
			if (httpsTransportBindingElement != null && httpsTransportBindingElement.MessageSecurityVersion != null && httpsTransportBindingElement.MessageSecurityVersion.SecurityPolicyVersion != WSFederationHttpBinding.WSMessageSecurityVersion.SecurityPolicyVersion)
			{
				return false;
			}
			WSFederationHttpSecurity wsfederationHttpSecurity;
			if (WSFederationHttpBinding.TryCreateSecurity(sbe, mode, transportSecurity, flag, out wsfederationHttpSecurity))
			{
				binding = new WSFederationHttpBinding(wsfederationHttpSecurity, privacy, flag);
			}
			return (rsbe == null || rsbe.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessagingFebruary2005) && (tfbe == null || tfbe.TransactionProtocol == TransactionProtocol.WSAtomicTransactionOctober2004) && binding != null;
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002677D File Offset: 0x0002497D
		protected override TransportBindingElement GetTransport()
		{
			if (this.security.Mode == WSFederationHttpSecurityMode.None || this.security.Mode == WSFederationHttpSecurityMode.Message)
			{
				return base.HttpTransport;
			}
			return base.HttpsTransport;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x000267A7 File Offset: 0x000249A7
		internal static bool GetSecurityModeFromTransport(TransportBindingElement transport, HttpTransportSecurity transportSecurity, out WSFederationHttpSecurityMode mode)
		{
			mode = (WSFederationHttpSecurityMode)3;
			if (transport is HttpsTransportBindingElement)
			{
				mode = WSFederationHttpSecurityMode.TransportWithMessageCredential;
			}
			else
			{
				if (!(transport is HttpTransportBindingElement))
				{
					return false;
				}
				mode = WSFederationHttpSecurityMode.Message;
			}
			return true;
		}

		// Token: 0x06000A04 RID: 2564 RVA: 0x000267C9 File Offset: 0x000249C9
		protected override SecurityBindingElement CreateMessageSecurity()
		{
			return this.security.CreateMessageSecurity(base.ReliableSession.Enabled, WSFederationHttpBinding.WSMessageSecurityVersion);
		}

		// Token: 0x06000A05 RID: 2565 RVA: 0x000267E6 File Offset: 0x000249E6
		private static bool TryCreateSecurity(SecurityBindingElement sbe, WSFederationHttpSecurityMode mode, HttpTransportSecurity transportSecurity, bool isReliableSession, out WSFederationHttpSecurity security)
		{
			return WSFederationHttpSecurity.TryCreate(sbe, mode, transportSecurity, isReliableSession, WSFederationHttpBinding.WSMessageSecurityVersion, out security) && SecurityElementBase.AreBindingsMatching(security.CreateMessageSecurity(isReliableSession, WSFederationHttpBinding.WSMessageSecurityVersion), sbe);
		}

		// Token: 0x06000A06 RID: 2566 RVA: 0x00026810 File Offset: 0x00024A10
		public override BindingElementCollection CreateBindingElements()
		{
			BindingElementCollection bindingElementCollection = base.CreateBindingElements();
			PrivacyNoticeBindingElement privacyNoticeBindingElement = this.CreatePrivacyPolicy();
			if (privacyNoticeBindingElement != null)
			{
				bindingElementCollection.Insert(0, privacyNoticeBindingElement);
			}
			return bindingElementCollection;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00026837 File Offset: 0x00024A37
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeSecurity()
		{
			return this.Security.InternalShouldSerialize();
		}

		// Token: 0x04000B9E RID: 2974
		private static readonly MessageSecurityVersion WSMessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;

		// Token: 0x04000B9F RID: 2975
		private Uri privacyNoticeAt;

		// Token: 0x04000BA0 RID: 2976
		private int privacyNoticeVersion;

		// Token: 0x04000BA1 RID: 2977
		private WSFederationHttpSecurity security;
	}
}
