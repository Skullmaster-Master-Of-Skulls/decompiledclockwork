using System;

namespace System.Xml.Schema
{
	// Token: 0x0200024F RID: 591
	public interface IXmlSchemaInfo
	{
		// Token: 0x1700079D RID: 1949
		// (get) Token: 0x06002308 RID: 8968
		XmlSchemaValidity Validity { get; }

		// Token: 0x1700079E RID: 1950
		// (get) Token: 0x06002309 RID: 8969
		bool IsDefault { get; }

		// Token: 0x1700079F RID: 1951
		// (get) Token: 0x0600230A RID: 8970
		bool IsNil { get; }

		// Token: 0x170007A0 RID: 1952
		// (get) Token: 0x0600230B RID: 8971
		XmlSchemaSimpleType MemberType { get; }

		// Token: 0x170007A1 RID: 1953
		// (get) Token: 0x0600230C RID: 8972
		XmlSchemaType SchemaType { get; }

		// Token: 0x170007A2 RID: 1954
		// (get) Token: 0x0600230D RID: 8973
		XmlSchemaElement SchemaElement { get; }

		// Token: 0x170007A3 RID: 1955
		// (get) Token: 0x0600230E RID: 8974
		XmlSchemaAttribute SchemaAttribute { get; }
	}
}
