using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200008B RID: 139
	internal class CanonicalXmlWhitespace : XmlWhitespace, ICanonicalizableNode
	{
		// Token: 0x06000277 RID: 631 RVA: 0x0000E5FC File Offset: 0x0000D5FC
		public CanonicalXmlWhitespace(string strData, XmlDocument doc, bool defaultNodeSetInclusionState) : base(strData, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x06000278 RID: 632 RVA: 0x0000E60D File Offset: 0x0000D60D
		// (set) Token: 0x06000279 RID: 633 RVA: 0x0000E615 File Offset: 0x0000D615
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

		// Token: 0x0600027A RID: 634 RVA: 0x0000E61E File Offset: 0x0000D61E
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				strBuilder.Append(Utils.EscapeWhitespaceData(this.Value));
			}
		}

		// Token: 0x0600027B RID: 635 RVA: 0x0000E640 File Offset: 0x0000D640
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding(false);
				byte[] bytes = utf8Encoding.GetBytes(Utils.EscapeWhitespaceData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x040004EA RID: 1258
		private bool m_isInNodeSet;
	}
}
