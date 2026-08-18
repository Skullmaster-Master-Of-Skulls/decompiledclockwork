using System;

namespace Org.BouncyCastle.Asn1.IsisMtt
{
	// Token: 0x02000311 RID: 785
	public abstract class IsisMttObjectIdentifiers
	{
		// Token: 0x040013B6 RID: 5046
		public static readonly DerObjectIdentifier IdIsisMtt = new DerObjectIdentifier("1.3.36.8");

		// Token: 0x040013B7 RID: 5047
		public static readonly DerObjectIdentifier IdIsisMttCP = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMtt + ".1");

		// Token: 0x040013B8 RID: 5048
		public static readonly DerObjectIdentifier IdIsisMttCPAccredited = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttCP + ".1");

		// Token: 0x040013B9 RID: 5049
		public static readonly DerObjectIdentifier IdIsisMttAT = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMtt + ".3");

		// Token: 0x040013BA RID: 5050
		public static readonly DerObjectIdentifier IdIsisMttATDateOfCertGen = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".1");

		// Token: 0x040013BB RID: 5051
		public static readonly DerObjectIdentifier IdIsisMttATProcuration = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".2");

		// Token: 0x040013BC RID: 5052
		public static readonly DerObjectIdentifier IdIsisMttATAdmission = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".3");

		// Token: 0x040013BD RID: 5053
		public static readonly DerObjectIdentifier IdIsisMttATMonetaryLimit = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".4");

		// Token: 0x040013BE RID: 5054
		public static readonly DerObjectIdentifier IdIsisMttATDeclarationOfMajority = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".5");

		// Token: 0x040013BF RID: 5055
		public static readonly DerObjectIdentifier IdIsisMttATIccsn = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".6");

		// Token: 0x040013C0 RID: 5056
		public static readonly DerObjectIdentifier IdIsisMttATPKReference = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".7");

		// Token: 0x040013C1 RID: 5057
		public static readonly DerObjectIdentifier IdIsisMttATRestriction = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".8");

		// Token: 0x040013C2 RID: 5058
		public static readonly DerObjectIdentifier IdIsisMttATRetrieveIfAllowed = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".9");

		// Token: 0x040013C3 RID: 5059
		public static readonly DerObjectIdentifier IdIsisMttATRequestedCertificate = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".10");

		// Token: 0x040013C4 RID: 5060
		public static readonly DerObjectIdentifier IdIsisMttATNamingAuthorities = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".11");

		// Token: 0x040013C5 RID: 5061
		public static readonly DerObjectIdentifier IdIsisMttATCertInDirSince = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".12");

		// Token: 0x040013C6 RID: 5062
		public static readonly DerObjectIdentifier IdIsisMttATCertHash = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".13");

		// Token: 0x040013C7 RID: 5063
		public static readonly DerObjectIdentifier IdIsisMttATNameAtBirth = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".14");

		// Token: 0x040013C8 RID: 5064
		public static readonly DerObjectIdentifier IdIsisMttATAdditionalInformation = new DerObjectIdentifier(IsisMttObjectIdentifiers.IdIsisMttAT + ".15");

		// Token: 0x040013C9 RID: 5065
		public static readonly DerObjectIdentifier IdIsisMttATLiabilityLimitationFlag = new DerObjectIdentifier("0.2.262.1.10.12.0");
	}
}
