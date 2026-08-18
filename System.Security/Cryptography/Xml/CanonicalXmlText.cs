using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200008A RID: 138
	internal class CanonicalXmlText : XmlText, ICanonicalizableNode
	{
		// Token: 0x06000272 RID: 626 RVA: 0x0000E580 File Offset: 0x0000D580
		public CanonicalXmlText(string strData, XmlDocument doc, bool defaultNodeSetInclusionState) : base(strData, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x06000273 RID: 627 RVA: 0x0000E591 File Offset: 0x0000D591
		// (set) Token: 0x06000274 RID: 628 RVA: 0x0000E599 File Offset: 0x0000D599
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

		// Token: 0x06000275 RID: 629 RVA: 0x0000E5A2 File Offset: 0x0000D5A2
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				strBuilder.Append(Utils.EscapeTextData(this.Value));
			}
		}

		// Token: 0x06000276 RID: 630 RVA: 0x0000E5C0 File Offset: 0x0000D5C0
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding(false);
				byte[] bytes = utf8Encoding.GetBytes(Utils.EscapeTextData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x040004E9 RID: 1257
		private bool m_isInNodeSet;
	}
}
