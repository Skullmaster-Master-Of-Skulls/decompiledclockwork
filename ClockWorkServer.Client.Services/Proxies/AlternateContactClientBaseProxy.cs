using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000D2 RID: 210
	internal class AlternateContactClientBaseProxy : ClientBase<IAlternateContact>, IAlternateContact, IService
	{
		// Token: 0x06000822 RID: 2082 RVA: 0x00015520 File Offset: 0x00013720
		public AlternateContactClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000823 RID: 2083 RVA: 0x0001552B File Offset: 0x0001372B
		public AlternateContactClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000824 RID: 2084 RVA: 0x00015538 File Offset: 0x00013738
		public CreateAlternateContactResp CreateAlternateContact(CreateAlternateContactReq Request)
		{
			return base.Channel.CreateAlternateContact(Request);
		}

		// Token: 0x06000825 RID: 2085 RVA: 0x00015556 File Offset: 0x00013756
		public void DeleteAlternateContact(DeleteAlternateContactReq Request)
		{
			base.Channel.DeleteAlternateContact(Request);
		}

		// Token: 0x06000826 RID: 2086 RVA: 0x00015568 File Offset: 0x00013768
		public LoadAlternateContactByIdResp LoadAlternateContactById(LoadAlternateContactByIdReq Request)
		{
			return base.Channel.LoadAlternateContactById(Request);
		}

		// Token: 0x06000827 RID: 2087 RVA: 0x00015588 File Offset: 0x00013788
		public LoadAlternateContactsByCourseResp LoadAlternateContactsByCourse(LoadAlternateContactsByCourseReq Request)
		{
			return base.Channel.LoadAlternateContactsByCourse(Request);
		}

		// Token: 0x06000828 RID: 2088 RVA: 0x000155A8 File Offset: 0x000137A8
		public LoadAlternateContactsBySearchStringResp LoadAlternateContactsBySearchString(LoadAlternateContactsBySearchStringReq Request)
		{
			return base.Channel.LoadAlternateContactsBySearchString(Request);
		}

		// Token: 0x06000829 RID: 2089 RVA: 0x000155C6 File Offset: 0x000137C6
		public void UpdateAlternateContact(UpdateAlternateContactReq Request)
		{
			base.Channel.UpdateAlternateContact(Request);
		}

		// Token: 0x0600082A RID: 2090 RVA: 0x000155D6 File Offset: 0x000137D6
		public void AssignAlternateContactToCourse(AssignAlternateContactToCourseReq Request)
		{
			base.Channel.AssignAlternateContactToCourse(Request);
		}

		// Token: 0x0600082B RID: 2091 RVA: 0x000155E8 File Offset: 0x000137E8
		public LoadAlternateContactByUsernameResp LoadAlternateContactByUsername(LoadAlternateContactByUsernameReq Request)
		{
			return base.Channel.LoadAlternateContactByUsername(Request);
		}

		// Token: 0x0600082C RID: 2092 RVA: 0x00015606 File Offset: 0x00013806
		public void RemoveAlternateContactFromCourse(RemoveAlternateContactFromCourseReq Request)
		{
			base.Channel.RemoveAlternateContactFromCourse(Request);
		}

		// Token: 0x0600082D RID: 2093 RVA: 0x00015618 File Offset: 0x00013818
		public LoadAlternateContactByEmployeeIdResp LoadAlternateContactByEmployeeId(LoadAlternateContactByEmployeeIdReq Request)
		{
			return base.Channel.LoadAlternateContactByEmployeeId(Request);
		}

		// Token: 0x0600082E RID: 2094 RVA: 0x00015638 File Offset: 0x00013838
		public GetUniqueCourseRegistrationStartDatesByAlternateContactResp GetUniqueCourseRegistrationStartDatesByAlternateContact(GetUniqueCourseRegistrationStartDatesByAlternateContactReq Request)
		{
			return base.Channel.GetUniqueCourseRegistrationStartDatesByAlternateContact(Request);
		}
	}
}
