using System;

namespace System.Xml.Schema
{
	// Token: 0x02000209 RID: 521
	internal class Datatype_untypedAtomicType : Datatype_anyAtomicType
	{
		// Token: 0x06002172 RID: 8562 RVA: 0x000B682C File Offset: 0x000B4A2C
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUntypedConverter.Untyped;
		}

		// Token: 0x170006FD RID: 1789
		// (get) Token: 0x06002173 RID: 8563 RVA: 0x000B6833 File Offset: 0x000B4A33
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x170006FE RID: 1790
		// (get) Token: 0x06002174 RID: 8564 RVA: 0x000B6836 File Offset: 0x000B4A36
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UntypedAtomic;
			}
		}
	}
}
