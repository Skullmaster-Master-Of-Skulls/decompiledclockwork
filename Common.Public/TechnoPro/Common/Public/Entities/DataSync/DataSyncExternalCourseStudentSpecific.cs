using System;
using TechnoPro.Common.Public.Entities.CourseRegistrations;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003CA RID: 970
	public class DataSyncExternalCourseStudentSpecific
	{
		// Token: 0x17000C4A RID: 3146
		// (get) Token: 0x06001DC6 RID: 7622 RVA: 0x0002168B File Offset: 0x0001F88B
		// (set) Token: 0x06001DC7 RID: 7623 RVA: 0x00021693 File Offset: 0x0001F893
		public string GradeLetter { get; set; }

		// Token: 0x17000C4B RID: 3147
		// (get) Token: 0x06001DC8 RID: 7624 RVA: 0x0002169C File Offset: 0x0001F89C
		// (set) Token: 0x06001DC9 RID: 7625 RVA: 0x000216A4 File Offset: 0x0001F8A4
		public string InProgressGradeLetter { get; set; }

		// Token: 0x17000C4C RID: 3148
		// (get) Token: 0x06001DCA RID: 7626 RVA: 0x000216AD File Offset: 0x0001F8AD
		// (set) Token: 0x06001DCB RID: 7627 RVA: 0x000216B5 File Offset: 0x0001F8B5
		public decimal Grade { get; set; }

		// Token: 0x17000C4D RID: 3149
		// (get) Token: 0x06001DCC RID: 7628 RVA: 0x000216BE File Offset: 0x0001F8BE
		// (set) Token: 0x06001DCD RID: 7629 RVA: 0x000216C6 File Offset: 0x0001F8C6
		public decimal InProgressGrade { get; set; }

		// Token: 0x17000C4E RID: 3150
		// (get) Token: 0x06001DCE RID: 7630 RVA: 0x000216CF File Offset: 0x0001F8CF
		// (set) Token: 0x06001DCF RID: 7631 RVA: 0x000216D7 File Offset: 0x0001F8D7
		public double TuitionCost { get; set; }

		// Token: 0x17000C4F RID: 3151
		// (get) Token: 0x06001DD0 RID: 7632 RVA: 0x000216E0 File Offset: 0x0001F8E0
		// (set) Token: 0x06001DD1 RID: 7633 RVA: 0x000216E8 File Offset: 0x0001F8E8
		public DateTime? RegistrationDate { get; set; }

		// Token: 0x17000C50 RID: 3152
		// (get) Token: 0x06001DD2 RID: 7634 RVA: 0x000216F1 File Offset: 0x0001F8F1
		// (set) Token: 0x06001DD3 RID: 7635 RVA: 0x000216F9 File Offset: 0x0001F8F9
		public string RegistrationNote { get; set; }

		// Token: 0x17000C51 RID: 3153
		// (get) Token: 0x06001DD4 RID: 7636 RVA: 0x00021702 File Offset: 0x0001F902
		public bool IsEmpty
		{
			get
			{
				return this.IsEqualTo(null);
			}
		}

		// Token: 0x06001DD5 RID: 7637 RVA: 0x0002170C File Offset: 0x0001F90C
		private static bool AreCourseSpecificStringsEqual(string s1, string s2)
		{
			return (s1 ?? "").Trim().Equals((s2 ?? "").Trim(), StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001DD6 RID: 7638 RVA: 0x00021744 File Offset: 0x0001F944
		public bool IsEqualTo(CourseStudentSpecific courseRegStudentSpecific)
		{
			bool flag = courseRegStudentSpecific == null;
			if (flag)
			{
				courseRegStudentSpecific = new CourseStudentSpecific();
			}
			return DataSyncExternalCourseStudentSpecific.AreCourseSpecificStringsEqual(this.GradeLetter, courseRegStudentSpecific.GradeLetter) && DataSyncExternalCourseStudentSpecific.AreCourseSpecificStringsEqual(this.InProgressGradeLetter, courseRegStudentSpecific.InProgressGradeLetter) && this.Grade == courseRegStudentSpecific.Grade && this.InProgressGrade == courseRegStudentSpecific.InProgressGrade && Math.Abs(this.TuitionCost - courseRegStudentSpecific.TuitionCost) < 1E-06 && DataSyncExternalCourseStudentSpecific.AreNullableDatesEqual(this.RegistrationDate, courseRegStudentSpecific.RegistrationDate) && DataSyncExternalCourseStudentSpecific.AreCourseSpecificStringsEqual(this.RegistrationNote, courseRegStudentSpecific.RegistrationNote);
		}

		// Token: 0x06001DD7 RID: 7639 RVA: 0x000217F8 File Offset: 0x0001F9F8
		private static bool AreNullableDatesEqual(DateTime? d1, DateTime? d2)
		{
			bool flag = d1 == null && d2 == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = d1 == null || d2 == null;
				result = (!flag2 && (d1.Value.Year == d2.Value.Year && d1.Value.Month == d2.Value.Month) && d1.Value.Day == d2.Value.Day);
			}
			return result;
		}
	}
}
