using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x020000CC RID: 204
	public sealed class XmlAttributeCollection : XmlNamedNodeMap, ICollection, IEnumerable
	{
		// Token: 0x06000C20 RID: 3104 RVA: 0x00037221 File Offset: 0x00036221
		internal XmlAttributeCollection(XmlNode parent) : base(parent)
		{
		}

		// Token: 0x170002B7 RID: 695
		[IndexerName("ItemOf")]
		public XmlAttribute this[int i]
		{
			get
			{
				XmlAttribute result;
				try
				{
					result = (XmlAttribute)base.Nodes[i];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw new IndexOutOfRangeException(Res.GetString("Xdom_IndexOutOfRange"));
				}
				return result;
			}
		}

		// Token: 0x170002B8 RID: 696
		[IndexerName("ItemOf")]
		public XmlAttribute this[string name]
		{
			get
			{
				ArrayList nodes = base.Nodes;
				int hashCode = XmlName.GetHashCode(name);
				for (int i = 0; i < nodes.Count; i++)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)nodes[i];
					if (hashCode == xmlAttribute.LocalNameHash && name == xmlAttribute.Name)
					{
						return xmlAttribute;
					}
				}
				return null;
			}
		}

		// Token: 0x170002B9 RID: 697
		[IndexerName("ItemOf")]
		public XmlAttribute this[string localName, string namespaceURI]
		{
			get
			{
				ArrayList nodes = base.Nodes;
				int hashCode = XmlName.GetHashCode(localName);
				for (int i = 0; i < nodes.Count; i++)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)nodes[i];
					if (hashCode == xmlAttribute.LocalNameHash && localName == xmlAttribute.LocalName && namespaceURI == xmlAttribute.NamespaceURI)
					{
						return xmlAttribute;
					}
				}
				return null;
			}
		}

		// Token: 0x06000C24 RID: 3108 RVA: 0x00037328 File Offset: 0x00036328
		internal int FindNodeOffset(XmlAttribute node)
		{
			ArrayList nodes = base.Nodes;
			for (int i = 0; i < nodes.Count; i++)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)nodes[i];
				if (xmlAttribute.LocalNameHash == node.LocalNameHash && xmlAttribute.Name == node.Name && xmlAttribute.NamespaceURI == node.NamespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C25 RID: 3109 RVA: 0x00037394 File Offset: 0x00036394
		internal int FindNodeOffsetNS(XmlAttribute node)
		{
			ArrayList nodes = base.Nodes;
			for (int i = 0; i < nodes.Count; i++)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)nodes[i];
				if (xmlAttribute.LocalNameHash == node.LocalNameHash && xmlAttribute.LocalName == node.LocalName && xmlAttribute.NamespaceURI == node.NamespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C26 RID: 3110 RVA: 0x00037400 File Offset: 0x00036400
		public override XmlNode SetNamedItem(XmlNode node)
		{
			if (node != null && !(node is XmlAttribute))
			{
				throw new ArgumentException(Res.GetString("Xdom_AttrCol_Object"));
			}
			int num = base.FindNodeOffset(node.LocalName, node.NamespaceURI);
			if (num == -1)
			{
				return this.InternalAppendAttribute((XmlAttribute)node);
			}
			XmlNode result = base.RemoveNodeAt(num);
			this.InsertNodeAt(num, node);
			return result;
		}

		// Token: 0x06000C27 RID: 3111 RVA: 0x00037460 File Offset: 0x00036460
		public XmlAttribute Prepend(XmlAttribute node)
		{
			if (node.OwnerDocument != null && node.OwnerDocument != this.parent.OwnerDocument)
			{
				throw new ArgumentException(Res.GetString("Xdom_NamedNode_Context"));
			}
			if (node.OwnerElement != null)
			{
				this.Detach(node);
			}
			this.RemoveDuplicateAttribute(node);
			this.InsertNodeAt(0, node);
			return node;
		}

		// Token: 0x06000C28 RID: 3112 RVA: 0x000374BC File Offset: 0x000364BC
		public XmlAttribute Append(XmlAttribute node)
		{
			XmlDocument ownerDocument = node.OwnerDocument;
			if (ownerDocument == null || !ownerDocument.IsLoading)
			{
				if (ownerDocument != null && ownerDocument != this.parent.OwnerDocument)
				{
					throw new ArgumentException(Res.GetString("Xdom_NamedNode_Context"));
				}
				if (node.OwnerElement != null)
				{
					this.Detach(node);
				}
				this.AddNode(node);
			}
			else
			{
				base.AddNodeForLoad(node, ownerDocument);
				this.InsertParentIntoElementIdAttrMap(node);
			}
			return node;
		}

		// Token: 0x06000C29 RID: 3113 RVA: 0x00037528 File Offset: 0x00036528
		public XmlAttribute InsertBefore(XmlAttribute newNode, XmlAttribute refNode)
		{
			if (newNode == refNode)
			{
				return newNode;
			}
			if (refNode == null)
			{
				return this.Append(newNode);
			}
			if (refNode.OwnerElement != this.parent)
			{
				throw new ArgumentException(Res.GetString("Xdom_AttrCol_Insert"));
			}
			if (newNode.OwnerDocument != null && newNode.OwnerDocument != this.parent.OwnerDocument)
			{
				throw new ArgumentException(Res.GetString("Xdom_NamedNode_Context"));
			}
			if (newNode.OwnerElement != null)
			{
				this.Detach(newNode);
			}
			int num = base.FindNodeOffset(refNode.LocalName, refNode.NamespaceURI);
			int num2 = this.RemoveDuplicateAttribute(newNode);
			if (num2 >= 0 && num2 < num)
			{
				num--;
			}
			this.InsertNodeAt(num, newNode);
			return newNode;
		}

		// Token: 0x06000C2A RID: 3114 RVA: 0x000375D0 File Offset: 0x000365D0
		public XmlAttribute InsertAfter(XmlAttribute newNode, XmlAttribute refNode)
		{
			if (newNode == refNode)
			{
				return newNode;
			}
			if (refNode == null)
			{
				return this.Prepend(newNode);
			}
			if (refNode.OwnerElement != this.parent)
			{
				throw new ArgumentException(Res.GetString("Xdom_AttrCol_Insert"));
			}
			if (newNode.OwnerDocument != null && newNode.OwnerDocument != this.parent.OwnerDocument)
			{
				throw new ArgumentException(Res.GetString("Xdom_NamedNode_Context"));
			}
			if (newNode.OwnerElement != null)
			{
				this.Detach(newNode);
			}
			int num = base.FindNodeOffset(refNode.LocalName, refNode.NamespaceURI);
			int num2 = this.RemoveDuplicateAttribute(newNode);
			if (num2 >= 0 && num2 < num)
			{
				num--;
			}
			this.InsertNodeAt(num + 1, newNode);
			return newNode;
		}

		// Token: 0x06000C2B RID: 3115 RVA: 0x0003767C File Offset: 0x0003667C
		public XmlAttribute Remove(XmlAttribute node)
		{
			if (this.nodes != null)
			{
				int count = this.nodes.Count;
				for (int i = 0; i < count; i++)
				{
					if (this.nodes[i] == node)
					{
						this.RemoveNodeAt(i);
						return node;
					}
				}
			}
			return null;
		}

		// Token: 0x06000C2C RID: 3116 RVA: 0x000376C3 File Offset: 0x000366C3
		public XmlAttribute RemoveAt(int i)
		{
			if (i < 0 || i >= this.Count || this.nodes == null)
			{
				return null;
			}
			return (XmlAttribute)this.RemoveNodeAt(i);
		}

		// Token: 0x06000C2D RID: 3117 RVA: 0x000376E8 File Offset: 0x000366E8
		public void RemoveAll()
		{
			int i = this.Count;
			while (i > 0)
			{
				i--;
				this.RemoveAt(i);
			}
		}

		// Token: 0x06000C2E RID: 3118 RVA: 0x00037710 File Offset: 0x00036710
		void ICollection.CopyTo(Array array, int index)
		{
			int i = 0;
			int count = base.Nodes.Count;
			while (i < count)
			{
				array.SetValue(this.nodes[i], index);
				i++;
				index++;
			}
		}

		// Token: 0x170002BA RID: 698
		// (get) Token: 0x06000C2F RID: 3119 RVA: 0x0003774D File Offset: 0x0003674D
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170002BB RID: 699
		// (get) Token: 0x06000C30 RID: 3120 RVA: 0x00037750 File Offset: 0x00036750
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000C31 RID: 3121 RVA: 0x00037753 File Offset: 0x00036753
		int ICollection.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x06000C32 RID: 3122 RVA: 0x0003775C File Offset: 0x0003675C
		public void CopyTo(XmlAttribute[] array, int index)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				array[index] = (XmlAttribute)((XmlNode)this.nodes[i]).CloneNode(true);
				i++;
				index++;
			}
		}

		// Token: 0x06000C33 RID: 3123 RVA: 0x000377A0 File Offset: 0x000367A0
		internal override XmlNode AddNode(XmlNode node)
		{
			this.RemoveDuplicateAttribute((XmlAttribute)node);
			XmlNode result = base.AddNode(node);
			this.InsertParentIntoElementIdAttrMap((XmlAttribute)node);
			return result;
		}

		// Token: 0x06000C34 RID: 3124 RVA: 0x000377D0 File Offset: 0x000367D0
		internal override XmlNode InsertNodeAt(int i, XmlNode node)
		{
			XmlNode result = base.InsertNodeAt(i, node);
			this.InsertParentIntoElementIdAttrMap((XmlAttribute)node);
			return result;
		}

		// Token: 0x06000C35 RID: 3125 RVA: 0x000377F4 File Offset: 0x000367F4
		internal override XmlNode RemoveNodeAt(int i)
		{
			XmlNode xmlNode = base.RemoveNodeAt(i);
			this.RemoveParentFromElementIdAttrMap((XmlAttribute)xmlNode);
			XmlAttribute defaultAttribute = this.parent.OwnerDocument.GetDefaultAttribute((XmlElement)this.parent, xmlNode.Prefix, xmlNode.LocalName, xmlNode.NamespaceURI);
			if (defaultAttribute != null)
			{
				this.InsertNodeAt(i, defaultAttribute);
			}
			return xmlNode;
		}

		// Token: 0x06000C36 RID: 3126 RVA: 0x00037850 File Offset: 0x00036850
		internal void Detach(XmlAttribute attr)
		{
			attr.OwnerElement.Attributes.Remove(attr);
		}

		// Token: 0x06000C37 RID: 3127 RVA: 0x00037864 File Offset: 0x00036864
		internal void InsertParentIntoElementIdAttrMap(XmlAttribute attr)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			if (xmlElement != null)
			{
				if (this.parent.OwnerDocument == null)
				{
					return;
				}
				XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
				if (idinfoByElement != null && idinfoByElement.Prefix == attr.XmlName.Prefix && idinfoByElement.LocalName == attr.XmlName.LocalName)
				{
					this.parent.OwnerDocument.AddElementWithId(attr.Value, xmlElement);
				}
			}
		}

		// Token: 0x06000C38 RID: 3128 RVA: 0x000378F0 File Offset: 0x000368F0
		internal void RemoveParentFromElementIdAttrMap(XmlAttribute attr)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			if (xmlElement != null)
			{
				if (this.parent.OwnerDocument == null)
				{
					return;
				}
				XmlName idinfoByElement = this.parent.OwnerDocument.GetIDInfoByElement(xmlElement.XmlName);
				if (idinfoByElement != null && idinfoByElement.Prefix == attr.XmlName.Prefix && idinfoByElement.LocalName == attr.XmlName.LocalName)
				{
					this.parent.OwnerDocument.RemoveElementWithId(attr.Value, xmlElement);
				}
			}
		}

		// Token: 0x06000C39 RID: 3129 RVA: 0x0003797C File Offset: 0x0003697C
		internal int RemoveDuplicateAttribute(XmlAttribute attr)
		{
			int num = base.FindNodeOffset(attr.LocalName, attr.NamespaceURI);
			if (num != -1)
			{
				XmlAttribute attr2 = (XmlAttribute)base.Nodes[num];
				base.RemoveNodeAt(num);
				this.RemoveParentFromElementIdAttrMap(attr2);
			}
			return num;
		}

		// Token: 0x06000C3A RID: 3130 RVA: 0x000379C4 File Offset: 0x000369C4
		internal bool PrepareParentInElementIdAttrMap(string attrPrefix, string attrLocalName)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			XmlDocument ownerDocument = this.parent.OwnerDocument;
			XmlName idinfoByElement = ownerDocument.GetIDInfoByElement(xmlElement.XmlName);
			return idinfoByElement != null && idinfoByElement.Prefix == attrPrefix && idinfoByElement.LocalName == attrLocalName;
		}

		// Token: 0x06000C3B RID: 3131 RVA: 0x00037A18 File Offset: 0x00036A18
		internal void ResetParentInElementIdAttrMap(string oldVal, string newVal)
		{
			XmlElement elem = this.parent as XmlElement;
			XmlDocument ownerDocument = this.parent.OwnerDocument;
			ownerDocument.RemoveElementWithId(oldVal, elem);
			ownerDocument.AddElementWithId(newVal, elem);
		}

		// Token: 0x06000C3C RID: 3132 RVA: 0x00037A50 File Offset: 0x00036A50
		internal XmlAttribute InternalAppendAttribute(XmlAttribute node)
		{
			XmlNode xmlNode = base.AddNode(node);
			this.InsertParentIntoElementIdAttrMap(node);
			return (XmlAttribute)xmlNode;
		}
	}
}
