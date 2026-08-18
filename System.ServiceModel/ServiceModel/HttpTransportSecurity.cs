using System;
using System.ComponentModel;
using System.Net;
using System.Security.Authentication.ExtendedProtection;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x02000139 RID: 313
	[__DynamicallyInvokable]
	public sealed class HttpTransportSecurity
	{
		// Token: 0x06000892 RID: 2194 RVA: 0x00022A62 File Offset: 0x00020C62
		[__DynamicallyInvokable]
		public HttpTransportSecurity()
		{
			this.clientCredentialType = HttpClientCredentialType.None;
			this.proxyCredentialType = HttpProxyCredentialType.None;
			this.realm = "";
			this.extendedProtectionPolicy = ChannelBindingUtility.DefaultPolicy;
		}

		// Token: 0x17000230 RID: 560
		// (get) Token: 0x06000893 RID: 2195 RVA: 0x00022A8E File Offset: 0x00020C8E
		// (set) Token: 0x06000894 RID: 2196 RVA: 0x00022A96 File Offset: 0x00020C96
		[__DynamicallyInvokable]
		public HttpClientCredentialType ClientCredentialType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.clientCredentialType;
			}
			[__DynamicallyInvokable]
			set
			{
				if (!HttpClientCredentialTypeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.clientCredentialType = value;
			}
		}

		// Token: 0x17000231 RID: 561
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x00022ABC File Offset: 0x00020CBC
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x00022AC4 File Offset: 0x00020CC4
		public HttpProxyCredentialType ProxyCredentialType
		{
			get
			{
				return this.proxyCredentialType;
			}
			set
			{
				if (!HttpProxyCredentialTypeHelper.IsDefined(value))
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value"));
				}
				this.proxyCredentialType = value;
			}
		}

		// Token: 0x17000232 RID: 562
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x00022AEA File Offset: 0x00020CEA
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x00022AF2 File Offset: 0x00020CF2
		public string Realm
		{
			get
			{
				return this.realm;
			}
			set
			{
				this.realm = value;
			}
		}

		// Token: 0x17000233 RID: 563
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x00022AFB File Offset: 0x00020CFB
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x00022B04 File Offset: 0x00020D04
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

		// Token: 0x0600089B RID: 2203 RVA: 0x00022B55 File Offset: 0x00020D55
		internal void ConfigureTransportProtectionOnly(HttpsTransportBindingElement https)
		{
			this.DisableAuthentication(https);
			https.RequireClientCertificate = false;
		}

		// Token: 0x0600089C RID: 2204 RVA: 0x00022B65 File Offset: 0x00020D65
		private void ConfigureAuthentication(HttpTransportBindingElement http)
		{
			http.AuthenticationScheme = HttpClientCredentialTypeHelper.MapToAuthenticationScheme(this.clientCredentialType);
			http.ProxyAuthenticationScheme = HttpProxyCredentialTypeHelper.MapToAuthenticationScheme(this.proxyCredentialType);
			http.Realm = this.Realm;
			http.ExtendedProtectionPolicy = this.extendedProtectionPolicy;
		}

		// Token: 0x0600089D RID: 2205 RVA: 0x00022BA1 File Offset: 0x00020DA1
		private static void ConfigureAuthentication(HttpTransportBindingElement http, HttpTransportSecurity transportSecurity)
		{
			transportSecurity.clientCredentialType = HttpClientCredentialTypeHelper.MapToClientCredentialType(http.AuthenticationScheme);
			transportSecurity.proxyCredentialType = HttpProxyCredentialTypeHelper.MapToProxyCredentialType(http.ProxyAuthenticationScheme);
			transportSecurity.Realm = http.Realm;
			transportSecurity.extendedProtectionPolicy = http.ExtendedProtectionPolicy;
		}

		// Token: 0x0600089E RID: 2206 RVA: 0x00022BDD File Offset: 0x00020DDD
		private void DisableAuthentication(HttpTransportBindingElement http)
		{
			http.AuthenticationScheme = AuthenticationSchemes.Anonymous;
			http.ProxyAuthenticationScheme = AuthenticationSchemes.Anonymous;
			http.Realm = "";
			http.ExtendedProtectionPolicy = this.extendedProtectionPolicy;
		}

		// Token: 0x0600089F RID: 2207 RVA: 0x00022C0C File Offset: 0x00020E0C
		private static bool IsDisabledAuthentication(HttpTransportBindingElement http)
		{
			return http.AuthenticationScheme == AuthenticationSchemes.Anonymous && http.ProxyAuthenticationScheme == AuthenticationSchemes.Anonymous && http.Realm == "";
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x00022C3A File Offset: 0x00020E3A
		internal void ConfigureTransportProtectionAndAuthentication(HttpsTransportBindingElement https)
		{
			this.ConfigureAuthentication(https);
			https.RequireClientCertificate = (this.clientCredentialType == HttpClientCredentialType.Certificate);
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x00022C52 File Offset: 0x00020E52
		internal static void ConfigureTransportProtectionAndAuthentication(HttpsTransportBindingElement https, HttpTransportSecurity transportSecurity)
		{
			HttpTransportSecurity.ConfigureAuthentication(https, transportSecurity);
			if (https.RequireClientCertificate)
			{
				transportSecurity.ClientCredentialType = HttpClientCredentialType.Certificate;
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00022C6A File Offset: 0x00020E6A
		internal void ConfigureTransportAuthentication(HttpTransportBindingElement http)
		{
			if (this.clientCredentialType == HttpClientCredentialType.Certificate)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("CertificateUnsupportedForHttpTransportCredentialOnly")));
			}
			this.ConfigureAuthentication(http);
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x00022C96 File Offset: 0x00020E96
		internal static bool IsConfiguredTransportAuthentication(HttpTransportBindingElement http, HttpTransportSecurity transportSecurity)
		{
			if (HttpClientCredentialTypeHelper.MapToClientCredentialType(http.AuthenticationScheme) == HttpClientCredentialType.Certificate)
			{
				return false;
			}
			HttpTransportSecurity.ConfigureAuthentication(http, transportSecurity);
			return true;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00022CB0 File Offset: 0x00020EB0
		internal void DisableTransportAuthentication(HttpTransportBindingElement http)
		{
			this.DisableAuthentication(http);
		}

		// Token: 0x060008A5 RID: 2213 RVA: 0x00022CB9 File Offset: 0x00020EB9
		internal static bool IsDisabledTransportAuthentication(HttpTransportBindingElement http)
		{
			return HttpTransportSecurity.IsDisabledAuthentication(http);
		}

		// Token: 0x060008A6 RID: 2214 RVA: 0x00022CC1 File Offset: 0x00020EC1
		internal bool InternalShouldSerialize()
		{
			return this.ShouldSerializeClientCredentialType() || this.ShouldSerializeProxyCredentialType() || this.ShouldSerializeRealm() || this.ShouldSerializeExtendedProtectionPolicy();
		}

		// Token: 0x060008A7 RID: 2215 RVA: 0x00022CE3 File Offset: 0x00020EE3
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeClientCredentialType()
		{
			return this.ClientCredentialType > HttpClientCredentialType.None;
		}

		// Token: 0x060008A8 RID: 2216 RVA: 0x00022CEE File Offset: 0x00020EEE
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeProxyCredentialType()
		{
			return this.proxyCredentialType > HttpProxyCredentialType.None;
		}

		// Token: 0x060008A9 RID: 2217 RVA: 0x00022CF9 File Offset: 0x00020EF9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeRealm()
		{
			return this.Realm != "";
		}

		// Token: 0x060008AA RID: 2218 RVA: 0x00022D0B File Offset: 0x00020F0B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool ShouldSerializeExtendedProtectionPolicy()
		{
			return !ChannelBindingUtility.AreEqual(this.ExtendedProtectionPolicy, ChannelBindingUtility.DefaultPolicy);
		}

		// Token: 0x04000B36 RID: 2870
		internal const HttpClientCredentialType DefaultClientCredentialType = HttpClientCredentialType.None;

		// Token: 0x04000B37 RID: 2871
		internal const HttpProxyCredentialType DefaultProxyCredentialType = HttpProxyCredentialType.None;

		// Token: 0x04000B38 RID: 2872
		internal const string DefaultRealm = "";

		// Token: 0x04000B39 RID: 2873
		private HttpClientCredentialType clientCredentialType;

		// Token: 0x04000B3A RID: 2874
		private HttpProxyCredentialType proxyCredentialType;

		// Token: 0x04000B3B RID: 2875
		private string realm;

		// Token: 0x04000B3C RID: 2876
		private ExtendedProtectionPolicy extendedProtectionPolicy;
	}
}
