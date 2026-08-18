using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200024E RID: 590
	internal class UnionFacetsChecker : FacetsChecker
	{
		// Token: 0x06002305 RID: 8965 RVA: 0x000B9E78 File Offset: 0x000B8078
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

		// Token: 0x06002306 RID: 8966 RVA: 0x000B9EC0 File Offset: 0x000B80C0
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
