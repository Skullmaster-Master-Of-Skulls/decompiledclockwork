using System;

namespace System.Xml.Schema
{
	// Token: 0x02000208 RID: 520
	internal class Datatype_anyAtomicType : Datatype_anySimpleType
	{
		// Token: 0x0600216E RID: 8558 RVA: 0x000B6816 File Offset: 0x000B4A16
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlAnyConverter.AnyAtomic;
		}

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x0600216F RID: 8559 RVA: 0x000B681D File Offset: 0x000B4A1D
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x170006FC RID: 1788
		// (get) Token: 0x06002170 RID: 8560 RVA: 0x000B6820 File Offset: 0x000B4A20
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}
	}
}
