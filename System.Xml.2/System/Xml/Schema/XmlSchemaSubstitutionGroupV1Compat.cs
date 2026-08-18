using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002B4 RID: 692
	internal class XmlSchemaSubstitutionGroupV1Compat : XmlSchemaSubstitutionGroup
	{
		// Token: 0x1700093F RID: 2367
		// (get) Token: 0x06002800 RID: 10240 RVA: 0x000D20F3 File Offset: 0x000D02F3
		[XmlIgnore]
		internal XmlSchemaChoice Choice
		{
			get
			{
				return this.choice;
			}
		}

		// Token: 0x0400115B RID: 4443
		private XmlSchemaChoice choice = new XmlSchemaChoice();
	}
}
