using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000024 RID: 36
	internal class CanonicalXmlAttribute : XmlAttribute, ICanonicalizableNode
	{
		// Token: 0x060000F1 RID: 241 RVA: 0x00005B34 File Offset: 0x00003D34
		public CanonicalXmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc, bool defaultNodeSetInclusionState) : base(prefix, localName, namespaceURI, doc)
		{
			this.IsInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x060000F2 RID: 242 RVA: 0x00005B49 File Offset: 0x00003D49
		// (set) Token: 0x060000F3 RID: 243 RVA: 0x00005B51 File Offset: 0x00003D51
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

		// Token: 0x060000F4 RID: 244 RVA: 0x00005B5A File Offset: 0x00003D5A
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			strBuilder.Append(" " + this.Name + "=\"");
			strBuilder.Append(Utils.EscapeAttributeValue(this.Value));
			strBuilder.Append("\"");
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005B98 File Offset: 0x00003D98
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			byte[] bytes = utf8Encoding.GetBytes(" " + this.Name + "=\"");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = utf8Encoding.GetBytes(Utils.EscapeAttributeValue(this.Value));
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = utf8Encoding.GetBytes("\"");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
		}

		// Token: 0x04000397 RID: 919
		private bool m_isInNodeSet;
	}
}
