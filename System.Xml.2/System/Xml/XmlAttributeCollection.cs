using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System.Xml
{
	// Token: 0x020000FB RID: 251
	public sealed class XmlAttributeCollection : XmlNamedNodeMap, ICollection, IEnumerable
	{
		// Token: 0x06001158 RID: 4440 RVA: 0x0004911C File Offset: 0x0004731C
		internal XmlAttributeCollection(XmlNode parent) : base(parent)
		{
		}

		// Token: 0x17000353 RID: 851
		[IndexerName("ItemOf")]
		public XmlAttribute this[int i]
		{
			get
			{
				XmlAttribute result;
				try
				{
					result = (XmlAttribute)this.nodes[i];
				}
				catch (ArgumentOutOfRangeException)
				{
					throw new IndexOutOfRangeException(Res.GetString("Xdom_IndexOutOfRange"));
				}
				return result;
			}
		}

		// Token: 0x17000354 RID: 852
		[IndexerName("ItemOf")]
		public XmlAttribute this[string name]
		{
			get
			{
				int hashCode = XmlName.GetHashCode(name);
				for (int i = 0; i < this.nodes.Count; i++)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
					if (hashCode == xmlAttribute.LocalNameHash && name == xmlAttribute.Name)
					{
						return xmlAttribute;
					}
				}
				return null;
			}
		}

		// Token: 0x17000355 RID: 853
		[IndexerName("ItemOf")]
		public XmlAttribute this[string localName, string namespaceURI]
		{
			get
			{
				int hashCode = XmlName.GetHashCode(localName);
				for (int i = 0; i < this.nodes.Count; i++)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
					if (hashCode == xmlAttribute.LocalNameHash && localName == xmlAttribute.LocalName && namespaceURI == xmlAttribute.NamespaceURI)
					{
						return xmlAttribute;
					}
				}
				return null;
			}
		}

		// Token: 0x0600115C RID: 4444 RVA: 0x00049228 File Offset: 0x00047428
		internal int FindNodeOffset(XmlAttribute node)
		{
			for (int i = 0; i < this.nodes.Count; i++)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
				if (xmlAttribute.LocalNameHash == node.LocalNameHash && xmlAttribute.Name == node.Name && xmlAttribute.NamespaceURI == node.NamespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600115D RID: 4445 RVA: 0x00049294 File Offset: 0x00047494
		internal int FindNodeOffsetNS(XmlAttribute node)
		{
			for (int i = 0; i < this.nodes.Count; i++)
			{
				XmlAttribute xmlAttribute = (XmlAttribute)this.nodes[i];
				if (xmlAttribute.LocalNameHash == node.LocalNameHash && xmlAttribute.LocalName == node.LocalName && xmlAttribute.NamespaceURI == node.NamespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600115E RID: 4446 RVA: 0x00049300 File Offset: 0x00047500
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

		// Token: 0x0600115F RID: 4447 RVA: 0x00049360 File Offset: 0x00047560
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

		// Token: 0x06001160 RID: 4448 RVA: 0x000493BC File Offset: 0x000475BC
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

		// Token: 0x06001161 RID: 4449 RVA: 0x00049428 File Offset: 0x00047628
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

		// Token: 0x06001162 RID: 4450 RVA: 0x000494D0 File Offset: 0x000476D0
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

		// Token: 0x06001163 RID: 4451 RVA: 0x0004957C File Offset: 0x0004777C
		public XmlAttribute Remove(XmlAttribute node)
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
			return null;
		}

		// Token: 0x06001164 RID: 4452 RVA: 0x000495BB File Offset: 0x000477BB
		public XmlAttribute RemoveAt(int i)
		{
			if (i < 0 || i >= this.Count)
			{
				return null;
			}
			return (XmlAttribute)this.RemoveNodeAt(i);
		}

		// Token: 0x06001165 RID: 4453 RVA: 0x000495D8 File Offset: 0x000477D8
		public void RemoveAll()
		{
			int i = this.Count;
			while (i > 0)
			{
				i--;
				this.RemoveAt(i);
			}
		}

		// Token: 0x06001166 RID: 4454 RVA: 0x00049600 File Offset: 0x00047800
		void ICollection.CopyTo(Array array, int index)
		{
			int i = 0;
			int count = this.Count;
			while (i < count)
			{
				array.SetValue(this.nodes[i], index);
				i++;
				index++;
			}
		}

		// Token: 0x17000356 RID: 854
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x00049638 File Offset: 0x00047838
		bool ICollection.IsSynchronized
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000357 RID: 855
		// (get) Token: 0x06001168 RID: 4456 RVA: 0x0004963B File Offset: 0x0004783B
		object ICollection.SyncRoot
		{
			get
			{
				return this;
			}
		}

		// Token: 0x17000358 RID: 856
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0004963E File Offset: 0x0004783E
		int ICollection.Count
		{
			get
			{
				return base.Count;
			}
		}

		// Token: 0x0600116A RID: 4458 RVA: 0x00049648 File Offset: 0x00047848
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

		// Token: 0x0600116B RID: 4459 RVA: 0x0004968C File Offset: 0x0004788C
		internal override XmlNode AddNode(XmlNode node)
		{
			this.RemoveDuplicateAttribute((XmlAttribute)node);
			XmlNode result = base.AddNode(node);
			this.InsertParentIntoElementIdAttrMap((XmlAttribute)node);
			return result;
		}

		// Token: 0x0600116C RID: 4460 RVA: 0x000496BC File Offset: 0x000478BC
		internal override XmlNode InsertNodeAt(int i, XmlNode node)
		{
			XmlNode result = base.InsertNodeAt(i, node);
			this.InsertParentIntoElementIdAttrMap((XmlAttribute)node);
			return result;
		}

		// Token: 0x0600116D RID: 4461 RVA: 0x000496E0 File Offset: 0x000478E0
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

		// Token: 0x0600116E RID: 4462 RVA: 0x0004973C File Offset: 0x0004793C
		internal void Detach(XmlAttribute attr)
		{
			attr.OwnerElement.Attributes.Remove(attr);
		}

		// Token: 0x0600116F RID: 4463 RVA: 0x00049750 File Offset: 0x00047950
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

		// Token: 0x06001170 RID: 4464 RVA: 0x000497DC File Offset: 0x000479DC
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

		// Token: 0x06001171 RID: 4465 RVA: 0x00049868 File Offset: 0x00047A68
		internal int RemoveDuplicateAttribute(XmlAttribute attr)
		{
			int num = base.FindNodeOffset(attr.LocalName, attr.NamespaceURI);
			if (num != -1)
			{
				XmlAttribute attr2 = (XmlAttribute)this.nodes[num];
				base.RemoveNodeAt(num);
				this.RemoveParentFromElementIdAttrMap(attr2);
			}
			return num;
		}

		// Token: 0x06001172 RID: 4466 RVA: 0x000498B0 File Offset: 0x00047AB0
		internal bool PrepareParentInElementIdAttrMap(string attrPrefix, string attrLocalName)
		{
			XmlElement xmlElement = this.parent as XmlElement;
			XmlDocument ownerDocument = this.parent.OwnerDocument;
			XmlName idinfoByElement = ownerDocument.GetIDInfoByElement(xmlElement.XmlName);
			return idinfoByElement != null && idinfoByElement.Prefix == attrPrefix && idinfoByElement.LocalName == attrLocalName;
		}

		// Token: 0x06001173 RID: 4467 RVA: 0x00049904 File Offset: 0x00047B04
		internal void ResetParentInElementIdAttrMap(string oldVal, string newVal)
		{
			XmlElement elem = this.parent as XmlElement;
			XmlDocument ownerDocument = this.parent.OwnerDocument;
			ownerDocument.RemoveElementWithId(oldVal, elem);
			ownerDocument.AddElementWithId(newVal, elem);
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0004993C File Offset: 0x00047B3C
		internal XmlAttribute InternalAppendAttribute(XmlAttribute node)
		{
			XmlNode xmlNode = base.AddNode(node);
			this.InsertParentIntoElementIdAttrMap(node);
			return (XmlAttribute)xmlNode;
		}
	}
}
