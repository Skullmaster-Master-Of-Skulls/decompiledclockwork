using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.ICore
{
	// Token: 0x02000009 RID: 9
	public interface ILicensingManager
	{
		// Token: 0x0600003D RID: 61
		IDictionary<string, LicenseKeyInfo> FromFile(string filename);

		// Token: 0x0600003E RID: 62
		void ImportKey(LicenseKeyInfo keyInfo);

		// Token: 0x0600003F RID: 63
		LicenseKeyInfo GetSupportPlanKey();

		// Token: 0x06000040 RID: 64
		List<LicenseKeyInfo> GetKeys();

		// Token: 0x06000041 RID: 65
		ProductLicenseState GetProductState(string productName, out DateTime? expiryDate);

		// Token: 0x06000042 RID: 66
		List<string> GetProductNames();

		// Token: 0x06000043 RID: 67
		LicenseState GetLicenseState(LicenseKeyInfo key);

		// Token: 0x06000044 RID: 68
		void SaveValidationParameters(string productName, string validationParameters);

		// Token: 0x06000045 RID: 69
		LicenseKeyInfo GetProductKey(string productName);

		// Token: 0x06000046 RID: 70
		void ImportLicenseFromFile(string filename);
	}
}
