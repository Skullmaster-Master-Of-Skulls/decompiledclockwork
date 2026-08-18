using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200008C RID: 140
	internal class CanonicalXmlSignificantWhitespace : XmlSignificantWhitespace, ICanonicalizableNode
	{
		// Token: 0x0600027C RID: 636 RVA: 0x0000E680 File Offset: 0x0000D680
		public CanonicalXmlSignificantWhitespace(string strData, XmlDocument doc, bool defaultNodeSetInclusionState) : base(strData, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x0600027D RID: 637 RVA: 0x0000E691 File Offset: 0x0000D691
		// (set) Token: 0x0600027E RID: 638 RVA: 0x0000E699 File Offset: 0x0000D699
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

		// Token: 0x0600027F RID: 639 RVA: 0x0000E6A2 File Offset: 0x0000D6A2
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				strBuilder.Append(Utils.EscapeWhitespaceData(this.Value));
			}
		}

		// Token: 0x06000280 RID: 640 RVA: 0x0000E6C4 File Offset: 0x0000D6C4
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding(false);
				byte[] bytes = utf8Encoding.GetBytes(Utils.EscapeWhitespaceData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x040004EB RID: 1259
		private bool m_isInNodeSet;
	}
}
