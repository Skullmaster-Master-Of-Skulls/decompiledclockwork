using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000247 RID: 583
	internal class DurationFacetsChecker : FacetsChecker
	{
		// Token: 0x060022E5 RID: 8933 RVA: 0x000B957C File Offset: 0x000B777C
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			TimeSpan value2 = (TimeSpan)datatype.ValueConverter.ChangeType(value, typeof(TimeSpan));
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x060022E6 RID: 8934 RVA: 0x000B95B0 File Offset: 0x000B77B0
		internal override Exception CheckValueFacets(TimeSpan value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MaxInclusive) > 0)
			{
				return new XmlSchemaException("Sch_MaxInclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MaxExclusive) >= 0)
			{
				return new XmlSchemaException("Sch_MaxExclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MinInclusive) < 0)
			{
				return new XmlSchemaException("Sch_MinInclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && TimeSpan.Compare(value, (TimeSpan)restriction.MinExclusive) <= 0)
			{
				return new XmlSchemaException("Sch_MinExclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration))
			{
				return new XmlSchemaException("Sch_EnumerationConstraintFailed", string.Empty);
			}
			return null;
		}

		// Token: 0x060022E7 RID: 8935 RVA: 0x000B96A8 File Offset: 0x000B78A8
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((TimeSpan)value, enumeration);
		}

		// Token: 0x060022E8 RID: 8936 RVA: 0x000B96B8 File Offset: 0x000B78B8
		private bool MatchEnumeration(TimeSpan value, ArrayList enumeration)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (TimeSpan.Compare(value, (TimeSpan)enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
