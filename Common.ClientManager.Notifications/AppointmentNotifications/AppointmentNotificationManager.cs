using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using TechnoPro.ClockWorkServer.Contracts.DTO.Appointments;
using TechnoPro.ClockWorkServer.Contracts.DTO.AppointmentsCalendar;

namespace TechnoPro.Common.ClientManager.Notifications.AppointmentNotifications
{
	// Token: 0x0200001D RID: 29
	public class AppointmentNotificationManager
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x00003886 File Offset: 0x00001A86
		public static AppointmentNotificationManager CurrentInstance
		{
			get
			{
				if (AppointmentNotificationManager._currentInstance == null)
				{
					AppointmentNotificationManager._currentInstance = new AppointmentNotificationManager();
				}
				return AppointmentNotificationManager._currentInstance;
			}
		}

		// Token: 0x14000010 RID: 16
		// (add) Token: 0x060000D1 RID: 209 RVA: 0x000038A0 File Offset: 0x00001AA0
		// (remove) Token: 0x060000D2 RID: 210 RVA: 0x000038D8 File Offset: 0x00001AD8
		public event EventHandler<CalendarRefreshRequiredEventArgs> OnCalendarRefreshRequired;

		// Token: 0x060000D3 RID: 211 RVA: 0x0000390D File Offset: 0x00001B0D
		private void FireOnCalendarRefreshRequired(CalendarRefreshRequiredEventArgs e)
		{
			EventHandler<CalendarRefreshRequiredEventArgs> onCalendarRefreshRequired = this.OnCalendarRefreshRequired;
			if (onCalendarRefreshRequired == null)
			{
				return;
			}
			onCalendarRefreshRequired(this, e);
		}

		// Token: 0x14000011 RID: 17
		// (add) Token: 0x060000D4 RID: 212 RVA: 0x00003924 File Offset: 0x00001B24
		// (remove) Token: 0x060000D5 RID: 213 RVA: 0x0000395C File Offset: 0x00001B5C
		public event EventHandler<AppointmentDoubleBookingEventArgs> OnCheckIfAlreadyBookingNewAppointmentSlot;

		// Token: 0x060000D6 RID: 214 RVA: 0x00003994 File Offset: 0x00001B94
		private void FireOnCheckIfAlreadyBookingNewAppointmentSlot(BasicAppointmentInfo appInfo, string guid)
		{
			EventHandler<AppointmentDoubleBookingEventArgs> onCheckIfAlreadyBookingNewAppointmentSlot = this.OnCheckIfAlreadyBookingNewAppointmentSlot;
			if (onCheckIfAlreadyBookingNewAppointmentSlot == null)
			{
				return;
			}
			onCheckIfAlreadyBookingNewAppointmentSlot(this, new AppointmentDoubleBookingEventArgs
			{
				PersonId = ((appInfo.AttendeePersonIds != null && appInfo.AttendeePersonIds.Count > 0) ? appInfo.AttendeePersonIds[0] : 0),
				StartDateTime = appInfo.StartDateTime,
				EndDateTime = appInfo.EndDateTime,
				Guid = guid
			});
		}

		// Token: 0x14000012 RID: 18
		// (add) Token: 0x060000D7 RID: 215 RVA: 0x00003A04 File Offset: 0x00001C04
		// (remove) Token: 0x060000D8 RID: 216 RVA: 0x00003A3C File Offset: 0x00001C3C
		public event EventHandler<AppointmentDoubleBookingWarningEventArgs> OnSomeoneElseIsAlreadyBookingThisAppointment;

		// Token: 0x060000D9 RID: 217 RVA: 0x00003A71 File Offset: 0x00001C71
		private void FireOnSomeoneElseIsAlreadyBookingThisAppointment(string guid)
		{
			EventHandler<AppointmentDoubleBookingWarningEventArgs> onSomeoneElseIsAlreadyBookingThisAppointment = this.OnSomeoneElseIsAlreadyBookingThisAppointment;
			if (onSomeoneElseIsAlreadyBookingThisAppointment == null)
			{
				return;
			}
			onSomeoneElseIsAlreadyBookingThisAppointment(this, new AppointmentDoubleBookingWarningEventArgs
			{
				Guid = guid
			});
		}

