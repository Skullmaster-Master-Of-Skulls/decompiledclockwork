using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000D1 RID: 209
	public class AlternateContactReusableClientProxy : WCFTokenBasedReusableClientProxy<IAlternateContact>, IAlternateContact, IService
	{
		// Token: 0x06000815 RID: 2069 RVA: 0x0001529E File Offset: 0x0001349E
		public AlternateContactReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000816 RID: 2070 RVA: 0x000152A9 File Offset: 0x000134A9
		public AlternateContactReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000817 RID: 2071 RVA: 0x000152B8 File Offset: 0x000134B8
		public CreateAlternateContactResp CreateAlternateContact(CreateAlternateContactReq Request)
		{
			return this.WrapServiceMethod<CreateAlternateContactResp>(() => this.Proxy.CreateAlternateContact(Request));
		}

		// Token: 0x06000818 RID: 2072 RVA: 0x000152F0 File Offset: 0x000134F0
		public void DeleteAlternateContact(DeleteAlternateContactReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.DeleteAlternateContact(Request);
			});
		}

		// Token: 0x06000819 RID: 2073 RVA: 0x00015328 File Offset: 0x00013528
		public LoadAlternateContactByIdResp LoadAlternateContactById(LoadAlternateContactByIdReq Request)
		{
			return this.WrapServiceMethod<LoadAlternateContactByIdResp>(() => this.Proxy.LoadAlternateContactById(Request));
		}

		// Token: 0x0600081A RID: 2074 RVA: 0x00015360 File Offset: 0x00013560
		public LoadAlternateContactsByCourseResp LoadAlternateContactsByCourse(LoadAlternateContactsByCourseReq Request)
		{
			return this.WrapServiceMethod<LoadAlternateContactsByCourseResp>(() => this.Proxy.LoadAlternateContactsByCourse(Request));
		}

		// Token: 0x0600081B RID: 2075 RVA: 0x00015398 File Offset: 0x00013598
		public LoadAlternateContactsBySearchStringResp LoadAlternateContactsBySearchString(LoadAlternateContactsBySearchStringReq Request)
		{
			return this.WrapServiceMethod<LoadAlternateContactsBySearchStringResp>(() => this.Proxy.LoadAlternateContactsBySearchString(Request));
		}

		// Token: 0x0600081C RID: 2076 RVA: 0x000153D0 File Offset: 0x000135D0
		public void UpdateAlternateContact(UpdateAlternateContactReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.UpdateAlternateContact(Request);
			});
		}

		// Token: 0x0600081D RID: 2077 RVA: 0x00015408 File Offset: 0x00013608
		public void AssignAlternateContactToCourse(AssignAlternateContactToCourseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.AssignAlternateContactToCourse(Request);
			});
		}

		// Token: 0x0600081E RID: 2078 RVA: 0x00015440 File Offset: 0x00013640
		public LoadAlternateContactByUsernameResp LoadAlternateContactByUsername(LoadAlternateContactByUsernameReq Request)
		{
			return this.WrapServiceMethod<LoadAlternateContactByUsernameResp>(() => this.Proxy.LoadAlternateContactByUsername(Request));
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x00015478 File Offset: 0x00013678
		public void RemoveAlternateContactFromCourse(RemoveAlternateContactFromCourseReq Request)
		{
			this.WrapServiceMethod(delegate()
			{
				this.Proxy.RemoveAlternateContactFromCourse(Request);
			});
		}

		// Token: 0x06000820 RID: 2080 RVA: 0x000154B0 File Offset: 0x000136B0
		public LoadAlternateContactByEmployeeIdResp LoadAlternateContactByEmployeeId(LoadAlternateContactByEmployeeIdReq Request)
		{
			return this.WrapServiceMethod<LoadAlternateContactByEmployeeIdResp>(() => this.Proxy.LoadAlternateContactByEmployeeId(Request));
		}

		// Token: 0x06000821 RID: 2081 RVA: 0x000154E8 File Offset: 0x000136E8
		public GetUniqueCourseRegistrationStartDatesByAlternateContactResp GetUniqueCourseRegistrationStartDatesByAlternateContact(GetUniqueCourseRegistrationStartDatesByAlternateContactReq Request)
		{
			return this.WrapServiceMethod<GetUniqueCourseRegistrationStartDatesByAlternateContactResp>(() => this.Proxy.GetUniqueCourseRegistrationStartDatesByAlternateContact(Request));
		}
	}
}
