using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments.BaseAppParameters;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.Appointments;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.Appointments
{
	// Token: 0x0200006C RID: 108
	public class AppointmentAttendeeRestClientManager : BearerTokenRestProxy<IAppointmentAttendeeClientManager>, IAppointmentAttendeeClientManager, IWebService
	{
		// Token: 0x0600040B RID: 1035 RVA: 0x0000C1A9 File Offset: 0x0000A3A9
		public AppointmentAttendeeRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x0600040C RID: 1036 RVA: 0x0000C1B3 File Offset: 0x0000A3B3
		public AppointmentAttendeeRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x0600040D RID: 1037 RVA: 0x0000C1BE File Offset: 0x0000A3BE
		public IList<AttendeeDTO> LoadAttendeesByAppointmentId(int appointmentId)
		{
			return base.GetMany<AttendeeDTO>(string.Format("appointmentattendee/appid/{0}", appointmentId), true);
		}

		// Token: 0x0600040E RID: 1038 RVA: 0x0000C1D7 File Offset: 0x0000A3D7
		public AttendeeDTO LoadAttendeeById(int appointmentId, int personId)
		{
			return base.Get<AttendeeDTO>(string.Format("appointmentattendee/appid/{0}/pid/{1}", appointmentId, personId), true);
		}

		// Token: 0x0600040F RID: 1039 RVA: 0x0000C1F6 File Offset: 0x0000A3F6
		public AttendeeDTO LoadAttendeeById(int attendeeId)
		{
			return base.Get<AttendeeDTO>(string.Format("appointmentattendee/attid/{0}", attendeeId), true);
		}

		// Token: 0x06000410 RID: 1040 RVA: 0x0000C20F File Offset: 0x0000A40F
		public void DeleteAttendee(int appointmentId, int personId)
		{
			base.Delete(string.Format("appointmentattendee/appid/{0}/pid/{1}", appointmentId, personId));
		}

		// Token: 0x06000411 RID: 1041 RVA: 0x0000C22D File Offset: 0x0000A42D
		public void DeleteAttendee(int attendeeId)
		{
			base.Delete(string.Format("appointmentattendee/attid/{0}", attendeeId));
		}

		// Token: 0x06000412 RID: 1042 RVA: 0x0000C248 File Offset: 0x0000A448
		public int InsertOrUpdateAppointmentAttendee(int appointmentId, AttendeeDTO attendee)
		{
			InsertOrUpdateAppointmentAttendeeReq insertOrUpdateAppointmentAttendeeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentAttendeeReq>();
			insertOrUpdateAppointmentAttendeeReq.AppointmentId = appointmentId;
			insertOrUpdateAppointmentAttendeeReq.Attendee = attendee;
			return base.Post<InsertOrUpdateAppointmentAttendeeReq, int>(insertOrUpdateAppointmentAttendeeReq, "appointmentattendee");
		}

		// Token: 0x06000413 RID: 1043 RVA: 0x0000C27C File Offset: 0x0000A47C
		public void InsertOrUpdateAppointmentAttendees(int appointmentId, IList<AttendeeDTO> attendees)
		{
			InsertOrUpdateAppointmentAttendeesReq insertOrUpdateAppointmentAttendeesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<InsertOrUpdateAppointmentAttendeesReq>();
			insertOrUpdateAppointmentAttendeesReq.AppointmentId = appointmentId;
			insertOrUpdateAppointmentAttendeesReq.Attendees = attendees;
			base.Post<InsertOrUpdateAppointmentAttendeesReq>(insertOrUpdateAppointmentAttendeesReq, "appointmentattendee/list");
		}

		// Token: 0x06000414 RID: 1044 RVA: 0x0000C2B0 File Offset: 0x0000A4B0
		public void RemoveAttendeesNotInList(int appointmentId, IList<int> personIds)
		{
			RemoveAttendeesNotInListReq removeAttendeesNotInListReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<RemoveAttendeesNotInListReq>();
			removeAttendeesNotInListReq.AppointmentId = appointmentId;
			removeAttendeesNotInListReq.PersonIds = personIds;
			base.Post<RemoveAttendeesNotInListReq>(removeAttendeesNotInListReq, "appointmentattendee/removeattendeesnotinlist");
		}

		// Token: 0x06000415 RID: 1045 RVA: 0x0000C2E4 File Offset: 0x0000A4E4
		public void UpdateNoShowValue(int appointmentId, int personId, bool noShowValue)
		{
			UpdateNoShowValueReq updateNoShowValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateNoShowValueReq>();
			updateNoShowValueReq.AppointmentId = appointmentId;
			updateNoShowValueReq.PersonId = personId;
			updateNoShowValueReq.NoShowValue = noShowValue;
			base.Put<UpdateNoShowValueReq>(updateNoShowValueReq, "appointmentattendee/updatenoshowvalue");
		}

		// Token: 0x06000416 RID: 1046 RVA: 0x0000C320 File Offset: 0x0000A520
		public void UpdateNoShowValue(int attendeeId, bool noShowValue)
		{
			UpdateNoShowValueByAttendeeIdReq updateNoShowValueByAttendeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateNoShowValueByAttendeeIdReq>();
			updateNoShowValueByAttendeeIdReq.AttendeeId = attendeeId;
			updateNoShowValueByAttendeeIdReq.NoShowValue = noShowValue;
			base.Put<UpdateNoShowValueByAttendeeIdReq>(updateNoShowValueByAttendeeIdReq, "appointmentattendee/updatenoshowvaluebyattendee");
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x0000C354 File Offset: 0x0000A554
		public void UpdateMiscCodeValue(int appointmentId, int personId, int misccodeValue)
		{
			UpdateMiscCodeValueReq updateMiscCodeValueReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMiscCodeValueReq>();
			updateMiscCodeValueReq.AppointmentId = appointmentId;
			updateMiscCodeValueReq.PersonId = personId;
			updateMiscCodeValueReq.MiscCodeValue = misccodeValue;
			base.Put<UpdateMiscCodeValueReq>(updateMiscCodeValueReq, "appointmentattendee/updatemisccodevalue");
		}

		// Token: 0x06000418 RID: 1048 RVA: 0x0000C390 File Offset: 0x0000A590
		public void UpdateMiscCodeValue(int attendeeId, int misccodeValue)
		{
			UpdateMiscCodeValueByAttendeeIdReq updateMiscCodeValueByAttendeeIdReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateMiscCodeValueByAttendeeIdReq>();
			updateMiscCodeValueByAttendeeIdReq.AttendeeId = attendeeId;
			updateMiscCodeValueByAttendeeIdReq.MiscCodeValue = misccodeValue;
			base.Put<UpdateMiscCodeValueByAttendeeIdReq>(updateMiscCodeValueByAttendeeIdReq, "appointmentattendee/updatemisccodevaluebyattendee");
		}

		// Token: 0x06000419 RID: 1049 RVA: 0x0000C3C4 File Offset: 0x0000A5C4
		public void SwapAttendee(int AppointmentId, int OldPersonId, int NewPersonId)
		{
			SwapAttendeeReq swapAttendeeReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SwapAttendeeReq>();
			swapAttendeeReq.AppointmentId = AppointmentId;
			swapAttendeeReq.OldPersonId = OldPersonId;
			swapAttendeeReq.NewPersonId = NewPersonId;
			base.Post<SwapAttendeeReq>(swapAttendeeReq, "appointmentattendee/swapattendees");
		}

		// Token: 0x0600041A RID: 1050 RVA: 0x0000C400 File Offset: 0x0000A600
		public void UpdateNoShowValue(int appointmentId, IList<int> personIds, bool noShowValue)
		{
			foreach (int personId in personIds)
			{
				this.UpdateNoShowValue(appointmentId, personId, noShowValue);
			}
		}

		// Token: 0x0600041B RID: 1051 RVA: 0x0000C44C File Offset: 0x0000A64C
		public bool IsAttendeeDoubleBooked(int PersonId, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip)
		{
			return base.Get<bool>(string.Format("appointmentattendee/isattendeedoublebooked/pid/{0}/range/{1}/{2}/appidtoskip/{3}", new object[]
			{
				PersonId,
				StartDateTime,
				EndDateTime,
				AppointmentIdToSkip
			}), true);
		}

		// Token: 0x0600041C RID: 1052 RVA: 0x0000C48A File Offset: 0x0000A68A
		public IList<int> GetDoubleBookedAttendees(IList<int> PersonIdsToCheck, DateTime StartDateTime, DateTime EndDateTime, int AppointmentIdToSkip)
		{
			return base.GetMany<int>(string.Format("appointmentattendee/doublebookedattendees/pids/{0}/range/{1}/{2}/appidtoskip/{3}", new object[]
			{
				PersonIdsToCheck.CommaSeparatedValuesWithoutSpace<int>(),
				StartDateTime,
				EndDateTime,
				AppointmentIdToSkip
			}), true);
		}

		// Token: 0x0600041D RID: 1053 RVA: 0x0000C4C8 File Offset: 0x0000A6C8
		public IList<int> TryToRemoveAttendees(int appointmentId, params int[] personIds)
		{
			TryToRemoveAttendeesReq tryToRemoveAttendeesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TryToRemoveAttendeesReq>();
			tryToRemoveAttendeesReq.AppointmentId = appointmentId;
			tryToRemoveAttendeesReq.PersonIdList = personIds;
			return base.Post<TryToRemoveAttendeesReq, IList<int>>(tryToRemoveAttendeesReq, "appointmentattendee/trytoremoveattendees");
		}

		// Token: 0x0600041E RID: 1054 RVA: 0x0000C4FC File Offset: 0x0000A6FC
		public IList<int> TryToRemoveAttendees(IList<int> attendeeIds)
		{
			TryToRemoveAttendeesReq tryToRemoveAttendeesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TryToRemoveAttendeesReq>();
			tryToRemoveAttendeesReq.AttendeeIdList = attendeeIds;
			return base.Post<TryToRemoveAttendeesReq, IList<int>>(tryToRemoveAttendeesReq, "appointmentattendee/trytoremoveattendees");
		}

		// Token: 0x0600041F RID: 1055 RVA: 0x0000C527 File Offset: 0x0000A727
		public Dictionary<int, List<AttendeeDTO>> LoadAttendeesByAppointmentIds(IList<int> appointmentIds)
		{
			return base.Get<Dictionary<int, List<AttendeeDTO>>>(string.Format("appointmentattendee/appids/{0}", appointmentIds.CommaSeparatedValuesWithoutSpace<int>()), true);
		}
	}
}
