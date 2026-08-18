using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.IdentityModel;
using System.IdentityModel.Claims;
using System.IdentityModel.Policy;
using System.Net;
using System.Runtime;
using System.Security.Principal;
using System.ServiceModel.Channels;
using System.ServiceModel.Diagnostics;
using System.Xml;

namespace System.ServiceModel.Security
{
	// Token: 0x02000306 RID: 774
	internal class SpnegoTokenProvider : SspiNegotiationTokenProvider
	{
		// Token: 0x06001A6B RID: 6763 RVA: 0x00062D54 File Offset: 0x00060F54
		public SpnegoTokenProvider(SafeFreeCredentials credentialsHandle) : this(credentialsHandle, null)
		{
		}

		// Token: 0x06001A6C RID: 6764 RVA: 0x00062D5E File Offset: 0x00060F5E
		public SpnegoTokenProvider(SafeFreeCredentials credentialsHandle, SecurityBindingElement securityBindingElement) : base(securityBindingElement)
		{
			this.credentialsHandle = credentialsHandle;
		}

		// Token: 0x1700068D RID: 1677
		// (get) Token: 0x06001A6D RID: 6765 RVA: 0x00062D95 File Offset: 0x00060F95
		// (set) Token: 0x06001A6E RID: 6766 RVA: 0x00062D9D File Offset: 0x00060F9D
		public IdentityVerifier IdentityVerifier
		{
			get
			{
				return this.identityVerifier;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.identityVerifier = value;
			}
		}

