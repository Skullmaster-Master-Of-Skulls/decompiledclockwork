using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000022 RID: 34
	internal class CanonicalXmlDocument : XmlDocument, ICanonicalizableNode
	{
		// Token: 0x060000DD RID: 221 RVA: 0x000053A8 File Offset: 0x000035A8
		public CanonicalXmlDocument(bool defaultNodeSetInclusionState, bool includeComments)
		{
			base.PreserveWhitespace = true;
			this.m_includeComments = includeComments;
			this.m_defaultNodeSetInclusionState = defaultNodeSetInclusionState;
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000022 RID: 34
		// (get) Token: 0x060000DE RID: 222 RVA: 0x000053D9 File Offset: 0x000035D9
		// (set) Token: 0x060000DF RID: 223 RVA: 0x000053E1 File Offset: 0x000035E1
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

		// Token: 0x060000E0 RID: 224 RVA: 0x000053EC File Offset: 0x000035EC
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

		// Token: 0x060000E1 RID: 225 RVA: 0x00005464 File Offset: 0x00003664
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

		// Token: 0x060000E2 RID: 226 RVA: 0x000054DC File Offset: 0x000036DC
		public override XmlElement CreateElement(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlElement(prefix, localName, namespaceURI, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000054ED File Offset: 0x000036ED
		public override XmlAttribute CreateAttribute(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlAttribute(prefix, localName, namespaceURI, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000054ED File Offset: 0x000036ED
		protected override XmlAttribute CreateDefaultAttribute(string prefix, string localName, string namespaceURI)
		{
			return new CanonicalXmlAttribute(prefix, localName, namespaceURI, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x000054FE File Offset: 0x000036FE
		public override XmlText CreateTextNode(string text)
		{
			return new CanonicalXmlText(text, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000550D File Offset: 0x0000370D
		public override XmlWhitespace CreateWhitespace(string prefix)
		{
			return new CanonicalXmlWhitespace(prefix, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000551C File Offset: 0x0000371C
		public override XmlSignificantWhitespace CreateSignificantWhitespace(string text)
		{
			return new CanonicalXmlSignificantWhitespace(text, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x0000552B File Offset: 0x0000372B
		public override XmlProcessingInstruction CreateProcessingInstruction(string target, string data)
		{
			return new CanonicalXmlProcessingInstruction(target, data, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x0000553B File Offset: 0x0000373B
		public override XmlComment CreateComment(string data)
		{
			return new CanonicalXmlComment(data, this, this.m_defaultNodeSetInclusionState, this.m_includeComments);
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00005550 File Offset: 0x00003750
		public override XmlEntityReference CreateEntityReference(string name)
		{
			return new CanonicalXmlEntityReference(name, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x060000EB RID: 235 RVA: 0x0000555F File Offset: 0x0000375F
		public override XmlCDataSection CreateCDataSection(string data)
		{
			return new CanonicalXmlCDataSection(data, this, this.m_defaultNodeSetInclusionState);
		}

		// Token: 0x04000393 RID: 915
		private bool m_defaultNodeSetInclusionState;

		// Token: 0x04000394 RID: 916
		private bool m_includeComments;

		// Token: 0x04000395 RID: 917
		private bool m_isInNodeSet;
	}
}
