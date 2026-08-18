using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000089 RID: 137
	internal class CanonicalXmlAttribute : XmlAttribute, ICanonicalizableNode
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000E4A4 File Offset: 0x0000D4A4
		public CanonicalXmlAttribute(string prefix, string localName, string namespaceURI, XmlDocument doc, bool defaultNodeSetInclusionState) : base(prefix, localName, namespaceURI, doc)
		{
			this.IsInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000E4B9 File Offset: 0x0000D4B9
		// (set) Token: 0x0600026F RID: 623 RVA: 0x0000E4C1 File Offset: 0x0000D4C1
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

		// Token: 0x06000270 RID: 624 RVA: 0x0000E4CA File Offset: 0x0000D4CA
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			strBuilder.Append(" " + this.Name + "=\"");
			strBuilder.Append(Utils.EscapeAttributeValue(this.Value));
			strBuilder.Append("\"");
		}

		// Token: 0x06000271 RID: 625 RVA: 0x0000E508 File Offset: 0x0000D508
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

		// Token: 0x040004E8 RID: 1256
		private bool m_isInNodeSet;
	}
}
