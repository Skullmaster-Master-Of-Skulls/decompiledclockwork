using System;

namespace System.Xml.Schema
{
	// Token: 0x020001A8 RID: 424
	internal class XsdSimpleValue
	{
		// Token: 0x060015C0 RID: 5568 RVA: 0x00060B6C File Offset: 0x0005FB6C
		public XsdSimpleValue(XmlSchemaSimpleType st, object value)
		{
			this.xmlType = st;
			this.typedValue = value;
		}

		// Token: 0x17000531 RID: 1329
		// (get) Token: 0x060015C1 RID: 5569 RVA: 0x00060B82 File Offset: 0x0005FB82
		public XmlSchemaSimpleType XmlType
		{
			get
			{
				return this.xmlType;
			}
		}

		// Token: 0x17000532 RID: 1330
		// (get) Token: 0x060015C2 RID: 5570 RVA: 0x00060B8A File Offset: 0x0005FB8A
		public object TypedValue
		{
			get
			{
				return this.typedValue;
			}
		}

		// Token: 0x04000CF3 RID: 3315
		private XmlSchemaSimpleType xmlType;

		// Token: 0x04000CF4 RID: 3316
		private object typedValue;
	}
}
