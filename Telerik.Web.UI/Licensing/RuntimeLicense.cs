using System;

namespace Telerik.Licensing
{
	// Token: 0x02000424 RID: 1060
	internal class RuntimeLicense : DesignTimeLicense
	{
		// Token: 0x06002618 RID: 9752 RVA: 0x0007D1F9 File Offset: 0x0007B3F9
		internal RuntimeLicense(ILicenseKey licenseKey) : base(licenseKey)
		{
		}

		// Token: 0x06002619 RID: 9753 RVA: 0x0007D202 File Offset: 0x0007B402
		internal static RuntimeLicense CreateRuntimeLicense(ILicenseKey key)
		{
			return new RuntimeLicense(key);
		}
	}
}
