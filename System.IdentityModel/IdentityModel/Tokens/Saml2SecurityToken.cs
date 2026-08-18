using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000142 RID: 322
	public class Saml2SecurityToken : SecurityToken
	{
		// Token: 0x0600091F RID: 2335 RVA: 0x0002511F File Offset: 0x0002331F
		public Saml2SecurityToken(Saml2Assertion assertion) : this(assertion, EmptyReadOnlyCollection<SecurityKey>.Instance, null)
		{
		}

		// Token: 0x06000920 RID: 2336 RVA: 0x00025130 File Offset: 0x00023330
		public Saml2SecurityToken(Saml2Assertion assertion, ReadOnlyCollection<SecurityKey> keys, SecurityToken issuerToken)
		{
			if (assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertion");
			}
			if (keys == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("keys");
			}
			this.assertion = assertion;
			this.keys = keys;
			this.issuerToken = issuerToken;
		}

		// Token: 0x17000236 RID: 566
		// (get) Token: 0x06000921 RID: 2337 RVA: 0x0002517E File Offset: 0x0002337E
		public Saml2Assertion Assertion
		{
			get
			{
				return this.assertion;
			}
		}

		// Token: 0x17000237 RID: 567
		// (get) Token: 0x06000922 RID: 2338 RVA: 0x00025186 File Offset: 0x00023386
		public override string Id
		{
			get
			{
				return this.assertion.Id.Value;
			}
		}

		// Token: 0x17000238 RID: 568
		// (get) Token: 0x06000923 RID: 2339 RVA: 0x00025198 File Offset: 0x00023398
		public SecurityToken IssuerToken
		{
			get
			{
				return this.issuerToken;
			}
		}

		// Token: 0x17000239 RID: 569
		// (get) Token: 0x06000924 RID: 2340 RVA: 0x000251A0 File Offset: 0x000233A0
		public override ReadOnlyCollection<SecurityKey> SecurityKeys
		{
			get
			{
				return this.keys;
			}
		}

		// Token: 0x1700023A RID: 570
		// (get) Token: 0x06000925 RID: 2341 RVA: 0x000251A8 File Offset: 0x000233A8
		public override DateTime ValidFrom
		{
			get
			{
				if (this.assertion.Conditions != null && this.assertion.Conditions.NotBefore != null)
				{
					return this.assertion.Conditions.NotBefore.Value;
				}
				return DateTime.MinValue;
			}
		}

		// Token: 0x1700023B RID: 571
		// (get) Token: 0x06000926 RID: 2342 RVA: 0x000251FC File Offset: 0x000233FC
		public override DateTime ValidTo
		{
			get
			{
				if (this.assertion.Conditions != null && this.assertion.Conditions.NotOnOrAfter != null)
				{
					return this.assertion.Conditions.NotOnOrAfter.Value;
				}
				return DateTime.MaxValue;
			}
		}

		// Token: 0x06000927 RID: 2343 RVA: 0x0002524E File Offset: 0x0002344E
		public override bool MatchesKeyIdentifierClause(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			return Saml2AssertionKeyIdentifierClause.Matches(this.Id, keyIdentifierClause) || base.MatchesKeyIdentifierClause(keyIdentifierClause);
		}

		// Token: 0x06000928 RID: 2344 RVA: 0x00025267 File Offset: 0x00023467
		public override bool CanCreateKeyIdentifierClause<T>()
		{
			return typeof(T) == typeof(Saml2AssertionKeyIdentifierClause) || base.CanCreateKeyIdentifierClause<T>();
		}

		// Token: 0x06000929 RID: 2345 RVA: 0x0002528C File Offset: 0x0002348C
		public override T CreateKeyIdentifierClause<T>()
		{
			if (typeof(T) == typeof(Saml2AssertionKeyIdentifierClause))
			{
				return new Saml2AssertionKeyIdentifierClause(this.assertion.Id.Value) as T;
			}
			if (typeof(T) == typeof(SamlAssertionKeyIdentifierClause))
			{
				return new WrappedSaml2AssertionKeyIdentifierClause(new Saml2AssertionKeyIdentifierClause(this.assertion.Id.Value)) as T;
			}
			return base.CreateKeyIdentifierClause<T>();
		}

		// Token: 0x04000B60 RID: 2912
		private Saml2Assertion assertion;

		// Token: 0x04000B61 RID: 2913
		private ReadOnlyCollection<SecurityKey> keys;

		// Token: 0x04000B62 RID: 2914
		private SecurityToken issuerToken;
	}
}
