using System;
using System.Collections.ObjectModel;
using System.IdentityModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.Net;
using System.Runtime;
using System.Security.Cryptography.X509Certificates;
using System.Security.Principal;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x0200030C RID: 780
	internal sealed class TlsnegoTokenAuthenticator : SspiNegotiationTokenAuthenticator
	{
		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x06001AC0 RID: 6848 RVA: 0x000642F0 File Offset: 0x000624F0
		// (set) Token: 0x06001AC1 RID: 6849 RVA: 0x000642F8 File Offset: 0x000624F8
		public SecurityTokenAuthenticator ClientTokenAuthenticator
		{
			get
			{
				return this.clientTokenAuthenticator;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.clientTokenAuthenticator = value;
			}
		}

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x06001AC2 RID: 6850 RVA: 0x0006430C File Offset: 0x0006250C
		// (set) Token: 0x06001AC3 RID: 6851 RVA: 0x00064314 File Offset: 0x00062514
		public SecurityTokenProvider ServerTokenProvider
		{
			get
			{
				return this.serverTokenProvider;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.serverTokenProvider = value;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x06001AC4 RID: 6852 RVA: 0x00064328 File Offset: 0x00062528
		// (set) Token: 0x06001AC5 RID: 6853 RVA: 0x00064330 File Offset: 0x00062530
		public bool MapCertificateToWindowsAccount
		{
			get
			{
				return this.mapCertificateToWindowsAccount;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.mapCertificateToWindowsAccount = value;
			}
		}

		// Token: 0x06001AC6 RID: 6854 RVA: 0x00064344 File Offset: 0x00062544
		private X509SecurityToken ValidateX509Token(SecurityToken token)
		{
			X509SecurityToken x509SecurityToken = token as X509SecurityToken;
			if (x509SecurityToken == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("TokenProviderReturnedBadToken", new object[]
				{
					(token == null) ? "<null>" : token.GetType().ToString()
				})));
			}
			SecurityUtils.EnsureCertificateCanDoKeyExchange(x509SecurityToken.Certificate);
			return x509SecurityToken;
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x06001AC7 RID: 6855 RVA: 0x000643A0 File Offset: 0x000625A0
		public override XmlDictionaryString NegotiationValueType
		{
			get
			{
				if (base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrustFeb2005)
				{
					return XD.TrustApr2004Dictionary.TlsnegoValueTypeUri;
				}
				if (base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrust13)
				{
					return DXD.TrustDec2005Dictionary.TlsnegoValueTypeUri;
				}
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException());
			}
		}

		// Token: 0x06001AC8 RID: 6856 RVA: 0x00064400 File Offset: 0x00062600
		public override void OnOpen(TimeSpan timeout)
		{
			if (this.serverTokenProvider == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("NoServerX509TokenProvider")));
			}
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			SecurityUtils.OpenTokenProviderIfRequired(this.serverTokenProvider, timeoutHelper.RemainingTime());
			if (this.clientTokenAuthenticator != null)
			{
				SecurityUtils.OpenTokenAuthenticatorIfRequired(this.clientTokenAuthenticator, timeoutHelper.RemainingTime());
			}
			SecurityToken token = this.serverTokenProvider.GetToken(timeoutHelper.RemainingTime());
			this.serverToken = this.ValidateX509Token(token);
			base.OnOpen(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001AC9 RID: 6857 RVA: 0x00064490 File Offset: 0x00062690
		public override void OnClose(TimeSpan timeout)
		{
			TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
			if (this.serverTokenProvider != null)
			{
				SecurityUtils.CloseTokenProviderIfRequired(this.serverTokenProvider, timeoutHelper.RemainingTime());
				this.serverTokenProvider = null;
			}
			if (this.clientTokenAuthenticator != null)
			{
				SecurityUtils.CloseTokenAuthenticatorIfRequired(this.clientTokenAuthenticator, timeoutHelper.RemainingTime());
				this.clientTokenAuthenticator = null;
			}
			if (this.serverToken != null)
			{
				this.serverToken = null;
			}
			base.OnClose(timeoutHelper.RemainingTime());
		}

		// Token: 0x06001ACA RID: 6858 RVA: 0x00064504 File Offset: 0x00062704
		public override void OnAbort()
		{
			if (this.serverTokenProvider != null)
			{
				SecurityUtils.AbortTokenProviderIfRequired(this.serverTokenProvider);
				this.serverTokenProvider = null;
			}
			if (this.clientTokenAuthenticator != null)
			{
				SecurityUtils.AbortTokenAuthenticatorIfRequired(this.clientTokenAuthenticator);
				this.clientTokenAuthenticator = null;
			}
			if (this.serverToken != null)
			{
				this.serverToken = null;
			}
			base.OnAbort();
		}

		// Token: 0x06001ACB RID: 6859 RVA: 0x0006455C File Offset: 0x0006275C
		protected override void ValidateIncomingBinaryNegotiation(BinaryNegotiation incomingNego)
		{
			if (incomingNego != null && incomingNego.ValueTypeUri != this.NegotiationValueType.Value && base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrustFeb2005)
			{
				incomingNego.Validate(DXD.TrustDec2005Dictionary.TlsnegoValueTypeUri);
				return;
			}
			base.ValidateIncomingBinaryNegotiation(incomingNego);
		}

		// Token: 0x06001ACC RID: 6860 RVA: 0x000645B4 File Offset: 0x000627B4
		protected override SspiNegotiationTokenAuthenticatorState CreateSspiState(byte[] incomingBlob, string incomingValueTypeUri)
		{
			TlsSspiNegotiation tlsSspiNegotiation;
			if (LocalAppContextSwitches.DisableUsingServicePointManagerSecurityProtocols)
			{
				tlsSspiNegotiation = new TlsSspiNegotiation((SchProtocols)80, this.serverToken.Certificate, this.ClientTokenAuthenticator != null);
			}
			else
			{
				SchProtocols protocolFlags = (SchProtocols)(ServicePointManager.SecurityProtocol & (SecurityProtocolType)1073747285);
				tlsSspiNegotiation = new TlsSspiNegotiation(protocolFlags, this.serverToken.Certificate, this.ClientTokenAuthenticator != null);
			}
			if (base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrustFeb2005 && this.NegotiationValueType.Value != incomingValueTypeUri)
			{
				tlsSspiNegotiation.IncomingValueTypeUri = incomingValueTypeUri;
			}
			return new SspiNegotiationTokenAuthenticatorState(tlsSspiNegotiation);
		}

		// Token: 0x06001ACD RID: 6861 RVA: 0x00064648 File Offset: 0x00062848
		protected override BinaryNegotiation GetOutgoingBinaryNegotiation(ISspiNegotiation sspiNegotiation, byte[] outgoingBlob)
		{
			TlsSspiNegotiation tlsSspiNegotiation = sspiNegotiation as TlsSspiNegotiation;
			if (base.StandardsManager.MessageSecurityVersion.TrustVersion == TrustVersion.WSTrustFeb2005 && tlsSspiNegotiation != null && tlsSspiNegotiation.IncomingValueTypeUri != null)
			{
				return new BinaryNegotiation(tlsSspiNegotiation.IncomingValueTypeUri, outgoingBlob);
			}
			return base.GetOutgoingBinaryNegotiation(sspiNegotiation, outgoingBlob);
		}

		// Token: 0x06001ACE RID: 6862 RVA: 0x00064694 File Offset: 0x00062894
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateSspiNegotiation(ISspiNegotiation sspiNegotiation)
		{
			TlsSspiNegotiation tlsSspiNegotiation = (TlsSspiNegotiation)sspiNegotiation;
			if (!tlsSspiNegotiation.IsValidContext)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityNegotiationException(SR.GetString("InvalidSspiNegotiation")));
			}
			if (this.ClientTokenAuthenticator == null)
			{
				return EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			}
			X509Certificate2 remoteCertificate = tlsSspiNegotiation.RemoteCertificate;
			if (remoteCertificate == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperWarning(new SecurityTokenValidationException(SR.GetString("ClientCertificateNotProvided")));
			}
			ReadOnlyCollection<IAuthorizationPolicy> result;
			if (this.ClientTokenAuthenticator != null)
			{
				WindowsIdentity windowsIdentity;
				X509SecurityToken x509SecurityToken;
				if (!this.MapCertificateToWindowsAccount || !tlsSspiNegotiation.TryGetContextIdentity(out windowsIdentity))
				{
					x509SecurityToken = new X509SecurityToken(remoteCertificate);
				}
				else
				{
					x509SecurityToken = new X509WindowsSecurityToken(remoteCertificate, windowsIdentity, windowsIdentity.AuthenticationType, true);
					windowsIdentity.Dispose();
				}
				result = this.ClientTokenAuthenticator.ValidateToken(x509SecurityToken);
				x509SecurityToken.Dispose();
			}
			else
			{
				result = EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
			}
			return result;
		}

		// Token: 0x04001D35 RID: 7477
		private SecurityTokenAuthenticator clientTokenAuthenticator;

		// Token: 0x04001D36 RID: 7478
		private SecurityTokenProvider serverTokenProvider;

		// Token: 0x04001D37 RID: 7479
		private X509SecurityToken serverToken;

		// Token: 0x04001D38 RID: 7480
		private bool mapCertificateToWindowsAccount;
	}
}
