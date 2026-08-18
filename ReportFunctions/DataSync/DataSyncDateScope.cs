using System;
using System.Collections.Generic;
using ClockWorkAPI;

namespace ReportFunctions.DataSync
{
	// Token: 0x0200005A RID: 90
	public class DataSyncDateScope
	{
		// Token: 0x170000FD RID: 253
		// (get) Token: 0x060004FD RID: 1277 RVA: 0x000537BC File Offset: 0x000527BC
		// (set) Token: 0x060004FE RID: 1278 RVA: 0x000537D4 File Offset: 0x000527D4
		public DateTime StartDate
		{
			get
			{
				return this.startDate;
			}
			set
			{
				this.startDate = value;
			}
		}

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x060004FF RID: 1279 RVA: 0x000537E0 File Offset: 0x000527E0
		// (set) Token: 0x06000500 RID: 1280 RVA: 0x000537F8 File Offset: 0x000527F8
		public DateTime EndDate
		{
			get
			{
				return this.endDate;
			}
			set
			{
				this.endDate = value;
			}
		}

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000501 RID: 1281 RVA: 0x00053804 File Offset: 0x00052804
		// (set) Token: 0x06000502 RID: 1282 RVA: 0x0005381C File Offset: 0x0005281C
		public List<Course> ExternalCourses
		{
			get
			{
				return this.externalCourses;
			}
			set
			{
				this.externalCourses = value;
			}
		}

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x06000503 RID: 1283 RVA: 0x00053828 File Offset: 0x00052828
		// (set) Token: 0x06000504 RID: 1284 RVA: 0x00053840 File Offset: 0x00052840
		public List<Course> ClockWorkCourses
		{
			get
			{
				return this.clockWorkCourses;
			}
			set
			{
				this.clockWorkCourses = value;
			}
		}

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x06000505 RID: 1285 RVA: 0x0005384C File Offset: 0x0005284C
		// (set) Token: 0x06000506 RID: 1286 RVA: 0x00053864 File Offset: 0x00052864
		public List<DataSyncCourse> ActionCourses
		{
			get
			{
				return this.actionCourses;
			}
			set
			{
				this.actionCourses = value;
			}
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x0005386E File Offset: 0x0005286E
		public DataSyncDateScope()
		{
			ClockWorkCore.GetTermStartEndDates(out this.startDate, out this.endDate);
			this.externalCourses = new List<Course>();
			this.clockWorkCourses = new List<Course>();
			this.actionCourses = new List<DataSyncCourse>();
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000538AC File Offset: 0x000528AC
		public DataSyncDateScope(DateTime startDate, DateTime endDate)
		{
			this.startDate = startDate;
			this.endDate = endDate;
			this.externalCourses = new List<Course>();
			this.clockWorkCourses = new List<Course>();
			this.actionCourses = new List<DataSyncCourse>();
		}

		// Token: 0x06000509 RID: 1289 RVA: 0x000538E6 File Offset: 0x000528E6
		public DataSyncDateScope(DateTime middleDate)
		{
			ClockWorkCore.GetTermStartEndDates(middleDate, out this.startDate, out this.endDate);
			this.externalCourses = new List<Course>();
			this.clockWorkCourses = new List<Course>();
			this.actionCourses = new List<DataSyncCourse>();
		}

		// Token: 0x0600050A RID: 1290 RVA: 0x00053928 File Offset: 0x00052928
		public bool ShouldContain(Course course)
		{
			return !(course.EndDate.Date <= this.startDate.Date) && !(course.StartDate.Date >= this.endDate.Date);
		}

		// Token: 0x040002AA RID: 682
		private DateTime startDate;

		// Token: 0x040002AB RID: 683
		private DateTime endDate;

		// Token: 0x040002AC RID: 684
		private List<Course> externalCourses;

		// Token: 0x040002AD RID: 685
		private List<Course> clockWorkCourses;

		// Token: 0x040002AE RID: 686
		private List<DataSyncCourse> actionCourses;
	}
}
