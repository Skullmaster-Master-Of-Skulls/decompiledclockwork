using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;

namespace System.Xml.Schema
{
	// Token: 0x02000249 RID: 585
	internal class StringFacetsChecker : FacetsChecker
	{
		// Token: 0x1700079C RID: 1948
		// (get) Token: 0x060022EF RID: 8943 RVA: 0x000B98A0 File Offset: 0x000B7AA0
		private static Regex LanguagePattern
		{
			get
			{
				if (StringFacetsChecker.languagePattern == null)
				{
					Regex value = new Regex("^([a-zA-Z]{1,8})(-[a-zA-Z0-9]{1,8})*$", RegexOptions.None);
					Interlocked.CompareExchange<Regex>(ref StringFacetsChecker.languagePattern, value, null);
				}
				return StringFacetsChecker.languagePattern;
			}
		}

		// Token: 0x060022F0 RID: 8944 RVA: 0x000B98D4 File Offset: 0x000B7AD4
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			string value2 = datatype.ValueConverter.ToString(value);
			return this.CheckValueFacets(value2, datatype, true);
		}

		// Token: 0x060022F1 RID: 8945 RVA: 0x000B98F7 File Offset: 0x000B7AF7
		internal override Exception CheckValueFacets(string value, XmlSchemaDatatype datatype)
		{
			return this.CheckValueFacets(value, datatype, true);
		}

		// Token: 0x060022F2 RID: 8946 RVA: 0x000B9904 File Offset: 0x000B7B04
		internal Exception CheckValueFacets(string value, XmlSchemaDatatype datatype, bool verifyUri)
		{
			int length = value.Length;
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			Exception ex = this.CheckBuiltInFacets(value, datatype.TypeCode, verifyUri);
			if (ex != null)
			{
				return ex;
			}
			if (restrictionFlags != (RestrictionFlags)0)
			{
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
				if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration, datatype))
				{
					return new XmlSchemaException("Sch_EnumerationConstraintFailed", string.Empty);
				}
			}
			return null;
		}

		// Token: 0x060022F3 RID: 8947 RVA: 0x000B99C7 File Offset: 0x000B7BC7
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToString(value), enumeration, datatype);
		}

		// Token: 0x060022F4 RID: 8948 RVA: 0x000B99E0 File Offset: 0x000B7BE0
		private bool MatchEnumeration(string value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			if (datatype.TypeCode == XmlTypeCode.AnyUri)
			{
				for (int i = 0; i < enumeration.Count; i++)
				{
					if (value.Equals(((Uri)enumeration[i]).OriginalString))
					{
						return true;
					}
				}
			}
			else
			{
				for (int j = 0; j < enumeration.Count; j++)
				{
					if (value.Equals((string)enumeration[j]))
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x060022F5 RID: 8949 RVA: 0x000B9A50 File Offset: 0x000B7C50
		private Exception CheckBuiltInFacets(string s, XmlTypeCode typeCode, bool verifyUri)
		{
			Exception result = null;
			switch (typeCode)
			{
			case XmlTypeCode.AnyUri:
				if (verifyUri)
				{
					Uri uri;
					result = XmlConvert.TryToUri(s, out uri);
				}
				break;
			case XmlTypeCode.NormalizedString:
				result = XmlConvert.TryVerifyNormalizedString(s);
				break;
			case XmlTypeCode.Token:
				result = XmlConvert.TryVerifyTOKEN(s);
				break;
			case XmlTypeCode.Language:
				if (s == null || s.Length == 0)
				{
					return new XmlSchemaException("Sch_EmptyAttributeValue", string.Empty);
				}
				if (!StringFacetsChecker.LanguagePattern.IsMatch(s))
				{
					return new XmlSchemaException("Sch_InvalidLanguageId", string.Empty);
				}
				break;
			case XmlTypeCode.NmToken:
				result = XmlConvert.TryVerifyNMTOKEN(s);
				break;
			case XmlTypeCode.Name:
				result = XmlConvert.TryVerifyName(s);
				break;
			case XmlTypeCode.NCName:
			case XmlTypeCode.Id:
			case XmlTypeCode.Idref:
			case XmlTypeCode.Entity:
				result = XmlConvert.TryVerifyNCName(s);
				break;
			}
			return result;
		}

		// Token: 0x04000EB1 RID: 3761
		private static Regex languagePattern;
	}
}
