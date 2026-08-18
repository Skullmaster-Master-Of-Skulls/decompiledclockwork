using System;

namespace System.Xml.Schema
{
	// Token: 0x02000223 RID: 547
	internal class Datatype_normalizedString : Datatype_string
	{
		// Token: 0x1700074B RID: 1867
		// (get) Token: 0x06002207 RID: 8711 RVA: 0x000B72A4 File Offset: 0x000B54A4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x1700074C RID: 1868
		// (get) Token: 0x06002208 RID: 8712 RVA: 0x000B72A8 File Offset: 0x000B54A8
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Replace;
			}
		}

		// Token: 0x1700074D RID: 1869
		// (get) Token: 0x06002209 RID: 8713 RVA: 0x000B72AB File Offset: 0x000B54AB
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}
	}
}
