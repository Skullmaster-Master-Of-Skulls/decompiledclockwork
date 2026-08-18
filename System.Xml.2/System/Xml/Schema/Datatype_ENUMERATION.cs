using System;

namespace System.Xml.Schema
{
	// Token: 0x0200023F RID: 575
	internal class Datatype_ENUMERATION : Datatype_NMTOKEN
	{
		// Token: 0x17000794 RID: 1940
		// (get) Token: 0x06002292 RID: 8850 RVA: 0x000B7DDE File Offset: 0x000B5FDE
		public override XmlTokenizedType TokenizedType
		{
			get
			{
				return XmlTokenizedType.ENUMERATION;
			}
		}
	}
}
