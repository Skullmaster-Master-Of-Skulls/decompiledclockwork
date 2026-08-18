using System;
using System.Collections;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Xml.Schema
{
	// Token: 0x02000244 RID: 580
	internal abstract class FacetsChecker
	{
		// Token: 0x060022C0 RID: 8896 RVA: 0x000B8E28 File Offset: 0x000B7028
		internal virtual Exception CheckLexicalFacets(ref string parseString, XmlSchemaDatatype datatype)
		{
			this.CheckWhitespaceFacets(ref parseString, datatype);
			return this.CheckPatternFacets(datatype.Restriction, parseString);
		}

		// Token: 0x060022C1 RID: 8897 RVA: 0x000B8E40 File Offset: 0x000B7040
		internal virtual Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022C2 RID: 8898 RVA: 0x000B8E43 File Offset: 0x000B7043
		internal virtual Exception CheckValueFacets(decimal value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022C3 RID: 8899 RVA: 0x000B8E46 File Offset: 0x000B7046
		internal virtual Exception CheckValueFacets(long value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022C4 RID: 8900 RVA: 0x000B8E49 File Offset: 0x000B7049
		internal virtual Exception CheckValueFacets(int value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022C5 RID: 8901 RVA: 0x000B8E4C File Offset: 0x000B704C
		internal virtual Exception CheckValueFacets(short value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022C6 RID: 8902 RVA: 0x000B8E4F File Offset: 0x000B704F
		internal virtual Exception CheckValueFacets(byte value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022C7 RID: 8903 RVA: 0x000B8E52 File Offset: 0x000B7052
		internal virtual Exception CheckValueFacets(DateTime value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022C8 RID: 8904 RVA: 0x000B8E55 File Offset: 0x000B7055
		internal virtual Exception CheckValueFacets(double value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022C9 RID: 8905 RVA: 0x000B8E58 File Offset: 0x000B7058
		internal virtual Exception CheckValueFacets(float value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022CA RID: 8906 RVA: 0x000B8E5B File Offset: 0x000B705B
		internal virtual Exception CheckValueFacets(string value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022CB RID: 8907 RVA: 0x000B8E5E File Offset: 0x000B705E
		internal virtual Exception CheckValueFacets(byte[] value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022CC RID: 8908 RVA: 0x000B8E61 File Offset: 0x000B7061
		internal virtual Exception CheckValueFacets(TimeSpan value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022CD RID: 8909 RVA: 0x000B8E64 File Offset: 0x000B7064
		internal virtual Exception CheckValueFacets(XmlQualifiedName value, XmlSchemaDatatype datatype)
		{
			return null;
		}

		// Token: 0x060022CE RID: 8910 RVA: 0x000B8E68 File Offset: 0x000B7068
		internal void CheckWhitespaceFacets(ref string s, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			XmlSchemaDatatypeVariety variety = datatype.Variety;
			if (variety != XmlSchemaDatatypeVariety.Atomic)
			{
				if (variety == XmlSchemaDatatypeVariety.List)
				{
					s = s.Trim();
					return;
				}
			}
			else
			{
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
			}
		}

		// Token: 0x060022CF RID: 8911 RVA: 0x000B8EF0 File Offset: 0x000B70F0
		internal Exception CheckPatternFacets(RestrictionFacets restriction, string value)
		{
			if (restriction != null && (restriction.Flags & RestrictionFlags.Pattern) != (RestrictionFlags)0)
			{
				for (int i = 0; i < restriction.Patterns.Count; i++)
				{
					Regex regex = (Regex)restriction.Patterns[i];
					if (!regex.IsMatch(value))
					{
						return new XmlSchemaException("Sch_PatternConstraintFailed", string.Empty);
					}
				}
			}
			return null;
		}

		// Token: 0x060022D0 RID: 8912 RVA: 0x000B8F4C File Offset: 0x000B714C
		internal virtual bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return false;
		}

		// Token: 0x060022D1 RID: 8913 RVA: 0x000B8F50 File Offset: 0x000B7150
		internal virtual RestrictionFacets ConstructRestriction(DatatypeImplementation datatype, XmlSchemaObjectCollection facets, XmlNameTable nameTable)
		{
			RestrictionFacets restrictionFacets = new RestrictionFacets();
			FacetsChecker.FacetsCompiler facetsCompiler = new FacetsChecker.FacetsCompiler(datatype, restrictionFacets);
			for (int i = 0; i < facets.Count; i++)
			{
				XmlSchemaFacet xmlSchemaFacet = (XmlSchemaFacet)facets[i];
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

		// Token: 0x060022D2 RID: 8914 RVA: 0x000B9090 File Offset: 0x000B7290
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

		// Token: 0x02000490 RID: 1168
		private struct FacetsCompiler
		{
			// Token: 0x0600312A RID: 12586 RVA: 0x0011E128 File Offset: 0x0011C328
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

			// Token: 0x0600312B RID: 12587 RVA: 0x0011E21C File Offset: 0x0011C41C
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
				if ((this.baseFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && this.datatype.Restriction.MinLength > this.derivedRestriction.Length)
				{
					throw new XmlSchemaException("Sch_MaxMinLengthBaseLength", facet);
				}
				if ((this.baseFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && this.datatype.Restriction.MaxLength < this.derivedRestriction.Length)
				{
					throw new XmlSchemaException("Sch_MaxMinLengthBaseLength", facet);
				}
				this.SetFlag(facet, RestrictionFlags.Length);
			}

			// Token: 0x0600312C RID: 12588 RVA: 0x0011E358 File Offset: 0x0011C558
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

			// Token: 0x0600312D RID: 12589 RVA: 0x0011E460 File Offset: 0x0011C660
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

			// Token: 0x0600312E RID: 12590 RVA: 0x0011E568 File Offset: 0x0011C768
			internal void CompilePatternFacet(XmlSchemaPatternFacet facet)
			{
				this.CheckProhibitedFlag(facet, RestrictionFlags.Pattern, "Sch_PatternFacetProhibited");
				if (this.firstPattern)
				{
					this.regStr = new StringBuilder();
					this.regStr.Append("(");
					this.regStr.Append(facet.Value);
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

			// Token: 0x0600312F RID: 12591 RVA: 0x0011E5F4 File Offset: 0x0011C7F4
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

			// Token: 0x06003130 RID: 12592 RVA: 0x0011E65C File Offset: 0x0011C85C
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

			// Token: 0x06003131 RID: 12593 RVA: 0x0011E7BC File Offset: 0x0011C9BC
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

			// Token: 0x06003132 RID: 12594 RVA: 0x0011E860 File Offset: 0x0011CA60
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

			// Token: 0x06003133 RID: 12595 RVA: 0x0011E910 File Offset: 0x0011CB10
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

			// Token: 0x06003134 RID: 12596 RVA: 0x0011E9C0 File Offset: 0x0011CBC0
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

			// Token: 0x06003135 RID: 12597 RVA: 0x0011EA70 File Offset: 0x0011CC70
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

			// Token: 0x06003136 RID: 12598 RVA: 0x0011EB64 File Offset: 0x0011CD64
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

			// Token: 0x06003137 RID: 12599 RVA: 0x0011EC38 File Offset: 0x0011CE38
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
						throw new XmlSchemaException("Sch_PatternFacetInvalid", new string[]
						{
							ex.Message
						}, ex, this.pattern_facet.SourceUri, this.pattern_facet.LineNumber, this.pattern_facet.LinePosition, this.pattern_facet);
					}
				}
			}

			// Token: 0x06003138 RID: 12600 RVA: 0x0011ED38 File Offset: 0x0011CF38
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

			// Token: 0x06003139 RID: 12601 RVA: 0x0011EF7C File Offset: 0x0011D17C
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

			// Token: 0x0600313A RID: 12602 RVA: 0x0011F21C File Offset: 0x0011D41C
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

			// Token: 0x0600313B RID: 12603 RVA: 0x0011F4DC File Offset: 0x0011D6DC
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

			// Token: 0x0600313C RID: 12604 RVA: 0x0011F52C File Offset: 0x0011D72C
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

			// Token: 0x0600313D RID: 12605 RVA: 0x0011F619 File Offset: 0x0011D819
			private void CheckProhibitedFlag(XmlSchemaFacet facet, RestrictionFlags flag, string errorCode)
			{
				if ((this.validRestrictionFlags & flag) == (RestrictionFlags)0)
				{
					throw new XmlSchemaException(errorCode, this.datatype.TypeCodeString, facet);
				}
			}

			// Token: 0x0600313E RID: 12606 RVA: 0x0011F638 File Offset: 0x0011D838
			private void CheckDupFlag(XmlSchemaFacet facet, RestrictionFlags flag, string errorCode)
			{
				if ((this.derivedRestriction.Flags & flag) != (RestrictionFlags)0)
				{
					throw new XmlSchemaException(errorCode, facet);
				}
			}

			// Token: 0x0600313F RID: 12607 RVA: 0x0011F651 File Offset: 0x0011D851
			private void SetFlag(XmlSchemaFacet facet, RestrictionFlags flag)
			{
				this.derivedRestriction.Flags |= flag;
				if (facet.IsFixed)
				{
					this.derivedRestriction.FixedFlags |= flag;
				}
			}

			// Token: 0x06003140 RID: 12608 RVA: 0x0011F681 File Offset: 0x0011D881
			private void SetFlag(RestrictionFlags flag)
			{
				this.derivedRestriction.Flags |= flag;
				if ((this.baseFixedFlags & flag) != (RestrictionFlags)0)
				{
					this.derivedRestriction.FixedFlags |= flag;
				}
			}

			// Token: 0x04001E1B RID: 7707
			private DatatypeImplementation datatype;

			// Token: 0x04001E1C RID: 7708
			private RestrictionFacets derivedRestriction;

			// Token: 0x04001E1D RID: 7709
			private RestrictionFlags baseFlags;

			// Token: 0x04001E1E RID: 7710
			private RestrictionFlags baseFixedFlags;

			// Token: 0x04001E1F RID: 7711
			private RestrictionFlags validRestrictionFlags;

			// Token: 0x04001E20 RID: 7712
			private XmlSchemaDatatype nonNegativeInt;

			// Token: 0x04001E21 RID: 7713
			private XmlSchemaDatatype builtInType;

			// Token: 0x04001E22 RID: 7714
			private XmlTypeCode builtInEnum;

			// Token: 0x04001E23 RID: 7715
			private bool firstPattern;

			// Token: 0x04001E24 RID: 7716
			private StringBuilder regStr;

			// Token: 0x04001E25 RID: 7717
			private XmlSchemaPatternFacet pattern_facet;

			// Token: 0x04001E26 RID: 7718
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

			// Token: 0x020004E1 RID: 1249
			private struct Map
			{
				// Token: 0x060031CC RID: 12748 RVA: 0x00121B5A File Offset: 0x0011FD5A
				internal Map(char m, string r)
				{
					this.match = m;
					this.replacement = r;
				}

				// Token: 0x04001FC2 RID: 8130
				internal char match;

				// Token: 0x04001FC3 RID: 8131
				internal string replacement;
			}
		}
	}
}
