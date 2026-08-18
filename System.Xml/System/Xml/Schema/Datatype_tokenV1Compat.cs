using System;

namespace System.Xml.Schema
{
	// Token: 0x020001D0 RID: 464
	internal class Datatype_tokenV1Compat : Datatype_normalizedStringV1Compat
	{
		// Token: 0x170005BD RID: 1469
		// (get) Token: 0x060016E7 RID: 5863 RVA: 0x0006395C File Offset: 0x0006295C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}
	}
}
