using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsRecurring;
using TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.AppointmentsRecurring
{
	// Token: 0x02000080 RID: 128
	public class RecurringAppointmentRestClientManager : BearerTokenRestProxy<IRecurringAppointmentClientManager>, IRecurringAppointmentClientManager, IWebService
	{
		// Token: 0x060004F0 RID: 1264 RVA: 0x0000DCFC File Offset: 0x0000BEFC
		public RecurringAppointmentRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060004F1 RID: 1265 RVA: 0x0000DD06 File Offset: 0x0000BF06
		public RecurringAppointmentRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0000DD11 File Offset: 0x0000BF11
		public AppointmentRecurringInfoDTO LoadCurrentRecurringAppointmentsSet(int MasterGroupCode)
		{
			return base.Get<AppointmentRecurringInfoDTO>(string.Format("recurringappointment/current/mastergroupcode/{0}", MasterGroupCode), true);
		}

		// Token: 0x060004F3 RID: 1267 RVA: 0x0000DD2A File Offset: 0x0000BF2A
		public void UpdateRecurringAppointmentGroupInformationAndDates(AppointmentRecurringInfoDTO RecurringItems)
		{
			base.Put<AppointmentRecurringInfoDTO>(RecurringItems, "recurringappointment/groupinformationanddates");
		}

		// Token: 0x060004F4 RID: 1268 RVA: 0x0000DD38 File Offset: 0x0000BF38
		public IList<RecurringInstanceDTO> UpdateRecurringAppointmentInstances(BaseExtendedAppointmentDTO MasterAppointment, IList<RecurringInstanceDTO> RecurringInstances, RecurringInstanceSetModifyBehaviourDTO ModifyBehaviour)
		{
			UpdateRecurringAppointmentInstancesReq updateRecurringAppointmentInstancesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRecurringAppointmentInstancesReq>();
			updateRecurringAppointmentInstancesReq.MasterAppointment = MasterAppointment;
			updateRecurringAppointmentInstancesReq.AppointmentsInRecurringSet = RecurringInstances;
			updateRecurringAppointmentInstancesReq.ModifyBehaviour = ModifyBehaviour;
			return base.Post<UpdateRecurringAppointmentInstancesReq, IList<RecurringInstanceDTO>>(updateRecurringAppointmentInstancesReq, "recurringappointment/updateinstances");
		}

		// Token: 0x060004F5 RID: 1269 RVA: 0x0000DD74 File Offset: 0x0000BF74
		public IList<RecurringInstanceDTO> UpdateRecurringAppointmentInstances(AppointmentDTO MasterAppointment, IList<RecurringInstanceDTO> RecurringInstances, RecurringInstanceSetModifyBehaviourDTO ModifyBehaviour)
		{
			UpdateRecurringAppointmentInstancesReq updateRecurringAppointmentInstancesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRecurringAppointmentInstancesReq>();
			updateRecurringAppointmentInstancesReq.MasterAppointment = MasterAppointment;
			updateRecurringAppointmentInstancesReq.AppointmentsInRecurringSet = RecurringInstances;
			updateRecurringAppointmentInstancesReq.ModifyBehaviour = ModifyBehaviour;
			return base.Post<UpdateRecurringAppointmentInstancesReq, IList<RecurringInstanceDTO>>(updateRecurringAppointmentInstancesReq, "recurringappointment/updateinstances");
		}

		// Token: 0x060004F6 RID: 1270 RVA: 0x0000DDB0 File Offset: 0x0000BFB0
		public void DeleteEntireRecurringSet(int GroupCode)
		{
			IAppointmentClientManager appointmentClientManager = ObjectFactory.Resolve<IAppointmentClientManager>();
			List<RecurringAppointmentDTO> appSet = this.LoadCurrentRecurringAppointmentsSet(GroupCode).Appointments;
			foreach (RecurringAppointmentDTO recurringAppointmentDTO in appSet)
			{
				appointmentClientManager.DeleteAppointment(recurringAppointmentDTO.AppointmentId);
			}
			Task.Run(delegate()
			{
				AppointmentNotificationManager currentInstance = AppointmentNotificationManager.CurrentInstance;
				AppNotificationMessage appNotificationMessage = new AppNotificationMessage();
				appNotificationMessage.Code = eAppNotificationMessageCode.AppointmentCreateEnded;
				appNotificationMessage.AppInfos = appSet.ConvertAll<BasicAppointmentInfo>(delegate(RecurringAppointmentDTO g)
				{
					BasicAppointmentInfo basicAppointmentInfo = new BasicAppointmentInfo();
					IList<int> attendeePersonIds;
					if (g.Attendees != null)
					{
						attendeePersonIds = g.Attendees.ToList<AttendeeDTO>().ConvertAll<int>((AttendeeDTO q) => q.Person.PersonId);
					}
					else
					{
						attendeePersonIds = new List<int>();
					}
					basicAppointmentInfo.AttendeePersonIds = attendeePersonIds;
					basicAppointmentInfo.AppointmentId = g.AppointmentId;
					basicAppointmentInfo.StartDateTime = g.StartDateTime;
					basicAppointmentInfo.EndDateTime = g.EndDateTime;
					return basicAppointmentInfo;
				});
				return currentInstance.NotifyAsync(appNotificationMessage, null);
			});
		}

		// Token: 0x060004F7 RID: 1271 RVA: 0x0000DE38 File Offset: 0x0000C038
		public bool IsUserAllowedToEditAllAppointmentsInARecurringSet(int AppointmentId, int PersonId)
		{
			return base.Get<bool>(string.Format("recurringappointment/isuserallowedtoeditallappsinarecurringset/appid/{0}/pid/{1}", AppointmentId, PersonId), true);
		}

		// Token: 0x060004F8 RID: 1272 RVA: 0x0000DE57 File Offset: 0x0000C057
		public IDictionary<int, bool> LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(int AppointmentId, int PersonId)
		{
			return base.Get<IDictionary<int, bool>>(string.Format("recurringappointment/appsinarecurringsetwithpermissionstoeditforaspecificuser/appid/{0}/pid/{1}", AppointmentId, PersonId), true);
		}

		// Token: 0x060004F9 RID: 1273 RVA: 0x0000DE78 File Offset: 0x0000C078
		public void UpdateRecurringAppointmentAttendees(int groupCode, int appIdAlreadyUpdated, IList<AttendeeDTO> attendeesAdded, IList<AttendeeDTO> attendeesModified, IList<int> attendeePersonIdsRemoved)
		{
			UpdateRecurringAppointmentAttendeesReq updateRecurringAppointmentAttendeesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRecurringAppointmentAttendeesReq>();
			updateRecurringAppointmentAttendeesReq.GroupCode = groupCode;
			updateRecurringAppointmentAttendeesReq.AppIdAlreadyUpdated = appIdAlreadyUpdated;
			updateRecurringAppointmentAttendeesReq.AttendeesAdded = attendeesAdded;
			updateRecurringAppointmentAttendeesReq.AttendeesModified = attendeesModified;
			updateRecurringAppointmentAttendeesReq.AttendeePersonIdsRemoved = attendeePersonIdsRemoved;
			IList<AppointmentForNotificationDTO> result = base.Post<UpdateRecurringAppointmentAttendeesReq, IList<AppointmentForNotificationDTO>>(updateRecurringAppointmentAttendeesReq, "recurringappointment/updateattendees");
			Task.Run(() => AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(result.ToArray<AppointmentForNotificationDTO>()));
		}
	}
}
