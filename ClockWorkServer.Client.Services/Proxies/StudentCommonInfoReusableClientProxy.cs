using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000110 RID: 272
	public class StudentCommonInfoReusableClientProxy : WCFTokenBasedReusableClientProxy<IStudentCommonInfo>, IStudentCommonInfo, IService
	{
		// Token: 0x06000ABC RID: 2748 RVA: 0x0001B40A File Offset: 0x0001960A
		public StudentCommonInfoReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000ABD RID: 2749 RVA: 0x0001B415 File Offset: 0x00019615
		public StudentCommonInfoReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000ABE RID: 2750 RVA: 0x0001B424 File Offset: 0x00019624
		public LoadStudentCommonInfoResp LoadStudentCommonInfo(LoadStudentCommonInfoReq Request)
		{
			return this.WrapServiceMethod<LoadStudentCommonInfoResp>(() => this.Proxy.LoadStudentCommonInfo(Request));
		}

		// Token: 0x06000ABF RID: 2751 RVA: 0x0001B45C File Offset: 0x0001965C
		public LoadStudentByEmailAddressResp LoadStudentByEmailAddress(LoadStudentByEmailAddressReq Request)
		{
			return this.WrapServiceMethod<LoadStudentByEmailAddressResp>(() => this.Proxy.LoadStudentByEmailAddress(Request));
		}

		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001B494 File Offset: 0x00019694
		public LoadMyStudentsResp LoadMyStudents(LoadMyStudentsReq Request)
		{
			return this.WrapServiceMethod<LoadMyStudentsResp>(() => this.Proxy.LoadMyStudents(Request));
		}

		// Token: 0x06000AC1 RID: 2753 RVA: 0x0001B4CC File Offset: 0x000196CC
		public LoadStudentsWithCommonInfoResp LoadStudentsWithCommonInfo(LoadStudentsWithCommonInfoReq Request)
		{
			return this.WrapServiceMethod<LoadStudentsWithCommonInfoResp>(() => this.Proxy.LoadStudentsWithCommonInfo(Request));
		}
	}
}
