using System;
using System.Collections.ObjectModel;
using System.IdentityModel.Policy;
using System.ServiceModel.Channels;
using System.ServiceModel.Security.Tokens;

namespace System.ServiceModel.Security
{
	// Token: 0x020002EF RID: 751
	public class SecurityMessageProperty : IMessageProperty, IDisposable
	{
		// Token: 0x060018C1 RID: 6337 RVA: 0x0005C16C File Offset: 0x0005A36C
		public SecurityMessageProperty()
		{
			this.securityContext = ServiceSecurityContext.Anonymous;
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x060018C2 RID: 6338 RVA: 0x0005C18A File Offset: 0x0005A38A
		// (set) Token: 0x060018C3 RID: 6339 RVA: 0x0005C198 File Offset: 0x0005A398
		public ServiceSecurityContext ServiceSecurityContext
		{
			get
			{
				this.ThrowIfDisposed();
				return this.securityContext;
			}
			set
			{
				this.ThrowIfDisposed();
				this.securityContext = value;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x060018C4 RID: 6340 RVA: 0x0005C1A7 File Offset: 0x0005A3A7
		// (set) Token: 0x060018C5 RID: 6341 RVA: 0x0005C1AF File Offset: 0x0005A3AF
		public ReadOnlyCollection<IAuthorizationPolicy> ExternalAuthorizationPolicies
		{
			get
			{
				return this.externalAuthorizationPolicies;
			}
			set
			{
				this.externalAuthorizationPolicies = value;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x060018C6 RID: 6342 RVA: 0x0005C1B8 File Offset: 0x0005A3B8
		// (set) Token: 0x060018C7 RID: 6343 RVA: 0x0005C1C6 File Offset: 0x0005A3C6
		public SecurityTokenSpecification ProtectionToken
		{
			get
			{
				this.ThrowIfDisposed();
				return this.protectionToken;
			}
			set
			{
				this.ThrowIfDisposed();
				this.protectionToken = value;
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x060018C8 RID: 6344 RVA: 0x0005C1D5 File Offset: 0x0005A3D5
		// (set) Token: 0x060018C9 RID: 6345 RVA: 0x0005C1E3 File Offset: 0x0005A3E3
		public SecurityTokenSpecification InitiatorToken
		{
			get
			{
				this.ThrowIfDisposed();
				return this.initiatorToken;
			}
			set
			{
				this.ThrowIfDisposed();
				this.initiatorToken = value;
			}
		}

		// Token: 0x170005FD RID: 1533
		// (get) Token: 0x060018CA RID: 6346 RVA: 0x0005C1F2 File Offset: 0x0005A3F2
		// (set) Token: 0x060018CB RID: 6347 RVA: 0x0005C200 File Offset: 0x0005A400
		public SecurityTokenSpecification RecipientToken
		{
			get
			{
				this.ThrowIfDisposed();
				return this.recipientToken;
			}
			set
			{
				this.ThrowIfDisposed();
				this.recipientToken = value;
			}
		}

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x060018CC RID: 6348 RVA: 0x0005C20F File Offset: 0x0005A40F
		// (set) Token: 0x060018CD RID: 6349 RVA: 0x0005C21D File Offset: 0x0005A41D
		public SecurityTokenSpecification TransportToken
		{
			get
			{
				this.ThrowIfDisposed();
				return this.transportToken;
			}
			set
			{
				this.ThrowIfDisposed();
				this.transportToken = value;
			}
		}

		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x060018CE RID: 6350 RVA: 0x0005C22C File Offset: 0x0005A42C
		// (set) Token: 0x060018CF RID: 6351 RVA: 0x0005C234 File Offset: 0x0005A434
		public string SenderIdPrefix
		{
			get
			{
				return this.senderIdPrefix;
			}
			set
			{
				XmlHelper.ValidateIdPrefix(value);
				this.senderIdPrefix = value;
			}
		}

		// Token: 0x17000600 RID: 1536
		// (get) Token: 0x060018D0 RID: 6352 RVA: 0x0005C243 File Offset: 0x0005A443
		public bool HasIncomingSupportingTokens
		{
			get
			{
				this.ThrowIfDisposed();
				return this.incomingSupportingTokens != null && this.incomingSupportingTokens.Count > 0;
			}
		}

		// Token: 0x17000601 RID: 1537
		// (get) Token: 0x060018D1 RID: 6353 RVA: 0x0005C263 File Offset: 0x0005A463
		public Collection<SupportingTokenSpecification> IncomingSupportingTokens
		{
			get
			{
				this.ThrowIfDisposed();
				if (this.incomingSupportingTokens == null)
				{
					this.incomingSupportingTokens = new Collection<SupportingTokenSpecification>();
				}
				return this.incomingSupportingTokens;
			}
		}

		// Token: 0x17000602 RID: 1538
		// (get) Token: 0x060018D2 RID: 6354 RVA: 0x0005C284 File Offset: 0x0005A484
		public Collection<SupportingTokenSpecification> OutgoingSupportingTokens
		{
			get
			{
				if (this.outgoingSupportingTokens == null)
				{
					this.outgoingSupportingTokens = new Collection<SupportingTokenSpecification>();
				}
				return this.outgoingSupportingTokens;
			}
		}

		// Token: 0x17000603 RID: 1539
		// (get) Token: 0x060018D3 RID: 6355 RVA: 0x0005C29F File Offset: 0x0005A49F
		internal bool HasOutgoingSupportingTokens
		{
			get
			{
				return this.outgoingSupportingTokens != null && this.outgoingSupportingTokens.Count > 0;
			}
		}

		// Token: 0x060018D4 RID: 6356 RVA: 0x0005C2BC File Offset: 0x0005A4BC
		public IMessageProperty CreateCopy()
		{
			this.ThrowIfDisposed();
			SecurityMessageProperty securityMessageProperty = new SecurityMessageProperty();
			if (this.HasOutgoingSupportingTokens)
			{
				for (int i = 0; i < this.outgoingSupportingTokens.Count; i++)
				{
					securityMessageProperty.OutgoingSupportingTokens.Add(this.outgoingSupportingTokens[i]);
				}
			}
			if (this.HasIncomingSupportingTokens)
			{
				for (int j = 0; j < this.incomingSupportingTokens.Count; j++)
				{
					securityMessageProperty.IncomingSupportingTokens.Add(this.incomingSupportingTokens[j]);
				}
			}
			securityMessageProperty.securityContext = this.securityContext;
			securityMessageProperty.externalAuthorizationPolicies = this.externalAuthorizationPolicies;
			securityMessageProperty.senderIdPrefix = this.senderIdPrefix;
			securityMessageProperty.protectionToken = this.protectionToken;
			securityMessageProperty.initiatorToken = this.initiatorToken;
			securityMessageProperty.recipientToken = this.recipientToken;
			securityMessageProperty.transportToken = this.transportToken;
			return securityMessageProperty;
		}

		// Token: 0x060018D5 RID: 6357 RVA: 0x0005C394 File Offset: 0x0005A594
		public static SecurityMessageProperty GetOrCreate(Message message)
		{
			if (message == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("message");
			}
			SecurityMessageProperty securityMessageProperty = null;
			if (message.Properties != null)
			{
				securityMessageProperty = message.Properties.Security;
			}
			if (securityMessageProperty == null)
			{
				securityMessageProperty = new SecurityMessageProperty();
				message.Properties.Security = securityMessageProperty;
			}
			return securityMessageProperty;
		}

		// Token: 0x060018D6 RID: 6358 RVA: 0x0005C3E0 File Offset: 0x0005A5E0
		private void AddAuthorizationPolicies(SecurityTokenSpecification spec, Collection<IAuthorizationPolicy> policies)
		{
			if (spec != null && spec.SecurityTokenPolicies != null && spec.SecurityTokenPolicies.Count > 0)
			{
				for (int i = 0; i < spec.SecurityTokenPolicies.Count; i++)
				{
					policies.Add(spec.SecurityTokenPolicies[i]);
				}
			}
		}

		// Token: 0x060018D7 RID: 6359 RVA: 0x0005C42E File Offset: 0x0005A62E
		internal ReadOnlyCollection<IAuthorizationPolicy> GetInitiatorTokenAuthorizationPolicies()
		{
			return this.GetInitiatorTokenAuthorizationPolicies(true);
		}

		// Token: 0x060018D8 RID: 6360 RVA: 0x0005C437 File Offset: 0x0005A637
		internal ReadOnlyCollection<IAuthorizationPolicy> GetInitiatorTokenAuthorizationPolicies(bool includeTransportToken)
		{
			return this.GetInitiatorTokenAuthorizationPolicies(includeTransportToken, null);
		}

		// Token: 0x060018D9 RID: 6361 RVA: 0x0005C444 File Offset: 0x0005A644
		internal ReadOnlyCollection<IAuthorizationPolicy> GetInitiatorTokenAuthorizationPolicies(bool includeTransportToken, SecurityContextSecurityToken supportingSessionTokenToExclude)
		{
			if (!this.HasIncomingSupportingTokens)
			{
				if (this.transportToken != null && this.initiatorToken == null && this.protectionToken == null)
				{
					if (includeTransportToken && this.transportToken.SecurityTokenPolicies != null)
					{
						return this.transportToken.SecurityTokenPolicies;
					}
					return EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
				}
				else
				{
					if (this.transportToken == null && this.initiatorToken != null && this.protectionToken == null)
					{
						return this.initiatorToken.SecurityTokenPolicies ?? EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
					}
					if (this.transportToken == null && this.initiatorToken == null && this.protectionToken != null)
					{
						return this.protectionToken.SecurityTokenPolicies ?? EmptyReadOnlyCollection<IAuthorizationPolicy>.Instance;
					}
				}
			}
			Collection<IAuthorizationPolicy> collection = new Collection<IAuthorizationPolicy>();
			if (includeTransportToken)
			{
				this.AddAuthorizationPolicies(this.transportToken, collection);
			}
			this.AddAuthorizationPolicies(this.initiatorToken, collection);
			this.AddAuthorizationPolicies(this.protectionToken, collection);
			if (this.HasIncomingSupportingTokens)
			{
				int i = 0;
				while (i < this.incomingSupportingTokens.Count)
				{
					if (supportingSessionTokenToExclude == null)
					{
						goto IL_10B;
					}
					SecurityContextSecurityToken securityContextSecurityToken = this.incomingSupportingTokens[i].SecurityToken as SecurityContextSecurityToken;
					if (securityContextSecurityToken == null || !(securityContextSecurityToken.ContextId == supportingSessionTokenToExclude.ContextId))
					{
						goto IL_10B;
					}
					IL_13F:
					i++;
					continue;
					IL_10B:
					SecurityTokenAttachmentMode securityTokenAttachmentMode = this.incomingSupportingTokens[i].SecurityTokenAttachmentMode;
					if (securityTokenAttachmentMode == SecurityTokenAttachmentMode.Endorsing || securityTokenAttachmentMode == SecurityTokenAttachmentMode.Signed || securityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEncrypted || securityTokenAttachmentMode == SecurityTokenAttachmentMode.SignedEndorsing)
					{
						this.AddAuthorizationPolicies(this.incomingSupportingTokens[i], collection);
						goto IL_13F;
					}
					goto IL_13F;
				}
			}
			return new ReadOnlyCollection<IAuthorizationPolicy>(collection);
		}

		// Token: 0x060018DA RID: 6362 RVA: 0x0005C5A8 File Offset: 0x0005A7A8
		public void Dispose()
		{
			if (!this.disposed)
			{
				this.disposed = true;
			}
		}

		// Token: 0x060018DB RID: 6363 RVA: 0x0005C5B9 File Offset: 0x0005A7B9
		private void ThrowIfDisposed()
		{
			if (this.disposed)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ObjectDisposedException(base.GetType().FullName));
			}
		}

		// Token: 0x04001C55 RID: 7253
		private Collection<SupportingTokenSpecification> outgoingSupportingTokens;

		// Token: 0x04001C56 RID: 7254
		private Collection<SupportingTokenSpecification> incomingSupportingTokens;

		// Token: 0x04001C57 RID: 7255
		private SecurityTokenSpecification transportToken;

		// Token: 0x04001C58 RID: 7256
		private SecurityTokenSpecification protectionToken;

		// Token: 0x04001C59 RID: 7257
		private SecurityTokenSpecification initiatorToken;

		// Token: 0x04001C5A RID: 7258
		private SecurityTokenSpecification recipientToken;

		// Token: 0x04001C5B RID: 7259
		private ServiceSecurityContext securityContext;

		// Token: 0x04001C5C RID: 7260
		private ReadOnlyCollection<IAuthorizationPolicy> externalAuthorizationPolicies;

		// Token: 0x04001C5D RID: 7261
		private string senderIdPrefix = "_";

		// Token: 0x04001C5E RID: 7262
		private bool disposed;
	}
}
