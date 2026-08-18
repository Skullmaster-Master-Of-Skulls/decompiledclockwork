using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000112 RID: 274
	public class StudentManagementReusableClientProxy : WCFTokenBasedReusableClientProxy<IStudentManagement>, IStudentManagement, IService
	{
		// Token: 0x06000AC8 RID: 2760 RVA: 0x0001B59A File Offset: 0x0001979A
		public StudentManagementReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000AC9 RID: 2761 RVA: 0x0001B5A5 File Offset: 0x000197A5
		public StudentManagementReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000ACA RID: 2762 RVA: 0x0001B5B4 File Offset: 0x000197B4
		public LoadStudentSummaryResp LoadStudentSummary(LoadStudentSummaryReq Request)
		{
			return this.WrapServiceMethod<LoadStudentSummaryResp>(() => this.Proxy.LoadStudentSummary(Request));
		}

		// Token: 0x06000ACB RID: 2763 RVA: 0x0001B5EC File Offset: 0x000197EC
		public PermanentlyDeleteStudentsResp PermanentlyDeleteStudents(PermanentlyDeleteStudentsReq Request)
		{
			return this.WrapServiceMethod<PermanentlyDeleteStudentsResp>(() => this.Proxy.PermanentlyDeleteStudents(Request));
		}
	}
}
