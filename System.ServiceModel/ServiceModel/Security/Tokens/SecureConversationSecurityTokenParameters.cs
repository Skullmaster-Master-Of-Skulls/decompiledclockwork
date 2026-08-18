using System;
using System.Globalization;
using System.IdentityModel.Selectors;
using System.IdentityModel.Tokens;
using System.ServiceModel.Channels;
using System.Text;

namespace System.ServiceModel.Security.Tokens
{
	// Token: 0x020003A1 RID: 929
	[__DynamicallyInvokable]
	public class SecureConversationSecurityTokenParameters : SecurityTokenParameters
	{
		// Token: 0x060022AC RID: 8876 RVA: 0x0007F314 File Offset: 0x0007D514
		protected SecureConversationSecurityTokenParameters(SecureConversationSecurityTokenParameters other) : base(other)
		{
			this.requireCancellation = other.requireCancellation;
			this.canRenewSession = other.canRenewSession;
			if (other.bootstrapSecurityBindingElement != null)
			{
				this.bootstrapSecurityBindingElement = (SecurityBindingElement)other.bootstrapSecurityBindingElement.Clone();
			}
			if (other.bootstrapProtectionRequirements != null)
			{
				this.bootstrapProtectionRequirements = new ChannelProtectionRequirements(other.bootstrapProtectionRequirements);
			}
			if (other.issuerBindingContext != null)
			{
				this.issuerBindingContext = other.issuerBindingContext.Clone();
			}
		}

		// Token: 0x060022AD RID: 8877 RVA: 0x0007F397 File Offset: 0x0007D597
		[__DynamicallyInvokable]
		public SecureConversationSecurityTokenParameters() : this(null, true, null)
		{
		}

		// Token: 0x060022AE RID: 8878 RVA: 0x0007F3A2 File Offset: 0x0007D5A2
		[__DynamicallyInvokable]
		public SecureConversationSecurityTokenParameters(SecurityBindingElement bootstrapSecurityBindingElement) : this(bootstrapSecurityBindingElement, true, null)
		{
		}

		// Token: 0x060022AF RID: 8879 RVA: 0x0007F3AD File Offset: 0x0007D5AD
		public SecureConversationSecurityTokenParameters(SecurityBindingElement bootstrapSecurityBindingElement, bool requireCancellation) : this(bootstrapSecurityBindingElement, requireCancellation, true)
		{
		}

		// Token: 0x060022B0 RID: 8880 RVA: 0x0007F3B8 File Offset: 0x0007D5B8
		public SecureConversationSecurityTokenParameters(SecurityBindingElement bootstrapSecurityBindingElement, bool requireCancellation, bool canRenewSession) : this(bootstrapSecurityBindingElement, requireCancellation, canRenewSession, null)
		{
		}

		// Token: 0x060022B1 RID: 8881 RVA: 0x0007F3C4 File Offset: 0x0007D5C4
		public SecureConversationSecurityTokenParameters(SecurityBindingElement bootstrapSecurityBindingElement, bool requireCancellation, ChannelProtectionRequirements bootstrapProtectionRequirements) : this(bootstrapSecurityBindingElement, requireCancellation, true, null)
		{
		}

		// Token: 0x060022B2 RID: 8882 RVA: 0x0007F3D0 File Offset: 0x0007D5D0
		public SecureConversationSecurityTokenParameters(SecurityBindingElement bootstrapSecurityBindingElement, bool requireCancellation, bool canRenewSession, ChannelProtectionRequirements bootstrapProtectionRequirements)
		{
			this.bootstrapSecurityBindingElement = bootstrapSecurityBindingElement;
			this.canRenewSession = canRenewSession;
			if (bootstrapProtectionRequirements != null)
			{
				this.bootstrapProtectionRequirements = new ChannelProtectionRequirements(bootstrapProtectionRequirements);
			}
			else
			{
				this.bootstrapProtectionRequirements = new ChannelProtectionRequirements();
				this.bootstrapProtectionRequirements.IncomingEncryptionParts.AddParts(new MessagePartSpecification(true));
				this.bootstrapProtectionRequirements.IncomingSignatureParts.AddParts(new MessagePartSpecification(true));
				this.bootstrapProtectionRequirements.OutgoingEncryptionParts.AddParts(new MessagePartSpecification(true));
				this.bootstrapProtectionRequirements.OutgoingSignatureParts.AddParts(new MessagePartSpecification(true));
			}
			this.requireCancellation = requireCancellation;
		}

