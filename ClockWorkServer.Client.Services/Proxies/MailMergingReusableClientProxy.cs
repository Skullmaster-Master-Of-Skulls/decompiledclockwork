using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000E4 RID: 228
	public class MailMergingReusableClientProxy : WCFTokenBasedReusableClientProxy<IMailMerging>, IMailMerging, IService
	{
		// Token: 0x060008D5 RID: 2261 RVA: 0x00016DCE File Offset: 0x00014FCE
		public MailMergingReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x060008D6 RID: 2262 RVA: 0x00016DD9 File Offset: 0x00014FD9
		public MailMergingReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00016DE8 File Offset: 0x00014FE8
		public LookupCodeValuesResp LookupCodeValues(LookupCodeValuesReq Request)
		{
			return this.WrapServiceMethod<LookupCodeValuesResp>(() => this.Proxy.LookupCodeValues(Request));
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00016E20 File Offset: 0x00015020
		public ExtractCodesResp ExtractCodes(ExtractCodesReq Request)
		{
			return this.WrapServiceMethod<ExtractCodesResp>(() => this.Proxy.ExtractCodes(Request));
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00016E58 File Offset: 0x00015058
		public MailMergeTextResp MailMergeText(MailMergeTextReq Request)
		{
			return this.WrapServiceMethod<MailMergeTextResp>(() => this.Proxy.MailMergeText(Request));
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00016E90 File Offset: 0x00015090
		public OutputTextResp OutputText(OutputTextReq Request)
		{
			return this.WrapServiceMethod<OutputTextResp>(() => this.Proxy.OutputText(Request));
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00016EC8 File Offset: 0x000150C8
		public GetMailMergeCodeDefinitionsForDisplayResp GetMailMergeCodeDefinitionsForDisplay(GetMailMergeCodeDefinitionsForDisplayReq Request)
		{
			return this.WrapServiceMethod<GetMailMergeCodeDefinitionsForDisplayResp>(() => this.Proxy.GetMailMergeCodeDefinitionsForDisplay(Request));
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00016F00 File Offset: 0x00015100
		public TestAllMailMergeCodesResp TestAllMailMergeCodes(TestAllMailMergeCodesReq Request)
		{
			return this.WrapServiceMethod<TestAllMailMergeCodesResp>(() => this.Proxy.TestAllMailMergeCodes(Request));
		}
	}
}
