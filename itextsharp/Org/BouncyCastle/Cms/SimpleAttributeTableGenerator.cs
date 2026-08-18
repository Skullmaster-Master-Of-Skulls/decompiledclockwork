using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200047B RID: 1147
	public class SimpleAttributeTableGenerator : CmsAttributeTableGenerator
	{
		// Token: 0x06002703 RID: 9987 RVA: 0x000EC720 File Offset: 0x000EB720
		public SimpleAttributeTableGenerator(AttributeTable attributes)
		{
			this.attributes = attributes;
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000EC72F File Offset: 0x000EB72F
		public virtual AttributeTable GetAttributes(IDictionary parameters)
		{
			return this.attributes;
		}

		// Token: 0x04001ACA RID: 6858
		private readonly AttributeTable attributes;
	}
}
