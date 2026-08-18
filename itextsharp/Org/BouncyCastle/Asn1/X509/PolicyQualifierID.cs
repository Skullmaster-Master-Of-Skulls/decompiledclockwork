using System;

namespace Org.BouncyCastle.Asn1.X509
{
	// Token: 0x020001A8 RID: 424
	public sealed class PolicyQualifierID : DerObjectIdentifier
	{
		// Token: 0x06001030 RID: 4144 RVA: 0x0005D98D File Offset: 0x0005C98D
		private PolicyQualifierID(string id) : base(id)
		{
		}

		// Token: 0x04000BE6 RID: 3046
		private const string IdQt = "1.3.6.1.5.5.7.2";

		// Token: 0x04000BE7 RID: 3047
		public static readonly PolicyQualifierID IdQtCps = new PolicyQualifierID("1.3.6.1.5.5.7.2.1");

		// Token: 0x04000BE8 RID: 3048
		public static readonly PolicyQualifierID IdQtUnotice = new PolicyQualifierID("1.3.6.1.5.5.7.2.2");
	}
}
