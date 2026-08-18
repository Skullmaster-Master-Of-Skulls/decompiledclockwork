using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000200 RID: 512
	internal class ListFacetsChecker : FacetsChecker
	{
		// Token: 0x0600185A RID: 6234 RVA: 0x0006CC70 File Offset: 0x0006BC70
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			Array array = value as Array;
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			if ((restrictionFlags & (RestrictionFlags.Length | RestrictionFlags.MinLength | RestrictionFlags.MaxLength)) != (RestrictionFlags)0)
			{
				int length = array.Length;
				if ((restrictionFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && restriction.Length != length)
				{
					return new XmlSchemaException("Sch_LengthConstraintFailed", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && length < restriction.MinLength)
				{
					return new XmlSchemaException("Sch_MinLengthConstraintFailed", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && restriction.MaxLength < length)
				{
					return new XmlSchemaException("Sch_MaxLengthConstraintFailed", string.Empty);
				}
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
			{
				return new XmlSchemaException("Sch_EnumerationConstraintFailed", string.Empty);
			}
			return null;
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0006CD28 File Offset: 0x0006BD28
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
