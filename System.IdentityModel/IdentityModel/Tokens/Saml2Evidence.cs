using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x0200013D RID: 317
	public class Saml2Evidence
	{
		// Token: 0x060008FD RID: 2301 RVA: 0x00024D5B File Offset: 0x00022F5B
		public Saml2Evidence()
		{
		}

		// Token: 0x060008FE RID: 2302 RVA: 0x00024D84 File Offset: 0x00022F84
		public Saml2Evidence(Saml2Assertion assertion)
		{
			if (assertion == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("assertion");
			}
			this.assertions.Add(assertion);
		}

		// Token: 0x060008FF RID: 2303 RVA: 0x00024DD8 File Offset: 0x00022FD8
		public Saml2Evidence(Saml2Id idReference)
		{
			if (idReference == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("idReference");
			}
			this.assertionIdReferences.Add(idReference);
		}

		// Token: 0x06000900 RID: 2304 RVA: 0x00024E2C File Offset: 0x0002302C
		public Saml2Evidence(Uri uriReference)
		{
			if (null == uriReference)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("uriReference");
			}
			this.assertionUriReferences.Add(uriReference);
		}

		// Token: 0x17000228 RID: 552
		// (get) Token: 0x06000901 RID: 2305 RVA: 0x00024E85 File Offset: 0x00023085
		public Collection<Saml2Id> AssertionIdReferences
		{
			get
			{
				return this.assertionIdReferences;
			}
		}

		// Token: 0x17000229 RID: 553
		// (get) Token: 0x06000902 RID: 2306 RVA: 0x00024E8D File Offset: 0x0002308D
		public Collection<Saml2Assertion> Assertions
		{
			get
			{
				return this.assertions;
			}
		}

		// Token: 0x1700022A RID: 554
		// (get) Token: 0x06000903 RID: 2307 RVA: 0x00024E95 File Offset: 0x00023095
		public Collection<Uri> AssertionUriReferences
		{
			get
			{
				return this.assertionUriReferences;
			}
		}

		// Token: 0x04000B52 RID: 2898
		private Collection<Saml2Id> assertionIdReferences = new Collection<Saml2Id>();

		// Token: 0x04000B53 RID: 2899
		private Collection<Saml2Assertion> assertions = new Collection<Saml2Assertion>();

		// Token: 0x04000B54 RID: 2900
		private AbsoluteUriCollection assertionUriReferences = new AbsoluteUriCollection();
	}
}
