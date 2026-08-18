using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000087 RID: 135
	internal class CanonicalXmlDocument : XmlDocument, ICanonicalizableNode
	{
		// Token: 0x06000259 RID: 601 RVA: 0x0000DD04 File Offset: 0x0000CD04
		public CanonicalXmlDocument(bool defaultNodeSetInclusionState, bool includeComments)
		{
			base.PreserveWhitespace = true;
			this.m_includeComments = includeComments;
			this.m_defaultNodeSetInclusionState = defaultNodeSetInclusionState;
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x0600025A RID: 602 RVA: 0x0000DD35 File Offset: 0x0000CD35
		// (set) Token: 0x0600025B RID: 603 RVA: 0x0000DD3D File Offset: 0x0000CD3D
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

		// Token: 0x0600025C RID: 604 RVA: 0x0000DD48 File Offset: 0x0000CD48
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			docPos = DocPosition.BeforeRootElement;
			foreach (object obj in this.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					CanonicalizationDispatcher.Write(xmlNode, strBuilder, DocPosition.InRootElement, anc);
					docPos = DocPosition.AfterRootElement;
				}
				else
				{
					CanonicalizationDispatcher.Write(xmlNode, strBuilder, docPos, anc);
				}
			}
		}

		// Token: 0x0600025D RID: 605 RVA: 0x0000DDC0 File Offset: 0x0000CDC0
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			docPos = DocPosition.BeforeRootElement;
			foreach (object obj in this.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				if (xmlNode.NodeType == XmlNodeType.Element)
				{
					CanonicalizationDispatcher.WriteHash(xmlNode, hash, DocPosition.InRootElement, anc);
					docPos = DocPosition.AfterRootElement;
				}
				else
				{
					CanonicalizationDispatcher.WriteHash(xmlNode, hash, docPos, anc);
				}
			}
		}

		// Token: 0x0600025E RID: 606 RVA: 0x0000DE38 File Offset: 0x0000CE38
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlElement(prefix, localName, namespaceURI, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x0600025F RID: 607 RVA: 0x0000DE49 File Offset: 0x0000CE49
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlAttribute(prefix, localName, namespaceURI, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x06000260 RID: 608 RVA: 0x0000DE5A File Offset: 0x0000CE5A
		protected override XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlAttribute(prefix, localName, namespaceURI, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x06000261 RID: 609 RVA: 0x0000DE6B File Offset: 0x0000CE6B
		public override XmlText CreateTextNode(string text)
		{
			return new CanonicalXmlText(text, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x06000262 RID: 610 RVA: 0x0000DE7A File Offset: 0x0000CE7A
		public override XmlWhitespace CreateWhitespace(string prefix)
		{
			return new CanonicalXmlWhitespace(prefix, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x06000263 RID: 611 RVA: 0x0000DE89 File Offset: 0x0000CE89
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string text)
		{
			return new CanonicalXmlSignificantWhitespace(text, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x06000264 RID: 612 RVA: 0x0000DE98 File Offset: 0x0000CE98
		public override XmlProcessingInstruction CreateProcessingInstruction(string target, string data)
		{
			return new CanonicalXmlProcessingInstruction(target, data, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x06000265 RID: 613 RVA: 0x0000DEA8 File Offset: 0x0000CEA8
		public override XmlComment CreateComment(string data)
		{
			return new CanonicalXmlComment(data, this, this.m_defaultNodeSetInclusionState, this.m_includeComments);
		}

		// Token: 0x06000266 RID: 614 RVA: 0x0000DEBD File Offset: 0x0000CEBD
		public override XmlEntityReference CreateEntityReference(string name)
		{
			return new CanonicalXmlEntityReference(name, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x06000267 RID: 615 RVA: 0x0000DECC File Offset: 0x0000CECC
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new CanonicalXmlCDataSection(data, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x040004E4 RID: 1252
		private bool m_defaultNodeSetInclusionState;

		// Token: 0x040004E5 RID: 1253
		private bool m_includeComments;

		// Token: 0x040004E6 RID: 1254
		private bool m_isInNodeSet;
	}
}
