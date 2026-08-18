using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000341 RID: 833
	public class XmlAttributeEventArgs : EventArgs
	{
		// Token: 0x060028B0 RID: 10416 RVA: 0x000D1DDE File Offset: 0x000D0DDE
		internal XmlAttributeEventArgs(XmlAttribute attr, int lineNumber, int linePosition, object o, string qnames)
		{
			this.attr = attr;
			this.o = o;
			this.qnames = qnames;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x1700099F RID: 2463
		// (get) Token: 0x060028B1 RID: 10417 RVA: 0x000D1E0B File Offset: 0x000D0E0B
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		// Token: 0x170009A0 RID: 2464
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x000D1E13 File Offset: 0x000D0E13
		public XmlAttribute Attr
		{
			get
			{
				return this.attr;
			}
		}

		// Token: 0x170009A1 RID: 2465
		// (get) Token: 0x060028B3 RID: 10419 RVA: 0x000D1E1B File Offset: 0x000D0E1B
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x170009A2 RID: 2466
		// (get) Token: 0x060028B4 RID: 10420 RVA: 0x000D1E23 File Offset: 0x000D0E23
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x170009A3 RID: 2467
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x000D1E2B File Offset: 0x000D0E2B
		public string ExpectedAttributes
		{
			get
			{
				if (this.qnames != null)
				{
					return this.qnames;
				}
				return string.Empty;
			}
		}

		// Token: 0x0400168D RID: 5773
		private object o;

		// Token: 0x0400168E RID: 5774
		private XmlAttribute attr;

		// Token: 0x0400168F RID: 5775
		private string qnames;

		// Token: 0x04001690 RID: 5776
		private int lineNumber;

		// Token: 0x04001691 RID: 5777
		private int linePosition;
	}
}
