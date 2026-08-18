using System;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace TechnoPro.Common.ClientManager.Notifications.AlternateFormatNotifications
{
	// Token: 0x02000027 RID: 39
	public class StudentMediaRequestNotificationManager
	{
		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600011D RID: 285 RVA: 0x0000421B File Offset: 0x0000241B
		public static StudentMediaRequestNotificationManager Current
		{
			get
			{
				if (StudentMediaRequestNotificationManager._currentInstance == null)
				{
					StudentMediaRequestNotificationManager._currentInstance = new StudentMediaRequestNotificationManager();
				}
				return StudentMediaRequestNotificationManager._currentInstance;
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x000028FC File Offset: 0x00000AFC
		protected StudentMediaRequestNotificationManager()
		{
		}

		// Token: 0x14000018 RID: 24
		// (add) Token: 0x0600011F RID: 287 RVA: 0x00004234 File Offset: 0x00002434
		// (remove) Token: 0x06000120 RID: 288 RVA: 0x0000426C File Offset: 0x0000246C
		public event EventHandler<EventArgs> OnToBeApprovedStudentMediaRequestListChanged;

		// Token: 0x06000121 RID: 289 RVA: 0x000042A4 File Offset: 0x000024A4
		internal void FireOnToBeApprovedStudentMediaRequestListChanged()
		{
			EventHandler<EventArgs> onToBeApprovedStudentMediaRequestListChanged = this.OnToBeApprovedStudentMediaRequestListChanged;
			if (onToBeApprovedStudentMediaRequestListChanged != null)
			{
				onToBeApprovedStudentMediaRequestListChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000042C8 File Offset: 0x000024C8
		public Task NotifyOnToBeApprovedStudentMediaRequestListChangedAsync()
		{
			StudentMediaRequestNotificationManager.<NotifyOnToBeApprovedStudentMediaRequestListChangedAsync>d__8 <NotifyOnToBeApprovedStudentMediaRequestListChangedAsync>d__;
			<NotifyOnToBeApprovedStudentMediaRequestListChangedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOnToBeApprovedStudentMediaRequestListChangedAsync>d__.<>4__this = this;
			<NotifyOnToBeApprovedStudentMediaRequestListChangedAsync>d__.<>1__state = -1;
			<NotifyOnToBeApprovedStudentMediaRequestListChangedAsync>d__.<>t__builder.Start<StudentMediaRequestNotificationManager.<NotifyOnToBeApprovedStudentMediaRequestListChangedAsync>d__8>(ref <NotifyOnToBeApprovedStudentMediaRequestListChangedAsync>d__);
			return <NotifyOnToBeApprovedStudentMediaRequestListChangedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x14000019 RID: 25
		// (add) Token: 0x06000123 RID: 291 RVA: 0x0000430C File Offset: 0x0000250C
		// (remove) Token: 0x06000124 RID: 292 RVA: 0x00004344 File Offset: 0x00002544
		public event EventHandler<EventArgs> OnInProgressStudentMediaRequestListChanged;

		// Token: 0x06000125 RID: 293 RVA: 0x0000437C File Offset: 0x0000257C
		internal void FireOnInProgressStudentMediaRequestListChanged()
		{
			EventHandler<EventArgs> onInProgressStudentMediaRequestListChanged = this.OnInProgressStudentMediaRequestListChanged;
			if (onInProgressStudentMediaRequestListChanged != null)
			{
				onInProgressStudentMediaRequestListChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000126 RID: 294 RVA: 0x000043A0 File Offset: 0x000025A0
		public Task NotifyOnInProgressStudentMediaRequestListChangedAsync()
		{
			StudentMediaRequestNotificationManager.<NotifyOnInProgressStudentMediaRequestListChangedAsync>d__13 <NotifyOnInProgressStudentMediaRequestListChangedAsync>d__;
			<NotifyOnInProgressStudentMediaRequestListChangedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOnInProgressStudentMediaRequestListChangedAsync>d__.<>4__this = this;
			<NotifyOnInProgressStudentMediaRequestListChangedAsync>d__.<>1__state = -1;
			<NotifyOnInProgressStudentMediaRequestListChangedAsync>d__.<>t__builder.Start<StudentMediaRequestNotificationManager.<NotifyOnInProgressStudentMediaRequestListChangedAsync>d__13>(ref <NotifyOnInProgressStudentMediaRequestListChangedAsync>d__);
			return <NotifyOnInProgressStudentMediaRequestListChangedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x1400001A RID: 26
		// (add) Token: 0x06000127 RID: 295 RVA: 0x000043E4 File Offset: 0x000025E4
		// (remove) Token: 0x06000128 RID: 296 RVA: 0x0000441C File Offset: 0x0000261C
		public event EventHandler<EventArgs> OnCompletedStudentMediaRequestListChanged;

		// Token: 0x06000129 RID: 297 RVA: 0x00004454 File Offset: 0x00002654
		internal void FireOnCompletedStudentMediaRequestListChanged()
		{
			EventHandler<EventArgs> onCompletedStudentMediaRequestListChanged = this.OnCompletedStudentMediaRequestListChanged;
			if (onCompletedStudentMediaRequestListChanged != null)
			{
				onCompletedStudentMediaRequestListChanged(this, EventArgs.Empty);
			}
		}

		// Token: 0x0600012A RID: 298 RVA: 0x00004478 File Offset: 0x00002678
		public Task NotifyOnCompletedStudentMediaRequestListChangedAsync()
		{
			StudentMediaRequestNotificationManager.<NotifyOnCompletedStudentMediaRequestListChangedAsync>d__18 <NotifyOnCompletedStudentMediaRequestListChangedAsync>d__;
			<NotifyOnCompletedStudentMediaRequestListChangedAsync>d__.<>t__builder = AsyncTaskMethodBuilder.Create();
			<NotifyOnCompletedStudentMediaRequestListChangedAsync>d__.<>4__this = this;
			<NotifyOnCompletedStudentMediaRequestListChangedAsync>d__.<>1__state = -1;
			<NotifyOnCompletedStudentMediaRequestListChangedAsync>d__.<>t__builder.Start<StudentMediaRequestNotificationManager.<NotifyOnCompletedStudentMediaRequestListChangedAsync>d__18>(ref <NotifyOnCompletedStudentMediaRequestListChangedAsync>d__);
			return <NotifyOnCompletedStudentMediaRequestListChangedAsync>d__.<>t__builder.Task;
		}

		// Token: 0x04000077 RID: 119
		private static StudentMediaRequestNotificationManager _currentInstance;
	}
}
