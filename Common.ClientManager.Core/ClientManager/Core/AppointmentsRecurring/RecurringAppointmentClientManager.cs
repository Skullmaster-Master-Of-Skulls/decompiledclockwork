using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsWorkshops;
using TechnoPro.Common.ClientManager.Core.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.AppointmentsCalendar;
using TechnoPro.Common.ClientManager.ICore.AppointmentsRecurring;
using TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications;
using TechnoPro.Common.Core.Mappers.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.AppointmentsRecurring
{
	// Token: 0x02000096 RID: 150
	public class RecurringAppointmentClientManager : IRecurringAppointmentClientManager, IWebService
	{
		// Token: 0x0600056B RID: 1387 RVA: 0x00017DA4 File Offset: 0x00015FA4
		public AppointmentRecurringInfoDTO LoadCurrentRecurringAppointmentsSet(int MasterGroupCode)
		{
			LoadCurrentRecurringAppointmentsSetReq loadCurrentRecurringAppointmentsSetReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadCurrentRecurringAppointmentsSetReq>();
			loadCurrentRecurringAppointmentsSetReq.MasterGroupCode = MasterGroupCode;
			return ClientServiceFactory.GetClientInstance<IRecurringAppointment>().LoadCurrentRecurringAppointmentsSet(loadCurrentRecurringAppointmentsSetReq).RecurringSet;
		}

		// Token: 0x0600056C RID: 1388 RVA: 0x00017DDC File Offset: 0x00015FDC
		public void UpdateRecurringAppointmentGroupInformationAndDates(AppointmentRecurringInfoDTO RecurringItems)
		{
			UpdateRecurringAppointmentGroupInformationAndDatesReq updateRecurringAppointmentGroupInformationAndDatesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRecurringAppointmentGroupInformationAndDatesReq>();
			updateRecurringAppointmentGroupInformationAndDatesReq.RecurringSet = RecurringItems;
			ClientServiceFactory.GetClientInstance<IRecurringAppointment>().UpdateRecurringAppointmentGroupInformationAndDates(updateRecurringAppointmentGroupInformationAndDatesReq);
		}

		// Token: 0x0600056D RID: 1389 RVA: 0x0000387F File Offset: 0x00001A7F
		public IList<RecurringInstanceDTO> UpdateRecurringAppointmentInstances(BaseExtendedAppointmentDTO MasterAppointment, IList<RecurringInstanceDTO> RecurringInstances)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600056E RID: 1390 RVA: 0x0000387F File Offset: 0x00001A7F
		public IList<RecurringInstanceDTO> UpdateRecurringAppointmentInstances(AppointmentDTO MasterAppointment, IList<RecurringInstanceDTO> RecurringInstances)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600056F RID: 1391 RVA: 0x00017E0C File Offset: 0x0001600C
		public IList<RecurringInstanceDTO> UpdateRecurringAppointmentInstances(BaseExtendedAppointmentDTO MasterAppointment, IList<RecurringInstanceDTO> RecurringInstances, RecurringInstanceSetModifyBehaviourDTO ModifyBehaviour)
		{
			AppointmentRecurringInfoDTO appointmentRecurringInfoDTO = this.LoadCurrentRecurringAppointmentsSet(MasterAppointment.GroupCode);
			UpdateRecurringAppointmentInstancesReq updateRecurringAppointmentInstancesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRecurringAppointmentInstancesReq>();
			updateRecurringAppointmentInstancesReq.MasterAppointment = MasterAppointment;
			updateRecurringAppointmentInstancesReq.AppointmentsInRecurringSet = RecurringInstances;
			updateRecurringAppointmentInstancesReq.ModifyBehaviour = ModifyBehaviour;
			IList<RecurringInstanceDTO> appointmentsInRecurringSetWithNewAppointmentIds = ClientServiceFactory.GetClientInstance<IRecurringAppointment>().UpdateRecurringAppointmentInstances(updateRecurringAppointmentInstancesReq).AppointmentsInRecurringSetWithNewAppointmentIds;
			List<RecurringInstanceDTO> list = (from g in RecurringInstances
			where g.AppointmentId > 0
			select g).ToList<RecurringInstanceDTO>();
			using (IEnumerator<RecurringInstanceDTO> enumerator = appointmentsInRecurringSetWithNewAppointmentIds.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					RecurringInstanceDTO updatedInstance = enumerator.Current;
					bool flag = updatedInstance.AppointmentId > 0 && list.FirstOrDefault((RecurringInstanceDTO g) => g.AppointmentId == updatedInstance.AppointmentId) == null;
					if (flag)
					{
						list.Add(updatedInstance);
					}
				}
			}
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			AppointmentDTO appointmentDTO = appointmentClientManager.LoadAppointment(MasterAppointment.AppointmentId);
			AppointmentRecurringInfoDTO appointmentRecurringInfoDTO2 = this.LoadCurrentRecurringAppointmentsSet(appointmentDTO.GroupCode);
			using (List<RecurringAppointmentDTO>.Enumerator enumerator2 = appointmentRecurringInfoDTO.Appointments.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					RecurringAppointmentDTO oi = enumerator2.Current;
					bool flag2 = appointmentRecurringInfoDTO2.Appointments.FirstOrDefault((RecurringAppointmentDTO g) => g.AppointmentId == oi.AppointmentId) == null;
					if (flag2)
					{
						appointmentRecurringInfoDTO2.Appointments.Add(oi);
					}
				}
			}
			List<BasicAppointmentInfo> appInfos = appointmentRecurringInfoDTO2.Appointments.ToList<RecurringAppointmentDTO>().ConvertAll<BasicAppointmentInfo>(delegate(RecurringAppointmentDTO g)
			{
				BasicAppointmentInfo basicAppointmentInfo = new BasicAppointmentInfo();
				basicAppointmentInfo.AppointmentId = g.AppointmentId;
				basicAppointmentInfo.StartDateTime = g.StartDateTime;
				basicAppointmentInfo.EndDateTime = g.EndDateTime;
				IList<int> attendeePersonIds;
				if (g.Attendees != null)
				{
					attendeePersonIds = g.Attendees.ToList<AttendeeDTO>().ConvertAll<int>((AttendeeDTO h) => h.Person.PersonId);
				}
				else
				{
					attendeePersonIds = new List<int>();
				}
				basicAppointmentInfo.AttendeePersonIds = attendeePersonIds;
				return basicAppointmentInfo;
			});
			AppointmentNotificationManager.CurrentInstance.NotifyAsync(new AppNotificationMessage
			{
				Code = eAppNotificationMessageCode.AppointmentCreateEnded,
				AppInfos = appInfos
			}, null);
			return appointmentsInRecurringSetWithNewAppointmentIds;
		}

		// Token: 0x06000570 RID: 1392 RVA: 0x00018010 File Offset: 0x00016210
		public void DeleteEntireRecurringSet(int GroupCode)
		{
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			IRecurringAppointment clientInstance = ClientServiceFactory.GetClientInstance<IRecurringAppointment>();
			List<RecurringAppointmentDTO> appointments = clientInstance.LoadCurrentRecurringAppointmentsSet(new LoadCurrentRecurringAppointmentsSetReq
			{
				MasterGroupCode = GroupCode
			}).RecurringSet.Appointments;
			foreach (RecurringAppointmentDTO recurringAppointmentDTO in appointments)
			{
				appointmentClientManager.DeleteAppointment(recurringAppointmentDTO.AppointmentId);
			}
			AppointmentNotificationManager currentInstance = AppointmentNotificationManager.CurrentInstance;
			AppNotificationMessage appNotificationMessage = new AppNotificationMessage();
			appNotificationMessage.Code = eAppNotificationMessageCode.AppointmentCreateEnded;
			appNotificationMessage.AppInfos = appointments.ConvertAll<BasicAppointmentInfo>(delegate(RecurringAppointmentDTO g)
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
			currentInstance.NotifyAsync(appNotificationMessage, null);
		}

		// Token: 0x06000571 RID: 1393 RVA: 0x000180DC File Offset: 0x000162DC
		public IList<RecurringInstanceDTO> UpdateRecurringAppointmentInstances(AppointmentDTO MasterAppointment, IList<RecurringInstanceDTO> RecurringInstances, RecurringInstanceSetModifyBehaviourDTO ModifyBehaviour)
		{
			BaseExtendedAppointmentDTO masterAppointment = MasterAppointment.ToBaseExtendedAppointmentDTO();
			return this.UpdateRecurringAppointmentInstances(masterAppointment, RecurringInstances, ModifyBehaviour);
		}

		// Token: 0x06000572 RID: 1394 RVA: 0x00018100 File Offset: 0x00016300
		public bool IsUserAllowedToEditAllAppointmentsInARecurringSet(int AppointmentId, int PersonId)
		{
			IsUserAllowedToEditAllAppointmentsInARecurringSetReq isUserAllowedToEditAllAppointmentsInARecurringSetReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<IsUserAllowedToEditAllAppointmentsInARecurringSetReq>();
			isUserAllowedToEditAllAppointmentsInARecurringSetReq.AppointmentId = AppointmentId;
			isUserAllowedToEditAllAppointmentsInARecurringSetReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<IRecurringAppointment>().IsUserAllowedToEditAllAppointmentsInARecurringSet(isUserAllowedToEditAllAppointmentsInARecurringSetReq).AllowedToEditEntireGroup;
		}

		// Token: 0x06000573 RID: 1395 RVA: 0x00018140 File Offset: 0x00016340
		public IDictionary<int, bool> LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(int AppointmentId, int PersonId)
		{
			LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq loadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq>();
			loadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq.AppointmentId = AppointmentId;
			loadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq.PersonId = PersonId;
			return ClientServiceFactory.GetClientInstance<IRecurringAppointment>().LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(loadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq).EditPermissions;
		}

		// Token: 0x06000574 RID: 1396 RVA: 0x00018180 File Offset: 0x00016380
		public void UpdateRecurringAppointmentAttendees(int groupCode, int appIdAlreadyUpdated, IList<AttendeeDTO> attendeesAdded, IList<AttendeeDTO> attendeesModified, IList<int> attendeePersonIdsRemoved)
		{
			UpdateRecurringAppointmentAttendeesReq updateRecurringAppointmentAttendeesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRecurringAppointmentAttendeesReq>();
			updateRecurringAppointmentAttendeesReq.GroupCode = groupCode;
			updateRecurringAppointmentAttendeesReq.AppIdAlreadyUpdated = appIdAlreadyUpdated;
			updateRecurringAppointmentAttendeesReq.AttendeesAdded = attendeesAdded;
			updateRecurringAppointmentAttendeesReq.AttendeesModified = attendeesModified;
			updateRecurringAppointmentAttendeesReq.AttendeePersonIdsRemoved = attendeePersonIdsRemoved;
			IList<AppointmentForNotificationDTO> appointmentsForNotification = ClientServiceFactory.GetClientInstance<IRecurringAppointment>().UpdateRecurringAppointmentAttendees(updateRecurringAppointmentAttendeesReq).AppointmentsForNotification;
			AppointmentNotificationManager.CurrentInstance.NotifyOthersAppointmentChangedOrDeletedAsync(appointmentsForNotification.ToArray<AppointmentForNotificationDTO>());
		}

		// Token: 0x06000575 RID: 1397 RVA: 0x000181E8 File Offset: 0x000163E8
		public IList<RecurringInstanceDTO> UpdateRecurringWorkshopAppointmentInstances(WorkshopAppointmentDTO workshopApp, IList<RecurringInstanceDTO> RecurringInstances, RecurringInstanceSetModifyBehaviourDTO ModifyBehaviour)
		{
			AppointmentRecurringInfoDTO appointmentRecurringInfoDTO = this.LoadCurrentRecurringAppointmentsSet(workshopApp.GroupCode);
			UpdateRecurringWorkshopAppointmentInstancesReq updateRecurringWorkshopAppointmentInstancesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<UpdateRecurringWorkshopAppointmentInstancesReq>();
			updateRecurringWorkshopAppointmentInstancesReq.WorkshopApp = workshopApp;
			updateRecurringWorkshopAppointmentInstancesReq.RecurringInstances = RecurringInstances;
			updateRecurringWorkshopAppointmentInstancesReq.ModifyBehaviour = ModifyBehaviour;
			UpdateRecurringWorkshopAppointmentInstancesResp updateRecurringWorkshopAppointmentInstancesResp = ClientServiceFactory.GetClientInstance<IRecurringAppointment>().UpdateRecurringWorkshopAppointmentInstances(updateRecurringWorkshopAppointmentInstancesReq);
			IList<RecurringInstanceDTO> list = (updateRecurringWorkshopAppointmentInstancesResp != null) ? updateRecurringWorkshopAppointmentInstancesResp.RecurringInstances : null;
			List<RecurringInstanceDTO> list2 = (from g in RecurringInstances
			where g.AppointmentId > 0
			select g).ToList<RecurringInstanceDTO>();
			bool flag = list != null;
			if (flag)
			{
				using (IEnumerator<RecurringInstanceDTO> enumerator = list.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						RecurringInstanceDTO updatedInstance = enumerator.Current;
						bool flag2 = updatedInstance.AppointmentId > 0 && list2.FirstOrDefault((RecurringInstanceDTO g) => g.AppointmentId == updatedInstance.AppointmentId) == null;
						if (flag2)
						{
							list2.Add(updatedInstance);
						}
					}
				}
			}
			IAppointmentClientManager appointmentClientManager = new AppointmentClientManager();
			AppointmentDTO appointmentDTO = appointmentClientManager.LoadAppointment(workshopApp.AppointmentId);
			AppointmentRecurringInfoDTO appointmentRecurringInfoDTO2 = this.LoadCurrentRecurringAppointmentsSet(appointmentDTO.GroupCode);
			using (List<RecurringAppointmentDTO>.Enumerator enumerator2 = appointmentRecurringInfoDTO.Appointments.GetEnumerator())
			{
				while (enumerator2.MoveNext())
				{
					RecurringAppointmentDTO oi = enumerator2.Current;
					bool flag3 = appointmentRecurringInfoDTO2.Appointments.FirstOrDefault((RecurringAppointmentDTO g) => g.AppointmentId == oi.AppointmentId) == null;
					if (flag3)
					{
						appointmentRecurringInfoDTO2.Appointments.Add(oi);
					}
				}
			}
			List<BasicAppointmentInfo> appInfos = appointmentRecurringInfoDTO2.Appointments.ToList<RecurringAppointmentDTO>().ConvertAll<BasicAppointmentInfo>(delegate(RecurringAppointmentDTO g)
			{
				BasicAppointmentInfo basicAppointmentInfo = new BasicAppointmentInfo();
				basicAppointmentInfo.AppointmentId = g.AppointmentId;
				basicAppointmentInfo.StartDateTime = g.StartDateTime;
				basicAppointmentInfo.EndDateTime = g.EndDateTime;
				IList<int> attendeePersonIds;
				if (g.Attendees != null)
				{
					attendeePersonIds = g.Attendees.ToList<AttendeeDTO>().ConvertAll<int>((AttendeeDTO h) => h.Person.PersonId);
				}
				else
				{
					attendeePersonIds = new List<int>();
				}
				basicAppointmentInfo.AttendeePersonIds = attendeePersonIds;
				return basicAppointmentInfo;
			});
			AppointmentNotificationManager.CurrentInstance.NotifyAsync(new AppNotificationMessage
			{
				Code = eAppNotificationMessageCode.AppointmentCreateEnded,
				AppInfos = appInfos
			}, null);
			return list;
		}
	}
}
