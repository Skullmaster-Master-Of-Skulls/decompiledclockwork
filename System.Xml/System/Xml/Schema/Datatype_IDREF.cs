using System;

namespace System.Xml.Schema
{
	// Token: 0x020001D6 RID: 470
	internal class Datatype_IDREF : Datatype_NCName
	{
		// Token: 0x170005C5 RID: 1477
		// (get) Token: 0x060016F6 RID: 5878 RVA: 0x000639EC File Offset: 0x000629EC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Idref;
			}
		}

		// Token: 0x170005C6 RID: 1478
		// (get) Token: 0x060016F7 RID: 5879 RVA: 0x000639F0 File Offset: 0x000629F0
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.IDREF;
			}
		}
	}
}
