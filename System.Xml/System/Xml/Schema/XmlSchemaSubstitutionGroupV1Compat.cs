using System;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200027F RID: 639
	internal class XmlSchemaSubstitutionGroupV1Compat : XmlSchemaSubstitutionGroup
	{
		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x06001D67 RID: 7527 RVA: 0x00085FE9 File Offset: 0x00084FE9
		[XmlIgnore]
		internal XmlSchemaChoice Choice
		{
			get
			{
				return this.choice;
			}
		}

		// Token: 0x040011E5 RID: 4581
		private XmlSchemaChoice choice = new XmlSchemaChoice();
	}
}
