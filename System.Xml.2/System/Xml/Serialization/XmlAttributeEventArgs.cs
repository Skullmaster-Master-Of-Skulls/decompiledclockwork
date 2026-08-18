using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001C2 RID: 450
	public class XmlAttributeEventArgs : EventArgs
	{
		// Token: 0x06001EFB RID: 7931 RVA: 0x000A902A File Offset: 0x000A722A
		internal XmlAttributeEventArgs(XmlAttribute attr, int lineNumber, int linePosition, object o, string qnames)
		{
			this.attr = attr;
			this.o = o;
			this.qnames = qnames;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x17000659 RID: 1625
		// (get) Token: 0x06001EFC RID: 7932 RVA: 0x000A9057 File Offset: 0x000A7257
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		// Token: 0x1700065A RID: 1626
		// (get) Token: 0x06001EFD RID: 7933 RVA: 0x000A905F File Offset: 0x000A725F
		public XmlAttribute Attr
		{
			get
			{
				return this.attr;
			}
		}

		// Token: 0x1700065B RID: 1627
		// (get) Token: 0x06001EFE RID: 7934 RVA: 0x000A9067 File Offset: 0x000A7267
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x1700065C RID: 1628
		// (get) Token: 0x06001EFF RID: 7935 RVA: 0x000A906F File Offset: 0x000A726F
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x1700065D RID: 1629
		// (get) Token: 0x06001F00 RID: 7936 RVA: 0x000A9077 File Offset: 0x000A7277
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

		// Token: 0x04000CF4 RID: 3316
		private object o;

		// Token: 0x04000CF5 RID: 3317
		private XmlAttribute attr;

		// Token: 0x04000CF6 RID: 3318
		private string qnames;

		// Token: 0x04000CF7 RID: 3319
		private int lineNumber;

		// Token: 0x04000CF8 RID: 3320
		private int linePosition;
	}
}
