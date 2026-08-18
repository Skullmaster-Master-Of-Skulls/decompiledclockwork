using System;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Licensing;
using TechnoPro.Common.Core;
using TechnoPro.Common.Core.Mappers;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200005E RID: 94
	public class LicensingServiceManager : ILicensing, IService
	{
		// Token: 0x0600036A RID: 874 RVA: 0x00010018 File Offset: 0x0000E218
		public LicensingProductStatusResp GetProductStatus(LicensingProductStatusReq request)
		{
			LicensingManager licensingManager = new LicensingManager();
			DateTime? expiryDate;
			ProductLicenseState productState = licensingManager.GetProductState(request.ProductName, out expiryDate);
			return new LicensingProductStatusResp
			{
				LicenseStatus = (ProductLicenseStatus)productState,
				ExpiryDate = expiryDate
			};
		}

		// Token: 0x0600036B RID: 875 RVA: 0x00010054 File Offset: 0x0000E254
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600036C RID: 876 RVA: 0x00010068 File Offset: 0x0000E268
		public LicensingImportKeyResp ImportKey(LicensingImportKeyReq licImportKeyReq)
		{
			LicensingManager licensingManager = new LicensingManager();
			licensingManager.ImportKey(licImportKeyReq.License.ToDomainObject());
			return new LicensingImportKeyResp();
		}

		// Token: 0x0600036D RID: 877 RVA: 0x00010098 File Offset: 0x0000E298
		public LicensingSupportPlanKeyResp GetSupportPlanKey(LicensingSupportPlanKeyReq licSupportPlanKeyReq)
		{
			LicensingManager licensingManager = new LicensingManager();
			return new LicensingSupportPlanKeyResp
			{
				LicenseInfo = licensingManager.GetSupportPlanKey().ToDTO()
			};
		}

		// Token: 0x0600036E RID: 878 RVA: 0x000100C8 File Offset: 0x0000E2C8
		public LicensingKeysResp GetKeys(LicensingKeysReq licKeysReq)
		{
			LicensingManager licensingManager = new LicensingManager();
			return new LicensingKeysResp
			{
				Keys = licensingManager.GetKeys().ToDTO()
			};
		}

		// Token: 0x0600036F RID: 879 RVA: 0x000100F8 File Offset: 0x0000E2F8
		public LicensingValidationParametersResp SaveValidationParameters(LicensingValidationParametersReq licValidationParametersReq)
		{
			LicensingManager licensingManager = new LicensingManager();
			licensingManager.SaveValidationParameters(licValidationParametersReq.Parameters.ProductName, licValidationParametersReq.Parameters.Parameters);
			return new LicensingValidationParametersResp();
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00010134 File Offset: 0x0000E334
		public GetLicenseStateResp GetLicenseState(GetLicenseStateReq request)
		{
			LicensingManager licensingManager = new LicensingManager();
			return new GetLicenseStateResp
			{
				Status = licensingManager.GetLicenseState(request.Key.ToDomainObject())
			};
		}
	}
}
