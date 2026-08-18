using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000201 RID: 513
	internal class UnionFacetsChecker : FacetsChecker
	{
		// Token: 0x0600185D RID: 6237 RVA: 0x0006CD8C File Offset: 0x0006BD8C
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
			{
				return new XmlSchemaException("Sch_EnumerationConstraintFailed", string.Empty);
			}
			return null;
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0006CDD4 File Offset: 0x0006BDD4
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			foreach (object value2 in enumeration)
			{
				if (datatype.Compare(value, value2) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
