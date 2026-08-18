using System;
using System.Collections;
using System.Globalization;

namespace System.Xml.Schema
{
	// Token: 0x02000245 RID: 581
	internal class Numeric10FacetsChecker : FacetsChecker
	{
		// Token: 0x060022D4 RID: 8916 RVA: 0x000B90D7 File Offset: 0x000B72D7
		internal Numeric10FacetsChecker(decimal minVal, decimal maxVal)
		{
			this.minValue = minVal;
			this.maxValue = maxVal;
		}

		// Token: 0x060022D5 RID: 8917 RVA: 0x000B90F0 File Offset: 0x000B72F0
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			decimal value2 = datatype.ValueConverter.ToDecimal(value);
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x060022D6 RID: 8918 RVA: 0x000B9114 File Offset: 0x000B7314
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
			return this.CheckTotalAndFractionDigits(value, restriction.TotalDigits, restriction.FractionDigits, (restrictionFlags & RestrictionFlags.TotalDigits) > (RestrictionFlags)0, (restrictionFlags & RestrictionFlags.FractionDigits) > (RestrictionFlags)0);
		}

		// Token: 0x060022D7 RID: 8919 RVA: 0x000B928C File Offset: 0x000B748C
		internal override Exception CheckValueFacets(long value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x060022D8 RID: 8920 RVA: 0x000B92A8 File Offset: 0x000B74A8
		internal override Exception CheckValueFacets(int value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x060022D9 RID: 8921 RVA: 0x000B92C4 File Offset: 0x000B74C4
		internal override Exception CheckValueFacets(short value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x060022DA RID: 8922 RVA: 0x000B92E0 File Offset: 0x000B74E0
		internal override Exception CheckValueFacets(byte value, XmlSchemaDatatype datatype)
		{
			decimal value2 = value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x060022DB RID: 8923 RVA: 0x000B92FC File Offset: 0x000B74FC
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToDecimal(value), enumeration, datatype.ValueConverter);
		}

		// Token: 0x060022DC RID: 8924 RVA: 0x000B9318 File Offset: 0x000B7518
		internal bool MatchEnumeration(decimal value, ArrayList enumeration, XmlValueConverter valueConverter)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (value == valueConverter.ToDecimal(enumeration[i]))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060022DD RID: 8925 RVA: 0x000B9350 File Offset: 0x000B7550
		internal Exception CheckTotalAndFractionDigits(decimal value, int totalDigits, int fractionDigits, bool checkTotal, bool checkFraction)
		{
			decimal d = FacetsChecker.Power(10, totalDigits) - 1m;
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

		// Token: 0x04000EAE RID: 3758
		private static readonly char[] signs = new char[]
		{
			'+',
			'-'
		};

		// Token: 0x04000EAF RID: 3759
		private decimal maxValue;

		// Token: 0x04000EB0 RID: 3760
		private decimal minValue;
	}
}
