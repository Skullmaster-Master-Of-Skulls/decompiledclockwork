using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MergeDuplicates;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000EF RID: 239
	public class MergeDuplicateStudentReusableClientProxy : WCFTokenBasedReusableClientProxy<IMergeDuplicateStudent>, IMergeDuplicateStudent, IService
	{
		// Token: 0x06000936 RID: 2358 RVA: 0x00017A92 File Offset: 0x00015C92
		public MergeDuplicateStudentReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000937 RID: 2359 RVA: 0x00017A9D File Offset: 0x00015C9D
		public MergeDuplicateStudentReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000938 RID: 2360 RVA: 0x00017AAC File Offset: 0x00015CAC
		public FindPotentialDuplicateStudentsResp FindPotentialDuplicateStudents(FindPotentialDuplicateStudentsReq Request)
		{
			return this.WrapServiceMethod<FindPotentialDuplicateStudentsResp>(() => this.Proxy.FindPotentialDuplicateStudents(Request));
		}

		// Token: 0x06000939 RID: 2361 RVA: 0x00017AE4 File Offset: 0x00015CE4
		public LoadDuplicateStudentPreviewInfoResp LoadDuplicateStudentPreviewInfo(LoadDuplicateStudentPreviewInfoReq Request)
		{
			return this.WrapServiceMethod<LoadDuplicateStudentPreviewInfoResp>(() => this.Proxy.LoadDuplicateStudentPreviewInfo(Request));
		}

		// Token: 0x0600093A RID: 2362 RVA: 0x00017B1C File Offset: 0x00015D1C
		public MergeDuplicateStudentsResp MergeDuplicateStudents(MergeDuplicateStudentsReq Request)
		{
			return this.WrapServiceMethod<MergeDuplicateStudentsResp>(() => this.Proxy.MergeDuplicateStudents(Request));
		}
	}
}
