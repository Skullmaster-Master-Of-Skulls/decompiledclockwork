using System;

namespace Org.BouncyCastle.Asn1.Ocsp
{
	// Token: 0x0200020A RID: 522
	public class OcspResponseStatus : DerEnumerated
	{
		// Token: 0x06001402 RID: 5122 RVA: 0x00072D01 File Offset: 0x00071D01
		public OcspResponseStatus(int value) : base(value)
		{
		}

		// Token: 0x06001403 RID: 5123 RVA: 0x00072D0A File Offset: 0x00071D0A
		public OcspResponseStatus(DerEnumerated value) : base(value.Value.IntValue)
		{
		}

		// Token: 0x04000DCF RID: 3535
		public const int Successful = 0;

		// Token: 0x04000DD0 RID: 3536
		public const int MalformedRequest = 1;

		// Token: 0x04000DD1 RID: 3537
		public const int InternalError = 2;

		// Token: 0x04000DD2 RID: 3538
		public const int TryLater = 3;

		// Token: 0x04000DD3 RID: 3539
		public const int SignatureRequired = 5;

		// Token: 0x04000DD4 RID: 3540
		public const int Unauthorized = 6;
	}
}
