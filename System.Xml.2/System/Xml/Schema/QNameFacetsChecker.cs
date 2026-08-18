using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200024A RID: 586
	internal class QNameFacetsChecker : FacetsChecker
	{
		// Token: 0x060022F7 RID: 8951 RVA: 0x000B9B14 File Offset: 0x000B7D14
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			XmlQualifiedName value2 = (XmlQualifiedName)datatype.ValueConverter.ChangeType(value, typeof(XmlQualifiedName));
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x060022F8 RID: 8952 RVA: 0x000B9B48 File Offset: 0x000B7D48
		internal override Exception CheckValueFacets(XmlQualifiedName value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			if (restrictionFlags != (RestrictionFlags)0)
			{
				string text = value.ToString();
				int length = text.Length;
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
				if ((restrictionFlags & RestrictionFlags.Enumeration) != (RestrictionFlags)0 && !this.MatchEnumeration(value, restriction.Enumeration))
				{
					return new XmlSchemaException("Sch_EnumerationConstraintFailed", string.Empty);
				}
			}
			return null;
		}

		// Token: 0x060022F9 RID: 8953 RVA: 0x000B9BFD File Offset: 0x000B7DFD
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((XmlQualifiedName)datatype.ValueConverter.ChangeType(value, typeof(XmlQualifiedName)), enumeration);
		}

		// Token: 0x060022FA RID: 8954 RVA: 0x000B9C24 File Offset: 0x000B7E24
		private bool MatchEnumeration(XmlQualifiedName value, ArrayList enumeration)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (value.Equals((XmlQualifiedName)enumeration[i]))
				{
					return true;
				}
			}
			return false;
		}
	}
}
