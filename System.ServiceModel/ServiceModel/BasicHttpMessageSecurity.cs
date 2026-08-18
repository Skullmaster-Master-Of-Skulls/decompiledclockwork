using System;
using System.ComponentModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x02000130 RID: 304
	public sealed class BasicHttpMessageSecurity
	{
		// Token: 0x06000852 RID: 2130 RVA: 0x00021F36 File Offset: 0x00020136
		public BasicHttpMessageSecurity()
		{
			this.clientCredentialType = BasicHttpMessageCredentialType.UserName;
			this.algorithmSuite = SecurityAlgorithmSuite.Default;
		}

		// Token: 0x17000221 RID: 545
		// (get) Token: 0x06000853 RID: 2131 RVA: 0x00021F50 File Offset: 0x00020150
		// (set) Token: 0x06000854 RID: 2132 RVA: 0x00021F58 File Offset: 0x00020158
		public BasicHttpMessageCredentialType ClientCredentialType
		{
			get
			{
				return this.clientCredentialType;
			}
			set
			{
				if (!BasicHttpMessageCredentialTypeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.clientCredentialType = value;
			}
		}

		// Token: 0x17000222 RID: 546
		// (get) Token: 0x06000855 RID: 2133 RVA: 0x00021F7E File Offset: 0x0002017E
		// (set) Token: 0x06000856 RID: 2134 RVA: 0x00021F86 File Offset: 0x00020186
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
			}
		}

		// Token: 0x06000857 RID: 2135 RVA: 0x00021FA4 File Offset: 0x000201A4
		internal SecurityBindingElement CreateMessageSecurity(bool isSecureTransportMode)
		{
			SecurityBindingElement securityBindingElement;
			if (isSecureTransportMode)
			{
				MessageSecurityVersion wssecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile = MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10;
				BasicHttpMessageCredentialType basicHttpMessageCredentialType = this.clientCredentialType;
				if (basicHttpMessageCredentialType != BasicHttpMessageCredentialType.UserName)
				{
					if (basicHttpMessageCredentialType != BasicHttpMessageCredentialType.Certificate)
					{
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new NotSupportedException());
					}
					securityBindingElement = SecurityBindingElement.CreateCertificateOverTransportBindingElement(wssecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile);
				}
				else
				{
					securityBindingElement = SecurityBindingElement.CreateUserNameOverTransportBindingElement();
					securityBindingElement.MessageSecurityVersion = wssecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile;
				}
			}
			else
			{
				if (this.clientCredentialType != BasicHttpMessageCredentialType.Certificate)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("BasicHttpMessageSecurityRequiresCertificate")));
				}
				securityBindingElement = SecurityBindingElement.CreateMutualCertificateBindingElement(MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10, true);
			}
			securityBindingElement.DefaultAlgorithmSuite = this.AlgorithmSuite;
			securityBindingElement.SecurityHeaderLayout = SecurityHeaderLayout.Lax;
			securityBindingElement.SetKeyDerivation(false);
			securityBindingElement.DoNotEmitTrust = true;
			return securityBindingElement;
		}

		// Token: 0x06000858 RID: 2136 RVA: 0x00022044 File Offset: 0x00020244
		internal static bool TryCreate(SecurityBindingElement sbe, out BasicHttpMessageSecurity security, out bool isSecureTransportMode)
		{
			security = null;
			isSecureTransportMode = false;
			if (!sbe.DoNotEmitTrust)
			{
				return false;
			}
			if (!sbe.IsSetKeyDerivation(false))
			{
				return false;
			}
			if (sbe.SecurityHeaderLayout != SecurityHeaderLayout.Lax)
			{
				return false;
			}
			if (sbe.MessageSecurityVersion != MessageSecurityVersion.WSSecurity10WSTrustFebruary2005WSSecureConversationFebruary2005WSSecurityPolicy11BasicSecurityProfile10)
			{
				return false;
			}
			BasicHttpMessageCredentialType basicHttpMessageCredentialType;
			if (!SecurityBindingElement.IsMutualCertificateBinding(sbe, true))
			{
				isSecureTransportMode = true;
				if (SecurityBindingElement.IsCertificateOverTransportBinding(sbe))
				{
					basicHttpMessageCredentialType = BasicHttpMessageCredentialType.Certificate;
				}
				else
				{
					if (!SecurityBindingElement.IsUserNameOverTransportBinding(sbe))
					{
						return false;
					}
					basicHttpMessageCredentialType = BasicHttpMessageCredentialType.UserName;
				}
			}
			else
			{
				basicHttpMessageCredentialType = BasicHttpMessageCredentialType.Certificate;
			}
			security = new BasicHttpMessageSecurity();
			security.ClientCredentialType = basicHttpMessageCredentialType;
			security.AlgorithmSuite = sbe.DefaultAlgorithmSuite;
			return true;
		}

		// Token: 0x06000859 RID: 2137 RVA: 0x000220CB File Offset: 0x000202CB
		internal bool InternalShouldSerialize()
		{
			return this.ShouldSerializeAlgorithmSuite() || this.ShouldSerializeClientCredentialType();
		}

		// Token: 0x0600085A RID: 2138 RVA: 0x000220DD File Offset: 0x000202DD
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeAlgorithmSuite()
		{
			return this.algorithmSuite.GetType() != SecurityAlgorithmSuite.Default.GetType();
		}

		// Token: 0x0600085B RID: 2139 RVA: 0x000220F9 File Offset: 0x000202F9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeClientCredentialType()
		{
			return this.clientCredentialType > BasicHttpMessageCredentialType.UserName;
		}

		// Token: 0x04000B0E RID: 2830
		internal const BasicHttpMessageCredentialType DefaultClientCredentialType = BasicHttpMessageCredentialType.UserName;

		// Token: 0x04000B0F RID: 2831
		private BasicHttpMessageCredentialType clientCredentialType;

		// Token: 0x04000B10 RID: 2832
		private SecurityAlgorithmSuite algorithmSuite;
	}
}
