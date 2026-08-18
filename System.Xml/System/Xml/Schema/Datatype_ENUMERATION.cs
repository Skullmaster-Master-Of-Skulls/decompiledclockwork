using System;

namespace System.Xml.Schema
{
	// Token: 0x020001E9 RID: 489
	internal class Datatype_ENUMERATION : Datatype_NMTOKEN
	{
		// Token: 0x170005FF RID: 1535
		// (get) Token: 0x06001768 RID: 5992 RVA: 0x0006449A File Offset: 0x0006349A
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENUMERATION;
			}
		}
	}
}
