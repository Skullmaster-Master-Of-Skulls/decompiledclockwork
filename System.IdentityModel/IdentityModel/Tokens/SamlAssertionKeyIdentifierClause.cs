using System;
using System.Globalization;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200014D RID: 333
	public class SamlAssertionKeyIdentifierClause : SecurityKeyIdentifierClause
	{
		// Token: 0x060009F8 RID: 2552 RVA: 0x0002D1D0 File Offset: 0x0002B3D0
		public SamlAssertionKeyIdentifierClause(string assertionId) : this(assertionId, null, 0)
		{
		}

		// Token: 0x060009F9 RID: 2553 RVA: 0x0002D1DC File Offset: 0x0002B3DC
		public SamlAssertionKeyIdentifierClause(string assertionId, byte[] derivationNonce, int derivationLength) : this(assertionId, derivationNonce, derivationLength, null, null, null, null, null)
		{
		}

		// Token: 0x060009FA RID: 2554 RVA: 0x0002D1F8 File Offset: 0x0002B3F8
		internal SamlAssertionKeyIdentifierClause(string assertionId, byte[] derivationNonce, int derivationLength, string valueType, string tokenTypeUri, string binding, string location, string authorityKind) : base(null, derivationNonce, derivationLength)
		{
			if (assertionId == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertionId");
			}
			this.assertionId = assertionId;
			this.valueType = valueType;
			this.tokenTypeUri = tokenTypeUri;
			this.binding = binding;
			this.location = location;
			this.authorityKind = authorityKind;
		}

		// Token: 0x17000264 RID: 612
		// (get) Token: 0x060009FB RID: 2555 RVA: 0x0002D250 File Offset: 0x0002B450
		public string AssertionId
		{
			get
			{
				return this.assertionId;
			}
		}

		// Token: 0x17000265 RID: 613
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x0002D258 File Offset: 0x0002B458
		internal string TokenTypeUri
		{
			get
			{
				return this.tokenTypeUri;
			}
		}

		// Token: 0x17000266 RID: 614
		// (get) Token: 0x060009FD RID: 2557 RVA: 0x0002D260 File Offset: 0x0002B460
		internal string ValueType
		{
			get
			{
				return this.valueType;
			}
		}

		// Token: 0x17000267 RID: 615
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0002D268 File Offset: 0x0002B468
		internal string Binding
		{
			get
			{
				return this.binding;
			}
		}

		// Token: 0x17000268 RID: 616
		// (get) Token: 0x060009FF RID: 2559 RVA: 0x0002D270 File Offset: 0x0002B470
		internal string Location
		{
			get
			{
				return this.location;
			}
		}

		// Token: 0x17000269 RID: 617
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x0002D278 File Offset: 0x0002B478
		internal string AuthorityKind
		{
			get
			{
				return this.authorityKind;
			}
		}

		// Token: 0x06000A01 RID: 2561 RVA: 0x0002D280 File Offset: 0x0002B480
		public override bool Matches(SecurityKeyIdentifierClause keyIdentifierClause)
		{
			SamlAssertionKeyIdentifierClause samlAssertionKeyIdentifierClause = keyIdentifierClause as SamlAssertionKeyIdentifierClause;
			return this == samlAssertionKeyIdentifierClause || (samlAssertionKeyIdentifierClause != null && samlAssertionKeyIdentifierClause.Matches(this.assertionId));
		}

		// Token: 0x06000A02 RID: 2562 RVA: 0x0002D2AB File Offset: 0x0002B4AB
		public bool Matches(string assertionId)
		{
			return this.assertionId == assertionId;
		}

		// Token: 0x06000A03 RID: 2563 RVA: 0x0002D2B9 File Offset: 0x0002B4B9
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "SamlAssertionKeyIdentifierClause(AssertionId = '{0}')", new object[]
			{
				this.AssertionId
			});
		}

		// Token: 0x04000B97 RID: 2967
		private readonly string assertionId;

		// Token: 0x04000B98 RID: 2968
		private readonly string valueType;

		// Token: 0x04000B99 RID: 2969
		private readonly string tokenTypeUri;

		// Token: 0x04000B9A RID: 2970
		private readonly string binding;

		// Token: 0x04000B9B RID: 2971
		private readonly string location;

		// Token: 0x04000B9C RID: 2972
		private readonly string authorityKind;
	}
}
