using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000029 RID: 41
	internal class CanonicalXmlProcessingInstruction : XmlProcessingInstruction, ICanonicalizableNode
	{
		// Token: 0x0600010B RID: 267 RVA: 0x00005ED3 File Offset: 0x000040D3
		public CanonicalXmlProcessingInstruction(string target, string data, XmlDocument doc, bool defaultNodeSetInclusionState) : base(target, data, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x0600010C RID: 268 RVA: 0x00005EE6 File Offset: 0x000040E6
		// (set) Token: 0x0600010D RID: 269 RVA: 0x00005EEE File Offset: 0x000040EE
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

		// Token: 0x0600010E RID: 270 RVA: 0x00005EF8 File Offset: 0x000040F8
		public void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (!this.IsInNodeSet)
			{
				return;
			}
			if (docPos == DocPosition.AfterRootElement)
			{
				strBuilder.Append('\n');
			}
			strBuilder.Append("<?");
			strBuilder.Append(this.Name);
			if (this.Value != null && this.Value.Length > 0)
			{
				strBuilder.Append(" " + this.Value);
			}
			strBuilder.Append("?>");
			if (docPos == DocPosition.BeforeRootElement)
			{
				strBuilder.Append('\n');
			}
		}

		// Token: 0x0600010F RID: 271 RVA: 0x00005F7C File Offset: 0x0000417C
		public void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc)
		{
			if (!this.IsInNodeSet)
			{
				return;
			}
			UTF8Encoding utf8Encoding = new UTF8Encoding(false);
			byte[] bytes;
			if (docPos == DocPosition.AfterRootElement)
			{
				bytes = utf8Encoding.GetBytes("(char) 10");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
			bytes = utf8Encoding.GetBytes("<?");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			bytes = utf8Encoding.GetBytes(this.Name);
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			if (this.Value != null && this.Value.Length > 0)
			{
				bytes = utf8Encoding.GetBytes(" " + this.Value);
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
			bytes = utf8Encoding.GetBytes("?>");
			hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			if (docPos == DocPosition.BeforeRootElement)
			{
				bytes = utf8Encoding.GetBytes("(char) 10");
				hash.TransformBlock(bytes, 0, bytes.Length, bytes, 0);
			}
		}

		// Token: 0x0400039D RID: 925
		private bool m_isInNodeSet;
	}
}
