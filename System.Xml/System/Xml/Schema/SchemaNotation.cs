using System;

namespace System.Xml.Schema
{
	// Token: 0x02000218 RID: 536
	internal sealed class SchemaNotation
	{
		// Token: 0x060019B7 RID: 6583 RVA: 0x0007BC14 File Offset: 0x0007AC14
		internal SchemaNotation(XmlQualifiedName name)
		{
			this.name = name;
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x060019B8 RID: 6584 RVA: 0x0007BC23 File Offset: 0x0007AC23
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x060019B9 RID: 6585 RVA: 0x0007BC2B File Offset: 0x0007AC2B
		// (set) Token: 0x060019BA RID: 6586 RVA: 0x0007BC33 File Offset: 0x0007AC33
		internal string SystemLiteral
		{
			get
			{
				return this.systemLiteral;
			}
			set
			{
				this.systemLiteral = value;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x060019BB RID: 6587 RVA: 0x0007BC3C File Offset: 0x0007AC3C
		// (set) Token: 0x060019BC RID: 6588 RVA: 0x0007BC44 File Offset: 0x0007AC44
		internal string Pubid
		{
			get
			{
				return this.pubid;
			}
			set
			{
				this.pubid = value;
			}
		}

		// Token: 0x04001005 RID: 4101
		internal const int SYSTEM = 0;

		// Token: 0x04001006 RID: 4102
		internal const int PUBLIC = 1;

		// Token: 0x04001007 RID: 4103
		private XmlQualifiedName name;

		// Token: 0x04001008 RID: 4104
		private string systemLiteral;

		// Token: 0x04001009 RID: 4105
		private string pubid;
	}
}
