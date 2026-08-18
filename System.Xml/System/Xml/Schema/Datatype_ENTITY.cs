using System;

namespace System.Xml.Schema
{
	// Token: 0x020001D7 RID: 471
	internal class Datatype_ENTITY : Datatype_NCName
	{
		// Token: 0x170005C7 RID: 1479
		// (get) Token: 0x060016F9 RID: 5881 RVA: 0x000639FB File Offset: 0x000629FB
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Entity;
			}
		}

		// Token: 0x170005C8 RID: 1480
		// (get) Token: 0x060016FA RID: 5882 RVA: 0x000639FF File Offset: 0x000629FF
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENTITY;
			}
		}
	}
}
