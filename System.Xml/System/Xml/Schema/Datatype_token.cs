using System;

namespace System.Xml.Schema
{
	// Token: 0x020001CF RID: 463
	internal class Datatype_token : Datatype_normalizedString
	{
		// Token: 0x170005BB RID: 1467
		// (get) Token: 0x060016E4 RID: 5860 RVA: 0x0006394D File Offset: 0x0006294D
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}

		// Token: 0x170005BC RID: 1468
		// (get) Token: 0x060016E5 RID: 5861 RVA: 0x00063951 File Offset: 0x00062951
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}
	}
}
