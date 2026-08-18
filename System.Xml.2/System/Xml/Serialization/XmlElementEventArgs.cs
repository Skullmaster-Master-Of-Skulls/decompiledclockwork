using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001C4 RID: 452
	public class XmlElementEventArgs : EventArgs
	{
		// Token: 0x06001F05 RID: 7941 RVA: 0x000A908D File Offset: 0x000A728D
		internal XmlElementEventArgs(XmlElement elem, int lineNumber, int linePosition, object o, string qnames)
		{
			this.elem = elem;
			this.o = o;
			this.qnames = qnames;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x1700065E RID: 1630
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x000A90BA File Offset: 0x000A72BA
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		// Token: 0x1700065F RID: 1631
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x000A90C2 File Offset: 0x000A72C2
		public XmlElement Element
		{
			get
			{
				return this.elem;
			}
		}

		// Token: 0x17000660 RID: 1632
		// (get) Token: 0x06001F08 RID: 7944 RVA: 0x000A90CA File Offset: 0x000A72CA
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x17000661 RID: 1633
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x000A90D2 File Offset: 0x000A72D2
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x17000662 RID: 1634
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x000A90DA File Offset: 0x000A72DA
		public string ExpectedElements
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

		// Token: 0x04000CF9 RID: 3321
		private object o;

		// Token: 0x04000CFA RID: 3322
		private XmlElement elem;

		// Token: 0x04000CFB RID: 3323
		private string qnames;

		// Token: 0x04000CFC RID: 3324
		private int lineNumber;

		// Token: 0x04000CFD RID: 3325
		private int linePosition;
	}
}
