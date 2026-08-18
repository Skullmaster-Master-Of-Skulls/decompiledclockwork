using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000D9 RID: 217
	public class LookupSubjectReusableClientProxy : WCFTokenBasedReusableClientProxy<ILookupSubject>, ILookupSubject, IService
	{
		// Token: 0x06000881 RID: 2177 RVA: 0x000162AE File Offset: 0x000144AE
		public LookupSubjectReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000882 RID: 2178 RVA: 0x000162B9 File Offset: 0x000144B9
		public LookupSubjectReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000883 RID: 2179 RVA: 0x000162C8 File Offset: 0x000144C8
		public LoadLookupSubjectResp LoadLookupSubject(LoadLookupSubjectReq Request)
		{
			return this.WrapServiceMethod<LoadLookupSubjectResp>(() => this.Proxy.LoadLookupSubject(Request));
		}

		// Token: 0x06000884 RID: 2180 RVA: 0x00016300 File Offset: 0x00014500
		public LoadLookupSubjectByIdResp LoadLookupSubjectById(LoadLookupSubjectByIdReq Request)
		{
			return this.WrapServiceMethod<LoadLookupSubjectByIdResp>(() => this.Proxy.LoadLookupSubjectById(Request));
		}

		// Token: 0x06000885 RID: 2181 RVA: 0x00016338 File Offset: 0x00014538
		public LoadLookupSubjectBySubjectCodeResp LoadLookupSubjectBySubjectCode(LoadLookupSubjectBySubjectCodeReq Request)
		{
			return this.WrapServiceMethod<LoadLookupSubjectBySubjectCodeResp>(() => this.Proxy.LoadLookupSubjectBySubjectCode(Request));
		}

		// Token: 0x06000886 RID: 2182 RVA: 0x00016370 File Offset: 0x00014570
		public LoadLookupSubjectBySubjectDescriptionResp LoadLookupSubjectBySubjectDescription(LoadLookupSubjectBySubjectDescriptionReq Request)
		{
			return this.WrapServiceMethod<LoadLookupSubjectBySubjectDescriptionResp>(() => this.Proxy.LoadLookupSubjectBySubjectDescription(Request));
		}

		// Token: 0x06000887 RID: 2183 RVA: 0x000163A8 File Offset: 0x000145A8
		public LoadLookupSubjectsBySessionResp LoadLookupSubjectsBySession(LoadLookupSubjectsBySessionReq Request)
		{
			return this.WrapServiceMethod<LoadLookupSubjectsBySessionResp>(() => this.Proxy.LoadLookupSubjectsBySession(Request));
		}

		// Token: 0x06000888 RID: 2184 RVA: 0x000163E0 File Offset: 0x000145E0
		public SaveSubjectResp SaveSubject(SaveSubjectReq Request)
		{
			return this.WrapServiceMethod<SaveSubjectResp>(() => this.Proxy.SaveSubject(Request));
		}

		// Token: 0x06000889 RID: 2185 RVA: 0x00016418 File Offset: 0x00014618
		public LoadAllLookupSubjectsResp LoadAllLookupSubjects(LoadAllLookupSubjectsReq Request)
		{
			return this.WrapServiceMethod<LoadAllLookupSubjectsResp>(() => this.Proxy.LoadAllLookupSubjects(Request));
		}
	}
}
