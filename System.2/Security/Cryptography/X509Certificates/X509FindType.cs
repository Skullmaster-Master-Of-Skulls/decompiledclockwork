using System;

namespace System.Security.Cryptography.X509Certificates
{
	// Token: 0x02000468 RID: 1128
	public enum X509FindType
	{
		// Token: 0x040025D0 RID: 9680
		FindByThumbprint,
		// Token: 0x040025D1 RID: 9681
		FindBySubjectName,
		// Token: 0x040025D2 RID: 9682
		FindBySubjectDistinguishedName,
		// Token: 0x040025D3 RID: 9683
		FindByIssuerName,
		// Token: 0x040025D4 RID: 9684
		FindByIssuerDistinguishedName,
		// Token: 0x040025D5 RID: 9685
		FindBySerialNumber,
		// Token: 0x040025D6 RID: 9686
		FindByTimeValid,
		// Token: 0x040025D7 RID: 9687
		FindByTimeNotYetValid,
		// Token: 0x040025D8 RID: 9688
		FindByTimeExpired,
		// Token: 0x040025D9 RID: 9689
		FindByTemplateName,
		// Token: 0x040025DA RID: 9690
		FindByApplicationPolicy,
		// Token: 0x040025DB RID: 9691
		FindByCertificatePolicy,
		// Token: 0x040025DC RID: 9692
		FindByExtension,
		// Token: 0x040025DD RID: 9693
		FindByKeyUsage,
		// Token: 0x040025DE RID: 9694
		FindBySubjectKeyIdentifier
	}
}
