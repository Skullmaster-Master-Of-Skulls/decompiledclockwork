using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000C7 RID: 199
	public class LegacyDynamicDataReusableClientProxy : WCFTokenBasedReusableClientProxy<ILegacyDynamicData>, ILegacyDynamicData, IService
	{
		// Token: 0x060007D3 RID: 2003 RVA: 0x000149C6 File Offset: 0x00012BC6
		public LegacyDynamicDataReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007D4 RID: 2004 RVA: 0x000149D1 File Offset: 0x00012BD1
		public LegacyDynamicDataReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007D5 RID: 2005 RVA: 0x000149E0 File Offset: 0x00012BE0
		public GetDynamicDataDecryptedPreviewItemsResp GetDynamicDataDecryptedPreviewItems(GetDynamicDataDecryptedPreviewItemsReq Request)
		{
			return this.WrapServiceMethod<GetDynamicDataDecryptedPreviewItemsResp>(() => this.Proxy.GetDynamicDataDecryptedPreviewItems(Request));
		}

		// Token: 0x060007D6 RID: 2006 RVA: 0x00014A18 File Offset: 0x00012C18
		public ReverseEncryptionOnDataResp ReverseEncryptionOnData(ReverseEncryptionOnDataReq Request)
		{
			return this.WrapServiceMethod<ReverseEncryptionOnDataResp>(() => this.Proxy.ReverseEncryptionOnData(Request));
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00014A50 File Offset: 0x00012C50
		public LookupStaffSignatureBase64Resp LookupStaffSignatureBase64(LookupStaffSignatureBase64Req Request)
		{
			return this.WrapServiceMethod<LookupStaffSignatureBase64Resp>(() => this.Proxy.LookupStaffSignatureBase64(Request));
		}
	}
}
