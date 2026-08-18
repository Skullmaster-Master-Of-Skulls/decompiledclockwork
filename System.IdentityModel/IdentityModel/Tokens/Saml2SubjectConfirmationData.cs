using System;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000147 RID: 327
	public class Saml2SubjectConfirmationData
	{
		// Token: 0x17000247 RID: 583
		// (get) Token: 0x060009AA RID: 2474 RVA: 0x0002B8A8 File Offset: 0x00029AA8
		// (set) Token: 0x060009AB RID: 2475 RVA: 0x0002B8B0 File Offset: 0x00029AB0
		public string Address
		{
			get
			{
				return this.address;
			}
			set
			{
				this.address = XmlUtil.NormalizeEmptyString(value);
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x060009AC RID: 2476 RVA: 0x0002B8BE File Offset: 0x00029ABE
		// (set) Token: 0x060009AD RID: 2477 RVA: 0x0002B8C6 File Offset: 0x00029AC6
		public Saml2Id InResponseTo
		{
			get
			{
				return this.inResponseTo;
			}
			set
			{
				this.inResponseTo = value;
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x060009AE RID: 2478 RVA: 0x0002B8CF File Offset: 0x00029ACF
		public Collection<SecurityKeyIdentifier> KeyIdentifiers
		{
			get
			{
				return this.keyIdentifiers;
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x060009AF RID: 2479 RVA: 0x0002B8D7 File Offset: 0x00029AD7
		// (set) Token: 0x060009B0 RID: 2480 RVA: 0x0002B8DF File Offset: 0x00029ADF
		public DateTime? NotBefore
		{
			get
			{
				return this.notBefore;
			}
			set
			{
				this.notBefore = DateTimeUtil.ToUniversalTime(value);
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x060009B1 RID: 2481 RVA: 0x0002B8ED File Offset: 0x00029AED
		// (set) Token: 0x060009B2 RID: 2482 RVA: 0x0002B8F5 File Offset: 0x00029AF5
		public DateTime? NotOnOrAfter
		{
			get
			{
				return this.notOnOrAfter;
			}
			set
			{
				this.notOnOrAfter = DateTimeUtil.ToUniversalTime(value);
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x060009B3 RID: 2483 RVA: 0x0002B903 File Offset: 0x00029B03
		// (set) Token: 0x060009B4 RID: 2484 RVA: 0x0002B90B File Offset: 0x00029B0B
		public Uri Recipient
		{
			get
			{
				return this.recipient;
			}
			set
			{
				if (null != value && !value.IsAbsoluteUri)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID0013"));
				}
				this.recipient = value;
			}
		}

		// Token: 0x04000B72 RID: 2930
		private string address;

		// Token: 0x04000B73 RID: 2931
		private Saml2Id inResponseTo;

		// Token: 0x04000B74 RID: 2932
		private Collection<SecurityKeyIdentifier> keyIdentifiers = new Collection<SecurityKeyIdentifier>();

		// Token: 0x04000B75 RID: 2933
		private DateTime? notBefore;

		// Token: 0x04000B76 RID: 2934
		private DateTime? notOnOrAfter;

		// Token: 0x04000B77 RID: 2935
		private Uri recipient;
	}
}
