using System;

namespace Org.BouncyCastle.Asn1.X509.Qualified
{
	// Token: 0x02000626 RID: 1574
	public sealed class Rfc3739QCObjectIdentifiers
	{
		// Token: 0x0600356E RID: 13678 RVA: 0x0014B712 File Offset: 0x0014A712
		private Rfc3739QCObjectIdentifiers()
		{
		}

		// Token: 0x040023B5 RID: 9141
		public static readonly DerObjectIdentifier IdQcs = new DerObjectIdentifier("1.3.6.1.5.5.7.11");

		// Token: 0x040023B6 RID: 9142
		public static readonly DerObjectIdentifier IdQcsPkixQCSyntaxV1 = new DerObjectIdentifier(Rfc3739QCObjectIdentifiers.IdQcs + ".1");

		// Token: 0x040023B7 RID: 9143
		public static readonly DerObjectIdentifier IdQcsPkixQCSyntaxV2 = new DerObjectIdentifier(Rfc3739QCObjectIdentifiers.IdQcs + ".2");
	}
}
