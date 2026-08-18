using System;
using System.Text;

namespace System.Xml
{
	// Token: 0x02000388 RID: 904
	internal sealed class RegionIterator : BaseRegionIterator
	{
		// Token: 0x06002FD8 RID: 12248 RVA: 0x002D6438 File Offset: 0x002D5838
		internal RegionIterator(XmlBoundElement rowElement) : base(((XmlDataDocument)rowElement.OwnerDocument).Mapper)
		{
			this.rowElement = rowElement;
			this.currentNode = rowElement;
		}

		// Token: 0x06002FD9 RID: 12249 RVA: 0x002D6478 File Offset: 0x002D5878
		internal override void Reset()
		{
			this.currentNode = this.rowElement;
		}

		// Token: 0x1700078A RID: 1930
		// (get) Token: 0x06002FDA RID: 12250 RVA: 0x002D6498 File Offset: 0x002D5898
		internal override XmlNode CurrentNode
		{
			get
			{
				return this.currentNode;
			}
		}

		// Token: 0x06002FDB RID: 12251 RVA: 0x002D64B8 File Offset: 0x002D58B8
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

		// Token: 0x06002FDC RID: 12252 RVA: 0x002D6508 File Offset: 0x002D5908
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

		// Token: 0x06002FDD RID: 12253 RVA: 0x002D65B8 File Offset: 0x002D59B8
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

		// Token: 0x06002FDE RID: 12254 RVA: 0x002D6618 File Offset: 0x002D5A18
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

		// Token: 0x04001DA7 RID: 7591
		private XmlBoundElement rowElement;

		// Token: 0x04001DA8 RID: 7592
		private XmlNode currentNode;
	}
}
