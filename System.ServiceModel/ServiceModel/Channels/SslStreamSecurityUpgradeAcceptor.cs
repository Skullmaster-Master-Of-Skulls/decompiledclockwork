using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IdentityModel.Policy;
using System.IdentityModel.Tokens;
using System.IO;
using System.Net.Security;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Diagnostics;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200082B RID: 2091
	internal class SslStreamSecurityUpgradeAcceptor : StreamSecurityUpgradeAcceptorBase
	{
		// Token: 0x06004E27 RID: 20007 RVA: 0x0011D7E1 File Offset: 0x0011B9E1
		public SslStreamSecurityUpgradeAcceptor(SslStreamSecurityUpgradeProvider parent) : base("application/ssl-tls")
		{
			this.parent = parent;
			this.clientSecurity = new SecurityMessageProperty();
		}

		// Token: 0x1700138C RID: 5004
		// (get) Token: 0x06004E28 RID: 20008 RVA: 0x0011D800 File Offset: 0x0011BA00
		internal ChannelBinding ChannelBinding
		{
			get
			{
				return this.channelBindingToken;
			}
		}

		// Token: 0x1700138D RID: 5005
		// (get) Token: 0x06004E29 RID: 20009 RVA: 0x0011D808 File Offset: 0x0011BA08
		internal bool IsChannelBindingSupportEnabled
		{
			get
			{
				return ((IChannelBindingProvider)this.parent).IsChannelBindingSupportEnabled;
			}
		}

		// Token: 0x06004E2A RID: 20010 RVA: 0x0011D818 File Offset: 0x0011BA18
		protected override Stream OnAcceptUpgrade(Stream stream, out SecurityMessageProperty remoteSecurity)
		{
			if (TD.SslOnAcceptUpgradeIsEnabled())
			{
				TD.SslOnAcceptUpgrade(base.EventTraceActivity);
			}
			SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(this.ValidateRemoteCertificate));
			try
			{
				sslStream.AuthenticateAsServer(this.parent.ServerCertificate, this.parent.RequireClientCertificate, this.parent.SslProtocols, false);
			}
			catch (AuthenticationException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(ex.Message, ex));
			}
			catch (IOException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NegotiationFailedIO", new object[]
				{
					ex2.Message
				}), ex2));
			}
			if (SecurityUtils.ShouldValidateSslCipherStrength())
			{
				SecurityUtils.ValidateSslCipherStrength(sslStream.CipherStrength);
			}
			remoteSecurity = this.clientSecurity;
			if (this.IsChannelBindingSupportEnabled)
			{
				this.channelBindingToken = ChannelBindingUtility.GetToken(sslStream);
			}
			return sslStream;
		}

		// Token: 0x06004E2B RID: 20011 RVA: 0x0011D904 File Offset: 0x0011BB04
		protected override IAsyncResult OnBeginAcceptUpgrade(Stream stream, AsyncCallback callback, object state)
		{
			SslStreamSecurityUpgradeAcceptor.AcceptUpgradeAsyncResult acceptUpgradeAsyncResult = new SslStreamSecurityUpgradeAcceptor.AcceptUpgradeAsyncResult(this, callback, state);
			acceptUpgradeAsyncResult.Begin(stream);
			return acceptUpgradeAsyncResult;
		}

		// Token: 0x06004E2C RID: 20012 RVA: 0x0011D922 File Offset: 0x0011BB22
		protected override Stream OnEndAcceptUpgrade(IAsyncResult result, out SecurityMessageProperty remoteSecurity)
		{
			return SslStreamSecurityUpgradeAcceptor.AcceptUpgradeAsyncResult.End(result, out remoteSecurity, out this.channelBindingToken);
		}

		// Token: 0x06004E2D RID: 20013 RVA: 0x0011D934 File Offset: 0x0011BB34
		private bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			if (this.parent.RequireClientCertificate)
			{
				if (certificate == null)
				{
					if (DiagnosticUtility.ShouldTraceError)
					{
						TraceUtility.TraceEvent(TraceEventType.Error, 262189, SR.GetString("TraceCodeSslClientCertMissing"), this);
					}
					return false;
				}
				X509Certificate2 certificate2 = new X509Certificate2(certificate);
				this.clientCertificate = certificate2;
				try
				{
					SecurityToken token = new X509SecurityToken(certificate2, false);
					ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = this.parent.ClientCertificateAuthenticator.ValidateToken(token);
					this.clientSecurity = new SecurityMessageProperty();
					this.clientSecurity.TransportToken = new SecurityTokenSpecification(token, readOnlyCollection);
					this.clientSecurity.ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection);
				}
				catch (SecurityTokenException exception)
				{
					DiagnosticUtility.TraceHandledException(exception, TraceEventType.Information);
					return false;
				}
				return true;
			}
			return true;
		}

		// Token: 0x06004E2E RID: 20014 RVA: 0x0011D9EC File Offset: 0x0011BBEC
		public override SecurityMessageProperty GetRemoteSecurity()
		{
			if (this.clientSecurity.TransportToken != null)
			{
				return this.clientSecurity;
			}
			if (this.clientCertificate != null)
			{
				SecurityToken token = new X509SecurityToken(this.clientCertificate);
				ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = SecurityUtils.NonValidatingX509Authenticator.ValidateToken(token);
				this.clientSecurity = new SecurityMessageProperty();
				this.clientSecurity.TransportToken = new SecurityTokenSpecification(token, readOnlyCollection);
				this.clientSecurity.ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection);
				return this.clientSecurity;
			}
			return base.GetRemoteSecurity();
		}

		// Token: 0x040030D0 RID: 12496
		private SslStreamSecurityUpgradeProvider parent;

		// Token: 0x040030D1 RID: 12497
		private SecurityMessageProperty clientSecurity;

		// Token: 0x040030D2 RID: 12498
		private X509Certificate2 clientCertificate;

		// Token: 0x040030D3 RID: 12499
		private ChannelBinding channelBindingToken;

		// Token: 0x02000D29 RID: 3369
		private class AcceptUpgradeAsyncResult : StreamSecurityUpgradeAcceptorAsyncResult
		{
			// Token: 0x06007BE5 RID: 31717 RVA: 0x001CEE28 File Offset: 0x001CD028
			public AcceptUpgradeAsyncResult(SslStreamSecurityUpgradeAcceptor acceptor, AsyncCallback callback, object state) : base(callback, state)
			{
				this.acceptor = acceptor;
			}

			// Token: 0x06007BE6 RID: 31718 RVA: 0x001CEE3C File Offset: 0x001CD03C
			protected override IAsyncResult OnBegin(Stream stream, AsyncCallback callback)
			{
				if (TD.SslOnAcceptUpgradeIsEnabled())
				{
					TD.SslOnAcceptUpgrade(this.acceptor.EventTraceActivity);
				}
				this.sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(this.acceptor.ValidateRemoteCertificate));
				return this.sslStream.BeginAuthenticateAsServer(this.acceptor.parent.ServerCertificate, this.acceptor.parent.RequireClientCertificate, this.acceptor.parent.SslProtocols, false, callback, this);
			}

			// Token: 0x06007BE7 RID: 31719 RVA: 0x001CEEBC File Offset: 0x001CD0BC
			protected override Stream OnCompleteAuthenticateAsServer(IAsyncResult result)
			{
				this.sslStream.EndAuthenticateAsServer(result);
				if (SecurityUtils.ShouldValidateSslCipherStrength())
				{
					SecurityUtils.ValidateSslCipherStrength(this.sslStream.CipherStrength);
				}
				if (this.acceptor.IsChannelBindingSupportEnabled)
				{
					this.channelBindingToken = ChannelBindingUtility.GetToken(this.sslStream);
				}
				return this.sslStream;
			}

			// Token: 0x06007BE8 RID: 31720 RVA: 0x001CEF10 File Offset: 0x001CD110
			protected override SecurityMessageProperty ValidateCreateSecurity()
			{
				return this.acceptor.clientSecurity;
			}

			// Token: 0x06007BE9 RID: 31721 RVA: 0x001CEF20 File Offset: 0x001CD120
			public static Stream End(IAsyncResult result, out SecurityMessageProperty remoteSecurity, out ChannelBinding channelBinding)
			{
				Stream result2 = StreamSecurityUpgradeAcceptorAsyncResult.End(result, out remoteSecurity);
				channelBinding = ((SslStreamSecurityUpgradeAcceptor.AcceptUpgradeAsyncResult)result).channelBindingToken;
				return result2;
			}

			// Token: 0x04004721 RID: 18209
			private SslStreamSecurityUpgradeAcceptor acceptor;

			// Token: 0x04004722 RID: 18210
			private SslStream sslStream;

			// Token: 0x04004723 RID: 18211
			private ChannelBinding channelBindingToken;
		}
	}
}
