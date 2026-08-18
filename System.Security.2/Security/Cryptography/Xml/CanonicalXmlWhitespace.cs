using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000026 RID: 38
	internal class CanonicalXmlWhitespace : XmlWhitespace, ICanonicalizableNode
	{
		// Token: 0x060000FB RID: 251 RVA: 0x00005C8C File Offset: 0x00003E8C
		public CanonicalXmlWhitespace(string strData, XmlDocument doc, bool defaultNodeSetInclusionState) : base(strData, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000026 RID: 38
		// (get) Token: 0x060000FC RID: 252 RVA: 0x00005C9D File Offset: 0x00003E9D
		// (set) Token: 0x060000FD RID: 253 RVA: 0x00005CA5 File Offset: 0x00003EA5
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

		// Token: 0x060000FE RID: 254 RVA: 0x00005CAE File Offset: 0x00003EAE
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				strBuilder.Append(Utils.EscapeWhitespaceData(this.Value));
			}
		}

		// Token: 0x060000FF RID: 255 RVA: 0x00005CD0 File Offset: 0x00003ED0
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding(false);
				byte[] bytes = utf8Encoding.GetBytes(Utils.EscapeWhitespaceData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x04000399 RID: 921
		private bool m_isInNodeSet;
	}
}
