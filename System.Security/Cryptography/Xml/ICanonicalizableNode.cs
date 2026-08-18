using System;
using System.Text;

namespace System.Security.Cryptography.Xml
{
	// Token: 0x02000085 RID: 133
	internal interface ICanonicalizableNode
	{
		// Token: 0x17000065 RID: 101
		// (get) Token: 0x06000250 RID: 592
		// (set) Token: 0x06000251 RID: 593
		bool IsInNodeSet { get; set; }

		// Token: 0x06000252 RID: 594
		void Write(StringBuilder strBuilder, DocPosition docPos, AncestralNamespaceContextManager anc);

		// Token: 0x06000253 RID: 595
		void WriteHash(HashAlgorithm hash, DocPosition docPos, AncestralNamespaceContextManager anc);
	}
}
