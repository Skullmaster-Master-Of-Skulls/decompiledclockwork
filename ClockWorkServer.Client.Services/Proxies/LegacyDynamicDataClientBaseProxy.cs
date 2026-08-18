using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Legacy.DynamicData;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000C8 RID: 200
	internal class LegacyDynamicDataClientBaseProxy : ClientBase<ILegacyDynamicData>, ILegacyDynamicData, IService
	{
		// Token: 0x060007D8 RID: 2008 RVA: 0x00014A88 File Offset: 0x00012C88
		public LegacyDynamicDataClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00014A93 File Offset: 0x00012C93
		public LegacyDynamicDataClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00014AA0 File Offset: 0x00012CA0
		public GetDynamicDataDecryptedPreviewItemsResp GetDynamicDataDecryptedPreviewItems(GetDynamicDataDecryptedPreviewItemsReq Request)
		{
			return base.Channel.GetDynamicDataDecryptedPreviewItems(Request);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00014AC0 File Offset: 0x00012CC0
		public ReverseEncryptionOnDataResp ReverseEncryptionOnData(ReverseEncryptionOnDataReq Request)
		{
			return base.Channel.ReverseEncryptionOnData(Request);
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00014AE0 File Offset: 0x00012CE0
		public LookupStaffSignatureBase64Resp LookupStaffSignatureBase64(LookupStaffSignatureBase64Req Request)
		{
			return base.Channel.LookupStaffSignatureBase64(Request);
		}
	}
}
