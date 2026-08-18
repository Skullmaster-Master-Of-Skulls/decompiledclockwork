using System;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x02000595 RID: 1429
	[Serializable]
	internal class IllegalVariantTypeException : VariantTypeException
	{
		// Token: 0x06002FF6 RID: 12278 RVA: 0x000E24C6 File Offset: 0x000E14C6
		public IllegalVariantTypeException(long A_0, object A_1, string A_2) : base(A_0, A_1, A_2)
		{
		}

		// Token: 0x06002FF7 RID: 12279 RVA: 0x000E24D4 File Offset: 0x000E14D4
		public IllegalVariantTypeException(long A_0, object A_1) : this(A_0, A_1, string.Concat(new object[]
		{
			"The variant type ",
			A_0,
			" (",
			iu.b(A_0),
			", ",
			f5.a(A_0),
			") is illegal in this context."
		}))
		{
		}
	}
}
