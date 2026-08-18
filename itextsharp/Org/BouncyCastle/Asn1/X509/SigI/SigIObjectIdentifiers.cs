using System;

namespace Org.BouncyCastle.Asn1.X509.SigI
{
	// Token: 0x02000625 RID: 1573
	public sealed class SigIObjectIdentifiers
	{
		// Token: 0x0600356C RID: 13676 RVA: 0x0014B655 File Offset: 0x0014A655
		private SigIObjectIdentifiers()
		{
		}

		// Token: 0x040023AE RID: 9134
		public static readonly DerObjectIdentifier IdSigI = new DerObjectIdentifier("1.3.36.8");

		// Token: 0x040023AF RID: 9135
		public static readonly DerObjectIdentifier IdSigIKP = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigI + ".2");

		// Token: 0x040023B0 RID: 9136
		public static readonly DerObjectIdentifier IdSigICP = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigI + ".1");

		// Token: 0x040023B1 RID: 9137
		public static readonly DerObjectIdentifier IdSigION = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigI + ".4");

		// Token: 0x040023B2 RID: 9138
		public static readonly DerObjectIdentifier IdSigIKPDirectoryService = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigIKP + ".1");

		// Token: 0x040023B3 RID: 9139
		public static readonly DerObjectIdentifier IdSigIONPersonalData = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigION + ".1");

		// Token: 0x040023B4 RID: 9140
		public static readonly DerObjectIdentifier IdSigICPSigConform = new DerObjectIdentifier(SigIObjectIdentifiers.IdSigICP + ".1");
	}
}
