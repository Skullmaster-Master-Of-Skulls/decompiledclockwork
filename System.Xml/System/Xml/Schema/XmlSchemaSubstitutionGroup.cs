using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x0200027E RID: 638
	internal class XmlSchemaSubstitutionGroup : XmlSchemaObject
	{
		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x06001D63 RID: 7523 RVA: 0x00085FB2 File Offset: 0x00084FB2
		[XmlIgnore]
		internal ArrayList Members
		{
			get
			{
				return this.membersList;
			}
		}

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00085FBA File Offset: 0x00084FBA
		// (set) Token: 0x06001D65 RID: 7525 RVA: 0x00085FC2 File Offset: 0x00084FC2
		[XmlIgnore]
		internal XmlQualifiedName Examplar
		{
			get
			{
				return this.examplar;
			}
			set
			{
				this.examplar = value;
			}
		}

		// Token: 0x040011E3 RID: 4579
		private ArrayList membersList = new ArrayList();

		// Token: 0x040011E4 RID: 4580
		private XmlQualifiedName examplar = XmlQualifiedName.Empty;
	}
}
