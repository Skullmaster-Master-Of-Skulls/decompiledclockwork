using System;
using System.Collections.Generic;
using TechnoPro.Common.Public.Entities.DataSync.DataSyncCourses;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003D2 RID: 978
	public class DataSyncExternalCourseRowPart
	{
		// Token: 0x17000C61 RID: 3169
		// (get) Token: 0x06001DFC RID: 7676 RVA: 0x00021B20 File Offset: 0x0001FD20
		// (set) Token: 0x06001DFD RID: 7677 RVA: 0x00021B28 File Offset: 0x0001FD28
		public DateTime StartDate { get; set; }

		// Token: 0x17000C62 RID: 3170
		// (get) Token: 0x06001DFE RID: 7678 RVA: 0x00021B31 File Offset: 0x0001FD31
		// (set) Token: 0x06001DFF RID: 7679 RVA: 0x00021B39 File Offset: 0x0001FD39
		public DateTime EndDate { get; set; }

		// Token: 0x17000C63 RID: 3171
		// (get) Token: 0x06001E00 RID: 7680 RVA: 0x00021B42 File Offset: 0x0001FD42
		// (set) Token: 0x06001E01 RID: 7681 RVA: 0x00021B4A File Offset: 0x0001FD4A
		public string ExternalCourseId { get; set; }

		// Token: 0x17000C64 RID: 3172
		// (get) Token: 0x06001E02 RID: 7682 RVA: 0x00021B53 File Offset: 0x0001FD53
		// (set) Token: 0x06001E03 RID: 7683 RVA: 0x00021B5B File Offset: 0x0001FD5B
		public string Duration { get; set; }

		// Token: 0x17000C65 RID: 3173
		// (get) Token: 0x06001E04 RID: 7684 RVA: 0x00021B64 File Offset: 0x0001FD64
		// (set) Token: 0x06001E05 RID: 7685 RVA: 0x00021B6C File Offset: 0x0001FD6C
		public string Term { get; set; }

		// Token: 0x17000C66 RID: 3174
		// (get) Token: 0x06001E06 RID: 7686 RVA: 0x00021B75 File Offset: 0x0001FD75
		// (set) Token: 0x06001E07 RID: 7687 RVA: 0x00021B7D File Offset: 0x0001FD7D
		public string Subject { get; set; }

		// Token: 0x17000C67 RID: 3175
		// (get) Token: 0x06001E08 RID: 7688 RVA: 0x00021B86 File Offset: 0x0001FD86
		// (set) Token: 0x06001E09 RID: 7689 RVA: 0x00021B8E File Offset: 0x0001FD8E
		public string SubjectLong { get; set; }

		// Token: 0x17000C68 RID: 3176
		// (get) Token: 0x06001E0A RID: 7690 RVA: 0x00021B97 File Offset: 0x0001FD97
		// (set) Token: 0x06001E0B RID: 7691 RVA: 0x00021B9F File Offset: 0x0001FD9F
		public string Course { get; set; }

		// Token: 0x17000C69 RID: 3177
		// (get) Token: 0x06001E0C RID: 7692 RVA: 0x00021BA8 File Offset: 0x0001FDA8
		// (set) Token: 0x06001E0D RID: 7693 RVA: 0x00021BB0 File Offset: 0x0001FDB0
		public string Section { get; set; }

		// Token: 0x17000C6A RID: 3178
		// (get) Token: 0x06001E0E RID: 7694 RVA: 0x00021BB9 File Offset: 0x0001FDB9
		// (set) Token: 0x06001E0F RID: 7695 RVA: 0x00021BC1 File Offset: 0x0001FDC1
		public string TimeOfDay { get; set; }

		// Token: 0x17000C6B RID: 3179
		// (get) Token: 0x06001E10 RID: 7696 RVA: 0x00021BCA File Offset: 0x0001FDCA
		// (set) Token: 0x06001E11 RID: 7697 RVA: 0x00021BD2 File Offset: 0x0001FDD2
		public string Campus { get; set; }

		// Token: 0x17000C6C RID: 3180
		// (get) Token: 0x06001E12 RID: 7698 RVA: 0x00021BDB File Offset: 0x0001FDDB
		// (set) Token: 0x06001E13 RID: 7699 RVA: 0x00021BE3 File Offset: 0x0001FDE3
		public string Department { get; set; }

		// Token: 0x17000C6D RID: 3181
		// (get) Token: 0x06001E14 RID: 7700 RVA: 0x00021BEC File Offset: 0x0001FDEC
		// (set) Token: 0x06001E15 RID: 7701 RVA: 0x00021BF4 File Offset: 0x0001FDF4
		public string Location { get; set; }

		// Token: 0x17000C6E RID: 3182
		// (get) Token: 0x06001E16 RID: 7702 RVA: 0x00021BFD File Offset: 0x0001FDFD
		// (set) Token: 0x06001E17 RID: 7703 RVA: 0x00021C05 File Offset: 0x0001FE05
		public DataSyncExternalCourseStudentSpecificRowPart StudentSpecificInfo { get; set; }

		// Token: 0x17000C6F RID: 3183
		// (get) Token: 0x06001E18 RID: 7704 RVA: 0x00021C0E File Offset: 0x0001FE0E
		// (set) Token: 0x06001E19 RID: 7705 RVA: 0x00021C16 File Offset: 0x0001FE16
		public string CourseNote { get; set; }

		// Token: 0x17000C70 RID: 3184
		// (get) Token: 0x06001E1A RID: 7706 RVA: 0x00021C1F File Offset: 0x0001FE1F
		// (set) Token: 0x06001E1B RID: 7707 RVA: 0x00021C27 File Offset: 0x0001FE27
		public DataSyncExternalCourseInstructor Instructor { get; set; }

		// Token: 0x17000C71 RID: 3185
		// (get) Token: 0x06001E1C RID: 7708 RVA: 0x00021C30 File Offset: 0x0001FE30
		// (set) Token: 0x06001E1D RID: 7709 RVA: 0x00021C38 File Offset: 0x0001FE38
		public List<DataSyncExternalCourseTimetableItem> TimetableItems { get; set; }

		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x06001E1E RID: 7710 RVA: 0x00021C41 File Offset: 0x0001FE41
		// (set) Token: 0x06001E1F RID: 7711 RVA: 0x00021C49 File Offset: 0x0001FE49
		public IList<DataSyncExternalCourseFinalExamInfo> FinalExamInfos { get; set; }

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06001E20 RID: 7712 RVA: 0x00021C52 File Offset: 0x0001FE52
		// (set) Token: 0x06001E21 RID: 7713 RVA: 0x00021C5A File Offset: 0x0001FE5A
		public decimal Credits { get; set; }
	}
}
