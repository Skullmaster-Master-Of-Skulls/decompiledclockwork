using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People.PeopleParameters;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000113 RID: 275
	internal class StudentManagementClientBaseProxy : ClientBase<IStudentManagement>, IStudentManagement, IService
	{
		// Token: 0x06000ACC RID: 2764 RVA: 0x0001B624 File Offset: 0x00019824
		public StudentManagementClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000ACD RID: 2765 RVA: 0x0001B62F File Offset: 0x0001982F
		public StudentManagementClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000ACE RID: 2766 RVA: 0x0001B63C File Offset: 0x0001983C
		public LoadStudentSummaryResp LoadStudentSummary(LoadStudentSummaryReq Request)
		{
			return base.Channel.LoadStudentSummary(Request);
		}

		// Token: 0x06000ACF RID: 2767 RVA: 0x0001B65C File Offset: 0x0001985C
		public PermanentlyDeleteStudentsResp PermanentlyDeleteStudents(PermanentlyDeleteStudentsReq Request)
		{
			return base.Channel.PermanentlyDeleteStudents(Request);
		}
	}
}
