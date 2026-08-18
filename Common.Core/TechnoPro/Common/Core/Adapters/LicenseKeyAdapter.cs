using System;
using Infralution.Licensing;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Adapters
{
	// Token: 0x02000172 RID: 370
	public static class LicenseKeyAdapter
	{
		// Token: 0x06001041 RID: 4161 RVA: 0x00077CD8 File Offset: 0x00075ED8
		internal static bool IsValidKey(this LicenseKeyInfo key, string parameters)
		{
			EncryptedLicenseProvider encryptedLicenseProvider = new EncryptedLicenseProvider();
			EncryptedLicenseProvider.SetParameters(parameters);
			EncryptedLicense encryptedLicense = encryptedLicenseProvider.ValidateLicenseKey(key.LicenseKey);
			return encryptedLicense != null;
		}
	}
}
