using System;
using System.Configuration;
using System.ServiceModel.Channels;
using System.ServiceModel.Configuration;

namespace System.ServiceModel
{
	// Token: 0x02000160 RID: 352
	public class WS2007HttpBinding : WSHttpBinding
	{
		// Token: 0x06000A33 RID: 2611 RVA: 0x00026F0C File Offset: 0x0002510C
		public WS2007HttpBinding(string configName) : this()
		{
			this.ApplyConfiguration(configName);
		}

		// Token: 0x06000A34 RID: 2612 RVA: 0x00026F1B File Offset: 0x0002511B
		public WS2007HttpBinding()
		{
			base.ReliableSessionBindingElement.ReliableMessagingVersion = WS2007HttpBinding.WS2007ReliableMessagingVersion;
			base.TransactionFlowBindingElement.TransactionProtocol = WS2007HttpBinding.WS2007TransactionProtocol;
			base.HttpsTransport.MessageSecurityVersion = WS2007HttpBinding.WS2007MessageSecurityVersion;
		}

		// Token: 0x06000A35 RID: 2613 RVA: 0x00026F53 File Offset: 0x00025153
		public WS2007HttpBinding(SecurityMode securityMode) : this(securityMode, false)
		{
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00026F5D File Offset: 0x0002515D
		public WS2007HttpBinding(SecurityMode securityMode, bool reliableSessionEnabled) : base(securityMode, reliableSessionEnabled)
		{
			base.ReliableSessionBindingElement.ReliableMessagingVersion = WS2007HttpBinding.WS2007ReliableMessagingVersion;
			base.TransactionFlowBindingElement.TransactionProtocol = WS2007HttpBinding.WS2007TransactionProtocol;
			base.HttpsTransport.MessageSecurityVersion = WS2007HttpBinding.WS2007MessageSecurityVersion;
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x00026F97 File Offset: 0x00025197
		internal WS2007HttpBinding(WSHttpSecurity security, bool reliableSessionEnabled) : base(security, reliableSessionEnabled)
		{
			base.ReliableSessionBindingElement.ReliableMessagingVersion = WS2007HttpBinding.WS2007ReliableMessagingVersion;
			base.TransactionFlowBindingElement.TransactionProtocol = WS2007HttpBinding.WS2007TransactionProtocol;
			base.HttpsTransport.MessageSecurityVersion = WS2007HttpBinding.WS2007MessageSecurityVersion;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00026FD4 File Offset: 0x000251D4
		private void ApplyConfiguration(string configurationName)
		{
			WS2007HttpBindingCollectionElement bindingCollectionElement = WS2007HttpBindingCollectionElement.GetBindingCollectionElement();
			WS2007HttpBindingElement ws2007HttpBindingElement = bindingCollectionElement.Bindings[configurationName];
			if (ws2007HttpBindingElement == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ConfigurationErrorsException(SR.GetString("ConfigInvalidBindingConfigurationName", new object[]
				{
					configurationName,
					"ws2007HttpBinding"
				})));
			}
			ws2007HttpBindingElement.ApplyConfiguration(this);
		}

		// Token: 0x06000A39 RID: 2617 RVA: 0x0002702A File Offset: 0x0002522A
		protected override SecurityBindingElement CreateMessageSecurity()
		{
			return base.Security.CreateMessageSecurity(base.ReliableSession.Enabled, WS2007HttpBinding.WS2007MessageSecurityVersion);
		}

		// Token: 0x06000A3A RID: 2618 RVA: 0x00027048 File Offset: 0x00025248
		internal new static bool TryCreate(SecurityBindingElement sbe, TransportBindingElement transport, ReliableSessionBindingElement rsbe, TransactionFlowBindingElement tfbe, out Binding binding)
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
			if (httpsTransportBindingElement != null && httpsTransportBindingElement.MessageSecurityVersion != null && httpsTransportBindingElement.MessageSecurityVersion.SecurityPolicyVersion != WS2007HttpBinding.WS2007MessageSecurityVersion.SecurityPolicyVersion)
			{
				return false;
			}
			WSHttpSecurity security;
			if (WS2007HttpBinding.TryCreateSecurity(sbe, mode, defaultHttpTransportSecurity, flag, out security))
			{
				WS2007HttpBinding ws2007HttpBinding = new WS2007HttpBinding(security, flag);
				bool allowCookies;
				if (!WSHttpBinding.TryGetAllowCookiesFromTransport(transport, out allowCookies))
				{
					return false;
				}
				ws2007HttpBinding.AllowCookies = allowCookies;
				binding = ws2007HttpBinding;
			}
			return (rsbe == null || rsbe.ReliableMessagingVersion == ReliableMessagingVersion.WSReliableMessaging11) && (tfbe == null || tfbe.TransactionProtocol == TransactionProtocol.WSAtomicTransaction11) && binding != null;
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x000270F7 File Offset: 0x000252F7
		private static bool TryCreateSecurity(SecurityBindingElement sbe, UnifiedSecurityMode mode, HttpTransportSecurity transportSecurity, bool isReliableSession, out WSHttpSecurity security)
		{
			return WSHttpSecurity.TryCreate(sbe, mode, transportSecurity, isReliableSession, out security) && SecurityElementBase.AreBindingsMatching(security.CreateMessageSecurity(isReliableSession, WS2007HttpBinding.WS2007MessageSecurityVersion), sbe);
		}

		// Token: 0x04000BAE RID: 2990
		private static readonly ReliableMessagingVersion WS2007ReliableMessagingVersion = ReliableMessagingVersion.WSReliableMessaging11;

		// Token: 0x04000BAF RID: 2991
		private static readonly TransactionProtocol WS2007TransactionProtocol = TransactionProtocol.WSAtomicTransaction11;

		// Token: 0x04000BB0 RID: 2992
		private static readonly MessageSecurityVersion WS2007MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrust13WSSecureConversation13WSSecurityPolicy12BasicSecurityProfile10;
	}
}
