using System;
using System.Collections;
using Org.BouncyCastle.Asn1.Cms;

namespace Org.BouncyCastle.Cms
{
	// Token: 0x0200047A RID: 1146
	public interface CmsAttributeTableGenerator
	{
		// Token: 0x06002702 RID: 9986
		AttributeTable GetAttributes(IDictionary parameters);
	}
}
