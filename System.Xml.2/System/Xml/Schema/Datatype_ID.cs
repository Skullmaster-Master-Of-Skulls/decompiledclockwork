using System;

namespace System.Xml.Schema
{
	// Token: 0x0200022B RID: 555
	internal class Datatype_ID : Datatype_NCName
	{
		// Token: 0x17000758 RID: 1880
		// (get) Token: 0x0600221D RID: 8733 RVA: 0x000B7355 File Offset: 0x000B5555
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Id;
			}
		}

		// Token: 0x17000759 RID: 1881
		// (get) Token: 0x0600221E RID: 8734 RVA: 0x000B7359 File Offset: 0x000B5559
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ID;
			}
		}
	}
}