		// Token: 0x1700088C RID: 2188
		// (get) Token: 0x060022B3 RID: 8883 RVA: 0x0007F475 File Offset: 0x0007D675
		protected internal override bool HasAsymmetricKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700088D RID: 2189
		// (get) Token: 0x060022B4 RID: 8884 RVA: 0x0007F478 File Offset: 0x0007D678
		// (set) Token: 0x060022B5 RID: 8885 RVA: 0x0007F480 File Offset: 0x0007D680
		[__DynamicallyInvokable]
		public SecurityBindingElement BootstrapSecurityBindingElement
		{
			[__DynamicallyInvokable]
			get
			{
				return this.bootstrapSecurityBindingElement;
			}
			[__DynamicallyInvokable]
			set
			{
				this.bootstrapSecurityBindingElement = value;
			}
		}

		// Token: 0x1700088E RID: 2190
		// (get) Token: 0x060022B6 RID: 8886 RVA: 0x0007F489 File Offset: 0x0007D689
		public ChannelProtectionRequirements BootstrapProtectionRequirements
		{
			get
			{
				return this.bootstrapProtectionRequirements;
			}
		}

		// Token: 0x1700088F RID: 2191
		// (get) Token: 0x060022B7 RID: 8887 RVA: 0x0007F491 File Offset: 0x0007D691
		// (set) Token: 0x060022B8 RID: 8888 RVA: 0x0007F499 File Offset: 0x0007D699
		internal BindingContext IssuerBindingContext
		{
			get
			{
				return this.issuerBindingContext;
			}
			set
			{
				if (value != null)
				{
					value = value.Clone();
				}
				this.issuerBindingContext = value;
			}
		}

		// Token: 0x17000890 RID: 2192
		// (get) Token: 0x060022B9 RID: 8889 RVA: 0x0007F4AD File Offset: 0x0007D6AD
		private ISecurityCapabilities BootstrapSecurityCapabilities
		{
			get
			{
				return this.bootstrapSecurityBindingElement.GetIndividualProperty<ISecurityCapabilities>();
			}
		}

		// Token: 0x17000891 RID: 2193
		// (get) Token: 0x060022BA RID: 8890 RVA: 0x0007F4BA File Offset: 0x0007D6BA
		// (set) Token: 0x060022BB RID: 8891 RVA: 0x0007F4C2 File Offset: 0x0007D6C2
		public bool RequireCancellation
		{
			get
			{
				return this.requireCancellation;
			}
			set
			{
				this.requireCancellation = value;
			}
		}

		// Token: 0x17000892 RID: 2194
		// (get) Token: 0x060022BC RID: 8892 RVA: 0x0007F4CB File Offset: 0x0007D6CB
		// (set) Token: 0x060022BD RID: 8893 RVA: 0x0007F4D3 File Offset: 0x0007D6D3
		public bool CanRenewSession
		{
			get
			{
				return this.canRenewSession;
			}
			set
			{
				this.canRenewSession = value;
			}
		}

		// Token: 0x17000893 RID: 2195
		// (get) Token: 0x060022BE RID: 8894 RVA: 0x0007F4DC File Offset: 0x0007D6DC
		protected internal override bool SupportsClientAuthentication
		{
			get
			{
				return this.BootstrapSecurityCapabilities != null && this.BootstrapSecurityCapabilities.SupportsClientAuthentication;
			}
		}

