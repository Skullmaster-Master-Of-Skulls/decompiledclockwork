using System;
using System.Collections;
using System.Text.RegularExpressions;
using System.Threading;

namespace System.Xml.Schema
{
	// Token: 0x020001FC RID: 508
	internal class StringFacetsChecker : FacetsChecker
	{
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x06001847 RID: 6215 RVA: 0x0006C6E8 File Offset: 0x0006B6E8
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

		// Token: 0x06001848 RID: 6216 RVA: 0x0006C71C File Offset: 0x0006B71C
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			string value2 = datatype.ValueConverter.ToString(value);
			return this.CheckValueFacets(value2, datatype, true);
		}

		// Token: 0x06001849 RID: 6217 RVA: 0x0006C73F File Offset: 0x0006B73F
		internal override Exception CheckValueFacets(string value, XmlSchemaDatatype datatype)
		{
			return this.CheckValueFacets(value, datatype, true);
		}

		// Token: 0x0600184A RID: 6218 RVA: 0x0006C74C File Offset: 0x0006B74C
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

		// Token: 0x0600184B RID: 6219 RVA: 0x0006C80F File Offset: 0x0006B80F
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration(datatype.ValueConverter.ToString(value), enumeration, datatype);
		}

		// Token: 0x0600184C RID: 6220 RVA: 0x0006C828 File Offset: 0x0006B828
		private bool MatchEnumeration(string value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			if (datatype.TypeCode == XmlTypeCode.AnyUri)
			{
				using (IEnumerator enumerator = enumeration.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						object obj = enumerator.Current;
						Uri uri = (Uri)obj;
						if (value.Equals(uri.OriginalString))
						{
							return true;
						}
					}
					return false;
				}
			}
			foreach (object obj2 in enumeration)
			{
				string value2 = (string)obj2;
				if (value.Equals(value2))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600184D RID: 6221 RVA: 0x0006C8E8 File Offset: 0x0006B8E8
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

		// Token: 0x04000E50 RID: 3664
		private static Regex languagePattern;
	}
}
