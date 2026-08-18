using System;
using System.ComponentModel;

namespace Telerik.Licensing
{
	// Token: 0x02000423 RID: 1059
	internal static class LicenseFactory
	{
		// Token: 0x06002617 RID: 9751 RVA: 0x0007D1D3 File Offset: 0x0007B3D3
		public static License CreateLicense(ILicenseKey key)
		{
			if (key is DesignTimeKey)
			{
				return DesignTimeLicense.CreateDesigntimeLicense(key);
			}
			if (key is RuntimeKey)
			{
				return RuntimeLicense.CreateRuntimeLicense(key);
			}
			return DefaultLicense.CreateDefaultLicense(key);
		}
	}
}
