using System;

namespace System.Xml.Schema
{
	// Token: 0x020000AF RID: 175
	public interface IXmlSchemaInfo
	{
		// Token: 0x170001F5 RID: 501
		// (get) Token: 0x06000988 RID: 2440
		XmlSchemaValidity Validity { get; }

		// Token: 0x170001F6 RID: 502
		// (get) Token: 0x06000989 RID: 2441
		bool IsDefault { get; }

		// Token: 0x170001F7 RID: 503
		// (get) Token: 0x0600098A RID: 2442
		bool IsNil { get; }

		// Token: 0x170001F8 RID: 504
		// (get) Token: 0x0600098B RID: 2443
		XmlSchemaSimpleType MemberType { get; }

		// Token: 0x170001F9 RID: 505
		// (get) Token: 0x0600098C RID: 2444
		XmlSchemaType SchemaType { get; }

		// Token: 0x170001FA RID: 506
		// (get) Token: 0x0600098D RID: 2445
		XmlSchemaElement SchemaElement { get; }

		// Token: 0x170001FB RID: 507
		// (get) Token: 0x0600098E RID: 2446
		XmlSchemaAttribute SchemaAttribute { get; }
	}
}
