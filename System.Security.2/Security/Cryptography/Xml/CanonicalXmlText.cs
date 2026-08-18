using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000025 RID: 37
	internal class CanonicalXmlText : XmlText, ICanonicalizableNode
	{
		// Token: 0x060000F6 RID: 246 RVA: 0x00005C10 File Offset: 0x00003E10
		public CanonicalXmlText(string strData, XmlDocument doc, bool defaultNodeSetInclusionState) : base(strData, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x17000025 RID: 37
		// (get) Token: 0x060000F7 RID: 247 RVA: 0x00005C21 File Offset: 0x00003E21
		// (set) Token: 0x060000F8 RID: 248 RVA: 0x00005C29 File Offset: 0x00003E29
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

		// Token: 0x060000F9 RID: 249 RVA: 0x00005C32 File Offset: 0x00003E32
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				strBuilder.Append(Utils.EscapeTextData(this.Value));
			}
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00005C50 File Offset: 0x00003E50
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				UTF8Encoding utf8Encoding = new UTF8Encoding(false);
				byte[] bytes = utf8Encoding.GetBytes(Utils.EscapeTextData(this.Value));
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x04000398 RID: 920
		private bool m_isInNodeSet;
	}
}
