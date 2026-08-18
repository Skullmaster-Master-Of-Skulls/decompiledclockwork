using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000027 RID: 39
	internal class CanonicalXmlSignificantWhitespace : XmlSignificantWhitespace, ICanonicalizableNode
	{
		// Token: 0x06000100 RID: 256 RVA: 0x00005D10 File Offset: 0x00003F10
		public CanonicalXmlSignificantWhitespace(string strData, XmlDocument doc, bool defaultNodeSetInclusionState) : base(strData, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000027 RID: 39
		// (get) Token: 0x06000101 RID: 257 RVA: 0x00005D21 File Offset: 0x00003F21
		// (set) Token: 0x06000102 RID: 258 RVA: 0x00005D29 File Offset: 0x00003F29
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

		// Token: 0x06000103 RID: 259 RVA: 0x00005D32 File Offset: 0x00003F32
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				strBuilder.Append(Utils.EscapeWhitespaceData(this.Value));
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00005D54 File Offset: 0x00003F54
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet && docPos == DocPosition.InRootElement)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding(false);
				byte[] bytes = utf8Encoding.GetBytes(Utils.EscapeWhitespaceData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x0400039A RID: 922
		private bool m_isInNodeSet;
	}
}
