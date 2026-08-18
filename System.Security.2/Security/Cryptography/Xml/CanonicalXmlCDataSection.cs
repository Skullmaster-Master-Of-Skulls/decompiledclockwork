using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200002B RID: 43
	internal class CanonicalXmlCDataSection : XmlCDataSection, ICanonicalizableNode
	{
		// Token: 0x06000115 RID: 277 RVA: 0x000060A6 File Offset: 0x000042A6
		public CanonicalXmlCDataSection(string data, XmlDocument doc, bool defaultNodeSetInclusionState) : base(data, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000116 RID: 278 RVA: 0x000060B7 File Offset: 0x000042B7
		// (set) Token: 0x06000117 RID: 279 RVA: 0x000060BF File Offset: 0x000042BF
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

		// Token: 0x06000118 RID: 280 RVA: 0x000060C8 File Offset: 0x000042C8
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				strBuilder.Append(Utils.EscapeCData(this.Data));
			}
		}

		// Token: 0x06000119 RID: 281 RVA: 0x000060E4 File Offset: 0x000042E4
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding(false);
				byte[] bytes = utf8Encoding.GetBytes(Utils.EscapeCData(this.Data));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x0400039F RID: 927
		private bool m_isInNodeSet;
	}
}
