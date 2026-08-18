using System;

namespace System.Xml.Schema
{
	// Token: 0x02000224 RID: 548
	internal class Datatype_normalizedStringV1Compat : Datatype_string
	{
		// Token: 0x1700074E RID: 1870
		// (get) Token: 0x0600220B RID: 8715 RVA: 0x000B72B6 File Offset: 0x000B54B6
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.NormalizedString;
			}
		}

		// Token: 0x1700074F RID: 1871
		// (get) Token: 0x0600220C RID: 8716 RVA: 0x000B72BA File Offset: 0x000B54BA
		internal override bool HasValueFacets
		{
			get
			{
				return true;
			}
		}
	}
}
