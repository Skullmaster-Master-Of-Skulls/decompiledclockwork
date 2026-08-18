using System;

namespace System.Xml.Schema
{
	// Token: 0x020001B2 RID: 434
	internal class Datatype_anyAtomicType : Datatype_anySimpleType
	{
		// Token: 0x06001644 RID: 5700 RVA: 0x00062E94 File Offset: 0x00061E94
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlAnyConverter.AnyAtomic;
		}

		// Token: 0x17000566 RID: 1382
		// (get) Token: 0x06001645 RID: 5701 RVA: 0x00062E9B File Offset: 0x00061E9B
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x17000567 RID: 1383
		// (get) Token: 0x06001646 RID: 5702 RVA: 0x00062E9E File Offset: 0x00061E9E
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.AnyAtomicType;
			}
		}
	}
}
