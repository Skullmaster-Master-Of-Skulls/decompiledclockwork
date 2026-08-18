using System;

namespace Spire.License
{
	// Token: 0x02000005 RID: 5
	public abstract class LicenseInfoAdapter : BaseLicenseInfo
	{
		// Token: 0x06000022 RID: 34
		public abstract LicenseInfo ConvertToCurrentVersion();

		// Token: 0x06000023 RID: 35
		public abstract BaseLicenseInfo ConvertFromCurrentVersion(LicenseInfo license);
	}
}
