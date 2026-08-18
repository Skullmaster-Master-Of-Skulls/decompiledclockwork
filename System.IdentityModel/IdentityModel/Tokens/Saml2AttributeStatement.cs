using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Tokens
{
	// Token: 0x02000136 RID: 310
	public class Saml2AttributeStatement : Saml2Statement
	{
		// Token: 0x060008CD RID: 2253 RVA: 0x00024768 File Offset: 0x00022968
		public Saml2AttributeStatement()
		{
		}

		// Token: 0x060008CE RID: 2254 RVA: 0x0002477B File Offset: 0x0002297B
		public Saml2AttributeStatement(Saml2Attribute attribute) : this(new Saml2Attribute[]
		{
			attribute
		})
		{
		}

		// Token: 0x060008CF RID: 2255 RVA: 0x00024790 File Offset: 0x00022990
		public Saml2AttributeStatement(IEnumerable<Saml2Attribute> attributes)
		{
			if (attributes == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("attributes");
			}
			foreach (Saml2Attribute saml2Attribute in attributes)
			{
				if (saml2Attribute == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("attributes");
				}
				this.attributes.Add(saml2Attribute);
			}
		}

		// Token: 0x17000215 RID: 533
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00024814 File Offset: 0x00022A14
		public Collection<Saml2Attribute> Attributes
		{
			get
			{
				return this.attributes;
			}
		}

		// Token: 0x04000B3C RID: 2876
		private Collection<Saml2Attribute> attributes = new Collection<Saml2Attribute>();
	}
}
