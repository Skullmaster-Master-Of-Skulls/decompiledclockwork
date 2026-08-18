using System;

namespace System.Xml.Serialization
{
	// Token: 0x020001C6 RID: 454
	public class XmlNodeEventArgs : EventArgs
	{
		// Token: 0x06001F0F RID: 7951 RVA: 0x000A90F0 File Offset: 0x000A72F0
		internal XmlNodeEventArgs(XmlNode xmlNode, int lineNumber, int linePosition, object o)
		{
			this.o = o;
			this.xmlNode = xmlNode;
			this.lineNumber = lineNumber;
			this.linePosition = linePosition;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001F10 RID: 7952 RVA: 0x000A9115 File Offset: 0x000A7315
		public object ObjectBeingDeserialized
		{
			get
			{
				return this.o;
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001F11 RID: 7953 RVA: 0x000A911D File Offset: 0x000A731D
		public XmlNodeType NodeType
		{
			get
			{
				return this.xmlNode.NodeType;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001F12 RID: 7954 RVA: 0x000A912A File Offset: 0x000A732A
		public string Name
		{
			get
			{
				return this.xmlNode.Name;
			}
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001F13 RID: 7955 RVA: 0x000A9137 File Offset: 0x000A7337
		public string LocalName
		{
			get
			{
				return this.xmlNode.LocalName;
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001F14 RID: 7956 RVA: 0x000A9144 File Offset: 0x000A7344
		public string NamespaceURI
		{
			get
			{
				return this.xmlNode.NamespaceURI;
			}
		}

		// Token: 0x17000668 RID: 1640
		// (get) Token: 0x06001F15 RID: 7957 RVA: 0x000A9151 File Offset: 0x000A7351
		public string Text
		{
			get
			{
				return this.xmlNode.Value;
			}
		}

		// Token: 0x17000669 RID: 1641
		// (get) Token: 0x06001F16 RID: 7958 RVA: 0x000A915E File Offset: 0x000A735E
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x1700066A RID: 1642
		// (get) Token: 0x06001F17 RID: 7959 RVA: 0x000A9166 File Offset: 0x000A7366
		public int LinePosition
		{
			get
			{
				return this.linePosition;
			}
		}

		// Token: 0x04000CFE RID: 3326
		private object o;

		// Token: 0x04000CFF RID: 3327
		private XmlNode xmlNode;

		// Token: 0x04000D00 RID: 3328
		private int lineNumber;

		// Token: 0x04000D01 RID: 3329
		private int linePosition;
	}
}
