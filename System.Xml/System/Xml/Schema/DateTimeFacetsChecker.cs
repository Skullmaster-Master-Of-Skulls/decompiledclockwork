using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001FB RID: 507
	internal class DateTimeFacetsChecker : FacetsChecker
	{
		// Token: 0x06001842 RID: 6210 RVA: 0x0006C518 File Offset: 0x0006B518
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			DateTime value2 = datatype.ValueConverter.ToDateTime(value);
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x0006C53C File Offset: 0x0006B53C
		internal override Exception CheckValueFacets(DateTime value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MaxInclusive) > 0)
			{
				return new XmlSchemaException("Sch_MaxInclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MaxExclusive) >= 0)
			{
				return new XmlSchemaException("Sch_MaxExclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MinInclusive) < 0)
			{
				return new XmlSchemaException("Sch_MinInclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && datatype.Compare(value, (DateTime)restriction.MinExclusive) <= 0)
			{
				return new XmlSchemaException("Sch_MinExclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
			{
				return new XmlSchemaException("Sch_EnumerationConstraintFailed", string.Empty);
			}
			return null;
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x0006C661 File Offset: 0x0006B661
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDateTime(value), enumeration, datatype);
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x0006C678 File Offset: 0x0006B678
		private bool MatchEnumeration(DateTime value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			foreach (object obj in enumeration)
			{
				DateTime dateTime = (DateTime)obj;
				if (datatype.Compare(value, dateTime) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
