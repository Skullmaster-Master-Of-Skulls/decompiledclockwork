using System;

namespace System.Xml
{
	// Token: 0x02000389 RID: 905
	internal sealed class TreeIterator : BaseTreeIterator
	{
		// Token: 0x06002FDF RID: 12255 RVA: 0x002D66D8 File Offset: 0x002D5AD8
		internal TreeIterator(XmlNode nodeTop) : base(((XmlDataDocument)nodeTop.OwnerDocument).Mapper)
		{
			this.nodeTop = nodeTop;
			this.currentNode = nodeTop;
		}

		// Token: 0x06002FE0 RID: 12256 RVA: 0x002D6718 File Offset: 0x002D5B18
		internal override void Reset()
		{
			this.currentNode = this.nodeTop;
		}

		// Token: 0x1700078B RID: 1931
		// (get) Token: 0x06002FE1 RID: 12257 RVA: 0x002D6738 File Offset: 0x002D5B38
		internal override XmlNode CurrentNode
		{
			get
			{
				return this.currentNode;
			}
		}

		// Token: 0x06002FE2 RID: 12258 RVA: 0x002D6758 File Offset: 0x002D5B58
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

		// Token: 0x06002FE3 RID: 12259 RVA: 0x002D6788 File Offset: 0x002D5B88
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

		// Token: 0x04001DA9 RID: 7593
		private XmlNode nodeTop;

		// Token: 0x04001DAA RID: 7594
		private XmlNode currentNode;
	}
}
