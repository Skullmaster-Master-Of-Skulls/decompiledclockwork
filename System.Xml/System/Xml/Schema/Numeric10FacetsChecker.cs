using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x020001F8 RID: 504
	internal class Numeric10FacetsChecker : FacetsChecker
	{
		// Token: 0x0600182C RID: 6188 RVA: 0x0006BE72 File Offset: 0x0006AE72
		internal Numeric10FacetsChecker(decimal minVal, decimal maxVal)
		{
			this.minValue = minVal;
			this.maxValue = maxVal;
		}

		// Token: 0x0600182D RID: 6189 RVA: 0x0006BE88 File Offset: 0x0006AE88
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			decimal value2 = datatype.ValueConverter.ToDecimal(value);
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x0600182E RID: 6190 RVA: 0x0006BEAC File Offset: 0x0006AEAC
		internal override Exception CheckValueFacets(decimal value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			XmlValueConverter valueConverter = datatype.ValueConverter;
			if (value > this.maxValue || value < this.minValue)
			{
				return new OverflowException(Res.GetString("XmlConvert_Overflow", new object[]
				{
					value.ToString(CultureInfo.InvariantCulture),
					datatype.TypeCodeString
				}));
			}
			if (restrictionFlags == (RestrictionFlags)0)
			{
				return null;
			}
			if ((restrictionFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && value > valueConverter.ToDecimal(restriction.MaxInclusive))
			{
				return new XmlSchemaException("Sch_MaxInclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && value >= valueConverter.ToDecimal(restriction.MaxExclusive))
			{
				return new XmlSchemaException("Sch_MaxExclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && value < valueConverter.ToDecimal(restriction.MinInclusive))
			{
				return new XmlSchemaException("Sch_MinInclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && value <= valueConverter.ToDecimal(restriction.MinExclusive))
			{
				return new XmlSchemaException("Sch_MinExclusiveConstraintFailed", string.Empty);
			}
			if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, valueConverter))
			{
				return new XmlSchemaException("Sch_EnumerationConstraintFailed", string.Empty);
			}
			return this.CheckTotalAndFractionDigits(value, restriction.TotalDigits, restriction.FractionDigits, (restrictionFlags & RestrictionFlags.TotalDigits) != (RestrictionFlags)0, (restrictionFlags & RestrictionFlags.FractionDigits) != (RestrictionFlags)0);
		}

		// Token: 0x0600182F RID: 6191 RVA: 0x0006C02C File Offset: 0x0006B02C
		internal override Exception CheckValueFacets(long value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06001830 RID: 6192 RVA: 0x0006C048 File Offset: 0x0006B048
		internal override Exception CheckValueFacets(int value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06001831 RID: 6193 RVA: 0x0006C064 File Offset: 0x0006B064
		internal override Exception CheckValueFacets(short value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06001832 RID: 6194 RVA: 0x0006C080 File Offset: 0x0006B080
		internal override Exception CheckValueFacets(byte value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06001833 RID: 6195 RVA: 0x0006C09C File Offset: 0x0006B09C
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDecimal(value), enumeration, datatype.ValueConverter);
		}

		// Token: 0x06001834 RID: 6196 RVA: 0x0006C0B8 File Offset: 0x0006B0B8
		internal bool MatchEnumeration(decimal value, ArrayList enumeration, XmlValueConverter valueConverter)
		{
			foreach (object value2 in enumeration)
			{
				if (value == valueConverter.ToDecimal(value2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06001835 RID: 6197 RVA: 0x0006C118 File Offset: 0x0006B118
		internal Exception CheckTotalAndFractionDigits(decimal value, int totalDigits, int fractionDigits, bool checkTotal, bool checkFraction)
		{
			decimal d = --FacetsChecker.Power(10, totalDigits);
			int num = 0;
			if (value < 0m)
			{
				value = decimal.Negate(value);
			}
			while (decimal.Truncate(value) != value)
			{
				value *= 10m;
				num++;
			}
			if (checkTotal && (value > d || num > totalDigits))
			{
				return new XmlSchemaException("Sch_TotalDigitsConstraintFailed", string.Empty);
			}
			if (checkFraction && num > fractionDigits)
			{
				return new XmlSchemaException("Sch_FractionDigitsConstraintFailed", string.Empty);
			}
			return null;
		}

		// Token: 0x04000E4D RID: 3661
		private static readonly char[] signs = new char[]
		{
			'+',
			'-'
		};

		// Token: 0x04000E4E RID: 3662
		private decimal maxValue;

		// Token: 0x04000E4F RID: 3663
		private decimal minValue;
	}
}
