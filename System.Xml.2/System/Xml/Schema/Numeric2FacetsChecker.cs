using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x02000246 RID: 582
	internal class Numeric2FacetsChecker : FacetsChecker
	{
		// Token: 0x060022DF RID: 8927 RVA: 0x000B93FC File Offset: 0x000B75FC
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			double value2 = datatype.ValueConverter.ToDouble(value);
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x060022E0 RID: 8928 RVA: 0x000B9420 File Offset: 0x000B7620
		internal override Exception CheckValueFacets(double value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			XmlValueConverter valueConverter = datatype.ValueConverter;
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && value > valueConverter.ToDouble(restriction.MaxInclusive))
			{
				return new XmlSchemaException("Sch_MaxInclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && value >= valueConverter.ToDouble(restriction.MaxExclusive))
			{
				return new XmlSchemaException("Sch_MaxExclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && value < valueConverter.ToDouble(restriction.MinInclusive))
			{
				return new XmlSchemaException("Sch_MinInclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && value <= valueConverter.ToDouble(restriction.MinExclusive))
			{
				return new XmlSchemaException("Sch_MinExclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, valueConverter))
			{
				return new XmlSchemaException("Sch_EnumerationConstraintFailed", string.Empty);
			}
			return null;
		}

		// Token: 0x060022E1 RID: 8929 RVA: 0x000B950C File Offset: 0x000B770C
		internal override Exception CheckValueFacets(float value, XmlSchemaDatatype datatype)
		{
			double value2 = (double)value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x060022E2 RID: 8930 RVA: 0x000B9524 File Offset: 0x000B7724
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDouble(value), enumeration, datatype.ValueConverter);
		}

		// Token: 0x060022E3 RID: 8931 RVA: 0x000B9540 File Offset: 0x000B7740
		private bool MatchEnumeration(double value, ArrayList enumeration, XmlValueConverter valueConverter)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (value == valueConverter.ToDouble(enumeration[i]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
