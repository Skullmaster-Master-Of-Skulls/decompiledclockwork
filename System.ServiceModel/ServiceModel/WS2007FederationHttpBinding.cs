using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x0200015B RID: 347
	public class WS2007FederationHttpBinding : WSFederationHttpBinding
	{
		// Token: 0x06000A09 RID: 2569 RVA: 0x00026850 File Offset: 0x00024A50
		public WS2007FederationHttpBinding(string configName) : this()
		{
			this.ApplyConfiguration(configName);
		}

		// Token: 0x06000A0A RID: 2570 RVA: 0x0002685F File Offset: 0x00024A5F
		public WS2007FederationHttpBinding()
		{
			base.ReliableSessionBindingElement.ReliableMessagingVersion = WS2007FederationHttpBinding.WS2007ReliableMessagingVersion;
			base.TransactionFlowBindingElement.TransactionProtocol = WS2007FederationHttpBinding.WS2007TransactionProtocol;
			base.HttpsTransport.MessageSecurityVersion = WS2007FederationHttpBinding.WS2007MessageSecurityVersion;
		}

		// Token: 0x06000A0B RID: 2571 RVA: 0x00026897 File Offset: 0x00024A97
		public WS2007FederationHttpBinding(WSFederationHttpSecurityMode securityMode) : this(securityMode, false)
		{
		}

		// Token: 0x06000A0C RID: 2572 RVA: 0x000268A1 File Offset: 0x00024AA1
		public WS2007FederationHttpBinding(WSFederationHttpSecurityMode securityMode, bool reliableSessionEnabled) : base(securityMode, reliableSessionEnabled)
		{
			base.ReliableSessionBindingElement.ReliableMessagingVersion = WS2007FederationHttpBinding.WS2007ReliableMessagingVersion;
			base.TransactionFlowBindingElement.TransactionProtocol = WS2007FederationHttpBinding.WS2007TransactionProtocol;
			base.HttpsTransport.MessageSecurityVersion = WS2007FederationHttpBinding.WS2007MessageSecurityVersion;
		}

		// Token: 0x06000A0D RID: 2573 RVA: 0x000268DB File Offset: 0x00024ADB
		private WS2007FederationHttpBinding(WSFederationHttpSecurity security, PrivacyNoticeBindingElement privacy, bool reliableSessionEnabled) : base(security, privacy, reliableSessionEnabled)
		{
			base.ReliableSessionBindingElement.ReliableMessagingVersion = WS2007FederationHttpBinding.WS2007ReliableMessagingVersion;
			base.TransactionFlowBindingElement.TransactionProtocol = WS2007FederationHttpBinding.WS2007TransactionProtocol;
			base.HttpsTransport.MessageSecurityVersion = WS2007FederationHttpBinding.WS2007MessageSecurityVersion;
		}

		// Token: 0x06000A0E RID: 2574 RVA: 0x00026918 File Offset: 0x00024B18
		private void ApplyConfiguration(string configurationName)
		{
			WS2007FederationHttpBindingCollectionElement bindingCollectionElement = WS2007FederationHttpBindingCollectionElement.GetBindingCollectionElement();
			WS2007FederationHttpBindingElement ws2007FederationHttpBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (ws2007FederationHttpBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"ws2007FederationHttpBinding"
				})));
			}
			ws2007FederationHttpBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x06000A0F RID: 2575 RVA: 0x0002696E File Offset: 0x00024B6E
		protected override SecurityBindingElement CreateMessageSecurity()
		{
			return base.Security.CreateMessageSecurity(base.ReliableSession.Enabled, WS2007FederationHttpBinding.WS2007MessageSecurityVersion);
		}

		// Token: 0x06000A10 RID: 2576 RVA: 0x0002698C File Offset: 0x00024B8C
		internal new static bool TryCreate(SecurityBindingElement sbe, TransportBindingElement transport, PrivacyNoticeBindingElement privacy, ReliableSessionBindingElement rsbe, TransactionFlowBindingElement tfbe, out Binding binding)
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
			if (httpsTransportBindingElement != null && httpsTransportBindingElement.MessageSecurityVersion != null && httpsTransportBindingElement.MessageSecurityVersion.SecurityPolicyVersion != WS2007FederationHttpBinding.WS2007MessageSecurityVersion.SecurityPolicyVersion)
			{
				return false;
			}
			WSFederationHttpSecurity security;
			if (WS2007FederationHttpBinding.TryCreateSecurity(sbe, mode, transportSecurity, flag, out security))
			{
				binding = new WS2007FederationHttpBinding(security, privacy, flag);
			}
			return (rsbe == null || rsbe.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11) && (tfbe == null || tfbe.TransactionProtocol == TransactionProtocol.WSAtomicTransaction11) && binding != null;
		}

		// Token: 0x06000A11 RID: 2577 RVA: 0x00026A25 File Offset: 0x00024C25
		private static bool TryCreateSecurity(SecurityBindingElement sbe, WSFederationHttpSecurityMode mode, HttpTransportSecurity transportSecurity, bool isReliableSession, out WSFederationHttpSecurity security)
		{
			return WSFederationHttpSecurity.TryCreate(sbe, mode, transportSecurity, isReliableSession, WS2007FederationHttpBinding.WS2007MessageSecurityVersion, out security) && SecurityElementBase.AreBindingsMatching(security.CreateMessageSecurity(isReliableSession, WS2007FederationHttpBinding.WS2007MessageSecurityVersion), sbe);
		}

		// Token: 0x04000BA2 RID: 2978
		private static readonly ReliableMessagingVersion WS2007ReliableMessagingVersion = ReliableMessagingVersion.WSReliableMessaging11;

		// Token: 0x04000BA3 RID: 2979
		private static readonly TransactionProtocol WS2007TransactionProtocol = TransactionProtocol.WSAtomicTransaction11;

		// Token: 0x04000BA4 RID: 2980
		private static readonly MessageSecurityVersion WS2007MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
	}
}
