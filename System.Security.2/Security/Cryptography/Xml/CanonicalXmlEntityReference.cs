using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200002A RID: 42
	internal class CanonicalXmlEntityReference : XmlEntityReference, ICanonicalizableNode
	{
		// Token: 0x06000110 RID: 272 RVA: 0x0000605E File Offset: 0x0000425E
		public CanonicalXmlEntityReference(string name, XmlDocument doc, bool defaultNodeSetInclusionState) : base(name, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x06000111 RID: 273 RVA: 0x0000606F File Offset: 0x0000426F
		// (set) Token: 0x06000112 RID: 274 RVA: 0x00006077 File Offset: 0x00004277
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

		// Token: 0x06000113 RID: 275 RVA: 0x00006080 File Offset: 0x00004280
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				CanonicalizationDispatcher.WriteGenericNode(this, strBuilder, docPos, anc);
			}
		}

		// Token: 0x06000114 RID: 276 RVA: 0x00006093 File Offset: 0x00004293
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (this.IsInNodeSet)
			{
				CanonicalizationDispatcher.WriteHashGenericNode(this, hash, docPos, anc);
			}
		}

		// Token: 0x0400039E RID: 926
		private bool m_isInNodeSet;
	}
}
