using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Runtime;
using System.Security.Authentication;
using System.Security.Principal;
using System.ServiceModel.Description;
using System.ServiceModel.Diagnostics.Application;
using System.ServiceModel.Security;

namespace System.ServiceModel.Channels
{
	// Token: 0x0200083A RID: 2106
	internal class WindowsStreamSecurityUpgradeProvider : StreamSecurityUpgradeProvider
	{
		// Token: 0x06004EAB RID: 20139 RVA: 0x0011EE98 File Offset: 0x0011D098
		public WindowsStreamSecurityUpgradeProvider(WindowsStreamSecurityBindingElement bindingElement, BindingContext context, bool isClient) : base(context.Binding)
		{
			this.extractGroupsForWindowsAccounts = true;
			this.protectionLevel = bindingElement.ProtectionLevel;
			this.scheme = context.Binding.Scheme;
			this.isClient = isClient;
			this.listenUri = TransportSecurityHelpers.GetListenUri(context.ListenUriBaseAddress, context.ListenUriRelativeAddress);
			SecurityCredentialsManager securityCredentialsManager = context.BindingParameters.Find<SecurityCredentialsManager>();
			if (securityCredentialsManager == null)
			{
				if (isClient)
				{
					securityCredentialsManager = ClientCredentials.CreateDefaultCredentials();
				}
				else
				{
					securityCredentialsManager = ServiceCredentials.CreateDefaultCredentials();
				}
			}
			this.securityTokenManager = securityCredentialsManager.CreateSecurityTokenManager();
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x06004EAC RID: 20140 RVA: 0x0011EF1F File Offset: 0x0011D11F
		public string Scheme
		{
			get
			{
				return this.scheme;
			}
		}

		// Token: 0x17001398 RID: 5016
		// (get) Token: 0x06004EAD RID: 20141 RVA: 0x0011EF27 File Offset: 0x0011D127
		internal bool ExtractGroupsForWindowsAccounts
		{
			get
			{
				return this.extractGroupsForWindowsAccounts;
			}
		}

		// Token: 0x17001399 RID: 5017
		// (get) Token: 0x06004EAE RID: 20142 RVA: 0x0011EF30 File Offset: 0x0011D130
		public override EndpointIdentity Identity
		{
			get
			{
				if (this.serverCredential != null && this.identity == null)
				{
					object thisLock = base.ThisLock;
					lock (thisLock)
					{
						if (this.identity == null)
						{
							this.identity = SecurityUtils.CreateWindowsIdentity(this.serverCredential);
						}
					}
				}
				return this.identity;
			}
		}

		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x06004EAF RID: 20143 RVA: 0x0011EF9C File Offset: 0x0011D19C
		internal IdentityVerifier IdentityVerifier
		{
			get
			{
				return this.identityVerifier;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x06004EB0 RID: 20144 RVA: 0x0011EFA4 File Offset: 0x0011D1A4
		public ProtectionLevel ProtectionLevel
		{
			get
			{
				return this.protectionLevel;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x06004EB1 RID: 20145 RVA: 0x0011EFAC File Offset: 0x0011D1AC
		private NetworkCredential ServerCredential
		{
			get
			{
				return this.serverCredential;
			}
		}

		// Token: 0x06004EB2 RID: 20146 RVA: 0x0011EFB4 File Offset: 0x0011D1B4
		public override StreamUpgradeAcceptor CreateUpgradeAcceptor()
		{
			base.ThrowIfDisposedOrNotOpen();
			return new WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeAcceptor(this);
		}

		// Token: 0x06004EB3 RID: 20147 RVA: 0x0011EFC2 File Offset: 0x0011D1C2
		public override StreamUpgradeInitiator CreateUpgradeInitiator(EndpointAddress remoteAddress, Uri via)
		{
			base.ThrowIfDisposedOrNotOpen();
			return new WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator(this, remoteAddress, via);
		}

		// Token: 0x06004EB4 RID: 20148 RVA: 0x0011EFD2 File Offset: 0x0011D1D2
		protected override void OnAbort()
		{
		}

		// Token: 0x06004EB5 RID: 20149 RVA: 0x0011EFD4 File Offset: 0x0011D1D4
		protected override void OnClose(TimeSpan timeout)
		{
		}

		// Token: 0x06004EB6 RID: 20150 RVA: 0x0011EFD6 File Offset: 0x0011D1D6
		protected override IAsyncResult OnBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
		{
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06004EB7 RID: 20151 RVA: 0x0011EFDF File Offset: 0x0011D1DF
		protected override void OnEndClose(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06004EB8 RID: 20152 RVA: 0x0011EFE8 File Offset: 0x0011D1E8
		protected override void OnOpen(TimeSpan timeout)
		{
			if (!this.isClient)
			{
				SecurityTokenRequirement sspiTokenRequirement = TransportSecurityHelpers.CreateSspiTokenRequirement(this.Scheme, this.listenUri);
				this.serverCredential = TransportSecurityHelpers.GetSspiCredential(this.securityTokenManager, sspiTokenRequirement, timeout, out this.extractGroupsForWindowsAccounts);
			}
		}

		// Token: 0x06004EB9 RID: 20153 RVA: 0x0011F028 File Offset: 0x0011D228
		protected override IAsyncResult OnBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
		{
			this.OnOpen(timeout);
			return new CompletedAsyncResult(callback, state);
		}

		// Token: 0x06004EBA RID: 20154 RVA: 0x0011F038 File Offset: 0x0011D238
		protected override void OnEndOpen(IAsyncResult result)
		{
			CompletedAsyncResult.End(result);
		}

		// Token: 0x06004EBB RID: 20155 RVA: 0x0011F040 File Offset: 0x0011D240
		protected override void OnOpened()
		{
			base.OnOpened();
			if (this.identityVerifier == null)
			{
				this.identityVerifier = IdentityVerifier.CreateDefault();
			}
			if (this.serverCredential == null)
			{
				this.serverCredential = CredentialCache.DefaultNetworkCredentials;
			}
		}

		// Token: 0x040030F2 RID: 12530
		private bool extractGroupsForWindowsAccounts;

		// Token: 0x040030F3 RID: 12531
		private EndpointIdentity identity;

		// Token: 0x040030F4 RID: 12532
		private IdentityVerifier identityVerifier;

		// Token: 0x040030F5 RID: 12533
		private ProtectionLevel protectionLevel;

		// Token: 0x040030F6 RID: 12534
		private SecurityTokenManager securityTokenManager;

		// Token: 0x040030F7 RID: 12535
		private NetworkCredential serverCredential;

		// Token: 0x040030F8 RID: 12536
		private string scheme;

		// Token: 0x040030F9 RID: 12537
		private bool isClient;

		// Token: 0x040030FA RID: 12538
		private Uri listenUri;

		// Token: 0x02000D2F RID: 3375
		private class WindowsStreamSecurityUpgradeAcceptor : StreamSecurityUpgradeAcceptorBase
		{
			// Token: 0x06007C07 RID: 31751 RVA: 0x001CF7B2 File Offset: 0x001CD9B2
			public WindowsStreamSecurityUpgradeAcceptor(WindowsStreamSecurityUpgradeProvider parent) : base("application/negotiate")
			{
				this.parent = parent;
				this.clientSecurity = new SecurityMessageProperty();
			}

			// Token: 0x06007C08 RID: 31752 RVA: 0x001CF7D4 File Offset: 0x001CD9D4
			protected override Stream OnAcceptUpgrade(Stream stream, out SecurityMessageProperty remoteSecurity)
			{
				NegotiateStream negotiateStream = new NegotiateStream(stream);
				try
				{
					if (TD.WindowsStreamSecurityOnAcceptUpgradeIsEnabled())
					{
						TD.WindowsStreamSecurityOnAcceptUpgrade(base.EventTraceActivity);
					}
					negotiateStream.AuthenticateAsServer(this.parent.ServerCredential, this.parent.ProtectionLevel, TokenImpersonationLevel.Identification);
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
				remoteSecurity = this.CreateClientSecurity(negotiateStream, this.parent.ExtractGroupsForWindowsAccounts);
				return negotiateStream;
			}

			// Token: 0x06007C09 RID: 31753 RVA: 0x001CF890 File Offset: 0x001CDA90
			protected override IAsyncResult OnBeginAcceptUpgrade(Stream stream, AsyncCallback callback, object state)
			{
				WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeAcceptor.AcceptUpgradeAsyncResult acceptUpgradeAsyncResult = new WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeAcceptor.AcceptUpgradeAsyncResult(this, callback, state);
				acceptUpgradeAsyncResult.Begin(stream);
				return acceptUpgradeAsyncResult;
			}

			// Token: 0x06007C0A RID: 31754 RVA: 0x001CF8AE File Offset: 0x001CDAAE
			protected override Stream OnEndAcceptUpgrade(IAsyncResult result, out SecurityMessageProperty remoteSecurity)
			{
				return StreamSecurityUpgradeAcceptorAsyncResult.End(result, out remoteSecurity);
			}

			// Token: 0x06007C0B RID: 31755 RVA: 0x001CF8B8 File Offset: 0x001CDAB8
			private SecurityMessageProperty CreateClientSecurity(NegotiateStream negotiateStream, bool extractGroupsForWindowsAccounts)
			{
				WindowsIdentity windowsIdentity = (WindowsIdentity)negotiateStream.RemoteIdentity;
				SecurityUtils.ValidateAnonymityConstraint(windowsIdentity, false);
				WindowsSecurityTokenAuthenticator windowsSecurityTokenAuthenticator = new WindowsSecurityTokenAuthenticator(extractGroupsForWindowsAccounts);
				SecurityToken token = new WindowsSecurityToken(windowsIdentity, SecurityUniqueId.Create().Value, windowsIdentity.AuthenticationType);
				ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = windowsSecurityTokenAuthenticator.ValidateToken(token);
				this.clientSecurity = new SecurityMessageProperty();
				this.clientSecurity.TransportToken = new SecurityTokenSpecification(token, readOnlyCollection);
				this.clientSecurity.ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection);
				return this.clientSecurity;
			}

			// Token: 0x06007C0C RID: 31756 RVA: 0x001CF936 File Offset: 0x001CDB36
			public override SecurityMessageProperty GetRemoteSecurity()
			{
				if (this.clientSecurity.TransportToken != null)
				{
					return this.clientSecurity;
				}
				return base.GetRemoteSecurity();
			}

			// Token: 0x0400473A RID: 18234
			private WindowsStreamSecurityUpgradeProvider parent;

			// Token: 0x0400473B RID: 18235
			private SecurityMessageProperty clientSecurity;

			// Token: 0x02000F50 RID: 3920
			private class AcceptUpgradeAsyncResult : StreamSecurityUpgradeAcceptorAsyncResult
			{
				// Token: 0x06008706 RID: 34566 RVA: 0x001F498F File Offset: 0x001F2B8F
				public AcceptUpgradeAsyncResult(WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeAcceptor acceptor, AsyncCallback callback, object state) : base(callback, state)
				{
					this.acceptor = acceptor;
				}

				// Token: 0x06008707 RID: 34567 RVA: 0x001F49A0 File Offset: 0x001F2BA0
				protected override IAsyncResult OnBegin(Stream stream, AsyncCallback callback)
				{
					this.negotiateStream = new NegotiateStream(stream);
					return this.negotiateStream.BeginAuthenticateAsServer(this.acceptor.parent.ServerCredential, this.acceptor.parent.ProtectionLevel, TokenImpersonationLevel.Identification, callback, this);
				}

				// Token: 0x06008708 RID: 34568 RVA: 0x001F49DC File Offset: 0x001F2BDC
				protected override Stream OnCompleteAuthenticateAsServer(IAsyncResult result)
				{
					this.negotiateStream.EndAuthenticateAsServer(result);
					return this.negotiateStream;
				}

				// Token: 0x06008709 RID: 34569 RVA: 0x001F49F0 File Offset: 0x001F2BF0
				protected override SecurityMessageProperty ValidateCreateSecurity()
				{
					return this.acceptor.CreateClientSecurity(this.negotiateStream, this.acceptor.parent.ExtractGroupsForWindowsAccounts);
				}

				// Token: 0x04004E89 RID: 20105
				private WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeAcceptor acceptor;

				// Token: 0x04004E8A RID: 20106
				private NegotiateStream negotiateStream;
			}
		}

		// Token: 0x02000D30 RID: 3376
		private class WindowsStreamSecurityUpgradeInitiator : StreamSecurityUpgradeInitiatorBase
		{
			// Token: 0x06007C0D RID: 31757 RVA: 0x001CF952 File Offset: 0x001CDB52
			public WindowsStreamSecurityUpgradeInitiator(WindowsStreamSecurityUpgradeProvider parent, EndpointAddress remoteAddress, Uri via) : base("application/negotiate", remoteAddress, via)
			{
				this.parent = parent;
				this.clientTokenProvider = TransportSecurityHelpers.GetSspiTokenProvider(parent.securityTokenManager, remoteAddress, via, parent.Scheme, out this.identityVerifier);
			}

			// Token: 0x06007C0E RID: 31758 RVA: 0x001CF987 File Offset: 0x001CDB87
			private IAsyncResult BaseBeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.BeginOpen(timeout, callback, state);
			}

			// Token: 0x06007C0F RID: 31759 RVA: 0x001CF992 File Offset: 0x001CDB92
			private void BaseEndOpen(IAsyncResult result)
			{
				base.EndOpen(result);
			}

			// Token: 0x06007C10 RID: 31760 RVA: 0x001CF99B File Offset: 0x001CDB9B
			internal override IAsyncResult BeginOpen(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator.OpenAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x06007C11 RID: 31761 RVA: 0x001CF9A6 File Offset: 0x001CDBA6
			internal override void EndOpen(IAsyncResult result)
			{
				WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator.OpenAsyncResult.End(result);
			}

			// Token: 0x06007C12 RID: 31762 RVA: 0x001CF9B0 File Offset: 0x001CDBB0
			internal override void Open(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.Open(timeoutHelper.RemainingTime());
				SecurityUtils.OpenTokenProviderIfRequired(this.clientTokenProvider, timeoutHelper.RemainingTime());
				this.credential = TransportSecurityHelpers.GetSspiCredential(this.clientTokenProvider, timeoutHelper.RemainingTime(), out this.impersonationLevel, out this.allowNtlm);
			}

			// Token: 0x06007C13 RID: 31763 RVA: 0x001CFA08 File Offset: 0x001CDC08
			private IAsyncResult BaseBeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return base.BeginClose(timeout, callback, state);
			}

			// Token: 0x06007C14 RID: 31764 RVA: 0x001CFA13 File Offset: 0x001CDC13
			private void BaseEndClose(IAsyncResult result)
			{
				base.EndClose(result);
			}

			// Token: 0x06007C15 RID: 31765 RVA: 0x001CFA1C File Offset: 0x001CDC1C
			internal override IAsyncResult BeginClose(TimeSpan timeout, AsyncCallback callback, object state)
			{
				return new WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator.CloseAsyncResult(this, timeout, callback, state);
			}

			// Token: 0x06007C16 RID: 31766 RVA: 0x001CFA27 File Offset: 0x001CDC27
			internal override void EndClose(IAsyncResult result)
			{
				WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator.CloseAsyncResult.End(result);
			}

			// Token: 0x06007C17 RID: 31767 RVA: 0x001CFA30 File Offset: 0x001CDC30
			internal override void Close(TimeSpan timeout)
			{
				TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
				base.Close(timeoutHelper.RemainingTime());
				SecurityUtils.CloseTokenProviderIfRequired(this.clientTokenProvider, timeoutHelper.RemainingTime());
			}

			// Token: 0x06007C18 RID: 31768 RVA: 0x001CFA64 File Offset: 0x001CDC64
			protected override IAsyncResult OnBeginInitiateUpgrade(Stream stream, AsyncCallback callback, object state)
			{
				if (TD.WindowsStreamSecurityOnInitiateUpgradeIsEnabled())
				{
					TD.WindowsStreamSecurityOnInitiateUpgrade();
				}
				WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator.InitiateUpgradeAsyncResult initiateUpgradeAsyncResult = new WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator.InitiateUpgradeAsyncResult(this, callback, state);
				initiateUpgradeAsyncResult.Begin(stream);
				return initiateUpgradeAsyncResult;
			}

			// Token: 0x06007C19 RID: 31769 RVA: 0x001CFA8E File Offset: 0x001CDC8E
			protected override Stream OnEndInitiateUpgrade(IAsyncResult result, out SecurityMessageProperty remoteSecurity)
			{
				return StreamSecurityUpgradeInitiatorAsyncResult.End(result, out remoteSecurity);
			}

			// Token: 0x06007C1A RID: 31770 RVA: 0x001CFA98 File Offset: 0x001CDC98
			private static SecurityMessageProperty CreateServerSecurity(NegotiateStream negotiateStream)
			{
				GenericIdentity genericIdentity = (GenericIdentity)negotiateStream.RemoteIdentity;
				string name = genericIdentity.Name;
				if (name != null && name.Length > 0)
				{
					ReadOnlyCollection<IAuthorizationPolicy> readOnlyCollection = SecurityUtils.CreatePrincipalNameAuthorizationPolicies(name);
					return new SecurityMessageProperty
					{
						TransportToken = new SecurityTokenSpecification(null, readOnlyCollection),
						ServiceSecurityContext = new ServiceSecurityContext(readOnlyCollection)
					};
				}
				return null;
			}

			// Token: 0x06007C1B RID: 31771 RVA: 0x001CFAF0 File Offset: 0x001CDCF0
			protected override Stream OnInitiateUpgrade(Stream stream, out SecurityMessageProperty remoteSecurity)
			{
				if (TD.WindowsStreamSecurityOnInitiateUpgradeIsEnabled())
				{
					TD.WindowsStreamSecurityOnInitiateUpgrade();
				}
				NegotiateStream negotiateStream;
				string targetName;
				EndpointIdentity expectedIdentity;
				this.InitiateUpgradePrepare(stream, out negotiateStream, out targetName, out expectedIdentity);
				try
				{
					negotiateStream.AuthenticateAsClient(this.credential, targetName, this.parent.ProtectionLevel, this.impersonationLevel);
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
				remoteSecurity = WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator.CreateServerSecurity(negotiateStream);
				this.ValidateMutualAuth(expectedIdentity, negotiateStream, remoteSecurity, this.allowNtlm);
				return negotiateStream;
			}

			// Token: 0x06007C1C RID: 31772 RVA: 0x001CFBB4 File Offset: 0x001CDDB4
			private void InitiateUpgradePrepare(Stream stream, out NegotiateStream negotiateStream, out string targetName, out EndpointIdentity identity)
			{
				negotiateStream = new NegotiateStream(stream);
				targetName = string.Empty;
				identity = null;
				if (this.parent.IdentityVerifier.TryGetIdentity(base.RemoteAddress, base.Via, out identity))
				{
					targetName = SecurityUtils.GetSpnFromIdentity(identity, base.RemoteAddress);
					return;
				}
				targetName = SecurityUtils.GetSpnFromTarget(base.RemoteAddress);
			}

			// Token: 0x06007C1D RID: 31773 RVA: 0x001CFC14 File Offset: 0x001CDE14
			private void ValidateMutualAuth(EndpointIdentity expectedIdentity, NegotiateStream negotiateStream, SecurityMessageProperty remoteSecurity, bool allowNtlm)
			{
				if (negotiateStream.IsMutuallyAuthenticated)
				{
					if (expectedIdentity != null && !this.parent.IdentityVerifier.CheckAccess(expectedIdentity, remoteSecurity.ServiceSecurityContext.AuthorizationContext))
					{
						string identityNamesFromContext = SecurityUtils.GetIdentityNamesFromContext(remoteSecurity.ServiceSecurityContext.AuthorizationContext);
						throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("RemoteIdentityFailedVerification", new object[]
						{
							identityNamesFromContext
						})));
					}
				}
				else if (!allowNtlm)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityNegotiationException(SR.GetString("StreamMutualAuthNotSatisfied")));
				}
			}

			// Token: 0x0400473C RID: 18236
			private WindowsStreamSecurityUpgradeProvider parent;

			// Token: 0x0400473D RID: 18237
			private IdentityVerifier identityVerifier;

			// Token: 0x0400473E RID: 18238
			private NetworkCredential credential;

			// Token: 0x0400473F RID: 18239
			private TokenImpersonationLevel impersonationLevel;

			// Token: 0x04004740 RID: 18240
			private SspiSecurityTokenProvider clientTokenProvider;

			// Token: 0x04004741 RID: 18241
			private bool allowNtlm;

			// Token: 0x02000F51 RID: 3921
			private class InitiateUpgradeAsyncResult : StreamSecurityUpgradeInitiatorAsyncResult
			{
				// Token: 0x0600870A RID: 34570 RVA: 0x001F4A13 File Offset: 0x001F2C13
				public InitiateUpgradeAsyncResult(WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator initiator, AsyncCallback callback, object state) : base(callback, state)
				{
					this.initiator = initiator;
				}

				// Token: 0x0600870B RID: 34571 RVA: 0x001F4A24 File Offset: 0x001F2C24
				protected override IAsyncResult OnBeginAuthenticateAsClient(Stream stream, AsyncCallback callback)
				{
					string targetName;
					this.initiator.InitiateUpgradePrepare(stream, out this.negotiateStream, out targetName, out this.expectedIdentity);
					return this.negotiateStream.BeginAuthenticateAsClient(this.initiator.credential, targetName, this.initiator.parent.ProtectionLevel, this.initiator.impersonationLevel, callback, this);
				}

				// Token: 0x0600870C RID: 34572 RVA: 0x001F4A7F File Offset: 0x001F2C7F
				protected override Stream OnCompleteAuthenticateAsClient(IAsyncResult result)
				{
					this.negotiateStream.EndAuthenticateAsClient(result);
					return this.negotiateStream;
				}

				// Token: 0x0600870D RID: 34573 RVA: 0x001F4A94 File Offset: 0x001F2C94
				protected override SecurityMessageProperty ValidateCreateSecurity()
				{
					SecurityMessageProperty securityMessageProperty = WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator.CreateServerSecurity(this.negotiateStream);
					this.initiator.ValidateMutualAuth(this.expectedIdentity, this.negotiateStream, securityMessageProperty, this.initiator.allowNtlm);
					return securityMessageProperty;
				}

				// Token: 0x04004E8B RID: 20107
				private EndpointIdentity expectedIdentity;

				// Token: 0x04004E8C RID: 20108
				private WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator initiator;

				// Token: 0x04004E8D RID: 20109
				private NegotiateStream negotiateStream;
			}

			// Token: 0x02000F52 RID: 3922
			private class OpenAsyncResult : AsyncResult
			{
				// Token: 0x0600870E RID: 34574 RVA: 0x001F4AD4 File Offset: 0x001F2CD4
				public OpenAsyncResult(WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator parent, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.parent = parent;
					TimeoutHelper timeoutHelper = new TimeoutHelper(timeout);
					this.onBaseOpen = Fx.ThunkCallback(new AsyncCallback(this.OnBaseOpen));
					this.onGetSspiCredential = Fx.ThunkCallback(new AsyncCallback(this.OnGetSspiCredential));
					this.onOpenTokenProvider = Fx.ThunkCallback(new AsyncCallback(this.OnOpenTokenProvider));
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

				// Token: 0x0600870F RID: 34575 RVA: 0x001F4B6C File Offset: 0x001F2D6C
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator.OpenAsyncResult>(result);
				}

				// Token: 0x06008710 RID: 34576 RVA: 0x001F4B78 File Offset: 0x001F2D78
				private bool HandleBaseOpenComplete(IAsyncResult result)
				{
					this.parent.BaseEndOpen(result);
					IAsyncResult asyncResult = SecurityUtils.BeginOpenTokenProviderIfRequired(this.parent.clientTokenProvider, this.timeoutHelper.RemainingTime(), this.onOpenTokenProvider, this);
					return asyncResult.CompletedSynchronously && this.HandleOpenTokenProviderComplete(asyncResult);
				}

				// Token: 0x06008711 RID: 34577 RVA: 0x001F4BC8 File Offset: 0x001F2DC8
				private bool HandleOpenTokenProviderComplete(IAsyncResult result)
				{
					SecurityUtils.EndOpenTokenProviderIfRequired(result);
					IAsyncResult asyncResult = TransportSecurityHelpers.BeginGetSspiCredential(this.parent.clientTokenProvider, this.timeoutHelper.RemainingTime(), this.onGetSspiCredential, this);
					return asyncResult.CompletedSynchronously && this.HandleGetSspiCredentialComplete(asyncResult);
				}

				// Token: 0x06008712 RID: 34578 RVA: 0x001F4C0F File Offset: 0x001F2E0F
				private bool HandleGetSspiCredentialComplete(IAsyncResult result)
				{
					this.parent.credential = TransportSecurityHelpers.EndGetSspiCredential(result, out this.parent.impersonationLevel, out this.parent.allowNtlm);
					return true;
				}

				// Token: 0x06008713 RID: 34579 RVA: 0x001F4C3C File Offset: 0x001F2E3C
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

				// Token: 0x06008714 RID: 34580 RVA: 0x001F4C8C File Offset: 0x001F2E8C
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

				// Token: 0x06008715 RID: 34581 RVA: 0x001F4CDC File Offset: 0x001F2EDC
				private void OnGetSspiCredential(IAsyncResult result)
				{
					if (result.CompletedSynchronously)
					{
						return;
					}
					Exception exception = null;
					bool flag = false;
					try
					{
						flag = this.HandleGetSspiCredentialComplete(result);
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

				// Token: 0x04004E8E RID: 20110
				private WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator parent;

				// Token: 0x04004E8F RID: 20111
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004E90 RID: 20112
				private AsyncCallback onBaseOpen;

				// Token: 0x04004E91 RID: 20113
				private AsyncCallback onOpenTokenProvider;

				// Token: 0x04004E92 RID: 20114
				private AsyncCallback onGetSspiCredential;
			}

			// Token: 0x02000F53 RID: 3923
			private class CloseAsyncResult : AsyncResult
			{
				// Token: 0x06008716 RID: 34582 RVA: 0x001F4D2C File Offset: 0x001F2F2C
				public CloseAsyncResult(WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator parent, TimeSpan timeout, AsyncCallback callback, object state) : base(callback, state)
				{
					this.parent = parent;
					this.timeoutHelper = new TimeoutHelper(timeout);
					this.onBaseClose = Fx.ThunkCallback(new AsyncCallback(this.OnBaseClose));
					this.onCloseTokenProvider = Fx.ThunkCallback(new AsyncCallback(this.OnCloseTokenProvider));
					IAsyncResult asyncResult = parent.BaseBeginClose(this.timeoutHelper.RemainingTime(), this.onBaseClose, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return;
					}
					if (this.HandleBaseCloseComplete(asyncResult))
					{
						base.Complete(true);
					}
				}

				// Token: 0x06008717 RID: 34583 RVA: 0x001F4DB5 File Offset: 0x001F2FB5
				public static void End(IAsyncResult result)
				{
					AsyncResult.End<WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator.CloseAsyncResult>(result);
				}

				// Token: 0x06008718 RID: 34584 RVA: 0x001F4DC0 File Offset: 0x001F2FC0
				private bool HandleBaseCloseComplete(IAsyncResult result)
				{
					this.parent.BaseEndClose(result);
					IAsyncResult asyncResult = SecurityUtils.BeginCloseTokenProviderIfRequired(this.parent.clientTokenProvider, this.timeoutHelper.RemainingTime(), this.onCloseTokenProvider, this);
					if (!asyncResult.CompletedSynchronously)
					{
						return false;
					}
					SecurityUtils.EndCloseTokenProviderIfRequired(asyncResult);
					return true;
				}

				// Token: 0x06008719 RID: 34585 RVA: 0x001F4E10 File Offset: 0x001F3010
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

				// Token: 0x0600871A RID: 34586 RVA: 0x001F4E60 File Offset: 0x001F3060
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

				// Token: 0x04004E93 RID: 20115
				private WindowsStreamSecurityUpgradeProvider.WindowsStreamSecurityUpgradeInitiator parent;

				// Token: 0x04004E94 RID: 20116
				private TimeoutHelper timeoutHelper;

				// Token: 0x04004E95 RID: 20117
				private AsyncCallback onBaseClose;

				// Token: 0x04004E96 RID: 20118
				private AsyncCallback onCloseTokenProvider;
			}
		}
	}
}
