using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel
{
	// Token: 0x0200013E RID: 318
	public sealed class MessageSecurityOverMsmq
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x0002371D File Offset: 0x0002191D
		public MessageSecurityOverMsmq()
		{
			this.clientCredentialType = MessageCredentialType.Windows;
			this.algorithmSuite = SecurityAlgorithmSuite.Default;
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00023737 File Offset: 0x00021937
		// (set) Token: 0x060008C6 RID: 2246 RVA: 0x0002373F File Offset: 0x0002193F
		[DefaultValue(MessageCredentialType.Windows)]
		public MessageCredentialType ClientCredentialType
		{
			get
			{
				return this.clientCredentialType;
			}
			set
			{
				if (!MessageCredentialTypeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.clientCredentialType = value;
			}
		}

		// Token: 0x1700023C RID: 572
		// (get) Token: 0x060008C7 RID: 2247 RVA: 0x00023765 File Offset: 0x00021965
		// (set) Token: 0x060008C8 RID: 2248 RVA: 0x0002376D File Offset: 0x0002196D
		[DefaultValue(typeof(SecurityAlgorithmSuite), "Default")]
		public SecurityAlgorithmSuite AlgorithmSuite
		{
			get
			{
				return this.algorithmSuite;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				this.algorithmSuite = value;
				this.wasAlgorithmSuiteSet = true;
			}
		}

		// Token: 0x1700023D RID: 573
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00023790 File Offset: 0x00021990
		internal bool WasAlgorithmSuiteSet
		{
			get
			{
				return this.wasAlgorithmSuiteSet;
			}
		}

		// Token: 0x060008CA RID: 2250 RVA: 0x00023798 File Offset: 0x00021998
		[MethodImpl(MethodImplOptions.NoInlining)]
		internal SecurityBindingElement CreateSecurityBindingElement()
		{
			bool flag = false;
			SymmetricSecurityBindingElement symmetricSecurityBindingElement;
			switch (this.clientCredentialType)
			{
			case MessageCredentialType.None:
				symmetricSecurityBindingElement = SecurityBindingElement.CreateAnonymousForCertificateBindingElement();
				break;
			case MessageCredentialType.Windows:
				symmetricSecurityBindingElement = SecurityBindingElement.CreateKerberosBindingElement();
				flag = true;
				break;
			case MessageCredentialType.UserName:
				symmetricSecurityBindingElement = SecurityBindingElement.CreateUserNameForCertificateBindingElement();
				break;
			case MessageCredentialType.Certificate:
				symmetricSecurityBindingElement = (SymmetricSecurityBindingElement)SecurityBindingElement.CreateMutualCertificateBindingElement();
				break;
			case MessageCredentialType.IssuedToken:
				symmetricSecurityBindingElement = SecurityBindingElement.CreateIssuedTokenForCertificateBindingElement(IssuedSecurityTokenParameters.CreateInfoCardParameters(new SecurityStandardsManager(), this.algorithmSuite));
				break;
			default:
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
			}
			symmetricSecurityBindingElement.MessageSecurityVersion = MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11;
			if (this.wasAlgorithmSuiteSet || !flag)
			{
				symmetricSecurityBindingElement.DefaultAlgorithmSuite = this.AlgorithmSuite;
			}
			else if (flag)
			{
				symmetricSecurityBindingElement.DefaultAlgorithmSuite = SecurityAlgorithmSuite.KerberosDefault;
			}
			symmetricSecurityBindingElement.IncludeTimestamp = false;
			symmetricSecurityBindingElement.LocalServiceSettings.DetectReplays = false;
			symmetricSecurityBindingElement.LocalClientSettings.DetectReplays = false;
			return symmetricSecurityBindingElement;
		}

		// Token: 0x060008CB RID: 2251 RVA: 0x0002386C File Offset: 0x00021A6C
		internal static bool TryCreate(SecurityBindingElement sbe, out MessageSecurityOverMsmq messageSecurity)
		{
			messageSecurity = null;
			if (sbe == null)
			{
				return false;
			}
			SymmetricSecurityBindingElement symmetricSecurityBindingElement = sbe as SymmetricSecurityBindingElement;
			if (symmetricSecurityBindingElement == null)
			{
				return false;
			}
			if (sbe.MessageSecurityVersion != MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10 && sbe.MessageSecurityVersion != MessageSecurityVersion.WSSecurity11WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11)
			{
				return false;
			}
			if (symmetricSecurityBindingElement.IncludeTimestamp)
			{
				return false;
			}
			bool flag = false;
			MessageCredentialType messageCredentialType;
			if (SecurityBindingElement.IsAnonymousForCertificateBinding(sbe))
			{
				messageCredentialType = MessageCredentialType.None;
			}
			else if (SecurityBindingElement.IsUserNameForCertificateBinding(sbe))
			{
				messageCredentialType = MessageCredentialType.UserName;
			}
			else if (SecurityBindingElement.IsMutualCertificateBinding(sbe))
			{
				messageCredentialType = MessageCredentialType.Certificate;
			}
			else if (SecurityBindingElement.IsKerberosBinding(sbe))
			{
				messageCredentialType = MessageCredentialType.Windows;
				flag = true;
			}
			else
			{
				IssuedSecurityTokenParameters parameters;
				if (!SecurityBindingElement.IsIssuedTokenForCertificateBinding(sbe, out parameters))
				{
					return false;
				}
				if (!IssuedSecurityTokenParameters.IsInfoCardParameters(parameters, new SecurityStandardsManager(sbe.MessageSecurityVersion, new WSSecurityTokenSerializer(sbe.MessageSecurityVersion.SecurityVersion, sbe.MessageSecurityVersion.TrustVersion, sbe.MessageSecurityVersion.SecureConversationVersion, true, null, null, null))))
				{
					return false;
				}
				messageCredentialType = MessageCredentialType.IssuedToken;
			}
			messageSecurity = new MessageSecurityOverMsmq();
			messageSecurity.ClientCredentialType = messageCredentialType;
			if (messageCredentialType != MessageCredentialType.IssuedToken && !flag)
			{
				messageSecurity.AlgorithmSuite = symmetricSecurityBindingElement.DefaultAlgorithmSuite;
			}
			return true;
		}

		// Token: 0x04000B4D RID: 2893
		internal const MessageCredentialType DefaultClientCredentialType = MessageCredentialType.Windows;

		// Token: 0x04000B4E RID: 2894
		private MessageCredentialType clientCredentialType;

		// Token: 0x04000B4F RID: 2895
		private SecurityAlgorithmSuite algorithmSuite;

		// Token: 0x04000B50 RID: 2896
		private bool wasAlgorithmSuiteSet;
	}
}
