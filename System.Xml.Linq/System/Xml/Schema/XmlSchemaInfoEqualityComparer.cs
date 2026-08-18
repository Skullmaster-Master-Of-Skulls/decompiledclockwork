using System;
using System.Collections.Generic;

namespace System.Xml.Schema
{
	// Token: 0x02000006 RID: 6
	internal class XmlSchemaInfoEqualityComparer : IEqualityComparer<XmlSchemaInfo>
	{
		// Token: 0x06000010 RID: 16 RVA: 0x0000285C File Offset: 0x00000A5C
		public bool Equals(XmlSchemaInfo si1, XmlSchemaInfo si2)
		{
			return si1 == si2 || (si1 != null && si2 != null && (si1.ContentType == si2.ContentType && si1.IsDefault == si2.IsDefault && si1.IsNil == si2.IsNil && si1.MemberType == si2.MemberType && si1.SchemaAttribute == si2.SchemaAttribute && si1.SchemaElement == si2.SchemaElement && si1.SchemaType == si2.SchemaType) && si1.Validity == si2.Validity);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000028EC File Offset: 0x00000AEC
		public int GetHashCode(XmlSchemaInfo si)
		{
			if (si == null)
			{
				return 0;
			}
			int num = (int)si.ContentType;
			if (si.IsDefault)
			{
				num ^= 1;
			}
			if (si.IsNil)
			{
				num ^= 1;
			}
			XmlSchemaSimpleType memberType = si.MemberType;
			if (memberType != null)
			{
				num ^= memberType.GetHashCode();
			}
			XmlSchemaAttribute schemaAttribute = si.SchemaAttribute;
			if (schemaAttribute != null)
			{
				num ^= schemaAttribute.GetHashCode();
			}
			XmlSchemaElement schemaElement = si.SchemaElement;
			if (schemaElement != null)
			{
				num ^= schemaElement.GetHashCode();
			}
			XmlSchemaType schemaType = si.SchemaType;
			if (schemaType != null)
			{
				num ^= schemaType.GetHashCode();
			}
			return num ^ (int)si.Validity;
		}
	}
}
