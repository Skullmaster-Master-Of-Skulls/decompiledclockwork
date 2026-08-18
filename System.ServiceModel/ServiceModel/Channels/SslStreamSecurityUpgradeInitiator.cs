using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Net.Security;
using System.Runtime;
using System.Security.Authentication;
using System.Security.Authentication.ExtendedProtection;
using System.Security.Cryptography.X509Certificates;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200082C RID: 2092
	internal class SslStreamSecurityUpgradeInitiator : StreamSecurityUpgradeInitiatorBase
	{
		// Token: 0x06004E2F RID: 20015 RVA: 0x0011DA68 File Offset: 0x0011BC68
		public SslStreamSecurityUpgradeInitiator(SslStreamSecurityUpgradeProvider parent, EndpointAddress remoteAddress, Uri via) : base("application/ssl-tls", remoteAddress, via)
		{
			this.parent = parent;
			InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement = new InitiatorServiceModelSecurityTokenRequirement();
			initiatorServiceModelSecurityTokenRequirement.TokenType = SecurityTokenTypes.X509Certificate;
			initiatorServiceModelSecurityTokenRequirement.RequireCryptographicToken = true;
			initiatorServiceModelSecurityTokenRequirement.KeyUsage = SecurityKeyUsage.Exchange;
			initiatorServiceModelSecurityTokenRequirement.TargetAddress = remoteAddress;
			initiatorServiceModelSecurityTokenRequirement.Via = via;
			initiatorServiceModelSecurityTokenRequirement.TransportScheme = this.parent.Scheme;
			initiatorServiceModelSecurityTokenRequirement.PreferSslCertificateAuthenticator = true;
			SecurityTokenResolver securityTokenResolver;
			this.serverCertificateAuthenticator = parent.ClientSecurityTokenManager.CreateSecurityTokenAuthenticator(initiatorServiceModelSecurityTokenRequirement, out securityTokenResolver);
			if (parent.RequireClientCertificate)
			{
				InitiatorServiceModelSecurityTokenRequirement initiatorServiceModelSecurityTokenRequirement2 = new InitiatorServiceModelSecurityTokenRequirement();
				initiatorServiceModelSecurityTokenRequirement2.TokenType = SecurityTokenTypes.X509Certificate;
				initiatorServiceModelSecurityTokenRequirement2.RequireCryptographicToken = true;
				initiatorServiceModelSecurityTokenRequirement2.KeyUsage = SecurityKeyUsage.Signature;
				initiatorServiceModelSecurityTokenRequirement2.TargetAddress = remoteAddress;
				initiatorServiceModelSecurityTokenRequirement2.Via = via;
				initiatorServiceModelSecurityTokenRequirement2.TransportScheme = this.parent.Scheme;
				this.clientCertificateProvider = parent.ClientSecurityTokenManager.CreateSecurityTokenProvider(initiatorServiceModelSecurityTokenRequirement2);
				if (this.clientCertificateProvider == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("ClientCredentialsUnableToCreateLocalTokenProvider", new object[]
					{
						initiatorServiceModelSecurityTokenRequirement2
					})));
				}
			}
		}

		// Token: 0x1700138E RID: 5006
		// (get) Token: 0x06004E30 RID: 20016 RVA: 0x0011DB66 File Offset: 0x0011BD66
		private static LocalCertificateSelectionCallback ClientCertificateSelectionCallback
		{
			get
			{
				if (SslStreamSecurityUpgradeInitiator.clientCertificateSelectionCallback == null)
				{
					SslStreamSecurityUpgradeInitiator.clientCertificateSelectionCallback = new LocalCertificateSelectionCallback(SslStreamSecurityUpgradeInitiator.SelectClientCertificate);
				}
				return SslStreamSecurityUpgradeInitiator.clientCertificateSelectionCallback;
			}
		}

		// Token: 0x1700138F RID: 5007
		// (get) Token: 0x06004E31 RID: 20017 RVA: 0x0011DB85 File Offset: 0x0011BD85
		internal ChannelBinding ChannelBinding
		{
			get
			{
				return this.channelBindingToken;
			}
		}

		// Token: 0x17001390 RID: 5008
		// (get) Token: 0x06004E32 RID: 20018 RVA: 0x0011DB8D File Offset: 0x0011BD8D
		internal bool IsChannelBindingSupportEnabled
		{
			get
			{
				return ((IChannelBindingProvider)this.parent).IsChannelBindingSupportEnabled;
			}
		}

		// Token: 0x06004E33 RID: 20019 RVA: 0x0011DB9A File Offset: 0x0011BD9A
		private IAsyncResult BaseBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.BeginOpen(timeout, callback, state);
		}

		// Token: 0x06004E34 RID: 20020 RVA: 0x0011DBA5 File Offset: 0x0011BDA5
		private void BaseEndOpen(IAsyncResult result)
		{
			base.EndOpen(result);
		}

		// Token: 0x06004E35 RID: 20021 RVA: 0x0011DBAE File Offset: 0x0011BDAE
		internal override IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SslStreamSecurityUpgradeInitiator.OpenAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06004E36 RID: 20022 RVA: 0x0011DBB9 File Offset: 0x0011BDB9
		internal override void EndOpen(IAsyncResult result)
		{
			SslStreamSecurityUpgradeInitiator.OpenAsyncResult.End(result);
		}

		// Token: 0x06004E37 RID: 20023 RVA: 0x0011DBC4 File Offset: 0x0011BDC4
		internal override void Open(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.Open(timeoutHelper.RemainingTime());
			if (this.clientCertificateProvider != null)
			{
				SecurityUtils.OpenTokenProviderIfRequired(this.clientCertificateProvider, timeoutHelper.RemainingTime());
				this.clientToken = (X509SecurityToken)this.clientCertificateProvider.GetToken(timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06004E38 RID: 20024 RVA: 0x0011DC1D File Offset: 0x0011BE1D
		private IAsyncResult BaseBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return base.BeginClose(timeout, callback, state);
		}

		// Token: 0x06004E39 RID: 20025 RVA: 0x0011DC28 File Offset: 0x0011BE28
		private void BaseEndClose(IAsyncResult result)
		{
			base.EndClose(result);
		}

		// Token: 0x06004E3A RID: 20026 RVA: 0x0011DC31 File Offset: 0x0011BE31
		internal override IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new SslStreamSecurityUpgradeInitiator.CloseAsyncResult(this, timeout, callback, state);
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x0011DC3C File Offset: 0x0011BE3C
		internal override void EndClose(IAsyncResult result)
		{
			SslStreamSecurityUpgradeInitiator.CloseAsyncResult.End(result);
		}

		// Token: 0x06004E3C RID: 20028 RVA: 0x0011DC44 File Offset: 0x0011BE44
		internal override void Close(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			base.Close(timeoutHelper.RemainingTime());
			if (this.clientCertificateProvider != null)
			{
				SecurityUtils.CloseTokenProviderIfRequired(this.clientCertificateProvider, timeoutHelper.RemainingTime());
			}
		}

		// Token: 0x06004E3D RID: 20029 RVA: 0x0011DC80 File Offset: 0x0011BE80
		protected override IAsyncResult OnBeginInitiateUpgrade(Stream stream, AsyncCallback callback, object state)
		{
			if (TD.SslOnInitiateUpgradeIsEnabled())
			{
				TD.SslOnInitiateUpgrade();
			}
			SslStreamSecurityUpgradeInitiator.InitiateUpgradeAsyncResult initiateUpgradeAsyncResult = new SslStreamSecurityUpgradeInitiator.InitiateUpgradeAsyncResult(this, callback, state);
			initiateUpgradeAsyncResult.Begin(stream);
			return initiateUpgradeAsyncResult;
		}

		// Token: 0x06004E3E RID: 20030 RVA: 0x0011DCAA File Offset: 0x0011BEAA
		protected override Stream OnEndInitiateUpgrade(IAsyncResult result, out SecurityMessageProperty remoteSecurity)
		{
			return SslStreamSecurityUpgradeInitiator.InitiateUpgradeAsyncResult.End(result, out remoteSecurity, out this.channelBindingToken);
		}

		// Token: 0x06004E3F RID: 20031 RVA: 0x0011DCBC File Offset: 0x0011BEBC
		protected override Stream OnInitiateUpgrade(Stream stream, out SecurityMessageProperty remoteSecurity)
		{
			if (TD.SslOnInitiateUpgradeIsEnabled())
			{
				TD.SslOnInitiateUpgrade();
			}
			X509CertificateCollection x509CertificateCollection = null;
			LocalCertificateSelectionCallback userCertificateSelectionCallback = null;
			if (this.clientToken != null)
			{
				x509CertificateCollection = new X509CertificateCollection();
				x509CertificateCollection.Add(this.clientToken.Certificate);
				userCertificateSelectionCallback = SslStreamSecurityUpgradeInitiator.ClientCertificateSelectionCallback;
			}
			SslStream sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(this.ValidateRemoteCertificate), userCertificateSelectionCallback);
			try
			{
				sslStream.AuthenticateAsClient(string.Empty, x509CertificateCollection, this.parent.SslProtocols, false);
			}
			catch (SecurityTokenValidationException ex)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(ex.Message, ex));
			}
			catch (AuthenticationException ex2)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(ex2.Message, ex2));
			}
			catch (IOException ex3)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("NegotiationFailedIO", new object[]
				{
					ex3.Message
				}), ex3));
			}
			if (SecurityUtils.ShouldValidateSslCipherStrength())
			{
				SecurityUtils.ValidateSslCipherStrength(sslStream.CipherStrength);
			}
			remoteSecurity = this.serverSecurity;
			if (this.IsChannelBindingSupportEnabled)
			{
				this.channelBindingToken = ChannelBindingUtility.GetToken(sslStream);
			}
			return sslStream;
		}

		// Token: 0x06004E40 RID: 20032 RVA: 0x0011DDE8 File Offset: 0x0011BFE8
		private static X509Certificate SelectClientCertificate(object sender, string targetHost, X509CertificateCollection localCertificates, X509Certificate remoteCertificate, string[] acceptableIssuers)
		{
			return localCertificates[0];
		}

		// Token: 0x06004E41 RID: 20033 RVA: 0x0011DDF4 File Offset: 0x0011BFF4
		private bool ValidateRemoteCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
		{
			X509Certificate2 certificate2 = new X509Certificate2(certificate);
			SecurityToken token = new X509SecurityToken(certificate2, false);
			ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = this.serverCertificateAuthenticator.ValidateToken(token);
			this.serverSecurity = new SecurityMessageProperty();
			this.serverSecurity.TransportToken = new SecurityTokenSpecification(token, readOnlyCollection);
			this.serverSecurity.ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection);
			AuthorizationContext authorizationContext = this.serverSecurity.ServiceSecurityContext.AuthorizationContext;
			this.parent.IdentityVerifier.EnsureOutgoingIdentity(base.RemoteAddress, base.Via, authorizationContext);
			return true;
		}

		// Token: 0x040030D4 RID: 12500
		private SslStreamSecurityUpgradeProvider parent;

		// Token: 0x040030D5 RID: 12501
		private SecurityMessageProperty serverSecurity;

		// Token: 0x040030D6 RID: 12502
		private SecurityTokenProvider clientCertificateProvider;

		// Token: 0x040030D7 RID: 12503
		private X509SecurityToken clientToken;

		// Token: 0x040030D8 RID: 12504
		private SecurityTokenAuthenticator serverCertificateAuthenticator;

		// Token: 0x040030D9 RID: 12505
		private ChannelBinding channelBindingToken;

		// Token: 0x040030DA RID: 12506
		private static LocalCertificateSelectionCallback clientCertificateSelectionCallback;

		// Token: 0x02000D2A RID: 3370
		private class InitiateUpgradeAsyncResult : StreamSecurityUpgradeInitiatorAsyncResult
		{
			// Token: 0x06007BEA RID: 31722 RVA: 0x001CEF44 File Offset: 0x001CD144
			public InitiateUpgradeAsyncResult(SslStreamSecurityUpgradeInitiator initiator, AsyncCallback callback, object state) : base(callback, state)
			{
				this.initiator = initiator;
				if (initiator.clientToken != null)
				{
					this.clientCertificates = new X509CertificateCollection();
					this.clientCertificates.Add(initiator.clientToken.Certificate);
					this.selectionCallback = SslStreamSecurityUpgradeInitiator.ClientCertificateSelectionCallback;
				}
			}

			// Token: 0x06007BEB RID: 31723 RVA: 0x001CEF98 File Offset: 0x001CD198
			protected override IAsyncResult OnBeginAuthenticateAsClient(Stream stream, AsyncCallback callback)
			{
				this.sslStream = new SslStream(stream, false, new RemoteCertificateValidationCallback(this.initiator.ValidateRemoteCertificate), this.selectionCallback);
				IAsyncResult result;
				try
				{
					result = this.sslStream.BeginAuthenticateAsClient(string.Empty, this.clientCertificates, this.initiator.parent.SslProtocols, false, callback, this);
				}
				catch (SecurityTokenValidationException ex)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(ex.Message, ex));
				}
				return result;
			}

			// Token: 0x06007BEC RID: 31724 RVA: 0x001CF020 File Offset: 0x001CD220
			protected override Stream OnCompleteAuthenticateAsClient(IAsyncResult result)
			{
				try
				{
					this.sslStream.EndAuthenticateAsClient(result);
				}
				catch (SecurityTokenValidationException ex)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(ex.Message, ex));
				}
				if (SecurityUtils.ShouldValidateSslCipherStrength())
				{
					SecurityUtils.ValidateSslCipherStrength(this.sslStream.CipherStrength);
				}
				if (this.initiator.IsChannelBindingSupportEnabled)
				{
					this.channelBindingToken = ChannelBindingUtility.GetToken(this.sslStream);
				}
				return this.sslStream;
			}

			// Token: 0x06007BED RID: 31725 RVA: 0x001CF0A0 File Offset: 0x001CD2A0
			protected override SecurityMessageProperty ValidateCreateSecurity()
			{
				return this.initiator.serverSecurity;
			}

			// Token: 0x06007BEE RID: 31726 RVA: 0x001CF0B0 File Offset: 0x001CD2B0
			public static Stream End(IAsyncResult result, out SecurityMessageProperty remoteSecurity, out ChannelBinding channelBinding)
			{
				Stream result2 = StreamSecurityUpgradeInitiatorAsyncResult.End(result, out remoteSecurity);
				channelBinding = ((SslStreamSecurityUpgradeInitiator.InitiateUpgradeAsyncResult)result).channelBindingToken;
				return result2;
			}

			// Token: 0x04004724 RID: 18212
			private X509CertificateCollection clientCertificates;

			// Token: 0x04004725 RID: 18213
			private SslStreamSecurityUpgradeInitiator initiator;

			// Token: 0x04004726 RID: 18214
			private LocalCertificateSelectionCallback selectionCallback;

			// Token: 0x04004727 RID: 18215
			private SslStream sslStream;

			// Token: 0x04004728 RID: 18216
			private ChannelBinding channelBindingToken;
		}

		// Token: 0x02000D2B RID: 3371
		private class OpenAsyncResult : AsyncResult
		{
			// Token: 0x06007BEF RID: 31727 RVA: 0x001CF0D4 File Offset: 0x001CD2D4
			public OpenAsyncResult(SslStreamSecurityUpgradeInitiator parent, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.onBaseOpen = Fx.ThunkCallback(new AsyncCallback(this.OnBaseOpen));
				if (parent.clientCertificateProvider != null)
				{
					this.onOpenTokenProvider = Fx.ThunkCallback(new AsyncCallback(this.OnOpenTokenProvider));
					this.onGetClientToken = Fx.ThunkCallback(new AsyncCallback(this.OnGetClientToken));
				}
				IAsyncResult asyncResult = parent.BaseBeginOpen(timeoutHelper.RemainingTime(), this.onBaseOpen, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				if (this.HandleBaseOpenComplete(asyncResult))
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007BF0 RID: 31728 RVA: 0x001CF174 File Offset: 0x001CD374
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<SslStreamSecurityUpgradeInitiator.OpenAsyncResult>(result);
			}

			// Token: 0x06007BF1 RID: 31729 RVA: 0x001CF180 File Offset: 0x001CD380
			private bool HandleBaseOpenComplete(IAsyncResult result)
			{
				this.parent.BaseEndOpen(result);
				if (this.parent.clientCertificateProvider == null)
				{
					return true;
				}
				IAsyncResult asyncResult = SecurityUtils.BeginOpenTokenProviderIfRequired(this.parent.clientCertificateProvider, this.timeoutHelper.RemainingTime(), this.onOpenTokenProvider, this);
				return asyncResult.CompletedSynchronously && this.HandleOpenTokenProviderComplete(asyncResult);
			}

			// Token: 0x06007BF2 RID: 31730 RVA: 0x001CF1DC File Offset: 0x001CD3DC
			private bool HandleOpenTokenProviderComplete(IAsyncResult result)
			{
				SecurityUtils.EndOpenTokenProviderIfRequired(result);
				IAsyncResult asyncResult = this.parent.clientCertificateProvider.BeginGetToken(this.timeoutHelper.RemainingTime(), this.onGetClientToken, this);
				return asyncResult.CompletedSynchronously && this.HandleGetTokenComplete(asyncResult);
			}

			// Token: 0x06007BF3 RID: 31731 RVA: 0x001CF223 File Offset: 0x001CD423
			private bool HandleGetTokenComplete(IAsyncResult result)
			{
				this.parent.clientToken = (X509SecurityToken)this.parent.clientCertificateProvider.EndGetToken(result);
				return true;
			}

			// Token: 0x06007BF4 RID: 31732 RVA: 0x001CF248 File Offset: 0x001CD448
			private void OnBaseOpen(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = this.HandleBaseOpenComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007BF5 RID: 31733 RVA: 0x001CF298 File Offset: 0x001CD498
			private void OnOpenTokenProvider(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = this.HandleOpenTokenProviderComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007BF6 RID: 31734 RVA: 0x001CF2E8 File Offset: 0x001CD4E8
			private void OnGetClientToken(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = this.HandleGetTokenComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x04004729 RID: 18217
			private SslStreamSecurityUpgradeInitiator parent;

			// Token: 0x0400472A RID: 18218
			private TimeoutHelper timeoutHelper;

			// Token: 0x0400472B RID: 18219
			private AsyncCallback onBaseOpen;

			// Token: 0x0400472C RID: 18220
			private AsyncCallback onOpenTokenProvider;

			// Token: 0x0400472D RID: 18221
			private AsyncCallback onGetClientToken;
		}

		// Token: 0x02000D2C RID: 3372
		private class CloseAsyncResult : AsyncResult
		{
			// Token: 0x06007BF7 RID: 31735 RVA: 0x001CF338 File Offset: 0x001CD538
			public CloseAsyncResult(SslStreamSecurityUpgradeInitiator parent, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
			{
				this.parent = parent;
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				this.onBaseClose = Fx.ThunkCallback(new AsyncCallback(this.OnBaseClose));
				if (parent.clientCertificateProvider != null)
				{
					this.onCloseTokenProvider = Fx.ThunkCallback(new AsyncCallback(this.OnCloseTokenProvider));
				}
				IAsyncResult asyncResult = parent.BaseBeginClose(timeoutHelper.RemainingTime(), this.onBaseClose, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return;
				}
				if (this.HandleBaseCloseComplete(asyncResult))
				{
					base.Complete(true);
				}
			}

			// Token: 0x06007BF8 RID: 31736 RVA: 0x001CF3C1 File Offset: 0x001CD5C1
			public static void End(IAsyncResult result)
			{
				AsyncResult.End<SslStreamSecurityUpgradeInitiator.CloseAsyncResult>(result);
			}

			// Token: 0x06007BF9 RID: 31737 RVA: 0x001CF3CC File Offset: 0x001CD5CC
			private bool HandleBaseCloseComplete(IAsyncResult result)
			{
				this.parent.BaseEndClose(result);
				if (this.parent.clientCertificateProvider == null)
				{
					return true;
				}
				IAsyncResult asyncResult = SecurityUtils.BeginCloseTokenProviderIfRequired(this.parent.clientCertificateProvider, this.timeoutHelper.RemainingTime(), this.onCloseTokenProvider, this);
				if (!asyncResult.CompletedSynchronously)
				{
					return false;
				}
				SecurityUtils.EndCloseTokenProviderIfRequired(asyncResult);
				return true;
			}

			// Token: 0x06007BFA RID: 31738 RVA: 0x001CF428 File Offset: 0x001CD628
			private void OnBaseClose(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				bool flag = false;
				try
				{
					flag = this.HandleBaseCloseComplete(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					flag = true;
					exception = ex;
				}
				if (flag)
				{
					base.Complete(false, exception);
				}
			}

			// Token: 0x06007BFB RID: 31739 RVA: 0x001CF478 File Offset: 0x001CD678
			private void OnCloseTokenProvider(IAsyncResult result)
			{
				if (result.CompletedSynchronously)
				{
					return;
				}
				Exception exception = null;
				try
				{
					SecurityUtils.EndCloseTokenProviderIfRequired(result);
				}
				catch (Exception ex)
				{
					if (Fx.IsFatal(ex))
					{
						throw;
					}
					exception = ex;
				}
				base.Complete(false, exception);
			}

			// Token: 0x0400472E RID: 18222
			private SslStreamSecurityUpgradeInitiator parent;

			// Token: 0x0400472F RID: 18223
			private TimeoutHelper timeoutHelper;

			// Token: 0x04004730 RID: 18224
			private AsyncCallback onBaseClose;

			// Token: 0x04004731 RID: 18225
			private AsyncCallback onCloseTokenProvider;
		}
	}
}
