using System;
using System.Text;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000020 RID: 32
	internal interface ICanonicalizableNode
	{
		// Token: 0x17000021 RID: 33
		// (get) Token: 0x060000D4 RID: 212
		// (set) Token: 0x060000D5 RID: 213
		bool IsInNodeSet { get; set; }

		// Token: 0x060000D6 RID: 214
		void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc);

		// Token: 0x060000D7 RID: 215
		void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc);
	}
}
