using System;

namespace System.Xml.Schema
{
	// Token: 0x020001B3 RID: 435
	internal class Datatype_untypedAtomicType : Datatype_anyAtomicType
	{
		// Token: 0x06001648 RID: 5704 RVA: 0x00062EAA File Offset: 0x00061EAA
		internal override XmlValueConverter CreateValueConverter(XmlSchemaType schemaType)
		{
			return XmlUntypedConverter.Untyped;
		}

		// Token: 0x17000568 RID: 1384
		// (get) Token: 0x06001649 RID: 5705 RVA: 0x00062EB1 File Offset: 0x00061EB1
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Preserve;
			}
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x0600164A RID: 5706 RVA: 0x00062EB4 File Offset: 0x00061EB4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.UntypedAtomic;
			}
		}
	}
}
