using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.ServiceModel.Activation;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000870 RID: 2160
	internal class HttpsChannelListener<TChannel> : HttpChannelListener<TChannel> where TChannel : class, IChannel
	{
		// Token: 0x060051B9 RID: 20921 RVA: 0x0012C924 File Offset: 0x0012AB24
		public HttpsChannelListener(HttpsTransportBindingElement httpsBindingElement, BindingContext context) : base(httpsBindingElement, context)
		{
			this.requireClientCertificate = httpsBindingElement.RequireClientCertificate;
			this.shouldValidateClientCertificate = HttpsChannelListener<TChannel>.ShouldValidateClientCertificate(this.requireClientCertificate, context);
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				securityCredentialsManager = ServiceCredentials.CreateDefaultCredentials();
			}
			SecurityTokenManager tokenManager = securityCredentialsManager.CreateSecurityTokenManager();
			this.certificateAuthenticator = TransportSecurityHelpers.GetCertificateTokenAuthenticator(tokenManager, context.Binding.Scheme, TransportSecurityHelpers.GetListenUri(context.ListenUriBaseAddress, context.ListenUriRelativeAddress));
			ServiceCredentials serviceCredentials = securityCredentialsManager as ServiceCredentials;
			if (serviceCredentials != null && serviceCredentials.ClientCertificate.Authentication.CertificateValidationMode == X509CertificateValidationMode.Custom)
			{
				this.useCustomClientCertificateVerification = true;
			}
			else
			{
				this.useCustomClientCertificateVerification = false;
				X509SecurityTokenAuthenticator x509SecurityTokenAuthenticator = this.certificateAuthenticator as X509SecurityTokenAuthenticator;
				if (x509SecurityTokenAuthenticator != null)
				{
					this.certificateAuthenticator = new X509SecurityTokenAuthenticator(X509CertificateValidator.None, x509SecurityTokenAuthenticator.MapCertificateToWindowsAccount, base.ExtractGroupsForWindowsAccounts, false);
				}
			}
			if (this.RequireClientCertificate && base.AuthenticationScheme.IsNotSet(AuthenticationSchemes.Anonymous))
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelper(new InvalidOperationException(SR.GetString("HttpAuthSchemeAndClientCert", new object[]
				{
					base.AuthenticationScheme
				})), TraceEventType.Error);
			}
			this.channelBindingProvider = new ChannelBindingProviderHelper();
		}

		// Token: 0x17001439 RID: 5177
		// (get) Token: 0x060051BA RID: 20922 RVA: 0x0012CA48 File Offset: 0x0012AC48
		public bool RequireClientCertificate
		{
			get
			{
				return this.requireClientCertificate;
			}
		}

		// Token: 0x1700143A RID: 5178
		// (get) Token: 0x060051BB RID: 20923 RVA: 0x0012CA50 File Offset: 0x0012AC50
		public override string Scheme
		{
			get
			{
				return Uri.UriSchemeHttps;
			}
		}

		// Token: 0x1700143B RID: 5179
		// (get) Token: 0x060051BC RID: 20924 RVA: 0x0012CA57 File Offset: 0x0012AC57
		public override bool IsChannelBindingSupportEnabled
		{
			get
			{
				return this.channelBindingProvider.IsChannelBindingSupportEnabled;
			}
		}

		// Token: 0x1700143C RID: 5180
		// (get) Token: 0x060051BD RID: 20925 RVA: 0x0012CA64 File Offset: 0x0012AC64
		internal override UriPrefixTable<ITransportManagerRegistration> TransportManagerTable
		{
			get
			{
				return SharedHttpsTransportManager.StaticTransportManagerTable;
			}
		}

		// Token: 0x060051BE RID: 20926 RVA: 0x0012CA6B File Offset: 0x0012AC6B
		public override T GetProperty<T>()
		{
			if (typeof(T) == typeof(IChannelBindingProvider))
			{
				return (T)((object)this.channelBindingProvider);
			}
			return base.GetProperty<T>();
		}

		// Token: 0x060051BF RID: 20927 RVA: 0x0012CA9A File Offset: 0x0012AC9A
		internal override void ApplyHostedContext(string virtualPath, bool isMetadataListener)
		{
			base.ApplyHostedContext(virtualPath, isMetadataListener);
			this.useHostedClientCertificateMapping = AspNetEnvironment.Current.ValidateHttpsSettings(virtualPath, ref this.requireClientCertificate);
			if (this.requireClientCertificate)
			{
				this.shouldValidateClientCertificate = true;
			}
		}

		// Token: 0x060051C0 RID: 20928 RVA: 0x0012CACA File Offset: 0x0012ACCA
		internal override ITransportManagerRegistration CreateTransportManagerRegistration(Uri listenUri)
		{
			return new SharedHttpsTransportManager(listenUri, this);
		}

		// Token: 0x060051C1 RID: 20929 RVA: 0x0012CAD4 File Offset: 0x0012ACD4
		private SecurityMessageProperty CreateSecurityProperty(X509Certificate2 certificate, WindowsIdentity identity, string authType)
		{
			SecurityToken token;
			if (identity != null)
			{
				token = new X509WindowsSecurityToken(certificate, identity, authType, false);
			}
			else
			{
				token = new X509SecurityToken(certificate, false);
			}
			ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = this.certificateAuthenticator.ValidateToken(token);
			return new SecurityMessageProperty
			{
				TransportToken = new SecurityTokenSpecification(token, readOnlyCollection),
				ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection)
			};
		}

		// Token: 0x060051C2 RID: 20930 RVA: 0x0012CB28 File Offset: 0x0012AD28
		public override SecurityMessageProperty ProcessAuthentication(HttpChannelListener.IHttpAuthenticationContext authenticationContext)
		{
			if (this.shouldValidateClientCertificate)
			{
				X509Certificate2 x509Certificate = null;
				SecurityMessageProperty result;
				try
				{
					bool flag;
					x509Certificate = authenticationContext.GetClientCertificate(out flag);
					if (x509Certificate != null)
					{
						bool flag2 = this.useCustomClientCertificateVerification;
						WindowsIdentity windowsIdentity = null;
						string authType = base.GetAuthType(authenticationContext);
						if (this.useHostedClientCertificateMapping)
						{
							windowsIdentity = authenticationContext.LogonUserIdentity;
							if (windowsIdentity == null || !windowsIdentity.IsAuthenticated)
							{
								windowsIdentity = WindowsIdentity.GetAnonymous();
							}
							else
							{
								windowsIdentity = SecurityUtils.CloneWindowsIdentityIfNecessary(windowsIdentity, "SSL/PCT");
								authType = "SSL/PCT";
							}
						}
						result = this.CreateSecurityProperty(x509Certificate, windowsIdentity, authType);
					}
					else
					{
						if (base.AuthenticationScheme == AuthenticationSchemes.Anonymous)
						{
							return new SecurityMessageProperty();
						}
						return base.ProcessAuthentication(authenticationContext);
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					if (AuditLevel.Failure == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Failure))
					{
						base.WriteAuditEvent(AuditLevel.Failure, (x509Certificate != null) ? SecurityUtils.GetCertificateId(x509Certificate) : string.Empty, exception);
					}
					throw;
				}
				if (AuditLevel.Success == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Success))
				{
					base.WriteAuditEvent(AuditLevel.Success, (x509Certificate != null) ? SecurityUtils.GetCertificateId(x509Certificate) : string.Empty, null);
				}
				return result;
			}
			if (base.AuthenticationScheme == AuthenticationSchemes.Anonymous)
			{
				return new SecurityMessageProperty();
			}
			return base.ProcessAuthentication(authenticationContext);
		}

		// Token: 0x060051C3 RID: 20931 RVA: 0x0012CC58 File Offset: 0x0012AE58
		public override SecurityMessageProperty ProcessAuthentication(HttpListenerContext listenerContext)
		{
			if (this.shouldValidateClientCertificate)
			{
				X509Certificate2 x509Certificate = null;
				SecurityMessageProperty result;
				try
				{
					X509Certificate clientCertificate = listenerContext.Request.GetClientCertificate();
					if (clientCertificate != null)
					{
						bool flag = this.useCustomClientCertificateVerification;
						x509Certificate = new X509Certificate2(clientCertificate);
						result = this.CreateSecurityProperty(x509Certificate, null, string.Empty);
					}
					else
					{
						if (base.AuthenticationScheme == AuthenticationSchemes.Anonymous)
						{
							return new SecurityMessageProperty();
						}
						return base.ProcessAuthentication(listenerContext);
					}
				}
				catch (Exception exception)
				{
					if (Fx.IsFatal(exception))
					{
						throw;
					}
					if (AuditLevel.Failure == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Failure))
					{
						base.WriteAuditEvent(AuditLevel.Failure, (x509Certificate != null) ? SecurityUtils.GetCertificateId(x509Certificate) : string.Empty, exception);
					}
					throw;
				}
				if (AuditLevel.Success == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Success))
				{
					base.WriteAuditEvent(AuditLevel.Success, (x509Certificate != null) ? SecurityUtils.GetCertificateId(x509Certificate) : string.Empty, null);
				}
				return result;
			}
			if (base.AuthenticationScheme == AuthenticationSchemes.Anonymous)
			{
				return new SecurityMessageProperty();
			}
			return base.ProcessAuthentication(listenerContext);
		}

		// Token: 0x060051C4 RID: 20932 RVA: 0x0012CD54 File Offset: 0x0012AF54
		public override HttpStatusCode ValidateAuthentication(HttpChannelListener.IHttpAuthenticationContext authenticationContext)
		{
			HttpStatusCode httpStatusCode = base.ValidateAuthentication(authenticationContext);
			if (httpStatusCode == HttpStatusCode.OK && this.shouldValidateClientCertificate)
			{
				bool flag;
				X509Certificate2 clientCertificate = authenticationContext.GetClientCertificate(out flag);
				if (clientCertificate == null)
				{
					if (this.RequireClientCertificate)
					{
						if (DiagnosticUtility.ShouldTraceError)
						{
							TraceUtility.TraceEvent(TraceEventType.Error, 262160, SR.GetString("TraceCodeHttpsClientCertificateNotPresent"), authenticationContext.CreateTraceRecord(), this, null);
						}
						httpStatusCode = HttpStatusCode.Forbidden;
					}
				}
				else if (!flag && !this.useCustomClientCertificateVerification)
				{
					if (DiagnosticUtility.ShouldTraceError)
					{
						TraceUtility.TraceEvent(TraceEventType.Error, 262159, SR.GetString("TraceCodeHttpsClientCertificateInvalid"), authenticationContext.CreateTraceRecord(), this, null);
					}
					httpStatusCode = HttpStatusCode.Forbidden;
				}
				if (httpStatusCode != HttpStatusCode.OK && AuditLevel.Failure == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Failure))
				{
					string @string = SR.GetString("HttpAuthenticationFailed", new object[]
					{
						base.AuthenticationScheme,
						httpStatusCode
					});
					Exception exception = DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(@string));
					base.WriteAuditEvent(AuditLevel.Failure, (clientCertificate != null) ? SecurityUtils.GetCertificateId(clientCertificate) : string.Empty, exception);
				}
			}
			return httpStatusCode;
		}

		// Token: 0x060051C5 RID: 20933 RVA: 0x0012CE60 File Offset: 0x0012B060
		public override HttpStatusCode ValidateAuthentication(HttpListenerContext listenerContext)
		{
			HttpStatusCode httpStatusCode = base.ValidateAuthentication(listenerContext);
			if (httpStatusCode == HttpStatusCode.OK && this.shouldValidateClientCertificate)
			{
				HttpListenerRequest request = listenerContext.Request;
				X509Certificate2 clientCertificate = request.GetClientCertificate();
				if (clientCertificate == null)
				{
					if (this.RequireClientCertificate)
					{
						if (DiagnosticUtility.ShouldTraceWarning)
						{
							TraceUtility.TraceEvent(TraceEventType.Warning, 262160, SR.GetString("TraceCodeHttpsClientCertificateNotPresent"), new HttpListenerRequestTraceRecord(listenerContext.Request), this, null);
						}
						httpStatusCode = HttpStatusCode.Forbidden;
					}
				}
				else if (request.ClientCertificateError != 0 && !this.useCustomClientCertificateVerification)
				{
					if (DiagnosticUtility.ShouldTraceWarning)
					{
						TraceUtility.TraceEvent(TraceEventType.Warning, 262159, SR.GetString("TraceCodeHttpsClientCertificateInvalid1", new object[]
						{
							"0x" + (request.ClientCertificateError & 65535).ToString("X", CultureInfo.InvariantCulture)
						}), new HttpListenerRequestTraceRecord(listenerContext.Request), this, null);
					}
					httpStatusCode = HttpStatusCode.Forbidden;
				}
				if (httpStatusCode != HttpStatusCode.OK && AuditLevel.Failure == (base.AuditBehavior.MessageAuthenticationAuditLevel & AuditLevel.Failure))
				{
					string @string = SR.GetString("HttpAuthenticationFailed", new object[]
					{
						base.AuthenticationScheme,
						httpStatusCode
					});
					Exception exception = DiagnosticUtility.ExceptionUtility.ThrowHelperError(new MessageSecurityException(@string));
					base.WriteAuditEvent(AuditLevel.Failure, (clientCertificate != null) ? SecurityUtils.GetCertificateId(clientCertificate) : string.Empty, exception);
				}
			}
			return httpStatusCode;
		}

		// Token: 0x060051C6 RID: 20934 RVA: 0x0012CFB6 File Offset: 0x0012B1B6
		private static bool ShouldValidateClientCertificate(bool requireClientCertificateValidation, BindingContext context)
		{
			return requireClientCertificateValidation || EndpointSettings.GetValue<bool>(context, "wcf:HttpTransport:ValidateOptionalClientCertificates", false);
		}

		// Token: 0x0400321F RID: 12831
		private readonly bool useCustomClientCertificateVerification;

		// Token: 0x04003220 RID: 12832
		private bool shouldValidateClientCertificate;

		// Token: 0x04003221 RID: 12833
		private bool useHostedClientCertificateMapping;

		// Token: 0x04003222 RID: 12834
		private bool requireClientCertificate;

		// Token: 0x04003223 RID: 12835
		private SecurityTokenAuthenticator certificateAuthenticator;

		// Token: 0x04003224 RID: 12836
		private const HttpStatusCode CertificateErrorStatusCode = HttpStatusCode.Forbidden;

		// Token: 0x04003225 RID: 12837
		private IChannelBindingProvider channelBindingProvider;
	}
}