		// Token: 0x17000894 RID: 2196
		// (get) Token: 0x060022BF RID: 8895 RVA: 0x0007F4F3 File Offset: 0x0007D6F3
		protected internal override bool SupportsServerAuthentication
		{
			get
			{
				return this.BootstrapSecurityCapabilities != null && this.BootstrapSecurityCapabilities.SupportsServerAuthentication;
			}
		}

		// Token: 0x17000895 RID: 2197
		// (get) Token: 0x060022C0 RID: 8896 RVA: 0x0007F50A File Offset: 0x0007D70A
		protected internal override bool SupportsClientWindowsIdentity
		{
			get
			{
				return this.BootstrapSecurityCapabilities != null && this.BootstrapSecurityCapabilities.SupportsClientWindowsIdentity;
			}
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x0007F521 File Offset: 0x0007D721
		protected override SecurityTokenParameters CloneCore()
		{
			return new SecureConversationSecurityTokenParameters(this);
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x0007F529 File Offset: 0x0007D729
		protected internal override SecurityKeyIdentifierClause CreateKeyIdentifierClause(SecurityToken token, SecurityTokenReferenceStyle referenceStyle)
		{
			if (token is GenericXmlSecurityToken)
			{
				return base.CreateGenericXmlTokenKeyIdentifierClause(token, referenceStyle);
			}
			return base.CreateKeyIdentifierClause<SecurityContextKeyIdentifierClause, LocalIdKeyIdentifierClause>(token, referenceStyle);
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x0007F544 File Offset: 0x0007D744
		protected internal override void InitializeSecurityTokenRequirement(SecurityTokenRequirement requirement)
		{
			requirement.TokenType = ServiceModelSecurityTokenTypes.SecureConversation;
			requirement.KeyType = SecurityKeyType.SymmetricKey;
			requirement.RequireCryptographicToken = true;
			requirement.Properties[ServiceModelSecurityTokenRequirement.SupportSecurityContextCancellationProperty] = this.RequireCancellation;
			requirement.Properties[ServiceModelSecurityTokenRequirement.SecureConversationSecurityBindingElementProperty] = this.BootstrapSecurityBindingElement;
			requirement.Properties[ServiceModelSecurityTokenRequirement.IssuerBindingContextProperty] = this.IssuerBindingContext.Clone();
			requirement.Properties[ServiceModelSecurityTokenRequirement.IssuedSecurityTokenParametersProperty] = base.Clone();
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x0007F5CC File Offset: 0x0007D7CC
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendLine(base.ToString());
			stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "RequireCancellation: {0}", new object[]
			{
				this.requireCancellation.ToString()
			}));
			if (this.bootstrapSecurityBindingElement == null)
			{
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "BootstrapSecurityBindingElement: null", new object[0]));
			}
			else
			{
				stringBuilder.AppendLine(string.Format(CultureInfo.InvariantCulture, "BootstrapSecurityBindingElement:", new object[0]));
				stringBuilder.AppendLine("  " + this.BootstrapSecurityBindingElement.ToString().Trim().Replace("\n", "\n  "));
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x04001FBD RID: 8125
		internal const bool defaultRequireCancellation = true;

		// Token: 0x04001FBE RID: 8126
		internal const bool defaultCanRenewSession = true;

		// Token: 0x04001FBF RID: 8127
		private SecurityBindingElement bootstrapSecurityBindingElement;

		// Token: 0x04001FC0 RID: 8128
		private ChannelProtectionRequirements bootstrapProtectionRequirements;

		// Token: 0x04001FC1 RID: 8129
		private bool requireCancellation;

		// Token: 0x04001FC2 RID: 8130
		private bool canRenewSession = true;

		// Token: 0x04001FC3 RID: 8131
		private BindingContext issuerBindingContext;
	}
}
