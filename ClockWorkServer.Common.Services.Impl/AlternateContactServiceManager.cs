using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.Common.Core.LookupCourses;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.ICore.LookupCourses;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200005F RID: 95
	public class AlternateContactServiceManager : IAlternateContact, IService
	{
		// Token: 0x06000372 RID: 882 RVA: 0x0001016C File Offset: 0x0000E36C
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x06000373 RID: 883 RVA: 0x00010180 File Offset: 0x0000E380
		public CreateAlternateContactResp CreateAlternateContact(CreateAlternateContactReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			int alternateContactId = alternateContactManager.CreateAlternateContact(Request.AltContact.ToDomainObject());
			return new CreateAlternateContactResp
			{
				AlternateContactId = alternateContactId
			};
		}

		// Token: 0x06000374 RID: 884 RVA: 0x000101C0 File Offset: 0x0000E3C0
		public LoadAlternateContactByIdResp LoadAlternateContactById(LoadAlternateContactByIdReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			AlternateContact alternateContact = alternateContactManager.LoadAlternateContactById(Request.AlternateContactId);
			return new LoadAlternateContactByIdResp
			{
				AltContact = alternateContact.ToDTO()
			};
		}

		// Token: 0x06000375 RID: 885 RVA: 0x00010200 File Offset: 0x0000E400
		public LoadAlternateContactsByCourseResp LoadAlternateContactsByCourse(LoadAlternateContactsByCourseReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			IList<AlternateContact> list = alternateContactManager.LoadAlternateContactsByCourse(Request.LuCourseId);
			LoadAlternateContactsByCourseResp loadAlternateContactsByCourseResp = new LoadAlternateContactsByCourseResp();
			IList<AlternateContactDTO> altContacts;
			if (list != null)
			{
				altContacts = list.ToList<AlternateContact>().ConvertAll<AlternateContactDTO>((AlternateContact f) => f.ToDTO());
			}
			else
			{
				altContacts = null;
			}
			loadAlternateContactsByCourseResp.AltContacts = altContacts;
			return loadAlternateContactsByCourseResp;
		}

		// Token: 0x06000376 RID: 886 RVA: 0x00010268 File Offset: 0x0000E468
		public LoadAlternateContactsBySearchStringResp LoadAlternateContactsBySearchString(LoadAlternateContactsBySearchStringReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			IList<AlternateContact> list = alternateContactManager.LoadAlternateContactsBySearchString(Request.SearchString);
			LoadAlternateContactsBySearchStringResp loadAlternateContactsBySearchStringResp = new LoadAlternateContactsBySearchStringResp();
			IList<AlternateContactDTO> altContacts;
			if (list != null)
			{
				altContacts = list.ToList<AlternateContact>().ConvertAll<AlternateContactDTO>((AlternateContact f) => f.ToDTO());
			}
			else
			{
				altContacts = null;
			}
			loadAlternateContactsBySearchStringResp.AltContacts = altContacts;
			return loadAlternateContactsBySearchStringResp;
		}

		// Token: 0x06000377 RID: 887 RVA: 0x000102D0 File Offset: 0x0000E4D0
		public void UpdateAlternateContact(UpdateAlternateContactReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			alternateContactManager.UpdateAlternateContact(Request.AltContact.ToDomainObject());
		}

		// Token: 0x06000378 RID: 888 RVA: 0x000102FC File Offset: 0x0000E4FC
		public void DeleteAlternateContact(DeleteAlternateContactReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			alternateContactManager.DeleteAlternateContact(Request.AlternateContactId);
		}

		// Token: 0x06000379 RID: 889 RVA: 0x00010324 File Offset: 0x0000E524
		public LoadAlternateContactByUsernameResp LoadAlternateContactByUsername(LoadAlternateContactByUsernameReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			AlternateContact alternateContact = alternateContactManager.LoadAlternateContactByUsername(Request.Username);
			return new LoadAlternateContactByUsernameResp
			{
				AlternateContact = alternateContact.ToDTO()
			};
		}

		// Token: 0x0600037A RID: 890 RVA: 0x00010364 File Offset: 0x0000E564
		public void AssignAlternateContactToCourse(AssignAlternateContactToCourseReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			alternateContactManager.AssignAlternateContactToCourse(Request.AlternateContactId, Request.LuCourseId);
		}

		// Token: 0x0600037B RID: 891 RVA: 0x00010394 File Offset: 0x0000E594
		public void RemoveAlternateContactFromCourse(RemoveAlternateContactFromCourseReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			alternateContactManager.RemoveAlternateContactFromCourse(Request.AlternateContactId, Request.LuCourseId);
		}

		// Token: 0x0600037C RID: 892 RVA: 0x000103C4 File Offset: 0x0000E5C4
		public LoadAlternateContactByEmployeeIdResp LoadAlternateContactByEmployeeId(LoadAlternateContactByEmployeeIdReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			AlternateContact alternateContact = alternateContactManager.LoadAlternateContactByEmployeeId(Request.EmployeeId);
			return new LoadAlternateContactByEmployeeIdResp
			{
				AlternateContact = ((alternateContact == null) ? null : alternateContact.ToDTO())
			};
		}

		// Token: 0x0600037D RID: 893 RVA: 0x00010408 File Offset: 0x0000E608
		public GetUniqueCourseRegistrationStartDatesByAlternateContactResp GetUniqueCourseRegistrationStartDatesByAlternateContact(GetUniqueCourseRegistrationStartDatesByAlternateContactReq Request)
		{
			IAlternateContactManager alternateContactManager = new AlternateContactManager(Request.GetOperationContext());
			IList<DateTime> uniqueCourseRegistrationStartDatesByAlternateContact = alternateContactManager.GetUniqueCourseRegistrationStartDatesByAlternateContact(Request.AlternateContactId);
			return new GetUniqueCourseRegistrationStartDatesByAlternateContactResp
			{
				Dates = uniqueCourseRegistrationStartDatesByAlternateContact
			};
		}
	}
}
