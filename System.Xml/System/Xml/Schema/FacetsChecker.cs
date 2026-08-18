using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Xml.Schema
{
	// Token: 0x020001F5 RID: 501
	internal abstract class FacetsChecker
	{
		// Token: 0x060017FF RID: 6143 RVA: 0x0006A517 File Offset: 0x00069517
		internal virtual Exception CheckLexicalFacets(ref string parseString, XmlSchemaDatatype datatype)
		{
			this.CheckWhitespaceFacets(ref parseString, datatype);
			return this.CheckPatternFacets(datatype.Restriction, parseString);
		}

		// Token: 0x06001800 RID: 6144 RVA: 0x0006A52F File Offset: 0x0006952F
		internal virtual Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06001801 RID: 6145 RVA: 0x0006A532 File Offset: 0x00069532
		internal virtual Exception CheckValueFacets(decimal value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06001802 RID: 6146 RVA: 0x0006A535 File Offset: 0x00069535
		internal virtual Exception CheckValueFacets(long value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06001803 RID: 6147 RVA: 0x0006A538 File Offset: 0x00069538
		internal virtual Exception CheckValueFacets(int value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06001804 RID: 6148 RVA: 0x0006A53B File Offset: 0x0006953B
		internal virtual Exception CheckValueFacets(short value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06001805 RID: 6149 RVA: 0x0006A53E File Offset: 0x0006953E
		internal virtual Exception CheckValueFacets(byte value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06001806 RID: 6150 RVA: 0x0006A541 File Offset: 0x00069541
		internal virtual Exception CheckValueFacets(DateTime value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06001807 RID: 6151 RVA: 0x0006A544 File Offset: 0x00069544
		internal virtual Exception CheckValueFacets(double value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06001808 RID: 6152 RVA: 0x0006A547 File Offset: 0x00069547
		internal virtual Exception CheckValueFacets(float value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x06001809 RID: 6153 RVA: 0x0006A54A File Offset: 0x0006954A
		internal virtual Exception CheckValueFacets(string value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x0600180A RID: 6154 RVA: 0x0006A54D File Offset: 0x0006954D
		internal virtual Exception CheckValueFacets(byte[] value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x0600180B RID: 6155 RVA: 0x0006A550 File Offset: 0x00069550
		internal virtual Exception CheckValueFacets(TimeSpan value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x0600180C RID: 6156 RVA: 0x0006A553 File Offset: 0x00069553
		internal virtual Exception CheckValueFacets(XmlQualifiedName value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x0600180D RID: 6157 RVA: 0x0006A558 File Offset: 0x00069558
		internal void CheckWhitespaceFacets(ref string s, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			switch (datatype.Variety)
			{
			case XmlSchemaDatatypeVariety.Atomic:
				if (datatype.BuiltInWhitespaceFacet == XmlSchemaWhiteSpace.Collapse)
				{
					s = XmlComplianceUtil.NonCDataNormalize(s);
					return;
				}
				if (datatype.BuiltInWhitespaceFacet == XmlSchemaWhiteSpace.Replace)
				{
					s = XmlComplianceUtil.CDataNormalize(s);
					return;
				}
				if (restriction != null && (restriction.Flags & RestrictionFlags.WhiteSpace) != (RestrictionFlags)0)
				{
					if (restriction.WhiteSpace == XmlSchemaWhiteSpace.Replace)
					{
						s = XmlComplianceUtil.CDataNormalize(s);
						return;
					}
					if (restriction.WhiteSpace == XmlSchemaWhiteSpace.Collapse)
					{
						s = XmlComplianceUtil.NonCDataNormalize(s);
					}
				}
				return;
			case XmlSchemaDatatypeVariety.List:
				s = s.Trim();
				return;
			default:
				return;
			}
		}

		// Token: 0x0600180E RID: 6158 RVA: 0x0006A5E8 File Offset: 0x000695E8
		internal Exception CheckPatternFacets(RestrictionFacets restriction, string value)
		{
			if (restriction != null && (restriction.Flags & RestrictionFlags.Pattern) != (RestrictionFlags)0)
			{
				foreach (object obj in restriction.Patterns)
				{
					Regex regex = (Regex)obj;
					if (!regex.IsMatch(value))
					{
						return new XmlSchemaException("Sch_PatternConstraintFailed", string.Empty);
					}
				}
			}
			return null;
		}

		// Token: 0x0600180F RID: 6159 RVA: 0x0006A668 File Offset: 0x00069668
		internal virtual bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return false;
		}

		// Token: 0x06001810 RID: 6160 RVA: 0x0006A66C File Offset: 0x0006966C
		internal virtual RestrictionFacets ConstructRestriction(DatatypeImplementation datatype, XmlSchemaObjectCollection facets, XmlNameTable nameTable)
		{
			RestrictionFacets restrictionFacets = new RestrictionFacets();
			FacetsChecker.FacetsCompiler facetsCompiler = new FacetsChecker.FacetsCompiler(datatype, restrictionFacets);
			foreach (XmlSchemaObject xmlSchemaObject in facets)
			{
				XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)xmlSchemaObject;
				if (xmlSchemaFacet.Value == null)
				{
					throw new XmlSchemaException("Sch_InvalidFacet", xmlSchemaFacet);
				}
				IXmlNamespaceResolver nsmgr = new SchemaNamespaceManager(xmlSchemaFacet);
				switch (xmlSchemaFacet.FacetType)
				{
				case FacetType.Length:
					facetsCompiler.CompileLengthFacet(xmlSchemaFacet);
					break;
				case FacetType.MinLength:
					facetsCompiler.CompileMinLengthFacet(xmlSchemaFacet);
					break;
				case FacetType.MaxLength:
					facetsCompiler.CompileMaxLengthFacet(xmlSchemaFacet);
					break;
				case FacetType.Pattern:
					facetsCompiler.CompilePatternFacet(xmlSchemaFacet as XmlSchemaPatternFacet);
					break;
				case FacetType.Whitespace:
					facetsCompiler.CompileWhitespaceFacet(xmlSchemaFacet);
					break;
				case FacetType.Enumeration:
					facetsCompiler.CompileEnumerationFacet(xmlSchemaFacet, nsmgr, nameTable);
					break;
				case FacetType.MinExclusive:
					facetsCompiler.CompileMinExclusiveFacet(xmlSchemaFacet);
					break;
				case FacetType.MinInclusive:
					facetsCompiler.CompileMinInclusiveFacet(xmlSchemaFacet);
					break;
				case FacetType.MaxExclusive:
					facetsCompiler.CompileMaxExclusiveFacet(xmlSchemaFacet);
					break;
				case FacetType.MaxInclusive:
					facetsCompiler.CompileMaxInclusiveFacet(xmlSchemaFacet);
					break;
				case FacetType.TotalDigits:
					facetsCompiler.CompileTotalDigitsFacet(xmlSchemaFacet);
					break;
				case FacetType.FractionDigits:
					facetsCompiler.CompileFractionDigitsFacet(xmlSchemaFacet);
					break;
				default:
					throw new XmlSchemaException("Sch_UnknownFacet", xmlSchemaFacet);
				}
			}
			facetsCompiler.FinishFacetCompile();
			facetsCompiler.CompileFacetCombinations();
			return restrictionFacets;
		}

		// Token: 0x06001811 RID: 6161 RVA: 0x0006A7E0 File Offset: 0x000697E0
		internal static decimal Power(int x, int y)
		{
			decimal num = 1m;
			decimal d = x;
			if (y > 28)
			{
				return decimal.MaxValue;
			}
			for (int i = 0; i < y; i++)
			{
				num *= d;
			}
			return num;
		}

		// Token: 0x020001F6 RID: 502
		private struct FacetsCompiler
		{
			// Token: 0x06001813 RID: 6163 RVA: 0x0006A828 File Offset: 0x00069828
			public FacetsCompiler(DatatypeImplementation baseDatatype, RestrictionFacets restriction)
			{
				this.firstPattern = true;
				this.regStr = null;
				this.pattern_facet = null;
				this.datatype = baseDatatype;
				this.derivedRestriction = restriction;
				this.baseFlags = ((this.datatype.Restriction != null) ? this.datatype.Restriction.Flags : ((RestrictionFlags)0));
				this.baseFixedFlags = ((this.datatype.Restriction != null) ? this.datatype.Restriction.FixedFlags : ((RestrictionFlags)0));
				this.validRestrictionFlags = this.datatype.ValidRestrictionFlags;
				this.nonNegativeInt = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.NonNegativeInteger).Datatype;
				this.builtInEnum = ((!(this.datatype is Datatype_union) && !(this.datatype is Datatype_List)) ? this.datatype.TypeCode : XmlTypeCode.None);
				this.builtInType = ((this.builtInEnum > XmlTypeCode.None) ? DatatypeImplementation.GetSimpleTypeFromTypeCode(this.builtInEnum).Datatype : this.datatype);
			}

			// Token: 0x06001814 RID: 6164 RVA: 0x0006A91C File Offset: 0x0006991C
			internal void CompileLengthFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.Length, "Sch_LengthFacetProhibited");
				this.CheckDupFlag(facet, RestrictionFlags.Length, "Sch_DupLengthFacet");
				this.derivedRestriction.Length = XmlBaseConverter.DecimalToInt32((decimal)this.ParseFacetValue(this.nonNegativeInt, facet, "Sch_LengthFacetInvalid", null, null));
				if ((this.baseFixedFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.Length, this.derivedRestriction.Length))
				{
					throw new XmlSchemaException("Sch_FacetBaseFixed", facet);
				}
				if ((this.baseFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && this.datatype.Restriction.Length < this.derivedRestriction.Length)
				{
					throw new XmlSchemaException("Sch_LengthGtBaseLength", facet);
				}
				if (((this.baseFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 || (this.baseFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0) && (this.datatype.Restriction.MaxLength < this.derivedRestriction.Length || this.datatype.Restriction.MinLength > this.derivedRestriction.Length))
				{
					throw new XmlSchemaException("Sch_MaxMinLengthBaseLength", facet);
				}
				this.SetFlag(facet, RestrictionFlags.Length);
			}

			// Token: 0x06001815 RID: 6165 RVA: 0x0006AA4C File Offset: 0x00069A4C
			internal void CompileMinLengthFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MinLength, "Sch_MinLengthFacetProhibited");
				this.CheckDupFlag(facet, RestrictionFlags.MinLength, "Sch_DupMinLengthFacet");
				this.derivedRestriction.MinLength = XmlBaseConverter.DecimalToInt32((decimal)this.ParseFacetValue(this.nonNegativeInt, facet, "Sch_MinLengthFacetInvalid", null, null));
				if ((this.baseFixedFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MinLength, this.derivedRestriction.MinLength))
				{
					throw new XmlSchemaException("Sch_FacetBaseFixed", facet);
				}
				if ((this.baseFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && this.datatype.Restriction.MinLength > this.derivedRestriction.MinLength)
				{
					throw new XmlSchemaException("Sch_MinLengthGtBaseMinLength", facet);
				}
				if ((this.baseFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && this.datatype.Restriction.Length < this.derivedRestriction.MinLength)
				{
					throw new XmlSchemaException("Sch_MaxMinLengthBaseLength", facet);
				}
				this.SetFlag(facet, RestrictionFlags.MinLength);
			}

			// Token: 0x06001816 RID: 6166 RVA: 0x0006AB54 File Offset: 0x00069B54
			internal void CompileMaxLengthFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MaxLength, "Sch_MaxLengthFacetProhibited");
				this.CheckDupFlag(facet, RestrictionFlags.MaxLength, "Sch_DupMaxLengthFacet");
				this.derivedRestriction.MaxLength = XmlBaseConverter.DecimalToInt32((decimal)this.ParseFacetValue(this.nonNegativeInt, facet, "Sch_MaxLengthFacetInvalid", null, null));
				if ((this.baseFixedFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MaxLength, this.derivedRestriction.MaxLength))
				{
					throw new XmlSchemaException("Sch_FacetBaseFixed", facet);
				}
				if ((this.baseFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && this.datatype.Restriction.MaxLength < this.derivedRestriction.MaxLength)
				{
					throw new XmlSchemaException("Sch_MaxLengthGtBaseMaxLength", facet);
				}
				if ((this.baseFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && this.datatype.Restriction.Length > this.derivedRestriction.MaxLength)
				{
					throw new XmlSchemaException("Sch_MaxMinLengthBaseLength", facet);
				}
				this.SetFlag(facet, RestrictionFlags.MaxLength);
			}

			// Token: 0x06001817 RID: 6167 RVA: 0x0006AC5C File Offset: 0x00069C5C
			internal void CompilePatternFacet(XmlSchemaPatternFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.Pattern, "Sch_PatternFacetProhibited");
				if (this.firstPattern)
				{
					this.regStr = new StringBuilder();
					this.regStr.Append("(");
					this.regStr.Append(facet.Value);
					this.pattern_facet = new XmlSchemaPatternFacet();
					this.pattern_facet = facet;
					this.firstPattern = false;
				}
				else
				{
					this.regStr.Append(")|(");
					this.regStr.Append(facet.Value);
				}
				this.SetFlag(facet, RestrictionFlags.Pattern);
			}

			// Token: 0x06001818 RID: 6168 RVA: 0x0006ACF4 File Offset: 0x00069CF4
			internal void CompileEnumerationFacet(XmlSchemaFacet facet, IXmlNamespaceResolver nsmgr, XmlNameTable nameTable)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.Enumeration, "Sch_EnumerationFacetProhibited");
				if (this.derivedRestriction.Enumeration == null)
				{
					this.derivedRestriction.Enumeration = new ArrayList();
				}
				this.derivedRestriction.Enumeration.Add(this.ParseFacetValue(this.datatype, facet, "Sch_EnumerationFacetInvalid", nsmgr, nameTable));
				this.SetFlag(facet, RestrictionFlags.Enumeration);
			}

			// Token: 0x06001819 RID: 6169 RVA: 0x0006AD5C File Offset: 0x00069D5C
			internal void CompileWhitespaceFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.WhiteSpace, "Sch_WhiteSpaceFacetProhibited");
				this.CheckDupFlag(facet, RestrictionFlags.WhiteSpace, "Sch_DupWhiteSpaceFacet");
				if (facet.Value == "preserve")
				{
					this.derivedRestriction.WhiteSpace = XmlSchemaWhiteSpace.Preserve;
				}
				else if (facet.Value == "replace")
				{
					this.derivedRestriction.WhiteSpace = XmlSchemaWhiteSpace.Replace;
				}
				else
				{
					if (!(facet.Value == "collapse"))
					{
						throw new XmlSchemaException("Sch_InvalidWhiteSpace", facet.Value, facet);
					}
					this.derivedRestriction.WhiteSpace = XmlSchemaWhiteSpace.Collapse;
				}
				if ((this.baseFixedFlags & RestrictionFlags.WhiteSpace) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.WhiteSpace, this.derivedRestriction.WhiteSpace))
				{
					throw new XmlSchemaException("Sch_FacetBaseFixed", facet);
				}
				XmlSchemaWhiteSpace xmlSchemaWhiteSpace;
				if ((this.baseFlags & RestrictionFlags.WhiteSpace) != (RestrictionFlags)0)
				{
					xmlSchemaWhiteSpace = this.datatype.Restriction.WhiteSpace;
				}
				else
				{
					xmlSchemaWhiteSpace = this.datatype.BuiltInWhitespaceFacet;
				}
				if (xmlSchemaWhiteSpace == XmlSchemaWhiteSpace.Collapse && (this.derivedRestriction.WhiteSpace == XmlSchemaWhiteSpace.Replace || this.derivedRestriction.WhiteSpace == XmlSchemaWhiteSpace.Preserve))
				{
					throw new XmlSchemaException("Sch_WhiteSpaceRestriction1", facet);
				}
				if (xmlSchemaWhiteSpace == XmlSchemaWhiteSpace.Replace && this.derivedRestriction.WhiteSpace == XmlSchemaWhiteSpace.Preserve)
				{
					throw new XmlSchemaException("Sch_WhiteSpaceRestriction2", facet);
				}
				this.SetFlag(facet, RestrictionFlags.WhiteSpace);
			}

			// Token: 0x0600181A RID: 6170 RVA: 0x0006AEBC File Offset: 0x00069EBC
			internal void CompileMaxInclusiveFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MaxInclusive, "Sch_MaxInclusiveFacetProhibited");
				this.CheckDupFlag(facet, RestrictionFlags.MaxInclusive, "Sch_DupMaxInclusiveFacet");
				this.derivedRestriction.MaxInclusive = this.ParseFacetValue(this.builtInType, facet, "Sch_MaxInclusiveFacetInvalid", null, null);
				if ((this.baseFixedFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MaxInclusive, this.derivedRestriction.MaxInclusive))
				{
					throw new XmlSchemaException("Sch_FacetBaseFixed", facet);
				}
				this.CheckValue(this.derivedRestriction.MaxInclusive, facet);
				this.SetFlag(facet, RestrictionFlags.MaxInclusive);
			}

			// Token: 0x0600181B RID: 6171 RVA: 0x0006AF60 File Offset: 0x00069F60
			internal void CompileMaxExclusiveFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MaxExclusive, "Sch_MaxExclusiveFacetProhibited");
				this.CheckDupFlag(facet, RestrictionFlags.MaxExclusive, "Sch_DupMaxExclusiveFacet");
				this.derivedRestriction.MaxExclusive = this.ParseFacetValue(this.builtInType, facet, "Sch_MaxExclusiveFacetInvalid", null, null);
				if ((this.baseFixedFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MaxExclusive, this.derivedRestriction.MaxExclusive))
				{
					throw new XmlSchemaException("Sch_FacetBaseFixed", facet);
				}
				this.CheckValue(this.derivedRestriction.MaxExclusive, facet);
				this.SetFlag(facet, RestrictionFlags.MaxExclusive);
			}

			// Token: 0x0600181C RID: 6172 RVA: 0x0006B010 File Offset: 0x0006A010
			internal void CompileMinInclusiveFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MinInclusive, "Sch_MinInclusiveFacetProhibited");
				this.CheckDupFlag(facet, RestrictionFlags.MinInclusive, "Sch_DupMinInclusiveFacet");
				this.derivedRestriction.MinInclusive = this.ParseFacetValue(this.builtInType, facet, "Sch_MinInclusiveFacetInvalid", null, null);
				if ((this.baseFixedFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MinInclusive, this.derivedRestriction.MinInclusive))
				{
					throw new XmlSchemaException("Sch_FacetBaseFixed", facet);
				}
				this.CheckValue(this.derivedRestriction.MinInclusive, facet);
				this.SetFlag(facet, RestrictionFlags.MinInclusive);
			}

			// Token: 0x0600181D RID: 6173 RVA: 0x0006B0C0 File Offset: 0x0006A0C0
			internal void CompileMinExclusiveFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.MinExclusive, "Sch_MinExclusiveFacetProhibited");
				this.CheckDupFlag(facet, RestrictionFlags.MinExclusive, "Sch_DupMinExclusiveFacet");
				this.derivedRestriction.MinExclusive = this.ParseFacetValue(this.builtInType, facet, "Sch_MinExclusiveFacetInvalid", null, null);
				if ((this.baseFixedFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.MinExclusive, this.derivedRestriction.MinExclusive))
				{
					throw new XmlSchemaException("Sch_FacetBaseFixed", facet);
				}
				this.CheckValue(this.derivedRestriction.MinExclusive, facet);
				this.SetFlag(facet, RestrictionFlags.MinExclusive);
			}

			// Token: 0x0600181E RID: 6174 RVA: 0x0006B170 File Offset: 0x0006A170
			internal void CompileTotalDigitsFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.TotalDigits, "Sch_TotalDigitsFacetProhibited");
				this.CheckDupFlag(facet, RestrictionFlags.TotalDigits, "Sch_DupTotalDigitsFacet");
				XmlSchemaDatatype xmlSchemaDatatype = DatatypeImplementation.GetSimpleTypeFromTypeCode(XmlTypeCode.PositiveInteger).Datatype;
				this.derivedRestriction.TotalDigits = XmlBaseConverter.DecimalToInt32((decimal)this.ParseFacetValue(xmlSchemaDatatype, facet, "Sch_TotalDigitsFacetInvalid", null, null));
				if ((this.baseFixedFlags & RestrictionFlags.TotalDigits) != (RestrictionFlags)0 && !this.datatype.IsEqual(this.datatype.Restriction.TotalDigits, this.derivedRestriction.TotalDigits))
				{
					throw new XmlSchemaException("Sch_FacetBaseFixed", facet);
				}
				if ((this.baseFlags & RestrictionFlags.TotalDigits) != (RestrictionFlags)0 && this.derivedRestriction.TotalDigits > this.datatype.Restriction.TotalDigits)
				{
					throw new XmlSchemaException("Sch_TotalDigitsMismatch", string.Empty);
				}
				this.SetFlag(facet, RestrictionFlags.TotalDigits);
			}

			// Token: 0x0600181F RID: 6175 RVA: 0x0006B264 File Offset: 0x0006A264
			internal void CompileFractionDigitsFacet(XmlSchemaFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.FractionDigits, "Sch_FractionDigitsFacetProhibited");
				this.CheckDupFlag(facet, RestrictionFlags.FractionDigits, "Sch_DupFractionDigitsFacet");
				this.derivedRestriction.FractionDigits = XmlBaseConverter.DecimalToInt32((decimal)this.ParseFacetValue(this.nonNegativeInt, facet, "Sch_FractionDigitsFacetInvalid", null, null));
				if (this.derivedRestriction.FractionDigits != 0 && this.datatype.TypeCode != XmlTypeCode.Decimal)
				{
					throw new XmlSchemaException("Sch_FractionDigitsFacetInvalid", Res.GetString("Sch_FractionDigitsNotOnDecimal"), facet);
				}
				if ((this.baseFlags & RestrictionFlags.FractionDigits) != (RestrictionFlags)0 && this.derivedRestriction.FractionDigits > this.datatype.Restriction.FractionDigits)
				{
					throw new XmlSchemaException("Sch_TotalDigitsMismatch", string.Empty);
				}
				this.SetFlag(facet, RestrictionFlags.FractionDigits);
			}

			// Token: 0x06001820 RID: 6176 RVA: 0x0006B338 File Offset: 0x0006A338
			internal void FinishFacetCompile()
			{
				if (!this.firstPattern)
				{
					if (this.derivedRestriction.Patterns == null)
					{
						this.derivedRestriction.Patterns = new ArrayList();
					}
					try
					{
						this.regStr.Append(")");
						string text = this.regStr.ToString();
						if (text.IndexOf('|') != -1)
						{
							this.regStr.Insert(0, "(");
							this.regStr.Append(")");
						}
						this.derivedRestriction.Patterns.Add(new Regex(FacetsChecker.FacetsCompiler.Preprocess(this.regStr.ToString()), RegexOptions.None));
					}
					catch (Exception ex)
					{
						throw new XmlSchemaException("Sch_PatternFacetInvalid", ex.Message, this.pattern_facet);
					}
				}
			}

			// Token: 0x06001821 RID: 6177 RVA: 0x0006B40C File Offset: 0x0006A40C
			private void CheckValue(object value, XmlSchemaFacet facet)
			{
				RestrictionFacets restriction = this.datatype.Restriction;
				switch (facet.FacetType)
				{
				case FacetType.MinExclusive:
					if ((this.baseFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MinExclusive) < 0)
					{
						throw new XmlSchemaException("Sch_MinExclusiveMismatch", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MinInclusive) < 0)
					{
						throw new XmlSchemaException("Sch_MinExlIncMismatch", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxExclusive) >= 0)
					{
						throw new XmlSchemaException("Sch_MinExlMaxExlMismatch", string.Empty);
					}
					break;
				case FacetType.MinInclusive:
					if ((this.baseFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MinInclusive) < 0)
					{
						throw new XmlSchemaException("Sch_MinInclusiveMismatch", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MinExclusive) < 0)
					{
						throw new XmlSchemaException("Sch_MinIncExlMismatch", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxExclusive) >= 0)
					{
						throw new XmlSchemaException("Sch_MinIncMaxExlMismatch", string.Empty);
					}
					break;
				case FacetType.MaxExclusive:
					if ((this.baseFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxExclusive) > 0)
					{
						throw new XmlSchemaException("Sch_MaxExclusiveMismatch", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxInclusive) > 0)
					{
						throw new XmlSchemaException("Sch_MaxExlIncMismatch", string.Empty);
					}
					break;
				case FacetType.MaxInclusive:
					if ((this.baseFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxInclusive) > 0)
					{
						throw new XmlSchemaException("Sch_MaxInclusiveMismatch", string.Empty);
					}
					if ((this.baseFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(value, restriction.MaxExclusive) >= 0)
					{
						throw new XmlSchemaException("Sch_MaxIncExlMismatch", string.Empty);
					}
					break;
				default:
					return;
				}
			}

			// Token: 0x06001822 RID: 6178 RVA: 0x0006B650 File Offset: 0x0006A650
			internal void CompileFacetCombinations()
			{
				RestrictionFacets restriction = this.datatype.Restriction;
				if ((this.derivedRestriction.Flags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0)
				{
					throw new XmlSchemaException("Sch_MaxInclusiveExclusive", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0)
				{
					throw new XmlSchemaException("Sch_MinInclusiveExclusive", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.Length) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & (RestrictionFlags.MinLength | RestrictionFlags.MaxLength)) != (RestrictionFlags)0)
				{
					throw new XmlSchemaException("Sch_LengthAndMinMax", string.Empty);
				}
				this.CopyFacetsFromBaseType();
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && this.derivedRestriction.MinLength > this.derivedRestriction.MaxLength)
				{
					throw new XmlSchemaException("Sch_MinLengthGtMaxLength", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && this.datatype.Compare(this.derivedRestriction.MinInclusive, this.derivedRestriction.MaxInclusive) > 0)
				{
					throw new XmlSchemaException("Sch_MinInclusiveGtMaxInclusive", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(this.derivedRestriction.MinInclusive, this.derivedRestriction.MaxExclusive) > 0)
				{
					throw new XmlSchemaException("Sch_MinInclusiveGtMaxExclusive", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0 && this.datatype.Compare(this.derivedRestriction.MinExclusive, this.derivedRestriction.MaxExclusive) > 0)
				{
					throw new XmlSchemaException("Sch_MinExclusiveGtMaxExclusive", string.Empty);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0 && (this.derivedRestriction.Flags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0 && this.datatype.Compare(this.derivedRestriction.MinExclusive, this.derivedRestriction.MaxInclusive) > 0)
				{
					throw new XmlSchemaException("Sch_MinExclusiveGtMaxInclusive", string.Empty);
				}
				if ((this.derivedRestriction.Flags & (RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits)) == (RestrictionFlags.TotalDigits | RestrictionFlags.FractionDigits) && this.derivedRestriction.FractionDigits > this.derivedRestriction.TotalDigits)
				{
					throw new XmlSchemaException("Sch_FractionDigitsGtTotalDigits", string.Empty);
				}
			}

			// Token: 0x06001823 RID: 6179 RVA: 0x0006B8F0 File Offset: 0x0006A8F0
			private void CopyFacetsFromBaseType()
			{
				RestrictionFacets restriction = this.datatype.Restriction;
				if ((this.derivedRestriction.Flags & RestrictionFlags.Length) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.Length) != (RestrictionFlags)0)
				{
					this.derivedRestriction.Length = restriction.Length;
					this.SetFlag(RestrictionFlags.Length);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinLength) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MinLength = restriction.MinLength;
					this.SetFlag(RestrictionFlags.MinLength);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MaxLength) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MaxLength = restriction.MaxLength;
					this.SetFlag(RestrictionFlags.MaxLength);
				}
				if ((this.baseFlags & RestrictionFlags.Pattern) != (RestrictionFlags)0)
				{
					if (this.derivedRestriction.Patterns == null)
					{
						this.derivedRestriction.Patterns = restriction.Patterns;
					}
					else
					{
						this.derivedRestriction.Patterns.AddRange(restriction.Patterns);
					}
					this.SetFlag(RestrictionFlags.Pattern);
				}
				if ((this.baseFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0)
				{
					if (this.derivedRestriction.Enumeration == null)
					{
						this.derivedRestriction.Enumeration = restriction.Enumeration;
					}
					this.SetFlag(RestrictionFlags.Enumeration);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.WhiteSpace) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.WhiteSpace) != (RestrictionFlags)0)
				{
					this.derivedRestriction.WhiteSpace = restriction.WhiteSpace;
					this.SetFlag(RestrictionFlags.WhiteSpace);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MaxInclusive) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MaxInclusive) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MaxInclusive = restriction.MaxInclusive;
					this.SetFlag(RestrictionFlags.MaxInclusive);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MaxExclusive) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MaxExclusive) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MaxExclusive = restriction.MaxExclusive;
					this.SetFlag(RestrictionFlags.MaxExclusive);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinInclusive) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MinInclusive) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MinInclusive = restriction.MinInclusive;
					this.SetFlag(RestrictionFlags.MinInclusive);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.MinExclusive) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.MinExclusive) != (RestrictionFlags)0)
				{
					this.derivedRestriction.MinExclusive = restriction.MinExclusive;
					this.SetFlag(RestrictionFlags.MinExclusive);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.TotalDigits) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.TotalDigits) != (RestrictionFlags)0)
				{
					this.derivedRestriction.TotalDigits = restriction.TotalDigits;
					this.SetFlag(RestrictionFlags.TotalDigits);
				}
				if ((this.derivedRestriction.Flags & RestrictionFlags.FractionDigits) == (RestrictionFlags)0 && (this.baseFlags & RestrictionFlags.FractionDigits) != (RestrictionFlags)0)
				{
					this.derivedRestriction.FractionDigits = restriction.FractionDigits;
					this.SetFlag(RestrictionFlags.FractionDigits);
				}
			}

			// Token: 0x06001824 RID: 6180 RVA: 0x0006BBB0 File Offset: 0x0006ABB0
			private object ParseFacetValue(XmlSchemaDatatype datatype, XmlSchemaFacet facet, string code, IXmlNamespaceResolver nsmgr, XmlNameTable nameTable)
			{
				object result;
				Exception ex = datatype.TryParseValue(facet.Value, nameTable, nsmgr, out result);
				if (ex == null)
				{
					return result;
				}
				throw new XmlSchemaException(code, new string[]
				{
					ex.Message
				}, ex, facet.SourceUri, facet.LineNumber, facet.LinePosition, facet);
			}

			// Token: 0x06001825 RID: 6181 RVA: 0x0006BC00 File Offset: 0x0006AC00
			private static string Preprocess(string pattern)
			{
				StringBuilder stringBuilder = new StringBuilder();
				stringBuilder.Append("^");
				char[] array = pattern.ToCharArray();
				int length = pattern.Length;
				int num = 0;
				for (int i = 0; i < length - 2; i++)
				{
					if (array[i] == '\\')
					{
						if (array[i + 1] == '\\')
						{
							i++;
						}
						else
						{
							char c = array[i + 1];
							for (int j = 0; j < FacetsChecker.FacetsCompiler.c_map.Length; j++)
							{
								if (FacetsChecker.FacetsCompiler.c_map[j].match == c)
								{
									if (num < i)
									{
										stringBuilder.Append(array, num, i - num);
									}
									stringBuilder.Append(FacetsChecker.FacetsCompiler.c_map[j].replacement);
									i++;
									num = i + 1;
									break;
								}
							}
						}
					}
				}
				if (num < length)
				{
					stringBuilder.Append(array, num, length - num);
				}
				stringBuilder.Append("$");
				return stringBuilder.ToString();
			}

			// Token: 0x06001826 RID: 6182 RVA: 0x0006BCED File Offset: 0x0006ACED
			private void CheckProhibitedFlag(XmlSchemaFacet facet, RestrictionFlags flag, string errorCode)
			{
				if ((this.validRestrictionFlags & flag) == (RestrictionFlags)0)
				{
					throw new XmlSchemaException(errorCode, this.datatype.TypeCodeString, facet);
				}
			}

			// Token: 0x06001827 RID: 6183 RVA: 0x0006BD0C File Offset: 0x0006AD0C
			private void CheckDupFlag(XmlSchemaFacet facet, RestrictionFlags flag, string errorCode)
			{
				if ((this.derivedRestriction.Flags & flag) != (RestrictionFlags)0)
				{
					throw new XmlSchemaException(errorCode, facet);
				}
			}

			// Token: 0x06001828 RID: 6184 RVA: 0x0006BD25 File Offset: 0x0006AD25
			private void SetFlag(XmlSchemaFacet facet, RestrictionFlags flag)
			{
				this.derivedRestriction.Flags |= flag;
				if (facet.IsFixed)
				{
					this.derivedRestriction.FixedFlags |= flag;
				}
			}

			// Token: 0x06001829 RID: 6185 RVA: 0x0006BD55 File Offset: 0x0006AD55
			private void SetFlag(RestrictionFlags flag)
			{
				this.derivedRestriction.Flags |= flag;
				if ((this.baseFixedFlags & flag) != (RestrictionFlags)0)
				{
					this.derivedRestriction.FixedFlags |= flag;
				}
			}

			// Token: 0x04000E3F RID: 3647
			private DatatypeImplementation datatype;

			// Token: 0x04000E40 RID: 3648
			private RestrictionFacets derivedRestriction;

			// Token: 0x04000E41 RID: 3649
			private RestrictionFlags baseFlags;

			// Token: 0x04000E42 RID: 3650
			private RestrictionFlags baseFixedFlags;

			// Token: 0x04000E43 RID: 3651
			private RestrictionFlags validRestrictionFlags;

			// Token: 0x04000E44 RID: 3652
			private XmlSchemaDatatype nonNegativeInt;

			// Token: 0x04000E45 RID: 3653
			private XmlSchemaDatatype builtInType;

			// Token: 0x04000E46 RID: 3654
			private XmlTypeCode builtInEnum;

			// Token: 0x04000E47 RID: 3655
			private bool firstPattern;

			// Token: 0x04000E48 RID: 3656
			private StringBuilder regStr;

			// Token: 0x04000E49 RID: 3657
			private XmlSchemaPatternFacet pattern_facet;

			// Token: 0x04000E4A RID: 3658
			private static readonly FacetsChecker.FacetsCompiler.Map[] c_map = new FacetsChecker.FacetsCompiler.Map[]
			{
				new FacetsChecker.FacetsCompiler.Map('c', "\\p{_xmlC}"),
				new FacetsChecker.FacetsCompiler.Map('C', "\\P{_xmlC}"),
				new FacetsChecker.FacetsCompiler.Map('d', "\\p{_xmlD}"),
				new FacetsChecker.FacetsCompiler.Map('D', "\\P{_xmlD}"),
				new FacetsChecker.FacetsCompiler.Map('i', "\\p{_xmlI}"),
				new FacetsChecker.FacetsCompiler.Map('I', "\\P{_xmlI}"),
				new FacetsChecker.FacetsCompiler.Map('w', "\\p{_xmlW}"),
				new FacetsChecker.FacetsCompiler.Map('W', "\\P{_xmlW}")
			};

			// Token: 0x020001F7 RID: 503
			private struct Map
			{
				// Token: 0x0600182B RID: 6187 RVA: 0x0006BE62 File Offset: 0x0006AE62
				internal Map(char m, string r)
				{
					this.match = m;
					this.replacement = r;
				}

				// Token: 0x04000E4B RID: 3659
				internal char match;

				// Token: 0x04000E4C RID: 3660
				internal string replacement;
			}
		}
	}
}
