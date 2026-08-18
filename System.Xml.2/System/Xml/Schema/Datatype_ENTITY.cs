using System;

namespace System.Xml.Schema
{
	// Token: 0x0200022D RID: 557
	internal class Datatype_ENTITY : Datatype_NCName
	{
		// Token: 0x1700075C RID: 1884
		// (get) Token: 0x06002223 RID: 8739 RVA: 0x000B7373 File Offset: 0x000B5573
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Entity;
			}
		}

		// Token: 0x1700075D RID: 1885
		// (get) Token: 0x06002224 RID: 8740 RVA: 0x000B7377 File Offset: 0x000B5577
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENTITY;
			}
		}
	}
}
