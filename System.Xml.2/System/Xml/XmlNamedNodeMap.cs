using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x0200006C RID: 108
	public class XmlNamedNodeMap : IEnumerable
	{
		// Token: 0x060003B0 RID: 944 RVA: 0x0000EAAC File Offset: 0x0000CCAC
		internal XmlNamedNodeMap(XmlNode parent)
		{
			this.parent = parent;
		}

		// Token: 0x060003B1 RID: 945 RVA: 0x0000EABC File Offset: 0x0000CCBC
		public virtual XmlNode GetNamedItem(string name)
		{
			int num = this.FindNodeOffset(name);
			if (num >= 0)
			{
				return (XmlNode)this.nodes[num];
			}
			return null;
		}

		// Token: 0x060003B2 RID: 946 RVA: 0x0000EAE8 File Offset: 0x0000CCE8
		public virtual XmlNode SetNamedItem(XmlNode node)
		{
			if (node == null)
			{
				return null;
			}
			int num = this.FindNodeOffset(node.LocalName, node.NamespaceURI);
			if (num == -1)
			{
				this.AddNode(node);
				return null;
			}
			return this.ReplaceNodeAt(num, node);
		}

		// Token: 0x060003B3 RID: 947 RVA: 0x0000EB24 File Offset: 0x0000CD24
		public virtual XmlNode RemoveNamedItem(string name)
		{
			int num = this.FindNodeOffset(name);
			if (num >= 0)
			{
				return this.RemoveNodeAt(num);
			}
			return null;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060003B4 RID: 948 RVA: 0x0000EB46 File Offset: 0x0000CD46
		public virtual int Count
		{
			get
			{
				return this.nodes.Count;
			}
		}

		// Token: 0x060003B5 RID: 949 RVA: 0x0000EB54 File Offset: 0x0000CD54
		public virtual XmlNode Item(int index)
		{
			if (index < 0 || index >= this.nodes.Count)
			{
				return null;
			}
			XmlNode result;
			try
			{
				result = (XmlNode)this.nodes[index];
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new IndexOutOfRangeException(Res.GetString("Xdom_IndexOutOfRange"));
			}
			return result;
		}

		// Token: 0x060003B6 RID: 950 RVA: 0x0000EBAC File Offset: 0x0000CDAC
		public virtual XmlNode GetNamedItem(string localName, string namespaceURI)
		{
			int num = this.FindNodeOffset(localName, namespaceURI);
			if (num >= 0)
			{
				return (XmlNode)this.nodes[num];
			}
			return null;
		}

		// Token: 0x060003B7 RID: 951 RVA: 0x0000EBDC File Offset: 0x0000CDDC
		public virtual XmlNode RemoveNamedItem(string localName, string namespaceURI)
		{
			int num = this.FindNodeOffset(localName, namespaceURI);
			if (num >= 0)
			{
				return this.RemoveNodeAt(num);
			}
			return null;
		}

		// Token: 0x060003B8 RID: 952 RVA: 0x0000EBFF File Offset: 0x0000CDFF
		public virtual IEnumerator GetEnumerator()
		{
			return this.nodes.GetEnumerator();
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000EC0C File Offset: 0x0000CE0C
		internal int FindNodeOffset(string name)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = (XmlNode)this.nodes[i];
				if (name == xmlNode.Name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000EC50 File Offset: 0x0000CE50
		internal int FindNodeOffset(string localName, string namespaceURI)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = (XmlNode)this.nodes[i];
				if (xmlNode.LocalName == localName && xmlNode.NamespaceURI == namespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000ECA4 File Offset: 0x0000CEA4
		internal virtual XmlNode AddNode(XmlNode node)
		{
			XmlNode oldParent;
			if (node.NodeType == XmlNodeType.Attribute)
			{
				oldParent = ((XmlAttribute)node).OwnerElement;
			}
			else
			{
				oldParent = node.ParentNode;
			}
			string value = node.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(node, oldParent, this.parent, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.Add(node);
			node.SetParent(this.parent);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return node;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000ED24 File Offset: 0x0000CF24
		internal virtual XmlNode AddNodeForLoad(XmlNode node, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(node, this.parent);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			this.nodes.Add(node);
			node.SetParent(this.parent);
			if (insertEventArgsForLoad != null)
			{
				doc.AfterEvent(insertEventArgsForLoad);
			}
			return node;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000ED6C File Offset: 0x0000CF6C
		internal virtual XmlNode RemoveNodeAt(int i)
		{
			XmlNode xmlNode = (XmlNode)this.nodes[i];
			string value = xmlNode.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(xmlNode, this.parent, null, value, value, XmlNodeChangedAction.Remove);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.RemoveAt(i);
			xmlNode.SetParent(null);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return xmlNode;
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000EDDC File Offset: 0x0000CFDC
		internal XmlNode ReplaceNodeAt(int i, XmlNode node)
		{
			XmlNode result = this.RemoveNodeAt(i);
			this.InsertNodeAt(i, node);
			return result;
		}

		// Token: 0x060003BF RID: 959 RVA: 0x0000EDFC File Offset: 0x0000CFFC
		internal virtual XmlNode InsertNodeAt(int i, XmlNode node)
		{
			XmlNode oldParent;
			if (node.NodeType == XmlNodeType.Attribute)
			{
				oldParent = ((XmlAttribute)node).OwnerElement;
			}
			else
			{
				oldParent = node.ParentNode;
			}
			string value = node.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(node, oldParent, this.parent, value, value, XmlNodeChangedAction.Insert);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.nodes.Insert(i, node);
			node.SetParent(this.parent);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return node;
		}

		// Token: 0x040001BC RID: 444
		internal XmlNode parent;

		// Token: 0x040001BD RID: 445
		internal XmlNamedNodeMap.SmallXmlNodeList nodes;

		// Token: 0x0200030F RID: 783
		internal struct SmallXmlNodeList
		{
			// Token: 0x17000A1C RID: 2588
			// (get) Token: 0x06002DB0 RID: 11696 RVA: 0x000ED89C File Offset: 0x000EBA9C
			public int Count
			{
				get
				{
					if (this.field == null)
					{
						return 0;
					}
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						return arrayList.Count;
					}
					return 1;
				}
			}

			// Token: 0x17000A1D RID: 2589
			public object this[int index]
			{
				get
				{
					if (this.field == null)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						return arrayList[index];
					}
					if (index != 0)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					return this.field;
				}
			}

			// Token: 0x06002DB2 RID: 11698 RVA: 0x000ED918 File Offset: 0x000EBB18
			public void Add(object value)
			{
				if (this.field == null)
				{
					if (value == null)
					{
						this.field = new ArrayList
						{
							null
						};
						return;
					}
					this.field = value;
					return;
				}
				else
				{
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						arrayList.Add(value);
						return;
					}
					this.field = new ArrayList
					{
						this.field,
						value
					};
					return;
				}
			}

			// Token: 0x06002DB3 RID: 11699 RVA: 0x000ED988 File Offset: 0x000EBB88
			public void RemoveAt(int index)
			{
				if (this.field == null)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				ArrayList arrayList = this.field as ArrayList;
				if (arrayList != null)
				{
					arrayList.RemoveAt(index);
					return;
				}
				if (index != 0)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this.field = null;
			}

			// Token: 0x06002DB4 RID: 11700 RVA: 0x000ED9D4 File Offset: 0x000EBBD4
			public void Insert(int index, object value)
			{
				if (this.field == null)
				{
					if (index != 0)
					{
						throw new ArgumentOutOfRangeException("index");
					}
					this.Add(value);
					return;
				}
				else
				{
					ArrayList arrayList = this.field as ArrayList;
					if (arrayList != null)
					{
						arrayList.Insert(index, value);
						return;
					}
					if (index == 0)
					{
						this.field = new ArrayList
						{
							value,
							this.field
						};
						return;
					}
					if (index == 1)
					{
						this.field = new ArrayList
						{
							this.field,
							value
						};
						return;
					}
					throw new ArgumentOutOfRangeException("index");
				}
			}

			// Token: 0x06002DB5 RID: 11701 RVA: 0x000EDA70 File Offset: 0x000EBC70
			public IEnumerator GetEnumerator()
			{
				if (this.field == null)
				{
					return XmlDocument.EmptyEnumerator;
				}
				ArrayList arrayList = this.field as ArrayList;
				if (arrayList != null)
				{
					return arrayList.GetEnumerator();
				}
				return new XmlNamedNodeMap.SmallXmlNodeList.SingleObjectEnumerator(this.field);
			}

			// Token: 0x04001475 RID: 5237
			private object field;

			// Token: 0x020004DA RID: 1242
			private class SingleObjectEnumerator : IEnumerator
			{
				// Token: 0x060031BD RID: 12733 RVA: 0x0012113A File Offset: 0x0011F33A
				public SingleObjectEnumerator(object value)
				{
					this.loneValue = value;
				}

				// Token: 0x17000A7A RID: 2682
				// (get) Token: 0x060031BE RID: 12734 RVA: 0x00121150 File Offset: 0x0011F350
				public object Current
				{
					get
					{
						if (this.position != 0)
						{
							throw new InvalidOperationException();
						}
						return this.loneValue;
					}
				}

				// Token: 0x060031BF RID: 12735 RVA: 0x00121166 File Offset: 0x0011F366
				public bool MoveNext()
				{
					if (this.position < 0)
					{
						this.position = 0;
						return true;
					}
					this.position = 1;
					return false;
				}

				// Token: 0x060031C0 RID: 12736 RVA: 0x00121182 File Offset: 0x0011F382
				public void Reset()
				{
					this.position = -1;
				}

				// Token: 0x04001FA4 RID: 8100
				private object loneValue;

				// Token: 0x04001FA5 RID: 8101
				private int position = -1;
			}
		}
	}
}
