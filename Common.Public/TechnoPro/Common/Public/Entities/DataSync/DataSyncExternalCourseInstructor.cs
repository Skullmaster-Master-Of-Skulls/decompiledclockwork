using System;
using TechnoPro.Common.Public.Entities.LookupCourses;

namespace TechnoPro.Common.Public.Entities.DataSync
{
	// Token: 0x020003D1 RID: 977
	public class DataSyncExternalCourseInstructor : BusinessBase<string>, IComparable
	{
		// Token: 0x17000C58 RID: 3160
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x00021912 File Offset: 0x0001FB12
		// (set) Token: 0x06001DE7 RID: 7655 RVA: 0x0002191A File Offset: 0x0001FB1A
		public virtual string ExternalInstructorId { get; set; }

		// Token: 0x17000C59 RID: 3161
		// (get) Token: 0x06001DE8 RID: 7656 RVA: 0x00021923 File Offset: 0x0001FB23
		// (set) Token: 0x06001DE9 RID: 7657 RVA: 0x0002192B File Offset: 0x0001FB2B
		public LookupInstructor ClockWorkInstructor { get; set; }

		// Token: 0x17000C5A RID: 3162
		// (get) Token: 0x06001DEA RID: 7658 RVA: 0x00021934 File Offset: 0x0001FB34
		// (set) Token: 0x06001DEB RID: 7659 RVA: 0x0002193C File Offset: 0x0001FB3C
		public string Name { get; set; }

		// Token: 0x17000C5B RID: 3163
		// (get) Token: 0x06001DEC RID: 7660 RVA: 0x00021945 File Offset: 0x0001FB45
		// (set) Token: 0x06001DED RID: 7661 RVA: 0x0002194D File Offset: 0x0001FB4D
		public string Email { get; set; }

		// Token: 0x17000C5C RID: 3164
		// (get) Token: 0x06001DEE RID: 7662 RVA: 0x00021956 File Offset: 0x0001FB56
		// (set) Token: 0x06001DEF RID: 7663 RVA: 0x0002195E File Offset: 0x0001FB5E
		public string Username { get; set; }

		// Token: 0x17000C5D RID: 3165
		// (get) Token: 0x06001DF0 RID: 7664 RVA: 0x00021967 File Offset: 0x0001FB67
		// (set) Token: 0x06001DF1 RID: 7665 RVA: 0x0002196F File Offset: 0x0001FB6F
		public string EmployeeId { get; set; }

		// Token: 0x17000C5E RID: 3166
		// (get) Token: 0x06001DF2 RID: 7666 RVA: 0x00021978 File Offset: 0x0001FB78
		// (set) Token: 0x06001DF3 RID: 7667 RVA: 0x00021980 File Offset: 0x0001FB80
		public string Phone { get; set; }

		// Token: 0x17000C5F RID: 3167
		// (get) Token: 0x06001DF4 RID: 7668 RVA: 0x00021989 File Offset: 0x0001FB89
		// (set) Token: 0x06001DF5 RID: 7669 RVA: 0x00021991 File Offset: 0x0001FB91
		public bool IsPrimary { get; set; }

		// Token: 0x17000C60 RID: 3168
		// (get) Token: 0x06001DF6 RID: 7670 RVA: 0x0002199A File Offset: 0x0001FB9A
		// (set) Token: 0x06001DF7 RID: 7671 RVA: 0x000219A2 File Offset: 0x0001FBA2
		public int Percentage { get; set; }

		// Token: 0x06001DF8 RID: 7672 RVA: 0x000219AC File Offset: 0x0001FBAC
		public bool IsSameAs(DataSyncExternalCourseInstructor item)
		{
			bool flag = item == null;
			return !flag && this.CompareTo(item) == 0;
		}

		// Token: 0x06001DF9 RID: 7673 RVA: 0x000219D4 File Offset: 0x0001FBD4
		public int CompareTo(object obj)
		{
			bool flag = obj == null || !(obj is DataSyncExternalCourseInstructor);
			int result;
			if (flag)
			{
				result = -1;
			}
			else
			{
				DataSyncExternalCourseInstructor dataSyncExternalCourseInstructor = (DataSyncExternalCourseInstructor)obj;
				string text = this.Name ?? "";
				string text2 = dataSyncExternalCourseInstructor.Name ?? "";
				bool flag2 = !text.Trim().Equals(text2.Trim(), StringComparison.OrdinalIgnoreCase);
				if (flag2)
				{
					result = text.CompareTo(text2);
				}
				else
				{
					bool flag3 = !this.StringsAreEqualIgnoreCase(this.Email, dataSyncExternalCourseInstructor.Email);
					if (flag3)
					{
						result = 1;
					}
					else
					{
						bool flag4 = !this.StringsAreEqualIgnoreCase(this.Username, dataSyncExternalCourseInstructor.Username);
						if (flag4)
						{
							result = 1;
						}
						else
						{
							bool flag5 = !this.StringsAreEqualIgnoreCase(this.EmployeeId, dataSyncExternalCourseInstructor.EmployeeId);
							if (flag5)
							{
								result = 1;
							}
							else
							{
								bool flag6 = !this.StringsAreEqualIgnoreCase(this.Phone, dataSyncExternalCourseInstructor.Phone);
								if (flag6)
								{
									result = 1;
								}
								else
								{
									result = 0;
								}
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06001DFA RID: 7674 RVA: 0x00021AD8 File Offset: 0x0001FCD8
		private bool StringsAreEqualIgnoreCase(string s1, string s2)
		{
			bool flag = s1 == null && s2 == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = s1 == null || s2 == null;
				result = (!flag2 && s1.Trim().Equals(s2.Trim(), StringComparison.OrdinalIgnoreCase));
			}
			return result;
		}
	}
}
