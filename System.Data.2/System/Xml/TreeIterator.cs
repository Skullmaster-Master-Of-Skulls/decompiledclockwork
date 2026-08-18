using System;

namespace System.Xml
{
	// Token: 0x02000088 RID: 136
	internal sealed class TreeIterator : BaseTreeIterator
	{
		// Token: 0x0600067F RID: 1663 RVA: 0x0004B184 File Offset: 0x0004A584
		internal TreeIterator(XmlNode nodeTop) : base(((XmlDataDocument)nodeTop.OwnerDocument).Mapper)
		{
			this.nodeTop = nodeTop;
			this.currentNode = nodeTop;
		}

		// Token: 0x06000680 RID: 1664 RVA: 0x0004B1B8 File Offset: 0x0004A5B8
		internal override void Reset()
		{
			this.currentNode = this.nodeTop;
		}

		// Token: 0x170000F2 RID: 242
		// (get) Token: 0x06000681 RID: 1665 RVA: 0x0004B1D4 File Offset: 0x0004A5D4
		internal override XmlNode CurrentNode
		{
			get
			{
				return this.currentNode;
			}
		}

		// Token: 0x06000682 RID: 1666 RVA: 0x0004B1E8 File Offset: 0x0004A5E8
		internal override bool Next()
		{
			XmlNode firstChild = this.currentNode.FirstChild;
			if (firstChild != null)
			{
				this.currentNode = firstChild;
				return true;
			}
			return this.NextRight();
		}

		// Token: 0x06000683 RID: 1667 RVA: 0x0004B214 File Offset: 0x0004A614
		internal override bool NextRight()
		{
			if (this.currentNode == this.nodeTop)
			{
				this.currentNode = null;
				return false;
			}
			XmlNode xmlNode = this.currentNode.NextSibling;
			if (xmlNode != null)
			{
				this.currentNode = xmlNode;
				return true;
			}
			xmlNode = this.currentNode;
			while (xmlNode != this.nodeTop && xmlNode.NextSibling == null)
			{
				xmlNode = xmlNode.ParentNode;
			}
			if (xmlNode == this.nodeTop)
			{
				this.currentNode = null;
				return false;
			}
			this.currentNode = xmlNode.NextSibling;
			return true;
		}

		// Token: 0x0400027C RID: 636
		private XmlNode nodeTop;

		// Token: 0x0400027D RID: 637
		private XmlNode currentNode;
	}
}
