using System;

namespace System.Xml.Schema
{
	// Token: 0x02000228 RID: 552
	internal class Datatype_NMTOKEN : Datatype_token
	{
		// Token: 0x17000754 RID: 1876
		// (get) Token: 0x06002215 RID: 8725 RVA: 0x000B72EC File Offset: 0x000B54EC
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NmToken;
			}
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06002216 RID: 8726 RVA: 0x000B72F0 File Offset: 0x000B54F0
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.NMTOKEN;
			}
		}
	}
}