		// Token: 0x060000DA RID: 218 RVA: 0x00003A90 File Offset: 0x00001C90
		public Task NotifyOthersAppointmentChangedOrDeletedAsync(params AppointmentForNotificationDTO[] appointmentsForNotification)
		{
			AppointmentNotificationManager.<NotifyOthersAppointmentChangedOrDeletedAsync>d__15 <NotifyOthersAppointmentChangedOrDeletedAsync>d__;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>4__this = this;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.appointmentsForNotification = appointmentsForNotification;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>1__state = -1;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>t__builder.Start<AppointmentNotificationManager.<NotifyOthersAppointmentChangedOrDeletedAsync>d__15>(ref <NotifyOthersAppointmentChangedOrDeletedAsync>d__);
			return <NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003ADC File Offset: 0x00001CDC
		public Task NotifyOthersAppointmentChangedOrDeletedAsync(AppointmentDTO Appointment)
		{
			AppointmentNotificationManager.<NotifyOthersAppointmentChangedOrDeletedAsync>d__16 <NotifyOthersAppointmentChangedOrDeletedAsync>d__;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>4__this = this;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.Appointment = Appointment;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>1__state = -1;
			<NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>t__builder.Start<AppointmentNotificationManager.<NotifyOthersAppointmentChangedOrDeletedAsync>d__16>(ref <NotifyOthersAppointmentChangedOrDeletedAsync>d__);
			return <NotifyOthersAppointmentChangedOrDeletedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00003B28 File Offset: 0x00001D28
		public Task NotifyOthersAppointmentWasCreatedAsync(AppointmentDTO Appointment)
		{
			AppointmentNotificationManager.<NotifyOthersAppointmentWasCreatedAsync>d__17 <NotifyOthersAppointmentWasCreatedAsync>d__;
			<NotifyOthersAppointmentWasCreatedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOthersAppointmentWasCreatedAsync>d__.<>4__this = this;
			<NotifyOthersAppointmentWasCreatedAsync>d__.Appointment = Appointment;
			<NotifyOthersAppointmentWasCreatedAsync>d__.<>1__state = -1;
			<NotifyOthersAppointmentWasCreatedAsync>d__.<>t__builder.Start<AppointmentNotificationManager.<NotifyOthersAppointmentWasCreatedAsync>d__17>(ref <NotifyOthersAppointmentWasCreatedAsync>d__);
			return <NotifyOthersAppointmentWasCreatedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00003B74 File Offset: 0x00001D74
		public Task NotifyAsync(AppNotificationMessage msg, string Guid = null)
		{
			AppointmentNotificationManager.<NotifyAsync>d__18 <NotifyAsync>d__;
			<NotifyAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyAsync>d__.<>4__this = this;
			<NotifyAsync>d__.msg = msg;
			<NotifyAsync>d__.Guid = Guid;
			<NotifyAsync>d__.<>1__state = -1;
			<NotifyAsync>d__.<>t__builder.Start<AppointmentNotificationManager.<NotifyAsync>d__18>(ref <NotifyAsync>d__);
			return <NotifyAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00003BC8 File Offset: 0x00001DC8
		private Task NotifyOtherClockWorksThatImAlreadyEditingThisAppointmentAsync(string guid)
		{
			AppointmentNotificationManager.<NotifyOtherClockWorksThatImAlreadyEditingThisAppointmentAsync>d__19 <NotifyOtherClockWorksThatImAlreadyEditingThisAppointmentAsync>d__;
			<NotifyOtherClockWorksThatImAlreadyEditingThisAppointmentAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOtherClockWorksThatImAlreadyEditingThisAppointmentAsync>d__.guid = guid;
			<NotifyOtherClockWorksThatImAlreadyEditingThisAppointmentAsync>d__.<>1__state = -1;
			<NotifyOtherClockWorksThatImAlreadyEditingThisAppointmentAsync>d__.<>t__builder.Start<AppointmentNotificationManager.<NotifyOtherClockWorksThatImAlreadyEditingThisAppointmentAsync>d__19>(ref <NotifyOtherClockWorksThatImAlreadyEditingThisAppointmentAsync>d__);
			return <NotifyOtherClockWorksThatImAlreadyEditingThisAppointmentAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00003C0C File Offset: 0x00001E0C
		private Task NotifyOtherClockWorksRegardingAppointmentCreationModificationOrDeletionAsync(BasicAppointmentInfo Appointment)
		{
			AppointmentNotificationManager.<NotifyOtherClockWorksRegardingAppointmentCreationModificationOrDeletionAsync>d__20 <NotifyOtherClockWorksRegardingAppointmentCreationModificationOrDeletionAsync>d__;
			<NotifyOtherClockWorksRegardingAppointmentCreationModificationOrDeletionAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOtherClockWorksRegardingAppointmentCreationModificationOrDeletionAsync>d__.Appointment = Appointment;
			<NotifyOtherClockWorksRegardingAppointmentCreationModificationOrDeletionAsync>d__.<>1__state = -1;
			<NotifyOtherClockWorksRegardingAppointmentCreationModificationOrDeletionAsync>d__.<>t__builder.Start<AppointmentNotificationManager.<NotifyOtherClockWorksRegardingAppointmentCreationModificationOrDeletionAsync>d__20>(ref <NotifyOtherClockWorksRegardingAppointmentCreationModificationOrDeletionAsync>d__);
			return <NotifyOtherClockWorksRegardingAppointmentCreationModificationOrDeletionAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00003C50 File Offset: 0x00001E50
		private Task NotifyAppointmentCreateStartedAsync(BasicAppointmentInfo appInfo, string Guid)
		{
			AppointmentNotificationManager.<NotifyAppointmentCreateStartedAsync>d__21 <NotifyAppointmentCreateStartedAsync>d__;
			<NotifyAppointmentCreateStartedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyAppointmentCreateStartedAsync>d__.appInfo = appInfo;
			<NotifyAppointmentCreateStartedAsync>d__.Guid = Guid;
			<NotifyAppointmentCreateStartedAsync>d__.<>1__state = -1;
			<NotifyAppointmentCreateStartedAsync>d__.<>t__builder.Start<AppointmentNotificationManager.<NotifyAppointmentCreateStartedAsync>d__21>(ref <NotifyAppointmentCreateStartedAsync>d__);
			return <NotifyAppointmentCreateStartedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00003C9B File Offset: 0x00001E9B
		private void NotifyAppointmentCreateEnded(IList<BasicAppointmentInfo> appInfos, string Guid)
		{
			this.FireOnCalendarRefreshRequired(new CalendarRefreshRequiredEventArgs
			{
				AppointmentInfos = appInfos
			});
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00003CAF File Offset: 0x00001EAF
		private void NotifyAppointmentDeleted(BasicAppointmentInfo appInfo)
		{
			this.FireOnCalendarRefreshRequired(new CalendarRefreshRequiredEventArgs
			{
				AppointmentInfos = new List<BasicAppointmentInfo>
				{
					appInfo
				}
			});
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00002946 File Offset: 0x00000B46
		private void NotifyAppointmentUpdateStarted(BasicAppointmentInfo appinfo, string Guid)
		{
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00003CAF File Offset: 0x00001EAF
		private void NotifyAppointmentUpdateEnded(BasicAppointmentInfo appInfo, string Guid)
		{
			this.FireOnCalendarRefreshRequired(new CalendarRefreshRequiredEventArgs
			{
				AppointmentInfos = new List<BasicAppointmentInfo>
				{
					appInfo
				}
			});
		}

		// Token: 0x04000053 RID: 83
		private static AppointmentNotificationManager _currentInstance;
	}
}
