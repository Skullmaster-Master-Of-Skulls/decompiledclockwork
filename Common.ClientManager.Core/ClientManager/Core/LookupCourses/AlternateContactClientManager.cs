using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.LookupCourses
{
	// Token: 0x0200003E RID: 62
	public class AlternateContactClientManager : IAlternateContactClientManager, IWebService
	{
		// Token: 0x0600023D RID: 573 RVA: 0x0000AB94 File Offset: 0x00008D94
		public int CreateAlternateContact(AlternateContactDTO AltContact)
		{
			CreateAlternateContactReq createAlternateContactReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<CreateAlternateContactReq>();
			createAlternateContactReq.AltContact = AltContact;
			return ClientServiceFactory.GetClientInstance<IAlternateContact>().CreateAlternateContact(createAlternateContactReq).AlternateContactId;
		}

		// Token: 0x0600023E RID: 574 RVA: 0x0000ABCC File Offset: 0x00008DCC
		public AlternateContactDTO LoadAlternateContactById(int AlternateContactId)
		{
			LoadAlternateContactByIdReq loadAlternateContactByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAlternateContactByIdReq>();
			loadAlternateContactByIdReq.AlternateContactId = AlternateContactId;
			return ClientServiceFactory.GetClientInstance<IAlternateContact>().LoadAlternateContactById(loadAlternateContactByIdReq).AltContact;
		}

		// Token: 0x0600023F RID: 575 RVA: 0x0000AC04 File Offset: 0x00008E04
		public AlternateContactDTO LoadAlternateContactByEmployeeId(string EmployeeId)
		{
			LoadAlternateContactByEmployeeIdReq loadAlternateContactByEmployeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAlternateContactByEmployeeIdReq>();
			loadAlternateContactByEmployeeIdReq.EmployeeId = EmployeeId;
			return ClientServiceFactory.GetClientInstance<IAlternateContact>().LoadAlternateContactByEmployeeId(loadAlternateContactByEmployeeIdReq).AlternateContact;
		}

		// Token: 0x06000240 RID: 576 RVA: 0x0000AC3C File Offset: 0x00008E3C
		public IList<AlternateContactDTO> LoadAlternateContactsByCourse(int LuCourseId)
		{
			LoadAlternateContactsByCourseReq loadAlternateContactsByCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAlternateContactsByCourseReq>();
			loadAlternateContactsByCourseReq.LuCourseId = LuCourseId;
			return ClientServiceFactory.GetClientInstance<IAlternateContact>().LoadAlternateContactsByCourse(loadAlternateContactsByCourseReq).AltContacts;
		}

		// Token: 0x06000241 RID: 577 RVA: 0x0000AC74 File Offset: 0x00008E74
		public IList<AlternateContactDTO> LoadAlternateContactsBySearchString(string SearchString)
		{
			LoadAlternateContactsBySearchStringReq loadAlternateContactsBySearchStringReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAlternateContactsBySearchStringReq>();
			loadAlternateContactsBySearchStringReq.SearchString = SearchString;
			return ClientServiceFactory.GetClientInstance<IAlternateContact>().LoadAlternateContactsBySearchString(loadAlternateContactsBySearchStringReq).AltContacts;
		}

		// Token: 0x06000242 RID: 578 RVA: 0x0000ACAC File Offset: 0x00008EAC
		public void UpdateAlternateContact(AlternateContactDTO AltContact)
		{
			UpdateAlternateContactReq updateAlternateContactReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateAlternateContactReq>();
			updateAlternateContactReq.AltContact = AltContact;
			ClientServiceFactory.GetClientInstance<IAlternateContact>().UpdateAlternateContact(updateAlternateContactReq);
		}

		// Token: 0x06000243 RID: 579 RVA: 0x0000ACDC File Offset: 0x00008EDC
		public void DeleteAlternateContact(int AlternateContactId)
		{
			DeleteAlternateContactReq deleteAlternateContactReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAlternateContactReq>();
			deleteAlternateContactReq.AlternateContactId = AlternateContactId;
			ClientServiceFactory.GetClientInstance<IAlternateContact>().DeleteAlternateContact(deleteAlternateContactReq);
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AD0C File Offset: 0x00008F0C
		public AlternateContactDTO LoadAlternateContactByUsername(string Username)
		{
			LoadAlternateContactByUsernameReq loadAlternateContactByUsernameReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAlternateContactByUsernameReq>();
			loadAlternateContactByUsernameReq.Username = Username;
			return ClientServiceFactory.GetClientInstance<IAlternateContact>().LoadAlternateContactByUsername(loadAlternateContactByUsernameReq).AlternateContact;
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AD44 File Offset: 0x00008F44
		public void AssignAlternateContactToCourse(int AlternateContactId, int LuCourseId)
		{
			AssignAlternateContactToCourseReq assignAlternateContactToCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<AssignAlternateContactToCourseReq>();
			assignAlternateContactToCourseReq.AlternateContactId = AlternateContactId;
			assignAlternateContactToCourseReq.LuCourseId = LuCourseId;
			ClientServiceFactory.GetClientInstance<IAlternateContact>().AssignAlternateContactToCourse(assignAlternateContactToCourseReq);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AD7C File Offset: 0x00008F7C
		public void RemoveAlternateContactFromCourse(int AlternateContactId, int LuCourseId)
		{
			RemoveAlternateContactFromCourseReq removeAlternateContactFromCourseReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveAlternateContactFromCourseReq>();
			removeAlternateContactFromCourseReq.AlternateContactId = AlternateContactId;
			removeAlternateContactFromCourseReq.LuCourseId = LuCourseId;
			ClientServiceFactory.GetClientInstance<IAlternateContact>().RemoveAlternateContactFromCourse(removeAlternateContactFromCourseReq);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000ADB4 File Offset: 0x00008FB4
		public IList<DateTime> GetUniqueCourseRegistrationStartDatesByAlternateContact(int AlternateContactId)
		{
			GetUniqueCourseRegistrationStartDatesByAlternateContactReq getUniqueCourseRegistrationStartDatesByAlternateContactReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetUniqueCourseRegistrationStartDatesByAlternateContactReq>();
			getUniqueCourseRegistrationStartDatesByAlternateContactReq.AlternateContactId = AlternateContactId;
			return ClientServiceFactory.GetClientInstance<IAlternateContact>().GetUniqueCourseRegistrationStartDatesByAlternateContact(getUniqueCourseRegistrationStartDatesByAlternateContactReq).Dates;
		}
	}
}
