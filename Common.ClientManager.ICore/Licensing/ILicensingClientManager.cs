using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.Settings;

namespace TechnoPro.Common.ClientManager.ICore.Licensing
{
	// Token: 0x02000041 RID: 65
	public interface ILicensingClientManager : IWebService
	{
		// Token: 0x060001D9 RID: 473
		LicensingProductStatusResp GetProductStatus(string ProductName);

		// Token: 0x060001DA RID: 474
		LicensingProductStatusResp GetProductStatus(Group Module);

		// Token: 0x060001DB RID: 475
		LicenseState GetLicenseState(LicenseInfoDTO Key);

		// Token: 0x060001DC RID: 476
		IList<LicenseInfoDTO> GetKeys();

		// Token: 0x060001DD RID: 477
		void SaveValidationParameters(string productName, string validationParameters);

		// Token: 0x060001DE RID: 478
		void ImportKey(LicenseInfoDTO license);
	}
}
