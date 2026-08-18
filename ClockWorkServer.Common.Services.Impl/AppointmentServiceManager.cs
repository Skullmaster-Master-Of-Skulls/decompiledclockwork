using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar.AppParameters;
using TechnoPro.Common.Core.Appointments;
using TechnoPro.Common.Core.AppointmentsCalendar;
using TechnoPro.Common.Core.Mappers.Appointments;
using TechnoPro.Common.Core.Mappers.AppointmentsCalendar;
using TechnoPro.Common.ICore.Appointments;
using TechnoPro.Common.ICore.AppointmentsCalendar;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.Appointments;
using TechnoPro.Common.Public.Entities.AppointmentsCalendar;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x02000010 RID: 16
	public class AppointmentServiceManager : IAppointment, IService
	{
		// Token: 0x060000CE RID: 206 RVA: 0x00005130 File Offset: 0x00003330
		public int CheckConnectivity()
		{
			return 1;
		}

		// Token: 0x060000CF RID: 207 RVA: 0x00005144 File Offset: 0x00003344
		public LoadDeletedAppointmentByIdResp LoadDeletedAppointmentById(LoadDeletedAppointmentByIdReq request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(request.GetOperationContext());
			Appointment appointment = appointmentManager.LoadDeletedAppointmentById(request.AppointmentId);
			return new LoadDeletedAppointmentByIdResp
			{
				Appointment = ((appointment != null) ? appointment.ToDTO() : null)
			};
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00005188 File Offset: 0x00003388
		public void CancelAppointment(CancelAppointmentReq request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(request.GetOperationContext());
			appointmentManager.CancelAppointment(false, request.AppointmentId, request.AppointmentCancelInfo.ToDomainObject());
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x000051BC File Offset: 0x000033BC
		public void UnCancelAppointment(UnCancelAppointmentReq request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(request.GetOperationContext());
			appointmentManager.UnCancelAppointment(false, request.AppointmentId);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x000051E4 File Offset: 0x000033E4
		public LoadAppointmentsResp LoadAppointments(LoadAppointmentsReq request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(request.GetOperationContext());
			List<Appointment> source = appointmentManager.LoadAppointments(request.PersonIds, request.AppTypeIds, request.HideCancelled, request.LoadPerStudentDataIcons, request.LoadPerAnonymousDataIcons, request.StartDateTime, request.EndDateTime);
			List<AppointmentDTO> appointments = (from app in source
			select app.ToDTO()).ToList<AppointmentDTO>();
			return new LoadAppointmentsResp
			{
				Appointments = appointments
			};
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x0000526C File Offset: 0x0000346C
		public void UnMarkAppointmentTentative(UnMarkAppointmentTentativeReq request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(request.GetOperationContext());
			appointmentManager.UnMarkAppointmentTentative(false, request.AppointmentId);
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00005294 File Offset: 0x00003494
		public void MarkAppointmentTentative(MarkAppointmentTentativeReq request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(request.GetOperationContext());
			appointmentManager.MarkAppointmentTentative(false, request.AppointmentId);
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x000052BC File Offset: 0x000034BC
		public LoadAppointmentByIdResp LoadAppointmentById(LoadAppointmentByIdReq request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(request.GetOperationContext());
			return new LoadAppointmentByIdResp
			{
				Appointment = appointmentManager.LoadAppointment(request.AppointmentId).ToDTO()
			};
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x000052F8 File Offset: 0x000034F8
		public LoadAppointmentExtendedInfoResp LoadAppointmentExtendedInfo(LoadAppointmentExtendedInfoReq request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(request.GetOperationContext());
			return new LoadAppointmentExtendedInfoResp
			{
				OrganizerPersonId = appointmentManager.LoadAppointmentOrganizerPersonId(request.AppointmentId)
			};
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00005330 File Offset: 0x00003530
		public void DeleteAppointment(DeleteAppointmentReq request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(request.GetOperationContext());
			appointmentManager.DeleteAppointment(false, request.AppointmentId);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00005358 File Offset: 0x00003558
		public UpdateAppointmentResp UpdateAppointment(UpdateAppointmentReq Request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(Request.GetOperationContext());
			appointmentManager.UpdateAppointment(false, Request.Appointment.ToDomainObject());
			return new UpdateAppointmentResp();
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00005390 File Offset: 0x00003590
		public CreateAppointmentResp CreateAppointment(CreateAppointmentReq Request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(Request.GetOperationContext());
			int appointmentId = appointmentManager.CreateAppointment(false, Request.Appointment.ToDomainObject());
			return new CreateAppointmentResp
			{
				AppointmentId = appointmentId
			};
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000053D0 File Offset: 0x000035D0
		public void UpdateCalendarAppointmentParts(UpdateCalendarAppointmentPartsReq Request)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(Request.GetOperationContext());
			baseAppointmentManager.UpdateAppointmentParts(false, Request.Appointment.ToDomainObject(), Request.PartsToUpdate);
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00005404 File Offset: 0x00003604
		public void InsertOrUpdateAppointmentMemo(InsertOrUpdateAppointmentMemoReq Request)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(Request.GetOperationContext());
			baseAppointmentManager.InsertOrUpdateAppointmentMemo(false, Request.AppointmentId, Request.MemoText);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00005434 File Offset: 0x00003634
		public FreeTimeSearchResp FreeTimeSearch(FreeTimeSearchReq Request)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(Request.GetOperationContext());
			IList<BaseBasicAppointment> list = baseAppointmentManager.FreeTimeSearch(Request.FreeTimeSearchContext.ToDomainObject());
			FreeTimeSearchResp freeTimeSearchResp = new FreeTimeSearchResp();
			IList<BaseBasicAppointmentDTO> availableSlots;
			if (list == null)
			{
				availableSlots = null;
			}
			else
			{
				availableSlots = list.ToList<BaseBasicAppointment>().ConvertAll<BaseBasicAppointmentDTO>((BaseBasicAppointment g) => g.ToDTO());
			}
			freeTimeSearchResp.AvailableSlots = availableSlots;
			return freeTimeSearchResp;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x000054A0 File Offset: 0x000036A0
		public LoadAppointmentsAndAvailabilityResp LoadAppointmentsAndAvailability(LoadAppointmentsAndAvailabilityReq Request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(Request.GetOperationContext());
			AppointmentsWithAvailabilityAndTimetable appointmentsWithAvailabilityAndTimetable = appointmentManager.LoadAppointmentsAndAvailability(Request.LoadOptions.ToDomainObject());
			AppointmentsWithAvailabilityAndTimetableDTO appointmentsWithAvailabilityAndTimetable2 = (appointmentsWithAvailabilityAndTimetable != null) ? appointmentsWithAvailabilityAndTimetable.ToDTO() : null;
			return new LoadAppointmentsAndAvailabilityResp
			{
				AppointmentsWithAvailabilityAndTimetable = appointmentsWithAvailabilityAndTimetable2
			};
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000054EC File Offset: 0x000036EC
		[DebuggerStepThrough]
		public Task<LoadAppointmentsAndAvailabilityResp> LoadAppointmentsAndAvailabilityAsync(LoadAppointmentsAndAvailabilityReq Request)
		{
			AppointmentServiceManager.<LoadAppointmentsAndAvailabilityAsync>d__16 <LoadAppointmentsAndAvailabilityAsync>d__ = new AppointmentServiceManager.<LoadAppointmentsAndAvailabilityAsync>d__16();
			<LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder = AsyncTaskMethodBuilder<LoadAppointmentsAndAvailabilityResp>.Create();
			<LoadAppointmentsAndAvailabilityAsync>d__.<>4__this = this;
			<LoadAppointmentsAndAvailabilityAsync>d__.Request = Request;
			<LoadAppointmentsAndAvailabilityAsync>d__.<>1__state = -1;
			<LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder.Start<AppointmentServiceManager.<LoadAppointmentsAndAvailabilityAsync>d__16>(ref <LoadAppointmentsAndAvailabilityAsync>d__);
			return <LoadAppointmentsAndAvailabilityAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00005538 File Offset: 0x00003738
		public LoadBasicAppointmentInformationByUserAndDateRangeResp LoadBasicAppointmentInformationByUserAndDateRange(LoadBasicAppointmentInformationByUserAndDateRangeReq Request)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(Request.GetOperationContext());
			IList<BaseBasicAppointment> list = baseAppointmentManager.LoadBaseBasicAppointmentsByPersonAndDateRange(Request.PersonId, Request.HideCancelledAppointments, Request.StartDate, Request.EndDate);
			LoadBasicAppointmentInformationByUserAndDateRangeResp loadBasicAppointmentInformationByUserAndDateRangeResp = new LoadBasicAppointmentInformationByUserAndDateRangeResp();
			IList<BaseBasicAppointmentDTO> appointments;
			if (list == null)
			{
				appointments = null;
			}
			else
			{
				appointments = list.ToList<BaseBasicAppointment>().ConvertAll<BaseBasicAppointmentDTO>((BaseBasicAppointment g) => g.ToDTO());
			}
			loadBasicAppointmentInformationByUserAndDateRangeResp.Appointments = appointments;
			return loadBasicAppointmentInformationByUserAndDateRangeResp;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x000055B4 File Offset: 0x000037B4
		public UpdateAppointmentExternalIdResp UpdateAppointmentExternalId(UpdateAppointmentExternalIdReq request)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(request.GetOperationContext());
			baseAppointmentManager.UpdateAppointmentExternalId(request.AppointmentId, request.ExternalId);
			return new UpdateAppointmentExternalIdResp();
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x000055EC File Offset: 0x000037EC
		public GetNumberOfAppointmentsWithAppTypeResp GetNumberOfAppointmentsWithAppType(GetNumberOfAppointmentsWithAppTypeReq Request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(Request.GetOperationContext());
			return new GetNumberOfAppointmentsWithAppTypeResp
			{
				NumberOfAppointments = appointmentManager.GetNumberOfAppointmentsWithAppType(Request.AppTypeId)
			};
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00005624 File Offset: 0x00003824
		public SwapAppointmentTypeForAllAppointmentsResp SwapAppointmentTypeForAllAppointments(SwapAppointmentTypeForAllAppointmentsReq Request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(Request.GetOperationContext());
			appointmentManager.SwapAppointmentTypeForAllAppointments(Request.AppTypeIdToReplace, Request.AppTypeIdToKeep);
			return new SwapAppointmentTypeForAllAppointmentsResp();
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x0000565C File Offset: 0x0000385C
		public LoadAppointmentsWithSpecialPermissionsResp LoadAppointmentsWithSpecialPermissions(LoadAppointmentsWithSpecialPermissionsReq Request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(Request.GetOperationContext());
			IAppointmentManager appointmentManager2 = appointmentManager;
			IList<int> personIds = Request.PersonIds;
			List<int> personIds2 = (personIds != null) ? personIds.ToList<int>() : null;
			IList<int> appTypeIds = Request.AppTypeIds;
			IDictionary<int, IList<eAppointmentPermissionRestriction>> permissionRestrictions;
			IList<Appointment> list = appointmentManager2.LoadAppointmentsWithSpecialPermissions(personIds2, (appTypeIds != null) ? appTypeIds.ToList<int>() : null, Request.HideCancelled, Request.StartDateTime, Request.StartDateTime.AddDays((double)(Request.NumDays - 1)), out permissionRestrictions);
			LoadAppointmentsWithSpecialPermissionsResp loadAppointmentsWithSpecialPermissionsResp = new LoadAppointmentsWithSpecialPermissionsResp();
			IList<AppointmentDTO> appointments;
			if (list == null)
			{
				appointments = null;
			}
			else
			{
				appointments = (from g in list
				select g.ToDTO()).ToList<AppointmentDTO>();
			}
			loadAppointmentsWithSpecialPermissionsResp.Appointments = appointments;
			loadAppointmentsWithSpecialPermissionsResp.PermissionRestrictions = permissionRestrictions;
			return loadAppointmentsWithSpecialPermissionsResp;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00005710 File Offset: 0x00003910
		public LoadAppointmentWithSpecialPermissionsResp LoadAppointmentWithSpecialPermissions(LoadAppointmentWithSpecialPermissionsReq Request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(Request.GetOperationContext());
			IList<eAppointmentPermissionRestriction> permissionRestrictions;
			Appointment appointment = appointmentManager.LoadAppointmentWithSpecialPermissions(Request.AppointmentId, out permissionRestrictions);
			return new LoadAppointmentWithSpecialPermissionsResp
			{
				Appointment = ((appointment != null) ? appointment.ToDTO() : null),
				PermissionRestrictions = permissionRestrictions
			};
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00005760 File Offset: 0x00003960
		public UpdateAppointmentDateAndTimeResp UpdateAppointmentDateAndTime(UpdateAppointmentDateAndTimeReq Request)
		{
			IBaseAppointmentManager baseAppointmentManager = new BaseAppointmentManager(Request.GetOperationContext());
			baseAppointmentManager.UpdateDateAndTime(false, Request.AppointmentId, Request.NewStartDateTime, Request.NewEndDateTime);
			return new UpdateAppointmentDateAndTimeResp();
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x000057A0 File Offset: 0x000039A0
		public CancelAttendeeAppointmentResp CancelAttendeeAppointment(CancelAttendeeAppointmentReq Request)
		{
			IAppointmentManager appointmentManager = new AppointmentManager(Request.GetOperationContext());
			IAppointmentManager appointmentManager2 = appointmentManager;
			int appointmentId = Request.AppointmentId;
			int personId = Request.PersonId;
			AppCancelInfoDTO cancelInfo = Request.CancelInfo;
			appointmentManager2.CancelAttendeeAppointment(appointmentId, personId, (cancelInfo != null) ? cancelInfo.ToDomainObject() : null);
			return new CancelAttendeeAppointmentResp();
		}
	}
}
