using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000087 RID: 135
	internal sealed class RegionIterator : BaseRegionIterator
	{
		// Token: 0x06000678 RID: 1656 RVA: 0x0004AF24 File Offset: 0x0004A324
		internal RegionIterator(XmlBoundElement rowElement) : base(((XmlDataDocument)rowElement.OwnerDocument).Mapper)
		{
			this.rowElement = rowElement;
			this.currentNode = rowElement;
		}

		// Token: 0x06000679 RID: 1657 RVA: 0x0004AF58 File Offset: 0x0004A358
		internal override void Reset()
		{
			this.currentNode = this.rowElement;
		}

		// Token: 0x170000F1 RID: 241
		// (get) Token: 0x0600067A RID: 1658 RVA: 0x0004AF74 File Offset: 0x0004A374
		internal override XmlNode CurrentNode
		{
			get
			{
				return this.currentNode;
			}
		}

		// Token: 0x0600067B RID: 1659 RVA: 0x0004AF88 File Offset: 0x0004A388
		internal override bool Next()
		{
			ElementState elementState = this.rowElement.ElementState;
			XmlNode firstChild = this.currentNode.FirstChild;
			if (firstChild != null)
			{
				this.currentNode = firstChild;
				this.rowElement.ElementState = elementState;
				return true;
			}
			return this.NextRight();
		}

		// Token: 0x0600067C RID: 1660 RVA: 0x0004AFCC File Offset: 0x0004A3CC
		internal override bool NextRight()
		{
			if (this.currentNode == this.rowElement)
			{
				this.currentNode = null;
				return false;
			}
			ElementState elementState = this.rowElement.ElementState;
			XmlNode xmlNode = this.currentNode.NextSibling;
			if (xmlNode != null)
			{
				this.currentNode = xmlNode;
				this.rowElement.ElementState = elementState;
				return true;
			}
			xmlNode = this.currentNode;
			while (xmlNode != this.rowElement && xmlNode.NextSibling == null)
			{
				xmlNode = xmlNode.ParentNode;
			}
			if (xmlNode == this.rowElement)
			{
				this.currentNode = null;
				this.rowElement.ElementState = elementState;
				return false;
			}
			this.currentNode = xmlNode.NextSibling;
			this.rowElement.ElementState = elementState;
			return true;
		}

		// Token: 0x0600067D RID: 1661 RVA: 0x0004B078 File Offset: 0x0004A478
		internal bool NextInitialTextLikeNodes(out string value)
		{
			ElementState elementState = this.rowElement.ElementState;
			XmlNode firstChild = this.CurrentNode.FirstChild;
			value = RegionIterator.GetInitialTextFromNodes(ref firstChild);
			if (firstChild == null)
			{
				this.rowElement.ElementState = elementState;
				return this.NextRight();
			}
			this.currentNode = firstChild;
			this.rowElement.ElementState = elementState;
			return true;
		}

		// Token: 0x0600067E RID: 1662 RVA: 0x0004B0D0 File Offset: 0x0004A4D0
		private static string GetInitialTextFromNodes(ref XmlNode n)
		{
			string text = null;
			if (n != null)
			{
				while (n.NodeType == XmlNodeType.Whitespace)
				{
					n = n.NextSibling;
					if (n == null)
					{
						return string.Empty;
					}
				}
				if (XmlDataDocument.IsTextLikeNode(n) && (n.NextSibling == null || !XmlDataDocument.IsTextLikeNode(n.NextSibling)))
				{
					text = n.Value;
					n = n.NextSibling;
				}
				else
				{
					StringBuilder stringBuilder = new StringBuilder();
					while (n != null && XmlDataDocument.IsTextLikeNode(n))
					{
						if (n.NodeType != XmlNodeType.Whitespace)
						{
							stringBuilder.Append(n.Value);
						}
						n = n.NextSibling;
					}
					text = stringBuilder.ToString();
				}
			}
			if (text == null)
			{
				text = string.Empty;
			}
			return text;
		}

		// Token: 0x0400027A RID: 634
		private XmlBoundElement rowElement;

		// Token: 0x0400027B RID: 635
		private XmlNode currentNode;
	}
}
