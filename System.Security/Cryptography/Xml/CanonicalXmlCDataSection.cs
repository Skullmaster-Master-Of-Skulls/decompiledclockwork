using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000090 RID: 144
	internal class CanonicalXmlCDataSection : XmlCDataSection, ICanonicalizableNode
	{
		// Token: 0x06000291 RID: 657 RVA: 0x0000EA16 File Offset: 0x0000DA16
		public CanonicalXmlCDataSection(string data, XmlDocument doc, bool defaultNodeSetInclusionState) : base(data, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000292 RID: 658 RVA: 0x0000EA27 File Offset: 0x0000DA27
		// (set) Token: 0x06000293 RID: 659 RVA: 0x0000EA2F File Offset: 0x0000DA2F
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

		// Token: 0x06000294 RID: 660 RVA: 0x0000EA38 File Offset: 0x0000DA38
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				strBuilder.Append(Utils.EscapeCData(this.Data));
			}
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000EA54 File Offset: 0x0000DA54
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding(false);
				byte[] bytes = utf8Encoding.GetBytes(Utils.EscapeCData(this.Data));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x040004F0 RID: 1264
		private bool m_isInNodeSet;
	}
}
