using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x0200033F RID: 831
	public abstract class PkixAttrCertChecker
	{
		// Token: 0x06001E0F RID: 7695
		public abstract ISet GetSupportedExtensions();

		// Token: 0x06001E10 RID: 7696
		public abstract void Check(IX509AttributeCertificate attrCert, PkixCertPath certPath, PkixCertPath holderCertPath, ICollection unresolvedCritExts);

		// Token: 0x06001E11 RID: 7697
		public abstract PkixAttrCertChecker Clone();
	}
}
