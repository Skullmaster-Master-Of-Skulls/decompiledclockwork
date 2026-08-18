using System;

namespace System.Xml.Schema
{
	// Token: 0x020001CE RID: 462
	internal class Datatype_normalizedStringV1Compat : Datatype_string
	{
		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060016E1 RID: 5857 RVA: 0x0006393E File Offset: 0x0006293E
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060016E2 RID: 5858 RVA: 0x00063942 File Offset: 0x00062942
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}
	}
}
