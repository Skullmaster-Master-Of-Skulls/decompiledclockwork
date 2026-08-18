using System;
using ClockWorkAPI;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;

namespace ReportFunctions.DataSync
{
	// Token: 0x0200004C RID: 76
	public class DataSyncCourse
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x0004C028 File Offset: 0x0004B028
		// (set) Token: 0x06000448 RID: 1096 RVA: 0x0004C040 File Offset: 0x0004B040
		public Course ExternalCourse
		{
			get
			{
				return this.externalCourse;
			}
			set
			{
				this.externalCourse = value;
			}
		}

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0004C04C File Offset: 0x0004B04C
		// (set) Token: 0x0600044A RID: 1098 RVA: 0x0004C064 File Offset: 0x0004B064
		public Course ClockWorkCourse
		{
			get
			{
				return this.clockWorkCourse;
			}
			set
			{
				this.clockWorkCourse = value;
			}
		}

		// Token: 0x170000CC RID: 204
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0004C070 File Offset: 0x0004B070
		// (set) Token: 0x0600044C RID: 1100 RVA: 0x0004C088 File Offset: 0x0004B088
		public PersonBaseDTO Student
		{
			get
			{
				return this.student;
			}
			set
			{
				this.student = value;
			}
		}

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x0004C094 File Offset: 0x0004B094
		// (set) Token: 0x0600044E RID: 1102 RVA: 0x0004C0AC File Offset: 0x0004B0AC
		public DataSyncCourseCompletionStatus Completed
		{
			get
			{
				return this.completed;
			}
			set
			{
				this.completed = value;
			}
		}

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0004C0B8 File Offset: 0x0004B0B8
		// (set) Token: 0x06000450 RID: 1104 RVA: 0x0004C0D0 File Offset: 0x0004B0D0
		public DataSyncCourseError DataSyncError
		{
			get
			{
				return this.dataSyncError;
			}
			set
			{
				this.dataSyncError = value;
			}
		}

		// Token: 0x170000CF RID: 207
		// (get) Token: 0x06000451 RID: 1105 RVA: 0x0004C0DC File Offset: 0x0004B0DC
		// (set) Token: 0x06000452 RID: 1106 RVA: 0x0004C0F4 File Offset: 0x0004B0F4
		public DataSyncCourseAction PendingDataSyncAction
		{
			get
			{
				return this.pendingDataSyncAction;
			}
			set
			{
				this.pendingDataSyncAction = value;
			}
		}

		// Token: 0x0400025D RID: 605
		private Course externalCourse;

		// Token: 0x0400025E RID: 606
		private Course clockWorkCourse;

		// Token: 0x0400025F RID: 607
		private PersonBaseDTO student;

		// Token: 0x04000260 RID: 608
		private DataSyncCourseCompletionStatus completed = DataSyncCourseCompletionStatus.Pending;

		// Token: 0x04000261 RID: 609
		private DataSyncCourseError dataSyncError = null;

		// Token: 0x04000262 RID: 610
		private DataSyncCourseAction pendingDataSyncAction = DataSyncCourseAction.Unknown;
	}
}
