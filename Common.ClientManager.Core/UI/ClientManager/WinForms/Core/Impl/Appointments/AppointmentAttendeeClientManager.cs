using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.UI.ClientManager.WinForms.Core.Impl.Appointments
{
	// Token: 0x02000003 RID: 3
	public class AppointmentAttendeeClientManager : IAppointmentAttendeeClientManager, IWebService
	{
		// Token: 0x06000007 RID: 7 RVA: 0x0000216C File Offset: 0x0000036C
		public IList<AttendeeDTO> LoadAttendeesByAppointmentId(int appointmentId)
		{
			LoadAttendeesByAppointmentIdReq loadAttendeesByAppointmentIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAttendeesByAppointmentIdReq>();
			loadAttendeesByAppointmentIdReq.AppointmentId = appointmentId;
			return ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().LoadAttendeesByAppointmentId(loadAttendeesByAppointmentIdReq).Attendees;
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000021A4 File Offset: 0x000003A4
		public AttendeeDTO LoadAttendeeById(int appointmentId, int personId)
		{
			LoadAttendeeByIdReq loadAttendeeByIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAttendeeByIdReq>();
			loadAttendeeByIdReq.AppointmentId = appointmentId;
			loadAttendeeByIdReq.PersonId = personId;
			return ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().LoadAttendeeById(loadAttendeeByIdReq).Attendee;
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021E4 File Offset: 0x000003E4
		public AttendeeDTO LoadAttendeeByAttendeeId(int attendeeId)
		{
			LoadAttendeeByAttendeeIdReq loadAttendeeByAttendeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAttendeeByAttendeeIdReq>();
			loadAttendeeByAttendeeIdReq.AttendeeId = attendeeId;
			return ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().LoadAttendeeByAttendeeId(loadAttendeeByAttendeeIdReq).Attendee;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000221C File Offset: 0x0000041C
		public void DeleteAttendee(int appointmentId, int personId)
		{
			DeleteAttendeeReq deleteAttendeeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAttendeeReq>();
			deleteAttendeeReq.AppointmentId = appointmentId;
			deleteAttendeeReq.PersonId = personId;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().DeleteAttendee(deleteAttendeeReq);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002254 File Offset: 0x00000454
		public void DeleteAttendeeByAttendeeId(int attendeeId)
		{
			DeleteAttendeeByAttendeeIdReq deleteAttendeeByAttendeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAttendeeByAttendeeIdReq>();
			deleteAttendeeByAttendeeIdReq.AttendeeId = attendeeId;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().DeleteAttendeeByAttendeeId(deleteAttendeeByAttendeeIdReq);
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002284 File Offset: 0x00000484
		public int InsertOrUpdateAppointmentAttendee(int appointmentId, AttendeeDTO attendee)
		{
			InsertOrUpdateAppointmentAttendeeReq insertOrUpdateAppointmentAttendeeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentAttendeeReq>();
			insertOrUpdateAppointmentAttendeeReq.AppointmentId = appointmentId;
			insertOrUpdateAppointmentAttendeeReq.Attendee = attendee;
			return ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().InsertOrUpdateAppointmentAttendee(insertOrUpdateAppointmentAttendeeReq).AttendeeId;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000022C4 File Offset: 0x000004C4
		public void InsertOrUpdateAppointmentAttendees(int appointmentId, IList<AttendeeDTO> attendees)
		{
			InsertOrUpdateAppointmentAttendeesReq insertOrUpdateAppointmentAttendeesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentAttendeesReq>();
			insertOrUpdateAppointmentAttendeesReq.AppointmentId = appointmentId;
			insertOrUpdateAppointmentAttendeesReq.Attendees = attendees;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().InsertOrUpdateAppointmentAttendees(insertOrUpdateAppointmentAttendeesReq);
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000022FC File Offset: 0x000004FC
		public void RemoveAttendeesNotInList(int appointmentId, IList<int> personIds)
		{
			RemoveAttendeesNotInListReq removeAttendeesNotInListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveAttendeesNotInListReq>();
			removeAttendeesNotInListReq.AppointmentId = appointmentId;
			removeAttendeesNotInListReq.PersonIds = personIds;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().RemoveAttendeesNotInList(removeAttendeesNotInListReq);
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002334 File Offset: 0x00000534
		public void UpdateNoShowValue(int appointmentId, int personId, bool noShowValue)
		{
			UpdateNoShowValueReq updateNoShowValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateNoShowValueReq>();
			updateNoShowValueReq.AppointmentId = appointmentId;
			updateNoShowValueReq.PersonId = personId;
			updateNoShowValueReq.NoShowValue = noShowValue;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().UpdateNoShowValue(updateNoShowValueReq);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002374 File Offset: 0x00000574
		public void UpdateNoShowValueByAttendeeId(int attendeeId, bool noShowValue)
		{
			UpdateNoShowValueByAttendeeIdReq updateNoShowValueByAttendeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateNoShowValueByAttendeeIdReq>();
			updateNoShowValueByAttendeeIdReq.AttendeeId = attendeeId;
			updateNoShowValueByAttendeeIdReq.NoShowValue = noShowValue;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().UpdateNoShowValueByAttendeeId(updateNoShowValueByAttendeeIdReq);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023AC File Offset: 0x000005AC
		public void UpdateMiscCodeValue(int appointmentId, int personId, int misccodeValue)
		{
			UpdateMiscCodeValueReq updateMiscCodeValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMiscCodeValueReq>();
			updateMiscCodeValueReq.AppointmentId = appointmentId;
			updateMiscCodeValueReq.PersonId = personId;
			updateMiscCodeValueReq.MiscCodeValue = misccodeValue;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().UpdateMiscCodeValue(updateMiscCodeValueReq);
		}

		// Token: 0x06000012 RID: 18 RVA: 0x000023EC File Offset: 0x000005EC
		public void UpdateMiscCodeValueByAttendeeId(int attendeeId, int misccodeValue)
		{
			UpdateMiscCodeValueByAttendeeIdReq updateMiscCodeValueByAttendeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMiscCodeValueByAttendeeIdReq>();
			updateMiscCodeValueByAttendeeIdReq.AttendeeId = attendeeId;
			updateMiscCodeValueByAttendeeIdReq.MiscCodeValue = misccodeValue;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().UpdateMiscCodeValueByAttendeeId(updateMiscCodeValueByAttendeeIdReq);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00002424 File Offset: 0x00000624
		public void SwapAttendee(int AppointmentId, int OldPersonId, int NewPersonId)
		{
			SwapAttendeeReq swapAttendeeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SwapAttendeeReq>();
			swapAttendeeReq.AppointmentId = AppointmentId;
			swapAttendeeReq.OldPersonId = OldPersonId;
			swapAttendeeReq.NewPersonId = NewPersonId;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().SwapAttendee(swapAttendeeReq);
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002464 File Offset: 0x00000664
		public AttendeeDTO LoadAttendeeById(int attendeeId)
		{
			LoadAttendeeByAttendeeIdReq loadAttendeeByAttendeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAttendeeByAttendeeIdReq>();
			loadAttendeeByAttendeeIdReq.AttendeeId = attendeeId;
			return ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().LoadAttendeeByAttendeeId(loadAttendeeByAttendeeIdReq).Attendee;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x0000249C File Offset: 0x0000069C
		public void DeleteAttendee(int attendeeId)
		{
			DeleteAttendeeByAttendeeIdReq deleteAttendeeByAttendeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<DeleteAttendeeByAttendeeIdReq>();
			deleteAttendeeByAttendeeIdReq.AttendeeId = attendeeId;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().DeleteAttendeeByAttendeeId(deleteAttendeeByAttendeeIdReq);
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000024CC File Offset: 0x000006CC
		public void UpdateNoShowValue(int attendeeId, bool noShowValue)
		{
			UpdateNoShowValueByAttendeeIdReq updateNoShowValueByAttendeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateNoShowValueByAttendeeIdReq>();
			updateNoShowValueByAttendeeIdReq.AttendeeId = attendeeId;
			updateNoShowValueByAttendeeIdReq.NoShowValue = noShowValue;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().UpdateNoShowValueByAttendeeId(updateNoShowValueByAttendeeIdReq);
		}

		// Token: 0x06000017 RID: 23 RVA: 0x00002504 File Offset: 0x00000704
		public void UpdateMiscCodeValue(int attendeeId, int misccodeValue)
		{
			UpdateMiscCodeValueByAttendeeIdReq updateMiscCodeValueByAttendeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMiscCodeValueByAttendeeIdReq>();
			updateMiscCodeValueByAttendeeIdReq.AttendeeId = attendeeId;
			updateMiscCodeValueByAttendeeIdReq.MiscCodeValue = misccodeValue;
			ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().UpdateMiscCodeValueByAttendeeId(updateMiscCodeValueByAttendeeIdReq);
		}

		// Token: 0x06000018 RID: 24 RVA: 0x0000253C File Offset: 0x0000073C
		public void UpdateNoShowValue(int appointmentId, IList<int> personIds, bool noShowValue)
		{
			foreach (int personId in personIds)
			{
				this.UpdateNoShowValue(appointmentId, personId, noShowValue);
			}
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000258C File Offset: 0x0000078C
		public bool IsAttendeeDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip)
		{
			IsAttendeeDoubleBookedReq isAttendeeDoubleBookedReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsAttendeeDoubleBookedReq>();
			isAttendeeDoubleBookedReq.PersonId = PersonId;
			isAttendeeDoubleBookedReq.StartDateTime = StartDateTime;
			isAttendeeDoubleBookedReq.EndDateTime = EndDateTime;
			isAttendeeDoubleBookedReq.AppointmentIdToSkip = AppointmentIdToSkip;
			return ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().IsAttendeeDoubleBooked(isAttendeeDoubleBookedReq).IsDoubleBooked;
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000025DC File Offset: 0x000007DC
		public IList<int> GetDoubleBookedAttendees(IList<int> PersonIdsToCheck, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip)
		{
			GetDoubleBookedAttendeesReq getDoubleBookedAttendeesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetDoubleBookedAttendeesReq>();
			getDoubleBookedAttendeesReq.PersonIds = PersonIdsToCheck;
			getDoubleBookedAttendeesReq.StartDateTime = StartDateTime;
			getDoubleBookedAttendeesReq.EndDateTime = EndDateTime;
			getDoubleBookedAttendeesReq.AppointmentIdToSkip = AppointmentIdToSkip;
			return ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().GetDoubleBookedAttendees(getDoubleBookedAttendeesReq).DoubleBookedPersonIds;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000262C File Offset: 0x0000082C
		public IList<int> TryToRemoveAttendees(int appointmentId, params int[] personIds)
		{
			TryToRemoveAttendeesReq tryToRemoveAttendeesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TryToRemoveAttendeesReq>();
			tryToRemoveAttendeesReq.AppointmentId = appointmentId;
			tryToRemoveAttendeesReq.PersonIdList = personIds;
			return ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().TryToRemoveAttendees(tryToRemoveAttendeesReq).NotAllowToDeletePersonIdList;
		}

		// Token: 0x0600001C RID: 28 RVA: 0x0000266C File Offset: 0x0000086C
		public IList<int> TryToRemoveAttendees(IList<int> attendeeIds)
		{
			TryToRemoveAttendeesReq tryToRemoveAttendeesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TryToRemoveAttendeesReq>();
			tryToRemoveAttendeesReq.AttendeeIdList = attendeeIds;
			return ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().TryToRemoveAttendees(tryToRemoveAttendeesReq).NotAllowToDeletePersonIdList;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000026A4 File Offset: 0x000008A4
		public Dictionary<int, List<AttendeeDTO>> LoadAttendeesByAppointmentIds(IList<int> appointmentIds)
		{
			LoadAttendeesByAppointmentIdsReq loadAttendeesByAppointmentIdsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAttendeesByAppointmentIdsReq>();
			loadAttendeesByAppointmentIdsReq.AppointmentIds = appointmentIds;
			LoadAttendeesByAppointmentIdsResp loadAttendeesByAppointmentIdsResp = ClientServiceFactory.GetClientInstance<IAppointmentAttendee>().LoadAttendeesByAppointmentIds(loadAttendeesByAppointmentIdsReq);
			return (loadAttendeesByAppointmentIdsResp != null) ? loadAttendeesByAppointmentIdsResp.AppointmentIdsWithAttendees : null;
		}
	}
}
