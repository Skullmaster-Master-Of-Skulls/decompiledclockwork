using System;
using System.ComponentModel;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Channels;
using System.ServiceModel.Security;

namespace System.ServiceModel
{
	// Token: 0x02000155 RID: 341
	[__DynamicallyInvokable]
	public sealed class TcpTransportSecurity
	{
		// Token: 0x060009D5 RID: 2517 RVA: 0x000261DA File Offset: 0x000243DA
		[__DynamicallyInvokable]
		public TcpTransportSecurity()
		{
			this.clientCredentialType = TcpClientCredentialType.Windows;
			this.protectionLevel = ProtectionLevel.EncryptAndSign;
			this.extendedProtectionPolicy = ChannelBindingUtility.DefaultPolicy;
			this.sslProtocols = TransportDefaults.SslProtocols;
		}

		// Token: 0x17000289 RID: 649
		// (get) Token: 0x060009D6 RID: 2518 RVA: 0x00026206 File Offset: 0x00024406
		// (set) Token: 0x060009D7 RID: 2519 RVA: 0x0002620E File Offset: 0x0002440E
		[DefaultValue(TcpClientCredentialType.Windows)]
		[__DynamicallyInvokable]
		public TcpClientCredentialType ClientCredentialType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.clientCredentialType;
			}
			[__DynamicallyInvokable]
			set
			{
				if (!TcpClientCredentialTypeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.clientCredentialType = value;
			}
		}

		// Token: 0x1700028A RID: 650
		// (get) Token: 0x060009D8 RID: 2520 RVA: 0x00026234 File Offset: 0x00024434
		// (set) Token: 0x060009D9 RID: 2521 RVA: 0x0002623C File Offset: 0x0002443C
		[DefaultValue(ProtectionLevel.EncryptAndSign)]
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
			set
			{
				if (!ProtectionLevelHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.protectionLevel = value;
			}
		}

