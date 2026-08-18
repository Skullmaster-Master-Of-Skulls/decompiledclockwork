using System;
using System.Collections;

namespace System.Xml
{
	// Token: 0x020000CB RID: 203
	public class XmlNamedNodeMap : IEnumerable
	{
		// Token: 0x06000C0F RID: 3087 RVA: 0x00036E09 File Offset: 0x00035E09
		internal XmlNamedNodeMap(XmlNode parent)
		{
			this.parent = parent;
			this.nodes = null;
		}

		// Token: 0x06000C10 RID: 3088 RVA: 0x00036E20 File Offset: 0x00035E20
		public virtual XmlNode GetNamedItem(string name)
		{
			int num = this.FindNodeOffset(name);
			if (num >= 0)
			{
				return (XmlNode)this.Nodes[num];
			}
			return null;
		}

		// Token: 0x06000C11 RID: 3089 RVA: 0x00036E4C File Offset: 0x00035E4C
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

		// Token: 0x06000C12 RID: 3090 RVA: 0x00036E88 File Offset: 0x00035E88
		public virtual XmlNode RemoveNamedItem(string name)
		{
			int num = this.FindNodeOffset(name);
			if (num >= 0)
			{
				return this.RemoveNodeAt(num);
			}
			return null;
		}

		// Token: 0x170002B5 RID: 693
		// (get) Token: 0x06000C13 RID: 3091 RVA: 0x00036EAA File Offset: 0x00035EAA
		public virtual int Count
		{
			get
			{
				if (this.nodes != null)
				{
					return this.nodes.Count;
				}
				return 0;
			}
		}

		// Token: 0x06000C14 RID: 3092 RVA: 0x00036EC4 File Offset: 0x00035EC4
		public virtual XmlNode Item(int index)
		{
			if (index < 0 || index >= this.Nodes.Count)
			{
				return null;
			}
			XmlNode result;
			try
			{
				result = (XmlNode)this.Nodes[index];
			}
			catch (ArgumentOutOfRangeException)
			{
				throw new IndexOutOfRangeException(Res.GetString("Xdom_IndexOutOfRange"));
			}
			return result;
		}

		// Token: 0x06000C15 RID: 3093 RVA: 0x00036F1C File Offset: 0x00035F1C
		public virtual XmlNode GetNamedItem(string localName, string namespaceURI)
		{
			int num = this.FindNodeOffset(localName, namespaceURI);
			if (num >= 0)
			{
				return (XmlNode)this.Nodes[num];
			}
			return null;
		}

		// Token: 0x06000C16 RID: 3094 RVA: 0x00036F4C File Offset: 0x00035F4C
		public virtual XmlNode RemoveNamedItem(string localName, string namespaceURI)
		{
			int num = this.FindNodeOffset(localName, namespaceURI);
			if (num >= 0)
			{
				return this.RemoveNodeAt(num);
			}
			return null;
		}

		// Token: 0x170002B6 RID: 694
		// (get) Token: 0x06000C17 RID: 3095 RVA: 0x00036F6F File Offset: 0x00035F6F
		internal ArrayList Nodes
		{
			get
			{
				if (this.nodes == null)
				{
					this.nodes = new ArrayList();
				}
				return this.nodes;
			}
		}

		// Token: 0x06000C18 RID: 3096 RVA: 0x00036F8A File Offset: 0x00035F8A
		public virtual IEnumerator GetEnumerator()
		{
			if (this.nodes == null)
			{
				return XmlDocument.EmptyEnumerator;
			}
			return this.Nodes.GetEnumerator();
		}

		// Token: 0x06000C19 RID: 3097 RVA: 0x00036FA8 File Offset: 0x00035FA8
		internal int FindNodeOffset(string name)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = (XmlNode)this.Nodes[i];
				if (name == xmlNode.Name)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C1A RID: 3098 RVA: 0x00036FEC File Offset: 0x00035FEC
		internal int FindNodeOffset(string localName, string namespaceURI)
		{
			int count = this.Count;
			for (int i = 0; i < count; i++)
			{
				XmlNode xmlNode = (XmlNode)this.Nodes[i];
				if (xmlNode.LocalName == localName && xmlNode.NamespaceURI == namespaceURI)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06000C1B RID: 3099 RVA: 0x00037040 File Offset: 0x00036040
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
			this.Nodes.Add(node);
			node.SetParent(this.parent);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return node;
		}

		// Token: 0x06000C1C RID: 3100 RVA: 0x000370C4 File Offset: 0x000360C4
		internal virtual XmlNode AddNodeForLoad(XmlNode node, XmlDocument doc)
		{
			XmlNodeChangedEventArgs insertEventArgsForLoad = doc.GetInsertEventArgsForLoad(node, this.parent);
			if (insertEventArgsForLoad != null)
			{
				doc.BeforeEvent(insertEventArgsForLoad);
			}
			this.Nodes.Add(node);
			node.SetParent(this.parent);
			if (insertEventArgsForLoad != null)
			{
				doc.AfterEvent(insertEventArgsForLoad);
			}
			return node;
		}

		// Token: 0x06000C1D RID: 3101 RVA: 0x00037110 File Offset: 0x00036110
		internal virtual XmlNode RemoveNodeAt(int i)
		{
			XmlNode xmlNode = (XmlNode)this.Nodes[i];
			string value = xmlNode.Value;
			XmlNodeChangedEventArgs eventArgs = this.parent.GetEventArgs(xmlNode, this.parent, null, value, value, XmlNodeChangedAction.Remove);
			if (eventArgs != null)
			{
				this.parent.BeforeEvent(eventArgs);
			}
			this.Nodes.RemoveAt(i);
			xmlNode.SetParent(null);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return xmlNode;
		}

		// Token: 0x06000C1E RID: 3102 RVA: 0x00037180 File Offset: 0x00036180
		internal XmlNode ReplaceNodeAt(int i, XmlNode node)
		{
			XmlNode result = this.RemoveNodeAt(i);
			this.InsertNodeAt(i, node);
			return result;
		}

		// Token: 0x06000C1F RID: 3103 RVA: 0x000371A0 File Offset: 0x000361A0
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
			this.Nodes.Insert(i, node);
			node.SetParent(this.parent);
			if (eventArgs != null)
			{
				this.parent.AfterEvent(eventArgs);
			}
			return node;
		}

		// Token: 0x040008EF RID: 2287
		internal XmlNode parent;

		// Token: 0x040008F0 RID: 2288
		internal ArrayList nodes;
	}
}
