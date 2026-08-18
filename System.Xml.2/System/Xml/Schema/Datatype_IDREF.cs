using System;

namespace System.Xml.Schema
{
	// Token: 0x0200022C RID: 556
	internal class Datatype_IDREF : Datatype_NCName
	{
		// Token: 0x1700075A RID: 1882
		// (get) Token: 0x06002220 RID: 8736 RVA: 0x000B7364 File Offset: 0x000B5564
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Idref;
			}
		}

		// Token: 0x1700075B RID: 1883
		// (get) Token: 0x06002221 RID: 8737 RVA: 0x000B7368 File Offset: 0x000B5568
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.IDREF;
			}
		}
	}
}