		// Token: 0x1700028B RID: 651
		// (get) Token: 0x060009DA RID: 2522 RVA: 0x00026262 File Offset: 0x00024462
		// (set) Token: 0x060009DB RID: 2523 RVA: 0x0002626C File Offset: 0x0002446C
		public ExtendedProtectionPolicy ExtendedProtectionPolicy
		{
			get
			{
				return this.extendedProtectionPolicy;
			}
			set
			{
				if (value == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
				}
				if (value.PolicyEnforcement == PolicyEnforcement.Always && !ExtendedProtectionPolicy.OSSupportsExtendedProtection)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new PlatformNotSupportedException(SR.GetString("ExtendedProtectionNotSupported")));
				}
				this.extendedProtectionPolicy = value;
			}
		}

		// Token: 0x1700028C RID: 652
		// (get) Token: 0x060009DC RID: 2524 RVA: 0x000262BD File Offset: 0x000244BD
		// (set) Token: 0x060009DD RID: 2525 RVA: 0x000262C5 File Offset: 0x000244C5
		[DefaultValue(SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12)]
		public SslProtocols SslProtocols
		{
			get
			{
				return this.sslProtocols;
			}
			set
			{
				SslProtocolsHelper.Validate(value);
				this.sslProtocols = value;
			}
		}

		// Token: 0x060009DE RID: 2526 RVA: 0x000262D4 File Offset: 0x000244D4
		private SslStreamSecurityBindingElement CreateSslBindingElement(bool requireClientCertificate)
		{
			if (this.protectionLevel != ProtectionLevel.EncryptAndSign)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnsupportedSslProtectionLevel", new object[]
				{
					this.protectionLevel
				})));
			}
			return new SslStreamSecurityBindingElement
			{
				RequireClientCertificate = requireClientCertificate,
				SslProtocols = this.sslProtocols
			};
		}

		// Token: 0x060009DF RID: 2527 RVA: 0x00026334 File Offset: 0x00024534
		private static bool IsSslBindingElement(BindingElement element, TcpTransportSecurity transportSecurity, out bool requireClientCertificate, out SslProtocols sslProtocols)
		{
			requireClientCertificate = false;
			sslProtocols = TransportDefaults.SslProtocols;
			SslStreamSecurityBindingElement sslStreamSecurityBindingElement = element as SslStreamSecurityBindingElement;
			if (sslStreamSecurityBindingElement == null)
			{
				return false;
			}
			transportSecurity.ProtectionLevel = ProtectionLevel.EncryptAndSign;
			requireClientCertificate = sslStreamSecurityBindingElement.RequireClientCertificate;
			sslProtocols = sslStreamSecurityBindingElement.SslProtocols;
			return true;
		}

		// Token: 0x060009E0 RID: 2528 RVA: 0x0002636F File Offset: 0x0002456F
		internal BindingElement CreateTransportProtectionOnly()
		{
			return this.CreateSslBindingElement(false);
		}

		// Token: 0x060009E1 RID: 2529 RVA: 0x00026378 File Offset: 0x00024578
		internal static bool SetTransportProtectionOnly(BindingElement transport, TcpTransportSecurity transportSecurity)
		{
			bool flag;
			SslProtocols sslProtocols;
			return TcpTransportSecurity.IsSslBindingElement(transport, transportSecurity, out flag, out sslProtocols);
		}

		// Token: 0x060009E2 RID: 2530 RVA: 0x00026390 File Offset: 0x00024590
		internal BindingElement CreateTransportProtectionAndAuthentication()
		{
			if (this.clientCredentialType == TcpClientCredentialType.Certificate || this.clientCredentialType == TcpClientCredentialType.None)
			{
				return this.CreateSslBindingElement(this.clientCredentialType == TcpClientCredentialType.Certificate);
			}
			return new WindowsStreamSecurityBindingElement
			{
				ProtectionLevel = this.protectionLevel
			};
		}

		// Token: 0x060009E3 RID: 2531 RVA: 0x000263D4 File Offset: 0x000245D4
		internal static bool SetTransportProtectionAndAuthentication(BindingElement transport, TcpTransportSecurity transportSecurity)
		{
			bool flag = false;
			SslProtocols sslProtocols = TransportDefaults.SslProtocols;
			if (transport is WindowsStreamSecurityBindingElement)
			{
				transportSecurity.ClientCredentialType = TcpClientCredentialType.Windows;
				transportSecurity.ProtectionLevel = ((WindowsStreamSecurityBindingElement)transport).ProtectionLevel;
				return true;
			}
			if (TcpTransportSecurity.IsSslBindingElement(transport, transportSecurity, out flag, out sslProtocols))
			{
				transportSecurity.ClientCredentialType = (flag ? TcpClientCredentialType.Certificate : TcpClientCredentialType.None);
				transportSecurity.SslProtocols = sslProtocols;
				return true;
			}
			return false;
		}

		// Token: 0x060009E4 RID: 2532 RVA: 0x0002642F File Offset: 0x0002462F
		internal bool InternalShouldSerialize()
		{
			return this.ClientCredentialType != TcpClientCredentialType.Windows || this.ProtectionLevel != ProtectionLevel.EncryptAndSign || this.SslProtocols != TransportDefaults.SslProtocols || this.ShouldSerializeExtendedProtectionPolicy();
		}

		// Token: 0x060009E5 RID: 2533 RVA: 0x00026458 File Offset: 0x00024658
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeExtendedProtectionPolicy()
		{
			return !ChannelBindingUtility.AreEqual(this.ExtendedProtectionPolicy, ChannelBindingUtility.DefaultPolicy);
		}

		// Token: 0x04000B93 RID: 2963
		internal const TcpClientCredentialType DefaultClientCredentialType = TcpClientCredentialType.Windows;

		// Token: 0x04000B94 RID: 2964
		internal const ProtectionLevel DefaultProtectionLevel = ProtectionLevel.EncryptAndSign;

		// Token: 0x04000B95 RID: 2965
		private TcpClientCredentialType clientCredentialType;

		// Token: 0x04000B96 RID: 2966
		private ProtectionLevel protectionLevel;

		// Token: 0x04000B97 RID: 2967
		private ExtendedProtectionPolicy extendedProtectionPolicy;

		// Token: 0x04000B98 RID: 2968
		private SslProtocols sslProtocols;
	}
}
