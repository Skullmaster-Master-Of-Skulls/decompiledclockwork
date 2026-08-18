using System;

namespace System.Xml.Serialization
{
	// Token: 0x02000345 RID: 837
	public class XmlNodeEventArgs : EventArgs
	{
		// Token: 0x060028C4 RID: 10436 RVA: 0x000D1EA4 File Offset: 0x000D0EA4
		internal XmlNodeEventArgs(XmlNode xmlNode, int lineNumber, int linePosition, object o)
		{
			this.o = o;
			this.xmlNode = xmlNode;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x060028C5 RID: 10437 RVA: 0x000D1EC9 File Offset: 0x000D0EC9
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x000D1ED1 File Offset: 0x000D0ED1
		public XmlNodeType NodeType
		{
			get
			{
				return this.xmlNode.NodeType;
			}
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060028C7 RID: 10439 RVA: 0x000D1EDE File Offset: 0x000D0EDE
		public string Name
		{
			get
			{
				return this.xmlNode.Name;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060028C8 RID: 10440 RVA: 0x000D1EEB File Offset: 0x000D0EEB
		public string LocalName
		{
			get
			{
				return this.xmlNode.LocalName;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060028C9 RID: 10441 RVA: 0x000D1EF8 File Offset: 0x000D0EF8
		public string NamespaceURI
		{
			get
			{
				return this.xmlNode.NamespaceURI;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060028CA RID: 10442 RVA: 0x000D1F05 File Offset: 0x000D0F05
		public string Text
		{
			get
			{
				return this.xmlNode.Value;
			}
		}

		// Token: 0x170009AF RID: 2479
		// (get) Token: 0x060028CB RID: 10443 RVA: 0x000D1F12 File Offset: 0x000D0F12
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x170009B0 RID: 2480
		// (get) Token: 0x060028CC RID: 10444 RVA: 0x000D1F1A File Offset: 0x000D0F1A
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x04001697 RID: 5783
		private object o;

		// Token: 0x04001698 RID: 5784
		private XmlNode xmlNode;

		// Token: 0x04001699 RID: 5785
		private int lineNumber;

		// Token: 0x0400169A RID: 5786
		private int linePosition;
	}
}
