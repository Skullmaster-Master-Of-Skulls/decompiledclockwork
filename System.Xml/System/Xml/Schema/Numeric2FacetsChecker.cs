using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001F9 RID: 505
	internal class Numeric2FacetsChecker : FacetsChecker
	{
		// Token: 0x06001837 RID: 6199 RVA: 0x0006C1CC File Offset: 0x0006B1CC
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			double value2 = datatype.ValueConverter.ToDouble(value);
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06001838 RID: 6200 RVA: 0x0006C1F0 File Offset: 0x0006B1F0
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

		// Token: 0x06001839 RID: 6201 RVA: 0x0006C2DC File Offset: 0x0006B2DC
		internal override Exception CheckValueFacets(float value, XmlSchemaDatatype datatype)
		{
			double value2 = (double)value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x0600183A RID: 6202 RVA: 0x0006C2F4 File Offset: 0x0006B2F4
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDouble(value), enumeration, datatype.ValueConverter);
		}

		// Token: 0x0600183B RID: 6203 RVA: 0x0006C310 File Offset: 0x0006B310
		private bool MatchEnumeration(double value, ArrayList enumeration, XmlValueConverter valueConverter)
		{
			foreach (object value2 in enumeration)
			{
				if (value == valueConverter.ToDouble(value2))
				{
					return true;
				}
			}
			return false;
		}
	}
}
