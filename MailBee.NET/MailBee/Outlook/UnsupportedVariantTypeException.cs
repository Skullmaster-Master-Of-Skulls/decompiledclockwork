using System;
using a.b;

namespace MailBee.Outlook
{
	// Token: 0x0200059F RID: 1439
	[Serializable]
	internal abstract class UnsupportedVariantTypeException : VariantTypeException
	{
		// Token: 0x0600303E RID: 12350 RVA: 0x000E2F08 File Offset: 0x000E1F08
		public UnsupportedVariantTypeException(long A_0, object A_1) : base(A_0, A_1, string.Concat(new object[]
		{
			"HPSF does not yet support the variant type ",
			A_0,
			" (",
			iu.b(A_0),
			", ",
			f5.a(A_0),
			"). If you want support for this variant type in one of the next POI releases please submit a request for enhancement (RFE) To <http://issues.apache.org/bugzilla/>! Thank you!"
		}))
		{
		}
	}
}
