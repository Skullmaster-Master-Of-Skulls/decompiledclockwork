using System;
using System.Collections;
using Org.BouncyCastle.Utilities.Collections;
using Org.BouncyCastle.X509;

namespace Org.BouncyCastle.Pkix
{
	// Token: 0x02000600 RID: 1536
	public abstract class PkixCertPathChecker
	{
		// Token: 0x0600345D RID: 13405
		public abstract void Init(bool forward);

		// Token: 0x0600345E RID: 13406
		public abstract bool IsForwardCheckingSupported();

		// Token: 0x0600345F RID: 13407
		public abstract ISet GetSupportedExtensions();

		// Token: 0x06003460 RID: 13408
		public abstract void Check(X509Certificate cert, ICollection unresolvedCritExts);

		// Token: 0x06003461 RID: 13409 RVA: 0x001454DA File Offset: 0x001444DA
		public virtual object Clone()
		{
			return base.MemberwiseClone();
		}
	}
}
