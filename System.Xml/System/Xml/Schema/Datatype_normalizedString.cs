using System;

namespace System.Xml.Schema
{
	// Token: 0x020001CD RID: 461
	internal class Datatype_normalizedString : Datatype_string
	{
		// Token: 0x170005B6 RID: 1462
		// (get) Token: 0x060016DD RID: 5853 RVA: 0x0006392C File Offset: 0x0006292C
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x170005B7 RID: 1463
		// (get) Token: 0x060016DE RID: 5854 RVA: 0x00063930 File Offset: 0x00062930
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Replace;
			}
		}

		// Token: 0x170005B8 RID: 1464
		// (get) Token: 0x060016DF RID: 5855 RVA: 0x00063933 File Offset: 0x00062933
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}
	}
}
