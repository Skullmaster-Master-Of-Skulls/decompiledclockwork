using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Licensing;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000CD RID: 205
	public class LicensingReusableClientProxy : WCFTokenBasedReusableClientProxy<ILicensing>, ILicensing, IService
	{
		// Token: 0x060007F7 RID: 2039 RVA: 0x00014E76 File Offset: 0x00013076
		public LicensingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007F8 RID: 2040 RVA: 0x00014E81 File Offset: 0x00013081
		public LicensingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007F9 RID: 2041 RVA: 0x00014E90 File Offset: 0x00013090
		public LicensingKeysResp GetKeys(LicensingKeysReq licKeysReq)
		{
			return this.WrapServiceMethod<LicensingKeysResp>(() => this.Proxy.GetKeys(licKeysReq));
		}

		// Token: 0x060007FA RID: 2042 RVA: 0x00014EC8 File Offset: 0x000130C8
		public LicensingProductStatusResp GetProductStatus(LicensingProductStatusReq licensingProductStatusReq)
		{
			return this.WrapServiceMethod<LicensingProductStatusResp>(() => this.Proxy.GetProductStatus(licensingProductStatusReq));
		}

		// Token: 0x060007FB RID: 2043 RVA: 0x00014F00 File Offset: 0x00013100
		public LicensingSupportPlanKeyResp GetSupportPlanKey(LicensingSupportPlanKeyReq licSupportPlanKeyReq)
		{
			return this.WrapServiceMethod<LicensingSupportPlanKeyResp>(() => this.Proxy.GetSupportPlanKey(licSupportPlanKeyReq));
		}

		// Token: 0x060007FC RID: 2044 RVA: 0x00014F38 File Offset: 0x00013138
		public LicensingImportKeyResp ImportKey(LicensingImportKeyReq licImportKeyReq)
		{
			return this.WrapServiceMethod<LicensingImportKeyResp>(() => this.Proxy.ImportKey(licImportKeyReq));
		}

		// Token: 0x060007FD RID: 2045 RVA: 0x00014F70 File Offset: 0x00013170
		public LicensingValidationParametersResp SaveValidationParameters(LicensingValidationParametersReq licValidationParametersReq)
		{
			return this.WrapServiceMethod<LicensingValidationParametersResp>(() => this.Proxy.SaveValidationParameters(licValidationParametersReq));
		}

		// Token: 0x060007FE RID: 2046 RVA: 0x00014FA8 File Offset: 0x000131A8
		public GetLicenseStateResp GetLicenseState(GetLicenseStateReq request)
		{
			return this.WrapServiceMethod<GetLicenseStateResp>(() => this.Proxy.GetLicenseState(request));
		}
	}
}
