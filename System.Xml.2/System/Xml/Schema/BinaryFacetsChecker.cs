using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x0200024C RID: 588
	internal class BinaryFacetsChecker : FacetsChecker
	{
		// Token: 0x060022FD RID: 8957 RVA: 0x000B9C6C File Offset: 0x000B7E6C
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			byte[] value2 = (byte[])value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x060022FE RID: 8958 RVA: 0x000B9C88 File Offset: 0x000B7E88
		internal override Exception CheckValueFacets(byte[] value, XmlSchemaDatatype datatype)
		{
			RestrictionFacets restriction = datatype.Restriction;
			int num = value.Length;
			RestrictionFlags restrictionFlags = (restriction != null) ? restriction.Flags : ((RestrictionFlags)0);
			if (restrictionFlags != (RestrictionFlags)0)
			{
				if ((restrictionFlags & RestrictionFlags.Length) != (RestrictionFlags)0 && restriction.Length != num)
				{
					return new XmlSchemaException("Sch_LengthConstraintFailed", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MinLength) != (RestrictionFlags)0 && num < restriction.MinLength)
				{
					return new XmlSchemaException("Sch_MinLengthConstraintFailed", string.Empty);
				}
				if ((restrictionFlags & RestrictionFlags.MaxLength) != (RestrictionFlags)0 && restriction.MaxLength < num)
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

		// Token: 0x060022FF RID: 8959 RVA: 0x000B9D34 File Offset: 0x000B7F34
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((byte[])value, enumeration, datatype);
		}

		// Token: 0x06002300 RID: 8960 RVA: 0x000B9D44 File Offset: 0x000B7F44
		private bool MatchEnumeration(byte[] value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			for (int i = 0; i < enumeration.Count; i++)
			{
				if (datatype.Compare(value, (byte[])enumeration[i]) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
