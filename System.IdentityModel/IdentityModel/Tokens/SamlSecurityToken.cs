using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200015E RID: 350
	public class SamlSecurityToken : SecurityToken
	{
		// Token: 0x06000AA3 RID: 2723 RVA: 0x000304B0 File Offset: 0x0002E6B0
		protected SamlSecurityToken()
		{
		}

		// Token: 0x06000AA4 RID: 2724 RVA: 0x000304B8 File Offset: 0x0002E6B8
		public SamlSecurityToken(SamlAssertion assertion)
		{
			this.Initialize(assertion);
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000304C7 File Offset: 0x0002E6C7
		protected void Initialize(SamlAssertion assertion)
		{
			if (assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertion");
			}
			this.assertion = assertion;
			this.assertion.MakeReadOnly();
		}

		// Token: 0x170002A5 RID: 677
		// (get) Token: 0x06000AA6 RID: 2726 RVA: 0x000304EE File Offset: 0x0002E6EE
		public override string Id
		{
			get
			{
				return this.assertion.AssertionId;
			}
		}

		// Token: 0x170002A6 RID: 678
		// (get) Token: 0x06000AA7 RID: 2727 RVA: 0x000304FB File Offset: 0x0002E6FB
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return this.assertion.SecurityKeys;
			}
		}

		// Token: 0x170002A7 RID: 679
		// (get) Token: 0x06000AA8 RID: 2728 RVA: 0x00030508 File Offset: 0x0002E708
		public SamlAssertion Assertion
		{
			get
			{
				return this.assertion;
			}
		}

		// Token: 0x170002A8 RID: 680
		// (get) Token: 0x06000AA9 RID: 2729 RVA: 0x00030510 File Offset: 0x0002E710
		public override DateTime ValidFrom
		{
			get
			{
				if (this.assertion.Conditions != null)
				{
					return this.assertion.Conditions.NotBefore;
				}
				return SecurityUtils.MinUtcDateTime;
			}
		}

		// Token: 0x170002A9 RID: 681
		// (get) Token: 0x06000AAA RID: 2730 RVA: 0x00030535 File Offset: 0x0002E735
		public override DateTime ValidTo
		{
			get
			{
				if (this.assertion.Conditions != null)
				{
					return this.assertion.Conditions.NotOnOrAfter;
				}
				return SecurityUtils.MaxUtcDateTime;
			}
		}

		// Token: 0x06000AAB RID: 2731 RVA: 0x0003055A File Offset: 0x0002E75A
		public override bool CanCreateKeyIdentifierClause<T>()
		{
			return typeof(T) == typeof(SamlAssertionKeyIdentifierClause);
		}

		// Token: 0x06000AAC RID: 2732 RVA: 0x0003057C File Offset: 0x0002E77C
		public override T CreateKeyIdentifierClause<T>()
		{
			if (typeof(T) == typeof(SamlAssertionKeyIdentifierClause))
			{
				return new SamlAssertionKeyIdentifierClause(this.Id) as T;
			}
			throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidOperationException(SR.GetString("UnableToCreateTokenReference")));
		}

		// Token: 0x06000AAD RID: 2733 RVA: 0x000305D4 File Offset: 0x0002E7D4
		public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			SamlAssertionKeyIdentifierClause samlAssertionKeyIdentifierClause = keyIdentifierClause as SamlAssertionKeyIdentifierClause;
			return samlAssertionKeyIdentifierClause != null && samlAssertionKeyIdentifierClause.Matches(this.Id);
		}

		// Token: 0x04000BD1 RID: 3025
		private SamlAssertion assertion;
	}
}
