using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000132 RID: 306
	public class Saml2Advice
	{
		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060008A0 RID: 2208 RVA: 0x00024271 File Offset: 0x00022471
		public Collection<Saml2Id> AssertionIdReferences
		{
			get
			{
				return this.assertionIdReferences;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060008A1 RID: 2209 RVA: 0x00024279 File Offset: 0x00022479
		public Collection<Saml2Assertion> Assertions
		{
			get
			{
				return this.assertions;
			}
		}

		// Token: 0x17000202 RID: 514
		// (get) Token: 0x060008A2 RID: 2210 RVA: 0x00024281 File Offset: 0x00022481
		public Collection<Uri> AssertionUriReferences
		{
			get
			{
				return this.assertionUriReferences;
			}
		}

		// Token: 0x04000B27 RID: 2855
		private Collection<Saml2Id> assertionIdReferences = new Collection<Saml2Id>();

		// Token: 0x04000B28 RID: 2856
		private Collection<Saml2Assertion> assertions = new Collection<Saml2Assertion>();

		// Token: 0x04000B29 RID: 2857
		private AbsoluteUriCollection assertionUriReferences = new AbsoluteUriCollection();
	}
}
