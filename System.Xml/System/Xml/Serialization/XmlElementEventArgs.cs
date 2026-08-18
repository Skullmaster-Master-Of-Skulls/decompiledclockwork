using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000343 RID: 835
	public class XmlElementEventArgs : EventArgs
	{
		// Token: 0x060028BA RID: 10426 RVA: 0x000D1E41 File Offset: 0x000D0E41
		internal XmlElementEventArgs(XmlElement elem, int lineNumber, int linePosition, object o, string qnames)
		{
			this.elem = elem;
			this.o = o;
			this.qnames = qnames;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x170009A4 RID: 2468
		// (get) Token: 0x060028BB RID: 10427 RVA: 0x000D1E6E File Offset: 0x000D0E6E
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x000D1E76 File Offset: 0x000D0E76
		public XmlElement Element
		{
			get
			{
				return this.elem;
			}
		}

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x060028BD RID: 10429 RVA: 0x000D1E7E File Offset: 0x000D0E7E
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x000D1E86 File Offset: 0x000D0E86
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x060028BF RID: 10431 RVA: 0x000D1E8E File Offset: 0x000D0E8E
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

		// Token: 0x04001692 RID: 5778
		private object o;

		// Token: 0x04001693 RID: 5779
		private XmlElement elem;

		// Token: 0x04001694 RID: 5780
		private string qnames;

		// Token: 0x04001695 RID: 5781
		private int lineNumber;

		// Token: 0x04001696 RID: 5782
		private int linePosition;
	}
}
