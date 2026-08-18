using System;

namespace System.Xml.Linq
{
	// Token: 0x0200001B RID: 27
	[__DynamicallyInvokable]
	public class XCData : XText
	{
		// Token: 0x060000E0 RID: 224 RVA: 0x00005058 File Offset: 0x00003258
		[__DynamicallyInvokable]
		public XCData(string value) : base(value)
		{
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00005061 File Offset: 0x00003261
		[__DynamicallyInvokable]
		public XCData(XCData other) : base(other)
		{
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x0000506A File Offset: 0x0000326A
		internal XCData(XmlReader r) : base(r)
		{
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000E3 RID: 227 RVA: 0x00005073 File Offset: 0x00003273
		[__DynamicallyInvokable]
		public override XmlNodeType NodeType
		{
			[__DynamicallyInvokable]
			get
			{
				return XmlNodeType.CDATA;
			}
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005076 File Offset: 0x00003276
		[__DynamicallyInvokable]
		public override void WriteTo(XmlWriter writer)
		{
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			writer.WriteCData(this.text);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005092 File Offset: 0x00003292
		internal override XNode CloneNode()
		{
			return new XCData(this);
		}
	}
}