		// Token: 0x1700068E RID: 1678
		// (get) Token: 0x06001A6F RID: 6767 RVA: 0x00062DB1 File Offset: 0x00060FB1
		// (set) Token: 0x06001A70 RID: 6768 RVA: 0x00062DBC File Offset: 0x00060FBC
		public TokenImpersonationLevel AllowedImpersonationLevel
		{
			get
			{
				return this.allowedImpersonationLevel;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				TokenImpersonationLevelHelper.Validate(value);
				if (value == TokenImpersonationLevel.None)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentOutOfRangeException("value", string.Format(CultureInfo.InvariantCulture, SR.GetString("SpnegoImpersonationLevelCannotBeSetToNone"), new object[0])));
				}
				this.allowedImpersonationLevel = value;
			}
		}

		// Token: 0x1700068F RID: 1679
		// (get) Token: 0x06001A71 RID: 6769 RVA: 0x00062E13 File Offset: 0x00061013
		// (set) Token: 0x06001A72 RID: 6770 RVA: 0x00062E1B File Offset: 0x0006101B
		public ICredentials ClientCredential
		{
			get
			{
				return this.clientCredential;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.clientCredential = value;
			}
		}

		// Token: 0x17000690 RID: 1680
		// (get) Token: 0x06001A73 RID: 6771 RVA: 0x00062E2F File Offset: 0x0006102F
		// (set) Token: 0x06001A74 RID: 6772 RVA: 0x00062E37 File Offset: 0x00061037
		public bool AllowNtlm
		{
			get
			{
				return this.allowNtlm;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.allowNtlm = value;
			}
		}

		// Token: 0x17000691 RID: 1681
		// (get) Token: 0x06001A75 RID: 6773 RVA: 0x00062E4B File Offset: 0x0006104B
		// (set) Token: 0x06001A76 RID: 6774 RVA: 0x00062E53 File Offset: 0x00061053
		public bool AuthenticateServer
		{
			get
			{
				return this.authenticateServer;
			}
			set
			{
				base.CommunicationObject.ThrowIfDisposedOrImmutable();
				this.authenticateServer = value;
			}
		}

		// Token: 0x17000692 RID: 1682
		// (get) Token: 0x06001A77 RID: 6775 RVA: 0x00062E67 File Offset: 0x00061067
		// (set) Token: 0x06001A78 RID: 6776 RVA: 0x00062E6F File Offset: 0x0006106F
		public bool InteractiveNegoExLogonEnabled
		{
			get
			{
				return this.interactiveNegoExLogonEnabled;
			}
			set
			{
				this.interactiveNegoExLogonEnabled = value;
			}
		}

		// Token: 0x17000693 RID: 1683
		// (get) Token: 0x06001A79 RID: 6777 RVA: 0x00062E78 File Offset: 0x00061078
		public override XmlDictionaryString NegotiationValueType
		{
			get
			{
				return XD.TrustApr2004Dictionary.SpnegoValueTypeUri;
			}
		}

		// Token: 0x06001A7A RID: 6778 RVA: 0x00062E84 File Offset: 0x00061084
		public override void OnOpening()
		{
			bool flag = SecurityUtils.IsOsGreaterThanXP();
			base.OnOpening();
			if (this.credentialsHandle == null)
			{
				string text;
				if (!this.allowNtlm && !flag)
				{
					text = "Kerberos";
				}
				else
				{
					text = "Negotiate";
				}
				NetworkCredential credential = null;
				if (this.clientCredential != null)
				{
					credential = this.clientCredential.GetCredential(base.TargetAddress.Uri, text);
				}
				if (!this.allowNtlm && flag)
				{
					this.credentialsHandle = SecurityUtils.GetCredentialsHandle(text, credential, false, new string[]
					{
						"!NTLM"
					});
				}
				else
				{
					this.credentialsHandle = SecurityUtils.GetCredentialsHandle(text, credential, false, new string[0]);
				}
				this.ownCredentialsHandle = true;
			}
		}

		// Token: 0x06001A7B RID: 6779 RVA: 0x00062F29 File Offset: 0x00061129
		public override void OnClose(TimeSpan timeout)
		{
			base.OnClose(timeout);
			this.FreeCredentialsHandle();
		}

		// Token: 0x06001A7C RID: 6780 RVA: 0x00062F38 File Offset: 0x00061138
		public override void OnAbort()
		{
			base.OnAbort();
			this.FreeCredentialsHandle();
		}

		// Token: 0x06001A7D RID: 6781 RVA: 0x00062F46 File Offset: 0x00061146
		private void FreeCredentialsHandle()
		{
			if (this.credentialsHandle != null)
			{
				if (this.ownCredentialsHandle)
				{
					this.credentialsHandle.Close();
				}
				this.credentialsHandle = null;
			}
		}

		// Token: 0x06001A7E RID: 6782 RVA: 0x00062F6A File Offset: 0x0006116A
		protected override bool CreateNegotiationStateCompletesSynchronously(EndpointAddress target, Uri via)
		{
			return true;
		}

		// Token: 0x06001A7F RID: 6783 RVA: 0x00062F70 File Offset: 0x00061170
		protected override IAsyncResult BeginCreateNegotiationState(EndpointAddress target, Uri via, TimeSpan timeout, AsyncCallback callback, object state)
		{
			SspiNegotiationTokenProviderState data = this.CreateNegotiationState(target, via, timeout);
			return new CompletedAsyncResult<SspiNegotiationTokenProviderState>(data, callback, state);
		}

		// Token: 0x06001A80 RID: 6784 RVA: 0x00062F91 File Offset: 0x00061191
		protected override SspiNegotiationTokenProviderState EndCreateNegotiationState(IAsyncResult result)
		{
			return CompletedAsyncResult<SspiNegotiationTokenProviderState>.End(result);
		}

		// Token: 0x06001A81 RID: 6785 RVA: 0x00062F9C File Offset: 0x0006119C
		protected override SspiNegotiationTokenProviderState CreateNegotiationState(EndpointAddress target, Uri via, TimeSpan timeout)
		{
			base.EnsureEndpointAddressDoesNotRequireEncryption(target);
			EndpointIdentity endpointIdentity = null;
			if (this.identityVerifier == null)
			{
				endpointIdentity = target.Identity;
			}
			else
			{
				this.identityVerifier.TryGetIdentity(target, out endpointIdentity);
			}
			string servicePrincipalName;
			if (this.AuthenticateServer || !this.AllowNtlm)
			{
				servicePrincipalName = SecurityUtils.GetSpnFromIdentity(endpointIdentity, target);
			}
			else
			{
				Claim identityClaim = endpointIdentity.IdentityClaim;
				if (identityClaim != null && (identityClaim.ClaimType == ClaimTypes.Spn || identityClaim.ClaimType == ClaimTypes.Upn))
				{
					servicePrincipalName = identityClaim.Resource.ToString();
				}
				else
				{
					servicePrincipalName = "host/" + target.Uri.DnsSafeHost;
				}
			}
			string package;
			if (!this.allowNtlm && !SecurityUtils.IsOsGreaterThanXP())
			{
				package = "Kerberos";
			}
			else
			{
				package = "Negotiate";
			}
			WindowsSspiNegotiation sspiNegotiation = new WindowsSspiNegotiation(package, this.credentialsHandle, this.AllowedImpersonationLevel, servicePrincipalName, true, this.InteractiveNegoExLogonEnabled, this.allowNtlm);
			return new SspiNegotiationTokenProviderState(sspiNegotiation);
		}

		// Token: 0x06001A82 RID: 6786 RVA: 0x00063088 File Offset: 0x00061288
		protected override ReadOnlyCollection<IAuthorizationPolicy> ValidateSspiNegotiation(ISspiNegotiation sspiNegotiation)
		{
			WindowsSspiNegotiation windowsSspiNegotiation = (WindowsSspiNegotiation)sspiNegotiation;
			if (!windowsSspiNegotiation.IsValidContext)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("InvalidSspiNegotiation")));
			}
			if (this.AuthenticateServer && !windowsSspiNegotiation.IsMutualAuthFlag)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("CannotAuthenticateServer")));
			}
			SecurityTraceRecordHelper.TraceClientSpnego(windowsSspiNegotiation);
			return SecurityUtils.CreatePrincipalNameAuthorizationPolicies(windowsSspiNegotiation.ServicePrincipalName);
		}

		// Token: 0x04001D1E RID: 7454
		private TokenImpersonationLevel allowedImpersonationLevel = TokenImpersonationLevel.Identification;

		// Token: 0x04001D1F RID: 7455
		private ICredentials clientCredential;

		// Token: 0x04001D20 RID: 7456
		private IdentityVerifier identityVerifier = IdentityVerifier.CreateDefault();

		// Token: 0x04001D21 RID: 7457
		private bool allowNtlm = true;

		// Token: 0x04001D22 RID: 7458
		private bool authenticateServer = true;

		// Token: 0x04001D23 RID: 7459
		private SafeFreeCredentials credentialsHandle;

		// Token: 0x04001D24 RID: 7460
		private bool ownCredentialsHandle;

		// Token: 0x04001D25 RID: 7461
		private bool interactiveNegoExLogonEnabled = true;
	}
}
