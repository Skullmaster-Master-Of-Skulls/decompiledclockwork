using System;

namespace System.Xml.Schema
{
	// Token: 0x02000226 RID: 550
	internal class Datatype_tokenV1Compat : Datatype_normalizedStringV1Compat
	{
		// Token: 0x17000752 RID: 1874
		// (get) Token: 0x06002211 RID: 8721 RVA: 0x000B72D4 File Offset: 0x000B54D4
		public override XmlTypeCode TypeCode
		{
			get
			{
				return XmlTypeCode.Token;
			}
		}
	}
}
