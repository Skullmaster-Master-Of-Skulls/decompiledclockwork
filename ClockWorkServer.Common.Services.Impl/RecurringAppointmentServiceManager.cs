using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsRecurring;
using TechnoPro.Common.Core.AppointmentsRecurring;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AppointmentsRecurring;
using TechnoPro.Common.Core.Mappers.AppointmentsWorkshops;
using TechnoPro.Common.ICore.AppointmentsRecurring;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsRecurring;
using TechnoPro.Common.Public.Entities.AppointmentsWorkshops;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000012 RID: 18
	public class RecurringAppointmentServiceManager : IRecurringAppointment, IService
	{
		// Token: 0x060000F0 RID: 240 RVA: 0x00005968 File Offset: 0x00003B68
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000597C File Offset: 0x00003B7C
		public LoadCurrentRecurringAppointmentsSetResp LoadCurrentRecurringAppointmentsSet(LoadCurrentRecurringAppointmentsSetReq Request)
		{
			IRecurringAppointmentManager recurringAppointmentManager = new RecurringAppointmentManager(Request.GetOperationContext());
			AppointmentRecurringInfo appointmentRecurringInfo = recurringAppointmentManager.LoadCurrentRecurringAppointmentsSet(Request.MasterGroupCode);
			return new LoadCurrentRecurringAppointmentsSetResp
			{
				RecurringSet = ((appointmentRecurringInfo == null) ? null : appointmentRecurringInfo.ToDTO())
			};
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000059C0 File Offset: 0x00003BC0
		public void UpdateRecurringAppointmentGroupInformationAndDates(UpdateRecurringAppointmentGroupInformationAndDatesReq Request)
		{
			IRecurringAppointmentManager recurringAppointmentManager = new RecurringAppointmentManager(Request.GetOperationContext());
			recurringAppointmentManager.UpdateRecurringAppointmentGroupInformationAndDates(Request.RecurringSet.ToDomainObject());
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x000059EC File Offset: 0x00003BEC
		public UpdateRecurringAppointmentInstancesResp UpdateRecurringAppointmentInstances(UpdateRecurringAppointmentInstancesReq Request)
		{
			IRecurringAppointmentManager recurringAppointmentManager = new RecurringAppointmentManager(Request.GetOperationContext());
			IList<RecurringInstance> list = recurringAppointmentManager.UpdateRecurringAppointmentInstances(Request.MasterAppointment.ToDomainObject(), Request.AppointmentsInRecurringSet.ToDomainObject(), Request.ModifyBehaviour.ToDomainObject());
			return new UpdateRecurringAppointmentInstancesResp
			{
				AppointmentsInRecurringSetWithNewAppointmentIds = ((list == null) ? null : list.ToDTO())
			};
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00005A4C File Offset: 0x00003C4C
		public LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserResp LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserReq Request)
		{
			IRecurringAppointmentManager recurringAppointmentManager = new RecurringAppointmentManager(Request.GetOperationContext());
			IDictionary<int, bool> editPermissions = recurringAppointmentManager.LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUser(Request.AppointmentId, Request.PersonId);
			return new LoadAppointmentsInARecurringSetWithPermissionsToEditForASpecificUserResp
			{
				EditPermissions = editPermissions
			};
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x00005A8C File Offset: 0x00003C8C
		public IsUserAllowedToEditAllAppointmentsInARecurringSetResp IsUserAllowedToEditAllAppointmentsInARecurringSet(IsUserAllowedToEditAllAppointmentsInARecurringSetReq Request)
		{
			IRecurringAppointmentManager recurringAppointmentManager = new RecurringAppointmentManager(Request.GetOperationContext());
			bool allowedToEditEntireGroup = recurringAppointmentManager.IsUserAllowedToEditAllAppointmentsInARecurringSet(Request.AppointmentId, Request.PersonId);
			return new IsUserAllowedToEditAllAppointmentsInARecurringSetResp
			{
				AllowedToEditEntireGroup = allowedToEditEntireGroup
			};
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00005ACC File Offset: 0x00003CCC
		public UpdateRecurringAppointmentAttendeesResp UpdateRecurringAppointmentAttendees(UpdateRecurringAppointmentAttendeesReq Request)
		{
			IRecurringAppointmentManager recurringAppointmentManager = new RecurringAppointmentManager(Request.GetOperationContext());
			IList<AppointmentForNotification> list = recurringAppointmentManager.UpdateRecurringAppointmentAttendees(Request.GroupCode, Request.AppIdAlreadyUpdated, (from g in Request.AttendeesAdded
			select g.ToDomainObject()).ToList<Attendee>(), (from g in Request.AttendeesModified
			select g.ToDomainObject()).ToList<Attendee>(), Request.AttendeePersonIdsRemoved);
			UpdateRecurringAppointmentAttendeesResp updateRecurringAppointmentAttendeesResp = new UpdateRecurringAppointmentAttendeesResp();
			IList<AppointmentForNotificationDTO> appointmentsForNotification;
			if (list == null)
			{
				appointmentsForNotification = null;
			}
			else
			{
				appointmentsForNotification = (from g in list
				select g.ToDTO()).ToList<AppointmentForNotificationDTO>();
			}
			updateRecurringAppointmentAttendeesResp.AppointmentsForNotification = appointmentsForNotification;
			return updateRecurringAppointmentAttendeesResp;
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00005BA0 File Offset: 0x00003DA0
		public UpdateRecurringWorkshopAppointmentInstancesResp UpdateRecurringWorkshopAppointmentInstances(UpdateRecurringWorkshopAppointmentInstancesReq Request)
		{
			IRecurringAppointmentManager recurringAppointmentManager = new RecurringAppointmentManager(Request.GetOperationContext());
			WorkshopAppointment workshopAppointment = Request.WorkshopApp.ToDomainObject();
			IRecurringAppointmentManager recurringAppointmentManager2 = recurringAppointmentManager;
			WorkshopAppointment workshopApp = workshopAppointment;
			IList<RecurringInstanceDTO> recurringInstances = Request.RecurringInstances;
			IList<RecurringInstance> recurringInstances2;
			if (recurringInstances == null)
			{
				recurringInstances2 = null;
			}
			else
			{
				recurringInstances2 = (from g in recurringInstances
				select g.ToDomainObject()).ToList<RecurringInstance>();
			}
			RecurringInstanceSetModifyBehaviourDTO modifyBehaviour = Request.ModifyBehaviour;
			IList<RecurringInstance> list = recurringAppointmentManager2.UpdateRecurringWorkshopAppointmentInstances(workshopApp, recurringInstances2, (modifyBehaviour != null) ? modifyBehaviour.ToDomainObject() : null);
			UpdateRecurringWorkshopAppointmentInstancesResp updateRecurringWorkshopAppointmentInstancesResp = new UpdateRecurringWorkshopAppointmentInstancesResp();
			IList<RecurringInstanceDTO> recurringInstances3;
			if (list == null)
			{
				recurringInstances3 = null;
			}
			else
			{
				recurringInstances3 = (from g in list
				select g.ToDTO()).ToList<RecurringInstanceDTO>();
			}
			updateRecurringWorkshopAppointmentInstancesResp.RecurringInstances = recurringInstances3;
			return updateRecurringWorkshopAppointmentInstancesResp;
		}
	}
}
