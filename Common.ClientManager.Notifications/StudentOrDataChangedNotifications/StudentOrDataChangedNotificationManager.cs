using System;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace TechnoPro.Common.ClientManager.Notifications.StudentOrDataChangedNotifications
{
	// Token: 0x02000005 RID: 5
	public class StudentOrDataChangedNotificationManager : IDisposable
	{
		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600002B RID: 43 RVA: 0x0000292E File Offset: 0x00000B2E
		public static StudentOrDataChangedNotificationManager Instance
		{
			get
			{
				if (StudentOrDataChangedNotificationManager._instance == null)
				{
					StudentOrDataChangedNotificationManager._instance = new StudentOrDataChangedNotificationManager();
				}
				return StudentOrDataChangedNotificationManager._instance;
			}
		}

		// Token: 0x0600002D RID: 45 RVA: 0x00002946 File Offset: 0x00000B46
		public void Dispose()
		{
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600002E RID: 46 RVA: 0x00002948 File Offset: 0x00000B48
		// (remove) Token: 0x0600002F RID: 47 RVA: 0x00002980 File Offset: 0x00000B80
		public event EventHandler<CurrentStudentEventArgs> OnCurrentStudentChangeRequested;

		// Token: 0x14000006 RID: 6
		// (add) Token: 0x06000030 RID: 48 RVA: 0x000029B8 File Offset: 0x00000BB8
		// (remove) Token: 0x06000031 RID: 49 RVA: 0x000029F0 File Offset: 0x00000BF0
		public event EventHandler<SummaryManagementsUpdateEventArgs> OnSummaryManagementsUpdateRequired;

		// Token: 0x14000007 RID: 7
		// (add) Token: 0x06000032 RID: 50 RVA: 0x00002A28 File Offset: 0x00000C28
		// (remove) Token: 0x06000033 RID: 51 RVA: 0x00002A60 File Offset: 0x00000C60
		public event EventHandler<ManualDataSyncRequestedArgs> OnManualDataSyncRequested;

		// Token: 0x14000008 RID: 8
		// (add) Token: 0x06000034 RID: 52 RVA: 0x00002A98 File Offset: 0x00000C98
		// (remove) Token: 0x06000035 RID: 53 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public event EventHandler<OpenCurrentStudentsProfileArgs> OnOpenCurrentStudentsProfileRequested;

		// Token: 0x14000009 RID: 9
		// (add) Token: 0x06000036 RID: 54 RVA: 0x00002B08 File Offset: 0x00000D08
		// (remove) Token: 0x06000037 RID: 55 RVA: 0x00002B40 File Offset: 0x00000D40
		public event EventHandler<ShowStudentInfoRequestedArgs> OnShowStudentInfoRequested;

		// Token: 0x1400000A RID: 10
		// (add) Token: 0x06000038 RID: 56 RVA: 0x00002B78 File Offset: 0x00000D78
		// (remove) Token: 0x06000039 RID: 57 RVA: 0x00002BB0 File Offset: 0x00000DB0
		public event EventHandler<ShowStudentInfoRequestedArgs> OnDeleteStudentRequested;

		// Token: 0x1400000B RID: 11
		// (add) Token: 0x0600003A RID: 58 RVA: 0x00002BE8 File Offset: 0x00000DE8
		// (remove) Token: 0x0600003B RID: 59 RVA: 0x00002C20 File Offset: 0x00000E20
		public event EventHandler OnRefreshCurrentStudentRequested;

		// Token: 0x1400000C RID: 12
		// (add) Token: 0x0600003C RID: 60 RVA: 0x00002C58 File Offset: 0x00000E58
		// (remove) Token: 0x0600003D RID: 61 RVA: 0x00002C90 File Offset: 0x00000E90
		public event EventHandler<ShowNewStudentFormRequestedArgs> OnShowNewStudentFormRequested;

		// Token: 0x0600003E RID: 62 RVA: 0x00002CC8 File Offset: 0x00000EC8
		private void FireOnShowNewStudentFormRequested(Func<PersonBaseDTO, bool> studentAddedResult)
		{
			EventHandler<ShowNewStudentFormRequestedArgs> onShowNewStudentFormRequested = this.OnShowNewStudentFormRequested;
			if (onShowNewStudentFormRequested == null)
			{
				return;
			}
			ShowNewStudentFormRequestedArgs e = new ShowNewStudentFormRequestedArgs
			{
				StudentAddedResult = studentAddedResult
			};
			onShowNewStudentFormRequested(this, e);
		}

		// Token: 0x0600003F RID: 63 RVA: 0x00002CF8 File Offset: 0x00000EF8
		private void FireOnRefreshCurrentStudentRequested()
		{
			EventHandler onRefreshCurrentStudentRequested = this.OnRefreshCurrentStudentRequested;
			if (onRefreshCurrentStudentRequested != null)
			{
				onRefreshCurrentStudentRequested(this, EventArgs.Empty);
			}
		}

		// Token: 0x06000040 RID: 64 RVA: 0x00002D1C File Offset: 0x00000F1C
		private void FireOnSummaryManagementsUpdateRequired(int screenNum)
		{
			EventHandler<SummaryManagementsUpdateEventArgs> onSummaryManagementsUpdateRequired = this.OnSummaryManagementsUpdateRequired;
			if (onSummaryManagementsUpdateRequired != null)
			{
				onSummaryManagementsUpdateRequired(this, new SummaryManagementsUpdateEventArgs
				{
					ScreenNum = screenNum
				});
			}
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002D48 File Offset: 0x00000F48
		private void FireOnCurrentStudentChangeRequested(int PersonId, bool registerStudentChangeEventWithSystem, bool rememberLastSelectedPid)
		{
			EventHandler<CurrentStudentEventArgs> onCurrentStudentChangeRequested = this.OnCurrentStudentChangeRequested;
			if (onCurrentStudentChangeRequested != null)
			{
				onCurrentStudentChangeRequested(this, new CurrentStudentEventArgs
				{
					PersonId = PersonId,
					RegisterStudentChangeEventWithSystem = registerStudentChangeEventWithSystem,
					RememberLastSelectedPid = rememberLastSelectedPid
				});
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x00002D80 File Offset: 0x00000F80
		private void FireOnManualDataSyncRequested(string studentNumber, bool syncData, bool syncCourses)
		{
			EventHandler<ManualDataSyncRequestedArgs> onManualDataSyncRequested = this.OnManualDataSyncRequested;
			if (onManualDataSyncRequested != null)
			{
				onManualDataSyncRequested(this, new ManualDataSyncRequestedArgs
				{
					StudentNumber = studentNumber,
					SyncData = syncData,
					SyncCourses = syncCourses
				});
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002DB8 File Offset: 0x00000FB8
		private void FireOnOpenCurrentStudentsProfileRequested(int screenNum, string title)
		{
			EventHandler<OpenCurrentStudentsProfileArgs> onOpenCurrentStudentsProfileRequested = this.OnOpenCurrentStudentsProfileRequested;
			if (onOpenCurrentStudentsProfileRequested != null)
			{
				onOpenCurrentStudentsProfileRequested(this, new OpenCurrentStudentsProfileArgs
				{
					ScreenNum = screenNum,
					Title = title
				});
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002DEC File Offset: 0x00000FEC
		private void FireOnShowStudentInfoRequested(PersonBaseDTO student)
		{
			EventHandler<ShowStudentInfoRequestedArgs> onShowStudentInfoRequested = this.OnShowStudentInfoRequested;
			if (onShowStudentInfoRequested != null)
			{
				onShowStudentInfoRequested(this, new ShowStudentInfoRequestedArgs
				{
					Student = student
				});
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002E18 File Offset: 0x00001018
		private void FireOnDeleteStudentRequested(PersonBaseDTO student)
		{
			EventHandler<ShowStudentInfoRequestedArgs> onDeleteStudentRequested = this.OnDeleteStudentRequested;
			if (onDeleteStudentRequested != null)
			{
				onDeleteStudentRequested(this, new ShowStudentInfoRequestedArgs
				{
					Student = student
				});
			}
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002E42 File Offset: 0x00001042
		public void NotifyShowNewStudentFormRequested(Func<PersonBaseDTO, bool> studentAddedResult)
		{
			this.FireOnShowNewStudentFormRequested(studentAddedResult);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x00002E4B File Offset: 0x0000104B
		public void NotifyRefreshCurrentStudentRequested()
		{
			this.FireOnRefreshCurrentStudentRequested();
		}

		// Token: 0x06000048 RID: 72 RVA: 0x00002E53 File Offset: 0x00001053
		public void NotifyShowStudentInfoRequested()
		{
			this.FireOnShowStudentInfoRequested(null);
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00002E5C File Offset: 0x0000105C
		public void NotifyDeleteStudentRequested(PersonBaseDTO student)
		{
			this.FireOnDeleteStudentRequested(student);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00002E65 File Offset: 0x00001065
		public void NotifyShowStudentInfoRequested(PersonBaseDTO student)
		{
			this.FireOnShowStudentInfoRequested(student);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00002E6E File Offset: 0x0000106E
		public void NotifyOpenCurrentStudentsProfileRequested(int screenNum, string title)
		{
			this.FireOnOpenCurrentStudentsProfileRequested(screenNum, title);
		}

		// Token: 0x0600004C RID: 76 RVA: 0x00002E78 File Offset: 0x00001078
		public void NotifyManualDataSyncRequested(string studentNumber)
		{
			this.NotifyManualDataSyncRequested(studentNumber, true, true);
		}

		// Token: 0x0600004D RID: 77 RVA: 0x00002E83 File Offset: 0x00001083
		public void NotifyManualDataSyncRequested(string studentNumber, bool syncData, bool syncCourses)
		{
			this.FireOnManualDataSyncRequested(studentNumber, syncData, syncCourses);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x00002E8E File Offset: 0x0000108E
		public void NotifySummaryManagementsUpdateRequired(int screenNum)
		{
			this.FireOnSummaryManagementsUpdateRequired(screenNum);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00002E97 File Offset: 0x00001097
		public void NotifyCurrentStudentNeedsToBeChanged(int PersonId)
		{
			this.NotifyCurrentStudentNeedsToBeChanged(PersonId, true, true);
		}

		// Token: 0x06000050 RID: 80 RVA: 0x00002EA2 File Offset: 0x000010A2
		public void NotifyCurrentStudentNeedsToBeChanged(int PersonId, bool registerStudentChangeEventWithSystem)
		{
			this.FireOnCurrentStudentChangeRequested(PersonId, registerStudentChangeEventWithSystem, false);
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00002EAD File Offset: 0x000010AD
		public void NotifyCurrentStudentNeedsToBeChanged(int PersonId, bool registerStudentChangeEventWithSystem, bool rememberLastSelectedPid)
		{
			this.FireOnCurrentStudentChangeRequested(PersonId, registerStudentChangeEventWithSystem, rememberLastSelectedPid);
		}

		// Token: 0x0400000A RID: 10
		private static StudentOrDataChangedNotificationManager _instance;
	}
}
