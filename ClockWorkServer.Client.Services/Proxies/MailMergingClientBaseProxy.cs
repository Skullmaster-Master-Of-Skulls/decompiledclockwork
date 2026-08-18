using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000E5 RID: 229
	internal class MailMergingClientBaseProxy : ClientBase<IMailMerging>, IMailMerging, IService
	{
		// Token: 0x060008DD RID: 2269 RVA: 0x00016F38 File Offset: 0x00015138
		public MailMergingClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00016F43 File Offset: 0x00015143
		public MailMergingClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00016F50 File Offset: 0x00015150
		public LookupCodeValuesResp LookupCodeValues(LookupCodeValuesReq Request)
		{
			return base.Channel.LookupCodeValues(Request);
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00016F70 File Offset: 0x00015170
		public ExtractCodesResp ExtractCodes(ExtractCodesReq Request)
		{
			return base.Channel.ExtractCodes(Request);
		}

		// Token: 0x060008E1 RID: 2273 RVA: 0x00016F90 File Offset: 0x00015190
		public MailMergeTextResp MailMergeText(MailMergeTextReq Request)
		{
			return base.Channel.MailMergeText(Request);
		}

		// Token: 0x060008E2 RID: 2274 RVA: 0x00016FB0 File Offset: 0x000151B0
		public OutputTextResp OutputText(OutputTextReq Request)
		{
			return base.Channel.OutputText(Request);
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00016FD0 File Offset: 0x000151D0
		public GetMailMergeCodeDefinitionsForDisplayResp GetMailMergeCodeDefinitionsForDisplay(GetMailMergeCodeDefinitionsForDisplayReq Request)
		{
			return base.Channel.GetMailMergeCodeDefinitionsForDisplay(Request);
		}

		// Token: 0x060008E4 RID: 2276 RVA: 0x00016FF0 File Offset: 0x000151F0
		public TestAllMailMergeCodesResp TestAllMailMergeCodes(TestAllMailMergeCodesReq Request)
		{
			return base.Channel.TestAllMailMergeCodes(Request);
		}
	}
}
