using System;
using System.Text;
using System.Xml;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x0200008E RID: 142
	internal class CanonicalXmlProcessingInstruction : XmlProcessingInstruction, ICanonicalizableNode
	{
		// Token: 0x06000287 RID: 647 RVA: 0x0000E843 File Offset: 0x0000D843
		public CanonicalXmlProcessingInstruction(string target, string data, XmlDocument doc, bool defaultNodeSetInclusionState) : base(target, data, doc)
		{
			this.m_isInNodeSet = defaultNodeSetInclusionState;
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x06000288 RID: 648 RVA: 0x0000E856 File Offset: 0x0000D856
		// (set) Token: 0x06000289 RID: 649 RVA: 0x0000E85E File Offset: 0x0000D85E
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

		// Token: 0x0600028A RID: 650 RVA: 0x0000E868 File Offset: 0x0000D868
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

		// Token: 0x0600028B RID: 651 RVA: 0x0000E8EC File Offset: 0x0000D8EC
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

		// Token: 0x040004EE RID: 1262
		private bool m_isInNodeSet;
	}
}
