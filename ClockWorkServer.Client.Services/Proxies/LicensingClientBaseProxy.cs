using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Licensing;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000CE RID: 206
	internal class LicensingClientBaseProxy : ClientBase<ILicensing>, ILicensing, IService
	{
		// Token: 0x060007FF RID: 2047 RVA: 0x00014FE0 File Offset: 0x000131E0
		public LicensingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000800 RID: 2048 RVA: 0x00014FEB File Offset: 0x000131EB
		public LicensingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000801 RID: 2049 RVA: 0x00014FF8 File Offset: 0x000131F8
		public LicensingKeysResp GetKeys(LicensingKeysReq licKeysReq)
		{
			return base.Channel.GetKeys(licKeysReq);
		}

		// Token: 0x06000802 RID: 2050 RVA: 0x00015018 File Offset: 0x00013218
		public LicensingProductStatusResp GetProductStatus(LicensingProductStatusReq licensingProductStatusReq)
		{
			return base.Channel.GetProductStatus(licensingProductStatusReq);
		}

		// Token: 0x06000803 RID: 2051 RVA: 0x00015038 File Offset: 0x00013238
		public LicensingSupportPlanKeyResp GetSupportPlanKey(LicensingSupportPlanKeyReq licSupportPlanKeyReq)
		{
			return base.Channel.GetSupportPlanKey(licSupportPlanKeyReq);
		}

		// Token: 0x06000804 RID: 2052 RVA: 0x00015058 File Offset: 0x00013258
		public LicensingImportKeyResp ImportKey(LicensingImportKeyReq licImportKeyReq)
		{
			return base.Channel.ImportKey(licImportKeyReq);
		}

		// Token: 0x06000805 RID: 2053 RVA: 0x00015078 File Offset: 0x00013278
		public LicensingValidationParametersResp SaveValidationParameters(LicensingValidationParametersReq licValidationParametersReq)
		{
			return base.Channel.SaveValidationParameters(licValidationParametersReq);
		}

		// Token: 0x06000806 RID: 2054 RVA: 0x00015098 File Offset: 0x00013298
		public GetLicenseStateResp GetLicenseState(GetLicenseStateReq request)
		{
			return base.Channel.GetLicenseState(request);
		}
	}
}
