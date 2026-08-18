using System;

namespace System.Xml.Schema
{
	// Token: 0x02000200 RID: 512
	internal class XsdSimpleValue
	{
		// Token: 0x06002112 RID: 8466 RVA: 0x000B4A70 File Offset: 0x000B2C70
		public XsdSimpleValue(XmlSchemaSimpleType st, object value)
		{
			this.xmlType = st;
			this.typedValue = value;
		}

		// Token: 0x170006D3 RID: 1747
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x000B4A86 File Offset: 0x000B2C86
		public XmlSchemaSimpleType XmlType
		{
			get
			{
				return this.xmlType;
			}
		}

		// Token: 0x170006D4 RID: 1748
		// (get) Token: 0x06002114 RID: 8468 RVA: 0x000B4A8E File Offset: 0x000B2C8E
		public object TypedValue
		{
			get
			{
				return this.typedValue;
			}
		}

		// Token: 0x04000DE8 RID: 3560
		private XmlSchemaSimpleType xmlType;

		// Token: 0x04000DE9 RID: 3561
		private object typedValue;
	}
}
