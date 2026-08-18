using System;

namespace System.Xml.Schema
{
	// Token: 0x020001D5 RID: 469
	internal class Datatype_ID : Datatype_NCName
	{
		// Token: 0x170005C3 RID: 1475
		// (get) Token: 0x060016F3 RID: 5875 RVA: 0x000639DD File Offset: 0x000629DD
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Id;
			}
		}

		// Token: 0x170005C4 RID: 1476
		// (get) Token: 0x060016F4 RID: 5876 RVA: 0x000639E1 File Offset: 0x000629E1
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ID;
			}
		}
	}
}
