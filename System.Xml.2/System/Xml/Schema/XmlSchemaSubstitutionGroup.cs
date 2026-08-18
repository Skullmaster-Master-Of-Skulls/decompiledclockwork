using System;
using System.Collections;
using System.Xml.Serialization;

namespace System.Xml.Schema
{
	// Token: 0x020002B3 RID: 691
	internal class XmlSchemaSubstitutionGroup : XmlSchemaObject
	{
		// Token: 0x1700093D RID: 2365
		// (get) Token: 0x060027FC RID: 10236 RVA: 0x000D20BC File Offset: 0x000D02BC
		[XmlIgnore]
		internal ArrayList Members
		{
			get
			{
				return this.membersList;
			}
		}

		// Token: 0x1700093E RID: 2366
		// (get) Token: 0x060027FD RID: 10237 RVA: 0x000D20C4 File Offset: 0x000D02C4
		// (set) Token: 0x060027FE RID: 10238 RVA: 0x000D20CC File Offset: 0x000D02CC
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

		// Token: 0x04001159 RID: 4441
		private ArrayList membersList = new ArrayList();

		// Token: 0x0400115A RID: 4442
		private XmlQualifiedName examplar = XmlQualifiedName.Empty;
	}
}
