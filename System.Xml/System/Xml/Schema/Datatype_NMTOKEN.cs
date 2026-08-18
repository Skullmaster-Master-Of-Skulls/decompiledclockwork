using System;

namespace System.Xml.Schema
{
	// Token: 0x020001D2 RID: 466
	internal class Datatype_NMTOKEN : Datatype_token
	{
		// Token: 0x170005BF RID: 1471
		// (get) Token: 0x060016EB RID: 5867 RVA: 0x00063974 File Offset: 0x00062974
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NmToken;
			}
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060016EC RID: 5868 RVA: 0x00063978 File Offset: 0x00062978
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NMTOKEN;
			}
		}
	}
}
