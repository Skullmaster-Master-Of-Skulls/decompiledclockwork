using System;

namespace System.Xml.Schema
{
	// Token: 0x02000225 RID: 549
	internal class Datatype_token : Datatype_normalizedString
	{
		// Token: 0x17000750 RID: 1872
		// (get) Token: 0x0600220E RID: 8718 RVA: 0x000B72C5 File Offset: 0x000B54C5
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}

		// Token: 0x17000751 RID: 1873
		// (get) Token: 0x0600220F RID: 8719 RVA: 0x000B72C9 File Offset: 0x000B54C9
		internal override XmlSchemaWhiteSpace BuiltInWhitespaceFacet
		{
			get
			{
				return XmlSchemaWhiteSpace.Collapse;
			}
		}
	}
}
