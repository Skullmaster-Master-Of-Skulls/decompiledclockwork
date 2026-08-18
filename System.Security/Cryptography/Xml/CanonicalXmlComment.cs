using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200008D RID: 141
	internal class CanonicalXmlComment : XmlComment, ICanonicalizableNode
	{
		// Token: 0x06000281 RID: 641 RVA: 0x0000E704 File Offset: 0x0000D704
		public CanonicalXmlComment(string comment, XmlDocument doc, bool defaultNodeSetInclusionState, bool includeComments) : base(comment, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
			this.m_includeComments = includeComments;
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x06000282 RID: 642 RVA: 0x0000E71D File Offset: 0x0000D71D
		// (set) Token: 0x06000283 RID: 643 RVA: 0x0000E725 File Offset: 0x0000D725
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

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x06000284 RID: 644 RVA: 0x0000E72E File Offset: 0x0000D72E
		public bool IncludeComments
		{
			get
			{
				return this.m_includeComments;
			}
		}

		// Token: 0x06000285 RID: 645 RVA: 0x0000E738 File Offset: 0x0000D738
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (!this.IsInNodeSet || !this.IncludeComments)
			{
				return;
			}
			if (docPos == DocPosition.AfterRootElement)
			{
				strBuilder.Append('\n');
			}
			strBuilder.Append("<!--");
			strBuilder.Append(this.Value);
			strBuilder.Append("-->");
			if (docPos == DocPosition.BeforeRootElement)
			{
				strBuilder.Append('\n');
			}
		}

		// Token: 0x06000286 RID: 646 RVA: 0x0000E794 File Offset: 0x0000D794
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (!this.IsInNodeSet || !this.IncludeComments)
			{
				return;
			}
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			byte[] bytes = utf8Encoding.GetBytes("(char) 10");
			if (docPos == DocPosition.AfterRootElement)
			{
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
			bytes = utf8Encoding.GetBytes("<!--");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = utf8Encoding.GetBytes(this.Value);
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = utf8Encoding.GetBytes("-->");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			if (docPos == DocPosition.BeforeRootElement)
			{
				bytes = utf8Encoding.GetBytes("(char) 10");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x040004EC RID: 1260
		private bool m_isInNodeSet;

		// Token: 0x040004ED RID: 1261
		private bool m_includeComments;
	}
}
