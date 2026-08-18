using System;
using System.Collections;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000088 RID: 136
	internal class CanonicalXmlElement : XmlElement, ICanonicalizableNode
	{
		// Token: 0x06000268 RID: 616 RVA: 0x0000DEDB File Offset: 0x0000CEDB
		public CanonicalXmlElement(string prefix, string localName, string namespaceURI, XmlDocument doc, bool defaultNodeSetInclusionState) : base(prefix, localName, namespaceURI, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x06000269 RID: 617 RVA: 0x0000DEF0 File Offset: 0x0000CEF0
		// (set) Token: 0x0600026A RID: 618 RVA: 0x0000DEF8 File Offset: 0x0000CEF8
		public bool IsInNodeSet
		{
			get
			{
				return this.m_isInNodeSet;
			}
			set
			{
				this.m_isInNodeSet = value;
			}
		}

		// Token: 0x0600026B RID: 619 RVA: 0x0000DF04 File Offset: 0x0000CF04
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			Hashtable nsLocallyDeclared = new Hashtable();
			SortedList sortedList = new SortedList(new NamespaceSortOrder());
			SortedList sortedList2 = new SortedList(new AttributeSortOrder());
			XmlAttributeCollection attributes = this.Attributes;
			if (attributes != null)
			{
				foreach (object obj in attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					if (((CanonicalXmlAttribute)xmlAttribute).IsInNodeSet || Utils.IsNamespaceNode(xmlAttribute) || Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						if (Utils.IsNamespaceNode(xmlAttribute))
						{
							anc.TrackNamespaceNode(xmlAttribute, sortedList, nsLocallyDeclared);
						}
						else if (Utils.IsXmlNamespaceNode(xmlAttribute))
						{
							anc.TrackXmlNamespaceNode(xmlAttribute, sortedList, sortedList2, nsLocallyDeclared);
						}
						else if (this.IsInNodeSet)
						{
							sortedList2.Add(xmlAttribute, null);
						}
					}
				}
			}
			if (!Utils.IsCommittedNamespace(this, this.Prefix, this.NamespaceURI))
			{
				string name = (this.Prefix.Length > 0) ? ("xmlns:" + this.Prefix) : "xmlns";
				XmlAttribute xmlAttribute2 = this.OwnerDocument.CreateAttribute(name);
				xmlAttribute2.Value = this.NamespaceURI;
				anc.TrackNamespaceNode(xmlAttribute2, sortedList, nsLocallyDeclared);
			}
			if (this.IsInNodeSet)
			{
				anc.GetNamespacesToRender(this, sortedList2, sortedList, nsLocallyDeclared);
				strBuilder.Append("<" + this.Name);
				foreach (object obj2 in sortedList.GetKeyList())
				{
					(obj2 as CanonicalXmlAttribute).Write(strBuilder, docPos, anc);
				}
				foreach (object obj3 in sortedList2.GetKeyList())
				{
					(obj3 as CanonicalXmlAttribute).Write(strBuilder, docPos, anc);
				}
				strBuilder.Append(">");
			}
			anc.EnterElementContext();
			anc.LoadUnrenderedNamespaces(nsLocallyDeclared);
			anc.LoadRenderedNamespaces(sortedList);
			XmlNodeList childNodes = this.ChildNodes;
			foreach (object obj4 in childNodes)
			{
				XmlNode node = (XmlNode)obj4;
				CanonicalizationDispatcher.Write(node, strBuilder, docPos, anc);
			}
			anc.ExitElementContext();
			if (this.IsInNodeSet)
			{
				strBuilder.Append("</" + this.Name + ">");
			}
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000E1B4 File Offset: 0x0000D1B4
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			Hashtable nsLocallyDeclared = new Hashtable();
			SortedList sortedList = new SortedList(new NamespaceSortOrder());
			SortedList sortedList2 = new SortedList(new AttributeSortOrder());
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			XmlAttributeCollection attributes = this.Attributes;
			if (attributes != null)
			{
				foreach (object obj in attributes)
				{
					XmlAttribute xmlAttribute = (XmlAttribute)obj;
					if (((CanonicalXmlAttribute)xmlAttribute).IsInNodeSet || Utils.IsNamespaceNode(xmlAttribute) || Utils.IsXmlNamespaceNode(xmlAttribute))
					{
						if (Utils.IsNamespaceNode(xmlAttribute))
						{
							anc.TrackNamespaceNode(xmlAttribute, sortedList, nsLocallyDeclared);
						}
						else if (Utils.IsXmlNamespaceNode(xmlAttribute))
						{
							anc.TrackXmlNamespaceNode(xmlAttribute, sortedList, sortedList2, nsLocallyDeclared);
						}
						else if (this.IsInNodeSet)
						{
							sortedList2.Add(xmlAttribute, null);
						}
					}
				}
			}
			if (!Utils.IsCommittedNamespace(this, this.Prefix, this.NamespaceURI))
			{
				string name = (this.Prefix.Length > 0) ? ("xmlns:" + this.Prefix) : "xmlns";
				XmlAttribute xmlAttribute2 = this.OwnerDocument.CreateAttribute(name);
				xmlAttribute2.Value = this.NamespaceURI;
				anc.TrackNamespaceNode(xmlAttribute2, sortedList, nsLocallyDeclared);
			}
			if (this.IsInNodeSet)
			{
				anc.GetNamespacesToRender(this, sortedList2, sortedList, nsLocallyDeclared);
				byte[] bytes = utf8Encoding.GetBytes("<" + this.Name);
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
				foreach (object obj2 in sortedList.GetKeyList())
				{
					(obj2 as CanonicalXmlAttribute).WriteHash(hash, docPos, anc);
				}
				foreach (object obj3 in sortedList2.GetKeyList())
				{
					(obj3 as CanonicalXmlAttribute).WriteHash(hash, docPos, anc);
				}
				bytes = utf8Encoding.GetBytes(">");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
			anc.EnterElementContext();
			anc.LoadUnrenderedNamespaces(nsLocallyDeclared);
			anc.LoadRenderedNamespaces(sortedList);
			XmlNodeList childNodes = this.ChildNodes;
			foreach (object obj4 in childNodes)
			{
				XmlNode node = (XmlNode)obj4;
				CanonicalizationDispatcher.WriteHash(node, hash, docPos, anc);
			}
			anc.ExitElementContext();
			if (this.IsInNodeSet)
			{
				byte[] bytes = utf8Encoding.GetBytes("</" + this.Name + ">");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x040004E7 RID: 1255
		private bool m_isInNodeSet;
	}
}
