using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.DAO
{
	// Token: 0x0200000D RID: 13
	public interface ILicenseKeyDAO
	{
		// Token: 0x06000014 RID: 20
		LicenseKeyInfo Get(string key);

		// Token: 0x06000015 RID: 21
		IDictionary<string, LicenseKeyInfo> FromFile(string filename);

		// Token: 0x06000016 RID: 22
		void Save(LicenseKeyInfo licenseKeyInfo);

		// Token: 0x06000017 RID: 23
		LicenseKeyInfo GetProductKey(string productName);

		// Token: 0x06000018 RID: 24
		LicenseKeyInfo GetSupportPlanKey();

		// Token: 0x06000019 RID: 25
		List<LicenseKeyInfo> GetKeys();

		// Token: 0x0600001A RID: 26
		List<string> GetProductNames();

		// Token: 0x0600001B RID: 27
		List<LicenseProductInfo> GetProductsInfo();

		// Token: 0x0600001C RID: 28
		void SaveValidationParameters(string productName, string validationParameters);
	}
}
