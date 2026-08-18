using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200008F RID: 143
	internal class CanonicalXmlEntityReference : XmlEntityReference, ICanonicalizableNode
	{
		// Token: 0x0600028C RID: 652 RVA: 0x0000E9CE File Offset: 0x0000D9CE
		public CanonicalXmlEntityReference(string name, XmlDocument doc, bool defaultNodeSetInclusionState) : base(name, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000E9DF File Offset: 0x0000D9DF
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000E9E7 File Offset: 0x0000D9E7
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

		// Token: 0x0600028F RID: 655 RVA: 0x0000E9F0 File Offset: 0x0000D9F0
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				CanonicalizationDispatcher.WriteGenericNode(this, strBuilder, docPos, anc);
			}
		}

		// Token: 0x06000290 RID: 656 RVA: 0x0000EA03 File Offset: 0x0000DA03
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				CanonicalizationDispatcher.WriteHashGenericNode(this, hash, docPos, anc);
			}
		}

		// Token: 0x040004EF RID: 1263
		private bool m_isInNodeSet;
	}
}
