using System;

namespace System.Xml.Schema
{
	// Token: 0x020001D3 RID: 467
	internal class Datatype_Name : Datatype_token
	{
		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060016EE RID: 5870 RVA: 0x00063983 File Offset: 0x00062983
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Name;
			}
		}
	}
}
