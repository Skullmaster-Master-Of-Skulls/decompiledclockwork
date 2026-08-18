using System;
using System.Collections;

namespace System.Xml.Schema
{
	// Token: 0x020001FF RID: 511
	internal class BinaryFacetsChecker : FacetsChecker
	{
		// Token: 0x06001855 RID: 6229 RVA: 0x0006CB30 File Offset: 0x0006BB30
		internal override Exception CheckValueFacets(object value, XmlSchemaDatatype datatype)
		{
			byte[] value2 = (byte[])value;
			return this.CheckValueFacets(value2, datatype);
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0006CB4C File Offset: 0x0006BB4C
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

		// Token: 0x06001857 RID: 6231 RVA: 0x0006CBF8 File Offset: 0x0006BBF8
		internal override bool MatchEnumeration(object value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			return this.MatchEnumeration((byte[])value, enumeration, datatype);
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0006CC08 File Offset: 0x0006BC08
		private bool MatchEnumeration(byte[] value, ArrayList enumeration, XmlSchemaDatatype datatype)
		{
			foreach (object obj in enumeration)
			{
				byte[] value2 = (byte[])obj;
				if (datatype.Compare(value, value2) == 0)
				{
					return true;
				}
			}
			return false;
		}
	}
}
