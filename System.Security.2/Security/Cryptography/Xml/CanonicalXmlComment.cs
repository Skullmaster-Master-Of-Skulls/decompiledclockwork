using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000028 RID: 40
	internal class CanonicalXmlComment : XmlComment, ICanonicalizableNode
	{
		// Token: 0x06000105 RID: 261 RVA: 0x00005D94 File Offset: 0x00003F94
		public CanonicalXmlComment(string comment, XmlDocument doc, bool defaultNodeSetInclusionState, bool includeComments) : base(comment, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
			this.m_includeComments = includeComments;
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00005DAD File Offset: 0x00003FAD
		// (set) Token: 0x06000107 RID: 263 RVA: 0x00005DB5 File Offset: 0x00003FB5
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

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00005DBE File Offset: 0x00003FBE
		public bool IncludeComments
		{
			get
			{
				return this.m_includeComments;
			}
		}

		// Token: 0x06000109 RID: 265 RVA: 0x00005DC8 File Offset: 0x00003FC8
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

		// Token: 0x0600010A RID: 266 RVA: 0x00005E24 File Offset: 0x00004024
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

		// Token: 0x0400039B RID: 923
		private bool m_isInNodeSet;

		// Token: 0x0400039C RID: 924
		private bool m_includeComments;
	}
}
