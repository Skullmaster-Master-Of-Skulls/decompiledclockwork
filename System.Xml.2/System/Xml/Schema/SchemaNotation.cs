using System;

namespace System.Xml.Schema
{
	// Token: 0x02000261 RID: 609
	internal sealed class SchemaNotation
	{
		// Token: 0x06002489 RID: 9353 RVA: 0x000C83F0 File Offset: 0x000C65F0
		internal SchemaNotation(XmlQualifiedName name)
		{
			this.name = name;
		}

		// Token: 0x1700080B RID: 2059
		// (get) Token: 0x0600248A RID: 9354 RVA: 0x000C83FF File Offset: 0x000C65FF
		internal XmlQualifiedName Name
		{
			get
			{
				return this.name;
			}
		}

		// Token: 0x1700080C RID: 2060
		// (get) Token: 0x0600248B RID: 9355 RVA: 0x000C8407 File Offset: 0x000C6607
		// (set) Token: 0x0600248C RID: 9356 RVA: 0x000C840F File Offset: 0x000C660F
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

		// Token: 0x1700080D RID: 2061
		// (get) Token: 0x0600248D RID: 9357 RVA: 0x000C8418 File Offset: 0x000C6618
		// (set) Token: 0x0600248E RID: 9358 RVA: 0x000C8420 File Offset: 0x000C6620
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

		// Token: 0x04000FD8 RID: 4056
		internal const int SYSTEM = 0;

		// Token: 0x04000FD9 RID: 4057
		internal const int PUBLIC = 1;

		// Token: 0x04000FDA RID: 4058
		private XmlQualifiedName name;

		// Token: 0x04000FDB RID: 4059
		private string systemLiteral;

		// Token: 0x04000FDC RID: 4060
		private string pubid;
	}
}
