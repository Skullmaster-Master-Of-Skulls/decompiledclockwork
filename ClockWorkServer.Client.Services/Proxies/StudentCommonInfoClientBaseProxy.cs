using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x02000111 RID: 273
	internal class StudentCommonInfoClientBaseProxy : ClientBase<IStudentCommonInfo>, IStudentCommonInfo, IService
	{
		// Token: 0x06000AC2 RID: 2754 RVA: 0x0001B504 File Offset: 0x00019704
		public StudentCommonInfoClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000AC3 RID: 2755 RVA: 0x0001B50F File Offset: 0x0001970F
		public StudentCommonInfoClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000AC4 RID: 2756 RVA: 0x0001B51C File Offset: 0x0001971C
		public LoadStudentCommonInfoResp LoadStudentCommonInfo(LoadStudentCommonInfoReq Request)
		{
			return base.Channel.LoadStudentCommonInfo(Request);
		}

		// Token: 0x06000AC5 RID: 2757 RVA: 0x0001B53C File Offset: 0x0001973C
		public LoadStudentByEmailAddressResp LoadStudentByEmailAddress(LoadStudentByEmailAddressReq Request)
		{
			return base.Channel.LoadStudentByEmailAddress(Request);
		}

		// Token: 0x06000AC6 RID: 2758 RVA: 0x0001B55C File Offset: 0x0001975C
		public LoadMyStudentsResp LoadMyStudents(LoadMyStudentsReq Request)
		{
			return base.Channel.LoadMyStudents(Request);
		}

		// Token: 0x06000AC7 RID: 2759 RVA: 0x0001B57C File Offset: 0x0001977C
		public LoadStudentsWithCommonInfoResp LoadStudentsWithCommonInfo(LoadStudentsWithCommonInfoReq Request)
		{
			return base.Channel.LoadStudentsWithCommonInfo(Request);
		}
	}
}
