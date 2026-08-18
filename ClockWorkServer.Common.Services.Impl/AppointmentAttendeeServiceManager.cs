using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000022 RID: 34
	public class AppointmentAttendeeServiceManager : IAppointmentAttendee, IService
	{
		// Token: 0x0600018A RID: 394 RVA: 0x00007F70 File Offset: 0x00006170
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x0600018B RID: 395 RVA: 0x00007F84 File Offset: 0x00006184
		public void UpdateAttendeeNoShow(UpdateAttendeeNoShowReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
		}

		// Token: 0x0600018C RID: 396 RVA: 0x00007FA0 File Offset: 0x000061A0
		public LoadAttendeesByAppointmentIdResp LoadAttendeesByAppointmentId(LoadAttendeesByAppointmentIdReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			IList<Attendee> list = appointmentAttendeeManager.LoadAttendeesByAppointmentId(Request.AppointmentId);
			LoadAttendeesByAppointmentIdResp loadAttendeesByAppointmentIdResp = new LoadAttendeesByAppointmentIdResp();
			IList<AttendeeDTO> attendees;
			if (list == null)
			{
				attendees = null;
			}
			else
			{
				attendees = list.ToList<Attendee>().ConvertAll<AttendeeDTO>((Attendee f) => f.ToDTO());
			}
			loadAttendeesByAppointmentIdResp.Attendees = attendees;
			return loadAttendeesByAppointmentIdResp;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00008008 File Offset: 0x00006208
		public LoadAttendeeByIdResp LoadAttendeeById(LoadAttendeeByIdReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			Attendee attendee = appointmentAttendeeManager.LoadAttendeeById(Request.AppointmentId, Request.PersonId);
			return new LoadAttendeeByIdResp
			{
				Attendee = ((attendee == null) ? null : attendee.ToDTO())
			};
		}

		// Token: 0x0600018E RID: 398 RVA: 0x00008054 File Offset: 0x00006254
		public LoadAttendeeByAttendeeIdResp LoadAttendeeByAttendeeId(LoadAttendeeByAttendeeIdReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			Attendee attendee = appointmentAttendeeManager.LoadAttendeeById(Request.AttendeeId);
			return new LoadAttendeeByAttendeeIdResp
			{
				Attendee = ((attendee == null) ? null : attendee.ToDTO())
			};
		}

		// Token: 0x0600018F RID: 399 RVA: 0x00008098 File Offset: 0x00006298
		public void DeleteAttendee(DeleteAttendeeReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			appointmentAttendeeManager.DeleteAttendee(false, Request.AppointmentId, Request.PersonId);
		}

		// Token: 0x06000190 RID: 400 RVA: 0x000080C8 File Offset: 0x000062C8
		public void DeleteAttendeeByAttendeeId(DeleteAttendeeByAttendeeIdReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			appointmentAttendeeManager.DeleteAttendee(false, Request.AttendeeId);
		}

		// Token: 0x06000191 RID: 401 RVA: 0x000080F0 File Offset: 0x000062F0
		public void RemoveAttendeesNotInList(RemoveAttendeesNotInListReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			appointmentAttendeeManager.RemoveAttendeesNotInList(false, Request.AppointmentId, Request.PersonIds);
		}

		// Token: 0x06000192 RID: 402 RVA: 0x00008120 File Offset: 0x00006320
		public void UpdateNoShowValue(UpdateNoShowValueReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			appointmentAttendeeManager.UpdateNoShowValue(false, Request.AppointmentId, Request.PersonId, Request.NoShowValue);
		}

		// Token: 0x06000193 RID: 403 RVA: 0x00008154 File Offset: 0x00006354
		public void UpdateNoShowValueByAttendeeId(UpdateNoShowValueByAttendeeIdReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			appointmentAttendeeManager.UpdateNoShowValue(false, Request.AttendeeId, Request.NoShowValue);
		}

		// Token: 0x06000194 RID: 404 RVA: 0x00008184 File Offset: 0x00006384
		public void UpdateMiscCodeValue(UpdateMiscCodeValueReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			appointmentAttendeeManager.UpdateMiscCodeValue(false, Request.AppointmentId, Request.PersonId, Request.MiscCodeValue);
		}

		// Token: 0x06000195 RID: 405 RVA: 0x000081B8 File Offset: 0x000063B8
		public void UpdateMiscCodeValueByAttendeeId(UpdateMiscCodeValueByAttendeeIdReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			appointmentAttendeeManager.UpdateMiscCodeValue(false, Request.AttendeeId, Request.MiscCodeValue);
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000081E8 File Offset: 0x000063E8
		public InsertOrUpdateAppointmentAttendeeResp InsertOrUpdateAppointmentAttendee(InsertOrUpdateAppointmentAttendeeReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			int attendeeId = appointmentAttendeeManager.InsertOrUpdateAppointmentAttendee(false, Request.AppointmentId, Request.Attendee.ToDomainObject());
			return new InsertOrUpdateAppointmentAttendeeResp
			{
				AttendeeId = attendeeId
			};
		}

		// Token: 0x06000197 RID: 407 RVA: 0x0000822C File Offset: 0x0000642C
		public void InsertOrUpdateAppointmentAttendees(InsertOrUpdateAppointmentAttendeesReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			appointmentAttendeeManager.InsertOrUpdateAppointmentAttendees(false, Request.AppointmentId, Request.Attendees.ToList<AttendeeDTO>().ConvertAll<Attendee>((AttendeeDTO f) => f.ToDomainObject()));
		}

		// Token: 0x06000198 RID: 408 RVA: 0x00008284 File Offset: 0x00006484
		public void SwapAttendee(SwapAttendeeReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			appointmentAttendeeManager.SwapAttendee(false, Request.AppointmentId, Request.OldPersonId, Request.NewPersonId);
		}

		// Token: 0x06000199 RID: 409 RVA: 0x000082B8 File Offset: 0x000064B8
		public IsAttendeeDoubleBookedResp IsAttendeeDoubleBooked(IsAttendeeDoubleBookedReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			bool isDoubleBooked = appointmentAttendeeManager.IsAttendeeDoubleBooked(Request.PersonId, Request.StartDateTime, Request.EndDateTime, Request.AppointmentIdToSkip);
			return new IsAttendeeDoubleBookedResp
			{
				IsDoubleBooked = isDoubleBooked
			};
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00008304 File Offset: 0x00006504
		public GetDoubleBookedAttendeesResp GetDoubleBookedAttendees(GetDoubleBookedAttendeesReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			IList<int> doubleBookedAttendees = appointmentAttendeeManager.GetDoubleBookedAttendees(Request.PersonIds, Request.StartDateTime, Request.EndDateTime, Request.AppointmentIdToSkip);
			return new GetDoubleBookedAttendeesResp
			{
				DoubleBookedPersonIds = doubleBookedAttendees
			};
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008350 File Offset: 0x00006550
		public TryToRemoveAttendeesResp TryToRemoveAttendees(TryToRemoveAttendeesReq request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(request.GetOperationContext());
			return new TryToRemoveAttendeesResp
			{
				NotAllowToDeletePersonIdList = ((request.AttendeeIdList != null && request.AttendeeIdList.Count > 0) ? appointmentAttendeeManager.TryToRemoveAttendees(request.AttendeeIdList) : appointmentAttendeeManager.TryToRemoveAttendees(request.AppointmentId, request.PersonIdList.ToArray<int>()))
			};
		}

		// Token: 0x0600019C RID: 412 RVA: 0x000083B8 File Offset: 0x000065B8
		public LoadAttendeesByAppointmentIdsResp LoadAttendeesByAppointmentIds(LoadAttendeesByAppointmentIdsReq Request)
		{
			IAppointmentAttendeeManager appointmentAttendeeManager = new AppointmentAttendeeManager(Request.GetOperationContext());
			IDictionary<int, IList<Attendee>> dictionary = appointmentAttendeeManager.LoadAttendeesByAppointmentIds(Request.AppointmentIds);
			Dictionary<int, List<AttendeeDTO>> dictionary2;
			if (dictionary == null)
			{
				dictionary2 = null;
			}
			else
			{
				dictionary2 = dictionary.ToDictionary((KeyValuePair<int, IList<Attendee>> g) => g.Key, (KeyValuePair<int, IList<Attendee>> g) => (from h in g.Value
				select h.ToDTO()).ToList<AttendeeDTO>());
			}
			Dictionary<int, List<AttendeeDTO>> dictionary3 = dictionary2;
			return new LoadAttendeesByAppointmentIdsResp
			{
				AppointmentIdsWithAttendees = ((dictionary == null) ? null : dictionary3)
			};
		}
	}
}
